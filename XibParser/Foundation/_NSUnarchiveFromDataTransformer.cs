using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class _NSUnarchiveFromDataTransformer : NSValueTransformer
    {
        new public static Class Class = new Class(typeof(_NSUnarchiveFromDataTransformer));
        new public static _NSUnarchiveFromDataTransformer alloc() { return new _NSUnarchiveFromDataTransformer(); }

       
        public override id transformedValue(id value)
        {
            id data = null;

           if (value != null)
           {
               data = NSUnarchiver.unarchiveObjectWithData((NSData)value);
           }

            return data;
        }

        public virtual id reverseTransformedValue(id value)
        {
           return NSArchiver.archivedDataWithRootObject(value);
        }


        public override NSString description()
        {
            return new NSString("<shared NSUnarchiveFromData transformer>");
        }




        
    }
}
