using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSArchiver : NSCoder
    {
        new public static Class Class = new Class(typeof(NSArchiver));
        new public static NSArchiver alloc() { return new NSArchiver(); }


        public static NSData archivedDataWithRootObject(id rootObject)
        {
            return null;
        }

    }
}
