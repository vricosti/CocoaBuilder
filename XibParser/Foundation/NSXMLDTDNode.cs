using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLDTDNode : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLDTDNode));
        new public static NSXMLDTDNode alloc() { return new NSXMLDTDNode(); }

        protected uint _DTDKind;


        public virtual void setDTDKind(uint kind)
        {
            _DTDKind = kind;
            if (_parent != null)
                ((NSXMLDTD)_parent)._setModified(true);

        }
    }
}
