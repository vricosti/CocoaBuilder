using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSValueTransformer : NSObject
    {
        new public static Class Class = new Class(typeof(NSValueTransformer));
        new public static NSValueTransformer alloc() { return new NSValueTransformer(); }
    }
}
