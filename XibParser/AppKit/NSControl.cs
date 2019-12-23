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
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSControl.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSControl.m
    public class NSControl : NSView, IAction
    {
        new public static Class Class = new Class(typeof(NSControl));
        
        private static Class usedCellClass;
        private static Class cellClass;
        private static Class actionCellClass;
        //protected static NSNotificationCenter nc;

        // Attributes
        protected int _tag;
        protected id _cell; // id so compiler wont complain too much for subclasses
        protected bool _ignoresMultiClick;


        static NSControl() { initialize(); }
        public static void initialize()
        {
            cellClass = NSCell.Class;
            usedCellClass = cellClass;
            actionCellClass = NSActionCell.Class;
        }

        public NSControl()
        {

        }

        public static Class CellClass
        {
            get { return cellClass; }
            set { usedCellClass = (value != null) ? value : cellClass; }
        }


        public NSCell Cell
        {
            get { return (NSCell)_cell; }
            set { _cell = value; }
        }

        public id SelectedCell
        {
            get { return _cell; }
        }

        [ObjcPropAttribute("enabled", GetName = "isEnabled")]
        public virtual bool Enabled
        {
            get { return ((NSCell)SelectedCell).Enabled; }
            set 
            {
                ((NSCell)SelectedCell).Enabled = true;
                if (!value)
                    this.AbortEditing();

                setNeedsDisplay(true);
            }
        }

        [ObjcPropAttribute("DoubleValue", SetName = null)]
        public virtual double DoubleValue
        {
            get { return ((NSCell)SelectedCell).DoubleValue; }
            set
            {
                NSCell selected = (NSCell)SelectedCell;
                bool wasEditing = AbortEditing();

                selected.DoubleValue = value;
                if (!selected.isKindOfClass(actionCellClass))
                    setNeedsDisplay(true);

                if (wasEditing)
                {
                    //FIXME 
                    //[[self window] makeFirstResponder: self];
                }
            }
        }

        [ObjcPropAttribute("FloatValue", SetName = null)]
        public virtual float FloatValue
        {
            get { return ((NSCell)SelectedCell).FloatValue; }
            set
            {
                NSCell selected = (NSCell)SelectedCell;
                bool wasEditing = AbortEditing();

                selected.FloatValue = value;
                if (!selected.isKindOfClass(actionCellClass))
                    setNeedsDisplay(true);

                if (wasEditing)
                {
                    //FIXME 
                    //[[self window] makeFirstResponder: self];
                }
            }
        }

        [ObjcPropAttribute("intValue", SetName = null)]
        public virtual int IntValue
        {
            get { return ((NSCell)SelectedCell).IntValue; }
            set
            {
                NSCell selected = (NSCell)SelectedCell;
                bool wasEditing = AbortEditing();

                selected.IntValue = value;
                if (!selected.isKindOfClass(actionCellClass))
                    setNeedsDisplay(true);

                if (wasEditing)
                {
                    //FIXME 
                    //[[self window] makeFirstResponder: self];
                }
            }
        }

        [ObjcPropAttribute("IntegerValue", SetName = null)]
        public virtual int IntegerValue
        {
            get { return ((NSCell)SelectedCell).IntValue; }
            set
            {
                NSCell selected = (NSCell)SelectedCell;
                bool wasEditing = AbortEditing();

                selected.IntegerValue = value;
                if (!selected.isKindOfClass(actionCellClass))
                    setNeedsDisplay(true);

                if (wasEditing)
                {
                    //FIXME 
                    //[[self window] makeFirstResponder: self];
                }
            }
        }

        
        [ObjcPropAttribute("stringValue", SetName = null)]
        public virtual NSString StringValue
        {
            get { return ((NSCell)SelectedCell).StringValue; }
            set
            {
                NSCell selected = (NSCell)SelectedCell;
                bool wasEditing = AbortEditing();

                selected.StringValue = value;
                if (!selected.isKindOfClass(actionCellClass))
                    setNeedsDisplay(true);

                if (wasEditing)
                {
                    //FIXME 
                    //[[self window] makeFirstResponder: self];
                }
            }
        }

        [ObjcPropAttribute("ObjectValue")]
        public virtual object ObjectValue
        {
            get { return ((NSCell)SelectedCell).ObjectValue; }
            set 
            {
                NSCell selected = (NSCell)SelectedCell;
                bool wasEditing = AbortEditing();

                selected.ObjectValue = value;
                if (!selected.isKindOfClass(actionCellClass))
                    setNeedsDisplay(true);

                if (wasEditing)
                {
                    //FIXME 
                    //[[self window] makeFirstResponder: self];
                }
            }
        }

        public virtual void SetNeedsDisplay()
        {
            base.setNeedsDisplay(true);
        }


        public virtual NSTextAlignment Alignment
        {
            get 
            {
                if (_cell != null)
                    return (((NSCell)_cell).Alignment);
                else
                    return NSTextAlignment.NSNaturalTextAlignment;
            }
            set 
            {
                if (_cell != null)
                {
                    AbortEditing();

                    ((NSCell)_cell).Alignment = value;
                    if (!((NSCell)_cell).isKindOfClass(actionCellClass))
                        setNeedsDisplay(true);
                } 
            }
        }

        [ObjcPropAttribute("Font")]
        public virtual NSFont Font
        {
            get 
            { 
                return (_cell != null) ? ((NSCell)_cell).Font : null; 
            }
            set
            {
                if (_cell != null)
                {
                    NSText editor = this.CurrentEditor;
                    ((NSCell)_cell).Font = value;
                    if (editor != null)
                    {
                        //FIXME
                        //editor.Font = value;
                    }
                }
            }
        }

        [ObjcPropAttribute("formatter")]
        public virtual NSFormatter Formatter
        {
            get { return ((NSCell)_cell).Formatter; }
            set 
            {
                if (_cell != null)
                {
                    ((NSCell)_cell).Formatter = value;
                    if (!((NSCell)_cell).isKindOfClass(actionCellClass))
                        setNeedsDisplay(true);
                } 
            }
        }

        [ObjcPropAttribute("baseWritingDirection")]
        public virtual NSWritingDirection BaseWritingDirection
        {
            get { return ((NSCell)_cell).BaseWritingDirection; }
            set
            {
                if (_cell != null)
                {
                    ((NSCell)_cell).BaseWritingDirection = value;
                    if (!((NSCell)_cell).isKindOfClass(actionCellClass))
                        setNeedsDisplay(true);
                } 
            }
        }

        public virtual bool AbortEditing()
        {
            NSText text;

            text = this.CurrentEditor;
            if (text == null)
            {
                return false;
            }

            //((NSCell)SelectedCell).EndEditing(text);
            
            return true;
        }

        public virtual NSText CurrentEditor
        {
            get
            {
                if (_cell != null)
                {
                    //FIXME
                }

                return null;
            }
        }

        public virtual void ValidateEditing()
        {
            //FIXME
        }

        public virtual void TextDidBeginEditing(NSNotification aNotification)
        {
            //FIXME
        }

        public virtual void TextDidChange(NSNotification aNotification)
        {
            //FIXME
        }

        public virtual void TextDidEndEditing(NSNotification aNotification)
        {
            //FIXME
        }

        public virtual void CalcSize()
        {
            //FIXME
            //[_cell calcDrawInfo: [self bounds]];
        }

        public virtual void SizeToFit()
        {
            //FIXME
            //[self setFrameSize: [_cell cellSize]];
        }


        [ObjcProp("opaque", GetName = "isOpaque", SetName = null)]
        public override bool Opaque
        {
            get
            {
                return ((NSCell)_cell).Opaque;
            }
        }


        public override void DrawRect(NSRect aRect)
        {
            //FIXME
            //[self drawCell: _cell];
        }


        public virtual void DrawCell(NSCell aCell)
        {
            if (((NSCell)_cell).Equals(aCell))
            {
                //FIXME
                //[_cell drawWithFrame: _bounds inView: self];
                //((NSCell)_cell).DrawWithFrame(_bounds, this);
            }
        }

        public virtual void DrawCellInside(NSCell aCell)
        {
            if (((NSCell)_cell).Equals(aCell))
            {
                //FIXME
                //[_cell drawInteriorWithFrame: _bounds inView: self];
                //((NSCell)_cell).DrawInteriorWithFrame(_bounds, this);
            }
        }

         public virtual void SelectCell(NSCell aCell)
         {
             if (((NSCell)_cell).Equals(aCell))
             {
                 ((NSCell)_cell).State = (int)NSCellStateValue.NSOnState;
                 setNeedsDisplay(true);
             }
         }
        
         public virtual void UpdateCell(NSCell aCell)
         {
             setNeedsDisplay(true);
         }

         public virtual void UpdateCellInside(NSCell aCell)
         {
             setNeedsDisplay(true);
         }

         [ObjcPropAttribute("action")]
         public virtual SEL Action
         {
             get { return ((NSCell)_cell).Action; }
             set { ((NSCell)_cell).Action = value; }
         }


         [ObjcPropAttribute("continuous", GetName = "isContinuous")]
         public virtual bool Continuous
         {
             get { return ((NSCell)_cell).Continuous; }
             set { ((NSCell)_cell).Continuous = value; }
         }

         [ObjcPropAttribute("target")]
         public virtual id Target
         {
             get { return ((NSCell)_cell).Target; }
             set { ((NSCell)_cell).Target = value; }
         }

         [ObjcPropAttribute("attributedStringValue")]
         public virtual NSAttributedString AttributedStringValue
         {
             get 
             { 
                 //FIXME
                 return null; 
             }
             set 
             { 
                 //FIXME
             }
         }

         [ObjcPropAttribute("tag")]
         public virtual int Tag
         {
             get { return _tag; }
             set { _tag = value; }
         }

         public virtual void performClick(id sender)
         {
             //FIXME
         }

         [ObjcPropAttribute("refusesFirstResponder")]
         public virtual bool RefusesFirstResponder
         {
             get { return ((NSCell)SelectedCell).RefusesFirstResponder; }
             set { ((NSCell)SelectedCell).RefusesFirstResponder = value; }
         }

        [ObjcPropAttribute("acceptsFirstResponder", SetName=null)]
         public virtual bool AcceptsFirstResponder
         {
             get { return ((NSCell)SelectedCell).AcceptsFirstResponder; }
         }

        public virtual void MouseDown(NSEvent theEvent)
        {
            //FIXME
        }

        
        public virtual bool ShouldBeTreatedAsInkEvent(NSEvent theEvent)
        {
            return false;
        }

        public override void ResetCursorRects()
        {
            //FIXME
            //[_cell resetCursorRect: _bounds inView: self];
        }

        [ObjcPropAttribute("ignoresMultiClick")]
        public virtual bool IgnoresMultiClick
        {
            get { return _ignoresMultiClick; }
            set { _ignoresMultiClick = value; }
        }

        [ObjcPropAttribute("mouseDownFlags")]
        public virtual int MouseDownFlags
        {
            get { return ((NSCell)SelectedCell).MouseDownFlags; }
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.initWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {
                NSCell cell = (NSCell)aDecoder.decodeObjectForKey("NSCell");

                if (cell != null)
                {
                    Cell = cell; 
                }
                else
                {
                    // This is needed for subclasses without cells, like NSColorWeel
                    // as we store some properties only on the cell.
                    
                    //FIXME
                    Cell = (NSCell)NSCell.alloc().init(); 
                }
                if (aDecoder.containsValueForKey("NSEnabled"))
                {
                    this.Enabled = aDecoder.decodeBoolForKey("NSEnabled");
                }
                if (aDecoder.containsValueForKey("NSTag"))
                {
                    this.Tag = aDecoder.decodeIntForKey("NSTag");
                }
            }

            return self;
        }
    }
}
