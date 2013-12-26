using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLTreeReader : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLTreeReader));
        new public static NSXMLTreeReader alloc() { return new NSXMLTreeReader(); }


        public virtual id initWithData(NSData data, Class documentClass, uint mask, ref NSError error)
        {

            return null;
        }

        public virtual id parse()
        {

        }



    }
}
