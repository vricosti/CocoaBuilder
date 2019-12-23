using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSException : NSObject
    {
        new public static Class Class = new Class(typeof(NSException));

        public static void raise(NSString name, NSString format, params NSString[] args)
        {
            string msg = string.Format("{0}: {1}", name, format);
            throw new Exception(msg);
        }
    }
}
