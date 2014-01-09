using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSUserDefaults : NSObject
    {
        new public static Class Class = new Class(typeof(NSUserDefaults));
        new public static NSUserDefaults alloc() { return new NSUserDefaults(); }

        private static volatile NSUserDefaults _instance;
        private static object _syncRoot = new Object();

        private NSUserDefaults() { }

        public static NSUserDefaults standardUserDefaults()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new NSUserDefaults();
                }
            }

            return _instance;
        }

        public virtual bool boolForKey(NSString key)
        {
            return false;
        }

        public virtual bool hasPreferenceForKey(NSString key)
        {
            return false;
        }

    }
}
