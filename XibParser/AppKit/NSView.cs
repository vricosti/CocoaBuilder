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
using System.Xml.Linq;

//https://github.com/gnustep/gnustep-gui/blob/master/Source/NSView.m

namespace Smartmobili.Cocoa
{
    [FlagsAttribute]
    public enum NSViewAutoresizingMasks : uint
    {
        NSViewNotSizable	= 0,	// view does not resize with its superview
        NSViewMinXMargin	= 1,	// left margin between views can stretch
        NSViewWidthSizable	= 2,	// view's width can stretch
        NSViewMaxXMargin	= 4,	// right margin between views can stretch
        NSViewMinYMargin	= 8,	// bottom margin between views can stretch
        NSViewHeightSizable	= 16,	// view's height can stretch
        NSViewMaxYMargin	= 32, 	// top margin between views can stretch

        NSViewAutoresizingMask = 0x3F,
        NSViewAutoresizesSubviewsMask = 1 << 8,
        //NSViewHiddenMask = 1 << 31
    }

    public class NSView : NSResponder
    {
        new public static Class Class = new Class(typeof(NSView));

        public struct _rFlagsType  
        {
            [BitfieldLength(1)]
            public uint flipped_view;      /* Flipped state the last time we checked. */
            [BitfieldLength(1)]
            public uint has_subviews;		/* The view has subviews.	*/
            [BitfieldLength(1)]
            public uint has_currects;		/* The view has cursor rects.	*/
            [BitfieldLength(1)]
            public uint has_trkrects;		/* The view has tracking rects.	*/
            [BitfieldLength(1)]
            public uint has_draginfo;		/* View has drag types. 	*/
            [BitfieldLength(1)]
            public uint opaque_view;		/* For views whose opacity may	*/

            [BitfieldLength(1)]             /* change to keep track of it.	*/
            public uint valid_rects;		/* Some cursor rects may be ok.	*/
            [BitfieldLength(1)]
            public uint needs_display;	    /* view needs display.   	*/
            [BitfieldLength(1)]
            public uint has_tooltips;		/* The view has tooltips set.	*/
            [BitfieldLength(1)]
            public uint ignores_backing;   /* The view does not trigger    */
                                            /* backing flush when drawn     */
        }

        protected NSRect _frame;
        protected NSRect _bounds;
        protected id _frameMatrix;
        protected id _boundsMatrix;
        protected id _matrixToWindow;
        protected id _matrixFromWindow;

        protected NSView _super_view;

        protected NSMutableArray _sub_views;

        protected NSWindow _window;

        protected NSMutableArray _tracking_rects;
        protected NSMutableArray _cursor_rects;

        protected NSRect _invalidRect;
        protected NSRect _visibleRect;
        protected int _gstate;
        protected id _nextKeyView;
        protected id _previousKeyView;

        protected _rFlagsType _rFlags;

        protected bool _is_rotated_from_base;
        protected bool _is_rotated_or_scaled_from_base;
        protected bool _post_frame_changes;
        protected bool _post_bounds_changes;
        protected bool _autoresizes_subviews;
        protected bool _coordinates_valid;
        protected bool _allocate_gstate;
        protected bool _renew_gstate;
        protected bool _is_hidden;
        protected bool _in_live_resize;

        protected uint _autoresizingMask;
        protected NSFocusRingType _focusRingType;
        protected NSRect _autoresizingFrameError;


        public int NSvFlags { get; set; }

        public virtual NSRect Frame { get; set; }

        public virtual NSSize FrameSize { get; set; }

        public virtual id Superview { get; set; }

        public virtual id Window { get; set; }

        public virtual id PreviousKeyView { get; set; }

        public virtual id NextKeyView { get; set; }

        public virtual string ReuseIdentifierKey { get; set; }

        //<string key="NSOffsets">{0, 0}</string>
        public virtual NSPoint Offsets { get; set; }

        public virtual string ClassName { get; set; }



        public NSArray SubViews 
        {
            get { return _sub_views; }
        }

        public NSView()
        {
           //Init();
        }

        public override id Init()
        {
            return InitWithFrame(NSRect.Zero);
        }

        public virtual id InitWithFrame(NSRect frameRect)
        {
            id self = this;

            if (base.Init() == null)
                return null;

            if (frameRect.Size.Width < 0)
            {
                frameRect.Size = new NSSize(0, frameRect.Size.Height);
            }
            if (frameRect.Size.Height < 0)
            {
                frameRect.Size = new NSSize(frameRect.Size.Width, 0);
            }

            _frame = frameRect;			// Set frame rectangle
            _bounds.Origin = NSPoint.Zero;		// Set bounds rectangle
            _bounds.Size = _frame.Size;

            // _frameMatrix = [NSAffineTransform new];    // Map fromsuperview to frame
            // _boundsMatrix = [NSAffineTransform new];   // Map from superview to bounds
            _matrixToWindow = NSAffineTransform.Alloc().Init();   // Map to window coordinates
            _matrixFromWindow = NSAffineTransform.Alloc().Init(); // Map from window coordinates

            _sub_views = (NSMutableArray)NSMutableArray.Alloc().Init();
            _tracking_rects = (NSMutableArray)NSMutableArray.Alloc().Init();
            _cursor_rects = (NSMutableArray)NSMutableArray.Alloc().Init();

            // Some values are already set by initialisation
            //_super_view = nil;
            //_window = nil;
            //_is_rotated_from_base = NO;
            //_is_rotated_or_scaled_from_base = NO;
            _rFlags.needs_display = 1;
            _post_bounds_changes = true;
            _post_frame_changes = true;
            _autoresizes_subviews = true;
            _autoresizingMask = (uint)NSViewAutoresizingMasks.NSViewNotSizable;
            //_coordinates_valid = NO;
            //_nextKeyView = 0;
            //_previousKeyView = 0;

            return self;
        }



