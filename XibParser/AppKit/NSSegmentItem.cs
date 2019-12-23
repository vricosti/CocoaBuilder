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


namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSSegmentedCell.m
    public class NSSegmentItem : NSObject
    {
        new public static Class Class = new Class(typeof(NSSegmentItem));

        protected bool _selected;
        protected bool _enabled;
        protected int _tag;
        protected float _width;
        protected NSMenu _menu;
        protected NSString _label;
        protected NSString _tool_tip;
        protected NSImage _image;
        protected NSRect _frame;

        [ObjcPropAttribute("selected", GetName = "isSelected")]
        public virtual bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        [ObjcPropAttribute("enabled", GetName = "isEnabled")]
        public virtual bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [ObjcPropAttribute("menu")]
        public virtual NSMenu Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        [ObjcPropAttribute("label")]
        public virtual NSString Label
        {
            get { return _label; }
            set { _label = value; }
        }

        [ObjcPropAttribute("toolTip")]
        public virtual NSString ToolTip
        {
            get { return _tool_tip; }
            set { _tool_tip = value; }
        }

        [ObjcPropAttribute("image")]
        public virtual NSImage Image
        {
            get { return _image; }
            set { _image = value; }
        }

        [ObjcPropAttribute("tag")]
        public virtual int Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        [ObjcPropAttribute("width")]
        public virtual float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        [ObjcPropAttribute("frame")]
        public virtual NSRect Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }


        new public static NSSegmentItem alloc()
        {
            return new NSSegmentItem();
        }

        public override id init()
        {
            id self = this;

            base.init();
            if (self == null)
                return null;

            _enabled = true;

            return self;
        }


        public override void encodeWithCoder(NSCoder aCoder)
        {
            if (aCoder.AllowsKeyedCoding)
            {
                if (_label != null)
                    aCoder.encodeObjectForKey(_label, @"NSSegmentItemLabel");
                if (_image != null)
                    aCoder.encodeObjectForKey(_image, @"NSSegmentItemImage");
                if (_menu != null)
                    aCoder.encodeObjectForKey(_menu, @"NSSegmentItemMenu");
                if (_enabled)
                    aCoder.encodeBoolForKey(true, @"NSSegmentItemEnabled");
                else
                    aCoder.encodeBoolForKey(true, @"NSSegmentItemDisabled");
                if (_selected)
                    aCoder.encodeBoolForKey(true, @"NSSegmentItemSelected");
                if (_width != 0.0)
                    aCoder.encodeFloatForKey(_width, @"NSSegmentItemWidth");
                if (_tag != 0)
                    aCoder.encodeIntForKey(_tag, @"NSSegmentItemTag");
            }
            else
            {
                aCoder.encodeObject(_label);
                aCoder.encodeObject(_image);
                aCoder.encodeObject(_menu);
                aCoder.encodeValueOfObjCType<bool>(ref _enabled);
                aCoder.encodeValueOfObjCType<bool>(ref _selected);
                aCoder.encodeValueOfObjCType<float>(ref _width);
                aCoder.encodeValueOfObjCType<int>(ref _tag);
            }
        }
        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.containsValueForKey(@"NSSegmentItemLabel"))
                    Label = (NSString)aDecoder.decodeObjectForKey(@"NSSegmentItemLabel");
                if (aDecoder.containsValueForKey(@"NSSegmentItemImage"))
                    Image = (NSImage)aDecoder.decodeObjectForKey(@"NSSegmentItemImage");
                if (aDecoder.containsValueForKey(@"NSSegmentItemMenu"))
                    Menu = (NSMenu)aDecoder.decodeObjectForKey(@"NSSegmentItemMenu");
                if (aDecoder.containsValueForKey(@"NSSegmentItemEnabled"))
                    _enabled = aDecoder.decodeBoolForKey(@"NSSegmentItemEnabled");
                else if (aDecoder.containsValueForKey(@"NSSegmentItemDisabled"))
                    _enabled = !aDecoder.decodeBoolForKey(@"NSSegmentItemDisabled");
                else
                    _enabled = true;
                if (aDecoder.containsValueForKey(@"NSSegmentItemSelected"))
                    _selected = aDecoder.decodeBoolForKey(@"NSSegmentItemSelected");
                if (aDecoder.containsValueForKey(@"NSSegmentItemWidth"))
                    _width = aDecoder.decodeFloatForKey(@"NSSegmentItemWidth");
                if (aDecoder.containsValueForKey(@"NSSegmentItemTag"))
                    _tag = aDecoder.decodeIntForKey(@"NSSegmentItemTag");
            }
            else
            {
                _label = (NSString)aDecoder.decodeObject();
                _image = (NSImage)aDecoder.decodeObject();
                _menu = (NSMenu)aDecoder.decodeObject();
                aDecoder.decodeValueOfObjCType<bool>(ref _enabled);
                aDecoder.decodeValueOfObjCType<bool>(ref _selected);
                aDecoder.decodeValueOfObjCType<float>(ref _width);
                aDecoder.decodeValueOfObjCType<int>(ref _tag);
            }

            return self;
        }

    }
}
