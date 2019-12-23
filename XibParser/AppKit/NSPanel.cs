using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSPanel : NSWindow
    {
        new public static Class Class = new Class(typeof(NSPanel));

        static NSPanel()
        { 

        }

        static void initialize()
        {
            //Version = 1;
        }


        public override id init()
        {
            int style =  (int)(NSWindowStyleMasks.NSTitledWindowMask | NSWindowStyleMasks.NSClosableWindowMask);
            return InitWithContentRect(NSRect.Zero, style, NSBackingStoreType.NSBackingStoreBuffered, false);
        }

        public virtual id InitWithContentRect(NSRect contentRect, int aStyle, NSBackingStoreType bufferingType, bool flag)
        {
            id self = this;



            return self;
        }

    }
}
