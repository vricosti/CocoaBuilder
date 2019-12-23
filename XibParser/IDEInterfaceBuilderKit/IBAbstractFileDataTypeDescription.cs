using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBAbstractFileDataTypeDescription : NSObject
    {
        new public static Class Class = new Class(typeof(IBAbstractFileDataTypeDescription));
        new public static IBAbstractFileDataTypeDescription alloc() { return new IBAbstractFileDataTypeDescription(); }




    }
}
