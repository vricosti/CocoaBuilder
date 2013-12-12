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
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSMenuItem.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSMenuItem.m
    public class NSMenuItem : NSObject
    {
        new public static Class Class = new Class(typeof(NSMenuItem));

        protected NSMenu _menu;
        protected NSString _title;
        protected NSString _keyEquivalent;
        protected uint _keyEquivalentModifierMask;
        protected uint _mnemonicLocation;
        protected int _state;
        protected NSImage _image;
        protected NSImage _onStateImage;
        protected NSImage _offStateImage;
        protected NSImage _mixedStateImage;
        protected id _target;
        protected SEL _action;
        protected int _tag;
        protected id _representedObject;
        protected NSMenu _submenu;
        protected bool _enabled;
        protected bool _changesState;
        protected bool _isAlternate;
        protected char _indentation; // 0..15
        protected NSString _toolTip;

        public NSMenu Menu { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsSeparator { get; set; }
        
        public string Title { get; set; }

        public NSMenu SubMenu { get; set; }


        [ObjcPropAttribute("enabled", GetName = "isEnabled")]
        public virtual bool Enabled
        {
            get { return _enabled; }
            set 
            { 
                if (value == _enabled)
                    return;

                _enabled = value;
                //_menu.itemChanged: self];
            }
        }

        public NSMenuItem()
        {

        }


        public override id initWithCoder(NSCoder aDecoder)
        {
            base.initWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                //NSString title;
                //NSString action;
                //NSString key;
                bool isSeparator = false;
                //int keyMask;

                if (aDecoder.containsValueForKey(@"NSIsSeparator"))
                {
                    isSeparator = aDecoder.decodeBoolForKey(@"NSIsSeparator");
                }

                Title = (NSString)aDecoder.decodeObjectForKey("NSTitle");

                IsDisabled = aDecoder.decodeBoolForKey("NSIsDisabled");

                IsSeparator = aDecoder.decodeBoolForKey("NSIsSeparator");

                SubMenu = (NSMenu)aDecoder.decodeObjectForKey("NSSubmenu");
            }

            return this;
        }
    }
}
