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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;


namespace Smartmobili.Cocoa
{
    public class IBXMLElement : NSObject
    {
        new public static Class Class = new Class(typeof(IBXMLElement));
        new public static IBXMLElement alloc() { return new IBXMLElement(); }



    }

    public class IBXMLDecoderApple : NSKeyedUnarchiver
    {
        new public static Class Class = new Class(typeof(IBXMLDecoderApple));
        new public static IBXMLDecoderApple alloc() { return new IBXMLDecoderApple(); }

        protected NSData _originalDocumentData;
        protected id _successfullyDecodedObjects;
        protected NSMutableDictionary _childMap;
        protected id _objectIDsToObjectNodes;
        protected id _objectNodesToObjectIDs;
        protected NSMutableDictionary _objectIDsToObjects;
        protected id _objectsToObjectIDs;
        protected id _uniquedValueObjects;
        protected id _classFallbacks;
        protected id _uniquedIDs;
        protected id _document;
        protected id _currentElement;
        protected id _objectElementNamesWithPotentialObjectChildren;
        protected id _objectTypeElementNames;
        protected id _specialCaseCollectionElementNames;
        protected id _elementNamesToRebuildIDsFromTable;
        protected id _specialTypesToClasses;
        protected id _delegate;
        protected id _nextGenericKey;

        public override bool AllowsKeyedCoding { get { return true; } }

        public override id initForReadingWithData(NSData data, object dummyObject = null)
        {
            NSError outErr = null;
            return initForReadingWithData(data, ref outErr);
        }
        public override id initForReadingWithData(NSData data, ref NSError outError)
        {
            return null;
        }
        
        public override float decodeFloatForKey(NSString key)
        {
            NSDictionary objects = (NSDictionary)_childMap.objectForKey(_currentElement);
            NSString floatString = objects.objectForKey(key).ToString();
            return IBXMLCoderDoubleFromString(floatString);
        }
        public virtual id objectForOID(NSString key)
        {
            return _objectIDsToObjects.objectForKey(key);
        }

