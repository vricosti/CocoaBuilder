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
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSColor.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSColor.m

    public enum NSControlTint : uint 
    {
        NSDefaultControlTint,
        NSBlueControlTint,
        NSGraphiteControlTint = 6,
        NSClearControlTint
    }

    public enum NSControlSize : uint
    {
        NSRegularControlSize,
        NSSmallControlSize,
        NSMiniControlSize
    }


    public class NSColor : NSObject
    {
        new public static Class Class = new Class(typeof(NSColor));

        //<int key="NSColorSpace">3</int>
        //<bytes key="NSWhite">MCAwLjUxAA</bytes>


        new public static NSColor Alloc()
        {
            return new NSColor();
        }

        public NSColor()
        {
        }


        public static NSColor TextColor
        {
            get { return new NSColor(); }
        }


        public static NSColor TextBackgroundColor
        {
            get { return new NSColor(); }
        }


        public override id InitWithCoder(NSCoder decoder)
        {
            base.InitWithCoder(decoder);

            //foreach (var xElement in decoder.XmlElement.Elements())
            //{
            //    string key = xElement.Attribute("key").Value;
            //    switch (key)
            //    {

                   

            //        default:
            //            System.Diagnostics.Debug.WriteLine("NSColor : unknown key " + key);
            //            break;

            //    }
            //}

            return this;
        }

      

    }
}
