using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSLocale : NSObject
    {
        new public static Class Class = new Class(typeof(NSLocale));
        new public static NSLocale alloc() { return new NSLocale(); }
    }
}
