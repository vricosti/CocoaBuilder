using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class WebView : NSView
    {
        new public static Class Class = new Class(typeof(WebView));
        new public static WebView Alloc() { return new WebView(); }

        public WebView()
        {
        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.InitWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {

            }
            return self;
        }
    }
}
