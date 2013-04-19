using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa.AppKit
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSToolbarItem.m
    public class GSToolbarButton : NSButton
    {
        NSToolbarItem _toolbarItem;


        public id InitWithToolbarItem(NSToolbarItem toolbarItem)
        {
            id self = null;

           // Frame will be reset by the layout method
            //self = [super initWithFrame: NSMakeRect(ItemBackViewX, ItemBackViewY, ItemBackViewDefaultWidth, ItemBackViewDefaultHeight)]; 
  
  if (self != null)
    {

      _toolbarItem = toolbarItem;

     
      //this.TIt
      //[self setTitle: @""];
      //[self setEnabled: NO];
      //[_cell setBezeled: YES];
      //[self setImagePosition: NSImageAbove];
      //[self setHighlightsBy: 
      //          NSChangeGrayCellMask | NSChangeBackgroundCellMask];
      //[self setFont: NormalFont]; 
    }

  return self; 
        }

    }
}
