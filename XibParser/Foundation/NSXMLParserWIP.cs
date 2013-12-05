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
        public uint parserflasg; // 0x0C
        public NSError error; //0x10
        public NSMutableArray namespaces; //0x14
        public id/*NSMapTable*/ slowStringMap; //0x18
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
        private bool _isCleanedUp;

        static volatile bool _isInited;
        static object syncRoot = new Object();

        //C# specific 
        List<GCHandle> _pinnedHandles = new List<GCHandle>();

        IntPtr _reserved0; //0x04
        id _delegate; //0x08
        NSXMLParserInfo _reserved1; //0x0C
        IntPtr _reserved2; // 0x10
        IntPtr _reserved3; //0x14
        
        NSData _data;


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

            GCHandle handle;

            _reserved1 = (NSXMLParserInfo)NSXMLParserInfo.Alloc().Init();
            _reserved1.saxHandler = new LibXml.xmlSAXHandler();
            _pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler, GCHandleType.Pinned));

            //_reserved1.saxHandler.internalSubset = IntPtr.Zero;
            //_reserved1.saxHandler.isStandalone = IntPtr.Zero;
            //_reserved1.saxHandler.hasInternalSubset = IntPtr.Zero;
            //_reserved1.saxHandler.hasExternalSubset = IntPtr.Zero;
            //_reserved1.saxHandler.resolveEntity = IntPtr.Zero;
            //_reserved1.saxHandler.getEntity = IntPtr.Zero;
            //_reserved1.saxHandler.entityDecl = IntPtr.Zero;
            //_reserved1.saxHandler.notationDecl = IntPtr.Zero;
            //_reserved1.saxHandler.attributeDecl = IntPtr.Zero;
            //_reserved1.saxHandler.elementDecl = IntPtr.Zero;
            //_reserved1.saxHandler.unparsedEntityDecl = IntPtr.Zero;
            //_reserved1.saxHandler.setDocumentLocator = IntPtr.Zero;
            _reserved1.saxHandler.startDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(startDocumentSAXFunc));
            _reserved1.saxHandler.endDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(endDocumentSAXFunc));
            //_reserved1.saxHandler.startElement = IntPtr.Zero;
            //_reserved1.saxHandler.endElement = IntPtr.Zero;
            //_reserved1.saxHandler.reference = IntPtr.Zero;
            //_reserved1.saxHandler.characters = IntPtr.Zero;
            //_reserved1.saxHandler.ignorableWhitespace = IntPtr.Zero;
            //_reserved1.saxHandler.processingInstruction = IntPtr.Zero;
            //_reserved1.saxHandler.comment = IntPtr.Zero;
            //_reserved1.saxHandler.warning = IntPtr.Zero;
            //_reserved1.saxHandler.error = IntPtr.Zero;
            //_reserved1.saxHandler.fatalError = IntPtr.Zero; /* unused error() get all the errors */
            //_reserved1.saxHandler.getParameterEntity = IntPtr.Zero;
            //_reserved1.saxHandler.cdataBlock = IntPtr.Zero;
            //_reserved1.saxHandler.externalSubset = IntPtr.Zero;
            //_reserved1.saxHandler.initialized = 0;

            if (_reserved1.saxHandler.GetType() == typeof(LibXml.xmlSAXHandler))
            {
                _reserved1.saxHandler.initialized = LibXml.XML_SAX2_MAGIC;
                //_reserved1.saxHandler._private = IntPtr.Zero; 
                //_reserved1.saxHandler.startElementNs = IntPtr.Zero; 
                //_reserved1.saxHandler.endElementNs = IntPtr.Zero;
                //_reserved1.saxHandler.serror = IntPtr.Zero;
            }

            //_reserved1.saxHandler.internalSubset = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.internalSubset, GCHandleType.Pinned));
            //_reserved1.saxHandler.isStandalone = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.isStandalone, GCHandleType.Pinned));
            //_reserved1.saxHandler.hasInternalSubset = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.hasInternalSubset, GCHandleType.Pinned));
            //_reserved1.saxHandler.hasExternalSubset = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.hasExternalSubset, GCHandleType.Pinned));
            //_reserved1.saxHandler.resolveEntity = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.resolveEntity, GCHandleType.Pinned));
            //_reserved1.saxHandler.getEntity = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.getEntity, GCHandleType.Pinned));
            //_reserved1.saxHandler.entityDecl = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.entityDecl, GCHandleType.Pinned));
            //_reserved1.saxHandler.notationDecl = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.notationDecl, GCHandleType.Pinned));
            //_reserved1.saxHandler.attributeDecl = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.attributeDecl, GCHandleType.Pinned));
            //_reserved1.saxHandler.elementDecl = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.elementDecl, GCHandleType.Pinned));
            //_reserved1.saxHandler.unparsedEntityDecl = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.unparsedEntityDecl, GCHandleType.Pinned));
            //_reserved1.saxHandler.setDocumentLocator = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.setDocumentLocator, GCHandleType.Pinned));
            //_reserved1.saxHandler.startDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(startDocumentSAXFunc));
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.startDocument, GCHandleType.Pinned));
            //_reserved1.saxHandler.endDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(endDocumentSAXFunc));
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.endDocument, GCHandleType.Pinned));
            //_reserved1.saxHandler.startElement = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.startElement, GCHandleType.Pinned));
            //_reserved1.saxHandler.endElement = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.endElement, GCHandleType.Pinned));
            //_reserved1.saxHandler.reference = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.reference, GCHandleType.Pinned));
            //_reserved1.saxHandler.characters = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.characters, GCHandleType.Pinned));
            //_reserved1.saxHandler.ignorableWhitespace = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.ignorableWhitespace, GCHandleType.Pinned));
            //_reserved1.saxHandler.processingInstruction = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.processingInstruction, GCHandleType.Pinned));
            //_reserved1.saxHandler.comment = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.comment, GCHandleType.Pinned));
            //_reserved1.saxHandler.warning = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.warning, GCHandleType.Pinned));
            //_reserved1.saxHandler.error = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.error, GCHandleType.Pinned));
            //_reserved1.saxHandler.fatalError = IntPtr.Zero; /* unused error() get all the errors */
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.fatalError, GCHandleType.Pinned));
            //_reserved1.saxHandler.getParameterEntity = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.getParameterEntity, GCHandleType.Pinned));
            //_reserved1.saxHandler.cdataBlock = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.cdataBlock, GCHandleType.Pinned));
            //_reserved1.saxHandler.externalSubset = IntPtr.Zero;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.externalSubset, GCHandleType.Pinned));

            
            _reserved1.saxHandler.initialized = 0xDEEDBEAF;
            //_pinnedHandles.Add(GCHandle.Alloc(_reserved1.saxHandler.initialized, GCHandleType.Pinned));

            // We add the '\0' to the xml data for libxml interop
            byte[] dst = new byte[data.Length + 1];
            Array.Copy(data.Bytes, 0, dst, 0, data.Length);

            _data = NSData.Alloc().InitWithBytes(dst);

            return self;
        }

       

        protected virtual void startDocumentSAXFunc(IntPtr ctx)
        {
            System.Diagnostics.Trace.WriteLine("startDocumentSAXFunc");
        }

        protected virtual void endDocumentSAXFunc(IntPtr ctx)
        {
            System.Diagnostics.Trace.WriteLine("endDocumentSAXFunc");
        }

        public virtual void Parse()
        {
            byte[] test = _data.Bytes;

            int iSizeOfXmlSAXHandler = Marshal.SizeOf(typeof(LibXml.xmlSAXHandler));
            IntPtr saxHandlerPtr = Marshal.AllocHGlobal(iSizeOfXmlSAXHandler);
            Marshal.StructureToPtr(_reserved1.saxHandler, saxHandlerPtr, false);


            int result = LibXml.xmlSAXUserParseMemory(saxHandlerPtr, IntPtr.Zero, _data.Bytes, _data.Length);
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
                        LibXml.xmlCleanupParser();
                        foreach(GCHandle handle in _pinnedHandles)
                        {
                            handle.Free();
                        }
                    }
                }

                _isCleanedUp = true;
                _disposed = true;
            }
        }


    }
}
