using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSIndexSet : NSObject
    {
        new public static Class Class = new Class(typeof(NSIndexSet));
        new public static NSIndexSet alloc() { return new NSIndexSet(); }
    }
}
