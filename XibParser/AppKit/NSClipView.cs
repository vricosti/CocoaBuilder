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
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSClipView.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSClipView.m
    public class NSClipView : NSView
    {
        new public static Class Class = new Class(typeof(NSClipView));
        new public static NSClipView Alloc() { return new NSClipView(); }

        NSView _documentView;
        //NSCursor _cursor;
        NSColor _backgroundColor;
        bool _drawsBackground;
        bool _copiesOnScroll;
        /* Cached */
        bool _isOpaque;

        public NSView DocumentView { get; set; }

        public NSClipView()
        {}

        private static NSRect IntegralRect(NSRect rect, NSView view)
        {
            NSRect dummy = new NSRect();

            //NSRect output;
            //int rounded;

            //output = [view convertRect: rect  toView: nil];

            //rounded = (int)(output.origin.x);
            //if ((CGFloat)rounded != output.origin.x)
            //  {
            //    output.origin.x = rounded + 1;
            //  }

            //rounded = (int)(output.origin.y);
            //if ((CGFloat)rounded != output.origin.y)
            //  {
            //    output.origin.y = rounded + 1;
            //  }

            //rounded = (int)(NSMaxX (output));
            //if ((CGFloat)rounded != NSMaxX (output))
            //  {
            //    output.size.width = rounded - output.origin.x;
            //  }

            //rounded = (int)(NSMaxY (output));
            //if ((CGFloat)rounded != NSMaxY (output))
            //  {
            //    output.size.height = rounded - output.origin.y;
            //  }

            //return [view convertRect: output  fromView: nil];
            //return output;

            return dummy;
        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.InitWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {

            }
            return self;
        }
    }
}
