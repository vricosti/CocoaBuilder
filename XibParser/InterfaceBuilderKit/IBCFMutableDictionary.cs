using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBCFMutableDictionary : NSObject
    {
        new public static Class Class = new Class(typeof(IBCFMutableDictionary));
        new public static IBCFMutableDictionary Alloc() { return new IBCFMutableDictionary(); }


        public virtual id ObjectForKey(id aKey)
        {
            id obj = null;

           

            return obj;
        }
    }
}
