using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSGraphicsContext : NSObject
    {
        new public static Class Class = new Class(typeof(NSGraphicsContext));
        new public static NSGraphicsContext Alloc() { return new NSGraphicsContext(); }
    }
}
