/*
* XibParser.
* Copyright (C) 2013 Smartmobili SARL
* 
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License as published by the Free Software Foundation; either
* version 2 of the License, or (at your option) any later version.
* 
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
* 
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, write to the
* Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
* Boston, MA  02110-1301, USA. 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Smartmobili.Cocoa
{
    public class NSXMLElement : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLElement));
        new public static NSXMLElement Alloc() { return new NSXMLElement(); }


        public virtual id InitWithName(NSString name)
        {
            return this.InitWithNameURI(name, null);
        }



        public virtual id InitWithNameURI(NSString name, NSString uri)
        {
            id self = this;

            return self;
        }

       public virtual id InitWithNameStringValue(NSString name, NSString stringValue)
       {
           id self = this;

           return self;
       }


//        – initWithName:
//– initWithName:stringValue:
//– initWithXMLString:error:
//– initWithName:URI:

         //NSXMLElement *root = [[NSXMLElement alloc] initWithName:@"Request"];





//        function -[NSXMLElement(IBXMLElementAddition) elements] {
//    eax = *stack_chk_guard;
//    var_28 = eax;
//    var_152 = *eax;
//    eax = [arg_0 children];
//    var_40 = eax;
//    eax = [eax countByEnumeratingWithState:&var_112 objects:&var_48 count:0x10];
//    var_36 = eax;
//    if (eax == 0x0) goto loc_160dc;
//    goto loc_16012;

//loc_160dc:
//    eax = [NSArray emptyArray];
//    ecx = eax;

//loc_160f6:
//    if (*var_28 == var_152) {
//            return ecx;
//    }
//    else {
//            eax = __stack_chk_fail();
//    }
//    return eax;

//loc_16012:
//    var_32 = *var_120;
//    var_44 = 0x0;
//    do {
//            ebx = 0x0;
//            do {
//                    if (*var_120 != var_32) {
//                            objc_enumerationMutation(var_40);
//                    }
//                    eax = [*(var_116 + ebx * 0x4) kind];
//                    if (eax == 0x2) {
//                            if (var_44 == 0x0) {
//                                    eax = [NSMutableArray array];
//                            }
//                            var_44 = eax;
//                            [eax addObject:edi];
//                    }
//                    ebx = ebx + 0x1;
//            } while (ebx < var_36);
//            eax = [var_40 countByEnumeratingWithState:&var_112 objects:&var_48 count:0x10];
//            var_36 = eax;
//    } while (eax != 0x0);
//    if (var_44 != 0x0) goto loc_160f6;
//    goto loc_160dc;
//}

        public virtual NSMutableArray Elements()
        {
            return (NSMutableArray)NSMutableArray.Alloc().Init();
        }

       
    }
}
