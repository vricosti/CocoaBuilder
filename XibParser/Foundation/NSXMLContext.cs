using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLContext : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLContext));
        new public static NSXMLContext alloc() { return new NSXMLContext(); }

        public static NSString stringForObjectValue(id anObject)
        {
            return "";
        }

    }
}
