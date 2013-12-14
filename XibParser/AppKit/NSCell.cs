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
using Smartmobili.Cocoa.Utils;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSCell_Class/Reference/NSCell.html
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSCell.m
    public class NSCell : NSObject, INSNumber, IAction
    {
        new public static Class Class = new Class(typeof(NSCell));
        new public static NSCell alloc() { return new NSCell(); }

        public struct GSCellFlagsType
        {
            // total 32 bits.  0 bits left.
            [BitfieldLength(1)]
            public uint contents_is_attributed_string;
            [BitfieldLength(1)]
            public uint is_highlighted;
            [BitfieldLength(1)]
            public uint is_disabled;
            [BitfieldLength(1)]
            public uint is_editable;
            [BitfieldLength(1)]
            public uint is_rich_text;
            [BitfieldLength(1)]
            public uint imports_graphics;
            [BitfieldLength(1)]
            public uint shows_first_responder;
            [BitfieldLength(1)]
            public uint refuses_first_responder;
            [BitfieldLength(1)]
            public uint sends_action_on_end_editing;
            [BitfieldLength(1)]
            public uint is_bordered;
            [BitfieldLength(1)]
            public uint is_bezeled;
            [BitfieldLength(1)]
            public uint is_scrollable;
            [BitfieldLength(1)]
            public uint reserved;
            [BitfieldLength(3)]
            public uint text_align; // 5 values
            [BitfieldLength(1)]
            public uint is_selectable;
            [BitfieldLength(1)]
            public uint allows_mixed_state;
            [BitfieldLength(1)]
            public uint has_valid_object_value;
            [BitfieldLength(2)]
            public uint type;           // 3 values
            [BitfieldLength(3)]
            public uint image_position; // 7 values
            [BitfieldLength(4)]
            public uint entry_type;     // 8 values
            [BitfieldLength(1)]
            public uint allows_undo;
            [BitfieldLength(3)]
            public uint line_break_mode; // 6 values

            // total 20 bits.  4 bits extension, 8 bits left.
            [BitfieldLength(2)]
            public int state; // 3 values but one negative
            [BitfieldLength(8)]
            public uint mnemonic_location;
            [BitfieldLength(3)]
            public uint control_tint;
            [BitfieldLength(2)]
            public uint control_size;
            [BitfieldLength(2)]
            public uint focus_ring_type; // 3 values
            [BitfieldLength(2)]
            public uint base_writing_direction; // 3 values
            // 4 bits reserved for subclass use
            [BitfieldLength(1)]
            public uint subclass_bool_one;
            [BitfieldLength(1)]
            public uint subclass_bool_two;
            [BitfieldLength(1)]
            public uint subclass_bool_three;
            [BitfieldLength(1)]
            public uint subclass_bool_four;
            // Set while the cell is edited/selected
            [BitfieldLength(1)]
            public uint in_editing;
        };

        protected object _contents;
        protected NSImage _cell_image;
        protected NSFont _font;
        protected id _object_value;
        protected GSCellFlagsType _cell;
        protected uint _mouse_down_flags;
        protected uint _action_mask;
        protected NSFormatter _formatter;
        protected NSMenu _menu;
        //id _represented_object;
        //object _reserved1;

        [ObjcPropAttribute("Font")]
        public virtual NSFont Font
        {
            get { return _font; }
            set
            {
                if (_cell.type != (uint)NSCellType.NSTextCellType)
                {
                    this.Type = NSCellType.NSTextCellType;
                }
                _font = value;
            }
        }

        [ObjcPropAttribute("Image")]
        public virtual NSImage Image
        {
            get { return (_cell.type == (uint)NSCellType.NSImageCellType) ? _cell_image : null; }
            set
            {
                if (_cell.type != (uint)NSCellType.NSImageCellType)
                {
                    this.Type = NSCellType.NSImageCellType;
                }
                _cell_image = value;
            }
        }

        [ObjcPropAttribute("Type")]
        public virtual NSCellType Type
        {
            get
            {
                if (_cell.type == (uint)NSCellType.NSImageCellType && _cell_image == null)
                    return NSCellType.NSNullCellType;

                return (NSCellType)_cell.type;
            }
            set
            {
                if (_cell.type == (uint)value)
                {
                    return;
                }

                _cell.type = (uint)value;
                switch (_cell.type)
                {
                    case (uint)NSCellType.NSTextCellType:
                        {
                            _contents = "title";
                            _cell.contents_is_attributed_string = 0;
                            /* Doc says we have to reset the font too. */
                            _font = NSFont.SystemFontOfSize(-1);
                            break;
                        }
                    case (uint)NSCellType.NSImageCellType:
                        {
                            _cell_image = null;
                            break;
                        }
                }
            }
        }

        [ObjcPropAttribute("ObjectValue")]
        public virtual object ObjectValue
        {
            get
            {
                if (_cell.state == (int)NSCellStateValue.NSOffState)
                {
                    return NSNumber.numberWithBool(false);
                }
                else if (_cell.state == (int)NSCellStateValue.NSOnState)
                {
                    return NSNumber.numberWithBool(true);
                }
                else // NSMixedState
                {
                    return NSNumber.numberWithInt(-1);
                }
            }
            set
            {
                //id objVal = (id)value;
                NSObject objVal = (NSObject)value;
                if (objVal == null)
                {
                    this.State = (int)NSCellStateValue.NSOffState;
                }
                //else if (objVal.respondsToSelector: @selector(intValue)])
                // TODO : maybe implement respondsToSelector using reflection
                // Something like objVal.respondsToSelector("intValue"); (int)objVal.SendObjMsg("intValue");
                // (int)objVal.ObjcSendMsg();
                else if (objVal is INSNumber)
                {

                    this.State = ((INSNumber)objVal).intValue();
                }
                else
                {
                    this.State = (int)NSCellStateValue.NSOnState;
                }
            }
        }

        [ObjcPropAttribute("HasValidObjectValue", SetName = null)]
        public virtual bool HasValidObjectValue
        {
            get { return Convert.ToBoolean(_cell.has_valid_object_value); }
        }

        public virtual double DoubleValue
        {
            get { return doubleValue(); }
            set { setDoubleValue(value); }
        }

        public virtual double doubleValue()
        {
            if ((Convert.ToBoolean(_cell.has_valid_object_value) == true) 
                && (_object_value is INSNumber))
            {
                return (double)((INSNumber)_object_value).doubleValue();
            }
            else
            {
                return this.StringValue.doubleValue();
            }
        }

        public virtual void setDoubleValue(double value)
        {
            this.ObjectValue = NSNumber.numberWithDouble(value);
        }

        public virtual float FloatValue
        {
            get { return floatValue(); }
            set { setFloatValue(value); }
        }

        public virtual float floatValue()
        {
            return _cell.state;
        }

        public virtual void setFloatValue(float value)
        {
            this.ObjectValue = NSNumber.numberWithFloat(value);
        }

        public virtual int IntegerValue
        {
            get { return integerValue(); }
            set { setIntegerValue(value); }
        }
        public virtual int integerValue()
        {
            return _cell.state;
        }

        public virtual void setIntegerValue(int value)
        {
            this.ObjectValue = NSNumber.numberWithInteger(value);
        }

       
        public virtual int IntValue
        {
            get { return intValue(); }
            set { setIntValue(value); }
        }
        public virtual int intValue()
        {
            return _cell.state;
        }
        public virtual void setIntValue(int value)
        {
            this.ObjectValue = NSNumber.numberWithInteger(value);
        }

        public virtual bool BoolValue
        {
            get { return boolValue(); }  
        }
        public virtual bool boolValue()
        {
            return false;
        }


        [ObjcPropAttribute("StringValue", SetName = null)]
        public virtual NSString StringValue
        {
            get
            {
                if (null == _contents)
                {
                    return @"";
                }

                if (Convert.ToBoolean(_cell.contents_is_attributed_string) == false)
                {
                    return (NSString)_contents;
                }
                else
                {
                    throw new NotImplementedException("NSCell.StringValue");
                    //return (NSAttributedString);
                }
            }

            set
            {
                NSString aString = value;
                /* We warn about nil for compatibiliy with MacOS X, which refuses nil.  */
                if (aString == null)
                {
                    System.Diagnostics.Debug.WriteLine("MacOSXCompatibility " + "Attempt to use nil as string value");
                }

                if (_cell.type != (uint)NSCellType.NSTextCellType)
                {
                    this.Type = NSCellType.NSTextCellType;
                }

                if (_formatter == null)
                {

                }
                else
                {
                    //FIXME
                    //id newObjectValue;
                }
            }
        }

        [ObjcPropAttribute("wraps")]
        public virtual bool Wraps
        {
            get
            {
                return ((_cell.line_break_mode == (uint)NSLineBreakMode.NSLineBreakByWordWrapping)
                    || (_cell.line_break_mode == (uint)NSLineBreakMode.NSLineBreakByCharWrapping));
            }
            set
            {
                if (value)
                {
                    if (!this.Wraps)
                        this.LineBreakMode = NSLineBreakMode.NSLineBreakByWordWrapping;

                }
                else
                {
                    if (this.Wraps)
                        this.LineBreakMode = NSLineBreakMode.NSLineBreakByClipping;
                }
            }
        }

        [ObjcPropAttribute("scrollable", GetName = "isScrollable")]
        public virtual bool Scrollable
        {
            get { return (_cell.is_scrollable == 1); }
            set
            {
                _cell.is_scrollable = Convert.ToUInt32(value);

                if (value)
                {
                    this.Wraps = false;
                }
            }
        }

        [ObjcPropAttribute("selectable", GetName = "isSelectable")]
        public virtual bool Selectable
        {
            get { return ((_cell.is_selectable == 1) || (_cell.is_editable == 1)); }
            set
            {
                _cell.is_selectable = Convert.ToUInt32(value);

                if (!value)
                    _cell.is_editable = 0;
            }
        }

        [ObjcPropAttribute("bezeled", GetName = "isBezeled")]
        public virtual bool Bezeled
        {
            get { return Convert.ToBoolean(_cell.is_bezeled); }
            set
            {
                _cell.is_bezeled = Convert.ToUInt32(value);
                _cell.is_bordered = Convert.ToUInt32(false);
            }
        }

        [ObjcPropAttribute("bordered", GetName = "isBordered")]
        public virtual bool Bordered
        {
            get { return Convert.ToBoolean(_cell.is_bordered); }
            set
            {
                _cell.is_bordered = Convert.ToUInt32(value);
                _cell.is_bezeled = Convert.ToUInt32(false);
            }
        }

        [ObjcPropAttribute("opaque", SetName = null, GetName = "isOpaque")]
        public virtual bool Opaque
        {
            get { return false; }
        }

        [ObjcPropAttribute("focusRingType")]
        public virtual NSFocusRingType FocusRingType
        {
            get { return (NSFocusRingType)_cell.focus_ring_type; }
            set { _cell.focus_ring_type = Convert.ToUInt32(value); }
        }


        [ObjcPropAttribute("editable", GetName = "isEditable")]
        public virtual bool Editable
        {
            get { return _cell.is_editable.ToBool(); }
            set { _cell.is_editable = Convert.ToUInt32(value); }
        }

        [ObjcPropAttribute("enabled", GetName = "isEnabled")]
        public virtual bool Enabled
        {
            get { return (Convert.ToBoolean(_cell.is_disabled) == false); }
            set { _cell.is_disabled = Convert.ToUInt32(!value); }
        }



        [ObjcPropAttribute("State")]
        public virtual int State
        {
            get { return _cell.state; }
            set
            {
                if (value > 0 || (value < 0 && Convert.ToBoolean(_cell.allows_mixed_state) == false))
                {
                    _cell.state = (int)NSCellStateValue.NSOnState;
                }
                else if (value == 0)
                {
                    _cell.state = (int)NSCellStateValue.NSOffState;
                }
                else
                {
                    _cell.state = (int)NSCellStateValue.NSMixedState;
                }
            }
        }

        public virtual bool AllowsMixedState
        {
            get { return (Convert.ToBoolean(_cell.allows_mixed_state)); }
            set
            {
                _cell.allows_mixed_state = Convert.ToUInt32(value);
                if (!value && (_cell.state == (int)NSCellStateValue.NSMixedState))
                {
                    this.SetNextState();
                }
            }
        }

        public int NextState()
        {
            switch (_cell.state)
            {
                case (int)NSCellStateValue.NSOnState:
                    {
                        return (int)NSCellStateValue.NSOffState;
                    }
                case (int)NSCellStateValue.NSOffState:
                    {
                        if (_cell.allows_mixed_state == 1)
                        {
                            return (int)NSCellStateValue.NSMixedState;
                        }
                        else
                        {
                            return (int)NSCellStateValue.NSOnState;
                        }
                    }
                case (int)NSCellStateValue.NSMixedState:
                default:
                    {
                        return (int)NSCellStateValue.NSOnState;
                    }
            }
        }

        public void SetNextState()
        {
            this.State = this.NextState();
            //[self setState: [self nextState]]
        }


        [ObjcPropAttribute("lineBreakMode")]
        public virtual NSLineBreakMode LineBreakMode
        {
            get { return (NSLineBreakMode)_cell.line_break_mode; }
            set
            {
                if ((value == NSLineBreakMode.NSLineBreakByCharWrapping) || (value == NSLineBreakMode.NSLineBreakByWordWrapping))
                {
                    _cell.is_scrollable = 0;
                }
                _cell.line_break_mode = Convert.ToUInt32(value);
            }
        }

        [ObjcPropAttribute("baseWritingDirection")]
        public virtual NSWritingDirection BaseWritingDirection
        {
            get { return (NSWritingDirection)_cell.base_writing_direction; }
            set { _cell.base_writing_direction = Convert.ToUInt32(value); }
        }

        [ObjcPropAttribute("action")]
        public virtual SEL Action
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        [ObjcPropAttribute("target")]
        public virtual id Target
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }



        [ObjcPropAttribute("continuous", GetName = "isContinuous")]
        public virtual bool Continuous
        {
            get { return ((_action_mask & (uint)NSEventMask.NSPeriodicMask)) != 0; }
            set
            {
                if (value)
                {
                    _action_mask |= (uint)NSEventMask.NSPeriodicMask;
                }
                else
                {
                    _action_mask &= ~((uint)NSEventMask.NSPeriodicMask);
                }
            }
        }

        public virtual int SendActionOn(int mask)
        {
            uint previousMask = _action_mask;

            _action_mask = (uint)mask;

            return (int)previousMask;
        }

        [ObjcPropAttribute("tag")]
        public virtual int Tag
        {
            get { return -1; }
            set { throw new NotImplementedException(); }
        }

        [ObjcPropAttribute("entryType")]
        public virtual int EntryType
        {
            get { return (int)_cell.entry_type; }
            set
            {
                this.Type = NSCellType.NSTextCellType;
                _cell.entry_type = (uint)value;
            }
        }

        [ObjcPropAttribute("menu")]
        public virtual NSMenu Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }


        public virtual bool AcceptsFirstResponder
        {
            get { return ((_cell.is_disabled == 0) && (_cell.refuses_first_responder == 0)); }
        }

        [ObjcPropAttribute("showsFirstResponder")]
        public virtual bool ShowsFirstResponder
        {
            get { return ((_cell.is_disabled == 0) && (_cell.refuses_first_responder == 0)); }
            set { _cell.shows_first_responder = Convert.ToUInt32(value); }
        }

        [ObjcPropAttribute("refusesFirstResponder")]
        public virtual bool RefusesFirstResponder
        {
            get { return Convert.ToBoolean(_cell.refuses_first_responder); }
            set { _cell.refuses_first_responder = Convert.ToUInt32(value); }
        }

        public virtual int MouseDownFlags
        {
            get { return (int)_mouse_down_flags; }
        }

        public virtual NSString KeyEquivalent
        {
            get { return @""; }
            set { }
        }

        public virtual NSControlSize ControlSize
        {
            get { return (NSControlSize)_cell.control_size; }
            set { _cell.control_size = Convert.ToUInt32(value); }
        }

        public virtual NSControlTint ControlTint
        {
            get { return (NSControlTint)_cell.control_tint; }
            set { _cell.control_tint = Convert.ToUInt32(value); }
        }

        public virtual NSView ControlView
        {
            get { return null; }
            set { }
        }

        [ObjcPropAttribute("highlighted", GetName = "isHighlighted")]
        public virtual bool Highlighted
        {
            get { return Convert.ToBoolean(_cell.is_highlighted); }
            set { _cell.is_highlighted = Convert.ToUInt32(value); }
        }


        [ObjcPropAttribute("sendsActionOnEndEditing")]
        public virtual bool SendsActionOnEndEditing
        {
            get { return Convert.ToBoolean(_cell.sends_action_on_end_editing); }
            set { _cell.sends_action_on_end_editing = Convert.ToUInt32(value); }
        }

        [ObjcPropAttribute("allowsUndo")]
        public virtual bool AllowsUndo
        {
            get { return Convert.ToBoolean(_cell.allows_undo); }
            set { _cell.allows_undo = Convert.ToUInt32(value); }
        }

        public virtual NSTextAlignment Alignment
        {
            get { return (NSTextAlignment)_cell.text_align; }
            set { _cell.text_align = (uint)value; }
        }

        [ObjcPropAttribute("importsGraphics")]
        public virtual bool ImportsGraphics
        {
            get { return _cell.imports_graphics.ToBool(); }
            set
            {
                _cell.imports_graphics = Convert.ToUInt32(value);
                if (value)
                    _cell.is_rich_text = 1;
            }
        }

        [ObjcPropAttribute("allowsEditingTextAttributes")]
        public virtual bool AllowsEditingTextAttributes
        {
            get { return _cell.is_rich_text.ToBool(); }
            set
            {
                _cell.is_rich_text = Convert.ToUInt32(value);
                if (!value)
                    _cell.imports_graphics = 0;
            }
        }

        public virtual NSString Title
        {
            get
            {
                return this.StringValue;
            }
            set
            {
                this.StringValue = value;
            }
        }

        [ObjcPropAttribute("formatter")]
        public virtual NSFormatter Formatter
        {
            get { return _formatter; }
            set { _formatter = value; }
        }



        public NSCell()
        {

        }

        #region Initializing a Cell

        [ObjcMethodAttribute("init")]
        public override id init()
        {
            return initTextCell((NSString)"");
        }

        [ObjcMethodAttribute("initWithCoder")]
        public override id initWithCoder(NSCoder aDecoder)
        {

            if (aDecoder.AllowsKeyedCoding)
            {
                id contents = (id)aDecoder.decodeObjectForKey("NSContents");

                //In objc messages can be send to nil object - so in this case I had to add a 
                // test to check if contents variable is null or not
                // NSPopUpButtonCell doesn't have NSContents object
                if (contents != null && contents.isKindOfClass(NSString.Class))
                {
                    initTextCell((NSString)contents);
                }
                else if (contents != null && contents.isKindOfClass(NSImage.Class))
                {
                    initImageCell((NSImage)contents);
                }
                else
                {
                    init();
                    this.ObjectValue = contents;
                }

                if (aDecoder.containsValueForKey("NSCellFlags"))
                {
                    uint mask = 0;
                    uint cFlags = (uint)aDecoder.decodeIntForKey("NSCellFlags");

                    FocusRingType = (NSFocusRingType)(cFlags & 0x3);
                    ShowsFirstResponder = ((cFlags & 0x4) == 0x4);
                    if ((cFlags & 0x20) != 0x20) { mask |= (uint)NSEventMask.NSLeftMouseUpMask; }
                    Wraps = ((cFlags & 0x40) != 0x40);
                    if ((cFlags & 0x100) == 0x100)
                        mask |= (uint)NSEventMask.NSLeftMouseDraggedMask;
                    if ((cFlags & 0x40000) == 0x40000)
                        mask |= (uint)NSEventMask.NSLeftMouseDownMask;
                    if ((cFlags & 0x80000) == 0x80000)
                        mask |= (uint)NSEventMask.NSPeriodicMask;
                    this.SendActionOn((int)mask);

                    this.Scrollable = ((cFlags & 0x100000) == 0x100000);
                    this.Selectable = ((cFlags & 0x200000) == 0x200000);
                    this.Bezeled = ((cFlags & 0x400000) == 0x400000);
                    this.Bordered = ((cFlags & 0x800000) == 0x800000);
                    this.Type = (NSCellType)((cFlags & 0xC000000) >> 26);
                    this.Editable = ((cFlags & 0x10000000) == 0x10000000);
                    this.Enabled = ((cFlags & 0x20000000) != 0x20000000);
                    this.Highlighted = ((cFlags & 0x40000000) == 0x40000000);
                    this.State = (int)(((cFlags & 0x80000000) == 0x80000000) ? NSCellStateValue.NSOnState : NSCellStateValue.NSOffState);
                }

                if (aDecoder.containsValueForKey("NSCellFlags2"))
                {
                    int cFlags2 = aDecoder.decodeIntForKey("NSCellFlags2");

                    this.ControlTint = (NSControlTint)((cFlags2 & 0xE0) >> 5);
                    this.LineBreakMode = (NSLineBreakMode)((cFlags2 & 0xE00) >> 9);
                    this.ControlSize = (NSControlSize)((cFlags2 & 0xE0000) >> 17);
                    this.SendsActionOnEndEditing = ((cFlags2 & 0x400000) == 0x400000);
                    this.AllowsMixedState = ((cFlags2 & 0x1000000) == 0x1000000);
                    this.RefusesFirstResponder = ((cFlags2 & 0x2000000) == 0x2000000);
                    this.Alignment = (NSTextAlignment)((cFlags2 & 0x1C000000) >> 26);
                    this.ImportsGraphics = ((cFlags2 & 0x20000000) == 0x20000000);
                    this.AllowsEditingTextAttributes = ((cFlags2 & 0x40000000) == 0x40000000);
                }

                if (aDecoder.containsValueForKey("NSSupport"))
                {

                    id support = aDecoder.decodeObjectForKey("NSSupport");

                    if (support.isKindOfClass(NSFont.Class))
                    {
                        this.Font = (NSFont)support;
                    }
                    else if (support.isKindOfClass(NSImage.Class))
                    {
                        this.Image = (NSImage)support;
                    }
                }

                if (aDecoder.containsValueForKey("NSFormatter"))
                {
                    NSFormatter formatter = (NSFormatter)aDecoder.decodeObjectForKey("NSFormatter");
                    Formatter = formatter;
                }
            }
            return this;
        }

        [ObjcMethodAttribute("initTextCell")]
        public virtual id initTextCell(NSString aString)
        {
            id self = this;

            _cell.type = (uint)NSCellType.NSTextCellType;
            _contents = aString;
            _font = NSFont.SystemFontOfSize(-1);
            _action_mask = (uint)NSEventMask.NSLeftMouseUpMask;
            //_menu = [object_getClass(self) defaultMenu];
            //[self setFocusRingType: [object_getClass(self) defaultFocusRingType]];

            return self;
        }

        [ObjcMethodAttribute("initImageCell")]
        public virtual id initImageCell(NSImage anImage)
        {
            id self = this;

            _cell.type = (uint)NSCellType.NSImageCellType;
            _cell_image = anImage;
            _cell.image_position = (uint)NSCellImagePosition.NSImageOnly;
            _font = NSFont.SystemFontOfSize(-1);
            _action_mask = (uint)NSEventMask.NSLeftMouseUpMask;
            //_menu = [object_getClass(self) defaultMenu];
            //[self setFocusRingType: [object_getClass(self) defaultFocusRingType]];

            return self;
        }

        #endregion


        public int CellAttribute(NSCellAttribute aParameter)
        {
            switch (aParameter)
            {
                case NSCellAttribute.NSCellDisabled: return (int)_cell.is_disabled;
                case NSCellAttribute.NSCellState: return _cell.state;
                case NSCellAttribute.NSCellEditable: return (int)_cell.is_editable;
                case NSCellAttribute.NSCellHighlighted: return (int)_cell.is_highlighted;
                case NSCellAttribute.NSCellIsBordered: return (int)_cell.is_bordered;
                case NSCellAttribute.NSCellAllowsMixedState: return (int)_cell.allows_mixed_state;

                /*
                  case NSPushInCell: return 0; 
                  case NSChangeGrayCell: return 0; 
                  case NSCellLightsByContents: return 0; 
                  case NSCellLightsByGray: return 0; 
                  case NSChangeBackgroundCell: return 0; 
                  case NSCellLightsByBackground: return 0; 
                  case NSCellChangesContents: return 0;  
                  case NSCellIsInsetButton: return 0;  
                */
                case NSCellAttribute.NSCellHasOverlappingImage:
                    {
                        return (_cell.image_position == (uint)NSCellImagePosition.NSImageOverlaps).ToInt();
                    }
                case NSCellAttribute.NSCellHasImageHorizontal:
                    {
                        return ((_cell.image_position == (uint)NSCellImagePosition.NSImageRight)
                               || (_cell.image_position == (uint)NSCellImagePosition.NSImageLeft)).ToInt();
                    }
                case NSCellAttribute.NSCellHasImageOnLeftOrBottom:
                    {
                        return ((_cell.image_position == (uint)NSCellImagePosition.NSImageBelow)
                                || (_cell.image_position == (uint)NSCellImagePosition.NSImageLeft)).ToInt();
                    }
                default:
                    {
                        //NSWarnLog (@"cell attribute %d not supported", (int)aParameter);
                        break;
                    }
            }

            return 0;
        }

        public virtual void SetCellAttribute(NSCellAttribute aParameter, int toValue)
        {
            switch (aParameter)
            {
                case NSCellAttribute.NSCellDisabled:
                    {
                        _cell.is_disabled = (uint)toValue;
                        break;
                    }
                case NSCellAttribute.NSCellState:
                    {
                        _cell.state = toValue;
                        break;
                    }
                case NSCellAttribute.NSCellEditable:
                    {
                        _cell.is_editable = (uint)toValue;
                        break;
                    }
                case NSCellAttribute.NSCellHighlighted:
                    {
                        _cell.is_highlighted = (uint)toValue;
                        break;
                    }
                case NSCellAttribute.NSCellHasOverlappingImage:
                    {
                        if (Convert.ToBoolean(toValue))
                        {
                            _cell.image_position = (uint)NSCellImagePosition.NSImageOverlaps;
                        }
                        else
                        {
                            if (_cell.image_position == (uint)NSCellImagePosition.NSImageOverlaps)
                            {
                                _cell.image_position = (uint)NSCellImagePosition.NSImageLeft;
                            }
                        }
                        break;
                    }
                case NSCellAttribute.NSCellHasImageHorizontal:
                    {
                        if (Convert.ToBoolean(toValue))
                        {
                            if (_cell.image_position != (uint)NSCellImagePosition.NSImageLeft &&
                                _cell.image_position != (uint)NSCellImagePosition.NSImageRight)
                            {
                                _cell.image_position = (uint)NSCellImagePosition.NSImageLeft;
                            }
                        }
                        else
                        {
                            if (_cell.image_position == (uint)NSCellImagePosition.NSImageLeft)
                            {
                                _cell.image_position = (uint)NSCellImagePosition.NSImageAbove;
                            }
                            else if (_cell.image_position == (uint)NSCellImagePosition.NSImageRight)
                            {
                                _cell.image_position = (uint)NSCellImagePosition.NSImageBelow;
                            }
                        }
                        break;
                    }
                case NSCellAttribute.NSCellHasImageOnLeftOrBottom:
                    {
                        if (Convert.ToBoolean(toValue))
                        {
                            if (_cell.image_position == (uint)NSCellImagePosition.NSImageAbove)
                            {
                                _cell.image_position = (uint)NSCellImagePosition.NSImageBelow;
                            }
                            else
                            {
                                _cell.image_position = (uint)NSCellImagePosition.NSImageLeft;
                            }
                        }
                        else
                        {
                            if (_cell.image_position == (uint)NSCellImagePosition.NSImageBelow)
                            {
                                _cell.image_position = (uint)NSCellImagePosition.NSImageAbove;
                            }
                            else
                            {
                                _cell.image_position = (uint)NSCellImagePosition.NSImageRight;
                            }
                        }
                        break;
                    }
                /*
              case NSCellChangesContents:
                _cell. = value;
                break;
              case NSCellIsInsetButton:
                _cell. = value;
                break;
          */
                case NSCellAttribute.NSCellIsBordered:
                    {
                        _cell.is_bordered = (uint)toValue;
                        break;
                    }
                case NSCellAttribute.NSCellAllowsMixedState:
                    {
                        _cell.allows_mixed_state = (uint)toValue;
                        break;
                    }
                default:
                    {
                        //NSWarnLog(@"cell attribute %d not supported", (int)aParameter);
                        break;
                    }
            }
        }

    }
}
