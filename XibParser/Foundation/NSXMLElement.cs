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
    public class NSXMLElement : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLElement));
        new public static NSXMLElement alloc() { return new NSXMLElement(); }

        protected NSArray _namespaces;
        protected NSArray _attributes;
        protected NSXMLChildren _children;
        protected bool _zeroOrOneNamespaces;
        protected bool _zeroOrOneAttributes;
        protected NSString _name;
        protected int _prefixIndex;
        protected NSString _URI;
        protected bool _childrenHaveMutated;
        protected char[] _passing;

        public override void setURI(NSString uri)
        {
            if (this._URI == uri)
                return;

            this._URI = uri;
            if (this._URI == null)
            {
                this._setQNamesAreResolved(false);
            }

        }

        public override void setName(NSString name)
        {
            if (name == _name)
                return;
        }


        public override NSString name()
        {
            return this._name;
        }


        public override NSString stringValue()
        {
            throw new NotImplementedException("stringValue");
            return null;
        }


        public override uint childCount()
        {
           if (this._objectValue != null)
               return 1;
           else
               return this._children.count();
        }


        // WARNING : there is also a _children but it's amibiguous
        public override NSArray children()
        {
            NSArray children;

            if (_objectValue != null)
            {
                children = NSArray.arrayWithObject(_objectValue);
            }
            else
            {
                _children.makeStale();
                children = _children;
            }

            return children;
        }


        public virtual bool _QNamesAreResolved()
        {
            return (this._childrenHaveMutated == false);
        }



        public virtual void _setQNamesAreResolved(bool QNamesResolved)
        {
            this._childrenHaveMutated = (QNamesResolved == false);
            if (QNamesResolved == false)
                return;

            if (this.parent().kind() == NSXMLNodeKind.NSXMLElementKind)
            {
            loc_rec:
                if (this._QNamesAreResolved() == true)
                {
                    _setQNamesAreResolved(false);
                    if (parent().kind() == NSXMLNodeKind.NSXMLElementKind)
                        goto loc_rec;
                }
            }

        }

        public virtual NSString prefix()
        {
            if (this._prefixIndex == -1)
                return @"";
            else
                return this._name.substringToIndex((uint)this._prefixIndex);
        }

        public virtual id init()
        {
            return this.initWithName(null);
        }
        public virtual id initWithName(NSString name)
        {
            return this.initWithNameURI(name, null);
        }

        public virtual id initWithNameURI(NSString name, NSString uri)
        {
            id self = this;

            return self;
        }

        public virtual id initWithNameStringValue(NSString name, NSString stringValue)
        {
            id self = this;

            return self;
        }

        public id _initWithName(NSString name, NSString uri, int prefixIndex)
        {
            id self = this;

            if (base.init() != null)
            {
                this._zeroOrOneNamespaces = true;
                this._zeroOrOneAttributes = true;
                this._kind = NSXMLNodeKind.NSXMLElementKind;
                this.setName(name);
                this._prefixIndex = prefixIndex;
                this._setQNamesAreResolved(uri != null);
                this.setURI(uri);
            }

            return self;
        }


        //        function methImpl_NSXMLElement_attributeForName_ {
        //    r15 = rdx;
        //    r14 = rdi;
        //    rax = [rdi attributeForLocalName:rdx URI:0x0];
        //    if (rax != 0x0) goto loc_1c5048;
        //    goto loc_1c4fe0;

        //loc_1c5048:
        //    return rax;

        //loc_1c4fe0:
        //    rbx = *objc_msgSend;
        //    rax = [r14 resolveNamespaceForName:r15] stringValue];
        //    rcx = rbx;
        //    r12 = rax;
        //    rax = 0x0;
        //    if (r12 == 0x0) goto loc_1c5048;
        //    rbx = rcx;
        //    rax = (rbx)(*objc_classref_NSXMLNode, @selector(localNameForName:), r15);
        //    rdi = r14;
        //    rdx = rax;
        //    rcx = r12;
        //    rax = rbx;
        //    rax = (rax)(rdi, @selector(attributeForLocalName:URI:), rdx, rcx);
        //}

        public virtual NSXMLNode attributeForName(NSString name)
        {
            var attr = this.attributeForLocalName(name, null);
            if (attr == null)
            {
                //NSString 
            }
            else
            {
                return attr;
            }

            throw new NotImplementedException();
            return null;
        }


        public virtual NSXMLNode attributeForLocalName(NSString localName, NSString URI)
        {
            throw new NotImplementedException();
            return null;
        }
       

//        function methImpl_NSXMLElement_removeChildAtIndex_ {
//    rbx = rdx;
//    if (rdi._children == 0x0) goto loc_1c758a;
//    goto loc_1c74ed;

//loc_1c758a:
//    if (((r14._kind & 0xf) == 0x2) && (rbx == 0x0)) goto loc_1c75a3;
//    goto loc_1c75ec;

//loc_1c75a3:
//    rbx = *_OBJC_IVAR_$_NSXMLNode._objectValue;
//    [*(r14 + rbx) _setParent:0x0];
//    [*(r14 + rbx) release];
//    rsi = r14;
//    rdx = rbx;
//    rax = objc_assign_ivar(0x0, rsi, rdx);
//    return rax;

//loc_1c75ec:
//    return rax;

//loc_1c74ed:
//    r12 = *objc_msgSend;
//    [[r14 childAtIndex:rbx] _setParent:0x0];
//    rax = [*(r14 + _children) reallyRemoveObjectAtIndex:rbx];
//    r15 = *_OBJC_IVAR_$_NSXMLElement._children;
//    objc_assign_ivar(rax, self, _children);
//    rax = [*(r14 + r15) count];
//    if (rax > rbx) {
//            do {
//                    rax = [[r14._children objectAtIndex:rbx] _setIndex:rbx];
//                    rbx = rbx + 0x1;
//            } while (*objc_msgSend != rbx);
//    }
//    goto loc_1c75ec;
//}

        public override NSXMLNode childAtIndex(uint index)
        {
            return null;
        }

        public virtual void removeChildAtIndex(uint nodeIndex)
        {
            if (this._children == null)
            {
                if ((_kind == NSXMLNodeKind.NSXMLElementKind) && (nodeIndex == 0))
                {
                    ((NSXMLNode)_objectValue)._setParent(null);
                    _index = 0;
                }
            }
            else
            {
                this.childAtIndex(nodeIndex)._setParent(null);
                _children = _children.reallyRemoveObjectAtIndex(nodeIndex);
                if (_children.count() > nodeIndex)
                {
                    for(uint i = nodeIndex; i <  _children.count(); i++)
                    {
                        ((NSXMLNode)_children.objectAtIndex(i))._setIndex(i);
                    }
                }
            }
        }


        //        – initWithName:
        //– initWithName:stringValue:
        //– initWithXMLString:error:
        //– initWithName:URI:

        //NSXMLElement *root = [[NSXMLElement alloc] initWithName:@"Request"];



        //public virtual NSArray elements()
        //{
        //    NSArray elmts;

        //    var children = this.children();
        //    if (children != null && children.count() > 0)
        //    {
        //        elmts = NSMutableArray.array();
        //        foreach (NSXMLNode node in children)
        //        {
        //            if (node.kind() == NSXMLNodeKind.NSXMLElementKind)
        //            {
        //                elmts.addObject(node);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        elmts = NSArray.emptyArray();
        //    }
        //    return elmts;
        //}
    }
}
