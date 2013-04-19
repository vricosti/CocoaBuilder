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
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;
using Smartmobili.Cocoa.Utils;

namespace Smartmobili.Cocoa
{
    public enum NSBackingStoreType : uint
    {
        NSBackingStoreRetained,
        NSBackingStoreNonretained,
        NSBackingStoreBuffered
    }

    //https://github.com/gnustep/gnustep-gui/blob/master/Source/GSNibLoading.m
    public class NSWindowTemplate : NSObject, NSCoding
    {
        NSBackingStoreType _backingStoreType;
        NSSize _maxSize;
        NSSize _minSize;
        uint _windowStyle;
        NSString _title;
        id _viewClass;
        NSString _windowClass;
        NSRect _windowRect;
        NSRect _screenRect;
        id _realObject;
        id _view;
        GSWindowTemplateFlags _flags;
        NSString _autosaveName;
        Class _baseWindowClass;

#pragma warning disable 0649

        struct GSWindowTemplateFlags
        {
            [BitfieldLength(16)]
            public uint _unused;
            [BitfieldLength(2)]
            public uint style;
            [BitfieldLength(1)]
            public uint savePosition;
            [BitfieldLength(6)]
            public uint autoPositionMask;
            [BitfieldLength(1)]
            public uint dynamicDepthLimit;
            [BitfieldLength(1)]
            public uint wantsToBeColor;
            [BitfieldLength(1)]
            public uint isVisible;
            [BitfieldLength(1)]
            public uint isOneShot;
            [BitfieldLength(1)]
            public uint isDeferred;
            [BitfieldLength(1)]
            public uint isNotReleasedOnClose;
            [BitfieldLength(1)]
            public uint isHiddenOnDeactivate;
        };

#pragma warning restore 0649

        [ObjcPropAttribute("backingStore")]
        public NSBackingStoreType BackingStore
        {
            get { return _backingStoreType; }
            set { _backingStoreType = value; }
        }

        [ObjcPropAttribute("deferred", GetName = "isDeferred")]
        public bool Deferred
        {
            get { return Convert.ToBoolean(_flags.isDeferred); }
            set { _flags.isDeferred = Convert.ToUInt32(value); }
        }

        [ObjcPropAttribute("maxSize")]
        public NSSize MaxSize
        {
            get { return _maxSize; }
            set { _maxSize = value; }
        }

        [ObjcPropAttribute("minSize")]
        public NSSize MinSize
        {
            get { return _minSize; }
            set { _minSize = value; }
        }

        [ObjcPropAttribute("windowStyle")]
        public uint WindowStyle
        {
            get { return _windowStyle; }
            set { _windowStyle = value; }
        }

        [ObjcPropAttribute("title")]
        public NSString Title
        {
            get { return _title; }
            set { _title = value; }
        }

        [ObjcPropAttribute("viewClass")]
        public id ViewClass
        {
            get { return _viewClass; }
            set { _viewClass = value; }
        }

        [ObjcPropAttribute("windowRect")]
        public NSRect WindowRect
        {
            get { return _windowRect; }
            set { _windowRect = value; }
        }

        [ObjcPropAttribute("screenRect")]
        public NSRect ScreenRect
        {
            get { return _screenRect; }
            set { _screenRect = value; }
        }

        [ObjcPropAttribute("realObject")]
        public id RealObject
        {
            get { return _realObject; }
            set { _realObject = value; }
        }

        [ObjcPropAttribute("view")]
        public id View
        {
            get { return _view; }
            set { _view = value; }
        }

        [ObjcPropAttribute("className")]
        public NSString ClassName
        {
            get { return _windowClass; }
            set { _windowClass = value; }
        }

        public NSWindowTemplate()
        {
            
        }