        public virtual id objectForXMLElement(object element)
        {
            return null;
            //if ( )



//            r15 = rdi;
//    rax = [rdx name];
//    rax = [rax isEqualToString:@"reference"];
//    if (rax == 0x0) goto loc_15270;
//    goto loc_15252;

//loc_15270:
//    rbx = r15;
//    rdx = r14;
//    r14 = *objc_msgSend;
//    rax = (r14)(rbx, @selector(objectIDForObjectNode:), rdx);
//    r15 = rax;
//    r12 = rbx;
//    rax = (r14)(rbx, @selector(objectForOID:), r15);
//    if (rax == 0x0) {
//            rbx = r12;
//            var_32 = rbx;
//            rax = [rbx nodeForObjectID:r15];
//            r12 = rax;
//            rax = _IBXMLDecoderElementPotentiallyHasEmbeddedObjects(rbx, r12);
//            if (rax != 0x0) {
//                    r14 = *objc_msgSend;
//                    rax = [NSAutoreleasePool alloc];
//                    rax = (r14)(rax, *objc_sel_init);
//                    var_8 = rax;
//                    rdx = var_32;
//                    var_24 = rdx.nextGenericKey;
//                    r13 = rdx;
//                    rax = *_OBJC_IVAR_$_IBXMLDecoder.currentElement;
//                    rcx = *(r13 + rax);
//                    var_16 = rcx;
//                    *(r13 + rax) = r12;
//                    rax = (r14)(r13, @selector(allocateObjectForObjectElement:), r12, rcx);
//                    rbx = rax;
//                    (r14)(r13, @selector(setOID:forObject:), r15, rbx);
//                    *r13 = 0x0;
//                    rax = (r14)(r13, @selector(deserializeObject:fromXMLElement:), rbx, r13.currentElement);
//                    rax = (r14)(rax, @selector(awakeAfterUsingCoder:), r13);
//                    rbx = rax;
//                    (r14)(r13, @selector(setOID:forObject:), r15, rbx);
//                    (r14)(r13.successfullyDecodedObjects, @selector(addObject:), rbx);
//                    [var_8 release];
//                    [rbx autorelease];
//                    r13.currentElement = var_16;
//                    *r13 = var_24;
//            }
//            else {
//                    rbx = 0x0;
//                    if (r12 != 0x0) {
//                            r14 = *objc_msgSend;
//                            var_24 = r15;
//                            rax = (r14)(r12, *objc_sel_name);
//                            rax = (r14)(rax, @selector(isEqualTo:), @"boolean");
//                            if (rax != 0x0) {
//                                    r14 = *objc_msgSend;
//                                    rax = (r14)(r12, @selector(attributeForName:), @"value");
//                                    rax = (r14)(rax, @selector(stringValue));
//                                    rax = [rax isEqualToString:@"YES"];
//                                    rax = (r14)(*0x2c18f0, @selector(numberWithBool:), SIGN_EXTEND(rax));
//                                    rbx = rax;
//                            }
//                            else {
//                                    r14 = *objc_msgSend;
//                                    rax = (r14)(r12, *objc_sel_name);
//                                    rax = (r14)(rax, @selector(isEqualTo:), @"integer");
//                                    if (rax != 0x0) {
//                                            r14 = *objc_msgSend;
//                                            rax = (r14)(r12, @selector(attributeForName:), @"value");
//                                            rax = (r14)(rax, @selector(stringValue));
//                                            rax = (r14)(rax, @selector(longLongValue));
//                                            rax = (r14)(*0x2c18f0, @selector(numberWithLongLong:), rax);
//                                            rbx = rax;
//                                    }
//                                    else {
//                                            r14 = *objc_msgSend;
//                                            rax = (r14)(r12, *objc_sel_name);
//                                            rax = (r14)(rax, @selector(isEqualTo:), @"real");
//                                            if (rax != 0x0) {
//                                                    r14 = *objc_msgSend;
//                                                    rax = (r14)(r12, @selector(attributeForName:), @"value");
//                                                    rax = (r14)(rax, @selector(stringValue));
//                                                    _IBXMLCoderDoubleFromString(rax);
//                                                    rax = (r14)(*0x2c18f0, @selector(numberWithDouble:));
//                                                    rbx = rax;
//                                            }
//                                            else {
//                                                    r14 = *objc_msgSend;
//                                                    rax = (r14)(r12, *objc_sel_name);
//                                                    rax = (r14)(rax, @selector(isEqualTo:), @"string");
//                                                    rbx = 0x0;
//                                                    if (rax != 0x0) {
//                                                            r14 = *objc_msgSend;
//                                                            rax = (r14)(r12, @selector(attributeForName:), @"type");
//                                                            rax = (r14)(rax, @selector(stringValue));
//                                                            rax = [rax isEqualToString:@"base64-UTF8"];
//                                                            if (rax != 0x0) {
//                                                                    r14 = *objc_msgSend;
//                                                                    r13 = objc_msg_alloc;
//                                                                    rax = [*0x2c18b8 alloc];
//                                                                    rbx = rax;
//                                                                    rax = (r14)(r12, @selector(stringValue));
//                                                                    rax = (r14)(rbx, @selector(initWithPrettyBase64String:), rax);
//                                                                    r12 = objc_msg_autorelease;
//                                                                    rax = [rax autorelease];
//                                                                    rbx = rax;
//                                                                    rax = [*0x2c1870 alloc];
//                                                                    rax = (r14)(rax, @selector(initWithData:encoding:), rbx, 0x4);
//                                                                    rdi = rax;
//                                                                    rsi = r12;
//                                                            }
//                                                            else {
//                                                                    r14 = *objc_msgSend;
//                                                                    rax = (r14)(r12, @selector(stringValue));
//                                                                    rax = (r14)(rax, *objc_sel_copy);
//                                                                    rsi = objc_msg_autorelease;
//                                                                    rdi = rax;
//                                                            }
//                                                            rax = (*objc_msg_autorelease)();
//                                                            rax = [var_32.uniquedValueObjects member:rax];
//                                                            if (rax == 0x0) {
//                                                                    [r13 addObject:r12];
//                                                                    rbx = r12;
//                                                            }
//                                                    }
//                                            }
//                                    }
//                            }
//                            r14 = *objc_msgSend;
//                            r15 = var_32;
//                            (r14)(r15, @selector(setOID:forObject:), var_24, rbx);
//                            (r14)(r15.successfullyDecodedObjects, @selector(addObject:), rbx);
//                    }
//            }
//    }

//loc_156e4:
//    rax = rbx;
//    return rax;

//loc_15252:
//    rax = [r15 objectIDForObjectNode:r14];
//    rbx = 0x0;
//    if (rax == 0x0) goto loc_156e4;
//    goto loc_15270;
        }


        private float IBXMLCoderDoubleFromString(NSString text)
        {
            return 0;
        }
    }
}
