using Smartmobili.Cocoa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{

    public enum NSPopUpArrowPosition
    {
        NSPopUpNoArrow = 0,
        NSPopUpArrowAtCenter = 1,
        NSPopUpArrowAtBottom = 2
    }


    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSPopUpButtonCell.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSPopUpButtonCell.m
    public class NSPopUpButtonCell : NSMenuItemCell
    {
        new public static Class Class = new Class(typeof(NSPopUpButtonCell));

        public struct _pbcFlags
        {
            [BitfieldLength(1)]
            uint pullsDown;
            [BitfieldLength(3)]
            uint preferredEdge;
            [BitfieldLength(1)]
            uint usesItemFromMenu;
            [BitfieldLength(1)]
            uint altersStateOfSelectedItem;
            [BitfieldLength(2)]
            uint arrowPosition;
        }
    }
}
