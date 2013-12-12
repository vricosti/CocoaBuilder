using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public static class NSXMLElement_IBXMLElementAddition
    {
        public static NSMutableArray elements(this NSXMLElement element)
        {
            return (NSMutableArray)NSMutableArray.alloc().init();
        }



        public static void addAttributeWithName(this NSXMLElement element, NSString stringValue)
        {
           
        }

        
    }
}
