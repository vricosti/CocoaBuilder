using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSTableView : NSControl
    {
        new public static Class Class = new Class(typeof(NSTableView));
        new public static NSTableView alloc() { return new NSTableView(); }

        public NSTableView()
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
