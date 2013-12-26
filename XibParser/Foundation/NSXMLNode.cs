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
   
    public enum NSXMLNodeOptions : uint
    {
        NSXMLNodeOptionsNone = 0,
        NSXMLNodeIsCDATA = 1 << 0,
        NSXMLNodeExpandEmptyElement = 1 << 1, // <a></a>
        NSXMLNodeCompactEmptyElement = 1 << 2, // <a/>
        NSXMLNodeUseSingleQuotes = 1 << 3,
        NSXMLNodeUseDoubleQuotes = 1 << 4,
        NSXMLDocumentTidyHTML = 1 << 9,
        NSXMLDocumentTidyXML = 1 << 10,
        NSXMLDocumentValidate = 1 << 13,
        NSXMLNodeLoadExternalEntitiesAlways = 1 << 14,
        NSXMLNodeLoadExternalEntitiesSameOriginOnly = 1 << 15,
        NSXMLNodeLoadExternalEntitiesNever = 1 << 19,
        NSXMLDocumentXInclude = 1 << 16,
        NSXMLNodePrettyPrint = 1 << 17,
        NSXMLDocumentIncludeContentTypeDeclaration = 1 << 18,
        NSXMLNodePreserveNamespaceOrder = 1 << 20,
        NSXMLNodePreserveAttributeOrder = 1 << 21,
        NSXMLNodePreserveEntities = 1 << 22,
        NSXMLNodePreservePrefixes = 1 << 23,
        NSXMLNodePreserveCDATA = 1 << 24,
        NSXMLNodePreserveWhitespace = 1 << 25,
        NSXMLNodePreserveDTD = 1 << 26,
        NSXMLNodePreserveCharacterReferences = 1 << 27,
        NSXMLNodePreserveEmptyElements =
        (NSXMLNodeExpandEmptyElement | NSXMLNodeCompactEmptyElement),
        NSXMLNodePreserveQuotes =
        (NSXMLNodeUseSingleQuotes | NSXMLNodeUseDoubleQuotes),
        NSXMLNodePreserveAll = (
        NSXMLNodePreserveNamespaceOrder |
        NSXMLNodePreserveAttributeOrder |
        NSXMLNodePreserveEntities |
        NSXMLNodePreservePrefixes |
        NSXMLNodePreserveCDATA |
        NSXMLNodePreserveEmptyElements |
        NSXMLNodePreserveQuotes |
        NSXMLNodePreserveWhitespace |
        NSXMLNodePreserveDTD |
        NSXMLNodePreserveCharacterReferences |
        0xFFF00000) // high 12 bits
    }

    public enum NSXMLNodeKind
    {
        NSXMLInvalidKind = 0,
        NSXMLDocumentKind,
        NSXMLElementKind,
        NSXMLAttributeKind,
        NSXMLNamespaceKind,
        NSXMLProcessingInstructionKind,
        NSXMLCommentKind,
        NSXMLTextKind,
        NSXMLDTDKind,
        NSXMLEntityDeclarationKind,
        NSXMLAttributeDeclarationKind,
        NSXMLElementDeclarationKind,
        NSXMLNotationDeclarationKind
    }

    public class NSXMLNode : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLNode));
        new public static NSXMLNode alloc() { return new NSXMLNode(); }

        protected uint _index;

        protected NSXMLNodeKind _kind;

        protected id _objectValue;

        protected NSXMLNode _parent;

        protected id _private;



        public static NSXMLNode predefinedNamespaceForPrefix(NSString prefix)
        {
            return NSXMLContext.defaultNamespaceForPrefix(prefix);
        }
    
        public static NSString prefixForName(NSString qName) 
        {
            NSString prefix = "";
            
            if (qName != null && qName.length() != 0)
            {
                NSRange range = qName.rangeOfString(":", NSStringCompareOptions.NSLiteralSearch, new NSRange(1, qName.length()));
                if (range.Location != NS.NotFound)
                {
                    prefix = qName.substringToIndex(range.Location);
                }
            }
            
            return prefix;
        }


        public static NSString localNameForName(NSString qName)
        {
            NSString localName = qName;

            if (qName != null && qName.length() != 0)
            {
                NSRange range = qName.rangeOfString(":", NSStringCompareOptions.NSLiteralSearch, new NSRange(1, qName.length()));
                if (range.Location != NS.NotFound)
                {
                    localName = qName.substringFromIndex(range.Location + range.Length);
                }
            }

            return localName;
        }


        public virtual void _setKind(NSXMLNodeKind kind)
        {
            _kind = kind;
        }

        public virtual NSXMLNodeKind kind()
        {
            return _kind;
        }


        public virtual void setName(NSString name)
        { }

        public virtual NSString name()
        {
            return null;
        }

        public virtual NSString localName()
        {
            return null;
        }


        public virtual NSString URI()
        {
            return null;
        }
        public virtual void setURI(NSString uri)
        { }

        public virtual void _setIndex(uint index)
         {
            _index = index;
         }

        public virtual uint index()
        {
            return _index;
        }

         public virtual void _setParent(NSXMLNode parent)
         {
             _parent = parent;
         }

      
        public virtual NSXMLNode parent()
        {
            return _parent;
        }

        public virtual NSArray children()
        {
            return null;
        }

        public virtual uint childCount()
        {
            return 0;
        }

        public virtual NSXMLNode childAtIndex(uint index)
        {
            return null;
        }

        public virtual void setObjectValue(id objValue)
        {
            if (this._objectValue != objValue)
            {
                this._objectValue = objValue;
            }

        }

        public virtual NSString stringValue()
        {
            if (this._objectValue != null)
            {
                return NSXMLContext.stringForObjectValue(this._objectValue);
            }
            else
            {
                return @"";
            }
        }


        public virtual void setStringValue(NSString strValue)
        {
            this.setStringValue(strValue, false);
        }

    

        public virtual void setStringValue(NSString strValue, bool resolvingEntities)
        {
           if (resolvingEntities == true)
           {
               NSXMLFidelityNode.setObjectValuePreservingEntitiesForNode(this, strValue);
           }
           else
           {
               this.setObjectValue(strValue);
           }
        }

        public virtual NSString XMLString()
        {
            return this.XMLStringWithOptions(0);
        }

        public virtual NSString XMLStringWithOptions(uint options)
        {
            return this._XMLStringWithOptions_appendingToString(options, NSMutableString.String());
        }




        public virtual NSString _XMLStringWithOptions_appendingToString(uint options, NSString str)
        {
            if (this.kind() != NSXMLNodeKind.NSXMLCommentKind)
            {

            }
            else
            { 

            }
            return null;
        }

        public virtual NSData XMLData()
        {
            return this.XMLString().dataUsingEncoding(NSStringEncoding.NSUTF8StringEncoding);
        }

        public virtual NSString XPath()
        {
            NSString xpath = null;

            if (_parent == null)
            {
                xpath = "";
            }
            else
            {
                xpath = NSString.stringWithFormat(@"%@/node()[%ld]", _parent.XPath(), this.index());
            }
            return xpath;
        }


        public override id init()
        {
            id self = this;

            this._setKind(0);
            this._index = 0;

            return self;
        }


        public virtual id initWithKind(NSXMLNodeKind kind)
        {
            return this.initWithKind(kind, 0);
        }
       
        public virtual id initWithKind(NSXMLNodeKind kind, uint options)
        {
            id self = this.init();

            /*
             * Check whether we are already initializing an instance of the given
             * subclass. If we are not, release ourselves and allocate a subclass
             * instance instead.
             */
            switch(kind)
            {
                case NSXMLNodeKind.NSXMLDocumentKind:
                    if (self.isKindOfClass(NSXMLDocument.Class) == false)
                    {
                        self = NSXMLDocument.alloc().init();
                    }
                    break;
                    
                case NSXMLNodeKind.NSXMLElementKind:
                    if ((options & 0x800004) == 0)
                    {
                        if (self.isKindOfClass(NSXMLElement.Class) == false)
                        {
                            self = NSXMLElement.alloc().init();
                        }
                    }
                    else
                    {
                        if (self.isKindOfClass(NSXMLFidelityElement.Class) == false)
                        {
                            self = NSXMLFidelityElement.alloc().init();
                            ((NSXMLFidelityElement)self).setFidelity(options);
                        }
                    }
                    break;

                case NSXMLNodeKind.NSXMLAttributeKind:
                    if ((options & 0x8c00008) == 0)
                    {
                        if (self.isKindOfClass(NSXMLNamedNode.Class) == false)
                        {
                            self = NSXMLNamedNode.alloc().initWithKind(kind);
                        }
                    }
                    else
                    {
                        if (self.isKindOfClass(NSXMLNamedFidelityNode.Class) == false)
                        {
                            self = NSXMLNamedFidelityNode.alloc().initWithKind(kind);
                            ((NSXMLNamedFidelityNode)self).setFidelity(options);
                        }
                   } 
                    break;


                case NSXMLNodeKind.NSXMLNamespaceKind:
                case NSXMLNodeKind.NSXMLProcessingInstructionKind:
                    if ((options & 0x8 /*NSXMLNodeUseSingleQuotes*/) == 0)
                    {
                        if (self.isKindOfClass(NSXMLNamedNode.Class) == false)
                        {
                            self = NSXMLNamedNode.alloc().initWithKind(kind);
                        }
                    }
                    else
                    {
                        if (self.isKindOfClass(NSXMLNamedFidelityNode.Class) == false)
                        {
                            self = NSXMLNamedFidelityNode.alloc().initWithKind(kind);
                            ((NSXMLNamedFidelityNode)self).setFidelity(options);
                        }
                    } 
                    break;
                
                case NSXMLNodeKind.NSXMLCommentKind:
                    break;

                case NSXMLNodeKind.NSXMLTextKind:
                    if ((options & 0x8400001) == 0)
                    {
                        this._setKind(kind);
                    }
                    else 
                    {
                        if (self.isKindOfClass(NSXMLFidelityNode.Class) == false)
                        {
                            self = NSXMLFidelityNode.alloc().initWithKind(kind);
                            ((NSXMLNamedFidelityNode)self).setFidelity(options);
                        }
                    }
                    break;

                case NSXMLNodeKind.NSXMLDTDKind:
                    break;

                
                case NSXMLNodeKind.NSXMLEntityDeclarationKind:
                case NSXMLNodeKind.NSXMLElementDeclarationKind:
                case NSXMLNodeKind.NSXMLNotationDeclarationKind:
                    if (self.isKindOfClass(NSXMLDTDNode.Class) == false)
                    {
                        //self = NSXMLFidelityNode.alloc().initWithKind(kind);
                    }
                    break;

                case NSXMLNodeKind.NSXMLAttributeDeclarationKind:
                    if (self.isKindOfClass(NSXMLAttributeDeclaration.Class) == false)
                    {
                        self = null;
                    }
                    break;
                
                default:
                    break;

            }

            return self;
        }

