using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLFidelityElement : NSXMLElement
    {
        new public static Class Class = new Class(typeof(NSXMLFidelityElement));
        new public static NSXMLFidelityElement alloc() { return new NSXMLFidelityElement(); }

        protected NSString _startWhitespace;
        protected NSString _endWhitespace;
        protected uint _fidelity;


        public virtual id init()
        {
            id self = this;

           if (base.init() != null)
           {
               _kind = NSXMLNodeKind.NSXMLElementKind;
               _fidelity = 0;
           }

            return self;
        }

        public virtual uint GetFidelity()
        {
            return _fidelity;
        }

        public virtual void SetFidelity(uint fidelity)
        {
            _fidelity = fidelity;
        }

        public virtual void SetWhitespace(NSString startSpace)
        {
            _startWhitespace = startSpace;
        }

        public virtual void SetEndWhitespace(NSString endSpace)
        {
            _endWhitespace = endSpace;
        }
   



    }
}
