using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBCFMutableDictionary : NSMutableDictionary
    {
        new public static Class Class = new Class(typeof(IBCFMutableDictionary));
        new public static IBCFMutableDictionary alloc() { return new IBCFMutableDictionary(); }


        
    }
}