//        function methImpl_NSXMLNode_detach {
//    if (rdi._parent == 0x0) goto loc_1d2b1e;
//    goto loc_1d2ad8;

//loc_1d2b1e:
//    return rax;

//loc_1d2ad8:
//    rax = [rbx retain];
//    if ((rbx._kind & 0xff & 0xf) != 0x4) goto loc_1d2b29;
//    goto loc_1d2afd;

//loc_1d2b29:
//    if ((rax & 0xf) != 0x3) goto loc_1d2b5b;
//    goto loc_1d2b31;

//loc_1d2b5b:
//    rax = [*(rbx + r14) kind];
//    if ((rax == 0x1) || (rax == 0x2)) {
//            [*(rbx + r14) removeChildAtIndex:rbx._index >> 0x4];
//    }

//loc_1d2b96:
//    rbx._index = rbx._index & 0xf;
//    [rbx _setParent:0x0];
//    rdi = rbx;
//    rax = [rdi autorelease];

//loc_1d2b31:
//    r14 = *(rbx + r14);
//    rax = [rbx name];
//    rsi = @selector(removeAttributeForName:);

//loc_1d2b50:
//    rdx = rax;
//    (r15)(r14);
//    goto loc_1d2b96;

//loc_1d2afd:
//    r14 = *(rbx + r14);
//    rax = [rbx name];
//    rsi = @selector(removeNamespaceForPrefix:);
//    goto loc_1d2b50;
//}

        public virtual void detach()
        {
            if (this._parent == null)
                return;

            if(this._kind != NSXMLNodeKind.NSXMLNamespaceKind)
            {
                if (this._kind != NSXMLNodeKind.NSXMLAttributeKind)
                {
                    if ((_parent.kind() == NSXMLNodeKind.NSXMLDocumentKind) ||
                        (_parent.kind() == NSXMLNodeKind.NSXMLElementKind))
                    {
                        ((NSXMLElement)_parent).removeChildAtIndex(_index);
                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }

       
    }
}
