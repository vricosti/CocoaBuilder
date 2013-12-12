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

//https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSBox.h
//https://github.com/gnustep/gnustep-gui/blob/master/Source/NSBox.m

namespace Smartmobili.Cocoa
{
    public enum NSBoxType
    {
        //Specifies the primary box appearance. This is the default box type.
        NSBoxPrimary = 0,
        
        //Specifies the secondary box appearance.
        NSBoxSecondary = 1,

        //Specifies that the box is a separator.
        NSBoxSeparator = 2,

        //Specifies that the box is an OS X v10.2–style box.
        NSBoxOldStyle = 3,

        //Specifies that the appearance of the box is determined entirely by the by box-configuration methods, 
        //without automatically applying Apple human interface guidelines.
        NSBoxCustom = 4
    }

    public enum NSBorderType
    {
        //No border.
        NSNoBorder     = 0,

        //A black line border around the view.
        NSLineBorder   = 1,

        //A concave border that makes the view look sunken.
        NSBezelBorder  = 2,

        //A thin border that looks etched around the image.
        NSGrooveBorder = 3
    };

    public enum NSTitlePosition
    {
        NSNoTitle     = 0,
        NSAboveTop    = 1,
        NSAtTop       = 2,
        NSBelowTop    = 3,
        NSAboveBottom = 4,
        NSAtBottom    = 5,
        NSBelowBottom = 6
    }
    



    public class NSBox : NSView
    {
        new public static Class Class = new Class(typeof(NSBox));
        new public static NSBox alloc() { return new NSBox(); }

        protected id _cell;
        protected id _content_view;
        protected NSSize _offsets;
        protected NSRect _border_rect;
        protected NSRect _title_rect;
        protected NSBorderType _border_type;
        protected NSTitlePosition _title_position;
        protected NSBoxType _box_type;
        // Only used when the type is NSBoxCustom
        protected NSColor _fill_color;
        protected NSColor _border_color;
        protected double _border_width;
        protected double _corner_radius;
        protected bool _transparent;


        public virtual NSRect BorderRect 
        { 
            get { return _BorderRect(); } 
        }

        public virtual NSBorderType BorderType
        {
            get { return GetBorderType(); } 
            set { SetBorderType(value); }
        }

        public virtual NSBoxType BoxType
        {
            get { return GetBoxType(); }
            set { SetBoxType(value); }
        }

        public virtual NSString Title
        {
            get { return GetTitle(); }
            set { SetTitle(value); }
        }

        public virtual NSFont TitleFont
        {
            get { return GetTitleFont(); }
            set { SetTitleFont(value); }
        }

        public virtual NSTitlePosition TitlePosition
        {
            get { return GetTitlePosition(); }
            set { SetTitlePosition(value); }
        }

        public virtual id TitleCell
        {
            get { return GetTitleCell(); }
        }

        public virtual NSRect TitleRect
        {
            get { return GetTitleRect(); }
        }

        public virtual id ContentView
        {
            get { return GetContentView(); }
            set { SetContentView((NSView)value); }
        }

        public virtual NSSize ContentViewMargins
        {
            get { return GetContentViewMargins(); }
            set { SetContentViewMargins(value); }
        }

        public override NSRect Frame
        {
            set { SetFrame(value); }
        }
        
        public override NSSize FrameSize
        {
            set { SetFrameSize(value); }
        }

        public virtual NSSize FrameFromContentFrame
        {
            set { SetFrameFromContentFrame(value); }
        }

        public NSBox()
        {
        }


