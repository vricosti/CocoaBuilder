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
    public enum NSEventType
    {
        NSLeftMouseDown      = 1,
        NSLeftMouseUp        = 2,
        NSRightMouseDown     = 3,
        NSRightMouseUp       = 4,
        NSMouseMoved         = 5,
        NSLeftMouseDragged   = 6,
        NSRightMouseDragged  = 7,
        NSMouseEntered       = 8,
        NSMouseExited        = 9,
        NSKeyDown            = 10,
        NSKeyUp              = 11,
        NSFlagsChanged       = 12,
        NSAppKitDefined      = 13,
        NSSystemDefined      = 14,
        NSApplicationDefined = 15,
        NSPeriodic           = 16,
        NSCursorUpdate       = 17,
        NSScrollWheel        = 22,
        NSTabletPoint        = 23,
        NSTabletProximity    = 24,
        NSOtherMouseDown     = 25,
        NSOtherMouseUp       = 26,
        NSOtherMouseDragged  = 27,
        NSEventTypeGesture   = 29,
        NSEventTypeMagnify   = 30,
        NSEventTypeSwipe     = 31,
        NSEventTypeRotate    = 18,
        NSEventTypeBeginGesture = 19,
        NSEventTypeEndGesture   = 20,
        NSEventTypeSmartMagnify = 32,
        NSEventTypeQuickLook   = 33
    }


    [Flags]
    public enum NSEventMask
    {
        NSLeftMouseDownMask      = 1 << NSEventType.NSLeftMouseDown,
        NSLeftMouseUpMask        = 1 << NSEventType.NSLeftMouseUp,
        NSRightMouseDownMask     = 1 << NSEventType.NSRightMouseDown,
        NSRightMouseUpMask       = 1 << NSEventType.NSRightMouseUp,
        NSMouseMovedMask         = 1 << NSEventType.NSMouseMoved,
        NSLeftMouseDraggedMask   = 1 << NSEventType.NSLeftMouseDragged,
        NSRightMouseDraggedMask  = 1 << NSEventType.NSRightMouseDragged,
        NSMouseEnteredMask       = 1 << NSEventType.NSMouseEntered,
        NSMouseExitedMask        = 1 << NSEventType.NSMouseExited,
        NSKeyDownMask            = 1 << NSEventType.NSKeyDown,
        NSKeyUpMask              = 1 << NSEventType.NSKeyUp,
        NSFlagsChangedMask       = 1 << NSEventType.NSFlagsChanged,
        NSAppKitDefinedMask      = 1 << NSEventType.NSAppKitDefined,
        NSSystemDefinedMask      = 1 << NSEventType.NSSystemDefined,
        NSApplicationDefinedMask = 1 << NSEventType.NSApplicationDefined,
        NSPeriodicMask           = 1 << NSEventType.NSPeriodic,
        NSCursorUpdateMask       = 1 << NSEventType.NSCursorUpdate,
        NSScrollWheelMask        = 1 << NSEventType.NSScrollWheel,
        NSTabletPointMask        = 1 << NSEventType.NSTabletPoint,
        NSTabletProximityMask    = 1 << NSEventType.NSTabletProximity,
        NSOtherMouseDownMask     = 1 << NSEventType.NSOtherMouseDown,
        NSOtherMouseUpMask       = 1 << NSEventType.NSOtherMouseUp,
        NSOtherMouseDraggedMask  = 1 << NSEventType.NSOtherMouseDragged,
        NSEventMaskGesture       = 1 << NSEventType.NSEventTypeGesture,
        NSEventMaskMagnify       = 1 << NSEventType.NSEventTypeMagnify,
        //NSEventMaskSwipe         = 1U << NSEventType.NSEventTypeSwipe,
        NSEventMaskRotate        = 1 << NSEventType.NSEventTypeRotate,
        NSEventMaskBeginGesture  = 1 << NSEventType.NSEventTypeBeginGesture,
        NSEventMaskEndGesture    = 1 << NSEventType.NSEventTypeEndGesture,
        //NSEventMaskSmartMagnify  = 1ULL << NSEventType.NSEventTypeSmartMagnify,
        //NSAnyEventMask           = 0xffffffffU
}

    public class NSEvent
    {
        public NSEvent()
        {
        }
    }
}
