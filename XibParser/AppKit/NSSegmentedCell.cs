using Smartmobili.Cocoa.Utils;
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
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSSegmentedCell.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSSegmentedCell.m

    public enum NSSegmentStyle : int
    {
        NSSegmentStyleAutomatic = 0,
        NSSegmentStyleRounded = 1,
        NSSegmentStyleTexturedRounded = 2,
        NSSegmentStyleRoundRect = 3,
        NSSegmentStyleTexturedSquare = 4,
        NSSegmentStyleCapsule = 5,
        NSSegmentStyleSmallSquare = 6,
        NSSegmentStyleSlider = 7,
    }

    public enum NSSegmentSwitchTracking : int
    {
        NSSegmentSwitchTrackingSelectOne = 0,
        NSSegmentSwitchTrackingSelectAny = 1,
        NSSegmentSwitchTrackingMomentary = 2
    }

    public class NSSegmentedCell : NSActionCell
    {
        new public static Class Class = new Class(typeof(NSSegmentedCell));
        new public static NSSegmentedCell Alloc() { return new NSSegmentedCell(); }

        public struct GSSegmentCellFlags //_segmentCellFlags
        {
            [BitfieldLength(3)]
            public uint _tracking_mode;
            [BitfieldLength(1)]
            public uint _trimmed_labels;
            [BitfieldLength(1)]
            public uint _drawing;
            [BitfieldLength(2)]
            public uint unused1;
            [BitfieldLength(1)]
            public uint _recalcToolTips;
            [BitfieldLength(3)]
            public uint unused2;
            [BitfieldLength(8)]
            public uint _style;
            [BitfieldLength(13)]
            public uint unused3;
        }

        protected int _selected_segment;
        protected int _key_selection;
        protected NSMutableArray _items;
        protected GSSegmentCellFlags _segmentCellFlags;


        public override id Init()
        {
            id self = this;

            if (InitTextCell(@"") == null)
                return null;

            _segmentCellFlags._tracking_mode = (uint)NSSegmentSwitchTracking.NSSegmentSwitchTrackingSelectOne;
            _items = (NSMutableArray)NSMutableArray.Alloc().InitWithCapacity(2);
            _selected_segment = -1;
            Alignment = NSTextAlignment.NSCenterTextAlignment;

            return self;
        }

        public override id InitImageCell(NSImage anImage)
        {
            id self = this;

            if (base.InitImageCell(anImage) == null)
                return null;

            _segmentCellFlags._tracking_mode = (uint)NSSegmentSwitchTracking.NSSegmentSwitchTrackingSelectOne;
            _items = (NSMutableArray)NSMutableArray.Alloc().InitWithCapacity(2);
            _selected_segment = -1;
            Alignment = NSTextAlignment.NSCenterTextAlignment;

            return self;
        }

        public override id InitTextCell(NSString aString)
        {
            id self = this;

            if (base.InitTextCell(aString) == null)
                return null;

            _segmentCellFlags._tracking_mode = (uint)NSSegmentSwitchTracking.NSSegmentSwitchTrackingSelectOne;
            _items = (NSMutableArray)NSMutableArray.Alloc().InitWithCapacity(2);
            _selected_segment = -1;
            Alignment = NSTextAlignment.NSCenterTextAlignment;

            return self;
        }


        [ObjcProp("segmentCount")]
        public virtual int SegmentCount
        {
            get
            {
                return _items.Count;
            }

            set
            {
                int count = value;
                int size;

                if ((count < 0) || (count > 2048))
                {
                    throw new IndexOutOfRangeException(@"Illegal segment count.");
                }
  
                size = _items.Count;
               if (count < size)
                   _items.RemoveObjectsInRange(new NSRange((uint)count, (uint)(size - count)));
                    
  
                while (count-- > size)
                {
                    NSSegmentItem item = (NSSegmentItem)NSSegmentItem.Alloc().Init();
                    _items.AddObject(item);   
                }
            }
        }

        [ObjcProp("selectedSegment")]
        public virtual int SelectedSegment
        {
            get { return _selected_segment; }
            set { SetSelected(true, value); }
        }

      
        public virtual void SetSelected(bool flag, int seg)
        {
            NSSegmentItem segment = (NSSegmentItem)_items.ObjectAtIndex(seg);
            NSSegmentItem previous = null;

            if (_selected_segment != -1)
            {
                previous = (NSSegmentItem)_items.ObjectAtIndex(_selected_segment);
                if (_segmentCellFlags._tracking_mode == (uint)NSSegmentSwitchTracking.NSSegmentSwitchTrackingSelectOne)
                {
                    previous.Selected = false;
                }
            }

            if (segment.Enabled)
            {
                segment.Selected = flag;
                if (flag)
                {
                    _selected_segment = seg;
                }
                else if (seg == _selected_segment)
                {
                    _selected_segment = -1;
                }
            }
        }

        public virtual void SelectSegmentWithTag(int tag)
        {
            int segment = 0;

            foreach (NSSegmentItem o in _items)
            { 
                if(o.Tag == tag)
                {
                    break;
                }
                segment++;
            }

            SetSelected(true, segment);
        }

        public virtual void MakeNextSegmentKey()
        {
            int next;

            if (_selected_segment < _items.Count)
            {
                next = _selected_segment + 1;
            }
            else
            {
                next = 0;
            }
            SetSelected(false, _selected_segment);
            SetSelected(true, next);
        }

        public virtual void MakePreviousSegmentKey()
        {
            int prev;

            if (_selected_segment > 0)
            {
                prev = _selected_segment - 1;
            }
            else
            {
                prev = 0;
            }
            SetSelected(false, _selected_segment);
            SetSelected(true, prev);
        }

        [ObjcProp("trackingMode")]
        public virtual NSSegmentSwitchTracking TrackingMode
        {
            get { return (NSSegmentSwitchTracking)_segmentCellFlags._tracking_mode; }
            set { _segmentCellFlags._tracking_mode = (uint)value; }
        }

        // Working with individual segments...
        public virtual void SetWidthForSegment(float width, int seg)
        {
             id segment = _items.ObjectAtIndex(seg);
            ((NSSegmentItem)segment).Width = width;
        }

        public virtual float WidthForSegment(int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            return ((NSSegmentItem)segment).Width;
        }

        public virtual void SetImageForSegment(NSImage image, int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            ((NSSegmentItem)segment).Image = image;
        }

        public virtual NSImage ImageForSegment(int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            return ((NSSegmentItem)segment).Image;
        }

        public virtual void SetLabelForSegment(NSString label, int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            ((NSSegmentItem)segment).Label = label;
        }

        public virtual NSString LabelForSegment(int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            return ((NSSegmentItem)segment).Label;
        }

        public virtual bool IsSelectedForSegment(int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            return ((NSSegmentItem)segment).Selected;
        }

        public virtual void SetEnabledForSegment(bool flag, int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            ((NSSegmentItem)segment).Enabled = flag;
        }

        public virtual bool IsEnabledForSegment(int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            return ((NSSegmentItem)segment).Enabled;
        }

        public virtual void SetMenuForSegment(NSMenu menu, int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            ((NSSegmentItem)segment).Menu = menu;
        }

        public virtual NSMenu MenuForSegment(int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            return ((NSSegmentItem)segment).Menu;
        }

        public virtual void SetToolTipForSegment(NSString toolTip, int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            ((NSSegmentItem)segment).ToolTip = toolTip;
        }

        public virtual NSString ToolTipForSegment(int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            return ((NSSegmentItem)segment).ToolTip;
        }

        public virtual void SetTagForSegment(int tag, int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            ((NSSegmentItem)segment).Tag = tag;
        }

        public virtual int TagForSegment(int seg)
        {
            id segment = _items.ObjectAtIndex(seg);
            return ((NSSegmentItem)segment).Tag;
        }
        // Drawing custom content
        public virtual void DrawSegment(int seg, NSRect frame, NSView view)
        {
        }

        [ObjcProp("selectedSegment")]
        public virtual NSSegmentStyle SegmentStyle
        {
            get { return (NSSegmentStyle)_segmentCellFlags._style; }
            set { _segmentCellFlags._style = (uint)value; }
        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.InitWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {
                int i;

                _selected_segment = -1;
                _segmentCellFlags._tracking_mode = (uint)NSSegmentSwitchTracking.NSSegmentSwitchTrackingSelectOne;
                if (aDecoder.ContainsValueForKey(@"NSSegmentImages"))
                    _items = (NSMutableArray)aDecoder.DecodeObjectForKey(@"NSSegmentImages");
                else
                    _items = (NSMutableArray)NSMutableArray.Alloc().InitWithCapacity(2);

                for (i = 0; i < _items.Count; i++)
                {
                    if (IsSelectedForSegment(i))
                        _selected_segment = i;
                }

                if (aDecoder.ContainsValueForKey(@"NSSelectedSegment"))
                {
                    _selected_segment = aDecoder.DecodeIntForKey(@"NSSelectedSegment");
                    if (_selected_segment != -1)
                        SelectedSegment = _selected_segment;
                }

                _segmentCellFlags._style = (uint)aDecoder.DecodeIntForKey(@"NSSegmentStyle");
            }
            else
            {
                int style = 0;

                _segmentCellFlags._tracking_mode = (uint)NSSegmentSwitchTracking.NSSegmentSwitchTrackingSelectOne;
                _items = (NSMutableArray)aDecoder.DecodeObject();
                aDecoder.DecodeValueOfObjCType<int>(Objc.Encode(typeof(int)), ref _selected_segment);
                if (_selected_segment != -1)
                    SelectedSegment = _selected_segment;

                aDecoder.DecodeValueOfObjCType<int>(Objc.Encode(typeof(int)), ref style);
                _segmentCellFlags._style = (uint)style;
            }

            return self;
        }
    }
}
