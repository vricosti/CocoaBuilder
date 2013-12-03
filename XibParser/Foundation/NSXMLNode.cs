﻿/*
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
        new public static NSXMLNode Alloc() { return new NSXMLNode(); }

        public virtual id InitWithKind(NSXMLNodeKind kind)
        {
            return null;
        }
       




       
    }
}