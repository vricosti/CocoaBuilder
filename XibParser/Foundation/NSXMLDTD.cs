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
    }
}
