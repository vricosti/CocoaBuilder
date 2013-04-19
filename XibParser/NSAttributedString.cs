using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/Foundation/Classes/NSAttributedString_Class/Reference/Reference.html
    public class NSAttributedString : NSObject
    {
        protected NSString _string;


        public NSString String
        {
            get { return _string; }
        }


        public NSAttributedString()
        {

        }
    }
}
