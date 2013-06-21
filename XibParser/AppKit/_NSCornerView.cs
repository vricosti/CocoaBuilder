using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class _NSCornerView : NSView
    {
        new public static Class Class = new Class(typeof(_NSCornerView));
        new public static _NSCornerView Alloc() { return new _NSCornerView(); }

        public _NSCornerView()
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
