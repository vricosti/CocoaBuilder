using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBMutableBijectiveDictionary : NSMutableDictionary
    {
        new public static Class Class = new Class(typeof(IBMutableBijectiveDictionary));
        new public static IBMutableBijectiveDictionary alloc() { return new IBMutableBijectiveDictionary(); }

        protected DVTMapTable _keyToObjectMap;
        protected DVTMapTable _objectToKeyMap;
    }
}
