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
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSTextField.m
    public class NSTextField : NSControl
    {
        new public static Class Class = new Class(typeof(NSTextField));
        new public static NSTextField alloc() { return new NSTextField(); }

        private static Class usedCellClass;
        private static Class textFieldCellClass;

        protected id _delegate;
        protected SEL _error_action;
        protected NSText _text_object;


        static NSTextField() { initialize(); }
        new static void initialize()
        {
            //[self setVersion: 1];
            textFieldCellClass = NSTextFieldCell.Class;
            usedCellClass = textFieldCellClass;
            //nc = [NSNotificationCenter defaultCenter];

           //[self exposeBinding: NSEditableBinding];
           //[self exposeBinding: NSTextColorBinding];
        }

        new public static Class CellClass
        {
            get { return usedCellClass; }
            set 
            {
                Class factoryId = value;
                usedCellClass = factoryId != null ? factoryId : textFieldCellClass; 
            }
        }

        public NSTextField()
        {

        }

        internal virtual bool  _isFlipped()
        {
            return true;
        }

        internal virtual bool  _IsEditable()
        {
          return ((NSCell)_cell).Editable;
        }

        internal virtual bool _IsSelectable()
        {
            return ((NSCell)_cell).Selectable;
        }

        internal virtual void _SetEditable(bool flag)
        {
            ((NSCell)_cell).Editable = flag;
            if (_text_object != null)
                _text_object.Editable = flag;
        }

        internal virtual void _SetSelectable(bool flag)
        {
            ((NSCell)_cell).Selectable = flag;
            if (_text_object != null)
                _text_object.Selectable = flag;
        }

        public virtual void SelectText(id sender)
        {
            //FIXME
        }

        internal virtual id _NextText()
        {
            return this.NextKeyView;
        }

        internal virtual id _PreviousText()
        {
            return this.PreviousKeyView;
        }

        internal virtual void _SetNextText(id anObject)
        {
            this.NextKeyView = anObject;
        }

        internal virtual void _SetPreviousText(id anObject)
        {
            this.PreviousKeyView = anObject;
        }

        internal virtual void _SetDelegate(id anObject)
        {
            _delegate = anObject;
        }

        internal virtual id _Delegate()
        {
            return _delegate;
        }




        public override id initWithFrame(NSRect frameRect)
        {
            id self = this;

            if (base.initWithFrame(frameRect) == null)
                return null;

            ((NSCell)_cell).Bezeled = true;
            ((NSCell)_cell).Selectable = true;
            ((NSCell)_cell).Editable = true;
            
            //FIXME
            //((NSCell)_cell).DrawsBackground = true;
            _text_object = null;

            return self;
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            base.initWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                // do nothing for now...
            }
            else
            {
                //this._setDelegate(aDecoder.decodeObject);
                //[aDecoder decodeValueOfObjCType: @encode(SEL) at: &_error_action];
                aDecoder.decodeValueOfObjCType2<SEL>(out _error_action);
            }
            _text_object = null;

            return this;
        }


    }
}
