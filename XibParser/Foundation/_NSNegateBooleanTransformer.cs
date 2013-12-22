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


    }
}
