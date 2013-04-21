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
    public enum NSTextFieldBezelStyle
    {
        NSTextFieldSquareBezel = 0,
        NSTextFieldRoundedBezel
    }


    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSTextFieldCell_Class/Reference/Reference.html
    //
    public class NSTextFieldCell : NSActionCell
    {
        // Attributes
        protected NSColor _background_color;
        protected NSColor _text_color;
        protected NSTextFieldBezelStyle _bezelStyle;

        // Think of the following ones as of two BOOL ivars
        //#define _textfieldcell_draws_background _cell.subclass_bool_one
        //#define _textfieldcell_placeholder_is_attributed_string _cell.subclass_bool_three
        protected id _placeholder;


        [ObjcPropAttribute("backgroundColor")]
        public virtual NSColor BackgroundColor
        {
            get { return _background_color; }
            set
            {
                _background_color = value;
                _UpdateCell();
            }
        }

        [ObjcPropAttribute("textColor")]
        public virtual NSColor TextColor
        {
            get { return _text_color; }
            set
            {
                _text_color = value;
                _UpdateCell();
            }
        }

        [ObjcPropAttribute("bezelStyle")]
        public virtual NSTextFieldBezelStyle BezelStyle
        {
            get { return _bezelStyle; }
            set { _bezelStyle = value; }
        }

        [ObjcPropAttribute("drawsBackground")]
        public virtual bool DrawsBackground
        {
            get { return Convert.ToBoolean(_cell.subclass_bool_one); }
            set { _cell.subclass_bool_one = Convert.ToUInt32(value); }
        }

        [ObjcPropAttribute("placeholderString")]
        public virtual NSString PlaceholderString
        {
            get 
            {
                return (_cell.subclass_bool_three == 1) ? null : (NSString)_placeholder;
            }
            set 
            { 
                _placeholder = value;
                _cell.subclass_bool_three = 0; 
            }
        }

        [ObjcPropAttribute("placeholderAttributedString")]
        public virtual NSAttributedString PlaceholderAttributedString
        {
            get
            {
                return (_cell.subclass_bool_three == 1) ? (NSAttributedString)_placeholder : null;
            }
            set
            {
                _placeholder = value;
                _cell.subclass_bool_three = 1;
            }
        }


        public NSTextFieldCell()
        {

        }

        public override id InitTextCell(NSString aString)
        {
            id self = this;

             if (base.InitTextCell(aString) == null)
                return null;

            _text_color = NSColor.TextColor;
            _background_color = NSColor.TextBackgroundColor;
            //  _textfieldcell_draws_background = NO;
            _action_mask = (uint)(NSEventMask.NSKeyUpMask | NSEventMask.NSKeyDownMask);

            return self;
        }

        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                BackgroundColor = (NSColor)aDecoder.DecodeObjectForKey("NSBackgroundColor");
                TextColor = (NSColor)aDecoder.DecodeObjectForKey("NSTextColor");
            }

            return this;
        }


        public NSText SetUpFieldEditorAttributes(NSText textObject)
        {
            // FIXME
            //textObject = [super setUpFieldEditorAttributes: textObject];
            //[textObject setDrawsBackground: _cell.subclass_bool_one];
            //[textObject setBackgroundColor: _background_color];
            //[textObject setTextColor: _text_color];
            
            return textObject;
        }



        
        

    }
}
