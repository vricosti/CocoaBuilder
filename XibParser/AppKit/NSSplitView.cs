using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSSplitView_Class/Reference/Reference.html
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSSplitView.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSSplitView.m
    
    public enum NSSplitViewDividerStyle
    {
        NSSplitViewDividerStyleThick = 1,
        NSSplitViewDividerStyleThin = 2,
    }
    
    public class NSSplitView : NSView
    {
        new public static Class Class = new Class(typeof(NSSplitView));
        new public static NSSplitView Alloc() { return new NSSplitView(); }

        protected id _delegate;
        protected NSImage _dimpleImage;
        protected NSColor _backgroundColor;
        protected NSColor _dividerColor;
        protected NSString _autosaveName;
        protected float _dividerWidth;
        protected float _draggedBarWidth;
        protected bool _isVertical;
        protected bool _never_displayed_before;
        protected bool _is_pane_splitter;

        static NSSplitView() { Initialize(); }
        public static void Initialize()
        {
            
        }

    }
}
