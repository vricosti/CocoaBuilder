using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{ 
    public class NSMapTable : NSObject
    {
        new public static Class Class = new Class(typeof(NSMapTable));
        new public static NSMapTable alloc() { return new NSMapTable(); }

        public static id get(NSMapTable map, IntPtr ptr)
        {
            // FIXME
            return null;
        }

        public static void insertKnownAbsent(NSMapTable map, IntPtr key, id value)
        {
            // FIXME
        }

    }
}
