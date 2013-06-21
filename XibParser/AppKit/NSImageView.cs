﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/cocoa/reference/ApplicationKit/Classes/NSImageView_Class/Reference/Reference.html
    public class NSImageView : NSView
    {
        new public static Class Class = new Class(typeof(NSImageView));
        new public static NSImageView Alloc() { return new NSImageView(); }

        public NSImageView()
        {
        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.InitWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {

            }
            return self;
        }
    }
}
