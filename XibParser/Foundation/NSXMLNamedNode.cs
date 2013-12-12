using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLNamedNode : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLNamedNode));
        new public static NSXMLNamedNode alloc() { return new NSXMLNamedNode(); }

        protected NSString _URI;

        protected NSString _name;

        protected int _prefixIndex;

        //public virtual NSString _XMLStringWithOptions()
        //{

        //}

        public NSString name()
        {
            return _name;
        }









    }
}
