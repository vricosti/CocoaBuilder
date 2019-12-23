using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using xmlCharPtr = System.IntPtr;
using xmlExternalEntityLoaderPtr = System.IntPtr;
using xmlParserInputPtr = System.IntPtr;
using xmlParserCtxtPtr = System.IntPtr;
//using xmlTextReaderPtr = System.IntPtr;
using xmlTextReaderLocatorPtr = System.IntPtr;
using xmlEntityPtr = System.IntPtr;
using xmlDocPtr = System.IntPtr;

namespace Smartmobili.Cocoa
{
    public unsafe class NSXMLTreeReader : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLTreeReader));
        new public static NSXMLTreeReader alloc() { return new NSXMLTreeReader(); }


        GCHandle _instanceHandle;
        IntPtr _instancePtr;
        static IntPtr _nativeEmptyString; // To release


        private static LibXml.xmlTextReaderErrorFunc _xmlTextReaderErrorFuncDelegate;
        private static LibXml.charactersSAXFunc _characters3Delegate;
        private static LibXml.startElementNsSAX2Func _startElementNs1Delegate;
        private static LibXml.getEntitySAXFunc _getEntity1Delegate;

        private static bool _entitySetupOncePredicate;
        private static LibXml.xmlExternalEntityLoader __originalLoader;
        private static NSString ThreadXmlTreeReaderTag = "__CurrentNSXMLTreeReader";
        private LibXml.xmlExternalEntityLoader _xmlExternalEntityLoaderDelegate;
        private static int _enableXMLParsingMemoryGuards = -1;

        protected bool _hadError; //0x04
        protected bool _additiveContent; //0x05
        protected bool _isSingleDTDNode; //0x06
        protected bool _wasEmpty; //0x07
        protected bool _isMissingDTD; //0x08
        protected bool _elementClassOverridden; //0x09
        protected int _externalEntityLoadingPolicy; //0x0C
        protected uint _fidelityMask; //0x10
        protected NSData _data; //0x14
        protected NSString _uri; //0x18
        protected NSURL _url; //0x1C
        protected NSSet _allowedEntityURLs; //0x20
        protected NSXMLNode _root; //0x24
        protected NSXMLNode _current; //0x28
        protected NSError _error; //0x2C(x86) - 0x50(x64)
        protected NSMutableString _content; //0x30
        protected NSString _whitespace; //0x34
        protected NSXMLNode _text; //0x38
        protected NSMapTable _xmlCharToNSString; //0x3C
        protected NSMapTable _xmlCharHashToNSString; //0x40
        protected id _readerCharacters; //0x44
        protected xmlTextReader* _reader; //0x48
        protected Class _documentClass; //0x4C
        protected Class _dtdClass; //0x50
        protected Class _dtdNodeClass; //0x54
        protected Class _elementClass; //0x58
        protected Class _nodeClass; //0x5C

        public NSXMLTreeReader()
        {
            _instanceHandle = GCHandle.Alloc(this);
            _instancePtr = GCHandle.ToIntPtr(_instanceHandle);//(IntPtr)_instanceHandle;

            // Allocate native null-terminated empty string("")
            _nativeEmptyString = Marshal.AllocHGlobal(1);
            Marshal.Copy(new byte[] { 0x00 }, 0, _nativeEmptyString, 1);


            _xmlTextReaderErrorFuncDelegate = new LibXml.xmlTextReaderErrorFunc(_xmlTextReaderErrorFunc);
            _characters3Delegate = new LibXml.charactersSAXFunc(_characters3);
            _startElementNs1Delegate = new LibXml.startElementNsSAX2Func(_startElementNs3);
            _getEntity1Delegate = new LibXml.getEntitySAXFunc(_getEntity3);
        }


        public virtual NSString URI()
        {
            return _uri;
        }
        public virtual void setURI(NSString uri)
        {
            if (_uri != uri)
            {
                _uri.autorelease();
                _uri = (NSString)uri.retain();
            }
        }

        public virtual NSURL url()
        {
            if (_url == null)
            {
                if (_uri != null)
                {
                    _url = (NSURL)NSURL.alloc().initWithString(this.URI());
                }
            }

            return _url;
        }


        public virtual NSSet allowedEntityURLs()
        {
            return _allowedEntityURLs;
        }

        public virtual void setAllowedEntityURLs(NSSet allowedEntityURLs)
        {
            if (_allowedEntityURLs != allowedEntityURLs)
            {
                _allowedEntityURLs.autorelease();
                _allowedEntityURLs = (NSSet)allowedEntityURLs.retain();
            }
        }

        public virtual int externalEntityLoadingPolicy()
        {
            return _externalEntityLoadingPolicy;
        }

        public virtual void setExternalEntityLoadingPolicy(int externalEntityLoadingPolicy)
        {
            _externalEntityLoadingPolicy = externalEntityLoadingPolicy;
        }


        public virtual void setContent(NSMutableString content)
        {
            if (_content == content)
                return;

            _content.release();
            _content = (NSMutableString)content.mutableCopy();
            if (_content != null && _text == null)
            {
                _text = (NSXMLNode)((NSXMLNode)_nodeClass.alloc()).initWithKind(NSXMLNodeKind.NSXMLTextKind);
            }
        }

        public virtual void setRoot(NSXMLNode root)
        {
            if (_root != root)
            {
                _root.release();
                _root = (NSXMLNode)root.retain();
            }
        }


        public virtual id initWithData(NSData data, Class documentClass, uint mask, ref NSError error)
        {
            return initWithData(data, documentClass, false, mask, ref error);
        }

        public unsafe virtual id initWithData(NSData data, Class documentClass, bool isSingleDTDNode, uint mask, ref NSError error)
        {
            id self = base.init();
            if (base.init() != null)
            {
                self = this;
                if (NSThread.currentThread().threadDictionary().objectForKey(ThreadXmlTreeReaderTag) != null)
                {
                    NSException.raise("NSInternalInconsistencyException", "%@", "NSXMLDocument does not support reentrant parsing.");
                }
                NSThread.currentThread().threadDictionary().setObjectForKey(self, ThreadXmlTreeReaderTag);
                if (_entitySetupOncePredicate == false)
                {
                    _entitySetupOncePredicate = true;
                    __originalLoader = LibXml.xmlGetExternalEntityLoader();

                    _xmlExternalEntityLoaderDelegate = new LibXml.xmlExternalEntityLoader(_xmlExternalEntityLoader);
                    LibXml.xmlSetExternalEntityLoader(_xmlExternalEntityLoaderDelegate);
                }
                _isSingleDTDNode = isSingleDTDNode;
                _data = (NSData)data.retain();
                _error = error;

                bool additiveContent = true;
                if ((_fidelityMask & 0x8400000) == 0x0)
                    additiveContent = ((_fidelityMask & 0x1000000) >> 24) != 0;
                _additiveContent = additiveContent;
                _hadError = false;
                _wasEmpty = false;
                _isMissingDTD = false;

                _xmlCharToNSString = NS.CreateMapTable(200);
                _xmlCharHashToNSString = NS.CreateMapTable(200);

                _documentClass = (Class)Objc.MsgSend(documentClass, (NSString)"replacementClassForClass", NSXMLDocument.Class);
                _dtdClass = (Class)Objc.MsgSend(documentClass, (NSString)"replacementClassForClass", NSXMLDTD.Class);
                _dtdNodeClass = (Class)Objc.MsgSend(documentClass, (NSString)"replacementClassForClass", NSXMLDTDNode.Class);
                _elementClass = (Class)Objc.MsgSend(documentClass, (NSString)"replacementClassForClass", NSXMLElement.Class);
                _elementClassOverridden = (_elementClass != NSXMLElement.Class);
                _nodeClass = (Class)Objc.MsgSend(documentClass, (NSString)"replacementClassForClass", NSXMLNode.Class);
            }

            return self;
        }






        private static unsafe xmlParserInput* _xmlExternalEntityLoader(IntPtr URL, IntPtr ID, xmlParserCtxt* context)
        {
            xmlParserInput* result = (xmlParserInput*)IntPtr.Zero;

            //var originalLoader = InteropHelper.GetDelegateForFunctionPointer<LibXml.xmlExternalEntityLoader>(__originalLoader);
            var originalLoader = __originalLoader;

            NSXMLTreeReader treeReaderInstance = NSThread.currentThread().threadDictionary().objectForKey(NSXMLTreeReader.ThreadXmlTreeReaderTag) as NSXMLTreeReader;
            if (treeReaderInstance == null)
            {
                result = originalLoader(URL, ID, context);
            }
            else
            {
                int externalPolicy = treeReaderInstance.externalEntityLoadingPolicy();
                NSURL url = null;
                NSSet allowedEntityURLs = treeReaderInstance.allowedEntityURLs();
                if (allowedEntityURLs != null)
                {
                    NSString urlText = (NSString)NSString.alloc().initWithUTF8String(URL);
                    url = (NSURL)NSURL.alloc().initWithString(urlText);
                    if (url.scheme().isEqualToString("file"))
                    {
                        NSURL fileUrl = (NSURL)NSURL.alloc().initFileURLWithPath(url.path());
                        url.release();
                        url = fileUrl;
                    }
                    urlText.release();

                    if (url != null)
                    {
                        bool isAllowedEntityURL = (allowedEntityURLs.member(url) == null) ? true : false;
                        if ((externalPolicy == 0x8000) || (isAllowedEntityURL == false))
                        {
                            url.release();
                            if (!isAllowedEntityURL)
                                return originalLoader(URL, ID, context);
                        }
                    }
                }
                if (externalPolicy == 0x4000)
                    return originalLoader(URL, ID, context);
                if (externalPolicy != 0x80000)
                {
                    if (externalPolicy != 0x8000)
                    {
                        //xmlParserInputPtr	xmlNoNetExternalEntityLoader	(const char * URL, const char * ID, xmlParserCtxtPtr ctxt)
                        return originalLoader(URL, ID, context);
                    }
                    NSURL urlv36 = treeReaderInstance.url();
                    NSString urlText = (NSString)NSString.alloc().initWithUTF8String(URL);
                    NSURL url3 = (NSURL)NSURL.alloc().initWithString(urlText);
                    urlText.release();

                    if ((urlv36 == null) || (url3 == null))
                        return originalLoader(URL, ID, context);

                    if (url.host().isEqualToString(urlv36.host()))
                    {
                        if (url.port().isEqualToNumber(urlv36.port()))
                        {
                            bool isSchemeEqual = url.scheme().isEqualToString(urlv36.scheme());
                            url.release();

                            if (isSchemeEqual == false)
                                return (xmlParserInput*)IntPtr.Zero;
                            else
                                return originalLoader(URL, ID, context);
                        }
                        url.release();
                    }
                    else
                    {
                        url.release();
                    }
                }
            }

            return result;
        }


        public virtual NSString DTDString()
        {
            NSString dtdString = null;


            var startDTDCharSet = NSCharacterSet.characterSetWithCharactersInString("\\\"'[>");
            var endDTDCharSet = NSCharacterSet.characterSetWithCharactersInString("\\\"']");
            var wsCharSet = NSCharacterSet.whitespaceAndNewlineCharacterSet();

            var text = (NSString)NSString.alloc().initWithData(_data, NSStringEncoding.NSUTF8StringEncoding);
            NSRange range = text.rangeOfString("<!DOCTYPE");
            uint index = range.Location;
            if (range.Location < text.length())
            {
                while (true)
                {
                    char c = text.characterAtIndex(index);
                    // FIXME
                }
            }



            return dtdString;
        }

        internal virtual void _addEntity(NSXMLNode entity)
        {
            if (_text != null)
            {
                if (_text.isKindOfClass(NSXMLFidelityNode.Class) == false)
                {
                    _text.release();
                }
            }
            _text = (NSXMLFidelityNode)NSXMLFidelityNode.alloc().initWithKind(NSXMLNodeKind.NSXMLTextKind);

            uint v9 = _fidelityMask & 0x400000;
            uint v10 = v9 + 0x8000000;
            if ((_fidelityMask & 0x8000000) == 0)
                v10 = v9;
            ((NSXMLFidelityNode)_text).setFidelity(v10);

            uint index = (_content != null) ? _content.length() : 0;
            ((NSXMLFidelityNode)_text).addEntity(entity, index);

            NSMutableString str = (NSMutableString)NSMutableString.alloc().initWithFormat("&%@;", entity);
            if (_content != null)
            {
                _content.appendString(str);
            }
            else
            {
                this.setContent(str);
            }
            str.release();
        }

        protected virtual void _addContent()
        {
            if (_content != null)
            {
                _text.setObjectValue(_content);
                try
                { 
                    ((NSXMLElement)_current).addChild(_text);
                }
                catch
                {

                }
                _text.release();
                _text = null;
                setContent(null);
            }

            //_text.setObjectValue(content);
            //try
            //{
            //    ((NSXMLElement)_current).addChild(_text);
            //    _text = null;
            //    setContent(null);
            //}
            //catch
            //{

            //}
        }

        protected virtual bool setError(int error, NSString info, bool fatal)
        {
            bool hadError = false;
            NSString errMsg = info;
            int code = error;

            if ((IntPtr)_reader != IntPtr.Zero)
            {
                if (_reader->ctxt->lastError.level != xmlErrorLevel.XML_ERR_NONE)
                {
                    code = _reader->ctxt->lastError.code;
                    if (code == 27 /*XML_WAR_UNDECLARED_ENTITY */)
                        goto LABEL_14;
                    if (code == 94/*XML_ERR_NO_DTD*/ || code == 522/*XML_DTD_NO_DTD*/)
                    {
                        _isMissingDTD = true;
                        goto LABEL_14;
                    }
                    if (fatal == true)
                    {
                        if (code == 1/*XML_ERR_INTERNAL_ERROR*/)
                        {
                            errMsg = NSString.stringWithFormat("Line %d: %s\\n", _reader->ctxt->lastError.line, _reader->ctxt->lastError.message);
                        }
                        else
                        {
                            errMsg = NSString.stringWithFormat("Line %d: %s", _reader->ctxt->lastError.line, _reader->ctxt->lastError.message);
                        }
                    }
                }
            }

            if (fatal)
            {
                hadError = true;
            }



        LABEL_14:
            this._hadError = hadError;
            return hadError;
        }

        private static void _xmlTextReaderErrorFunc(IntPtr userData, IntPtr pMsg, int severity, xmlTextReaderLocatorPtr locator)
        {
            NSString msgLine;

            NSXMLTreeReader pThis = (NSXMLTreeReader)GCHandle.FromIntPtr(userData).Target;

            int line = LibXml.xmlTextReaderLocatorLineNumber(locator);
            if (pMsg != IntPtr.Zero)
            {
                msgLine = NSString.stringWithFormat("Line %d: %@", line, NSString.stringWithUTF8String(pMsg));
            }
            else
            {
                msgLine = NSString.stringWithFormat("Line %d: Unexpected error", line);
            }

            switch ((xmlParserSeverities)severity)
            {
                case xmlParserSeverities.XML_PARSER_SEVERITY_VALIDITY_WARNING:
                case xmlParserSeverities.XML_PARSER_SEVERITY_WARNING:
                    pThis.setError(1, msgLine, false);
                    break;

                case xmlParserSeverities.XML_PARSER_SEVERITY_VALIDITY_ERROR:
                case xmlParserSeverities.XML_PARSER_SEVERITY_ERROR:
                    pThis.setError(1, msgLine, true);
                    break;

                default:
                    break;
            }
        }

        internal unsafe void _initializeReader()
        {
            int options = 0;

            options = ((((_fidelityMask & 0x12000000) == 0)) ? 1 : 0 << 8) + 1024;
            if ((_fidelityMask & 0x10000) == 0)
                options = (((_fidelityMask & 0x12000000) == 0)) ? 1 : 0 << 8;
            if (_enableXMLParsingMemoryGuards == -1)
            {
                _enableXMLParsingMemoryGuards = NSUserDefaults
                    .standardUserDefaults()
                    .boolForKey("NSXMLDocumentEnableXMLParsingMemoryGuards") ? 1 : 0;
            }

            if (_enableXMLParsingMemoryGuards == 0)
                options |= 0x80000;


            byte[] bytes = _data.bytes();
            uint len = (_data != null) ? _data.length() : 0;
            _reader = LibXml.xmlReaderForMemory(bytes, (int)len, IntPtr.Zero, IntPtr.Zero, options);
            if ((IntPtr)_reader != IntPtr.Zero)
            {
                IntPtr xmlTextReaderErrorFunc = _xmlTextReaderErrorFuncDelegate.ToIntPtr();
                LibXml.xmlTextReaderSetErrorHandler(_reader, _xmlTextReaderErrorFunc, _instancePtr);

                _reader->sax->characters = _reader->characters;
                _reader->characters = _characters3Delegate.ToIntPtr();
                _reader->sax->startElementNs = _reader->startElementNs;
                _reader->startElementNs = _startElementNs1Delegate.ToIntPtr();
                _reader->sax->getEntity = _getEntity1Delegate.ToIntPtr();

                int prop;
                if ((_fidelityMask & 0x1400000) == 0)
                {
                    _reader->sax->cdataBlock = IntPtr.Zero;
                }
                prop = ((_fidelityMask & 0x400000) == 0) ? 1 : 0;
                LibXml.xmlTextReaderSetParserProp(_reader, (int)xmlParserProperties.XML_PARSER_SUBST_ENTITIES, prop);


                if ((_fidelityMask & 0x8000000) != 0)
                {
                    _reader->ctxt->options |= unchecked((int)0x80000000);
                }
                prop = ((_fidelityMask & 0x20) != 0) ? 1 : 0;
                LibXml.xmlTextReaderSetParserProp(_reader, (int)xmlParserProperties.XML_PARSER_VALIDATE, prop);
            }
        }

        private static unsafe void _startElementNs3(IntPtr pCtx,
            IntPtr pLocalname, IntPtr pPrefix, IntPtr pURI,
            int nb_namespaces, IntPtr pNamespaces,
            int nb_attributes, int nb_defaulted, IntPtr pAttributes)
        {
            xmlParserCtxt* parserCtxt = (xmlParserCtxt*)pCtx;

            //attributes: pointer to the array of (localname/prefix/URI/value/end) attribute values.
            xmlCharPtr[] attributes = pAttributes.ReadArray(nb_attributes * 5);

            //reader = (xmlTextReaderPtr)parserCtxt->_private
            xmlTextReader* pReader = (xmlTextReader*)parserCtxt->_private; //parserCtxt.Inc(0x110).Deref();
            if ((IntPtr)pReader != IntPtr.Zero)
            {
                //IntPtr reader_startElementNs = pReader->startElementNs;
                if (pReader->startElementNs != IntPtr.Zero)
                {
                    var reader_sax_startElementNs = pReader->sax->startElementNs; //pReader.Inc(0x18).DerefInc(0x74);
                    var readerSaxStartElementNs = InteropHelper.GetDelegateForFunctionPointer<LibXml.startElementNsSAX2Func>(reader_sax_startElementNs);
                    readerSaxStartElementNs(pCtx, pLocalname, pPrefix, pURI, nb_namespaces, pNamespaces, nb_attributes, nb_defaulted, pAttributes);

                    //IntPtr parserCtxt_node = parserCtxt.Inc(0x34).Deref();
                    if ((IntPtr)parserCtxt->node != IntPtr.Zero)
                    {
                        //IntPtr node_input = parserCtxt->input; //parserCtxt_node.Inc(0x24).Deref();
                        if ((IntPtr)parserCtxt->input != IntPtr.Zero)
                        {
                            IntPtr node_input_cur = parserCtxt->input->cur; //node_input.Inc(0x10).Deref();
                            if (node_input_cur != IntPtr.Zero)
                            {
                                char curChar = node_input_cur.GetChar();
                                char nextChar = node_input_cur.Inc().GetChar();
                                if (curChar == '/' && nextChar == '>')
                                {
                                    parserCtxt->node->extra = 1/*NODE_IS_EMPTY*/;
                                }
                            }

                            xmlAttr* nodeAttributes = parserCtxt->node->properties;
                            if (((IntPtr)nodeAttributes != IntPtr.Zero) && (nb_attributes > 0))
                            {
                                for (int i = 0; i < nb_attributes; i++)
                                {
                                    IntPtr v15 = attributes[(i * 5) + 4];
                                    nodeAttributes->_private = (void*)v15;
                                    nodeAttributes = nodeAttributes->next;
                                }
                            }
                        }
                    }
                }

                pReader->state = xmlTextReaderState.XML_TEXTREADER_ELEMENT;
            }
        }

        private static unsafe void _characters3(IntPtr ctx, IntPtr ch, int aLen)
        {
            xmlParserCtxt* parserCtx = (xmlParserCtxt*)ctx;

            xmlTextReader* reader = (xmlTextReader*)IntPtr.Zero;
            IntPtr reader_characters = IntPtr.Zero;
            IntPtr parserCtx_input_base = IntPtr.Zero;
            IntPtr parserCtx_input_cur = IntPtr.Zero;


            reader = (xmlTextReader*)parserCtx->_private;
            reader_characters = reader->characters;
            if (((IntPtr)reader != IntPtr.Zero) && ((IntPtr)reader_characters != IntPtr.Zero))
            {
                //options = (int)parserCtx->options;
                int options = parserCtx->options; // (Int32)parserCtx.Inc(0x168).Deref();
                parserCtx_input_base = parserCtx->input->_base; // parserCtx.Inc(0x24).Deref().Inc(0x0C).Deref();
                parserCtx_input_cur = parserCtx->input->cur;
                IntPtr pPrevChar = parserCtx_input_cur.Dec(-1);
                IntPtr pCurChar = parserCtx_input_cur;

                //if (options < 0 && *(parserCtx->input->cur - 1))
                char prevChar = pPrevChar.Deref().GetChar();
                if ((options < 0) && (prevChar == ';'))
                {
                    if (ch.GetChar() != pCurChar.Deref().GetChar())
                    {
                        if (pPrevChar.GetChar() != parserCtx_input_base.GetChar())
                        {
                            int index = 0;
                            while (true)
                            {
                                if (prevChar == ' ')
                                    return;
                                if (prevChar == '#')
                                    break;

                                prevChar = pPrevChar.Dec().GetChar();
                                index++;
                                pPrevChar = pPrevChar.Dec();

                                if (pPrevChar == parserCtx_input_base)
                                {
                                    if (prevChar == '#')
                                    {
                                        goto LABEL_14;
                                    }
                                }
                            }
                            parserCtx_input_base = pPrevChar;

                        LABEL_14:
                            var name = LibXml.xmlStrsub(parserCtx_input_base, 0, index);
                            var pCurnode = LibXml.xmlNewCharRef(parserCtx->myDoc, name);
                            LibXml.xmlAddChild(parserCtx->node, pCurnode);
                            LibXml.xmlFree(name);
                        }
                    }
                }
            }

        }

        private static unsafe xmlEntity* _getEntity3(IntPtr pCtx, IntPtr pName)
        {
            xmlEntity* pEntity = (xmlEntity*)IntPtr.Zero;

            xmlParserCtxt* parserCtxt = (xmlParserCtxt*)pCtx;

            //reader = (xmlTextReaderPtr)reader->ctx->_private
            xmlTextReader* pReader = (xmlTextReader*)parserCtxt->_private; //parserCtxt.Inc(0x110).Deref();
            if ((IntPtr)pReader != IntPtr.Zero)
            {
                //reader_sax_getEntity = pReader->sax.getEntity;
                //IntPtr reader_sax_getEntity = pReader.Inc(0x18).DerefInc(0x14).Deref();
                if (pReader->sax->getEntity != IntPtr.Zero)
                {
                    var readerSaxGetEntity = InteropHelper.GetDelegateForFunctionPointer<LibXml.getEntitySAXFunc>(pReader->sax->getEntity);
                    pEntity = readerSaxGetEntity(pCtx, pName);

                    if (parserCtxt->replaceEntities == 0)
                    {
                        if (parserCtxt->instate == xmlParserInputState.XML_PARSER_CONTENT)
                        {
                            if ((IntPtr)pEntity != IntPtr.Zero)
                            {
                                //if ((int)pEntity.Inc(0x30).Deref() == LibXml.XML_INTERNAL_PREDEFINED_ENTITY)
                                if (pEntity->etype == xmlEntityType.XML_INTERNAL_PREDEFINED_ENTITY)
                                {
                                    if ((IntPtr)parserCtxt->node != IntPtr.Zero)
                                    {
                                        var pCurnode = LibXml.xmlNewReference(parserCtxt->myDoc, pName);
                                        LibXml.xmlAddChild(parserCtxt->node, pCurnode);
                                    }
                                    pEntity->content = _nativeEmptyString;
                                }
                            }
                            else
                            {
                                if ((IntPtr)parserCtxt->node != IntPtr.Zero)
                                {
                                    var pCurnode = LibXml.xmlNewReference(parserCtxt->myDoc, pName);
                                    LibXml.xmlAddChild(parserCtxt->node, pCurnode);
                                }
                            }
                        }
                    }
                }
            }

            return pEntity;
        }

        public unsafe virtual void processRealDocument(xmlDoc* doc)
        {
            NSXMLDocument nsDoc = (NSXMLDocument)_documentClass.alloc().init();
            nsDoc.setURI(this.URI());
            NSString docVer = (NSString)NSString.alloc().initWithUTF8String(doc->version);
            nsDoc.setVersion(docVer);
            docVer.release();
            if (doc->encoding != null)
            {
                NSString docEncoding = (NSString)NSString.alloc().initWithUTF8String(doc->encoding);
                nsDoc.setCharacterEncoding(docEncoding);
                docEncoding.release();
            }
            if (doc->standalone != -1)
            {
                nsDoc.setStandalone((doc->standalone == 1) ? true : false);
            }
            this.setRoot(nsDoc);
            _current = nsDoc;
            nsDoc.release();
        }

        public unsafe virtual void processNode(xmlTextReader* reader)
        {
            int ret = 0;
            if (_wasEmpty)
            {
                if (reader->state == xmlTextReaderState.XML_TEXTREADER_ELEMENT)
                {
                    _current = _current.parent();
                }
                _wasEmpty = false;
            }
            
            xmlReaderTypes nodeType = (xmlReaderTypes)LibXml.xmlTextReaderNodeType(reader);
            switch(nodeType)
            {
                case xmlReaderTypes.XML_READER_TYPE_ELEMENT: { processElement(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_TEXT: { processText(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_CDATA: { processCDATA(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_ENTITY_REFERENCE: { processEntityReference(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_ENTITY: { processEntity(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_PROCESSING_INSTRUCTION: { processProcessingInstruction(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_COMMENT: { processComment(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_DOCUMENT: { processDocument(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_DOCUMENT_TYPE: { processDocumentType(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_DOCUMENT_FRAGMENT: { processDocumentFragment(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_NOTATION: { processNotation(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_WHITESPACE: { processWhitespace(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_SIGNIFICANT_WHITESPACE: { processSignificantWhitespace(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_END_ELEMENT: { processEndElement(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_END_ENTITY: { processEndEntity(reader); break; }
                case xmlReaderTypes.XML_READER_TYPE_XML_DECLARATION: { processXMLDeclaration(reader); break; }

                default:
                    break;
            }

            return;
        }


        public unsafe virtual NSXMLNamedNode createNamedNodeFromNode(xmlNode* pNode, xmlTextReader* pReader)
        {
            NSXMLNamedNode node = null;

            var nodekind = (pNode->type != xmlElementType.XML_ATTRIBUTE_NODE) ? NSXMLNodeKind.NSXMLNamespaceKind : NSXMLNodeKind.NSXMLAttributeKind;
            if ((_fidelityMask & 0x800018) != 0)
            {
                node = (NSXMLNamedFidelityNode)NSXMLNamedFidelityNode.alloc().initWithKind(nodekind);
                if ((_fidelityMask & 0x18) != 0)
                {
                    if ((int)pNode->_private == 39)
                    {
                        ((NSXMLNamedFidelityNode)node).setFidelity(0x800008);
                    }
                    else
                    {
                        ((NSXMLNamedFidelityNode)node).setFidelity(0x800010);
                    }
                }
            }
            else
            {
                node = (NSXMLNamedNode)NSXMLNamedNode.alloc().initWithKind(nodekind);
            }

            if (pNode->type == xmlElementType.XML_ATTRIBUTE_NODE)
            {
                IntPtr pName = LibXml.xmlTextReaderConstName(pReader);
                NSString name = (NSString)NS.MapGet(_xmlCharToNSString, pName);
                if (name != null)
                {
                    node.setName(name);
                }
                else
                {
                    name = (NSString)NSString.alloc().initWithUTF8String(pName);
                    NS.MapInsert(_xmlCharToNSString, pName, name);
                    node.setName(name);
                    name.release();
                }
                IntPtr pNsUri = LibXml.xmlTextReaderConstNamespaceUri(pReader);
                if (pNsUri != null)
                {
                    NSString nsUri = (NSString)NSString.alloc().initWithUTF8String(pNsUri);
                    node.setURI(nsUri);
                    nsUri.release();
                }
            }
            else
            {
                NSString name;
                if (pNode->name == IntPtr.Zero)
                {
                    name = (NSString)NSString.alloc().initWithString("");
                }
                else
                {
                    name = (NSString)NSString.alloc().initWithUTF8String(pNode->name);
                }
                node.setName(name);
                name.release();
            }

            var pTextValue = LibXml.xmlTextReaderConstValue(pReader);
            if (pTextValue != null)
            {
                NSString textValue = (NSString)NSString.alloc().initWithUTF8String(pTextValue);
                node.setObjectValue(textValue);
                textValue.release();
            }

            return node;
        }


        public unsafe virtual void processElement(xmlTextReader* reader)
        {
            NSXMLElement element = null;
            
            bool shouldReleaseName = false;

            bool isEmptyElement = LibXml.xmlTextReaderIsEmptyElement(reader) == 1 ? true : false;
            if (_additiveContent)
                this._addContent();

            NSString name = null;
            IntPtr pName = LibXml.xmlTextReaderConstName(reader);
            if (NS.MapGet(_xmlCharToNSString, pName) == null)
            {
                name = (NSString)NSString.alloc().initWithUTF8String(pName);
                NS.MapInsert(_xmlCharToNSString, pName, name);
                shouldReleaseName = true;
            }

            IntPtr pPrefix = LibXml.xmlTextReaderConstPrefix(reader);

            NSString nsUri = null;
            IntPtr pNsUri = LibXml.xmlTextReaderConstNamespaceUri(reader);
            if (pNsUri != IntPtr.Zero)
            {
                nsUri = (NSString)NS.MapGet(_xmlCharToNSString, pNsUri);
                if (nsUri == null)
                {
                     nsUri = (NSString)NSString.alloc().initWithUTF8String(pNsUri);
                    NS.MapInsert(_xmlCharToNSString, pNsUri, nsUri);
                }
                else
                    nsUri.retain();
            }

            uint fidelity = 0;
            if ((_fidelityMask & 0x800000) != 0)
            {
                fidelity = 0x800000;
            }
            if ((_fidelityMask & 0x06) != 0 && isEmptyElement)
            {
                fidelity = 0x800004;
            }
            if ((_fidelityMask & 0x2800006) == 0)
            {
                if (_elementClassOverridden == false)
                {
                    if (pPrefix != IntPtr.Zero)
                       element = (NSXMLElement)NSXMLElement.alloc()._initWithName(name, nsUri, pPrefix.strlen());
                    else
                        element = (NSXMLElement)NSXMLElement.alloc()._initWithName(name, nsUri, -2);   
                }
                else
                {
                    element = (NSXMLElement)((NSXMLElement)_elementClass.alloc()).initWithNameURI(name, nsUri);
                }
            }
            else
            {
                if (pPrefix != IntPtr.Zero)
                    element = (NSXMLFidelityElement)NSXMLFidelityElement.alloc()._initWithName(name, nsUri, pPrefix.strlen());
                else
                    element = (NSXMLFidelityElement)NSXMLFidelityElement.alloc()._initWithName(name, nsUri, -2);

                ((NSXMLFidelityElement)element).setFidelity(fidelity);
                
            }

            if (shouldReleaseName)
                name.release();
            nsUri.release();

            int ret = LibXml.xmlTextReaderMoveToFirstAttribute(reader);
            for (uint i = 0; ret == 1; i++ )
            {
                xmlNode* curNode = LibXml.xmlTextReaderCurrentNode(reader);
                NSXMLNamedNode namedNode = createNamedNodeFromNode(curNode, reader);
                if (curNode->type == xmlElementType.XML_ATTRIBUTE_NODE)
                {
                    if (_elementClassOverridden)
                        element.addAttribute(namedNode);
                    else
                        element._addTrustedAttribute(namedNode, i);

                }

                ret = LibXml.xmlTextReaderMoveToFirstAttribute(reader);
            }

        }

        public unsafe virtual void processText(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processCDATA(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processEntityReference(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processEntity(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processProcessingInstruction(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processComment(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processDocument(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processDocumentType(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processDocumentFragment(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processNotation(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processWhitespace(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processSignificantWhitespace(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processEndElement(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processEndEntity(xmlTextReader* reader)
        {

        }

        public unsafe virtual void processXMLDeclaration(xmlTextReader* reader)
        {

        }



        public virtual NSXMLNode parse()
        {
            xmlDoc* doc = null;
            int ret;

            _error = null;
            this._initializeReader();
            if ((IntPtr)this._reader == IntPtr.Zero)
                goto LABEL_22;

            doc = LibXml.xmlTextReaderCurrentDoc(this._reader);
            if (doc != null)
                doc->_private = (void*)_instancePtr;
            ret = LibXml.xmlTextReaderRead(_reader);
            if (ret != 1 && ((_fidelityMask & 0x20)!=0) && _isMissingDTD)
            {
                LibXml.xmlTextReaderClose(_reader);
                LibXml.xmlFreeTextReader(_reader);
                _reader = null;
                this._initializeReader();
                if (_reader != null)
                {
                    LibXml.xmlTextReaderSetParserProp(_reader, 3, 0);
                    ret = LibXml.xmlTextReaderRead(_reader);
                }
                else
                    goto LABEL_22;
            }

            try
            {
                doc = LibXml.xmlTextReaderCurrentDoc(_reader);
                if(doc != null)
                {
                    doc->_private = (void*)_instancePtr;
                    this.processRealDocument(doc);
                    ret = LibXml.xmlTextReaderRead(_reader);
                    while (ret == 1)
                    {
                        if (_hadError)
                            break;

                        processNode(_reader);

                        ret = LibXml.xmlTextReaderRead(_reader);
                    }
                    LibXml.xmlFreeDoc(doc);
                    LibXml.xmlTextReaderClose(_reader);
                    LibXml.xmlFreeTextReader(_reader);
                    _reader = null;
                }
            }
            catch(Exception ex)
            {

            }



        LABEL_22:
            if ((_fidelityMask & 0x400000) != 0)
            {
                //use global alloc
                //LibXml.xmlGetPredefinedEntity2(LibXml.NativeLTString)->content = LibXml.NativeLTEntityString;
                //LibXml.xmlGetPredefinedEntity2(LibXml.NativeGTString)->content = LibXml.NativeGTEntityString;
                //LibXml.xmlGetPredefinedEntity2(LibXml.NativeAmpString)->content = LibXml.NativeLTEntityString;
                //LibXml.xmlGetPredefinedEntity2(LibXml.NativeQuoteString)->content = LibXml.NativeLTEntityString; ;
                //LibXml.xmlGetPredefinedEntity2(LibXml.NativeAposString)->content = LibXml.NativeLTEntityString;

            }

            NSXMLNode root = _root;
            NSXMLNodeKind rootKind = (_root != null) ? _root.kind() : NSXMLNodeKind.NSXMLInvalidKind;
            if (_root == null || _hadError ||_isMissingDTD)
            {
                if ((_fidelityMask & 0x20) == 0 || _isMissingDTD == false || 
                    rootKind != NSXMLNodeKind.NSXMLDocumentKind || ((NSXMLDocument)_root).rootElement() == null ||
                    ((NSXMLDocument)_root).DTD() != null || ((NSXMLDocument)_root)._validateWithSchemaAndReturnError(_error))
                {
                    this.setRoot(null);
                    goto LABEL_35;
                }
            }
            else
            {

                if ((_fidelityMask & 0x40) != 0 && rootKind == NSXMLNodeKind.NSXMLDocumentKind)
                {
                    NSXMLDTD dtd = ((NSXMLDocument)_root).DTD();
                    if (dtd != null)
                    {
                        dtd._setDTDString(this.DTDString());
                    }
                }
            }
        LABEL_35:
            NSThread.currentThread().threadDictionary().removeObjectForKey(ThreadXmlTreeReaderTag);
            if (_root == null && _error == null)
            {
                NSDictionary userInfo = (NSDictionary)NSDictionary.dictionaryWithObjectForKey((NSString)"An unknown error occurred", (NSString)"NSLocalizedDescriptionKey");
                _error = (NSError)NSError.alloc().initWithDomain("NSXMLParserErrorDomain", 1, userInfo);
            }

            return _root;
        }



    }
}
