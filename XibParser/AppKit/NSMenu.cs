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

namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSMenu.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSMenu.m
    public class NSMenu : NSObject
    {
        new public static Class Class = new Class(typeof(NSMenu));

        public struct GSMenuFlags
        {
            [BitfieldLength(1)]
            public uint changedMessagesEnabled;
            [BitfieldLength(1)]
            public uint autoenable;
            [BitfieldLength(1)]
            public uint needsSizing;
            [BitfieldLength(1)]
            public uint is_tornoff;
            [BitfieldLength(1)]
            public uint transient;
            [BitfieldLength(1)]
            public uint horizontal;
            [BitfieldLength(1)]
            public uint mainMenuChanged;
            [BitfieldLength(25)]
            public uint unused;
        }

        protected NSString _title;
        protected NSMutableArray _items;
        protected NSMenuView _view;
        protected NSMenu _superMenu;
        protected NSMenu _attachedMenu;
        protected NSMutableArray _notifications;
        protected id _delegate;
        protected GSMenuFlags _menu;

        private NSWindow _aWindow;
        private NSWindow _bWindow;
        private NSMenu _oldAttachedMenu;
        private int _oldHiglightedIndex;
        private NSString _name;


        //static NSZone* menuZone = NULL;
        static NSString NSMenuLocationsKey = @"NSMenuLocations";
        static NSString NSEnqueuedMenuMoveName = @"EnqueuedMoveNotificationName";
        //static NSNotificationCenter nc;
        static bool menuBarVisible = true;





        public string Title { get; set; }

        public NSMutableArray MenuItems { get; set; }

        public string Name { get; set; }

        


        public static bool MenuBarVisible
        {
            get { return menuBarVisible; }
            set { menuBarVisible = value; }
        }

        new public static NSMenu Alloc()
        {
            return new NSMenu();
        }

        public override id Init()
        {
            return InitWithTitle(NSProcessInfo.ProcessInfo.ProcessName);
        }

        public virtual id InitWithTitle(NSString aTitle)
        {
            id self = this;
            NSMenuView menuRep;

            if (base.Init() == null)
                return null;

            _title = aTitle;
            _items = (NSMutableArray)NSMutableArray.Alloc().Init();
            _menu.changedMessagesEnabled = 1;
            _notifications = (NSMutableArray)NSMutableArray.Alloc().Init();
            _menu.needsSizing = 1;
            // According to the spec, menus do autoenable by default.
            _menu.autoenable = 1;

            // Create the windows that will display the menu.
            //_aWindow = [self _CreateWindow];
            //_bWindow = [self _CreateWindow];
            //[_bWindow setLevel: NSPopUpMenuWindowLevel];

            // Create a NSMenuView to draw our menu items.
            menuRep = (NSMenuView)NSMenuView.Alloc().InitWithFrame(NSRect.Zero);
            //[self setMenuRepresentation: menuRep];
           

            return self;
        }


        public override id InitWithCoder(NSCoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                Title = (NSString)aDecoder.DecodeObjectForKey("NSTitle");

                MenuItems = (NSMutableArray)aDecoder.DecodeObjectForKey("NSMenuItems");

                Name = (NSString)aDecoder.DecodeObjectForKey("NSName");
            }

            return this;
        }



    }
}
