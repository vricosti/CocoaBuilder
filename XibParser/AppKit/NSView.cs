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
using System.Xml.Linq;

namespace Smartmobili.Cocoa
{
    [FlagsAttribute]
    public enum NSViewAutoresizingMasks
    {
        NSViewAutoresizingMask = 0x3F,
        NSViewAutoresizesSubviewsMask = 1 << 8,
        NSViewHiddenMask = 1 << 31
    }

    public class NSView : NSResponder
    {
        new public static Class Class = new Class(typeof(NSView));

        //<int key="NSvFlags">256</int>
        public int NSvFlags { get; set; }

        //<string key="NSFrame">{{-0, 479}, {618, 29}}</string>
        //<string key="NSFrameSize">{618, 367}</string>
        public NSRect Frame { get; set; }

        public object Superview { get; set; }

        public object Window { get; set; }

        public object PrevKeyView { get; set; }

        public object NextKeyView { get; set; }

        public string ReuseIdentifierKey { get; set; }

        //<string key="NSOffsets">{0, 0}</string>
        public NSPoint Offsets { get; set; }

        

        public string ClassName { get; set; }


        //<string key="NSFrame">{{-0, 479}, {618, 29}}</string>
        //                    <reference key="NSSuperview" ref="1006"/>
        //                    <reference key="NSWindow"/>
        //                    <reference key="NSNextKeyView" ref="462115558"/>
        //                    <string key="NSReuseIdentifierKey">_NS:1192</string>
        //                    <string key="NSClassName">NSView</string>



        public NSMutableArray SubViews { get; set; }

        public NSView()
        {
            SubViews = new NSMutableArray();
        }


        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.ContainsValueForKey("NSFrame"))
                {
                    Frame = aDecoder.DecodeRectForKey("NSFrame");
                }
                else if (aDecoder.ContainsValueForKey("NSFrameSize"))
                {
                    Frame = aDecoder.DecodeSizeForKey("NSFrameSize");
                }

                PrevKeyView = aDecoder.DecodeObjectForKey("NSPreviousKeyView");
                NextKeyView = aDecoder.DecodeObjectForKey("NSNextKeyView");

                NSvFlags = aDecoder.DecodeIntForKey("NSvFlags");
                SubViews = (NSMutableArray)aDecoder.DecodeObjectForKey("NSSubviews");

                Window = aDecoder.DecodeObjectForKey("NSWindow");
                ClassName = (NSString)aDecoder.DecodeObjectForKey("NSWindow");

                Offsets = aDecoder.DecodePointForKey("NSOffsets");

                Superview = aDecoder.DecodeObjectForKey("NSSuperview");


                //foreach (var xElement in aDecoder.XmlElement.Elements())
                //{
                //    string key = xElement.Attribute("key").Value;
                //    switch (key)
                //    {
                //        //case "NSNextResponder": { NextResponder = (NSResponder)aDecoder.Create(xElement); break; }
                //        case "NSvFlags": { NSvFlags = (NSNumber)aDecoder.Create(xElement); break; }
                //        case "NSSubviews": { SubViews = (NSMutableArray)aDecoder.Create(xElement); break; }
                //        //case "NSFrame": { Frame = (NSRect)(NSString)aDecoder.Create(xElement); break; }
                //        //case "NSFrameSize": { Frame = (NSSize)(NSString)aDecoder.Create(xElement); break; }
                //        case "NSSuperview": { Superview = aDecoder.Create(xElement); break; }
                //        case "NSWindow": { Window = aDecoder.Create(xElement); break; }
                //        case "NSReuseIdentifierKey": { ReuseIdentifierKey = (NSString)aDecoder.Create(xElement); break; }
                //        case "NSOffsets": { Offsets = (NSPoint)(NSString)aDecoder.Create(xElement); break; }
                //        case "NSClassName": { ClassName = (NSString)aDecoder.Create(xElement); break; }

                //        default:
                //            //System.Diagnostics.Debug.WriteLine("IBConnectionRecord : unknown key " + key);
                //            break;

                //    }
                //}
            }

            return this;
        }


        public void SetNeedsDisplay(bool flag)
        {

        }
    }
}
