using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class _NSIsNilTransformer : NSValueTransformer
    {
        new public static Class Class = new Class(typeof(_NSIsNilTransformer));
        new public static _NSIsNilTransformer alloc() { return new _NSIsNilTransformer(); }


        public static bool supportsReverseTransformation()
        {
            return false;
        }

        public override id transformedValue(id value)
        {
            return NSNumber.numberWithBool(value == null);
        }

        public override NSString description()
        {
            return new NSString("<shared NSIsNil transformer>");
        }
    }
}
