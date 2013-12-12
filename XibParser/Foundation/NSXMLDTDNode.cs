using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLDTDNode : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLDTDNode));
        new public static NSXMLDTDNode Alloc() { return new NSXMLDTDNode(); }
    }
}
