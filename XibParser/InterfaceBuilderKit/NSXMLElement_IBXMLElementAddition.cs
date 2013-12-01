using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public static class NSXMLElement_IBXMLElementAddition
    {
        public static NSMutableArray Elements(this NSXMLElement element)
        {
            return (NSMutableArray)NSMutableArray.Alloc().Init();
        }



        public static void AddAttributeWithName(this NSXMLElement element, NSString stringValue)
        {
           
        }

        
    }
}
