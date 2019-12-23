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
    public class NSXMLElement : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLElement));
        new public static NSXMLElement alloc() { return new NSXMLElement(); }

        protected NSString _name; //0x14(x86) - 0x20(x64)
        protected NSXMLChildren _attributes; //0x18(x86) - 0x28(x64)
        protected NSArray _namespaces; //0x1C(x86) - 0x30(x64)
        protected NSXMLChildren _children42; //0x20(x86) - 0x38(x64)
        protected bool _childrenHaveMutated; //0x24(x86) - 0x40(x64)
        protected bool _zeroOrOneAttributes; //0x25(x86) - 0x41(x64)
        protected bool _zeroOrOneNamespaces; //0x26(x86) - 0x42(x64)
        protected bool _padding; //0x27(x86) - 0x43(x64)
        protected NSString _URI; //0x28(x86) - 0x48(x64)
        protected int _prefixIndexNC; //0x2C(x86) - 0x50(x64)
       
        
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

        public override id objectValue()
        {
            return this.stringValue();
        }

        public override NSString stringValue()
        {
            throw new NotImplementedException("stringValue");
            return null;
        }

        public virtual int _prefixIndex()
        {
            return _prefixIndexNC;
        }

        public override uint childCount()
        {
           if (this._objectValue != null)
               return 1;
           else
               return this._children42.count();
        }


        public virtual NSArray _children()
        {
            NSArray children;

            if (_objectValue != null)
            {
                children = NSArray.arrayWithObject(_objectValue);
            }
            else
            {
                children = _children42;
            }

            return children;
        }

        public override NSArray children()
        {
            NSArray children;

            if (_objectValue != null)
            {
                children = NSArray.arrayWithObject(_objectValue);
            }
            else
            {
                ((NSXMLChildren)_children42).makeStale();
                children = _children42;
            }

            return children;
        }


        public virtual bool _QNamesAreResolved()
        {
            return (this._childrenHaveMutated == false);
        }

        public virtual void _setQNamesAreResolved(bool qNamesAreResolved)
        {
            this._childrenHaveMutated = (qNamesAreResolved == false);
            if (qNamesAreResolved == false)
            {
                NSXMLNode curNode = this.parent();
                while (curNode != null)
                {
                    if ((curNode.kind() != NSXMLNodeKind.NSXMLElementKind) || 
                        ((NSXMLElement)curNode)._QNamesAreResolved() == false)
                        break;

                    ((NSXMLElement)curNode)._setQNamesAreResolved(false);

                    curNode = curNode.parent();
                }
            }

        }


        public virtual NSString prefix()
        {
            if (this._prefixIndexNC == -1)
                return @"";
            else
                return this._name.substringToIndex((uint)this._prefixIndexNC);
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
                this._prefixIndexNC = prefixIndex;
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


//        function methImpl_NSXMLElement__resolveNamespaceForPrefix_ {
//    r14 = rdx;
//    if (rdi == 0x0) goto loc_1c649f;
//    goto loc_1c644e;

//loc_1c649f:
//    rax = [r14 length];
//    r15 = 0x0;
//    if (rax == 0x0) goto loc_1c64dc;
//    goto loc_1c64b7;

//loc_1c64dc:
//    rax = r15;
//    return rax;

//loc_1c64b7:
//    rdx = r14;
//    rax = [NSXMLNode predefinedNamespaceForPrefix:rdx];

//loc_1c644e:
//    do {
//            r12 = *objc_msgSend;
//            rax = (r12)(rbx, @selector(namespaceForPrefix:), r14);
//            r15 = rax;
//            rax = (r12)(rbx, @selector(parent));
//            rbx = rax;
//            rax = (r12)(rbx, *objc_sel_kind);
//            if (rax != 0x2) {
//                    rbx = 0x0;
//            }
//    } while ((r15 == 0x0) && (rbx != 0x0));
//    if (r15 != 0x0) goto loc_1c64dc;
//    goto loc_1c649f;
//}
       
        public virtual NSXMLNode namespaceForPrefix(NSString name)
        {
            return null;
        }

        public virtual NSXMLNode _resolveNamespaceForPrefix(NSString prefix)
        {
            NSXMLNode nsNode = null;

            if (prefix == null || prefix.length() == 0)
                return null;

            NSXMLNode nsPrefixNode = null;
            bool keepOnSearch = true;
            do
            {
                nsPrefixNode = namespaceForPrefix(prefix);
                if (this.parent().kind() != NSXMLNodeKind.NSXMLElementKind)
                { 
                    keepOnSearch = false;
                    nsNode = nsPrefixNode;
                }

            }
            while ((nsPrefixNode == null) && (keepOnSearch));

            if (nsNode == null)
            {
                nsNode = NSXMLNode.predefinedNamespaceForPrefix(prefix);
            }

            return nsNode;
        }


        public virtual NSXMLNode resolveNamespaceForName(NSString name)
        {
            NSXMLNode node = this;

            NSString prefix = NSXMLNode.prefixForName(name);
            if (prefix != "")
            {
                node = this._resolveNamespaceForPrefix(prefix);
            }

            return node;
        }

       

        public virtual uint countOfAttributes()
        {
            uint result; 

            if (_zeroOrOneAttributes)
                result = (uint)((_attributes != null) ? 1 : 0);
            else
                result = _attributes.count();

            return result;
        }

        public virtual void insertObject_inAttributesAtIndex(NSXMLNode aNode, uint anIndex)
        {
            if (aNode.kind() != NSXMLNodeKind.NSXMLAttributeKind)
            {
                NSException.raise("not an attribute", "");
            }
            if(aNode.parent() != null)
            {
                NSException.raise("Cannot add an attribute with a parent; detach or copy first", "");
            }

            aNode._setParent(this);
            if ((int)Objc.MsgSend(aNode, "_prefixIndex") != -2)
            {
                if (aNode.URI() == null)
                {
                    Objc.MsgSend(aNode, "_resolveName");
                    if (aNode.URI() == null)
                        this._setQNamesAreResolved(false);
                }
            }
            if(_attributes == null)
            {
                _attributes = (NSXMLChildren)aNode.retain(); 
                return;
            }

            uint attrCount;
            if (_zeroOrOneAttributes)
            {
                NSXMLChildren children = (NSXMLChildren)NSXMLChildren.alloc().init();
                _attributes = (NSXMLChildren)children.reallyInsertObject(_attributes.autorelease(), 0);
                _zeroOrOneAttributes = false;
                attrCount = 1;
            }
            else
            {
                attrCount = _attributes.count();
                if (attrCount == 0)
                {
                    //FIXME
                    return;
                }
            }

            uint index = 0;
            do
            {
                var curNodeAttr = _attributes.objectAtIndex(index);
                if ((bool)Objc.MsgSend(curNodeAttr, "_nameIsEqualToNameOfNode", aNode) == false)
                    index = NS.NotFound;
                index++;
            }
            while (index < attrCount && index == NS.NotFound);
            
            if(index ==  NS.NotFound)
            {

            }
            else
            {

            }
        }
        public virtual void addAttribute(NSXMLNode node)
        {
            this.insertObject_inAttributesAtIndex(node, countOfAttributes());
        }


        public virtual void _addTrustedAttribute(NSXMLNode node, uint index)
        {
            node._setParent(this);
            if (_attributes != null)
            {
                if (_zeroOrOneAttributes)
                {
                    NSXMLChildren children = (NSXMLChildren)NSXMLChildren.alloc().init();
                    _attributes = (NSXMLChildren)children.reallyAddObject(_attributes.autorelease());
                    _zeroOrOneAttributes = false;
                }
                _attributes = (NSXMLChildren)_attributes.reallyInsertObject(node, index);
            }
            else
            {
                _attributes = (NSXMLChildren)node.retain();
            }
        }


        public virtual NSXMLNode attributeForName(NSString name)
        {
            NSXMLNode attr = null;
            attr = this.attributeForLocalName(name, null);
            if (attr == null)
            {
                NSString ns = this.resolveNamespaceForName(name).stringValue();
                if (ns != null)
                {

                }
            }
            else
            {
                return attr;
            }

            throw new NotImplementedException();
            return attr;
        }


        public virtual NSXMLNode attributeForLocalName(NSString localName, NSString URI)
        {
            throw new NotImplementedException();
            return null;
        }
       
        public override NSXMLNode childAtIndex(uint index)
        {
            return null;
        }

        public virtual void removeChildAtIndex(uint nodeIndex)
        {
            if (this._children42 == null)
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
                _children42 = _children42.reallyRemoveObjectAtIndex(nodeIndex);
                if (_children42.count() > nodeIndex)
                {
                    for(uint i = nodeIndex; i <  _children42.count(); i++)
                    {
                        ((NSXMLNode)_children42.objectAtIndex(i))._setIndex(i);
                    }
                }
            }
        }




        //- (void)insertChild:(NSXMLNode *)child atIndex:(NSUInteger)index
        public virtual void insertChild(NSXMLNode child, uint index)
        {

        }


        public virtual void addChild(NSXMLNode child)
        {
            this.insertChild(child, this.childCount());
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
