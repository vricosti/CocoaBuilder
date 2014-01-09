using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSUndoManager
    {
        new public static Class Class = new Class(typeof(NSUndoManager));
        new public static NSUndoManager alloc() { return new NSUndoManager(); }

        public virtual void removeAllActions()
        {

        }
    }
}
