using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSNull : NSObject
    {
        new public static Class Class = new Class(typeof(NSNull));
        new public static NSNull alloc() { return new NSNull(); }

        private static readonly NSNull _instance = new NSNull();

         private NSNull() { }

         public static NSNull getNull()
         {
             return _instance;
         }
    }
}
