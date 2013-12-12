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


    public class IBXMLDecoder_Apple : NSKeyedUnarchiver
    {
        new public static Class Class = new Class(typeof(IBXMLDecoder_Apple));
        new public static IBXMLDecoder_Apple alloc() { return new IBXMLDecoder_Apple(); }

        protected NSXMLElement _currentElement; //0x04
        protected NSMutableArray _successfullyDecodedObjects; //0x08
        protected IBCFMutableDictionary _objectNodesToObjectIDs; //0x0C
        protected NSMutableDictionary _objectsToObjectIDs; //0x10
        protected NSMutableDictionary _objectIDsToObjectNodes; //0x14
        protected NSMutableDictionary _objectIDsToObjects; //0x18
        protected IBCFMutableDictionary _childMap; //0x1C
        protected NSMutableDictionary _classFallbacks; //0x20
        protected NSMutableSet _uniquedIDs; //0x24
        protected NSMutableSet _uniquedValueObjects; //0x28
        protected NSXMLDocument _document;  //0x2C
        protected IBSelfCompressingDataWrapper _originalDocumentData; //0x30
        protected id _nextGenericKey; //0x34
        protected id _delegate; //0x38
        protected NSSet _objectElementNamesWithPotentialObjectChildren; //0x3C
        protected NSSet _objectTypeElementNames; //0x40
        protected NSSet _specialCaseCollectionElementNames; //0x44
        protected NSArray _elementNamesToRebuildIDsFromTable; //0x48
        protected NSDictionary _specialTypesToClasses; //0x4C
       
        

       

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

        private uint IBXMLDecoderBuildIDIndex(IBXMLDecoder_Apple decoder, NSXMLElement element)
        {
            if (decoder._objectTypeElementNames == null)
            {
                decoder._objectTypeElementNames = (NSSet)NSSet.alloc().InitWithObjects(
                    (NSString)"object", (NSString)"dictionary", (NSString)"array", (NSString)"set",
                    (NSString)"integer", (NSString)"real", (NSString)"boolean", (NSString)"string", null);
            }

            return 0;
        }

//function _IBXMLDecoderBuildIDIndex {
//    edi = edx;
//    var_84 = ecx;
//    eax = *stack_chk_guard;
//    var_60 = eax;
//    var_200 = *eax;
//    if (*(ecx + 0x40) == 0x0) {
//            eax = [NSSet alloc];
//            eax = [eax initWithObjects:@"object", @"dictionary", @"array", @"set", @"integer", @"real", @"boolean", @"string", 0x0];
//    }
//    var_68 = eax;
//    *(var_84 + 0x40) = eax;
//    eax = [edi elements];
//    var_76 = eax;
//    eax = [eax countByEnumeratingWithState:&var_160 objects:&var_96 count:0x10];
//    var_72 = eax;
//    if (eax != 0x0) {
//            var_64 = *var_168;
//            edi = @"id";
//            do {
//                    var_88 = 0x0;
//                    do {
//                            if (*var_168 != var_64) {
//                                    objc_enumerationMutation(var_76);
//                            }
//                            eax = [*(var_164 + var_88 * 0x4) name];
//                            eax = [var_68 containsObject:eax];
//                            if (eax == 0x0) {
//                                    eax = [esi name];
//                                    eax = [eax isEqualToString:@"reference"];
//                                    if (eax != 0x0) {
//                                            eax = [esi attributeForName:@"ref"];
//                                            if (eax != 0x0) {
//                                                    var_92 = esi;
//                                                    eax = [eax stringValue];
//                                                    esi = eax;
//                                                    eax = [arg_8 member:esi];
//                                                    if (eax == 0x0) {
//                                                            [edi addObject:esi];
//                                                            eax = esi;
//                                                    }
//                                                    [arg_4 setObject:eax forKey:var_92];
//                                                    edi = @"id";
//                                            }
//                                    }
//                            }
//                            else {
//                                    eax = [esi attributeForName:edi];
//                                    if (eax == 0x0) {
//                                            var_92 = esi;
//                                            eax = [NSAutoreleasePool alloc];
//                                            eax = [eax init];
//                                            edi = arg_8;
//                                            var_80 = eax;
//                                            do {
//                                                    do {
//                                                            esi = arg_C;
//                                                            ecx = *esi;
//                                                            *esi = ecx + 0x1;
//                                                            eax = [NSString stringWithFormat:@"%ld", ecx];
//                                                            eax = [edi containsObject:eax];
//                                                    } while (eax != 0x0);
//                                                    eax = [esi retain];
//                                                    esi = eax;
//                                                    [edi addObject:esi];
//                                            } while (esi == 0x0);
//                                            [var_80 release];
//                                            eax = [esi autorelease];
//                                            edi = eax;
//                                    }
//                                    else {
//                                            var_92 = esi;
//                                            eax = [esi attributeForName:edi];
//                                            eax = [eax stringValue];
//                                            esi = eax;
//                                            eax = [arg_8 member:esi];
//                                            edi = eax;
//                                            if (edi == 0x0) {
//                                                    [arg_8 addObject:esi];
//                                                    edi = esi;
//                                            }
//                                    }
//                                    eax = [arg_0 objectForKey:edi];
//                                    if (eax != 0x0) {
//                                            eax = [NSString stringWithFormat:@"No two objects should have the same ID.", 0x0];
//                                            eax = [eax length];
//                                            if (eax != 0x0) {
//                                                    NSLog(@"%@", esi);
//                                                    eax = _IBPrettyBacktrace(0x0);
//                                                    NSLog(@"Backtrace:\\n%@\\n", eax);
//                                                    eax = [NSException exceptionWithName:@"IBExpect Failure" reason:esi userInfo:0x0];
//                                                    [eax raise];
//                                            }
//                                    }
//                                    esi = var_92;
//                                    [arg_4 setObject:edi forKey:esi];
//                                    [arg_0 setObject:esi forKey:edi];
//                                    eax = _IBXMLDecoderElementPotentiallyHasEmbeddedObjects();
//                                    edi = @"id";
//                                    if (eax != 0x0) {
//                                            _IBXMLDecoderBuildIDIndex(arg_0, arg_4, var_84, arg_C);
//                                    }
//                            }
//                            eax = var_88 + 0x1;
//                            var_88 = eax;
//                    } while (eax < var_72);
//                    eax = [var_76 countByEnumeratingWithState:&var_160 objects:&var_96 count:0x10];
//                    var_72 = eax;
//            } while (eax != 0x0);
//    }
//    if (*var_60 == var_200) {
//            return eax;
//    }
//    else {
//            eax = __stack_chk_fail();
//    }
//    return eax;
//}




        public override float decodeFloatForKey(NSString key)
        {
            NSDictionary objects = (NSDictionary)_childMap.objectForKey(_currentElement);
            NSString floatString = objects.objectForKey(key).ToString();
            return IBXMLCoderDoubleFromString(floatString);
        }
        public virtual id ObjectForOID(NSString key)
        {
            return _objectIDsToObjects.objectForKey(key);
        }

        public virtual id ObjectForXMLElement(object element)
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
