using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class DVTMutableOrderedSet : NSMutableSet
    {
        new public static Class Class = new Class(typeof(DVTMutableOrderedSet));
        new public static DVTMutableOrderedSet alloc() { return new DVTMutableOrderedSet(); }

        protected NSMutableOrderedSet _orderedSet;
    }
}
