using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public static class NS
    {
        public const uint NSNotFound = (int)Int32.MaxValue;


        public static NSString Encode<T>()
        {
            return "";
        }

        public static double MinX(NSRect aRect)
        {
            return aRect.MinX;
        }

        public static double MinY(NSRect aRect)
        {
            return aRect.MinY;
        }
    }
}
