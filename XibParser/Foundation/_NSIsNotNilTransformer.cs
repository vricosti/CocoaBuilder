using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class _NSIsNotNilTransformer : NSValueTransformer
    {
        new public static Class Class = new Class(typeof(_NSIsNotNilTransformer));
        new public static _NSIsNotNilTransformer alloc() { return new _NSIsNotNilTransformer(); }


    }
}
