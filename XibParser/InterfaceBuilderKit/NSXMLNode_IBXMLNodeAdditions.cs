using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public static class NSXMLNode_IBXMLNodeAdditions
    {
        public static NSMutableArray Elements(this NSXMLElement element)
        {
            return (NSMutableArray)NSMutableArray.alloc().init();
        }

//        __NSXMLNode_IBXMLNodeAdditions__elementWithName_attributes__             __text 00000000001002D8 0000003C R . . . B . .
//__NSXMLNode_IBXMLNodeAdditions__elementWithName_namespaces_attributes__  __text 0000000000100271 00000067 R . . . B . .
//__NSXMLNode_IBXMLNodeAdditions__elementWithName_stringValue_attributes__ __text 0000000000062E52 0000005F R . . . B . .




    }
}
