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

    public class NSButton : NSControl
    {
        private NSButtonCell _cell;
        public NSButtonCell Cell 
        { 
            get { return _cell; } 
            protected set { _cell = value; } 
        }

        public NSButtonType ButtonType 
        { 
            // There is no getter and this is normal 
            set { _cell.ButtonType = value; } 
        }

        public int HighlightsBy
        {
            get { return _cell.HighlightsBy; }
            set { _cell.HighlightsBy = value; }
        }

        [ObjcPropAttribute("alternateTitle")]
        public int ShowsStateBy
        {
            get { return _cell.ShowsStateBy; }
            set { _cell.ShowsStateBy = value; }
        }

        [ObjcPropAttribute("alternateTitle")]
        public NSString AlternateTitle
        {
            get { return _cell.AlternateTitle; }
            set { _cell.AlternateTitle = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("title")]
        public NSString Title
        {
            get { return _cell.Title; }
            set { _cell.Title = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("alternateImage")]
        public NSImage AlternateImage
        {
            get { return _cell.AlternateImage; }
            set { _cell.AlternateImage = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("image")]
        public NSImage Image
        {
            get { return _cell.Image; }
            set { _cell.Image = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("imagePosition")]
        public NSCellImagePosition ImagePosition
        {
            get { return (NSCellImagePosition)_cell.ImagePosition; }
            set { _cell.ImagePosition = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("bordered", GetName = "isBordered")]
        public bool Bordered
        {
            get { return _cell.Bordered; }
            set { _cell.Bordered = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("transparent", GetName = "isTransparent")]
        public bool Transparent
        {
            get { return _cell.IsTransparent; }
            set { _cell.IsTransparent = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("bezelStyle")]
        public NSBezelStyle BezelStyle
        {
            get { return _cell.BezelStyle; }
            set { _cell.BezelStyle = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("showsBorderOnlyWhileMouseInside")]
        public bool ShowsBorderOnlyWhileMouseInside
        {
            get { return _cell.ShowsBorderOnlyWhileMouseInside; }
            set { _cell.ShowsBorderOnlyWhileMouseInside = value; this.SetNeedsDisplay(true); }
        }

        //
        // Setting the State
        //
        public int IntValue
        {
            set { this.State = value; }
        }

        public float FloatValue
        {
            set { this.State = (int)value; }
        }

        public double DoubleValue
        {
            set { this.State = (int)value; }
        }

        public int State
        {
            get { return _cell.ShowsStateBy; }
            set { _cell.State = value; this.SetNeedsDisplay(true); }
        }

        [ObjcPropAttribute("allowsMixedState")]
        public bool AllowsMixedState
        {
            get { return _cell.AllowsMixedState; }
            set { _cell.AllowsMixedState = value; this.SetNeedsDisplay(true); }
        }

        [ObjcMethod("getPeriodicDelay:interval")]
        public void GetPeriodicDelay(ref float delay, ref float interval)
        {
            //_cell.GetPeriodicDelay(ref delay, ref interval);
        }
		


       

        public NSButton()
        {

        }

        public override NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                _cell = (NSButtonCell)aDecoder.DecodeObjectForKey("NSCell");
            }

            return this;
        }


        
    }
}
