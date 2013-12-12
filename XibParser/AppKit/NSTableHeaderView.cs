using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSTableHeaderView : NSView
    {
        new public static Class Class = new Class(typeof(NSTableHeaderView));
        new public static NSTableHeaderView alloc() { return new NSTableHeaderView(); }

        public NSTableHeaderView()
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