        public override id initWithFrame(NSRect frameRect)
        {
            id self = this;

            NSView cv;

            if (base.initWithFrame(frameRect) == null)
                return null;

            _cell = (id)Objc.MsgSend(NSCell.alloc(), "initTextCell", @"Title");
            ((NSCell)_cell).Alignment = NSTextAlignment.NSCenterTextAlignment;
            ((NSCell)_cell).Bordered = false;
            ((NSCell)_cell).Editable = false;
            this.TitleFont = NSFont.SystemFontOfSize(NSFont.SmallSystemFontSize);

            _offsets.Width = 5;
            _offsets.Height = 5;
            _border_rect = _bounds;
            _border_type = NSBorderType.NSGrooveBorder;
            _title_position = NSTitlePosition.NSAtTop;
            _title_rect = NSRect.Zero;
            
            // FIXME
            //this.AutoresizesSubviews = false;
            
            cv = (NSView)NSView.alloc().init();
            this.ContentView = cv;

            return self;
        }


        internal virtual NSRect _BorderRect()
        {
            return _border_rect;
        }


        public virtual NSBorderType GetBorderType()
        {
            return _border_type;
        }

        public virtual void SetBorderType(NSBorderType aType)
        {
            _border_type = aType;
            if (_content_view != null)
                ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
            this.setNeedsDisplay(true);
        }

        public virtual NSBoxType GetBoxType()
        {
            return _box_type;
        }

        public virtual void SetBoxType(NSBoxType aType)
        {
            if (_box_type != aType)
            {
                _box_type = aType;
                if (_content_view != null)
                    ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
                this.setNeedsDisplay(true);
            }
        }

        public virtual void SetTitle(NSString aString)
        {
            ((NSCell)_cell).StringValue = aString;
            if (_content_view != null)
                ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
            this.setNeedsDisplay(true);
        }

        public virtual void _SetTitleWithMnemonic(NSString aString)
        {
            //FIXME : implement NSCell SetTitleWithMnemonic
            //((NSCell)_cell).StringValue = aString;
            if (_content_view != null)
                ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
            this.setNeedsDisplay(true);
        }

        public virtual void SetTitleFont(NSFont fontObj)
        {
            ((NSCell)_cell).Font = fontObj;
            if (_content_view != null)
                ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
            this.setNeedsDisplay(true);
        }

        public virtual void SetTitlePosition(NSTitlePosition aPosition)
        {
            if (_title_position != aPosition)
            {
                _title_position = aPosition;
                if (_content_view != null)
                    ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
                this.setNeedsDisplay(true);
            }
        }

        public virtual NSString GetTitle()
        {
            return ((NSCell)_cell).StringValue;
        }

        public virtual id GetTitleCell()
        {
            return _cell;
        }

        public virtual NSFont GetTitleFont()
        {
            return ((NSCell)_cell).Font;
        }

        public virtual NSTitlePosition GetTitlePosition()
        {
            return _title_position;
        }
         
        public virtual NSRect GetTitleRect()
        {
            return _title_rect;
        }

        public virtual id GetContentView()
        {
            return _content_view;
        }

        public virtual NSSize GetContentViewMargins()
        {
            return _offsets;
        }

        public virtual void SetContentView(NSView aView)
        {
            if (aView != null)
            {
                base.ReplaceSubviewWith((NSView)_content_view, (NSView)aView);
                _content_view = aView;
                ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
            }
        }

        public virtual void SetContentViewMargins(NSSize offsetSize)
        {
            _offsets = offsetSize;
            if (_content_view != null)
                ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
            this.setNeedsDisplay(true);
        }

        public override void SetFrame(NSRect frameRect)
        {
            base.Frame = frameRect;
            if (_content_view != null)
                ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
        }

        public override void SetFrameSize(NSSize newSize)
        {
            base.FrameSize = newSize;
            if (_content_view != null)
                ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);
        }

        
        public virtual void SetFrameFromContentFrame(NSRect contentFrame)
        {
            NSRect r = this.CalcSizesAllowingNegative(true);
            NSRect f = _frame;

            if (_super_view != null)
                r = _super_view.ConvertRectFromView(r, this);

            // Add the difference to the frame
            double w = f.Size.Width + (contentFrame.Size.Width - r.Size.Width);
            double h = f.Size.Height + (contentFrame.Size.Height - r.Size.Height);
            f.Size = new NSSize(w, h);

            double x = f.Origin.X + (contentFrame.Origin.X - r.Origin.X);
            double y = f.Origin.Y + (contentFrame.Origin.Y - r.Origin.Y);
            f.Origin = new NSPoint(x,y);

            this.Frame = f;
            ((NSView)_content_view).Frame = CalcSizesAllowingNegative(false);

        }


