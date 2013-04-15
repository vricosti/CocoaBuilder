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
    public class NSWindowTemplate : NSObject, NSCoding
    {
        struct NSWindowTemplateFlags
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

        private NSWindowTemplateFlags _NSWTFlags = new NSWindowTemplateFlags();
        public uint NSWTFlags { get { return (uint)EncodeNSWTFlags(); } set { DecodeNSWTFlags(value); } }

        public List<NSObject> Items { get; set; }

        public NSWindowStyleMasks StyleMask;

        public int Backing;

        public NSRect WindowRect { get; set; }

        public NSRect ScreenRect { get; set; }

        public string Title { get; set; }

        public string WindowClass { get; set; }

        public NSToolbar Toolbar { get; set; }

        public NSView WindowView { get; set; }

        public NSSize MinSize { get; set; }

        public NSSize MaxSize { get; set; }

        public bool IsRestorable { get; set; }

        public NSWindowTemplate()
        {
            Items = new List<NSObject>();
        }

        public override void EncodeWithCoder(NSObjectDecoder aCoder)
        {
            base.EncodeWithCoder(aCoder);
        }

        public override NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                StyleMask = (NSWindowStyleMasks)aDecoder.DecodeIntForKey("NSWindowStyleMask");
                Backing = aDecoder.DecodeIntForKey("NSWindowBacking");
                WindowRect = aDecoder.DecodeRectForKey("NSWindowRect");
                NSWTFlags = (uint)aDecoder.DecodeIntForKey("NSWTFlags");
                if (aDecoder.ContainsValueForKey("NSWindowTitle"))
                {
                    Title = (NSString)aDecoder.DecodeObjectForKey("NSWindowTitle");
                    StyleMask |= NSWindowStyleMasks.NSTitledWindowMask;
                }

                WindowClass = (NSString)aDecoder.DecodeObjectForKey("NSWindowClass");
                Toolbar = (NSToolbar)aDecoder.DecodeObjectForKey("NSViewClass");
                WindowView = (NSView)aDecoder.DecodeObjectForKey("NSWindowView");
                ScreenRect = (NSRect)aDecoder.DecodeRectForKey("NSScreenRect");
                IsRestorable = aDecoder.DecodeBoolForKey("NSWindowIsRestorable");

                MinSize = aDecoder.DecodeSizeForKey("NSMinSize");

                if (aDecoder.ContainsValueForKey("NSMaxSize"))
                {
                    MaxSize = aDecoder.DecodeSizeForKey("NSMaxSize");
                }
                else
                {
                    MaxSize = new NSSize((float)10e+4, (float)10e+4);
                }
            }

            return this;
        }

        private void DecodeNSWTFlags(uint nswtflags)
        {
            _NSWTFlags = PrimitiveConversion.FromLong<NSWindowTemplateFlags>(nswtflags);
            

        }

        private int EncodeNSWTFlags()
        {
           

            return (int)PrimitiveConversion.ToLong<NSWindowTemplateFlags>(_NSWTFlags);
        }

        //public static NSWindowTemplate Create(NSObjectDecoder aDecoder)
        //{
        //    NSWindowTemplate nsObj = new NSWindowTemplate();

        //    var xElement = aDecoder.XmlElement;
        //    var elements = xElement.Elements();
        //    foreach (var elm in elements)
        //    {
        //        aDecoder.XmlElement = elm;
        //        nsObj.Decode(aDecoder);
        //    }


        //    //var elementsxElement.Elements()
        //    //<int key="NSWindowStyleMask">15</int>
        //    //<int key="NSWindowBacking">2</int>
        //    //<string key="NSWindowRect">{{196, 240}, {618, 508}}</string>
        //    //<int key="NSWTFlags">544735232</int>
        //    //<string key="NSWindowTitle">Compose</string>
        //    //<string key="NSWindowClass">NSWindow</string>
        //    //<object class="NSToolbar" key="NSViewClass" id="1042547981">
        //    return nsObj;
        //}


        //protected void Decode(NSObjectDecoder aDecoder)
        //{
        //    var xElement = aDecoder.XmlElement;
        //    string key = xElement.Attribute("key").Value;
        //    switch (key)
        //    {

        //        case "NSWindowStyleMask": { StyleMask = (NSWindowStyleMask)(int)(NSNumber)aDecoder.Create(xElement); break; }
        //        case "NSWindowBacking": { Backing = (NSNumber)aDecoder.Create(xElement); break; }
        //        case "NSWindowRect": { WindowRect = (NSRect)(NSString)aDecoder.Create(xElement); break; }
        //        case "NSWTFlags": { NSWTFlags = (NSNumber)aDecoder.Create(xElement); break; }
        //        case "NSWindowTitle": { Title = (NSString)aDecoder.Create(xElement); break; }
        //        case "NSWindowClass": { WindowClass = (NSString)aDecoder.Create(xElement); break; }
        //        case "NSViewClass": { Toolbar = (NSToolbar)aDecoder.Create(xElement); break; }
        //        case "NSUserInterfaceItemIdentifier": { break; }
        //        case "NSWindowView": { View = (NSView)aDecoder.Create(xElement); break; }
        //        case "NSScreenRect": { ScreenRect = (NSString)aDecoder.Create(xElement); break; }
        //        case "NSMaxSize": { MaxSize = (NSSize)(NSString)aDecoder.Create(xElement); break; }
        //        case "NSWindowIsRestorable": { IsRestorable = (NSNumber)aDecoder.Create(xElement); break; }
        //        default:

        //            break;

        //    }
        //}

    }
}
