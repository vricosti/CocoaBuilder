using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLTidy : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLTidy));
        new public static NSXMLTidy alloc() { return new NSXMLTidy(); }

        public static bool isLoaded()
        {
            return false;
        }

        public static void loadTidy()
        {

        }



    }
}
