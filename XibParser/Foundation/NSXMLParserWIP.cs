/*
* XibParser.
* Copyright (C) 2013 Smartmobili SARL
* 
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License as published by the Free Software Foundation; either
* version 2 of the License, or (at your option) any later version.
* 
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
* 
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, write to the
* Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
* Boston, MA  02110-1301, USA. 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Org.System.Xml.Sax;
using Org.System.Xml.Sax.Helpers;
using SaxConsts = Org.System.Xml.Sax.Constants;
using System.Runtime.InteropServices;
 



namespace Smartmobili.Cocoa
{
    public enum NSXMLParserFlags : uint
    {
        NSXMLParserFlagsNone = 0,
        NSXMLParserFlagsProcessNs = 4,
        NSXMLParserFlagsReportNsPrefixes = 8,
        NSXMLParserFlagsResolveExternalEntities = 16,
        NSXMLParserFlagsContinueAfterFatalError = 32
    }

    public class NSXMLParserInfo : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLParserInfo));
        new public static NSXMLParserInfo Alloc() { return new NSXMLParserInfo(); }

        public LibXml.xmlSAXHandler saxHandler; //0x04
        public IntPtr parserContext; //0x08
        public uint parserFlags; // 0x0C
        public NSError error; //0x10
        public NSMutableArray namespaces; //0x14
        public NSMapTable/*NSMapTable*/ slowStringMap; //0x18
        public bool delegateAborted; //0x1C
        public bool haveDetectedEncoding; //0x1D
        public NSData bomChunk; //0x20
        public int chunkSize; //0x24

 

    }

    public class NSXMLParserWIP : NSObject, IDisposable
    {
        new public static Class Class = new Class(typeof(NSXMLParserWIP));
        new public static NSXMLParserWIP Alloc() { return new NSXMLParserWIP(); }

        private bool _disposed;

        
        private static volatile bool _isInited;
        private static volatile bool _isCleanedUp;
        private static object syncRoot = new Object();

        //Unamanaged resources (TO RELEASE)
        GCHandle _pinnedInstance;
        IntPtr _instancePtr;
        IntPtr _saxHandlerPtr;

        IntPtr _xmlStructuredErrorFuncPtr; // Do not release

        List<GCHandle> _pinnedHandles = new List<GCHandle>();

        IntPtr _reserved0; //0x04
        id _delegate; //0x08
        NSXMLParserInfo _reserved1; //0x0C
        int _reserved2; // 0x10
        NSInputStream _reserved3; //0x14


        public NSXMLParserWIP()
        {
            _pinnedInstance = GCHandle.Alloc(this);
            _instancePtr = (IntPtr)_pinnedInstance;
        }
        public virtual id Delegate
        {
            get { return GetDelegate(); }
            set { SetDelegate(value); }
        }
        
        public virtual void SetDelegate(id dlgate) 
        { 
            _delegate = dlgate; 
        }
        
        public virtual id GetDelegate() 
        { 
            return _delegate; 
        }

        protected virtual NSXMLParserInfo GetInfo()
        {
            return this._reserved1;
        }

        public virtual bool GetShouldProcessNamespaces()
        {
            return GetParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsProcessNs);
        }
        public virtual void SetShouldProcessNamespaces(bool shouldProcessNamespaces)
        {
            SetParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsProcessNs, shouldProcessNamespaces);
        }

        public virtual bool GetShouldResolveExternalEntities()
        {
            return GetParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsResolveExternalEntities);
        }
        public virtual void SetShouldResolveExternalEntities(bool shouldResolveExternal)
        {
            SetParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsResolveExternalEntities, shouldResolveExternal);
        }

        public virtual bool GetShouldReportNamespacePrefixes()
        {
            return GetParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsReportNsPrefixes);
        }
        public virtual void SetShouldReportNamespacePrefixes(bool shouldReportNamespacePrefixes)
        {
            SetParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsReportNsPrefixes, shouldReportNamespacePrefixes);
        }



        private bool GetParserFlagValue(NSXMLParserFlags flag)
        {
            uint mask = (uint)flag;
            return ((this._reserved1.parserFlags & mask) != 0);
        }
        private void SetParserFlagValue(NSXMLParserFlags flag, bool value)
        {
            uint mask = (uint)flag;

            if (value == true)
                this._reserved1.parserFlags |= mask;
            else
                this._reserved1.parserFlags &= ~mask;
        }

        public static void SetupLibXml()
        {
            if (_isInited == false)
            {
                lock (syncRoot)
                {
                    if (_isInited == false)
                    {
                        LibXml.xmlInitParser();
                        _isInited = true;
                    }
                }
            }
        }


        public virtual id InitWithData(NSData data)
        {
            id self = this;

            SetupLibXml();

            if (base.Init() != null)
            {
                _reserved1 = (NSXMLParserInfo)NSXMLParserInfo.Alloc().Init();
                
                _reserved1.saxHandler = new LibXml.xmlSAXHandler();
                _xmlStructuredErrorFuncPtr = Marshal.GetFunctionPointerForDelegate(new LibXml.xmlStructuredErrorFunc(_StructuredErrorFunc));

                _reserved1.parserContext = IntPtr.Zero;
                _reserved1.error = null;
                _reserved1.parserFlags = 0;
                _reserved1.slowStringMap = (NSMapTable)NSMapTable.Alloc().Init();

                InitializeSAX2Callbacks();
                _reserved1.parserFlags |= 0x40;
                _reserved1.parserFlags |= 0x80;

                if (data != null)
                {
                    _reserved3 = NSInputStream.InputStreamWithData(data);
                    if (data.Length < 0x100000)
                        _reserved1.chunkSize = data.Length;
                    else
                        _reserved1.chunkSize = 0x100000 * 256;
                }
            }

            return self;
        }

         public virtual id InitWithStream(NSInputStream aStream)
         {
             id self = this;

             NSInputStream stream = InitForIncrementalParsing(aStream);
             if (stream != null)
             {
                 _reserved1.parserFlags |= 0x80;
                 _reserved3 = stream;
             }

             return self;
         }


         protected virtual NSInputStream InitForIncrementalParsing(NSInputStream stream)
        {
            id self = this.InitWithData(null);
            if (self != null)
            {
                //_reserved1.parserFlags = _reserved1.parserFlags & 0xffffffffffffff7f;
            }
            return stream;
        }

        private void InitializeSAX2Callbacks()
        {
            unsafe 
            { 
                _reserved1.saxHandler.internalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_InternalSubset2));
                _reserved1.saxHandler.isStandalone = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_IsStandalone));
                _reserved1.saxHandler.hasInternalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_HasInternalSubset2));
                _reserved1.saxHandler.hasExternalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_HasExternalSubset2));
                _reserved1.saxHandler.resolveEntity = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_ResolveEntity));
                _reserved1.saxHandler.getEntity = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_GetEntity));
                _reserved1.saxHandler.entityDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_EntityDecl));
                _reserved1.saxHandler.notationDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_NotationDecl));
                _reserved1.saxHandler.attributeDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_AttributeDecl));
                _reserved1.saxHandler.elementDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_ElementDecl));
                _reserved1.saxHandler.unparsedEntityDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_UnparsedEntityDecl));
                _reserved1.saxHandler.startDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(StartDocument));
                _reserved1.saxHandler.endDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(EndDocument));
                _reserved1.saxHandler.startElementNs = Marshal.GetFunctionPointerForDelegate(new LibXml.startElementNsSAX2Func(StartElementNs));
                _reserved1.saxHandler.endElementNs = Marshal.GetFunctionPointerForDelegate(new LibXml.endElementNsSAX2Func(EndElementNs));
                _reserved1.saxHandler.characters = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_Characters));
                _reserved1.saxHandler.error = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_ErrorCallback));
                _reserved1.saxHandler.getParameterEntity = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_GetParameterEntity));
                _reserved1.saxHandler.cdataBlock = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_CdataBlock));
                _reserved1.saxHandler.comment = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_Comment));
                _reserved1.saxHandler.externalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_ExternalSubset2));
                _reserved1.saxHandler.initialized = LibXml.XML_SAX2_MAGIC;

                int iSizeOfXmlSAXHandler = Marshal.SizeOf(typeof(LibXml.xmlSAXHandler));
                _saxHandlerPtr = Marshal.AllocHGlobal(iSizeOfXmlSAXHandler);
                Marshal.StructureToPtr(_reserved1.saxHandler, _saxHandlerPtr, false);
            }
        }

        public virtual bool Parse()
        {
            //NSInputStream stream = _reserved3;
            //stream.Open();
            //byte[] buffer = new byte[_reserved1.chunkSize];
            //int readBytes = stream.Read(buffer, _reserved1.chunkSize);
            //stream.Close();
            //int result = LibXml.xmlSAXUserParseMemory(_saxHandlerPtr, IntPtr.Zero, buffer, buffer.Length);
            //return true;

            return this.ParseFromStream();
        }

        protected virtual bool ParseFromStream()
        {
            bool result = true;

            NSInputStream stream = _reserved3;
            if (stream != null)
            {
                stream.Open();
                byte[] buffer = new byte[_reserved1.chunkSize];
                int readBytes = stream.Read(buffer, _reserved1.chunkSize);
                if (readBytes != -1)
                {
                    do
                    {
                        ParseData(NSData.DataWithBytes(buffer));
                        readBytes = stream.Read(buffer, _reserved1.chunkSize);
                    }
                    while (readBytes > 0);

                    result = FinishIncrementalParse();
                }
                stream.Close();
            }
            else
            {
                result = false;
                //*err = [NSError errorWithDomain:"NSCocoaErrorDomain" ,-1, userInfo:[NSDictionary dictionaryWithObjectsAndKeys:@"Could not open data stream",
                //sourcePath,destinationPath],NSLocalizedDescriptionKey,[outputStream streamError],NSUnderlyingErrorKey,nil]];
                //NSError.ErrorWithDomain("NSCocoaErrorDomain", -1, )
                //this._SetExpandedParserError();
            }

            return result;
        }

        protected virtual bool ParseData(NSData data)
        {
            bool result = false;

            if ((this._reserved1.parserFlags & 0x40) == 0)
            {

            }

            LibXml.xmlSetStructuredErrorFunc(_instancePtr, _xmlStructuredErrorFuncPtr);
            if (this._reserved1.haveDetectedEncoding == true)
            {
                result = this._HandleParseResult(LibXml.xmlParseChunk(_reserved1.parserContext, data.Bytes, data.Length, 0));
            }
            else
            {
                int bomChunkLen = (_reserved1.bomChunk != null) ? _reserved1.bomChunk.Length : 0;
                int dataLen = (data != null) ? data.Length : 0;

                if (bomChunkLen + dataLen <= 3)
                {
                    NSData chunkData = data;
                    if (_reserved1.bomChunk != null)
                    {
                        chunkData = (NSMutableData)NSMutableData.Alloc().Init();
                        ((NSMutableData)chunkData).AppendData(_reserved1.bomChunk);
                        ((NSMutableData)chunkData).AppendData(data);
                        _reserved1.bomChunk = chunkData;
                    }
                   
                   
                    result = true;
                }
                else
                {
                    NSData chunkData = data;
                    if (_reserved1.bomChunk != null)
                    {
                        chunkData = (NSMutableData)NSMutableData.Alloc().Init();
                        ((NSMutableData)chunkData).AppendData(_reserved1.bomChunk);
                        ((NSMutableData)chunkData).AppendData(data);
                    }

                   
                    IntPtr saxHandlerPtr = (_delegate != null) ? _saxHandlerPtr : IntPtr.Zero;
                    _reserved1.parserContext = LibXml.xmlCreatePushParserCtxt(saxHandlerPtr, _instancePtr, chunkData.Bytes, 4, null);

                    bool shouldResolveExternals = GetShouldResolveExternalEntities();
                    int parserFlags = (shouldResolveExternals) ? (int)LibXml.XmlParserOption.XML_PARSE_DTDLOAD : 0;
                    LibXml.xmlCtxtUseOptions(_reserved1.parserContext, parserFlags);
                    _reserved1.haveDetectedEncoding = true;
                    _reserved1.bomChunk = null;

                    if (bomChunkLen + dataLen >= 5)
                    {
                        byte[] dst = new byte[data.Length - 4];
                        Buffer.BlockCopy(data.Bytes, 4, dst, 0, data.Length - 4);
                        NSData tmpData = (NSMutableData)NSMutableData.Alloc().InitWithBytes(dst);
                        ParseData(tmpData);
                    }

                    result = true;
                }
            }

            LibXml.xmlSetStructuredErrorFunc(IntPtr.Zero, IntPtr.Zero);
            return result;
        }

        protected virtual void _StructuredErrorFunc(IntPtr userData, IntPtr error)
        {
            System.Diagnostics.Debug.WriteLine("_StructuredErrorFunc");
        }

        protected virtual bool _HandleParseResult(int xmlParserError)
        {
            return true;
        }

        protected virtual bool FinishIncrementalParse()
        {
            return this._HandleParseResult(LibXml.xmlParseChunk(_reserved1.parserContext,null,0,1));
        }

        protected virtual NSError _SetExpandedParserError(NSError error)
        {
            _reserved1.error = error;
            return _reserved1.error;
        }
       

        private unsafe void _ExternalSubset2(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _Comment(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _CdataBlock(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _GetParameterEntity(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _ErrorCallback(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _Characters(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void EndElementNs(IntPtr ctx, string localname, string prefix, string URI)
        {
            throw new NotImplementedException();
        }
        private unsafe void StartElementNs(IntPtr ctx, IntPtr localname, IntPtr prefix, IntPtr URI, int nb_namespaces, string[] namespaces, int nb_attributes, int nb_defaulted, string[] attributes)
        {

            //byte[] localNameData = localname.GetBytes();

            bool shouldProcessNs = this.GetShouldProcessNamespaces();
            int prefixLen = LibXml.xmlStrlen(localname);

            NSString var_r14 = null;
            if ((shouldProcessNs == false))
            {
                if (localname != null)
                {
                    if (prefixLen != 0)
                        var_r14 = _NewColonSeparatedStringFromPrefixAndSuffix(prefix, localname);
                    else
                        var_r14 = _NSXMLParserNSStringFromBytes(localname, this.GetInfo());
                }
            }
            else
            {

            }
            
          

            //     if ((((shouldProcessNs == 0x0 ? 0xff : 0x0) & (len != 0x0 ? 0xff : 0x0)) == 0x0) && (localname != 0x0)) {
            //rax = [self _info];
            //rax = ___NSXMLParserNSStringFromBytes(r12, rax);
        }

        private NSString _NewColonSeparatedStringFromPrefixAndSuffix(IntPtr pPrefix, IntPtr pLocalname)
        {
            NSString str = null;

            string prefix = Encoding.UTF8.GetString(pPrefix.GetBytes());
            string localname = Encoding.UTF8.GetString(pLocalname.GetBytes());
            string tmpStr = string.Format("{0}:{1}", prefix, localname);
            str = (NSString)tmpStr;

            return str;
        }

        private string _NSXMLParserNSStringFromBytes(IntPtr pLocalname, NSXMLParserInfo info)
        {
            NSString str = (NSString) NS.MapGet(info.slowStringMap, localname);
            if (str == null)
            {
                str = (NSString)NSString.AllocWithZone(null).InitWithBytes(pLocalname, (uint)pLocalname.strlen(), NSStringEncoding.NSUTF8StringEncoding);
                NS.MapInsertKnownAbsent(info.slowStringMap, pLocalname, str);
            }
           

            return str;
        }

        //private unsafe void _EndElementNs(IntPtr ctx)
        //{
        //    throw new NotImplementedException();
        //}

        //private unsafe void _StartElementNs(IntPtr ctx)
        //{
        //    bool shouldProcessNs = this.GetShouldProcessNamespaces();
        //    int len = LibXml.xmlStrlen();


        //    if ((((var_shouldProcessNs  == 0x0 ? 0xff : 0x0) & (len != 0x0 ? 0xff : 0x0)) == 0x0) && (r12 != 0x0)) {
        //    rax = [var_self _info];
        //    rax = ___NSXMLParserNSStringFromBytes(r12, rax);
        //    }
        //}

        private unsafe void _UnparsedEntityDecl(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _ElementDecl(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _AttributeDecl(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _NotationDecl(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _EntityDecl(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _GetEntity(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _ResolveEntity(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _HasExternalSubset2(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _HasInternalSubset2(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _IsStandalone(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _InternalSubset2(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

       

        protected virtual void StartDocument(IntPtr ctx)
        {
            System.Diagnostics.Trace.WriteLine("startDocumentSAXFunc");
        }

        protected virtual void EndDocument(IntPtr ctx)
        {
            System.Diagnostics.Trace.WriteLine("endDocumentSAXFunc");
        }

        

        ~NSXMLParserWIP()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //If you need thread safety, use a lock around these  
            //operations, as well as in your methods that use the resource. 
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_isCleanedUp != true)
                    {
                        //LibXml.xmlCleanupParser();
                        //foreach(GCHandle handle in _pinnedHandles)
                        //{
                        //    handle.Free();
                        //}
                    }
                }

                _isCleanedUp = true;
                _disposed = true;
            }
        }


    }
}
