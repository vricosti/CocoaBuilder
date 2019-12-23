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

        private static bool _TidyLoaded;

        public static bool isLoaded()
        {
            return _TidyLoaded;
        }

        public static void loadTidy()
        {
            _TidyLoaded = true;
        }



    }
}
