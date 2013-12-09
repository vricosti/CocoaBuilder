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
        public int nestingLevel;

 

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

        IntPtr GetParserContext() 
        {
            return this._reserved1.parserContext;
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
                _reserved1.saxHandler.internalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.internalSubsetSAXFunc(_internalSubset2));
                _reserved1.saxHandler.isStandalone = Marshal.GetFunctionPointerForDelegate(new LibXml.isStandaloneSAXFunc(_isStandalone));
                _reserved1.saxHandler.hasInternalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.hasInternalSubsetSAXFunc(_hasInternalSubset2));
                _reserved1.saxHandler.hasExternalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.hasExternalSubsetSAXFunc(_hasExternalSubset2));
                _reserved1.saxHandler.resolveEntity = Marshal.GetFunctionPointerForDelegate(new LibXml.resolveEntitySAXFunc(_resolveEntity));
                _reserved1.saxHandler.getEntity = Marshal.GetFunctionPointerForDelegate(new LibXml.getEntitySAXFunc(_getEntity));
                _reserved1.saxHandler.entityDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.entityDeclSAXFunc(_entityDecl));
                _reserved1.saxHandler.notationDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.notationDeclSAXFunc(_notationDecl));
                _reserved1.saxHandler.attributeDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.attributeDeclSAXFunc(_AttributeDecl));
                _reserved1.saxHandler.elementDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.elementDeclSAXFunc(_elementDecl));
                _reserved1.saxHandler.unparsedEntityDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.unparsedEntityDeclSAXFunc(_unparsedEntityDecl));
                _reserved1.saxHandler.startDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_startDocument));
                _reserved1.saxHandler.endDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_endDocument));
                _reserved1.saxHandler.startElementNs = Marshal.GetFunctionPointerForDelegate(new LibXml.startElementNsSAX2Func(_startElementNs));
                _reserved1.saxHandler.endElementNs = Marshal.GetFunctionPointerForDelegate(new LibXml.endElementNsSAX2Func(_endElementNs));
                _reserved1.saxHandler.characters = Marshal.GetFunctionPointerForDelegate(new LibXml.charactersSAXFunc(_characters));
                _reserved1.saxHandler.error = Marshal.GetFunctionPointerForDelegate(new LibXml.errorSAXFunc(_errorCallback));
                _reserved1.saxHandler.getParameterEntity = Marshal.GetFunctionPointerForDelegate(new LibXml.getParameterEntitySAXFunc(_getParameterEntity));
                _reserved1.saxHandler.cdataBlock = Marshal.GetFunctionPointerForDelegate(new LibXml.cdataBlockSAXFunc(_cdataBlock));
                _reserved1.saxHandler.comment = Marshal.GetFunctionPointerForDelegate(new LibXml.commentSAXFunc(_comment));
                _reserved1.saxHandler.externalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.externalSubsetSAXFunc(_externalSubset2));
                _reserved1.saxHandler.initialized = LibXml.XML_SAX2_MAGIC;

                int iSizeOfXmlSAXHandler = Marshal.SizeOf(typeof(LibXml.xmlSAXHandler));
                _saxHandlerPtr = Marshal.AllocHGlobal(iSizeOfXmlSAXHandler);
                Marshal.StructureToPtr(_reserved1.saxHandler, _saxHandlerPtr, false);
            }
        }

        private unsafe void _cdataBlock(IntPtr ctx, IntPtr value, int len)
        {
            id dlegate = GetDelegate();
            if (dlegate != null && dlegate.RespondsToSelector(new SEL("ParserFoundCDATA")) == true)
            {
                NSString chars = (NSString)NSString.Alloc().InitWithBytes(value, (uint)len, NSStringEncoding.NSUTF8StringEncoding);
                Objc.MsgSend(dlegate, "ParserFoundCDATA", this, chars);
            }
            else
            {
                _characters(ctx, value, len);
            }
        }

        private unsafe Entity* _getParameterEntity(IntPtr ctx, IntPtr name)
        {
            throw new NotImplementedException();
        }

        private unsafe void _externalSubset2(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID)
        {
            LibXml.xmlSAX2ExternalSubset(GetParserContext(), name, ExternalID, SystemID);
        }

        private unsafe void _comment(IntPtr ctx, IntPtr value)
        {
            id dlegate = GetDelegate();
            if (dlegate != null && dlegate.RespondsToSelector(new SEL("ParserFoundComment")) == true)
            {
                NSString chars = null;
                if (value != IntPtr.Zero)
                {
                    chars = _NSXMLParserNSStringFromBytes(value, this.GetInfo());
                }
                Objc.MsgSend(dlegate, "ParserFoundComment", this, chars);
            }
        }

        private unsafe void _errorCallback(IntPtr ctx, IntPtr msg, params IntPtr[] prms)
        {
            throw new NotImplementedException();
        }

        private unsafe void _unparsedEntityDecl(IntPtr ctx, IntPtr name, IntPtr publicId, IntPtr systemId, IntPtr notationName)
        {
            throw new NotImplementedException();
        }

        private unsafe void _elementDecl(IntPtr ctx, IntPtr name, int type, IntPtr content)
        {
            id dlegate = GetDelegate();
            if (dlegate != null && dlegate.RespondsToSelector(new SEL("ParserFoundElementDeclarationWithName")) == true)
            {
                NSString chars = null;
                if (name != IntPtr.Zero)
                {
                    chars = _NSXMLParserNSStringFromBytes(name, this.GetInfo());
                }
                Objc.MsgSend(dlegate, "ParserFoundElementDeclarationWithName", this, chars, "");
            }
        }

        private unsafe void _AttributeDecl(IntPtr ctx, IntPtr pElem, IntPtr pFullname, int type, int def, IntPtr pDefaultValue, IntPtr tree)
        {
            id dlegate = GetDelegate();
            //parser:foundAttributeDeclarationWithName:forElement:type:defaultValue:
            if (dlegate != null && dlegate.RespondsToSelector(new SEL("ParserFoundAttributeDeclarationWithName")) == true)
            {
                NSString elem = null;
                if (pElem != IntPtr.Zero)
                {
                    elem = _NSXMLParserNSStringFromBytes(pElem, this.GetInfo());
                }
                NSString fullname = null;
                if (pFullname != IntPtr.Zero)
                {
                    fullname = _NSXMLParserNSStringFromBytes(pFullname, this.GetInfo());
                }
                NSString defaultValue = null;
                if (pDefaultValue != IntPtr.Zero)
                {
                    defaultValue = _NSXMLParserNSStringFromBytes(pDefaultValue, this.GetInfo());
                }
                Objc.MsgSend(dlegate, "ParserFoundAttributeDeclarationWithName", this, fullname, elem, type, defaultValue);

            }
            LibXml.xmlFreeEnumeration(tree);
        }

        private unsafe void _notationDecl(IntPtr ctx, IntPtr name, IntPtr publicId, IntPtr systemId)
        {
//            function __notationDecl {
//    var_8 = rcx;
//    r13 = rdx;
//    r15 = rsi;
//    rax = [rdi delegate];
//    var_16 = rax;
//    rax = [rax respondsToSelector:@selector(parser:foundNotationDeclarationWithName:publicID:systemID:)];
//    if (rax != 0x0) {
//            r12 = 0x0;
//            r14 = 0x0;
//            if (r15 != 0x0) {
//                    rax = [rbx _info];
//                    rax = ___NSXMLParserNSStringFromBytes(r15, rax);
//                    r14 = rax;
//            }
//            r15 = var_8;
//            if (r13 != 0x0) {
//                    rax = [rbx _info];
//                    rax = ___NSXMLParserNSStringFromBytes(r13, rax);
//                    r12 = rax;
//            }
//            r9 = 0x0;
//            if (r15 != 0x0) {
//                    rax = [rbx _info];
//                    rax = ___NSXMLParserNSStringFromBytes(r15, rax);
//                    r9 = rax;
//            }
//            rdi = var_16;
//            rdx = rbx;
//            rcx = r14;
//            r8 = r12;
//            rax = [rdi parser:rdx foundNotationDeclarationWithName:rcx publicID:r8 systemID:r9];
//    }
//    else {
//            return rax;
//    }
//    return rax;
//}

        }

        private unsafe void _entityDecl(IntPtr ctx, IntPtr name, int type, IntPtr publicId, IntPtr systemId, IntPtr content)
        {
            throw new NotImplementedException();
        }

        private unsafe Entity* _getEntity(IntPtr ctx, IntPtr name)
        {
            throw new NotImplementedException();
        }

        private unsafe Entity* _resolveEntity(IntPtr ctx, IntPtr publicId, IntPtr systemId)
        {
            throw new NotImplementedException();
        }

        private unsafe int _hasExternalSubset2(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe int _hasInternalSubset2(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe int _isStandalone(IntPtr ctx)
        {
            throw new NotImplementedException();
        }

        private unsafe void _internalSubset2(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID)
        {
            throw new NotImplementedException();
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
       

        

        private unsafe void _characters(IntPtr ctx, IntPtr ch, int len)
        {
            id dlegate = GetDelegate();
            if (dlegate == null || dlegate.RespondsToSelector(new SEL("ParserFoundCharacters")) == false)
                return;

            NSString chars = (NSString)NSString.Alloc().InitWithBytes(ch, (uint)len, NSStringEncoding.NSUTF8StringEncoding);
            Objc.MsgSend(this.GetDelegate(), "ParserFoundCharacters", this, chars);
        }

        private unsafe void _endElementNs(IntPtr ctx, IntPtr localname, IntPtr prefix, IntPtr URI)
        {
            throw new NotImplementedException();
        }
        private unsafe void _startElementNs(IntPtr ctx, IntPtr pLocalname, IntPtr pPrefix, IntPtr URI, int nb_namespaces, string[] namespaces, int nb_attributes, int nb_defaulted, string[] attributes)
        {
            GetInfo().nestingLevel++;
            bool shouldProcessNs = this.GetShouldProcessNamespaces();
            bool shouldReportNssPrefixes = this.GetShouldReportNamespacePrefixes();
            int prefixLen = LibXml.xmlStrlen(pLocalname);

            NSString var_r14 = null;
            if ((shouldProcessNs == false))
            {
                if (pLocalname != null)
                {
                    if (prefixLen != 0)
                        var_r14 = _NewColonSeparatedStringFromPrefixAndSuffix(pPrefix, pLocalname);
                    else
                        var_r14 = _NSXMLParserNSStringFromBytes(pLocalname, this.GetInfo());
                }
            }
            else
            {

            }

        }

        private NSString _NewColonSeparatedStringFromPrefixAndSuffix(IntPtr pPrefix, IntPtr pLocalname)
        {
            NSString str = null;

            string tmpStr = string.Format("{0}:{1}", pPrefix.GetString(), pLocalname.GetString());
            str = (NSString)tmpStr;

            return str;
        }

        private string _NSXMLParserNSStringFromBytes(IntPtr pLocalname, NSXMLParserInfo info)
        {
            NSString str = (NSString)NS.MapGet(info.slowStringMap, pLocalname);
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

       

       

        protected virtual void _startDocument(IntPtr ctx)
        {
            System.Diagnostics.Trace.WriteLine("startDocumentSAXFunc");
        }

        protected virtual void _endDocument(IntPtr ctx)
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
