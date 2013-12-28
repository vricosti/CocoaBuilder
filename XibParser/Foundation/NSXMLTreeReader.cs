using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLTreeReader : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLTreeReader));
        new public static NSXMLTreeReader alloc() { return new NSXMLTreeReader(); }

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
        protected id _reader; //0x48
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

            return null;
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

        public virtual id parse()
        {
            return null;
        }



    }
}
