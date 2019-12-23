using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class _NSCornerView : NSView
    {
        new public static Class Class = new Class(typeof(_NSCornerView));
        new public static _NSCornerView alloc() { return new _NSCornerView(); }

        public _NSCornerView()
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
