using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSUnarchiver : NSCoder
    {
        new public static Class Class = new Class(typeof(NSUnarchiver));
        new public static NSUnarchiver Alloc() { return new NSUnarchiver(); }



        public static id UnarchiveObjectWithData(NSData data)
        {
            return null;
        }

    }
}
