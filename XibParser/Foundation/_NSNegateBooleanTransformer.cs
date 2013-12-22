using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class _NSNegateBooleanTransformer : NSValueTransformer
    {
        new public static Class Class = new Class(typeof(_NSNegateBooleanTransformer));
        new public static _NSNegateBooleanTransformer alloc() { return new _NSNegateBooleanTransformer(); }


        new public static Class transformedValueClass()
        {
            return NSNumber.Class;
        }

        public virtual bool _isBooleanTransformer()
        {
            return true;
        }

        

        public override id transformedValue(id value)
        {
            return NSNumber.numberWithBool(!value.boolValue());
        }


        public override NSString description()
        {
            return new NSString("<shared NSNegateBoolean transformer>");
        }


    }
}
