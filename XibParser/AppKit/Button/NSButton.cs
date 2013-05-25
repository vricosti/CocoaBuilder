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
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSButton_Class/Reference/Reference.html
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSButton.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSButton.m
    public class NSButton : NSControl
    {
        new public static Class Class = new Class(typeof(NSButton));
        new public static NSButton Alloc() { return new NSButton(); }

        private static Class buttonCellClass;


        static NSButton() { Initialize(); }
        new static void Initialize()
        {
            NSButton.CellClass = NSButtonCell.Class;
        }

        new public static Class CellClass
        {
            get { return buttonCellClass; }
            set { buttonCellClass = value; }
        }

        
        public NSButtonType ButtonType 
        { 
            // There is no getter and this is normal 
            set { ((NSButtonCell)_cell).ButtonType = value; } 
        }

        public int HighlightsBy
        {
            get { return ((NSButtonCell)_cell).HighlightsBy; }
            set { ((NSButtonCell)_cell).HighlightsBy = value; }
        }

        [ObjcPropAttribute("alternateTitle")]
        public int ShowsStateBy
        {
            get { return ((NSButtonCell)_cell).ShowsStateBy; }
            set { ((NSButtonCell)_cell).ShowsStateBy = value; }
        }

        [ObjcPropAttribute("alternateTitle")]
        public NSString AlternateTitle
        {
            get { return ((NSButtonCell)_cell).AlternateTitle; }
            set { ((NSButtonCell)_cell).AlternateTitle = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("title")]
        public NSString Title
        {
            get { return ((NSButtonCell)_cell).Title; }
            set { ((NSButtonCell)_cell).Title = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("alternateImage")]
        public NSImage AlternateImage
        {
            get { return ((NSButtonCell)_cell).AlternateImage; }
            set { ((NSButtonCell)_cell).AlternateImage = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("image")]
        public NSImage Image
        {
            get { return ((NSButtonCell)_cell).Image; }
            set { ((NSButtonCell)_cell).Image = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("imagePosition")]
        public NSCellImagePosition ImagePosition
        {
            get { return (NSCellImagePosition)((NSButtonCell)_cell).ImagePosition; }
            set { ((NSButtonCell)_cell).ImagePosition = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("bordered", GetName = "isBordered")]
        public bool Bordered
        {
            get { return ((NSButtonCell)_cell).Bordered; }
            set { ((NSButtonCell)_cell).Bordered = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("transparent", GetName = "isTransparent")]
        public bool Transparent
        {
            get { return ((NSButtonCell)_cell).Transparent; }
            set { ((NSButtonCell)_cell).Transparent = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("bezelStyle")]
        public NSBezelStyle BezelStyle
        {
            get { return ((NSButtonCell)_cell).BezelStyle; }
            set { ((NSButtonCell)_cell).BezelStyle = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("showsBorderOnlyWhileMouseInside")]
        public bool ShowsBorderOnlyWhileMouseInside
        {
            get { return ((NSButtonCell)_cell).ShowsBorderOnlyWhileMouseInside; }
            set { ((NSButtonCell)_cell).ShowsBorderOnlyWhileMouseInside = value; this.SetNeedsDisplay(true); }
        }

        //
        // Setting the State
        //
        public override int IntValue
        {
            set { this.State = value; }
        }

        public override float FloatValue
        {
            set { this.State = (int)value; }
        }

        public override double DoubleValue
        {
            set { this.State = (int)value; }
        }

        public int State
        {
            get { return ((NSButtonCell)_cell).ShowsStateBy; }
            set { ((NSButtonCell)_cell).State = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("allowsMixedState")]
        public bool AllowsMixedState
        {
            get { return ((NSButtonCell)_cell).AllowsMixedState; }
            set { ((NSButtonCell)_cell).AllowsMixedState = value; this.SetNeedsDisplay(true); }
        }

        [ObjcMethod("getPeriodicDelay:interval")]
        public void GetPeriodicDelay(ref float delay, ref float interval)
        {
            ((NSButtonCell)_cell).GetPeriodicDelay(ref delay, ref interval);
        }
		


       

        public NSButton()
        {

        }

        //public override id InitWithCoder(NSCoder aDecoder)
        //{
        //    base.InitWithCoder(aDecoder);

        //    if (aDecoder.AllowsKeyedCoding)
        //    {
        //        _cell = (NSButtonCell)aDecoder.DecodeObjectForKey("NSCell");
        //    }

        //    return this;
        //}


        
    }
}
