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
    public enum NSBoxType
    {
        //Specifies the primary box appearance. This is the default box type.
        NSBoxPrimary = 0,
        
        //Specifies the secondary box appearance.
        NSBoxSecondary = 1,

        //Specifies that the box is a separator.
        NSBoxSeparator = 2,

        //Specifies that the box is an OS X v10.2–style box.
        NSBoxOldStyle = 3,

        //Specifies that the appearance of the box is determined entirely by the by box-configuration methods, 
        //without automatically applying Apple human interface guidelines.
        NSBoxCustom = 4
    }

    public enum NSBorderType
    {
        //No border.
        NSNoBorder     = 0,

        //A black line border around the view.
        NSLineBorder   = 1,

        //A concave border that makes the view look sunken.
        NSBezelBorder  = 2,

        //A thin border that looks etched around the image.
        NSGrooveBorder = 3
    };

    public enum NSTitlePosition
    {
        NSNoTitle     = 0,
        NSAboveTop    = 1,
        NSAtTop       = 2,
        NSBelowTop    = 3,
        NSAboveBottom = 4,
        NSAtBottom    = 5,
        NSBelowBottom = 6
    }
    



    public class NSBox : NSView
    {
        public NSBorderType BorderType { get; set; }

        public NSBoxType BoxType { get; set; }

        public NSTitlePosition TitlePosition { get; set; }

        public NSSize ContentViewMargins { get; set; }

        public object TitleCell { get; set; }

        public bool Transparent { get; set; }

        public NSBox()
        {

        }




        public override NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                BorderType = (NSBorderType)aDecoder.DecodeIntForKey("NSBorderType");
                BoxType = (NSBoxType)aDecoder.DecodeIntForKey("NSBoxType");
                TitlePosition = (NSTitlePosition)aDecoder.DecodeIntForKey("NSTitlePosition");
                ContentViewMargins = (NSSize)aDecoder.DecodeSizeForKey("NSOffsets");
                TitleCell = aDecoder.DecodeObjectForKey("NSTitleCell");
                Transparent = aDecoder.DecodeBoolForKey("NSTransparent");
            }

            return this;
        }
    }
}
