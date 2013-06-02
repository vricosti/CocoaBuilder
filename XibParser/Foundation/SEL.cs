using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class SEL
    {
        private NSString _selectorName;

        public SEL()
        {
        }

        public SEL(NSString aSelectorName)
        {
            _selectorName = aSelectorName;
        }

        public static NSString StringFromSelector(SEL sel)
        {
            return sel._selectorName;
        }

        public static SEL SelectorFromString(NSString aSelectorName)
        {
            return new SEL(aSelectorName);
        }
    }
}
