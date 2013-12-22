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


    }
}
