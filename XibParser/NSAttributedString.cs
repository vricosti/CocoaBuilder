using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/Foundation/Classes/NSAttributedString_Class/Reference/Reference.html
    //https://github.com/gnustep/gnustep-base/blob/master/Headers/Foundation/NSAttributedString.h
    public class NSAttributedString : NSObject
    {
        new public static Class Class = new Class(typeof(NSAttributedString));

        protected NSString _string;


        public NSString String
        {
            get { return _string; }
        }


        new public static NSAttributedString Alloc()
        {
            return new NSAttributedString();
        }


        public NSAttributedString()
        {

        }

        public virtual id InitWithRTF(NSData rtfData, ref NSDictionary docAttributes)
        {
            return this;
        }

        public virtual id InitWithRTFD(NSData rtfData, ref NSDictionary docAttributes)
        {
            return this;
        }
    }
}
