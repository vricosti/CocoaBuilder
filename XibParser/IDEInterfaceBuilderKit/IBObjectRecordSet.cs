using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBObjectRecordSet : NSObject
    {
        new public static Class Class = new Class(typeof(IBObjectRecordSet));
        new public static IBObjectRecordSet alloc() { return new IBObjectRecordSet(); }

        IBMutableOrderedSet _records;
        NSArray _objects;
    }
}
