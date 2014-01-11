using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using xmlExternalEntityLoaderPtr = System.IntPtr;
using xmlParserInputPtr = System.IntPtr;
using xmlParserCtxtPtr = System.IntPtr;
using xmlTextReaderPtr = System.IntPtr;
using xmlTextReaderLocatorPtr = System.IntPtr;
using xmlEntityPtr = System.IntPtr;
using xmlDocPtr = System.IntPtr;

namespace Smartmobili.Cocoa
{
    public class NSXMLTreeReader : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLTreeReader));
        new public static NSXMLTreeReader alloc() { return new NSXMLTreeReader(); }


        GCHandle _instanceHandle;
        IntPtr _instancePtr;
        private static LibXml.xmlTextReaderErrorFunc _xmlTextReaderErrorFuncDelegate;
        private static LibXml.charactersSAXFunc _characters3Delegate;
        private static LibXml.startElementNsSAX2Func _startElementNs1Delegate;
        private static LibXml.getEntitySAXFunc _getEntity1Delegate;

        private static bool _entitySetupOncePredicate;
        private static IntPtr __originalLoader;
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
        protected xmlTextReaderPtr _reader; //0x48
        protected Class _documentClass; //0x4C
        protected Class _dtdClass; //0x50
        protected Class _dtdNodeClass; //0x54
        protected Class _elementClass; //0x58
        protected Class _nodeClass; //0x5C

        public NSXMLTreeReader()
        {
            _instanceHandle = GCHandle.Alloc(this);
            _instancePtr = GCHandle.ToIntPtr(_instanceHandle);//(IntPtr)_instanceHandle;
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


        public virtual id initWithData(NSData data, Class documentClass, uint mask, ref NSError error)
        {
            return initWithData(data, documentClass, false, mask, ref error);
        }

        public virtual id initWithData(NSData data, Class documentClass, bool isSingleDTDNode, uint mask, ref NSError error)
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
                    LibXml.xmlSetExternalEntityLoader(Marshal.GetFunctionPointerForDelegate(_xmlExternalEntityLoaderDelegate));
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




        //xmlParserInputPtr xmlExternalEntityLoader(IntPtr URL, IntPtr ID, xmlParserCtxtPtr context);
        //public T GetDelegateForFunctionPointer<T>(IntPtr addr) where T : class
        //{
        //    System.Delegate fn_ptr = Marshal.GetDelegateForFunctionPointer(addr, typeof(T));
        //    return fn_ptr as T;
        //}

        private static unsafe xmlParserInputPtr _xmlExternalEntityLoader(IntPtr URL, IntPtr ID, xmlParserCtxtPtr context)
        {
            xmlParserInputPtr result = IntPtr.Zero;

            var originalLoader = InteropHelper.GetDelegateForFunctionPointer<LibXml.xmlExternalEntityLoader>(__originalLoader);

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
                                return IntPtr.Zero;
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

        protected virtual void _addContent(NSString content)
        {
            _text.setObjectValue(content);
            try
            {
                ((NSXMLElement)_current).addChild(_text);
                _text = null;
                setContent(null);
            }
            catch
            {

            }
        }

        protected virtual bool setError(bool error, NSString info, bool fatal)
        {
            bool result = false;

            if (_reader != IntPtr.Zero)
            {

            }
            else
            {

            }

            return result;
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
                    pThis.setError(true, msgLine, false);
                    break;

                case xmlParserSeverities.XML_PARSER_SEVERITY_VALIDITY_ERROR:
                case xmlParserSeverities.XML_PARSER_SEVERITY_ERROR:
                    pThis.setError(true, msgLine, true);
                    break;

                default:
                    break;
            }
        }

        internal void _initializeReader()
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
            if (_reader != IntPtr.Zero)
            {
                IntPtr xmlTextReaderErrorFunc = _xmlTextReaderErrorFuncDelegate.ToIntPtr();
                LibXml.xmlTextReaderSetErrorHandler(_reader, _xmlTextReaderErrorFunc, _instancePtr);

                ///////////////////////////////////////////////////////////////////////////////////////////
                //reader->sax->characters = reader->characters
                //reader->characters = _characters3
                //reader->sax->startElementNs = reader->startElementNs
                //reader->startElementNs = _startElementNs_1dc7d5
                //reader->sax->getEntity =  _getEntity_209819
#if WIN32
                _reader.Inc(0x18).Deref().Inc(0x44).Assign(_reader.Inc(0x30));
                _reader.Inc(0x30).Assign(_characters3Delegate.ToIntPtr());
                _reader.Inc(0x18).Deref().Inc(0x74).Assign(_reader.Inc(0x28));
                _reader.Inc(0x28).Assign(_startElementNs1Delegate.ToIntPtr());
                _reader.Inc(0x18).Deref().Inc(0x18).Assign(_getEntity1Delegate.ToIntPtr());
#else

#endif
                ///////////////////////////////////////////////////////////////////////////////////////////
                int prop;
                if ((_fidelityMask & 0x1400000) == 0)
                {
                    //reader->sax->cdataBlock = null;
                }
                prop = ((_fidelityMask & 0x400000) == 0) ? 1 : 0;
                LibXml.xmlTextReaderSetParserProp(_reader, 4, prop);


                if ((_fidelityMask & 0x8000000) != 0)
                {
                    //reader->ctxt->+0x168(x86)??? |= 0x80000000;
                }
                prop = ((_fidelityMask & 0x20) != 0) ? 1 : 0;
                LibXml.xmlTextReaderSetParserProp(_reader, 3, prop);
            }
        }

        private static unsafe void _startElementNs3(IntPtr pCtx, 
            IntPtr pLocalname, IntPtr pPrefix, IntPtr pURI, 
            int nb_namespaces, IntPtr namespaces,
            int nb_attributes, int nb_defaulted, IntPtr attributes)
        {
            xmlParserCtxtPtr parserCtxt = pCtx;

            //reader = (xmlTextReaderPtr)parserCtxt->_private
            xmlTextReaderPtr pReader = parserCtxt.Inc(0x110).Deref();
            if (pReader != IntPtr.Zero)
            {
                IntPtr reader_startElementNs = pReader.Inc(0x28).Deref();
                if (reader_startElementNs != IntPtr.Zero)
                {
                    var reader_sax_startElementNs = pReader.Inc(0x18).DerefInc(0x74);
                    var readerSaxStartElementNs = InteropHelper.GetDelegateForFunctionPointer<LibXml.startElementNsSAX2Func>(reader_sax_startElementNs);
                    readerSaxStartElementNs(pCtx, pLocalname, pPrefix, pURI, nb_namespaces, namespaces, nb_attributes, nb_defaulted, attributes);

                    IntPtr parserCtxt_node = parserCtxt.Inc(0x34).Deref();
                    if (parserCtxt_node != IntPtr.Zero)
                    {
                        IntPtr node_input = parserCtxt_node.Inc(0x24).Deref();
                        if (node_input != IntPtr.Zero)
                        {
                            IntPtr node_input_cur = node_input.Inc(0x10).Deref();
                            if (node_input_cur != IntPtr.Zero)
                            {
                                char curChar = node_input_cur.GetChar();
                                char nextChar = node_input_cur.Inc().GetChar();
                                if (curChar == '/' && nextChar == '>')
                                {
                                    //FIXME
                                    //*(_WORD*)(parserCtxt_node + 0x3A) = 1;
                                }
                            }

                            //FIXME
                            //v14 = *(_DWORD*)(parserCtxt_node + 0x2C);
                            //if (v14 && nb_attr > 0)
                            //{
                            //    v15 = attr + 0x10;
                            //    do
                            //    {
                            //        *(_DWORD*)v14 = **(_BYTE**)v15;
                            //        v15 += 20;
                            //        v14 = *(_DWORD*)(v14 + 0x18);
                            //        --v10;
                            //    }
                            //    while (v10);
                            //}
                        }
                    }
                }
                //*(_DWORD*)(reader + 0x10) = 1;
            }
        }

        private static unsafe void _characters3(IntPtr ctx, IntPtr ch, int aLen)
        {
            xmlParserCtxtPtr parserCtx = ctx;
            
            xmlTextReaderPtr reader = IntPtr.Zero;
            IntPtr reader_characters = IntPtr.Zero;
            IntPtr parserCtx_input_base =  IntPtr.Zero;
            IntPtr parserCtx_input_cur =  IntPtr.Zero;

#if WIN32
            //reader = (xmlTextReaderPtr)reader->ctx->_private
            reader = parserCtx.Inc(0x110).Deref();
            reader_characters = reader.Inc(0x30).Deref();
#endif
            if ((reader != IntPtr.Zero) && (reader_characters != IntPtr.Zero))
            {
                //options = (int)parserCtx->options;
                int options = (Int32)parserCtx.Inc(0x168).Deref();
                parserCtx_input_base = parserCtx.Inc(0x24).Deref().Inc(0x0C).Deref();
                parserCtx_input_cur = parserCtx.Inc(0x24).Deref().Inc(0x10).Deref();
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
                            IntPtr parserCtxt_myDoc = parserCtx.Inc(0x8).Deref();
                            IntPtr parserCtxt_node = parserCtx.Inc(0x34).Deref();
                            var name = LibXml.xmlStrsub(parserCtx_input_base, 0, index);
                            var pCurnode = LibXml.xmlNewCharRef(parserCtxt_myDoc, name);
                            LibXml.xmlAddChild(parserCtxt_node, pCurnode);
                            LibXml.xmlFree(name);
                        }
                    }
                }
            }

        }

        private static unsafe IntPtr _getEntity3(IntPtr pCtx, IntPtr pName)
        {
            IntPtr result = IntPtr.Zero;

            xmlParserCtxtPtr parserCtxt = pCtx;

            //reader = (xmlTextReaderPtr)reader->ctx->_private
            xmlTextReaderPtr pReader = parserCtxt.Inc(0x110).Deref();
            if (pReader != IntPtr.Zero)
            {
                //reader_sax_getEntity = pReader->sax.getEntity;
                IntPtr reader_sax_getEntity = pReader.Inc(0x18).DerefInc(0x14).Deref();
                if (reader_sax_getEntity != IntPtr.Zero)
                {
                    var readerSaxGetEntity = InteropHelper.GetDelegateForFunctionPointer<LibXml.getEntitySAXFunc>(reader_sax_getEntity);
                    xmlEntityPtr pEntity = readerSaxGetEntity(pCtx, pName);
                    
                    //if ( parserCtxt->replaceEntities == 0)
                    if ((int)parserCtxt.Inc(0x10).Deref() == 0)
                    {
                        if ((int)parserCtxt.Inc(0xAC).Deref() == LibXml.XML_PARSER_CONTENT)
                        {
                            if (pEntity != IntPtr.Zero)
                            {
                                if ((int)pEntity.Inc(0x30).Deref() == LibXml.XML_INTERNAL_PREDEFINED_ENTITY)
                                {
                                    IntPtr parserCtxt_node = parserCtxt.Inc(0x34).Deref();
                                    if (parserCtxt_node != IntPtr.Zero)
                                    {
                                        IntPtr parserCtxt_myDoc = parserCtxt.Inc(0x8).Deref();
                                        var pCurnode = LibXml.xmlNewReference(parserCtxt_myDoc, pName);
                                        LibXml.xmlAddChild(parserCtxt_node, pCurnode);
                                    }
                                    //FIXME
                                    //*(_DWORD *)(pEntity + 0x28) = &byte_2B5785[1];
                                    // MAYBE pEntity->content = "";
                                }
                            }
                            else
                            {
                                IntPtr parserCtxt_node = parserCtxt.Inc(0x34).Deref();
                                if (parserCtxt_node != IntPtr.Zero)
                                {
                                    IntPtr parserCtxt_myDoc = parserCtxt.Inc(0x8).Deref();
                                    var pCurnode = LibXml.xmlNewReference(parserCtxt_myDoc, pName);
                                    LibXml.xmlAddChild(parserCtxt_node, pCurnode);
                                }
                            }
                        }
                    }
                }
            }

            return IntPtr.Zero;
        }


        public virtual NSXMLNode parse()
        {
            _error = null;
            this._initializeReader();
            if (this._reader == IntPtr.Zero)
                goto LABEL_22;

            xmlDocPtr doc = LibXml.xmlTextReaderCurrentDoc(this._reader);


        LABEL_22: 
            ;

            return null;
        }



    }
}