        public override void EncodeWithCoder(NSObjectDecoder aCoder)
        {
            base.EncodeWithCoder(aCoder);
        }

        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.ContainsValueForKey("NSViewClass"))
                {
                    _viewClass = (id)aDecoder.DecodeObjectForKey("NSViewClass");
                }
                if (aDecoder.ContainsValueForKey("NSWindowClass"))
                {
                    _windowClass = (NSString)aDecoder.DecodeObjectForKey("NSWindowClass");
                }
                if (aDecoder.ContainsValueForKey("NSWindowStyleMask"))
                {
                    _windowStyle = (uint)aDecoder.DecodeIntForKey("NSWindowStyleMask");
                }
                if (aDecoder.ContainsValueForKey("NSWindowBacking"))
                {
                    _backingStoreType = (NSBackingStoreType)aDecoder.DecodeIntForKey("NSWindowBacking");
                }
                if (aDecoder.ContainsValueForKey("NSWindowView"))
                {
                    _view = (id)aDecoder.DecodeObjectForKey("NSWindowView");
                }
                if (aDecoder.ContainsValueForKey("NSWTFlags"))
                {
                    uint flags = (uint)aDecoder.DecodeIntForKey("NSWTFlags");
                    _flags = PrimitiveConversion.FromLong<GSWindowTemplateFlags>(flags);
                }
                if (aDecoder.ContainsValueForKey("NSMinSize"))
                {
                    _minSize = aDecoder.DecodeSizeForKey("NSMinSize");
                }
                if (aDecoder.ContainsValueForKey("NSMaxSize"))
                {
                    _maxSize = aDecoder.DecodeSizeForKey("NSMaxSize");
                }
                else
                {
                    _maxSize = new NSSize((float)10e+4, (float)10e+4);
                }

                if (aDecoder.ContainsValueForKey("NSWindowRect"))
                {
                    _windowRect = aDecoder.DecodeRectForKey("NSWindowRect");
                }
                if (aDecoder.ContainsValueForKey("NSFrameAutosaveName"))
                {
                    _autosaveName = (NSString)aDecoder.DecodeObjectForKey("NSFrameAutosaveName");
                }
                if (aDecoder.ContainsValueForKey("NSWindowTitle"))
                {
                    _title = (NSString)aDecoder.DecodeObjectForKey("NSWindowTitle");
                    _windowStyle |= (uint)NSWindowStyleMasks.NSTitledWindowMask;
                }

                //_baseWindowClass = [NSWindow class];
            }
            else
            {
                //[NSException raise: NSInvalidArgumentException 
                //             format: @"Can't decode %@ with %@.",NSStringFromClass([self class]),
                //             NSStringFromClass([coder class])];
            }

            //if (aDecoder.AllowsKeyedCoding)
            //{
            //    StyleMask = (NSWindowStyleMasks)aDecoder.DecodeIntForKey("NSWindowStyleMask");
            //    Backing = aDecoder.DecodeIntForKey("NSWindowBacking");
            //    WindowRect = aDecoder.DecodeRectForKey("NSWindowRect");
            //    NSWTFlags = (uint)aDecoder.DecodeIntForKey("NSWTFlags");
            //    if (aDecoder.ContainsValueForKey("NSWindowTitle"))
            //    {
            //        Title = (NSString)aDecoder.DecodeObjectForKey("NSWindowTitle");
            //        StyleMask |= NSWindowStyleMasks.NSTitledWindowMask;
            //    }

            //    WindowClass = (NSString)aDecoder.DecodeObjectForKey("NSWindowClass");
            //    Toolbar = (NSToolbar)aDecoder.DecodeObjectForKey("NSViewClass");
            //    WindowView = (NSView)aDecoder.DecodeObjectForKey("NSWindowView");
            //    ScreenRect = (NSRect)aDecoder.DecodeRectForKey("NSScreenRect");
            //    IsRestorable = aDecoder.DecodeBoolForKey("NSWindowIsRestorable");

            //    MinSize = aDecoder.DecodeSizeForKey("NSMinSize");

            //    if (aDecoder.ContainsValueForKey("NSMaxSize"))
            //    {
            //        MaxSize = aDecoder.DecodeSizeForKey("NSMaxSize");
            //    }
            //    else
            //    {
            //        MaxSize = new NSSize((float)10e+4, (float)10e+4);
            //    }
            //}

            return this;
        }

       

    }
}
