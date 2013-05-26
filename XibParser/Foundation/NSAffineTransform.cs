using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/Foundation/Classes/NSAffineTransform_Class/Reference/Reference.html
    public class NSAffineTransform : NSObject
    {
        new public static Class Class = new Class(typeof(NSAffineTransform));
        new public static NSAffineTransform Alloc() { return new NSAffineTransform(); }


    }
}
