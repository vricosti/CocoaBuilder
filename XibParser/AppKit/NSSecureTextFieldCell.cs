using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSSecureTextField_Class/Reference/Reference.html
    public class NSSecureTextFieldCell : NSTextFieldCell
    {
        new public static Class Class = new Class(typeof(NSSecureTextFieldCell));
        new public static NSSecureTextFieldCell alloc() { return new NSSecureTextFieldCell(); }

        public NSSecureTextFieldCell()
        {
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.initWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {

            }
            return self;
        }
    }
}
