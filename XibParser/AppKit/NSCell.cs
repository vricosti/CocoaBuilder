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

    public class NSCell : NSObject
    {
        public NSCellType CellType { get; set; }

        public object Contents { get; set; }

        public NSImage Image { get; set; }

        public NSFocusRingType FocusRingType { get; set; }

        public bool ShowsFirstResponder { get; set; }

        public bool Wraps { get; set; }

        public bool Scrollable { get; set; }

        public bool Selectable { get; set; }

        public bool Bezeled { get; set; }

        public bool Bordered { get; set; }

        public bool Editable { get; set; }

        public bool Enabled { get; set; }

        public bool Highlighted { get; set; }

        //public NSOnState State { get; set; }

        public NSControlTint ControlTint { get; set; }

        public NSLineBreakMode LineBreakMode { get; set; }

        public NSControlSize ControlSize { get; set; }

        public bool SendsActionOnEndEditing { get; set; }

        public bool AllowsMixedState { get; set; }

        public bool RefusesFirstResponder { get; set; }

        public NSTextAlignment TextAlignment { get; set; }

        public bool ImportsGraphics { get; set; }

        public bool AllowsEditingTextAttributes { get; set; }

        public NSCell()
        {

        }

        public override NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            /*id*/
            object contents = aDecoder.DecodeObjectForKey("NSContents");
            if (contents is NSString)
            {
                InitTextCell((NSString)contents);
            }
            else if (contents is NSImage)
            {
                InitImageCell((NSImage)contents);
            }
            else
            {

            }

            if (aDecoder.ContainsValueForKey("NSCellFlags"))
            {
                uint mask = 0;
                uint cFlags = (uint)aDecoder.DecodeIntForKey("NSCellFlags");

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
                this.CellType = (NSCellType)((cFlags & 0xC000000) >> 26);
                this.Editable = ((cFlags & 0x10000000) == 0x10000000);
                this.Enabled = ((cFlags & 0x20000000) != 0x20000000);
                this.Highlighted = ((cFlags & 0x40000000) == 0x40000000);
                //this.State = ((cFlags & 0x80000000) == 0x80000000) ? NSOnState : NSOffState;
            }

            if (aDecoder.ContainsValueForKey("NSCellFlags2"))
            {
                int cFlags2 = aDecoder.DecodeIntForKey("NSCellFlags2");

                this.ControlTint = (NSControlTint)((cFlags2 & 0xE0) >> 5);
                this.LineBreakMode = (NSLineBreakMode)((cFlags2 & 0xE00) >> 9);
                this.ControlSize = (NSControlSize)((cFlags2 & 0xE0000) >> 17);
                this.SendsActionOnEndEditing = ((cFlags2 & 0x400000) == 0x400000);
                this.AllowsMixedState = ((cFlags2 & 0x1000000) == 0x1000000);
                this.RefusesFirstResponder = ((cFlags2 & 0x2000000) == 0x2000000);
                this.TextAlignment = (NSTextAlignment)((cFlags2 & 0x1C000000) >> 26);
                this.ImportsGraphics = ((cFlags2 & 0x20000000) == 0x20000000);
                this.AllowsEditingTextAttributes = ((cFlags2 & 0x40000000) == 0x40000000);
            }

            if (aDecoder.ContainsValueForKey("NSSupport"))
            {
                /*id*/object support = aDecoder.DecodeObjectForKey("NSSupport");

                //if (support is NSFont)
                //{
                //    //[self setFont: support];
                //}
                //else if (support is NSImage)
                //{
                //    //[self setImage: support];
                //}
            }

            if (aDecoder.ContainsValueForKey("NSFormatter"))
            {
                //NSFormatter *formatter = [aDecoder decodeObjectForKey: @"NSFormatter"];

                //[self setFormatter: formatter];
            }

            return this;
        }

        public /*id*/object InitTextCell(NSString aString)
        {
            this.CellType = NSCellType.NSTextCellType;
            this.Contents = aString;
 
            //_cell.type = NSTextCellType;
            //_contents = RETAIN (aString);
            //_font = RETAIN ([fontClass systemFontOfSize: 0]);

            //// Implicitly set by allocation:
            ////
            ////_cell.contents_is_attributed_string = NO;
            ////_cell_image = nil;
            ////_cell.image_position = NSNoImage;
            ////_cell.is_disabled = NO;
            ////_cell.state = 0;
            ////_cell.is_highlighted = NO;
            ////_cell.is_editable = NO;
            ////_cell.is_bordered = NO;
            ////_cell.is_bezeled = NO;
            ////_cell.is_scrollable = NO;
            ////_cell.is_selectable = NO;
            ////_cell.line_break_mode = NSLineBreakByWordWrapping;
            //_action_mask = NSLeftMouseUpMask;
            //_menu = [object_getClass(self) defaultMenu];
            //[self setFocusRingType: [object_getClass(self) defaultFocusRingType]];

            return this;
        }

        public /*id*/object InitImageCell(NSImage aImage)
        {
            return this;
        }


        public int SendActionOn(int aMask)
        {
            return 0;
        }
    }
}