        public virtual void ReplaceSubviewWith(NSView oldView, NSView newView)
        {
            if (newView == oldView)
            {
                return;
            }
        }


        public virtual NSRect ConvertRectFromView(NSRect aRect, NSView aView)
        {
            return new NSRect();
        }

        

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            NSEnumerator e;
            NSView sub;
            NSArray subs;

            if (base.InitWithCoder(aDecoder) == null)
                return null;

             _matrixToWindow = NSAffineTransform.Alloc().Init();  // Map to window coordinates
             _matrixFromWindow = NSAffineTransform.Alloc().Init();// Map from window coordinates

            if (aDecoder.AllowsKeyedCoding)
            {
                NSView prevKeyView = null;
                NSView nextKeyView = null;

                if (aDecoder.ContainsValueForKey("NSFrame"))
                {
                    _frame = aDecoder.DecodeRectForKey("NSFrame");
                }
                else 
                {
                    _frame = NSRect.Zero;
                    if (aDecoder.ContainsValueForKey("NSFrameSize"))
                    {
                        _frame = aDecoder.DecodeSizeForKey("NSFrameSize");
                    }
                }

                // Set bounds rectangle
                _bounds.Origin = NSPoint.Zero;
                _bounds.Size = _frame.Size;
                if (aDecoder.ContainsValueForKey("NSBounds"))
                {
                    //[self setBounds: [aDecoder decodeRectForKey: @"NSBounds"]];
                }
                
                _sub_views = (NSMutableArray)NSMutableArray.Alloc().Init();
                _tracking_rects = (NSMutableArray)NSMutableArray.Alloc().Init();
                _cursor_rects = (NSMutableArray)NSMutableArray.Alloc().Init();

                _is_rotated_from_base = false;
                _is_rotated_or_scaled_from_base = false;
                _rFlags.needs_display = Convert.ToUInt32(true);
                _post_bounds_changes = true;
                _post_frame_changes = true;
                _autoresizes_subviews = true;
                _autoresizingMask =(uint) NSViewAutoresizingMasks.NSViewNotSizable;
                _coordinates_valid = false;
                /*
                 * Note: don't zero _nextKeyView and _previousKeyView, as the key view
                 * chain may already have been established by super's initWithCoder:
                 *
                 * _nextKeyView = 0;
                 * _previousKeyView = 0;
                 */

                // previous and next key views...
                 prevKeyView = (NSView)aDecoder.DecodeObjectForKey("NSPreviousKeyView");
                 nextKeyView = (NSView)aDecoder.DecodeObjectForKey("NSNextKeyView");
                if (nextKeyView != null)
                {
                    NextKeyView = nextKeyView;
                }
                if (prevKeyView != null)
                {
                    PreviousKeyView = prevKeyView;
                }
                if (aDecoder.ContainsValueForKey("NSvFlags"))
                {
                    uint vFlags = (uint)aDecoder.DecodeIntForKey("NSvFlags");
                    
                    // We are lucky here, Apple use the same constants
                    // in the lower bits of the flags
                    
                    //FIXME
                    //[self setAutoresizingMask: vFlags & 0x3F];
                    //[self setAutoresizesSubviews: ((vFlags & 0x100) == 0x100)];
                    //[self setHidden: ((vFlags & 0x80000000) == 0x80000000)];
                }

                 // iterate over subviews and put them into the view...
                subs = (NSArray)aDecoder.DecodeObjectForKey("NSSubviews");
                e = subs.ObjectEnumerator();
                 while ((sub = (NSView)e.NextObject()) != null)
                 {
                     System.Diagnostics.Debug.Assert(sub.GetClass() != NSCustomView.Class);
                     System.Diagnostics.Debug.Assert(sub.Window == null);
                     System.Diagnostics.Debug.Assert(sub.Superview == null);

                      //sub._viewWillMoveToWindow(_window);
                      //sub._viewWillMoveToSuperview(this);
                      //sub.setNextResponder(this);
	                  _sub_views.AddObject(sub);
	                  _rFlags.has_subviews = 1;
	                  //sub.ResetCursorRects();
	                  sub.SetNeedsDisplay(true);
	                  //sub._ViewDidMoveToWindow];
	                  //sub.ViewDidMoveToSuperview];
	                  //this.DidAddSubview: sub];

                 }



                //NSvFlags = aDecoder.DecodeIntForKey("NSvFlags");
                //_sub_views = (NSArray)aDecoder.DecodeObjectForKey("NSSubviews");

                //Window = aDecoder.DecodeObjectForKey("NSWindow");
                //ClassName = (NSString)aDecoder.DecodeObjectForKey("NSWindow");

                //Offsets = aDecoder.DecodePointForKey("NSOffsets");

                //Superview = aDecoder.DecodeObjectForKey("NSSuperview");
            }

            return self;
        }


        public virtual void SetNeedsDisplay(bool flag)
        {

        }
    }
}
