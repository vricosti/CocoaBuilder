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
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSSegmentedControl.m
    public class NSSegmentedControl : NSControl
    {
        new public static Class Class = new Class(typeof(NSSegmentedControl));
        new public static NSSegmentedControl alloc() { return new NSSegmentedControl(); }

        private static Class segmentedControlCellClass;


        static NSSegmentedControl() { initialize(); }
        new static void initialize()
        {
            segmentedControlCellClass = NSSegmentedCell.Class;
        }


        new public static Class CellClass
        {
            get { return segmentedControlCellClass; }
        }

        // Specifying number of segments...
        public virtual int SegmentCount
        {
            get { return ((NSSegmentedCell)_cell).SegmentCount; }
            set { ((NSSegmentedCell)_cell).SegmentCount = value; }
        }

        public virtual int SelectedSegment
        {
            get { return ((NSSegmentedCell)_cell).SelectedSegment; }
            set { ((NSSegmentedCell)_cell).SelectedSegment = value; }
        }

        public virtual void SelectSegmentWithTag(int tag)
        {
            ((NSSegmentedCell)_cell).SelectSegmentWithTag(tag);
        }

        public virtual void SetWidthForSegment(float width, int seg)
        {
            ((NSSegmentedCell)_cell).SetWidthForSegment(width, seg);
        }

        public virtual float WidthForSegment(int seg)
        {
            return ((NSSegmentedCell)_cell).WidthForSegment(seg);
        }

        public virtual void SetImageForSegment(NSImage image, int seg)
        {
            ((NSSegmentedCell)_cell).SetImageForSegment(image, seg);
        }

        public virtual NSImage ImageForSegment(int seg)
        {
            return ((NSSegmentedCell)_cell).ImageForSegment(seg);
        }

        public virtual void SetLabelForSegment(NSString label, int seg)
        {
            ((NSSegmentedCell)_cell).SetLabelForSegment(label, seg);
        }

        public virtual NSString LabelForSegment(int seg)
        {
            return ((NSSegmentedCell)_cell).LabelForSegment(seg);
        }

        public virtual bool IsSelectedForSegment(int seg)
        {
            return ((NSSegmentedCell)_cell).IsSelectedForSegment(seg);
        }

        public virtual void SetEnabledForSegment(bool flag, int seg)
        {
            ((NSSegmentedCell)_cell).SetEnabledForSegment(flag, seg);
        }

        public virtual bool IsEnabledForSegment(int seg)
        {
            return ((NSSegmentedCell)_cell).IsEnabledForSegment(seg);
        }

        public virtual void SetMenuForSegment(NSMenu menu, int seg)
        {
            ((NSSegmentedCell)_cell).SetMenuForSegment(menu, seg);
        }

        public virtual NSMenu MenuForSegment(int seg)
        {
            return ((NSSegmentedCell)_cell).MenuForSegment(seg);
        }

        public virtual NSSegmentStyle SegmentStyle
        {
            get { return ((NSSegmentedCell)_cell).SegmentStyle; }
            set { ((NSSegmentedCell)_cell).SegmentStyle = value; }
        }

#if ONE_DAY
        public virtual void setSegmentCount(int  count)
        {
            ((NSSegmentedCell)_cell).SegmentCount = count;
        }

        public virtual int segmentCount()
        {
            return ((NSSegmentedCell)_cell).SegmentCount;
        } 

        // Specifying selected segment...

        public virtual void setSelectedSegment(int  segment)
        {
            [_cell setSelectedSegment: segment];
        }

        public virtual int SelectedSegment()
        {
          return [_cell selectedSegment];
        }
        
        public virtual void SelectSegmentWithTag(int  tag)
        {
          [_cell selectSegmentWithTag: tag];
        }

        // Working with individual segments...
        public virtual void SetWidthForSegment(double width, int segment)
        {
          [_cell setWidth: width forSegment: segment];
        }
        
        public virtual double GetWidthForSegment(int segment)
        {
          return [_cell widthForSegment: segment];
        }
        
        public virtual void SetImageForSegment(NSImage image, int segment)
        {
          [_cell setImage: image forSegment: segment];
        }
        
        public virtual NSImage  GetImageForSegment(int segment)
        {
          return [_cell imageForSegment: segment];
        }
        
        public virtual void SetLabelForSegment(NSString label, int segment)
        {
          [_cell setLabel: label forSegment: segment];
        }
        
        public virtual NSString GetLabelForSegment(int segment)
        {
          return [_cell labelForSegment: segment];
        }
        
        public virtual void SetMenuForSegment(NSMenu menu, int segment)
        {
          [_cell setMenu: menu forSegment: segment];
        }
        
        public virtual NSMenu GetMenuForSegment(int segment)
        {
          return [_cell menuForSegment: segment];
        }
        
        public virtual void SetSelectedForSegment(bool flag, int segment)
        {
          [_cell setSelected: flag forSegment: segment];
        }
        
        public virtual bool IsSelectedForSegment(int segment)
        {
          return [_cell isSelectedForSegment: segment];
        }
        
        public virtual void SetEnabled: (BOOL)flag forSegment(int segment)
        {
          [_cell setEnabled: flag forSegment: segment];
        }
        
        public virtual bool IsEnabledForSegment(int segment)
        {
          return [_cell isEnabledForSegment: segment];
        }
        
        public virtual void SetSegmentStyle(NSSegmentStyle style)
        {
          [_cell setSegmentStyle: style];
        }
        
        public virtual NSSegmentStyle SegmentStyle()
        {
          return [_cell segmentStyle];
        }
#endif
    }
}
