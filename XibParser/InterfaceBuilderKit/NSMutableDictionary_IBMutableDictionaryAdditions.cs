using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public static class NSMutableDictionary_IBMutableDictionaryAdditions
    {
        public static void addObjectToArrayForKey(this NSMutableDictionary self, id anObject, id key)
        {
                NSMutableArray entry;

                if((entry = (NSMutableArray)self.objectForKey(key)) == null)
                {
                    entry = (NSMutableArray)NSMutableArray.alloc().init();
                    self.setObjectForKey(entry, key);
                    
                }
                entry.addObject(anObject);
        }
    }
}
