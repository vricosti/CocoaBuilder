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
            //string msg = string.Format("{2014-01-13 17:37:59.751 TestNSXMLParser[790:507] }");
            string userMsg = Tools.sprintf(format, args);
            System.Diagnostics.Debug.WriteLine(userMsg);

        }


    }
}
