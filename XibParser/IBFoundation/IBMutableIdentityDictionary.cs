using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBMutableIdentityDictionary : NSMutableDictionary
    {
        new public static Class Class = new Class(typeof(IBMutableIdentityDictionary));
        new public static IBMutableIdentityDictionary alloc() { return new IBMutableIdentityDictionary(); }

        new public static IBMutableIdentityDictionary dictionary()
        {
            return (IBMutableIdentityDictionary)NSMutableDictionary.alloc().init();
        }

        //NSMapTable* table;
        //NSSortDescriptor* codingSortDescriptor;
    }
}
