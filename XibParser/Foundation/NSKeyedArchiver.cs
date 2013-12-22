using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSKeyedArchiver : NSCoder
    {
        new public static Class Class = new Class(typeof(NSKeyedArchiver));
        new public static NSKeyedArchiver alloc() { return new NSKeyedArchiver(); }


        public static NSData archivedDataWithRootObject(id rootObject)
        {
            return null;
        }



    }
}
