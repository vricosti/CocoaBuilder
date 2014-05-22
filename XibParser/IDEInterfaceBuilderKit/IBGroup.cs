using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBGroup : NSObject
    {
        new public static Class Class = new Class(typeof(IBGroup));
        new public static IBGroup alloc() { return new IBGroup(); }

        protected DVTMutableOrderedSet _objectRecords;
        protected DVTMutableOrderedSet _objects;
        protected IBMutableBijectiveDictionary _identifierToRecordBijectiveDictionary;
        protected id _delegate;
    }
}
