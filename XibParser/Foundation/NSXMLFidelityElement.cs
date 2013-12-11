﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLFidelityElement : NSXMLElement
    {
        new public static Class Class = new Class(typeof(NSXMLFidelityElement));
        new public static NSXMLFidelityElement Alloc() { return new NSXMLFidelityElement(); }

        protected NSString _startWhitespace;
        protected NSString _endWhitespace;
        protected uint _fidelity;


        public virtual id Init()
        {
            id self = this;

           if (base.Init() != null)
           {
               _kind = NSXMLNodeKind.NSXMLElementKind;
               _fidelity = 0;
           }

            return self;
        }

        public uint GetFidelity()
        {
            return _fidelity;
        }

        public void SetFidelity(uint fidelity)
        {
            _fidelity = fidelity;
        }

        public void SetWhitespace(NSString startSpace)
        {
            _startWhitespace = startSpace;
        }

        public void SetEndWhitespace(NSString endSpace)
        {
            _endWhitespace = endSpace;
        }
   



    }
}
