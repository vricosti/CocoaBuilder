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

        protected int _index;

        protected NSXMLNodeKind _kind;

        protected id _objectValue;

        protected NSXMLNode _parent;

        protected id _private;


        public virtual void _SetKind(NSXMLNodeKind kind)
        {
            _kind = kind;
        }

        public virtual NSXMLNodeKind GetKind()
        {
            return _kind;
        }

        public virtual NSString GetURI()
        {
            return null;
        }

        public virtual int GetIndex()
        {
            return _index;
        }

        public virtual NSString XMLString()
        {
            return this.XMLStringWithOptions(0);
        }

        public virtual NSString XMLStringWithOptions(uint options)
        {
            return this._XMLStringWithOptions(options, NSMutableString.String());
        }

        public virtual NSString _XMLStringWithOptions(uint options, NSString str)
        {
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
                xpath = NSString.stringWithFormat(@"%@/node()[%ld]", _parent.XPath(), this.GetIndex());
            }
            return xpath;
        }


        public override id init()
        {
            id self = this;

            this._SetKind(0);
            this._index = 0;

            return self;
        }


        public virtual id InitWithKind(NSXMLNodeKind kind)
        {
            return this.InitWithKind(kind, 0);
        }
       
        public virtual id InitWithKind(NSXMLNodeKind kind, uint options)
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
                            ((NSXMLFidelityElement)self).SetFidelity(options);
                        }
                    }
                    break;

                case NSXMLNodeKind.NSXMLAttributeKind:
                    if ((options & 0x8c00008) == 0)
                    {
                        if (self.isKindOfClass(NSXMLNamedNode.Class) == false)
                        {
                            self = NSXMLNamedNode.alloc().InitWithKind(kind);
                        }
                    }
                    else
                    {
                        if (self.isKindOfClass(NSXMLNamedFidelityNode.Class) == false)
                        {
                            self = NSXMLNamedFidelityNode.alloc().InitWithKind(kind);
                            ((NSXMLNamedFidelityNode)self).SetFidelity(options);
                        }
                   } 
                    break;


                case NSXMLNodeKind.NSXMLNamespaceKind:
                case NSXMLNodeKind.NSXMLProcessingInstructionKind:
                    if ((options & 0x8 /*NSXMLNodeUseSingleQuotes*/) == 0)
                    {
                        if (self.isKindOfClass(NSXMLNamedNode.Class) == false)
                        {
                            self = NSXMLNamedNode.alloc().InitWithKind(kind);
                        }
                    }
                    else
                    {
                        if (self.isKindOfClass(NSXMLNamedFidelityNode.Class) == false)
                        {
                            self = NSXMLNamedFidelityNode.alloc().InitWithKind(kind);
                            ((NSXMLNamedFidelityNode)self).SetFidelity(options);
                        }
                    } 
                    break;
                
                case NSXMLNodeKind.NSXMLCommentKind:
                    break;

                case NSXMLNodeKind.NSXMLTextKind:
                    if ((options & 0x8400001) == 0)
                    {
                        this._SetKind(kind);
                    }
                    else 
                    {
                        if (self.isKindOfClass(NSXMLFidelityNode.Class) == false)
                        {
                            self = NSXMLFidelityNode.alloc().InitWithKind(kind);
                            ((NSXMLNamedFidelityNode)self).SetFidelity(options);
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
                        //self = NSXMLFidelityNode.alloc().InitWithKind(kind);
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

        

       
    }
}
