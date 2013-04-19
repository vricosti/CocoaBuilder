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
using Smartmobili.Cocoa.Utils;

namespace Smartmobili.Cocoa
{
    public class NSScrollView : NSView
    {
#pragma warning disable 0649

        struct NSScrollViewFlags
        {
            [BitfieldLength(2)]
            public uint border;
            [BitfieldLength(1)]
            public uint vScrollerRequired;
            [BitfieldLength(1)]
            public uint hScrollerRequired;
            [BitfieldLength(1)]
            public uint hasVScroller;
            [BitfieldLength(1)]
            public uint hasHScroller;
            [BitfieldLength(1)]
            public uint nonDynamic;
            [BitfieldLength(1)]
            public uint oldRulerInstalled;
            [BitfieldLength(1)]
            public uint showRulers;
            [BitfieldLength(1)]
            public uint hasHRuler;
            [BitfieldLength(1)]
            public uint hasVRuler;
            [BitfieldLength(1)]
            public uint __unused1;
            [BitfieldLength(1)]
            public uint doesNotDrawBackground;
            [BitfieldLength(1)]
            public uint __unused2;
            [BitfieldLength(1)]
            public uint __unused3;
            [BitfieldLength(1)]
            public uint __unused4;
            [BitfieldLength(1)]
            public uint autohidesScrollers;
            [BitfieldLength(1)]
            public uint __unused5;
            [BitfieldLength(14)]
            public uint __unused6;
        };

#pragma warning restore 0649

        private NSScrollViewFlags _scrollViewFlags = new NSScrollViewFlags();
        public uint NSsFlags { get { return (uint)EncodeNSsFlags(); } set { DecodeNSsFlags(value); } }

        public NSBorderType BorderType { get; set; }

        public NSClipView ContentView { get; set; }

        public NSView DocumentView { get; set; }

        public bool VerticalScrollerRequired { get; set; }

        public bool HorizontalScrollerRequired { get; set; }

        public bool HasHorizontalScroller { get; set; }

        public bool HasVerticalScroller { get; set; }

        public bool AutoHidesScrollers { get; set; }

        public NSScroller HorizontalScroller { get; set; }

        public NSScroller VerticalScroller { get; set; }

        public bool RulersVisible { get; set; }

        public bool ShowRulers { get; set; }

        public bool HasHorizontalRuler { get; set; }

        public bool HasVerticalRuler { get; set; }

        public NSRulerView HorizontalRulerView { get; set; }

        public NSRulerView VerticalRulerView { get; set; }

        //<int key="NSsFlags">133680</int>
        //                            <reference key="NSVScroller" ref="138269728"/>
        //                            <reference key="NSHScroller" ref="876693224"/>
        //                            <reference key="NSContentView" ref="204776699"/>

        public NSScrollView()
        {
        }

        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                NSScroller hScroller = (NSScroller)aDecoder.DecodeObjectForKey("NSHScroller");
                NSScroller vScroller = (NSScroller)aDecoder.DecodeObjectForKey("NSVScroller");
                NSClipView content = (NSClipView)aDecoder.DecodeObjectForKey("NSContentView");

                if (aDecoder.ContainsValueForKey("NSsFlags"))
                {
                    NSsFlags = (uint)aDecoder.DecodeIntForKey("NSsFlags");
                }
            }

            return this;
        }

        private void DecodeNSsFlags(uint nssflags)
        {
            _scrollViewFlags = PrimitiveConversion.FromLong<NSScrollViewFlags>(nssflags);
            BorderType = (NSBorderType)_scrollViewFlags.border;
            VerticalScrollerRequired = Convert.ToBoolean(_scrollViewFlags.vScrollerRequired);
            HorizontalScrollerRequired = Convert.ToBoolean(_scrollViewFlags.hScrollerRequired);
            HasVerticalScroller = Convert.ToBoolean(_scrollViewFlags.hasVScroller);
            HasHorizontalScroller = Convert.ToBoolean(_scrollViewFlags.hasHScroller);
            ShowRulers = Convert.ToBoolean(_scrollViewFlags.showRulers);
            HasHorizontalRuler = Convert.ToBoolean(_scrollViewFlags.hasHRuler);
            HasVerticalRuler = Convert.ToBoolean(_scrollViewFlags.hasVRuler);


            //NSScrollViewFlags scrollViewFlags = new NSScrollViewFlags();
            //scrollViewFlags = 10;

        }

        private int EncodeNSsFlags()
        {
            _scrollViewFlags.border = (uint)BorderType;
            _scrollViewFlags.vScrollerRequired = (uint)((VerticalScrollerRequired == true) ? 1 : 0);
            _scrollViewFlags.hScrollerRequired = (uint)((HorizontalScrollerRequired == true) ? 1 : 0);
            _scrollViewFlags.hasVScroller = (uint)((HasVerticalScroller == true) ? 1 : 0);
            _scrollViewFlags.hasHScroller = (uint)((HasHorizontalScroller == true) ? 1 : 0);
            _scrollViewFlags.showRulers = (uint)((ShowRulers == true) ? 1 : 0);
            _scrollViewFlags.hasHRuler = (uint)((HasHorizontalRuler == true) ? 1 : 0);
            _scrollViewFlags.hasVRuler = (uint)((HasVerticalRuler == true) ? 1 : 0);

            return (int)PrimitiveConversion.ToLong<NSScrollViewFlags>(_scrollViewFlags);
        }


    }
}
