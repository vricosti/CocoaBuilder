using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSPopUpButton_Class/Reference/Reference.html
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSPopUpButton.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSPopUpButton.m
    public class NSPopUpButton : NSButton
    {
        public NSPopUpButton()
        {

        }

        [ObjcProp("menu")]
        public NSMenu Menu 
        {
            get { return _cell.Menu; }
            set { _cell.Menu = value; }
        }

        //[ObjcProp("pullsDown")]
        //public bool PullsDown
        //{
        //    get { return _cell.Pu; }
        //    set { _cell.Menu = value; }
        //}

    }
}
