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

        protected bool _additiveContent;
        protected NSSet _allowedEntityURLs;
        protected NSMutableString _content;
        protected NSXMLNode _current;
        protected NSData _data;
        protected Class _documentClass;
        protected Class _dtdClass;
        protected Class _dtdNodeClass;
        protected Class _elementClass;
        protected bool _elementClassOverridden;
        protected NSError _error;
        protected int _externalEntityLoadingPolicy;
        protected uint _fidelityMask;
        protected bool _hadError;
        protected bool _isMissingDTD;
        protected bool _isSingleDTDNode;
        protected Class _nodeClass;
        protected id _reader;
        protected id _readerCharacters;
        protected NSXMLNode _root;
        protected NSXMLNode _text;
        protected bool _wasEmpty;
        protected NSString _whitespace;
        protected NSMapTable _xmlCharHashToNSString;
        protected NSMapTable _xmlCharToNSString;
        protected NSString _uri;
        protected NSURL _url;

        public virtual NSString Uri()
        {
            return _uri;
        }

        public virtual void setContent(NSMutableString content)
        {

        }

        public virtual id initWithData(NSData data, Class documentClass, uint mask, ref NSError error)
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
