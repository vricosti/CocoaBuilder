using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public abstract class NSStream : NSObject
    {
        new public static Class Class = new Class(typeof(NSStream));

        public abstract void Open();

        public abstract void Close();

    }
}
