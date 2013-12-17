using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLDTD : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLDTD));
        new public static NSXMLDTD alloc() { return new NSXMLDTD(); }

        protected NSString _name;
        protected NSString _publicID;
        protected NSString _systemID;
        protected NSXMLChildren _children;
        protected bool _childrenHaveMutated;
        protected NSMutableDictionary _entities;
        protected NSMutableDictionary _elements;
        protected NSMutableDictionary _notations;
        protected NSMutableDictionary _attributes;
        protected NSString _original;
        protected bool _modified;



    }
}
