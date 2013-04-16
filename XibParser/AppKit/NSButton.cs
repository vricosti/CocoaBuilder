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
    // Nitesh 15 April 2013
    // Change2
    public class NSButton : NSControl
    {
        private NSButtonCell _cell;
        public NSButtonCell Cell 
        { 
            get { return _cell; } 
            protected set { _cell = value; } 
        }

        public NSButtonType ButtonType 
        { 
            get { return _cell.ButtonType; } 
            set { _cell.ButtonType = value; } 
        }

        public int HighlightsBy
        {
            get { return _cell.HighlightsBy; }
            set { _cell.HighlightsBy = value; }
        }

        public int ShowsStateBy
        {
            get { return _cell.ShowsStateBy; }
            set { _cell.ShowsStateBy = value; }
        }

        //
        // Setting the State
        //
        public int IntValue
        {
            set { _cell.State = value; }
        }


        //- (void) setIntValue: (int)anInt
        //{
        //  [self setState: (anInt != 0)];
        //}
        
        //- (void) setFloatValue: (float)aFloat
        //{
        //  [self setState: (aFloat != 0)];
        //}
        
        //- (void) setDoubleValue: (double)aDouble
        //{
        //  [self setState: (aDouble != 0)];
        //}
       

        public NSButton()
        {

        }

        public override NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                _cell = (NSButtonCell)aDecoder.DecodeObjectForKey("NSCell");
            }

            return this;
        }

    }
}
