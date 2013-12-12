using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSNotificationCenter : NSObject
    {
        new public static Class Class = new Class(typeof(NSNotificationCenter));
        new public static NSNotificationCenter alloc() { return new NSNotificationCenter(); }

        private static NSNotificationCenter default_center = null;


        static NSNotificationCenter() { initialize(); }
        static void initialize()
        {
            default_center = (NSNotificationCenter)NSNotificationCenter.alloc().init();
        }


        public static NSNotificationCenter DefaultCenter
        {
            get { return default_center; }
        }
    }
}
