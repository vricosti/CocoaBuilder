using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{ 
    public class NSMapTable : NSObject
    {
        new public static Class Class = new Class(typeof(NSMapTable));
        new public static NSMapTable alloc() { return new NSMapTable(); }

        protected Dictionary<object, id> _dict = new Dictionary<object,id>();

        public static id Get(NSMapTable map, object key)
        {
            id val = null;
            map._dict.TryGetValue(key, out val);
            return val;
        }


        public static void Insert(NSMapTable map, IntPtr key, id value)
        {
            map.setObjectForKey(value, key);
        }

        public static void InsertKnownAbsent(NSMapTable map, IntPtr key, id value)
        {
            if (map._dict.ContainsKey(key) != false)
                NSException.raise(" NSInvalidArgumentException", "");

            map.setObjectForKey(value, key);
        }

        public virtual id objectForKey(object aKey)
        {
            return _dict[aKey];
        }

        public virtual void setObjectForKey(id anObject, object aKey)
        {
            _dict[aKey] = anObject;
        }

    }
}
