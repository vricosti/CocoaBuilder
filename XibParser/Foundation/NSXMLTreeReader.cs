using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using xmlExternalEntityLoaderPtr = System.IntPtr;
using xmlParserInputPtr = System.IntPtr;
using xmlParserCtxtPtr = System.IntPtr;
using xmlTextReaderPtr = System.IntPtr;


namespace Smartmobili.Cocoa
{
    public class NSXMLTreeReader : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLTreeReader));
        new public static NSXMLTreeReader alloc() { return new NSXMLTreeReader(); }

        private static bool _entitySetupOncePredicate;
        private static IntPtr __originalLoader;
        private NSString ThreadXmlTreeReaderTag = "__CurrentNSXMLTreeReader";
        private  LibXml.xmlExternalEntityLoader _xmlExternalEntityLoaderDelegate;
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
        protected NSError _error; //0x2C
        protected NSMutableString _content; //0x30
        protected NSString _whitespace; //0x34
        protected NSXMLNode _text; //0x38
        protected NSMapTable _xmlCharToNSString; //0x3C
        protected NSMapTable _xmlCharHashToNSString; //0x40
        protected id _readerCharacters; //0x44
        protected IntPtr _reader; //0x48
        protected Class _documentClass; //0x4C
        protected Class _dtdClass; //0x50
        protected Class _dtdNodeClass; //0x54
        protected Class _elementClass; //0x58
        protected Class _nodeClass; //0x5C
       
      
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
            if(_url == null)
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
            if(_allowedEntityURLs != allowedEntityURLs)
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
        public T GetDelegateForFunctionPointer<T>(IntPtr addr, Type t) where T : class
        {
            System.Delegate fn_ptr = Marshal.GetDelegateForFunctionPointer(addr, typeof(T));
            return fn_ptr as T;
        }

        private unsafe xmlParserInputPtr _xmlExternalEntityLoader(IntPtr URL, IntPtr ID, xmlParserCtxtPtr context)
        {
            xmlParserInputPtr result = IntPtr.Zero;

            var originalLoader = GetDelegateForFunctionPointer<LibXml.xmlExternalEntityLoader>(__originalLoader, typeof(LibXml.xmlExternalEntityLoader));

            NSXMLTreeReader treeReaderInstance = NSThread.currentThread().threadDictionary().objectForKey(ThreadXmlTreeReaderTag) as NSXMLTreeReader;
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
                    if(externalPolicy != 0x8000)
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
                while(true)
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
                if(_text.isKindOfClass(NSXMLFidelityNode.Class) == false)
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

        internal void _initializeReader()
        {
            int options = 0;
            
            options = (( ((_fidelityMask & 0x12000000) == 0)) ? 1 : 0 << 8) + 1024;
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

            }
        }


        public virtual id parse()
        {
           // _error = null;
            this._initializeReader();

            return null;
        }



    }
}
