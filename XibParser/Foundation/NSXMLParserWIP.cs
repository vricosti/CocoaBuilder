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
using xmlEntityPtr = System.IntPtr;
using xmlErrorPtr = System.IntPtr;


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
        new public static NSXMLParserInfo alloc() { return new NSXMLParserInfo(); }

        public LibXml.xmlSAXHandler saxHandler; //0x04
        public IntPtr parserContext; //0x08
        public uint parserFlags; // 0x0C
        public NSError error; //0x10
        public NSMutableArray namespaces; //0x14
        public NSMapTable/*NSMapTable*/ slowStringMap; //0x18
        public bool delegateAborted; //0x1C
        public bool haveDetectedEncoding; //0x1D
        public NSData bomChunk; //0x20
        public uint chunkSize; //0x24
        public int nestingLevel;

        

    }

    public class NSXMLParserWIP : NSObject, IDisposable
    {
        new public static Class Class = new Class(typeof(NSXMLParserWIP));
        new public static NSXMLParserWIP alloc() { return new NSXMLParserWIP(); }

        private bool _disposed;

        private static NSDictionary _emptyDic = (NSDictionary)NSDictionary.alloc().init();
        private static volatile bool _isInited;
        private static volatile bool _isCleanedUp;
        private static object syncRoot = new Object();

        //Unamanaged resources (TO RELEASE)
        GCHandle _instanceHandle;
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
            _instanceHandle = GCHandle.Alloc(this);
            _instancePtr = (IntPtr)_instanceHandle;
        }
        public virtual id Delegate
        {
            get { return getDelegate(); }
            set { setDelegate(value); }
        }

        public virtual void setDelegate(id dlgate)
        {
            _delegate = dlgate;
        }

        public virtual id getDelegate()
        {
            return _delegate;
        }

        protected virtual NSXMLParserInfo _info()
        {
            return this._reserved1;
        }

        public virtual bool shouldProcessNamespaces()
        {
            return parserFlagValue(NSXMLParserFlags.NSXMLParserFlagsProcessNs);
        }
        public virtual void setShouldProcessNamespaces(bool shouldProcessNamespaces)
        {
            setParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsProcessNs, shouldProcessNamespaces);
        }

        public virtual bool shouldResolveExternalEntities()
        {
            return parserFlagValue(NSXMLParserFlags.NSXMLParserFlagsResolveExternalEntities);
        }
        public virtual void setShouldResolveExternalEntities(bool shouldResolveExternal)
        {
            setParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsResolveExternalEntities, shouldResolveExternal);
        }

        public virtual bool shouldReportNamespacePrefixes()
        {
            return parserFlagValue(NSXMLParserFlags.NSXMLParserFlagsReportNsPrefixes);
        }
        public virtual void setShouldReportNamespacePrefixes(bool shouldReportNamespacePrefixes)
        {
            setParserFlagValue(NSXMLParserFlags.NSXMLParserFlagsReportNsPrefixes, shouldReportNamespacePrefixes);
        }

        IntPtr parserContext()
        {
            return this._reserved1.parserContext;
        }


        private bool parserFlagValue(NSXMLParserFlags flag)
        {
            uint mask = (uint)flag;
            return ((this._reserved1.parserFlags & mask) != 0);
        }
        private void setParserFlagValue(NSXMLParserFlags flag, bool value)
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


        public virtual id initWithData(NSData data)
        {
            SetupLibXml();

            id self = base.init();

            if (base.init() != null)
            {
                self = this;

                _reserved1 = (NSXMLParserInfo)NSXMLParserInfo.alloc().init();

                _reserved1.saxHandler = new LibXml.xmlSAXHandler();
                _xmlStructuredErrorFuncPtr = Marshal.GetFunctionPointerForDelegate(new LibXml.xmlStructuredErrorFunc(_StructuredErrorFunc));

                _reserved1.parserContext = IntPtr.Zero;
                _reserved1.error = null;
                _reserved1.parserFlags = 0;
                _reserved1.slowStringMap = (NSMapTable)NSMapTable.alloc().init();

                initializeSAX2Callbacks();
                _reserved1.parserFlags |= 0x40;
                _reserved1.parserFlags |= 0x80;
                _reserved1.namespaces = null;

                if (data != null)
                {
                    _reserved3 = NSInputStream.inputStreamWithData(data);
                    if (data.Length < 0x100000)
                        _reserved1.chunkSize = data.length();
                    else
                        _reserved1.chunkSize = 0x100000 * 256;
                }
            }

            return self;
        }

        public virtual id initWithStream(NSInputStream aStream)
        {
            id self = initForIncrementalParsing(aStream);
            if (self != null)
            {
                _reserved1.parserFlags |= 0x80;
                _reserved3 = (NSInputStream)aStream.retain();
            }

            return self;
        }


        protected virtual id initForIncrementalParsing(NSInputStream stream)
        {
            id self = this.initWithData(null);
            if (self != null)
            {
                _reserved1.parserFlags &= 0xffffff7f;
            }
            return self;
        }

        private void initializeSAX2Callbacks()
        {
            unsafe
            {
                _reserved1.saxHandler.internalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.internalSubsetSAXFunc(_internalSubset2));
                _reserved1.saxHandler.isStandalone = Marshal.GetFunctionPointerForDelegate(new LibXml.isStandaloneSAXFunc(_isStandalone));
                _reserved1.saxHandler.hasInternalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.hasInternalSubsetSAXFunc(_hasInternalSubset2));
                _reserved1.saxHandler.hasExternalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.hasExternalSubsetSAXFunc(_hasExternalSubset2));
                _reserved1.saxHandler.resolveEntity = IntPtr.Zero;//Marshal.GetFunctionPointerForDelegate(new LibXml.resolveEntitySAXFunc(_resolveEntity));
                _reserved1.saxHandler.getEntity = Marshal.GetFunctionPointerForDelegate(new LibXml.getEntitySAXFunc(_getEntity));
                _reserved1.saxHandler.entityDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.entityDeclSAXFunc(_entityDecl));
                _reserved1.saxHandler.notationDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.notationDeclSAXFunc(_notationDecl));
                _reserved1.saxHandler.attributeDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.attributeDeclSAXFunc(_attributeDecl));
                _reserved1.saxHandler.elementDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.elementDeclSAXFunc(_elementDecl));
                _reserved1.saxHandler.unparsedEntityDecl = Marshal.GetFunctionPointerForDelegate(new LibXml.unparsedEntityDeclSAXFunc(_unparsedEntityDecl));
                _reserved1.saxHandler.startDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.startDocumentSAXFunc(_startDocument));
                _reserved1.saxHandler.endDocument = Marshal.GetFunctionPointerForDelegate(new LibXml.endDocumentSAXFunc(_endDocument));
                _reserved1.saxHandler.startElementNs = Marshal.GetFunctionPointerForDelegate(new LibXml.startElementNsSAX2Func(_startElementNs));
                _reserved1.saxHandler.endElementNs = Marshal.GetFunctionPointerForDelegate(new LibXml.endElementNsSAX2Func(_endElementNs));
                _reserved1.saxHandler.characters = Marshal.GetFunctionPointerForDelegate(new LibXml.charactersSAXFunc(_characters));
                _reserved1.saxHandler.processingInstruction = Marshal.GetFunctionPointerForDelegate(new LibXml.processingInstructionSAXFunc(_processingInstruction));
                _reserved1.saxHandler.error = Marshal.GetFunctionPointerForDelegate(new LibXml.errorSAXFunc(_errorCallback));
                _reserved1.saxHandler.getParameterEntity = IntPtr.Zero;
                _reserved1.saxHandler.cdataBlock = Marshal.GetFunctionPointerForDelegate(new LibXml.cdataBlockSAXFunc(_cdataBlock));
                _reserved1.saxHandler.comment = Marshal.GetFunctionPointerForDelegate(new LibXml.commentSAXFunc(_comment));
                _reserved1.saxHandler.externalSubset = Marshal.GetFunctionPointerForDelegate(new LibXml.externalSubsetSAXFunc(_externalSubset2));
                _reserved1.saxHandler.initialized = LibXml.XML_SAX2_MAGIC;

                int iSizeOfXmlSAXHandler = Marshal.SizeOf(typeof(LibXml.xmlSAXHandler));
                _saxHandlerPtr = Marshal.AllocHGlobal(iSizeOfXmlSAXHandler);
                Marshal.StructureToPtr(_reserved1.saxHandler, _saxHandlerPtr, false);
            }
        }

        private static unsafe void _cdataBlock(IntPtr ctx, IntPtr value, int len)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            id dlegate = pThis.getDelegate();
            //parser:foundCDATA:
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundCDATA")) == true)
            {
                NSString cdata = (NSString)NSString.alloc().initWithBytes(value, (uint)len, NSStringEncoding.NSUTF8StringEncoding);
                Objc.MsgSend(dlegate, "parserFoundCDATA", pThis, cdata);
            }
            else
            {
                NSXMLParserWIP._characters(ctx, value, len);
            }
        }

        private static unsafe void _processingInstruction(IntPtr ctx, IntPtr pTarget, IntPtr pData)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;
            
            id dlegate = pThis.getDelegate();
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundProcessingInstructionWithTarget")) == true)
            {
                NSString target = null;
                if (pTarget != IntPtr.Zero)
                {
                    target = pThis._NSXMLParserNSStringFromBytes(pTarget, pThis._info());
                }

                NSString data = null;
                if (pData != IntPtr.Zero)
                {
                    data = pThis._NSXMLParserNSStringFromBytes(pTarget, pThis._info());
                }
                Objc.MsgSend(dlegate, "parserFoundProcessingInstructionWithTarget", pThis, target, data);
            }
        }

        private static unsafe void _externalSubset2(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            LibXml.xmlSAX2ExternalSubset(pThis.parserContext(), name, ExternalID, SystemID);
        }

        private static unsafe void _comment(IntPtr ctx, IntPtr pValue)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            id dlegate = pThis.getDelegate();
            //parser:foundComment:
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundComment")) == true)
            {
                NSString chars = null;
                if (pValue != IntPtr.Zero)
                {
                    chars = pThis._NSXMLParserNSStringFromBytes(pValue, pThis._info());
                }
                Objc.MsgSend(dlegate, "parserFoundComment", pThis, chars);
            }
        }

        private unsafe void _errorCallback(IntPtr ctx, IntPtr msg, params IntPtr[] prms)
        {
            throw new NotImplementedException();
        }

        private static unsafe void _unparsedEntityDecl(IntPtr ctx, IntPtr pName, IntPtr pPublicId, IntPtr pSystemId, IntPtr pNotationName)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            LibXml.xmlSAX2UnparsedEntityDecl(pThis.parserContext(), pName, pPublicId, pSystemId, pNotationName);

            id dlegate = pThis.getDelegate();
            //parser:foundUnparsedEntityDeclarationWithName:publicID:systemID:notationName:
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundUnparsedEntityDeclarationWithName")) == true)
            {
                NSString name = null;
                if (pName != IntPtr.Zero)
                {
                    name = pThis._NSXMLParserNSStringFromBytes(pName, pThis._info());
                }
                NSString publicId = null;
                if (pPublicId != IntPtr.Zero)
                {
                    publicId = pThis._NSXMLParserNSStringFromBytes(pPublicId, pThis._info());
                }
                NSString systemId = null;
                if (pSystemId != IntPtr.Zero)
                {
                    systemId = pThis._NSXMLParserNSStringFromBytes(pSystemId, pThis._info());
                }
                NSString notationName = null;
                if (pNotationName != IntPtr.Zero)
                {
                    notationName = pThis._NSXMLParserNSStringFromBytes(pNotationName, pThis._info());
                }
                Objc.MsgSend(dlegate, "parserFoundUnparsedEntityDeclarationWithName", pThis, name, publicId, systemId, notationName);
            }
        }

        private static unsafe void _elementDecl(IntPtr ctx, IntPtr pName, int type, IntPtr content)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            id dlegate = pThis.getDelegate();
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundElementDeclarationWithName")) == true)
            {
                NSString name = null;
                if (pName != IntPtr.Zero)
                {
                    name = pThis._NSXMLParserNSStringFromBytes(pName, pThis._info());
                }
                Objc.MsgSend(dlegate, "parserFoundElementDeclarationWithName", pThis, name, "");
            }
        }

        private static unsafe void _attributeDecl(IntPtr ctx, IntPtr pElem, IntPtr pFullname, int type, int def, IntPtr pDefaultValue, IntPtr tree)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            id dlegate = pThis.getDelegate();
            //parser:foundAttributeDeclarationWithName:forElement:type:defaultValue:
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundAttributeDeclarationWithName")) == true)
            {
                NSString elem = null;
                if (pElem != IntPtr.Zero)
                {
                    elem = pThis._NSXMLParserNSStringFromBytes(pElem, pThis._info());
                }
                NSString fullname = null;
                if (pFullname != IntPtr.Zero)
                {
                    fullname = pThis._NSXMLParserNSStringFromBytes(pFullname, pThis._info());
                }
                NSString defaultValue = null;
                if (pDefaultValue != IntPtr.Zero)
                {
                    defaultValue = pThis._NSXMLParserNSStringFromBytes(pDefaultValue, pThis._info());
                }
                Objc.MsgSend(dlegate, "parserFoundAttributeDeclarationWithName", pThis, fullname, elem, type, defaultValue);

            }
            LibXml.xmlFreeEnumeration(tree);
        }

        private static unsafe void _notationDecl(IntPtr ctx, IntPtr pName, IntPtr pPublicId, IntPtr pSystemId)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            id dlegate = pThis.getDelegate();
            //parser:foundNotationDeclarationWithName:publicID:systemID:
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundNotationDeclarationWithName")) == true)
            {
                NSString name = null;
                if (pName != IntPtr.Zero)
                {
                    name = pThis._NSXMLParserNSStringFromBytes(pName, pThis._info());
                }
                NSString publicId = null;
                if (pPublicId != IntPtr.Zero)
                {
                    publicId = pThis._NSXMLParserNSStringFromBytes(pPublicId, pThis._info());
                }
                NSString systemId = null;
                if (pSystemId != IntPtr.Zero)
                {
                    systemId = pThis._NSXMLParserNSStringFromBytes(pSystemId, pThis._info());
                }
                Objc.MsgSend(dlegate, "parserFoundNotationDeclarationWithName", pThis, name, publicId, systemId);
            }

        }

        private static unsafe void _entityDecl(IntPtr ctx, IntPtr pName, int type, IntPtr pPublicId, IntPtr pSystemId, IntPtr pContent)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            LibXml.xmlSAX2EntityDecl(pThis.parserContext(), pName, type, pPublicId, pSystemId, pContent);

            NSString content = null;
            if (pContent != IntPtr.Zero)
            {
                content = pThis._NSXMLParserNSStringFromBytes(pContent, pThis._info());
            }
            NSString name = null;
            if (pName != IntPtr.Zero)
            {
                name = pThis._NSXMLParserNSStringFromBytes(pName, pThis._info());
            }
            if (content.length() == 0)
            {
                if (pThis.shouldResolveExternalEntities())
                {
                    id dlegate = pThis.getDelegate();
                    //parser:foundExternalEntityDeclarationWithName:publicID:systemID:
                    if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundExternalEntityDeclarationWithName")) == true)
                    {
                        NSString publicId = null;
                        if (pPublicId != IntPtr.Zero)
                        {
                            publicId = pThis._NSXMLParserNSStringFromBytes(pPublicId, pThis._info());
                        }
                        NSString systemId = null;
                        if (pSystemId != IntPtr.Zero)
                        {
                            systemId = pThis._NSXMLParserNSStringFromBytes(pSystemId, pThis._info());
                        }
                        Objc.MsgSend(dlegate, "parserFoundExternalEntityDeclarationWithName", pThis, name, publicId, systemId);
                    }
                    //parser:foundInternalEntityDeclarationWithName:value:
                    if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundInternalEntityDeclarationWithName")) == true)
                    {
                        Objc.MsgSend(dlegate, "parserFoundInternalEntityDeclarationWithName", pThis, name, content);
                    }
                }
            }
            else
            {
                id dlegate = pThis.getDelegate();
                //parser:foundInternalEntityDeclarationWithName:value:
                if (dlegate != null && dlegate.respondsToSelector(new SEL("parserFoundInternalEntityDeclarationWithName")) == true)
                {
                    Objc.MsgSend(dlegate, "parserFoundInternalEntityDeclarationWithName", pThis, name, content);
                }
            }

        }

        private static unsafe IntPtr _getEntity(IntPtr ctx, IntPtr pName)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            IntPtr parserCtx = pThis.parserContext();
            id dlegate = pThis.getDelegate();

            xmlEntityPtr pEntity = LibXml.xmlGetPredefinedEntity(pName);
            if (pEntity == IntPtr.Zero)
            {
                pEntity = LibXml.xmlSAX2GetEntity(parserCtx, pName);
                if (pEntity != IntPtr.Zero)
                {
                    IntPtr parserCtxt_instate = parserCtx.Inc(0xAC);
                    if (parserCtxt_instate.Deref<int>() == 7 /*LibXml.XML_PARSER_CONTENT*/)
                    {
                        //reader->ctxt->_private = 1;
                        parserCtx.Inc(0x110).Deref().Assign((IntPtr)1);

                    }
                }
                else
                {
                    //parser:resolveExternalEntityName:systemID:
                    if (dlegate != null && dlegate.respondsToSelector(new SEL("parserResolveExternalEntityName")) == true)
                    {
                        NSString name = null;
                        if (pName != IntPtr.Zero)
                        {
                            name = pThis._NSXMLParserNSStringFromBytes(pName, pThis._info());
                        }

                        NSData data = (NSData)Objc.MsgSend(dlegate, "parserResolveExternalEntityName", pThis, name, 0);
                        if (data != null && IntPtr.Add(parserCtx, 0x8) != IntPtr.Zero)
                        {
                            IntPtr pChars = ((NSString)(NSString.alloc().initWithData(data, NSStringEncoding.NSUTF8StringEncoding))).UTF8String();
                            if (pChars != IntPtr.Zero)
                            {
                                NSXMLParserWIP._characters(ctx, pChars, pChars.strlen());
                            }
                        }
                    }
                }
            }



            return pEntity;
        }


        private static unsafe int _hasExternalSubset2(IntPtr ctx)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            // This offset is calculated from a console application using libxml2
            // void * pExtSubset = &(parserCtx.myDoc->extSubset);
            //fprintf(f, "----->&(parserCtx.myDoc->extSubset) 0x%p (+0x%x)\n", pExtSubset, (uint32_t)pExtSubset-(uint32_t) & (parserCtx.myDoc->_private));

            IntPtr parserCtx = pThis.parserContext();
            IntPtr parserCtxMyDoc = IntPtr.Add(parserCtx, 0x8);
            IntPtr extSubset = IntPtr.Add(Marshal.ReadIntPtr(parserCtxMyDoc), 0x30);

            return (extSubset != IntPtr.Zero) ? 1 : 0;
        }

        private static unsafe int _hasInternalSubset2(IntPtr ctx)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            IntPtr parserCtx = pThis.parserContext();
            IntPtr parserCtxMyDoc =  parserCtx.Inc(0x8).Deref();

            return parserCtxMyDoc.Inc(0x2C).Deref<int>();
        }

        private static unsafe int _isStandalone(IntPtr ctx)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            IntPtr parserCtx = pThis.parserContext();
            IntPtr parserCtxMyDoc = parserCtx.Inc(0x8).Deref();
            //IntPtr standalone = IntPtr.Add(Marshal.ReadIntPtr(parserCtxMyDoc), 0x28);
            return parserCtxMyDoc.Inc(0x28).Deref<int>();
            
        }

        private static unsafe void _internalSubset2(IntPtr ctx, IntPtr pName, IntPtr pExternalID, IntPtr pSystemID)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            IntPtr parserCtx = pThis.parserContext();
            LibXml.xmlSAX2InternalSubset(parserCtx, pName, pExternalID, pSystemID);
        }

        public virtual bool parse()
        {
            return this.parseFromStream();
        }

        protected virtual bool parseFromStream()
        {
            bool result = true;

            NSInputStream stream = _reserved3;
            if (stream != null)
            {
                stream.open();
                byte[] buffer = new byte[_reserved1.chunkSize];
                int readBytes = stream.read(buffer, _reserved1.chunkSize);
                if (readBytes != -1)
                {
                    do
                    {
                        parseData(NSData.dataWithBytes(buffer));
                        readBytes = stream.read(buffer, _reserved1.chunkSize);
                    }
                    while (readBytes > 0);

                    result = finishIncrementalParse();
                }
                stream.close();
            }
            else
            {
                
                var userInfo = (NSDictionary)NSDictionary.dictionaryWithObjectsAndKeys((NSString)"Could not open data stream", (NSString)"NSXMLParserErrorMessage", null);
                NSError err = NSError.errorWithDomain("NSCocoaErrorDomain", -1, userInfo);
                this._setExpandedParserError(err);
                result = false;
            }

            return result;
        }

        protected virtual bool parseData(NSData data)
        {
            bool result = false;

            if ((this._reserved1.parserFlags & 0x40) == 0)
            {
                throw new Exception("NSInternalInconsistencyException");
            }

            LibXml.xmlSetStructuredErrorFunc(_instancePtr, _xmlStructuredErrorFuncPtr);
            if (this._reserved1.haveDetectedEncoding == true)
            {
                result = this._handleParseResult(LibXml.xmlParseChunk(_reserved1.parserContext, data.bytes(), (int)data.length(), 0));
            }
            else
            {
                uint bomChunkLen = (_reserved1.bomChunk != null) ? _reserved1.bomChunk.Length : 0;
                uint dataLen = (data != null) ? data.Length : 0;

                if (bomChunkLen + dataLen <= 3)
                {
                    NSData chunkData = data;
                    if (_reserved1.bomChunk != null)
                    {
                        chunkData = (NSMutableData)NSMutableData.alloc().init();
                        ((NSMutableData)chunkData).appendData(_reserved1.bomChunk);
                        ((NSMutableData)chunkData).appendData(data);
                        _reserved1.bomChunk = chunkData;
                    }


                    result = true;
                }
                else
                {
                    NSData chunkData = data;
                    if (_reserved1.bomChunk != null)
                    {
                        chunkData = (NSMutableData)NSMutableData.alloc().init();
                        ((NSMutableData)chunkData).appendData(_reserved1.bomChunk);
                        ((NSMutableData)chunkData).appendData(data);
                    }


                    IntPtr saxHandlerPtr = (_delegate != null) ? _saxHandlerPtr : IntPtr.Zero;
                    _reserved1.parserContext = LibXml.xmlCreatePushParserCtxt(saxHandlerPtr, _instancePtr, chunkData.bytes(), 4, null);

                    bool shouldResolveExternals = shouldResolveExternalEntities();
                    int parserFlags = (shouldResolveExternals) ? (int)LibXml.XmlParserOption.XML_PARSE_DTDLOAD : 0;
                    LibXml.xmlCtxtUseOptions(_reserved1.parserContext, parserFlags);
                    _reserved1.haveDetectedEncoding = true;
                    _reserved1.bomChunk = null;

                    if (bomChunkLen + dataLen >= 5)
                    {
                        byte[] dst = new byte[data.Length - 4];
                        Buffer.BlockCopy(data.bytes(), 4, dst, 0, (int)data.length() - 4);
                        NSData tmpData = (NSMutableData)NSMutableData.alloc().initWithBytes(dst);
                        parseData(tmpData);
                    }

                    result = true;
                }
            }

            LibXml.xmlSetStructuredErrorFunc(IntPtr.Zero, IntPtr.Zero);
            return result;
        }

        protected virtual void _StructuredErrorFunc(IntPtr userData, IntPtr error)
        {
            // FIXME
            System.Diagnostics.Debug.WriteLine("_StructuredErrorFunc");
        }

        protected virtual NSError _setExpandedParserError(NSError error)
        {
            if (this._reserved1.error != null)
                this._reserved1.error.autorelease();
            this._reserved1.error = (NSError)error.retain();

            return _reserved1.error;
        }

        protected virtual NSError _setParserError(int code)
        {
            NSError err = (NSError)NSError.alloc().initWithDomain("NSXMLParserErrorDomain", code, null).autorelease();
            return this._setExpandedParserError(err);
        }

        private static NSError _NSErrorFromXMLError(xmlErrorPtr pError, NSXMLParserWIP pThis)
        {
            NSError err = null;


            return err;
        }



        protected virtual bool _handleParseResult(int xmlParserError)
        {
            bool result = true;

            if (xmlParserError > 0)
            {
                if (this._info().delegateAborted == false)
                {
                    xmlErrorPtr pError = LibXml.xmlCtxtGetLastError(this._reserved1.parserContext);
                    if (pError != IntPtr.Zero && pError.Inc(0x04).Deref<int>() == xmlParserError)
                    {
                        this._setExpandedParserError(_NSErrorFromXMLError(pError, this));
                    }
                    else
                    {
                        this._setParserError(xmlParserError);
                    }
                    result = false;
                }
                else
                {
                    result = false;

                    NSError err = NSError.errorWithDomain("NSXMLParserErrorDomain", 0x200, null);
                    this._setExpandedParserError(err);
                    id dlegate = this.getDelegate();
                    if (dlegate != null && dlegate.respondsToSelector(new SEL("parseErrorOccurred")))
                    {
                        Objc.MsgSend(dlegate, "parserFoundCharacters", this, err);
                    }
                }
            }


            return result;
        }

        protected virtual void _pushNamespaces(NSDictionary aNsDict)
        {
            if (this._reserved1.namespaces == null)
            {
                this._reserved1.namespaces = (NSMutableArray)NSMutableArray.alloc().init();
            }

            if (aNsDict != null)
            {
                this._reserved1.namespaces.addObject(aNsDict);

                //parser:didStartMappingPrefix:toURI:
                if (this._delegate != null && this._delegate.respondsToSelector(new SEL("parserDidStartMappingPrefix")))
                {
                    foreach(id key in aNsDict.allKeys())
                    {
                        NSString prefix = (NSString)key;
                        NSString nsURI = (NSString) aNsDict[key];
                        Objc.MsgSend(this._delegate, "parserDidStartMappingPrefix", this, prefix, nsURI);
                    }
                }
            }
            else
            {
                this._reserved1.namespaces.addObject(NSNull.getNull());
            }
        }


        protected virtual void _popNamespaces()
        {
            uint lastIndex = _reserved1.namespaces.count() - 1;
            NSDictionary nsDict = (NSDictionary)_reserved1.namespaces.objectAtIndex(lastIndex);
            if (nsDict.isEqual(NSNull.getNull()) == false)
            {
                if (this._delegate != null && this._delegate.respondsToSelector(new SEL("parserDidEndMappingPrefix")))
                {
                    foreach (id key in nsDict.allKeys())
                    {
                        NSString prefix = (NSString)key;
                        Objc.MsgSend(this._delegate, "parserDidEndMappingPrefix", this, prefix);
                    }
                }
            }

            _reserved1.namespaces.removeObjectAtIndex(lastIndex);

        }


        protected virtual bool finishIncrementalParse()
        {
            return this._handleParseResult(LibXml.xmlParseChunk(_reserved1.parserContext, null, 0, 1));
        }


        private static unsafe void _characters(IntPtr ctx, IntPtr ch, int len)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            id dlegate = pThis.getDelegate();
            if (dlegate == null || dlegate.respondsToSelector(new SEL("parserFoundCharacters")) == false)
                return;

            NSString chars = (NSString)NSString.alloc().initWithBytes(ch, (uint)len, NSStringEncoding.NSUTF8StringEncoding);
            Objc.MsgSend(dlegate, "parserFoundCharacters", pThis, chars);
        }

        private static unsafe void _startElementNs(
            IntPtr ctx, IntPtr pLocalname, IntPtr pPrefix, IntPtr pURI,
            int nb_namespaces, IntPtr pNamespaces,
            int nb_attributes, int nb_defaulted, IntPtr pAttributes)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            //namespaces: pointer to the array of prefix/URI pairs namespace definitions
            IntPtr[] namespaces = pNamespaces.ReadArray(nb_namespaces * 2);
            //attributes: pointer to the array of (localname/prefix/URI/value/end) attribute values.
            IntPtr[] attributes = pNamespaces.ReadArray(nb_attributes * 5);

            id dlegate = pThis.getDelegate();
            bool shouldProcessNs = pThis.shouldProcessNamespaces();
            bool shouldReportNsPrefixes = pThis.shouldReportNamespacePrefixes();
            int prefixLen = LibXml.xmlStrlen(pPrefix);

            NSString qName = null;
            if (shouldProcessNs || prefixLen == 0)
            {
                qName = pThis._NSXMLParserNSStringFromBytes(pLocalname, pThis._info());
            }

            if (prefixLen != 0)
            {
                qName = pThis._NewColonSeparatedStringFromPrefixAndSuffix(pPrefix, pLocalname);
            }
            

            NSString uri = null;
            if (shouldProcessNs)
            {
                if (pURI != IntPtr.Zero)
                {
                    uri = pThis._NSXMLParserNSStringFromBytes(pURI, pThis._info());
                }
            }

            NSMutableDictionary processDict = null;
            NSMutableDictionary reportDict = null;

            if (nb_namespaces + nb_attributes != 0)
            {
                processDict = (NSMutableDictionary)NSMutableDictionary.alloc().initWithCapacity((uint)(nb_namespaces + nb_attributes));
                if (shouldReportNsPrefixes)
                {
                    reportDict = (NSMutableDictionary)NSMutableDictionary.alloc().initWithCapacity((uint)nb_namespaces);
                }
                if (nb_namespaces != 0)
                {
                    int index = 0;
                    do
                    {
                        NSString prefixWithNs = @"";
                        NSString defaultPrefix = @"xmlns";
                        IntPtr pNsPrefix = namespaces[index]; //Marshal.ReadIntPtr(pNamespaces, 0);
                        if (pNsPrefix != IntPtr.Zero)
                        {
                            prefixWithNs = null;
                            if(shouldReportNsPrefixes)
                            {
                                prefixWithNs = pThis._NSXMLParserNSStringFromBytes(pNsPrefix, pThis._info());
                                //pQName  = pNamespaces[index].Deref();
                            }
                            defaultPrefix = pThis._NewColonSeparatedStringFromPrefixAndSuffix("xmlns", pNsPrefix);
                        }

                        NSString nsURI = "";
                        IntPtr pNsURI = namespaces[index + 1];
                        if (pNsURI != IntPtr.Zero)
                        {
                            nsURI = pThis._NSXMLParserNSStringFromBytes(pNsURI, pThis._info());
                        }
                        if (shouldReportNsPrefixes) 
                        {
                            reportDict.setObjectForKey(nsURI, prefixWithNs);
                        }
                        if (shouldProcessNs ) 
                        {
                            processDict.setObjectForKey(nsURI, defaultPrefix);
                        }

                        index += 2;
                    }
                    while (index < (2 * nb_namespaces));
                }
            }

            if (shouldReportNsPrefixes)
                pThis._pushNamespaces(reportDict);
            
            if (nb_attributes != 0)
            {
                int index = 0;
                do
                {
                    IntPtr pLocalName = attributes[0];
                    if (pLocalName != IntPtr.Zero)
                    {
                        NSString localName = null;

                        IntPtr prefix = attributes[0];
                        if (prefix.strlen() != 0)
                        {
                            localName = pThis._NewColonSeparatedStringFromPrefixAndSuffix(prefix, pLocalName);
                        }
                        else
                        {
                            localName = (NSString)pThis._NSXMLParserNSStringFromBytes(pLocalName, pThis._info()).retain();
                        }

                        NSString value = "";
                        IntPtr pValue = attributes[3];
                        if(pValue != IntPtr.Zero)
                        {
                           
                            IntPtr pEnd = attributes[4];
                            if (pEnd != IntPtr.Zero)
                            {
                                value = (NSString)NSString.alloc().initWithBytes(pValue, (uint)(pEnd.ToInt64() - pValue.ToInt64()), NSStringEncoding.NSUTF8StringEncoding);
                            }
                        }

                        if (processDict != null)
                            processDict.setObjectForKey(value, localName);
                    }

                    index += 5;
                }
                while (index < (5 * nb_attributes));
            }

            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserDidStartElement")))
            {
                if (shouldProcessNs)
                {

                }
                else
                {

                }
            }
            

        }

        private static unsafe void _endElementNs(IntPtr ctx, IntPtr pLocalname, IntPtr pPrefix, IntPtr pURI)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            bool shouldProcessNs = pThis.shouldProcessNamespaces();
            bool shouldReportNsPrefixes = pThis.shouldReportNamespacePrefixes();
            int prefixLen = LibXml.xmlStrlen(pPrefix);

            NSString localname = null;
            NSString qname = "";
            if (prefixLen != 0 & shouldProcessNs == false)
            {
                //v10 = pLocalname;
            }
            else
            {
                //v10 = pLocalname;
                if (pLocalname != IntPtr.Zero)
                {
                    localname = pThis._NSXMLParserNSStringFromBytes(pLocalname, pThis._info());
                }
            }

            if (prefixLen != 0)
            {
                qname = pThis._NewColonSeparatedStringFromPrefixAndSuffix(pPrefix, pLocalname);
            }
            else
            {
                qname = (NSString)localname.retain();
            }

            NSString uri = "";
            if (shouldProcessNs && pURI != IntPtr.Zero)
            {
                uri = pThis._NSXMLParserNSStringFromBytes(pURI, pThis._info());
            }


            id dlegate = pThis.getDelegate();
            //"parser:didEndElement:namespaceURI:qualifiedName:"
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserDidEndElement")) == true)
            {
                if (shouldProcessNs)
                {
                    Objc.MsgSend(dlegate, "parserDidEndElement", pThis, localname, uri, qname);
                }
                else
                {
                    Objc.MsgSend(dlegate, "parserDidEndElement", pThis, qname, uri, null);
                }
            }

            pThis._popNamespaces();
            qname.release();
        }
        

        private NSString _NewColonSeparatedStringFromPrefixAndSuffix(IntPtr pPrefix, IntPtr pLocalname)
        {
            NSString str = null;

            string tmpStr = string.Format("{0}:{1}", pPrefix.GetStringFromUTF8(), pLocalname.GetStringFromUTF8());
            str = (NSString)tmpStr;

            return str;
        }

        private NSString _NewColonSeparatedStringFromPrefixAndSuffix(NSString prefix, IntPtr pLocalname)
        {
            NSString str = null;

            string tmpStr = string.Format("{0}:{1}", prefix, pLocalname.GetStringFromUTF8());
            str = (NSString)tmpStr;

            return str;
        }

        private NSString _NSXMLParserNSStringFromBytes(IntPtr pLocalname, NSXMLParserInfo info)
        {
            NSString str = (NSString)NS.MapGet(info.slowStringMap, pLocalname);
            if (str == null)
            {
                str = (NSString)NSString.allocWithZone(null).initWithBytes(pLocalname, (uint)pLocalname.strlen(), NSStringEncoding.NSUTF8StringEncoding);
                NS.MapInsertKnownAbsent(info.slowStringMap, pLocalname, str);
            }


            return str;
        }


        protected static void _startDocument(IntPtr ctx)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            id dlegate = pThis.getDelegate();
            //parserDidStartDocument:
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserDidStartDocument")) == true)
            {
                Objc.MsgSend(dlegate, "parserDidStartDocument", pThis);
            }
        }

        protected static void _endDocument(IntPtr ctx)
        {
            NSXMLParserWIP pThis = ((GCHandle)ctx).Target as NSXMLParserWIP;

            id dlegate = pThis.getDelegate();
            //parserDidEndDocument:
            if (dlegate != null && dlegate.respondsToSelector(new SEL("parserDidEndDocument")) == true)
            {
                Objc.MsgSend(dlegate, "parserDidEndDocument", pThis);
            }
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
