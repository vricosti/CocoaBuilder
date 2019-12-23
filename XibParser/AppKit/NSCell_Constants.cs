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
    public enum NSFocusRingType
    {
        NSFocusRingTypeDefault = 0,
        NSFocusRingTypeNone = 1,
        NSFocusRingTypeExterior = 2
    }

    public enum NSCellType : uint
    {
        NSNullCellType = 0,
        NSTextCellType = 1,
        NSImageCellType = 2
    }

    public enum NSCellAttribute
    {
        NSCellDisabled = 0,
        NSCellState = 1,
        NSPushInCell = 2,
        NSCellEditable = 3,
        NSChangeGrayCell = 4,
        NSCellHighlighted = 5,
        NSCellLightsByContents = 6,
        NSCellLightsByGray = 7,
        NSChangeBackgroundCell = 8,
        NSCellLightsByBackground = 9,
        NSCellIsBordered = 10,
        NSCellHasOverlappingImage = 11,
        NSCellHasImageHorizontal = 12,
        NSCellHasImageOnLeftOrBottom = 13,
        NSCellChangesContents = 14,
        NSCellIsInsetButton = 15,
        NSCellAllowsMixedState = 16
    }

    public enum NSCellImagePosition
    {
        NSNoImage = 0,
        NSImageOnly = 1,
        NSImageLeft = 2,
        NSImageRight = 3,
        NSImageBelow = 4,
        NSImageAbove = 5,
        NSImageOverlaps = 6
    }

    public enum NSCellStateValue
    {
        NSMixedState = -1,
        NSOffState = 0,
        NSOnState = 1
    }

    [Flags]
    public enum NSCellMasks : uint
    {
        NSNoCellMask = 0,
        NSContentsCellMask = 1,
        NSPushInCellMask = 2,
        NSChangeGrayCellMask = 4,
        NSChangeBackgroundCellMask = 8
    }

    /// <summary>
    /// //////////////////////////////////////////////////////
    /// </summary>
    public enum NSControlTint 
    {
        NSDefaultControlTint  = 0,
        NSBlueControlTint     = 1,
        NSGraphiteControlTint = 6,
        NSClearControlTint    = 7
    }

    public enum NSLineBreakMode
    {
        NSLineBreakByWordWrapping = 0,
        NSLineBreakByCharWrapping,
        NSLineBreakByClipping,
        NSLineBreakByTruncatingHead,
        NSLineBreakByTruncatingTail,
        NSLineBreakByTruncatingMiddle
    }

    public enum NSControlSize
    {
        NSRegularControlSize,
        NSSmallControlSize,
        NSMiniControlSize
    }


   
}
