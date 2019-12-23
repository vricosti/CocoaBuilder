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
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSActionCell.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSActionCell.m
    public class NSActionCell : NSCell
    {
        new public static Class Class = new Class(typeof(NSActionCell));

        protected int _tag;
        protected id _target;
        protected SEL _action;
        protected NSView _control_view;

        public NSActionCell()
        {

        }

        [ObjcPropAttribute("action")]
        public override SEL Action
        {
            get { return _action; }
            set { _action = value; }
        }

        [ObjcPropAttribute("target")]
        public override id Target
        {
            get { return _target; }
            set { _target = value; }
        }

        [ObjcPropAttribute("tag")]
        public override int Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        [ObjcPropAttribute("controlView")]
        public override NSView ControlView
        {
            get { return _control_view; }
            set { _control_view = value; }
        }


        public override void encodeWithCoder(NSCoder aCoder)
        {
            base.encodeWithCoder(aCoder);
            if (aCoder.AllowsKeyedCoding)
            {
                aCoder.encodeIntegerForKey(Tag, "NSTag");
                if (Target != null)
                {
                    aCoder.encodeObjectForKey(Target, "NSTarget");
                }
                if (Action != null)
                {
                    aCoder.encodeObjectForKey(SEL.StringFromSelector(this.Action), "NSAction");
                }
                aCoder.encodeObjectForKey(_control_view, "NSControlView");
            }
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.initWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.containsValueForKey("NSTag"))
                {
                    this.Tag = aDecoder.decodeIntForKey("NSTag");
                }
                if (aDecoder.containsValueForKey("NSTarget"))
                {
                    this.Target = aDecoder.decodeObjectForKey("NSTarget");
                }
                if (aDecoder.containsValueForKey("NSAction"))
                {
                    NSString action = (NSString)aDecoder.decodeObjectForKey("NSAction");
                    this.Action = SEL.SelectorFromString(action);
                }
            }

            return self;
        }

        


        protected void _updateCell()
        {
            if (_control_view != null && _control_view.isKindOfClass(NSControl.Class))
            {
                ((NSControl)_control_view).UpdateCell(this);
            }
        }
    }
}
