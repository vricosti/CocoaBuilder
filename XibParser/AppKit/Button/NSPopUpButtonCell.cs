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

        [ObjcMethodAttribute("initWithCoder")]
        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;
            NSMenu menu;

            if (base.initWithCoder(aDecoder) == null)
                return null;

              if (aDecoder.AllowsKeyedCoding)
              {
                  menu = (NSMenu)aDecoder.decodeObjectForKey(@"NSMenu");
                  this.Menu = null;
                  this.Menu = menu;

                  if (aDecoder.containsValueForKey(@"NSAltersState"))
                  {

                  }
                  if (aDecoder.containsValueForKey(@"NSPullDown"))
                  {

                  }
                  if (aDecoder.containsValueForKey(@"NSUsesItemFromMenu"))
                  {

                  }
                  if (aDecoder.containsValueForKey(@"NSArrowPosition"))
                  {

                  }
                  if (aDecoder.containsValueForKey(@"NSPreferredEdge"))
                  {

                  }
                  if (aDecoder.containsValueForKey(@"NSSelectedIndex"))
                  {

                  }
                  if (aDecoder.containsValueForKey(@"NSMenuItem"))
                  {

                  }

              }


            return self;
        }


    }
}
