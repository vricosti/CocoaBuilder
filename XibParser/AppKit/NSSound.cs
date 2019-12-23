using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSSound_Class/Reference/Reference.html
    public class NSSound : NSObject
    {
        new public static Class Class = new Class(typeof(NSSound));
        new public static NSSound alloc() { return new NSSound(); }


        public NSSound()
        {

        }
    }
}
