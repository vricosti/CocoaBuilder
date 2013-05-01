﻿/*
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
    public class NSSegmentedControl : NSControl
    {
        new public static Class Class = new Class(typeof(NSSegmentedControl));

        private static Class segmentedControlCellClass;


        static NSSegmentedControl() { Initialize(); }
        new static void Initialize()
        {
            segmentedControlCellClass = NSSegmentedCell.Class;
        }


        new public static Class CellClass
        {
            get { return segmentedControlCellClass; }
        }

        //public int SegmentCount
        //{
        //    get { [_cell setSegmentCount: count]; }
        //    set { ((NSButtonCell)_cell).HighlightsBy = value; }
        //}




    }
}