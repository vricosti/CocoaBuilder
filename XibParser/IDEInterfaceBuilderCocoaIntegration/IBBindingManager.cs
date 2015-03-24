using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBBindingManager : NSObject
    {
        new public static Class Class = new Class(typeof(IBBindingManager));
        new public static IBBindingManager alloc() { return new IBBindingManager(); }

        //NSUserDefaultsController _sharedUserDefaultsControllerProxy;
        IBCocoaDocument _document;
    }
}
