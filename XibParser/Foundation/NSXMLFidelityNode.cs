﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLFidelityNode : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLFidelityNode));
        new public static NSXMLFidelityNode alloc() { return new NSXMLFidelityNode(); }

        public static void setObjectValuePreservingEntitiesForNode(id anObject, NSString str)
        {
            throw new NotImplementedException();
        }

    }
}