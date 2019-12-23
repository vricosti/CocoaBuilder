using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSOutlineView : NSTableView
    {
        new public static Class Class = new Class(typeof(NSOutlineView));
        new public static NSOutlineView alloc() { return new NSOutlineView(); }

        public NSOutlineView()
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
