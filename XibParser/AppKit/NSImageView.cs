using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/cocoa/reference/ApplicationKit/Classes/NSImageView_Class/Reference/Reference.html
    public class NSImageView : NSView
    {
        new public static Class Class = new Class(typeof(NSImageView));
        new public static NSImageView alloc() { return new NSImageView(); }

        public NSImageView()
        {
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.initWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {

            }
            return self;
        }
    }
}
