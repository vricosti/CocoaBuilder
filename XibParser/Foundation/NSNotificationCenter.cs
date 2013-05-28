using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSNotificationCenter : NSObject
    {
        new public static Class Class = new Class(typeof(NSNotificationCenter));
        new public static NSNotificationCenter Alloc() { return new NSNotificationCenter(); }

        private static NSNotificationCenter default_center = null;


        static NSNotificationCenter() { Initialize(); }
        static void Initialize()
        {
            default_center = (NSNotificationCenter)NSNotificationCenter.Alloc().Init();
        }


        public static NSNotificationCenter DefaultCenter
        {
            get { return default_center; }
        }
    }
}
