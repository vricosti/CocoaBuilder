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

        private static Class rectClass;
        private static Class viewClass;

        private static NSAffineTransform flip = null;

        private static NSNotificationCenter nc = null;

        protected NSRect _frame;
        protected NSRect _bounds;
		protected NSAffineTransform _frameMatrix;
		protected NSAffineTransform _boundsMatrix;
        protected NSAffineTransform _matrixToWindow;
        protected NSAffineTransform _matrixFromWindow;

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

        public virtual NSRect Frame 
        {
            get { return GetFrame(); }
            set { SetFrame(value); }
        }

        public virtual NSSize FrameSize 
        {
            get { return GetFrame().Size; }
            set { SetFrameSize(value); } 
        }


        public virtual NSRect Bounds
        {
            get { return GetBounds(); }
            set { SetBounds(value); }
        }
        //public virtual id Superview { get; set; }

        //public virtual id Window { get; set; }

        public virtual id PreviousKeyView { get; set; }

        public virtual id NextKeyView { get; set; }

        public virtual string ReuseIdentifierKey { get; set; }

        //<string key="NSOffsets">{0, 0}</string>
        public virtual NSPoint Offsets { get; set; }

        public virtual string ClassName { get; set; }



        public virtual NSArray SubViews 
        {
            get { return _sub_views; }
        }

        public NSView()
        {
           //Init();
        }



		public virtual NSArray GSGetDragTypes(NSView obj)
		{
			NSArray	t = null;

			//FIXME
			//[typesLock lock];
			//t = (NSArray*)NSMapGet(typesMap, (void*)(gsaddr)obj);
			//[typesLock unlock];
			return t;
		}

		public static void GSRemoveDragTypes(NSView obj)
		{
			//FIXME
			//[typesLock lock];
			//NSMapRemove(typesMap, (void*)(gsaddr)obj);
			//[typesLock unlock];
		}

		public static NSArray GSSetDragTypes(NSView obj, NSArray types)
		{
			uint	count = (uint)types.Count;
			NSString[] strings = new NSString[count];
			NSArray	t = null;
			uint	i = 0;

			/*
			 * Make a new array with copies of the type strings so we don't get
			 * them mutated by someone else.
			 */
			types.GetObjects(strings);
			for (i = 0; i < count; i++)
			{
				strings[i] = strings[i].Copy();
			}
			/*
			 * Store it.
			 */
			//[typesLock lock];
			//NSMapInsert(typesMap, (void*)(gsaddr)obj, (void*)(gsaddr)t);
			//[typesLock unlock];
			return t;
		}

		protected virtual void _InvalidateCoordinates()
        {
            //if (_coordinates_valid == true)
            //{
            //    uint count;

            //    _coordinates_valid = false;
            //    if (_rFlags.valid_rects != 0)
            //    {
            //        _window.InvalidateCursorRectsForView(this);
            //    }
            //    if (_rFlags.has_subviews != 0)
            //    {
            //        count = (uint)_sub_views.Count;
            //        if (count > 0)
            //        {
            //            NSView[] array = new NSView[count];
            //            uint i;

            //            _sub_views.GetObjects(array);
            //            for (i = 0; i < count; i++)
            //            {
            //                NSView sub = array[i];

            //                if (sub._coordinates_valid == true)
            //                {
			//					  _invalidateCoordinates();
            //                    //(*invalidateImp)(sub, invalidateSel);
            //                }
            //            }
            //        }
            //    }
            //    this.RenewGState();
            //}
        }

		protected virtual  NSAffineTransform _MatrixFromWindow()
        {
            this._RebuildCoordinates();
            return (NSAffineTransform)_matrixFromWindow;
        }

		protected virtual  NSAffineTransform _MatrixToWindow()
        {
            this._RebuildCoordinates();
            return (NSAffineTransform)_matrixToWindow;
        }

		protected virtual  void _RebuildCoordinates()
        {
      //       bool isFlipped = this._IsFlipped();
      //       bool lastFlipped = Convert.ToBoolean(_rFlags.flipped_view);

      //       if ((_coordinates_valid == false) || (isFlipped != lastFlipped))
      //       {
      //           _coordinates_valid = true;
      //           _rFlags.flipped_view = isFlipped;

      //           if (_window == null)
      //           {
      //               _visibleRect = NSRect.Zero;
      //               _matrixToWindow.MakeIdentityMatrix();
      //               _matrixFromWindow.MakeIdentityMatrix();
      //           }
      //           else
      //  {
      //    NSRect		superviewsVisibleRect;
      //    bool			superFlipped;
      //    NSAffineTransform	pMatrix;
      //    NSAffineTransformStruct     ts;
 
      //if (_super_view != null)
      //  {
      //    superFlipped = _super_view.IsFlipped();
      //    pMatrix = _super_view._MatrixToWindow();
      //  }
      //else
      //  {
      //    superFlipped = false;
      //    pMatrix = NSAffineTransform.Transform();
      //}

      //ts = pMatrix.GetTransformStruct();

      //    /* prepend translation */
      //    ts.tX = NS.MinX(_frame) * ts.m11 + NS.MinY(_frame) * ts.m21 + ts.tX;
      //    ts.tY = NS.MinX(_frame) * ts.m12 + NS.MinY(_frame) * ts.m22 + ts.tY;
      //    _matrixToWindow.SetTransformStruct(ts);
 
      //    /* prepend rotation */
      //    if (_frameMatrix != null)
      //      {
      //        //(*preImp)(_matrixToWindow, preSel, _frameMatrix);
      //      }
 
      //    if (isFlipped != superFlipped)
      //      {
      //        /*
      //         * The flipping process must result in a coordinate system that
      //         * exactly overlays the original.	 To do that, we must translate
      //         * the origin by the height of the view.
      //         */
      //        ts = [flip transformStruct];
      //        ts.tY = _frame.Size.Height;
      //        [flip setTransformStruct: ts];
      //        (*preImp)(_matrixToWindow, preSel, flip);
      //      }
      //    if (_boundsMatrix != nil)
      //      {
      //        (*preImp)(_matrixToWindow, preSel, _boundsMatrix);
      //      }
      //    ts = [_matrixToWindow transformStruct];
      //    [_matrixFromWindow setTransformStruct: ts];
      //    [_matrixFromWindow invert];

      //if (_super_view != null)
      //  {
      //    superviewsVisibleRect = [self convertRect: [_super_view visibleRect]
      //                     fromView: _super_view];

      //    _visibleRect = NSIntersectionRect(superviewsVisibleRect, _bounds);
      //  }
      //else
      //  {
      //    _visibleRect = _bounds;
      //  }
      //  }
      //       }
        }

		protected virtual void _ViewDidMoveToWindow()
		{
			this.ViewDidMoveToWindow();
			if (_rFlags.has_subviews != 0)
			{
				uint count = (uint)_sub_views.Count;

				if (count > 0)
				{
					uint i;
					NSView[] array = new NSView[count];

					_sub_views.GetObjects((id[])array);
					for (i = 0; i < count; ++i)
					{
						array [i]._ViewDidMoveToWindow ();
					}
				}
			}
		}

		protected virtual void  _ViewWillMoveToWindow(NSWindow newWindow)
		{
			bool old_allocate_gstate;

			this.ViewWillMoveToWindow(newWindow);
			if (_coordinates_valid)
			{
				//FIXME
				//(*invalidateImp)(self, invalidateSel);
			}
			if (_rFlags.has_currects != 0)
			{
				this.DiscardCursorRects();
			}

			if (newWindow == _window)
			{
				return;
			}

			// This call also reset _allocate_gstate, so we have 
			// to store this value and set it again.
			// This way we keep the logic in one place.
			old_allocate_gstate = _allocate_gstate;
			this.ReleaseGState();
			_allocate_gstate = old_allocate_gstate;

			if (_rFlags.has_draginfo != 0)
			{
				NSArray t = GSGetDragTypes(this);

				if (_window != null)
				{
					//FIXME
					//[GSDisplayServer removeDragTypes: t fromWindow: _window];
					//if ([_window autorecalculatesKeyViewLoop])
					//{
					//	[_window recalculateKeyViewLoop];
					//}
				}
				if (newWindow != null)
				{
					//FIXME
//					[GSDisplayServer addDragTypes: t toWindow: newWindow];
//					if ([newWindow autorecalculatesKeyViewLoop])
//					{
//						[newWindow recalculateKeyViewLoop];
//					}
				}
			}

			_window = newWindow;

			if (_rFlags.has_subviews != 0)
			{
				uint count = (uint)_sub_views.Count;

				if (count > 0)
				{
					uint i;
					NSView[] array = new NSView[count];

					_sub_views.GetObjects((id[])array);
					for (i = 0; i < count; ++i)
					{
						array[i]._ViewWillMoveToWindow(newWindow);
					}
				}
			}
		}

		protected virtual void _ViewWillMoveToSuperview(NSView newSuper)
		{
			this.ViewWillMoveToSuperview(newSuper);
			_super_view = newSuper;
		}

		/*
		 * Extend in super view covered by the frame of a view.
		 * When the frame is rotated, this is different from the frame.
		 */
		protected virtual NSRect _FrameExtend()
		{
			NSRect frame = _frame;

			if (_frameMatrix != null)
			{
				NSRect r = NSRect.Zero;

				r.Origin = NSPoint.Zero;
				r.Size = frame.Size;
				_frameMatrix.BoundingRectFor(r, ref r);
				frame = NS.OffsetRect(r, NS.MinX(frame),
				                     NS.MinY(frame));
			}

			return frame;
		}

		protected virtual NSString _SubtreeDescriptionWithPrefix(NSString prefix)
		{
			NSMutableString desc = (NSMutableString)NSMutableString.Alloc ().Init();
			NSEnumerator e;
			NSView v;

			desc.AppendFormat(@"%@%@\n", prefix, this.Description(), null);

			prefix = prefix.StringByAppendingString(@"  ");
			e = _sub_views.ObjectEnumerator();
			while ((v = (NSView)e.NextObject()) != null)
			{
				desc.AppendString(v._SubtreeDescriptionWithPrefix(prefix));
			}

			return desc;
		}

		/*
		 * Unofficial Cocoa method for debugging a view hierarchy.
		 */
		public virtual NSString _SubtreeDescription()
		{
			return this._SubtreeDescriptionWithPrefix(@""); 
		}

		public virtual NSString _FlagDescription()
		{
			return @"";
		}

		public virtual NSString _ResizeDescription()
		{
			return NSString.StringWithFormat(@"h=%c%c%c v=%c%c%c", 
			                                 (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewMinXMargin) != 0 ? '&' : '-',
			                                 (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewWidthSizable) != 0 ? '&' : '-',
			                                 (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewMaxXMargin) != 0 ? '&' : '-',
			                                 (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewMinYMargin) != 0 ? '&' : '-',
			                                 (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewHeightSizable) != 0 ? '&' : '-',
			                                 (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewMaxYMargin) != 0 ? '&' : '-', 
			                                 null);
		}

		public override NSString Description()
		{
			return NSString.StringWithFormat(@"%@ %@ %@ f=%@ b=%@", 
			        this._FlagDescription(), 
			        this._ResizeDescription(), base.Description(), 
			        NSString.FromRect(_frame), NSString.FromRect(_bounds), null);
		}

		/**
		Return the view at the top of graphics contexts stack
		or nil if none is focused.
		*/
		public static NSView FocusView()
		{
			//FIXME
			//return [GSCurrentContext() focusView];
			return null;
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
            _matrixToWindow = (NSAffineTransform)NSAffineTransform.Alloc().Init();   // Map to window coordinates
            _matrixFromWindow = (NSAffineTransform)NSAffineTransform.Alloc().Init(); // Map from window coordinates

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

		public virtual void AddSubview(NSView aView)
		{
			this.AddSubview (aView, NSWindowOrderingMode.NSWindowAbove, null);
		}


		public virtual void AddSubview(NSView aView, NSWindowOrderingMode place, NSView otherView)
		{
			uint index;

			if (aView == null)
			{
				return;
			}
			if (this.IsDescendantOf(aView))
			{
				NSException.Raise(@"NSInvalidArgumentException", 
				                  @"addSubview:positioned:relativeTo: creates a loop in the views tree!");
			}

			if (aView == otherView)
				return;

			
			aView.RemoveFromSuperview();

			// Do this after the removeFromSuperview, as aView may already 
			// be a subview and the index could change.
			if (otherView == null)
			{
				index = NS.NotFound;
			}
			else
			{
				index = _sub_views.IndexOfObjectIdenticalTo(otherView);
			}
				if (index == NS.NotFound)
			{
					if (place == NSWindowOrderingMode.NSWindowBelow)
					index = 0;
				else
					index = (uint)_sub_views.Count;
			}
				else if (place != NSWindowOrderingMode.NSWindowBelow)
			{
				index += 1;
			}

			aView._ViewWillMoveToWindow(_window);
			aView._ViewWillMoveToSuperview(this);
			aView.SetNextResponder(this);
			_sub_views.InsertObject(aView,index);
			_rFlags.has_subviews = 1;
			aView.ResetCursorRects();
			aView.SetNeedsDisplay(true);
			aView._ViewDidMoveToWindow();
			aView.ViewDidMoveToSuperview();
			this.DidAddSubview(aView);
		}

		/**
		 * Returns self if aView is the receiver or aView is a subview of the receiver,
		 * the ancestor view shared by aView and the receiver if any, or
		 * aView if it is an ancestor of the receiver, otherwise returns nil.
		 */
		public virtual NSView AncestorSharedWithView(NSView aView)
		{
			NSView self = this;

			if (self == aView)
				return self;

			if (this.IsDescendantOf(aView))
				return aView;

			if (aView.IsDescendantOf(self))
				return self;

			/*
			 * If neither are descendants of each other and either does not have a
			 * superview then they cannot have a common ancestor
			 */
			if (_super_view == null)
				return null;

			if (aView.Superview == null)
				return null;

			/* Find the common ancestor of superviews */
			return _super_view.AncestorSharedWithView((NSView)aView.Superview);
		}

		/**
		 * Returns YES if aView is an ancestor of the receiver.
		 */
		public virtual bool IsDescendantOf(NSView aView)
		{
			id self = this;

			if (aView == self)
				return true;

			if (_super_view == null)
				return false;

			if (_super_view == aView)
				return true;

			return _super_view.IsDescendantOf(aView);
		}

		public virtual NSView OpaqueAncestor()
		{
			NSView self = this;
			NSView	next = _super_view;
			NSView	current = self;

			while (next != null)
			{
				if (current.Opaque == true)
				{
					break;
				}
				current = next;
				next = current._super_view;
			}
			return current;
		}


		/**
		 * Removes the receiver from its superviews list of subviews.
		 */
		public virtual void RemoveFromSuperviewWithoutNeedingDisplay()
		{
			if (_super_view != null)
			{
				_super_view.RemoveSubview(this);
			}
		}

		public virtual void RemoveFromSuperview()
		{
			if (_super_view != null)
			{
				_super_view.SetNeedsDisplayInRect(_frame);
				this.RemoveFromSuperviewWithoutNeedingDisplay();
			}
		}

		public virtual void RemoveSubview(NSView aView)
		{
			NSView view;
			/*
			 * This must be first because it invokes -resignFirstResponder:, 
			 * which assumes the view is still in the view hierarchy
			 */


            //FIXME (VRI) : normally _window is not null I think ...
            if (_window != null)
            {
                for (view = (NSView)_window.FirstResponder;
                     view != null && view.RespondsToSelector(new SEL(@"GetSuperview"));
                     view = view.Superview)
                {
                    if (view == aView)
                    {
                        
                        //[_window makeFirstResponder: _window];
                        break;
                    }
                }
            }
			this.WillRemoveSubview(aView);
			aView._super_view = null;
			aView._ViewWillMoveToWindow(null);
			aView._ViewWillMoveToSuperview(null);
			aView.SetNextResponder(null);

			_sub_views.RemoveObjectIdenticalTo(aView);
			aView.SetNeedsDisplay(false);
			aView._ViewDidMoveToWindow();
			aView.ViewDidMoveToSuperview();

			if (_sub_views.Count == 0)
			{
				_rFlags.has_subviews = 0;
			}
		}

        public virtual void ReplaceSubviewWith(NSView oldView, NSView newView)
        {
            if (newView == oldView)
            {
                return;
            }
			/*
   			* NB. we implement the replacement in full rather than calling addSubview:
   			* since classes like NSBox override these methods but expect to be able to
   			* call [super replaceSubview:with:] safely.
   			*/
			if (oldView == null)
			{
				/*
       			* Strictly speaking, the docs say that if 'oldView' is not a subview
       			* of the receiver then we do nothing - but here we add newView anyway.
       			* So a replacement with no oldView is an addition.
       			*/
				//RETAIN(newView);
				newView.RemoveFromSuperview();
				newView._ViewWillMoveToWindow(_window);
				newView._ViewWillMoveToSuperview(this);
				newView.SetNextResponder(this);
				_sub_views.AddObject(newView);
				_rFlags.has_subviews = 1;
				newView.ResetCursorRects();
				newView.SetNeedsDisplay(true);
				newView._ViewDidMoveToWindow();
				newView.ViewDidMoveToSuperview();
				this.DidAddSubview(newView);
				//RELEASE(newView);
			}
			else if (_sub_views.IndexOfObjectIdenticalTo(oldView) != NS.NotFound)
			{
				if (newView == null)
				{
					/*
					 * If there is no new view to add - we just remove the old one.
					 * So a replacement with no newView is a removal.
					 */
					oldView.RemoveFromSuperview();
				}
				else
				{
					uint index;

					/*
	   				* Ok - the standard case - we remove the newView from wherever it
	   				* was (which may have been in this view), locate the position of
	   				* the oldView (which may have changed due to the removal of the
	   				* newView), remove the oldView, and insert the newView in it's
	   				* place.
	   				*/
					//RETAIN(newView);
					newView.RemoveFromSuperview();
					index = _sub_views.IndexOfObjectIdenticalTo(oldView);
					oldView.RemoveFromSuperview();
					newView._ViewWillMoveToWindow(_window);
					newView._ViewWillMoveToSuperview(this);
					newView.SetNextResponder(this);
					_sub_views.InsertObject(newView, index);
					_rFlags.has_subviews = 1;
					newView.ResetCursorRects();
					newView.SetNeedsDisplay(true);
					newView._ViewDidMoveToWindow();
					newView.ViewDidMoveToSuperview();
					this.DidAddSubview(newView);
					//RELEASE(newView);
				}
			}
        }

		public virtual void SetSubviews(NSArray newSubviews)
		{
			NSEnumerator en;
			NSView aView;
			NSMutableArray uniqNew = NSMutableArray.Array();

			if (null == newSubviews)
			{
				NSException.Raise(@"NSInvalidArgumentException" ,@"Setting nil as new subviews.");
			}

			// Use a copy as we remove from the subviews array
			en = NSArray.ArrayWithArray(_sub_views).ObjectEnumerator();
			while ((aView = (NSView)en.NextObject()) != null)
			{
				if (false == newSubviews.ContainsObject(aView))
				{
					aView.RemoveFromSuperview();
				}
			}

			en = newSubviews.ObjectEnumerator();
			while ((aView = (NSView)en.NextObject()) != null)
			{
				id supersub = aView.Superview;

				if (supersub != null && supersub != this)
				{
					NSException.Raise(@"NSInvalidArgumentException" ,@"Superviews of new subviews must be either nil or receiver.");
				}

				if (uniqNew.ContainsObject(aView))
				{
					NSException.Raise(@"NSInvalidArgumentException" ,@"Duplicated new subviews.");
				}

				if (false == _sub_views.ContainsObject(aView))
				{
					this.AddSubview(aView);
				}

				uniqNew.AddObject(aView);
			}

			_sub_views = uniqNew;

			// The order of the subviews may have changed
			this.SetNeedsDisplay(true);
		}


		public virtual void ViewWillMoveToSuperview(NSView newSuper)
		{}

		public virtual void ViewWillMoveToWindow(NSWindow newWindow)
		{}
		
		public virtual void DidAddSubview(NSView subview)
		{}

		public virtual void ViewDidMoveToSuperview()
		{}

		public virtual void ViewDidMoveToWindow()
		{}

		public virtual void WillRemoveSubview(NSView subview)
		{}

		static NSSize _computeScale(NSSize fs, NSSize bs)
		{
			NSSize scale = NS.MakeSize (0, 0);

			if (bs.Width == 0)
			{
				if (fs.Width == 0)
					scale.Width = 1;
				else
					scale.Width = Int64.MaxValue;
			}
			else
			{
				scale.Width = fs.Width / bs.Width;
			}
			if (bs.Height == 0)
			{
				if (fs.Height == 0)
					scale.Height = 1;
				else
					scale.Height = Int64.MaxValue;
			}
			else
			{
				scale.Height = fs.Height / bs.Height;
			}

			return scale;
		}

		public virtual void  _SetFrameAndClearAutoresizingError(NSRect frameRect)
		{
			_frame = frameRect;
			_autoresizingFrameError = NSRect.Zero;
		}


		public virtual void SetFrame(NSRect frameRect)
		{
			bool	changedOrigin = false;
			bool	changedSize = false;
			NSSize old_size = _frame.Size;

			if (frameRect.Size.Width < 0)
			{
				//NSWarnMLog(@"given negative width", 0);
				//frameRect.Size.Width = 0;
                frameRect.Size = NS.MakeSize(0, frameRect.Size.Height);
			}
			if (frameRect.Size.Height < 0)
			{
				//NSWarnMLog(@"given negative height", 0);
				//frameRect.Size.Height = 0;
                frameRect.Size = NS.MakeSize(frameRect.Size.Width, 0);
			}

			if (NS.EqualPoints(_frame.Origin, frameRect.Origin) == false)
			{
				changedOrigin = true;
			}
			if (NS.EqualSizes(_frame.Size, frameRect.Size) == false)
			{
				changedSize = true;
			}

			if (changedSize == true || changedOrigin == true)
			{
				this._SetFrameAndClearAutoresizingError(frameRect);

				if (changedSize == true)
				{
					if (_is_rotated_or_scaled_from_base == true)
					{
						NSAffineTransform matrix;
						NSRect frame = _frame;

						frame.Origin = NS.MakePoint(0, 0);
						matrix = (NSAffineTransform)_boundsMatrix.Copy();
						matrix.Invert();
						matrix.BoundingRectFor(frame, ref _bounds);
						//RELEASE(matrix);               
					}
					else
					{
						_bounds.Size = frameRect.Size;
					}
				}

				if (_coordinates_valid)
				{
					//FIXME
					//(*invalidateImp)(self, invalidateSel);
				}
				this.ResetCursorRects();
				this.ResizeSubviewsWithOldSize(old_size);
				if (_post_frame_changes)
				{
					//[nc postNotificationName: NSViewFrameDidChangeNotification object: self];
				}
			}
		}


		public virtual void SetFrameOrigin(NSPoint newOrigin)
		{
			if (NS.EqualPoints(_frame.Origin, newOrigin) == false)
			{
				NSRect newFrame = _frame;
				newFrame.Origin = newOrigin;

				if (_coordinates_valid)
				{
					//FIXME
					//(*invalidateImp)(self, invalidateSel);
				}
				this._SetFrameAndClearAutoresizingError(newFrame);
				this.ResetCursorRects();
				if (_post_frame_changes)
				{
					//FIXME
					//[nc postNotificationName: NSViewFrameDidChangeNotification object: self];
				}
			}
		}

		public virtual void SetFrameSize(NSSize newSize)
		{
			NSRect newFrame = _frame;
			if (newSize.Width < 0)
			{
				//NSWarnMLog(@"given negative width", 0);
				newSize.Width = 0;
			}
			if (newSize.Width < 0)
			{
				//NSWarnMLog(@"given negative height", 0);
				newSize.Height = 0;
			}
			if (NS.EqualSizes(_frame.Size, newSize) == false)
			{
				NSSize old_size = _frame.Size;

				if (_is_rotated_or_scaled_from_base)
				{
					if (_boundsMatrix == null)
					{
						double sx = _bounds.Size.Width  / _frame.Size.Width;
						double sy = _bounds.Size.Height / _frame.Size.Height;

						newFrame.Size = newSize;
						this._SetFrameAndClearAutoresizingError(newFrame);
                        _bounds.Size = NS.MakeSize(_frame.Size.Width * sx, _frame.Size.Height * sy); 
                        //_bounds.Size.Width  = _frame.Size.Width  * sx;
						//_bounds.Size.Height = _frame.Size.Height * sy;
					}
					else
					{
						NSAffineTransform matrix;
						NSRect frame;

						newFrame.Size = newSize;
						this._SetFrameAndClearAutoresizingError(newFrame);

						frame = _frame;
						frame.Origin = NS.MakePoint(0, 0);
						matrix = (NSAffineTransform)_boundsMatrix.Copy();
						matrix.Invert();
						matrix.BoundingRectFor(frame ,ref _bounds);
						//RELEASE(matrix);
					}
				}
				else
				{
					newFrame.Size = _bounds.Size = newSize;
					this._SetFrameAndClearAutoresizingError(newFrame);
				}

				if (_coordinates_valid)
				{
					//FIXME
					//(*invalidateImp)(self, invalidateSel);
				}
				this.ResetCursorRects();
				this.ResizeSubviewsWithOldSize(old_size);
				if (_post_frame_changes)
				{
					//FIXME
					//[nc postNotificationName: NSViewFrameDidChangeNotification object: self];
				}
			}
		}

		public virtual void SetFrameRotation(double angle)
		{
			double oldAngle = this.GetFrameRotation();

			if (oldAngle != angle)
			{
				/* no frame matrix, create one since it is needed for rotation */
				if (_frameMatrix == null)
				{
					// Map from superview to frame
					_frameMatrix = (NSAffineTransform)NSAffineTransform.Alloc().Init();
				}

				_frameMatrix.RotateByDegrees(angle - oldAngle);
				_is_rotated_from_base = _is_rotated_or_scaled_from_base = true;

				if (_coordinates_valid)
				{
					//FIXME
					//(*invalidateImp)(self, invalidateSel);
				}
				this.ResetCursorRects();
				if (_post_frame_changes)
				{
					//[nc postNotificationName: NSViewFrameDidChangeNotification object: self];
				}
			}
		}

		public virtual bool IsRotatedFromBase()
		{
			if (_is_rotated_from_base)
			{
				return true;
			}
			else if (_super_view != null)
			{
				return _super_view.IsRotatedFromBase();
			}
			else
			{
				return false;
			}
		}

		public virtual bool IsRotatedOrScaledFromBase()
		{
			if (_is_rotated_or_scaled_from_base)
			{
				return true;
			}
			else if (_super_view != null)
			{
				return _super_view.IsRotatedOrScaledFromBase();
			}
			else
			{
				return false;
			}
		}

		public virtual void SetBounds(NSRect aRect)
		{
			//NSDebugLLog(@"NSView", @"setBounds %@", NSStringFromRect(aRect));
			if (aRect.Size.Width < 0)
			{
				//NSWarnMLog(@"given negative width", 0);
				//aRect.Size.Width = 0;
                aRect.Size = NS.MakeSize(0, aRect.Size.Height);
			}
			if (aRect.Size.Height < 0)
			{
				//NSWarnMLog(@"given negative height", 0);
				//aRect.Size.Height = 0;
                aRect.Size = NS.MakeSize(aRect.Size.Width, 0);
			}

			if (_is_rotated_from_base || (NS.EqualRects(_bounds, aRect) == false))
			{
				NSAffineTransform matrix;
				NSPoint oldOrigin;
				NSSize scale;

				if (_boundsMatrix == null)
				{
					_boundsMatrix = (NSAffineTransform)NSAffineTransform.Alloc().Init(); 
				}

				// Adjust scale
				scale = _computeScale(_frame.Size, aRect.Size);
				if (scale.Width != 1 || scale.Height != 1)
				{
					_is_rotated_or_scaled_from_base = true;
				}
				_boundsMatrix.ScaleTo(scale.Width, scale.Height);
				{
					matrix = (NSAffineTransform)_boundsMatrix.Copy();
					matrix.Invert();
					oldOrigin = matrix.TransformPoint(NS.MakePoint(0, 0));
					//RELEASE(matrix);
				}
				_boundsMatrix.TranslateXByYBy(oldOrigin.X - aRect.Origin.X, oldOrigin.Y - aRect.Origin.Y);      
				if (!_is_rotated_from_base)
				{
					// Adjust bounds
					_bounds = aRect;
				}
				else
				{
					// Adjust bounds
					NSRect frame = _frame;

					frame.Origin = NS.MakePoint(0, 0);
					matrix = (NSAffineTransform)_boundsMatrix.Copy();
					matrix.Invert();
					matrix.BoundingRectFor(frame, ref _bounds);
					//RELEASE(matrix);
				}

				if (_coordinates_valid)
				{
					//FIXME
					//(*invalidateImp)(self, invalidateSel);
				}
				this.ResetCursorRects();
				if (_post_bounds_changes)
				{
					//[nc postNotificationName: NSViewBoundsDidChangeNotification object: self];
				}
			}
		}

		public virtual void SetBoundsOrigin(NSPoint newOrigin)
		{
			NSPoint oldOrigin;

			if (_boundsMatrix == null)
			{
				oldOrigin = NS.MakePoint(NS.MinX(_bounds), NS.MinY(_bounds));
			}
			else
			{
				NSAffineTransform matrix = (NSAffineTransform)_boundsMatrix.Copy();

				matrix.Invert();
				oldOrigin = matrix.TransformPoint(NS.MakePoint(0, 0));
				//RELEASE(matrix);
			}
			this.TranslateOriginToPoint(NS.MakePoint(oldOrigin.X - newOrigin.X, 
			                                          oldOrigin.Y - newOrigin.Y));
		}

		public virtual void SetBoundsSize(NSSize newSize)
		{
			NSSize scale;

			//NSDebugLLog(@"NSView", @"%@ setBoundsSize: %@", self, NSStringFromSize(newSize));

			if (newSize.Width < 0)
			{
				//NSWarnMLog(@"given negative width", 0);
				newSize.Width = 0;
			}
			if (newSize.Height < 0)
			{
				//NSWarnMLog(@"given negative height", 0);
				newSize.Height = 0;
			}

			scale = _computeScale(_frame.Size, newSize);
			if (scale.Width != 1 || scale.Height != 1)
			{
				_is_rotated_or_scaled_from_base = true;
			}

			if (_boundsMatrix == null)
			{
				_boundsMatrix = (NSAffineTransform)NSAffineTransform.Alloc().Init(); 
			}
			_boundsMatrix.ScaleTo(scale.Width, scale.Height);
			if (!_is_rotated_from_base)
			{
				scale = _computeScale(_bounds.Size, newSize);

                _bounds.Origin = NS.MakePoint(_bounds.Origin.X / scale.Width, _bounds.Origin.Y / scale.Height);
				//_bounds.Origin.X = _bounds.Origin.X / scale.Width;
				//_bounds.Origin.Y = _bounds.Origin.Y / scale.Height;
				_bounds.Size = newSize;
			}
			else
			{
				NSAffineTransform matrix;
				NSRect frame = _frame;

				frame.Origin = NS.MakePoint(0, 0);

				matrix = (NSAffineTransform)_boundsMatrix.Copy();
				matrix.Invert();
				matrix.BoundingRectFor(frame, ref _bounds);
				//RELEASE(matrix);               
			}

			if (_coordinates_valid)
			{
				//FIXME
				//(*invalidateImp)(self, invalidateSel);
			}
			this.ResetCursorRects();
			if (_post_bounds_changes)
			{
				//[nc postNotificationName: NSViewBoundsDidChangeNotification object: self];
			}
		}
		
		public virtual void SetBoundsRotation(double angle)
		{
			this.RotateByAngle(angle - this.GetBoundsRotation());
		}

		public virtual void TranslateOriginToPoint(NSPoint point)
		{
			//NSDebugLLog(@"NSView", @"%@ translateOriginToPoint: %@", self, NSStringFromPoint(point));
			if (NS.EqualPoints(NSPoint.Zero, point) == false)
			{
				if (_boundsMatrix == null)
				{
					_boundsMatrix = (NSAffineTransform)NSAffineTransform.Alloc().Init();
				}
				_boundsMatrix.TranslateXByYBy(point.X, point.Y);
				// Adjust bounds
                _bounds.Origin = NS.MakePoint(_bounds.Origin.X - point.X, _bounds.Origin.Y - point.Y);
                //_bounds.Origin.X -= point.X;
                //_bounds.Origin.Y -= point.Y;

				if (_coordinates_valid)
				{
					//FIXME
					//(*invalidateImp)(self, invalidateSel);
				}
				this.ResetCursorRects();
				if (_post_bounds_changes)
				{
					//[nc postNotificationName: NSViewBoundsDidChangeNotification object: self];
				}
			}
		}

		public virtual void ScaleUnitSquareToSize(NSSize newSize)
		{
			if (newSize.Width != 1.0 || newSize.Height != 1.0)
			{
				if (newSize.Width < 0)
				{
					//NSWarnMLog(@"given negative width", 0);
					newSize.Width = 0;
				}
				if (newSize.Height < 0)
				{
					//NSWarnMLog(@"given negative height", 0);
					newSize.Height = 0;
				}

				if (_boundsMatrix == null)
				{
					_boundsMatrix = (NSAffineTransform)NSAffineTransform.Alloc().Init(); 
				}
				_boundsMatrix.ScaleXByYBy(newSize.Width, newSize.Height);
				// Adjust bounds
                _bounds.Origin = NS.MakePoint(_bounds.Origin.X / newSize.Width, _bounds.Origin.Y / newSize.Height);
                _bounds.Size = NS.MakeSize(_bounds.Size.Width / newSize.Width, _bounds.Size.Height / newSize.Height);
                //_bounds.Origin.X = _bounds.Origin.X / newSize.Width;
                //_bounds.Origin.Y = _bounds.Origin.Y / newSize.Height;
                //_bounds.Size.Width  = _bounds.Size.Width  / newSize.Width;
                //_bounds.Size.Height = _bounds.Size.Height / newSize.Height;

				_is_rotated_or_scaled_from_base = true;

				if (_coordinates_valid)
				{
					//FIXME
					//(*invalidateImp)(self, invalidateSel);
				}
				this.ResetCursorRects();
				if (_post_bounds_changes)
				{
					//[nc postNotificationName: NSViewBoundsDidChangeNotification object: self];
				}
			}
		}

		public void RotateByAngle(double angle)
		{
			if (angle != 0.0)
			{
				NSAffineTransform matrix;
				NSRect frame = _frame;

				frame.Origin = NS.MakePoint(0, 0);
				if (_boundsMatrix == null)
				{
					_boundsMatrix = (NSAffineTransform)NSAffineTransform.Alloc().Init(); 
				}
				_boundsMatrix.RotateByDegrees(angle);
				// Adjust bounds
				matrix = (NSAffineTransform)_boundsMatrix.Copy();
				matrix.Invert();
				matrix.BoundingRectFor(frame, ref _bounds);
				//RELEASE(matrix);

				_is_rotated_from_base = _is_rotated_or_scaled_from_base = true;

				if (_coordinates_valid)
				{
					//FIXME
					//(*invalidateImp)(self, invalidateSel);
				}
				this.ResetCursorRects();
				if (_post_bounds_changes)
				{
					//[nc postNotificationName: NSViewBoundsDidChangeNotification object: self];
				}
			}
		}

		public virtual NSRect CenterScanRect(NSRect aRect)
		{
			//FIXME
			return new NSRect ();
		}

		public virtual NSPoint ConvertPointFromView(NSPoint aPoint, NSView aView)
		{
			NSPoint inBase;

			if (aView == this)
			{
				return aPoint;
			}

			if (aView != null)
			{
				//NS.Assert(_window == aView.Window, @"NSInvalidArgumentException");      
				inBase = aView._matrixToWindow.TransformPoint(aPoint);    
			}
			else
			{
				inBase = aPoint;
			}

			return this._matrixFromWindow.TransformPoint(inBase);
		}

		public virtual NSPoint ConvertPointToView(NSPoint aPoint, NSView aView)
		{
			NSPoint inBase;

			if (aView == this)
				return aPoint;

			inBase = this._MatrixToWindow().TransformPoint(aPoint);

			if (aView != null)
			{
				//NS.Assert(_window == [aView window], NSInvalidArgumentException);      
				return aView._matrixFromWindow.TransformPoint(inBase);
			}
			else
			{
				return inBase;
			}
		}

		/* Helper for -convertRect:fromView: and -convertRect:toView:. */
		private static NSRect convert_rect_using_matrices(NSRect aRect, NSAffineTransform matrix1,
		                                                  NSAffineTransform matrix2)
		{
			NSRect r = new NSRect();
			NSPoint[] p = new NSPoint[4];
			NSPoint min, max;
			int i;

			for (i = 0; i < 4; i++)
				p[i] = aRect.Origin;
			p[1].X += aRect.Size.Width;
			p[2].Y += aRect.Size.Height;
			p[3].X += aRect.Size.Width;
			p[3].Y += aRect.Size.Height;

			for (i = 0; i < 4; i++)
				p[i] = matrix1.TransformPoint(p[i]);

			min = max = p[0] = matrix2.TransformPoint(p[0]);
			for (i = 1; i < 4; i++)
			{
				p[i] = matrix2.TransformPoint(p[i]);
				min.X = Math.Min(min.X, p[i].X);
				min.Y = Math.Min(min.Y, p[i].Y);
				max.X = Math.Max(max.X, p[i].X);
				max.Y = Math.Max(max.Y, p[i].Y);
			}

			r.Origin = min;
            r.Size = NS.MakeSize(max.X - min.X, max.Y - min.Y);
            //r.Size.Width = max.X - min.X;
            //r.Size.Height = max.Y - min.Y;

			return r;
		}

		public virtual NSRect ConvertRectFromView(NSRect aRect, NSView aView)
		{
			NSAffineTransform matrix1, matrix2;

			if (aView == this || _window == null || (aView != null && aView.Window == null))
			{
				return aRect;
			}

			if (aView != null)
			{
				//NS.Assert(_window == aView.Window, @"NSInvalidArgumentException"); 
				matrix1 = aView._MatrixToWindow();      
			}
			else
			{
				matrix1 = NSAffineTransform.Transform;
			}

			matrix2 = this._MatrixFromWindow();

			return convert_rect_using_matrices(aRect, matrix1, matrix2);
		}


		public virtual NSRect ConvertRectToView(NSRect aRect, NSView aView)
		{
			NSAffineTransform matrix1, matrix2;

			if (aView == this || _window == null || (aView != null && aView.Window == null))
			{
				return aRect;
			}

			matrix1 = this._MatrixToWindow();

			if (aView != null)
			{
				//NS.Assert(_window == aView.Window, @"NSInvalidArgumentException");
				matrix2 = aView._MatrixFromWindow();
			}
			else
			{
				matrix2 = NSAffineTransform.Transform;
			}

			return convert_rect_using_matrices(aRect, matrix1, matrix2);
		}

		public virtual NSSize ConvertSizeFromView(NSSize aSize, NSView aView)
		{
			NSSize inBase;
			NSSize inSelf;

			if (aView != null)
			{
				//NS.Assert(_window == aView.Window, @"NSInvalidArgumentException");      
				inBase = aView._MatrixToWindow().TransformSize(aSize);
				if (inBase.Height < 0.0)
				{
					inBase.Height = -inBase.Height;
				} 
			}
			else
			{
				inBase = aSize;
			}

			inSelf = this._MatrixFromWindow().TransformSize(inBase);
			if (inSelf.Height < 0.0)
			{
				inSelf.Height = -inSelf.Height;
			}
			return inSelf;
		}

		public virtual NSSize ConvertSizeToView(NSSize aSize, NSView aView)
		{
			NSSize inBase = this._MatrixToWindow().TransformSize(aSize);
			if (inBase.Height < 0.0)
			{
				inBase.Height = -inBase.Height;
			} 

			if (aView != null)
			{
				NSSize inOther;
				//NS.Assert(_window == aView.Window, @"NSInvalidArgumentException");      
				inOther = aView._MatrixFromWindow().TransformSize(inBase);
				if (inOther.Height < 0.0)
				{
					inOther.Height = -inOther.Height;
				}
				return inOther;
			}
			else
			{
				return inBase;
			}
		}

		public virtual NSPoint ConvertPointFromBase(NSPoint aPoint)
		{
			return this.ConvertPointFromView(aPoint, null);
		}

		public virtual NSPoint ConvertPointToBase(NSPoint aPoint)
		{
			return this.ConvertPointToView(aPoint, null);
		}

		public virtual NSRect ConvertRectFromBase(NSRect aRect)
		{
			return this.ConvertRectFromView(aRect, null);
		}

		public virtual NSRect ConvertRectToBase(NSRect aRect)
		{
			return this.ConvertRectToView(aRect, null);
		}

		public virtual NSSize ConvertSizeFromBase(NSSize aSize)
		{
			return this.ConvertSizeFromView(aSize, null);
		}

		public virtual NSSize ConvertSizeToBase(NSSize aSize)
		{
			return this.ConvertSizeToView(aSize, null);
		}


		public virtual void SetPostsFrameChangedNotifications(bool flag)
		{
			_post_frame_changes = flag;
		}
		
		public virtual void SetPostsBoundsChangedNotifications(bool flag)
		{
			_post_bounds_changes = flag;
		}

		public virtual void ResizeSubviewsWithOldSize(NSSize oldSize)
		{
			if (_rFlags.has_subviews != 0)
			{
				NSEnumerator e;
				NSView o;

				if (_autoresizes_subviews == false || _is_rotated_from_base == true)
					return;

				e = _sub_views.ObjectEnumerator();
				o = (NSView)e.NextObject();
				while (o != null)
				{
					o.ResizeWithOldSuperviewSize(oldSize);
					o = (NSView)e.NextObject();
				}
			}
		}

		public static void Autoresize(double oldContainerSize,
		                              double newContainerSize,
		                              ref double contentPositionInOut,
                                      ref double contentSizeInOut,
		                       		  bool minMarginFlexible,
		                              bool sizeFlexible,
		                              bool maxMarginFlexible)
		{
            double change = newContainerSize - oldContainerSize;
            double oldContentSize = contentSizeInOut;
            double oldContentPosition = contentPositionInOut;
            double flexibleSpace = 0.0;

            // See how much flexible space we have to distrube the change over

            if (sizeFlexible)
                flexibleSpace += oldContentSize;

            if (minMarginFlexible)
                flexibleSpace += oldContentPosition;

            if (maxMarginFlexible)
                flexibleSpace += oldContainerSize - oldContentPosition - oldContentSize;


            if (flexibleSpace <= 0.0)
            {
                /**
                 * In this code path there is no flexible space so we divide 
                 * the available space equally among the flexible portions of the view
                 */
                int subdivisions = (sizeFlexible ? 1 : 0) +
                   (minMarginFlexible ? 1 : 0) +
                       (maxMarginFlexible ? 1 : 0);

                if (subdivisions > 0)
                {
                    double changePerOption = change / subdivisions;

                    if (sizeFlexible)
                    {
                        contentSizeInOut += changePerOption;
                    }
                    if (minMarginFlexible)
                    {
                        contentPositionInOut += changePerOption;
                    }
                }
            }
            else
            {
                /**
                * In this code path we distribute the change proportionately
                * over the flexible spaces
                */
                double changePerPoint = change / flexibleSpace;

                if (sizeFlexible)
                {
                    contentSizeInOut += changePerPoint * oldContentSize;
                }
                if (minMarginFlexible)
                {
                    contentPositionInOut += changePerPoint * oldContentPosition;
                }
            }
		}


		public virtual void ResizeWithOldSuperviewSize(NSSize oldSize)
		{
            NSSize superViewFrameSize;
            NSRect newFrame = _frame;
            NSRect newFrameRounded;

            if (_autoresizingMask == (uint)NSViewAutoresizingMasks.NSViewNotSizable)
                return;

            if (!NS.EqualRects(NS.ZeroRect, _autoresizingFrameError))
            {
                newFrame.Origin = NS.MakePoint(newFrame.Origin.X -_autoresizingFrameError.Origin.X,  newFrame.Origin.Y - _autoresizingFrameError.Origin.Y);
                //newFrame.Origin.X -= _autoresizingFrameError.Origin.X;
                //newFrame.Origin.Y -= _autoresizingFrameError.Origin.Y;

                newFrame.Size = NS.MakeSize(newFrame.Size.Width - _autoresizingFrameError.Size.Width, newFrame.Size.Height - _autoresizingFrameError.Size.Height);
                //newFrame.Size.Width -= _autoresizingFrameError.Size.Width;
                //newFrame.Size.Height -= _autoresizingFrameError.Size.Height;
            }

            superViewFrameSize = NS.MakeSize(0,0);
            if (_super_view != null)
                superViewFrameSize = _super_view.Frame.Size;

            double orgX = newFrame.Origin.X;
            double sizeW = newFrame.Size.Width;
            
            Autoresize(oldSize.Width,superViewFrameSize.Width,
                ref orgX,
                ref sizeW,
                (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewMinXMargin) != 0,
                (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewWidthSizable) != 0,
                (_autoresizingMask & (uint)NSViewAutoresizingMasks.NSViewMaxXMargin) != 0);
            
            newFrame.Origin = NS.MakePoint(orgX, newFrame.Origin.Y);
            newFrame.Size = NS.MakeSize(sizeW, newFrame.Size.Height);

            {
                bool flipped = ((_super_view != null) && _super_view.IsFlipped());

                double orgY = newFrame.Origin.Y;
                double sizeH = newFrame.Size.Height;
                
                Autoresize(oldSize.Height, superViewFrameSize.Height,
                    ref orgY,
                    ref sizeH,
                    flipped ? NS.IsBitSet(_autoresizingMask, (uint)NSViewAutoresizingMasks.NSViewMaxYMargin) : NS.IsBitSet(_autoresizingMask, (uint)NSViewAutoresizingMasks.NSViewMinYMargin),
                    NS.IsBitSet(_autoresizingMask, (uint)NSViewAutoresizingMasks.NSViewHeightSizable),
                    flipped ? NS.IsBitSet(_autoresizingMask, (uint)NSViewAutoresizingMasks.NSViewMinYMargin) : NS.IsBitSet(_autoresizingMask, (uint)NSViewAutoresizingMasks.NSViewMaxYMargin));
            }

            newFrameRounded = newFrame;

            /**
            * Perform rounding to pixel-align the frame if we are not rotated
            */
            if (!this.IsRotatedFromBase() && this.Superview != null)
            {
                newFrameRounded = ((NSView)this.Superview).CenterScanRect(newFrameRounded);
            }

            this.SetFrame(newFrameRounded);

            _autoresizingFrameError.Origin = NS.MakePoint(newFrameRounded.Origin.X - newFrame.Origin.X, newFrameRounded.Origin.Y - newFrame.Origin.Y);
            //_autoresizingFrameError.Origin.X = (newFrameRounded.Origin.X - newFrame.Origin.X);
            //_autoresizingFrameError.Origin.Y = (newFrameRounded.Origin.Y - newFrame.Origin.Y);
  
            _autoresizingFrameError.Size = NS.MakeSize(newFrameRounded.Size.Width - newFrame.Size.Width, newFrameRounded.Size.Height - newFrame.Size.Height);
            //_autoresizingFrameError.Size.Width = (newFrameRounded.Size.Width - newFrame.Size.Width);
            //_autoresizingFrameError.Size.Height = (newFrameRounded.Size.Height - newFrame.Size.Height);
		}

        public virtual void SetNeedsDisplayInRect(NSRect invalidRect)
        {
        
        }

        public virtual void Display()
        {

        }

        public virtual void DisplayIfNeeded()
        {

        }


        public virtual void DisplayIfNeededIgnoringOpacity()
        {

        }

        public virtual void DisplayIfNeededInRect(NSRect aRect)
        {

        }

        public virtual void DisplayIfNeededInRectIgnoringOpacity(NSRect aRect)
        {

        }

        public virtual void DisplayRect(NSRect aRect)
        {

        }
        public virtual void DisplayRectIgnoringOpacity(NSRect aRect)
        {

        }

        public virtual void DisplayRectIgnoringOpacity(NSRect aRect, NSGraphicsContext context)
        {

        }

        public virtual void DrawRect(NSRect rect)
        {

        }

        public virtual NSRect VisibleRect
        {
            get { return GetVisibleRect(); }
        }

        public virtual NSRect GetVisibleRect()
        {
            if (this.HiddenOrHasHiddenAncestor)
            {
                return NS.ZeroRect;
            }

            if (_coordinates_valid == false)
            {
                this._RebuildCoordinates();
            }
            return _visibleRect;
        }

        public static NSFocusRingType DefaultFocusRingType
        {
            get { return GetDefaultFocusRingType(); }
        }

        public virtual NSFocusRingType FocusRingType
        {
            get { return GetFocusRingType(); }
            set { SetFocusRingType(value); }
        }

        public virtual void SetFocusRingType(NSFocusRingType focusRingType)
        {
            _focusRingType = focusRingType;
        }

        public virtual NSFocusRingType GetFocusRingType()
        {
            return _focusRingType;
        }


        public static NSFocusRingType GetDefaultFocusRingType()
        {
            return NSFocusRingType.NSFocusRingTypeDefault;
        }

        public virtual bool Hidden
        {
            get { return IsHidden(); }
            set { SetHidden(value); }
        }

        public virtual bool HiddenOrHasHiddenAncestor
        {
            get { return IsHiddenOrHasHiddenAncestor(); }
        }

        public virtual void SetHidden(bool flag)
        {

            NSView view;

            if (_is_hidden == flag)
                return;

            _is_hidden = flag;

            if (_is_hidden)
            {
                if (_window != null)
                {
                    for (view = (NSView)_window.FirstResponder;
                         view != null && view.RespondsToSelector(new SEL("superview"));
                         view = (NSView)view.Superview)
                    {
                        if (view == this)
                        {
                            //_window.MakeFirstResponder(this.NextValidKeyView());
                            break;
                        }
                    }
                }
                if (_rFlags.has_draginfo != 0)
                {
                    if (_window != null)
                    {
                        //NSArray t = GSGetDragTypes(self);
                        //[GSDisplayServer removeDragTypes: t fromWindow: _window];
                    }
                }
                if (Superview != null)
                    ((NSView)this.Superview).SetNeedsDisplay(true);
            }
            else
            {
                if (_rFlags.has_draginfo != 0)
                {
                    if (_window != null)
                    {
                        //NSArray t = GSGetDragTypes(this);

                        //[GSDisplayServer addDragTypes: t toWindow: _window];
                    }
                }
                if (_rFlags.has_subviews != 0)
                {
                    // The _visibleRect of subviews will be NSZeroRect, because when they
                    // were calculated in -[_rebuildCoordinates], they were intersected
                    // with the result of calling -[visibleRect] on the hidden superview,
                    // which returns NSZeroRect for hidden views.
                    //
                    // So, recalculate the subview coordinates now to make them correct.

                    //[_sub_views makeObjectsPerformSelector: @selector(_invalidateCoordinates)];
                }
                this.SetNeedsDisplay(true);
            }
        }

        public virtual bool IsHidden()
        {
            return _is_hidden;
        }

        public virtual bool IsHiddenOrHasHiddenAncestor()
        {
            return (this.IsHidden() || _super_view.IsHiddenOrHasHiddenAncestor());
        }




		public virtual void DiscardCursorRects()
		{
		}

		public virtual void ResetCursorRects()
		{

		}


		protected void ReleaseGState()
		{

		}



        

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            NSEnumerator e;
            NSView sub;
            NSArray subs;

            if (base.InitWithCoder(aDecoder) == null)
                return null;

            _matrixToWindow = (NSAffineTransform)NSAffineTransform.Alloc().Init();  // Map to window coordinates
            _matrixFromWindow = (NSAffineTransform)NSAffineTransform.Alloc().Init();// Map from window coordinates

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
                    this.SetBounds(aDecoder.DecodeRectForKey(@"NSBounds"));
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
                    //2013-06-02 10:40:22.872 Gorm[26233] NSvFlags: 0x112 (274) (274)
                    //2013-06-02 10:40:22.872 Gorm[26233] NSvFlags: 0x80000100 (-2147483392) (-2147483392)
                    //2013-06-02 10:40:22.873 Gorm[26233] NSvFlags: 0x80000100 (-2147483392) (-2147483392)
                    //2013-06-02 10:40:22.873 Gorm[26233] NSvFlags: 0x136 (310) (310)
                    //2013-06-02 10:40:22.873 Gorm[26233] NSvFlags: 0x900 (2304) (2304)
                    //2013-06-02 10:40:22.873 Gorm[26233] NSvFlags: 0x8000010a (-2147483382) (-2147483382)
                    //2013-06-02 10:40:22.873 Gorm[26233] NSvFlags: 0x10a (266) (266)
                    //2013-06-02 10:40:22.873 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.873 Gorm[26233] NSvFlags: 0x10a (266) (266)
                    //2013-06-02 10:40:22.874 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.874 Gorm[26233] NSvFlags: 0x10a (266) (266)
                    //2013-06-02 10:40:22.874 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.874 Gorm[26233] NSvFlags: 0x112 (274) (274)
                    //2013-06-02 10:40:22.874 Gorm[26233] NSvFlags: 0x8000010a (-2147483382) (-2147483382)
                    //2013-06-02 10:40:22.874 Gorm[26233] NSvFlags: 0x100 (256) (256)
                    //2013-06-02 10:40:22.875 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.875 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.875 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.875 Gorm[26233] NSvFlags: 0x102 (258) (258)
                    //2013-06-02 10:40:22.876 Gorm[26233] NSvFlags: 0x104 (260) (260)
                    //2013-06-02 10:40:22.876 Gorm[26233] NSvFlags: 0x102 (258) (258)
                    //2013-06-02 10:40:22.876 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.879 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.885 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.893 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.901 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.902 Gorm[26233] NSvFlags: 0x10c (268) (268)
                    //2013-06-02 10:40:22.902 Gorm[26233] NSvFlags: 0x12d (301) (301)
                    //2013-06-02 10:40:22.903 Gorm[26233] NSvFlags: 0x112 (274) (274)

                    // We are lucky here, Apple use the same constants
                    // in the lower bits of the flags 
                   this.SetAutoresizingMask(vFlags & 0x3F);
                   this.SetAutoresizesSubviews((vFlags & 0x100) == 0x100);
                   this.SetHidden((vFlags & 0x80000000) == 0x80000000);
                }

                 // iterate over subviews and put them into the view...
                subs = (NSArray)aDecoder.DecodeObjectForKey("NSSubviews");
                if (subs != null)
                {
                    e = subs.ObjectEnumerator();
                    while ((sub = (NSView)e.NextObject()) != null)
                    {
                        System.Diagnostics.Debug.Assert(sub.GetClass() != NSCustomView.Class);
                        System.Diagnostics.Debug.Assert(sub.Window == null);
                        System.Diagnostics.Debug.Assert(sub.Superview == null);

                        sub._ViewWillMoveToWindow(_window);
                        sub._ViewWillMoveToSuperview(this);
                        sub.SetNextResponder(this);
                        _sub_views.AddObject(sub);
                        _rFlags.has_subviews = 1;
                        sub.ResetCursorRects();
                        sub.SetNeedsDisplay(true);
                        sub._ViewDidMoveToWindow();
                        sub.ViewDidMoveToSuperview();
                        this.DidAddSubview(sub);

                    }
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

        public virtual bool AutoresizesSubviews
        {
            get { return GetAutoresizesSubviews(); }
            set { SetAutoresizesSubviews(value); }
        }

        public virtual bool GetAutoresizesSubviews()
        {
            return _autoresizes_subviews;
        }

        public virtual void  SetAutoresizesSubviews(bool flag)
        {
            _autoresizes_subviews = flag;
        }

        public virtual uint AutoresizingMask
        {
            get { return GetAutoresizingMask(); }
            set { SetAutoresizingMask(value); }
        }

        public virtual uint GetAutoresizingMask()
        {
            return _autoresizingMask;
        }

        public virtual void SetAutoresizingMask(uint mask)
        {
            _autoresizingMask = mask;
        }

        public virtual NSWindow Window
        {
            get { return GetWindow(); }
        }

        public virtual NSWindow GetWindow()
        {
            return _window;
        }

        public virtual NSArray Subviews { get { return GetSubviews(); } }

        public virtual NSArray GetSubviews()
        {
            return _sub_views;
        }

        public virtual NSView Superview  { get { return GetSuperview(); }  }

        public virtual NSView GetSuperview()
        {
            return _super_view;
        }

        public virtual bool Opaque  { get { return GetIsOpaque(); }  }

        public virtual bool GetIsOpaque()
        { 
            return false;
        }

        public virtual bool GetNeedsDisplay()
        {
            return Convert.ToBoolean(_rFlags.needs_display);
        }


        public virtual int GetTag()
        {
            return -1;
        }


        public virtual bool IsFlipped()
        {
            return false;
        }

        public virtual NSRect GetBounds()
        {
            return _bounds;
        }

        public virtual NSRect GetFrame()
        {
            return _frame;
        }

        public virtual double GetBoundsRotation()
        {
            if (_boundsMatrix != null)
            {
                return _boundsMatrix.RotationAngle();
            }

            return 0.0;
        }

        public virtual double GetFrameRotation()
        {
            if (_frameMatrix != null)
            {
                return _frameMatrix.RotationAngle();
            }
            return 0.0;
        }

        public virtual void SetNeedsDisplay(bool flag)
        {

        }
    }
}
