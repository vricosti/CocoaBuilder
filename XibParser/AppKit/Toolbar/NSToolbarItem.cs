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
using System.Windows;
using System.Xml.Linq;

namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSToolbarItem.m
    public class NSToolbarItem : NSObject
    {
        new public static Class Class = new Class(typeof(NSToolbarItem));

        bool _autovalidates;
        NSString _itemIdentifier;
        NSString _label;
        NSString _paletteLabel;
        NSImage _image;
        id _view;
        NSMenuItem _menuFormRepresentation;
        NSString _toolTip;
        int _tag;
        int _visibilityPriority;

        // toolbar
        NSToolbar _toolbar;
        NSView _backView;
        bool _modified;
        bool _selectable;
        bool _isUserRemovable;

        // size
        NSSize _maxSize;
        NSSize _minSize;

        //FIXME : doesn't exist in GNUSTep
        bool _enabled;



        public NSString ItemIdentifier
        {
            get { return _itemIdentifier;  }
            set { _itemIdentifier = value; }
        }

        public NSString Label
        {
            get { return _label; }
            set { _label = value; }
        }

        public NSString PaletteLabel
        {
            get { return _paletteLabel; }
            set { _paletteLabel = value; }
        }

        public NSImage Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public NSString ToolTip
        {
            get { return _toolTip; }
            set { _toolTip = value; }
        }

        public NSSize MaxSize
        {
            get { return _maxSize; }
            set { _maxSize = value; }
        }

        public NSSize MinSize
        {
            get { return _minSize; }
            set { _minSize = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool Autovalidates
        {
            get { return _autovalidates; }
            set { _autovalidates = value; }
        }

        public int Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public int VisibilityPriority
        {
            get { return _visibilityPriority; }
            set { _visibilityPriority = value; }
        }

        public NSView View
        {
            get { return (NSView)_view; }
            set { _view = value; }
        }

        public bool IsUserRemovable
        {
            get { return _isUserRemovable; }
            set { _isUserRemovable = value; }
        }

        public NSToolbarItem()
        {

        }

        //public id InitWithItemIdentifier(NSString itemIdentifier)
        //{

        //}


        //public NSToolbarItem(NSObjectDecoder aDecoder)
        //    : base(aDecoder)
        //{
            
        //}

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                ItemIdentifier = (NSString)aDecoder.DecodeObjectForKey("NSToolbarItemIdentifier");

                Label = (NSString)aDecoder.DecodeObjectForKey("NSToolbarItemLabel");
                PaletteLabel = (NSString)aDecoder.DecodeObjectForKey("NSToolbarItemPaletteLabel");
                ToolTip = (NSString)aDecoder.DecodeObjectForKey("NSToolbarItemToolTip");
                View = (NSView)aDecoder.DecodeObjectForKey("NSToolbarItemView");
                Image = (NSImage)aDecoder.DecodeObjectForKey("NSToolbarItemImage");
                //Target = (NSString)aDecoder.DecodeObjectForKey("NSToolbarItemTarget");
                //Action = (NSString)aDecoder.DecodeObjectForKey("NSToolbarItemAction");
                MinSize = (NSSize)(NSString)aDecoder.DecodeObjectForKey("NSToolbarItemMinSize");
                MaxSize = (NSSize)(NSString)aDecoder.DecodeObjectForKey("NSToolbarItemMaxSize");
                Enabled = aDecoder.DecodeBoolForKey("NSToolbarItemEnabled");
                Autovalidates = aDecoder.DecodeBoolForKey("NSToolbarItemAutovalidates");
                Tag = aDecoder.DecodeIntForKey("NSToolbarItemTag");
                IsUserRemovable = aDecoder.DecodeBoolForKey("NSToolbarIsUserRemovable");
                VisibilityPriority = aDecoder.DecodeIntForKey("NSToolbarItemVisibilityPriority");
            }

            return self;
        }
    }
}
