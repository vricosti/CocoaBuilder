using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public static class NSXMLElement_IBXMLElementAddition
    {
        public static NSArray elements(this NSXMLElement xmlElement)
        {
            NSArray elmts;

            var children = xmlElement.children();
            if (children != null && children.count() > 0)
            {
                elmts = NSMutableArray.array();
                foreach (NSXMLNode node in children)
                {
                    if (node.kind() == NSXMLNodeKind.NSXMLElementKind)
                    {
                        elmts.addObject(node);
                    }
                }
            }
            else
            {
                elmts = NSArray.emptyArray();
            }
            return elmts;
            
        }



        public static void addAttributeWithName(this NSXMLElement element, NSString stringValue)
        {
           
        }

        
    }
}