        public virtual NSSize MinimumSize()
        {
            NSRect rect = new NSRect();
            //NSSize borderSize = [[GSTheme theme] sizeForBorderType: _border_type];

            if (_content_view.respondsToSelector(new SEL("MinimumSize")))
            {
                rect.Size = (NSSize)Objc.MsgSend(_content_view, @"MinimumSize");
            }
            else
            {
                NSArray subviewArray = ((NSView)_content_view).SubViews;
                if (subviewArray.Count != 0)
                {
                    id subview;
                    NSEnumerator enumerator;
                    enumerator = subviewArray.objectEnumerator();
                    rect = ((NSView)enumerator.nextObject()).Frame;

                    // Loop through subviews and calculate rect
                    // to encompass all
                    while ((subview = enumerator.nextObject()) != null)
                      {
                        rect = NSRect.Union(rect, ((NSView)subview).Frame);
                      }
                  }
                else // _content_view has no subviews
                  {
                    rect = NSRect.Zero;
                }
            }

            // FIXME
            //rect.size = [self convertSize: rect.size fromView:_content_view];
            //rect.size.width += (2 * _offsets.width) + (2 * borderSize.width);
            //rect.size.height += (2 * _offsets.height) + (2 * borderSize.height);

            return rect.Size;
        }



        public override void encodeWithCoder(NSCoder aCoder)
        {
            base.encodeWithCoder(aCoder);

            if (aCoder.AllowsKeyedCoding)
            {
                
            }
            else
            {

            }

        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            base.initWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.containsValueForKey(@"NSBoxType"))
                {
                    int boxType = aDecoder.decodeIntForKey(@"NSBoxType");
                    this.BoxType = (NSBoxType)boxType;
                }
                if (aDecoder.containsValueForKey(@"NSBorderType"))
                {
                    NSBorderType borderType = (NSBorderType)aDecoder.decodeIntForKey(@"NSBorderType");
                    this.BorderType = borderType;
                }
                if (aDecoder.containsValueForKey(@"NSTitlePosition"))
                {
                    NSTitlePosition titlePosition = (NSTitlePosition)aDecoder.decodeIntForKey(@"NSTitlePosition");
                    this.TitlePosition = titlePosition;
                }
                if (aDecoder.containsValueForKey(@"NSTransparent"))
                {
                    // On Apple this is always NO, we keep it for old GNUstep archives
                    _transparent = aDecoder.decodeBoolForKey(@"NSTransparent");
                }
                if (aDecoder.containsValueForKey(@"NSFullyTransparent"))
                {
                    _transparent = aDecoder.decodeBoolForKey(@"NSFullyTransparent");
                }
                if (aDecoder.containsValueForKey(@"NSOffsets"))
                {
                    this.ContentViewMargins = aDecoder.decodeSizeForKey(@"NSOffsets");
                }
                if (aDecoder.containsValueForKey(@"NSTitleCell"))
                {
                    NSCell titleCell = (NSCell)aDecoder.decodeObjectForKey(@"NSTitleCell");
                    _cell = titleCell;
                }
                if (aDecoder.containsValueForKey(@"NSContentView"))
                {
                    NSView contentView = (NSView)aDecoder.decodeObjectForKey(@"NSContentView");
                    this.ContentView = contentView;
                }
            }

            return this;
        }

        private NSRect CalcSizesAllowingNegative(bool aFlag)
        {
            return new NSRect();
        }

    }
}
