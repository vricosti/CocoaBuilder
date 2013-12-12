using AT.MIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSLog : NSObject
    {
        new public static Class Class = new Class(typeof(NSData));
        new public static NSData alloc() { return new NSData(); }


        public static void log(NSString format, params object[] args)
        {
            string msg = Tools.sprintf(format, args);
            System.Diagnostics.Debug.WriteLine(msg);

        }


    }
}
