using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class _NSKeyedUnarchiveFromDataTransformer : NSValueTransformer
    {
        new public static Class Class = new Class(typeof(_NSKeyedUnarchiveFromDataTransformer));
        new public static _NSKeyedUnarchiveFromDataTransformer alloc() { return new _NSKeyedUnarchiveFromDataTransformer(); }


        public override id transformedValue(id value)
        {
            id data = null;

            if (value != null)
            {
                data = NSKeyedUnarchiver.unarchiveObjectWithData((NSData)value);
            }

            return data;
        }

        public virtual id reverseTransformedValue(id value)
        {
            return NSKeyedArchiver.archivedDataWithRootObject(value);
        }


        public override NSString description()
        {
            return new NSString("<shared NSKeyedUnarchiveFromData transformer>");
        }
    }
}
