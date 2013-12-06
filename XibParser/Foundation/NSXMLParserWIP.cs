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
    public class NSXMLParserInfo : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLParserInfo));
        new public static NSXMLParserInfo Alloc() { return new NSXMLParserInfo(); }

        public LibXml.xmlSAXHandler saxHandler; //0x04
        public IntPtr parserContext; //0x08
        public uint parserflags; // 0x0C
        public NSError error; //0x10
        public NSMutableArray namespaces; //0x14
        public NSMutableDictionary/*NSMapTable*/ slowStringMap; //0x18
        public bool delegateAborted; //0x1C
        public bool haveDetectedEncoding; //0x1D
        public NSData bomChunk; //0x20
        public int chunkSize; //0x24

        public NSXMLParserInfo()
        {
            slowStringMap = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
        }

    }

    public class NSXMLParserWIP : NSObject, IDisposable
    {
        new public static Class Class = new Class(typeof(NSXMLParserWIP));
        new public static NSXMLParserWIP Alloc() { return new NSXMLParserWIP(); }

        private bool _disposed;

        
        private static volatile bool _isInited;
        private static volatile bool _isCleanedUp;
        private static object syncRoot = new Object();

        //C# specific 
        List<GCHandle> _pinnedHandles = new List<GCHandle>();

        IntPtr _reserved0; //0x04
        id _delegate; //0x08
        NSXMLParserInfo _reserved1; //0x0C
        NSData _reserved2; // 0x10
        IntPtr _reserved3; //0x14
        


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
            return false;
        }
        public virtual void SetShouldProcessNamespaces(bool shouldProcessNamespaces)
        {

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

                GCHandle handle;

                _reserved1 = (NSXMLParserInfo)NSXMLParserInfo.Alloc().Init();
                _reserved1.saxHandler = new LibXml.xmlSAXHandler();
                //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler, GCHandleType.Pinned));
                _reserved2 = data;

                InitializeSAX2Callbacks();
            }

            return self;
        }

        private void InitializeSAX2Callbacks()
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
        }

        public virtual void Parse()
        {
            int iSizeOfXmlSAXHandler = Marshal.SizeOf(typeof(LibXml.xmlSAXHandler));
            IntPtr saxHandlerPtr = Marshal.AllocHGlobal(iSizeOfXmlSAXHandler);
            Marshal.StructureToPtr(_reserved1.saxHandler, saxHandlerPtr, false);


            int result = LibXml.xmlSAXUserParseMemory(saxHandlerPtr, IntPtr.Zero, _reserved2.Bytes, _reserved2.Length);
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
        private unsafe void StartElementNs(IntPtr ctx, string localname, string prefix, string URI, int nb_namespaces, string[] namespaces, int nb_attributes, int nb_defaulted, string[] attributes)
        {
            bool shouldProcessNs = this.GetShouldProcessNamespaces();
            int prefixLen = LibXml.xmlStrlen(Encoding.UTF8.GetBytes(prefix));

            NSString var_r14 = null;
            if ((!shouldProcessNs) && (localname != null))
            {
                if (prefixLen != 0)
                    var_r14 = _NewColonSeparatedStringFromPrefixAndSuffix(prefix, localname);            
                else
                    var_r14 = _NSXMLParserNSStringFromBytes(localname, this.GetInfo());
                
            }
            


            //     if ((((shouldProcessNs == 0x0 ? 0xff : 0x0) & (len != 0x0 ? 0xff : 0x0)) == 0x0) && (localname != 0x0)) {
            //rax = [self _info];
            //rax = ___NSXMLParserNSStringFromBytes(r12, rax);
        }

        private NSString _NewColonSeparatedStringFromPrefixAndSuffix(NSString prefix, NSString localname)
        {
            NSString str = null;



            return str;
        }

        private string _NSXMLParserNSStringFromBytes(NSString localname, NSXMLParserInfo info)
        {
            NSString str = null;

            id tmpStr = null;
            if (info.slowStringMap.TryGetValue(localname, out tmpStr) == false)
            {
                
            }
            else
            {
                str = (NSString)tmpStr;
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
