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
using Smartmobili.Cocoa.Utils;

namespace Smartmobili.Cocoa
{
    public enum NSButtonType
    {
        NSMomentaryLightButton = 0,
        NSPushOnPushOffButton = 1,
        NSToggleButton = 2,
        NSSwitchButton = 3,
        NSRadioButton = 4,
        NSMomentaryChangeButton = 5,
        NSOnOffButton = 6,
        NSMomentaryPushInButton = 7,
        NSMomentaryPushButton = 0,
        NSMomentaryLight = 7
    }

    public enum NSBezelStyle
    {
        NSRoundedBezelStyle = 1,
        NSRegularSquareBezelStyle = 2,
        NSThickSquareBezelStyle = 3,
        NSThickerSquareBezelStyle = 4,
        NSDisclosureBezelStyle = 5,
        NSShadowlessSquareBezelStyle = 6,
        NSCircularBezelStyle = 7,
        NSTexturedSquareBezelStyle = 8,
        NSHelpButtonBezelStyle = 9,
        NSSmallSquareBezelStyle = 10,
        NSTexturedRoundedBezelStyle = 11,
        NSRoundRectBezelStyle = 12,
        NSRecessedBezelStyle = 13,
        NSRoundedDisclosureBezelStyle = 14,
        NSInlineBezelStyle = 15,
        NSSmallIconButtonBezelStyle = 2
    }

    public enum NSGradientType
    {
        NSGradientNone = 0,
        NSGradientConcaveWeak = 1,
        NSGradientConcaveStrong = 2,
        NSGradientConvexWeak = 3,
        NSGradientConvexStrong = 4
    }



    public class NSButtonCell : NSActionCell
    {
        // From GNUstep
        public struct WSButtonCellFlags
        {
            [BitfieldLength(8)]
            public uint unused2; // alt mnemonic loc.
            [BitfieldLength(1)]
            public uint useButtonImageSource;
            [BitfieldLength(6)]
            public uint unused1; // inset:2 doesn't dim:1 gradient:3
            [BitfieldLength(1)]
            public uint isTransparent;
            [BitfieldLength(1)]
            public uint lastState;
            [BitfieldLength(1)]
            public uint hasKeyEquiv;
            [BitfieldLength(1)]
            public uint isImageSizeDiff;
            [BitfieldLength(1)]
            public uint isImageAndText;
            [BitfieldLength(1)]
            public uint isBottomOrLeft;
            [BitfieldLength(1)]
            public uint isHorizontal;
            [BitfieldLength(1)]
            public uint imageDoesOverlap;
            [BitfieldLength(1)]
            public uint isBordered;
            [BitfieldLength(1)]
            public uint drawing;
            [BitfieldLength(1)]
            public uint highlightByGray;
            [BitfieldLength(1)]
            public uint highlightByBackground;
            [BitfieldLength(1)]
            public uint highlightByContents;
            [BitfieldLength(1)]
            public uint changeGray;
            [BitfieldLength(1)]
            public uint changeBackground;
            [BitfieldLength(1)]
            public uint changeContents;
            [BitfieldLength(1)]
            public uint isPushin;
        };

        struct WSButtonCellFlags2 
        {
            [BitfieldLength(3)]
            uint bezelStyle;
            [BitfieldLength(1)]
            uint showsBorderOnlyWhileMouseInside;
            [BitfieldLength(1)]
            uint mouseInside;
            [BitfieldLength(1)]
            uint bezelStyle2;
            [BitfieldLength(2)]
            uint imageScaling;
            [BitfieldLength(24)]
            uint keyEquivalentModifierMask;
        };


        public NSButtonType ButtonType { get; set; }

        public string AlternateTitle { get; set; }

        // TODO
        //public NSAttributedString AttributedTitle { get; set; }
        // TODO
        //public NSAttributedString AttributedAlternateTitle { get; set; }

        public string Title { get; set; }

        public NSFont Font { get; set; }

        // inherited
        //public NSImage Image { get; set; }

        public NSImageScaling ImageScaling { get; set; }

        public NSColor BackgroundColor { get; set; }
         public NSGradientType GradientType { get; set; }

        public NSImage AlternateImage { get; set; }

        public NSCellImagePosition ImagePosition { get; set; }

        public bool IsBordered { get; set; }

        public bool IsTransparent { get; set; }

        public NSBezelStyle BezelStyle { get; set; }

        public bool ShowsBorderOnlyWhileMouseInside { get; set; }

        // inherited
        //public bool AllowsMixedState { get; set; }

        public NSCellStateValue State { get; set; }

        public int PeriodicDelay { get; set; }

        public int PeriodicInterval { get; set; }

        private int _highlightsByMask;
        public int HighlightsBy { get { return _highlightsByMask; } set { _highlightsByMask = value; } }

        public NSCellMasks ShowsStateBy { get; set; }

        public bool ImageDimsWhenDisabled { get; set; }

        public string KeyEquivalent { get; set; }


        public NSButtonCell()
        {

        }

        //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSButtonCell.m
        public override NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                int delay = 0;
                int interval = 0;

                if (aDecoder.ContainsValueForKey("NSKeyEquivalent"))
                {
                    KeyEquivalent = (NSString)aDecoder.DecodeObjectForKey("NSKeyEquivalent");
                }
                if (aDecoder.ContainsValueForKey("NSNormalImage"))
                {
                    Image = (NSImage)aDecoder.DecodeObjectForKey("NSKeyEquivalent");
                }
                if (aDecoder.ContainsValueForKey("NSAlternateContents"))
                {
                    AlternateTitle = (NSString)aDecoder.DecodeObjectForKey("NSAlternateContents");
                }
                if (aDecoder.ContainsValueForKey("NSButtonFlags"))
                {
                    uint bFlags = (uint)aDecoder.DecodeIntForKey("NSButtonFlags");
                    WSButtonCellFlags buttonCellFlags = PrimitiveConversion.FromLong<WSButtonCellFlags>(bFlags);


                    //[self setTransparent: buttonCellFlags.isTransparent];
                    this.IsTransparent = Convert.ToBoolean(buttonCellFlags.isTransparent);
                    //[self setBordered: buttonCellFlags.isBordered];
                    this.IsBordered = Convert.ToBoolean(buttonCellFlags.isBordered);

                    //[self setCellAttribute: NSPushInCell
                    //      to: buttonCellFlags.isPushin];
                    this.SetCellAttribute(NSCellAttribute.NSPushInCell, (int)buttonCellFlags.isPushin);
                    //[self setCellAttribute: NSCellLightsByBackground
                    //      to: buttonCellFlags.highlightByBackground];
                    this.SetCellAttribute(NSCellAttribute.NSCellLightsByBackground, (int)buttonCellFlags.highlightByBackground);
                    //[self setCellAttribute: NSCellLightsByContents
                    //      to: buttonCellFlags.highlightByContents];
                    this.SetCellAttribute(NSCellAttribute.NSCellLightsByContents, (int)buttonCellFlags.highlightByContents);
                    //[self setCellAttribute: NSCellLightsByGray
                    //      to: buttonCellFlags.highlightByGray];
                    this.SetCellAttribute(NSCellAttribute.NSCellLightsByGray, (int)buttonCellFlags.highlightByGray);
                    //[self setCellAttribute: NSChangeBackgroundCell
                    //      to: buttonCellFlags.changeBackground];
                    this.SetCellAttribute(NSCellAttribute.NSChangeBackgroundCell, (int)buttonCellFlags.changeBackground);
                    //[self setCellAttribute: NSCellChangesContents
                    //      to: buttonCellFlags.changeContents];
                    this.SetCellAttribute(NSCellAttribute.NSCellChangesContents, (int)buttonCellFlags.changeContents);
                    //[self setCellAttribute: NSChangeGrayCell
                    //      to: buttonCellFlags.changeGray];
                    this.SetCellAttribute(NSCellAttribute.NSChangeGrayCell, (int)buttonCellFlags.changeGray);




                    //if (buttonCellFlags.imageDoesOverlap)
                    //  if (buttonCellFlags.isImageAndText)
                    //    [self setImagePosition: NSImageOverlaps];
                    //  else
                    //    [self setImagePosition: NSImageOnly];
                    //else if (buttonCellFlags.isImageAndText)
                    //  if (buttonCellFlags.isHorizontal)
                    //    if (buttonCellFlags.isBottomOrLeft)
                    //      [self setImagePosition: NSImageLeft];
                    //    else
                    //      [self setImagePosition: NSImageRight];
                    //  else
                    //    if (buttonCellFlags.isBottomOrLeft)
                    //      [self setImagePosition: NSImageBelow];
                    //    else
                    //      [self setImagePosition: NSImageAbove];
                    //else
                    //  [self setImagePosition: NSNoImage];


                    if (Convert.ToBoolean(buttonCellFlags.imageDoesOverlap))
                    {
                        if (Convert.ToBoolean(buttonCellFlags.isImageAndText))
                        {
                            this.ImagePosition = NSCellImagePosition.NSImageOverlaps;
                        }
                        else
                        {
                            this.ImagePosition = NSCellImagePosition.NSImageOnly;
                        }
                    }
                    else if (Convert.ToBoolean(buttonCellFlags.isImageAndText))
                    {
                        if (Convert.ToBoolean(buttonCellFlags.isHorizontal))
                        {
                            if (Convert.ToBoolean(buttonCellFlags.isBottomOrLeft))
                            {
                                this.ImagePosition = NSCellImagePosition.NSImageLeft;
                            }
                            else
                            {
                                this.ImagePosition = NSCellImagePosition.NSImageRight;
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(buttonCellFlags.isBottomOrLeft))
                            {
                                this.ImagePosition = NSCellImagePosition.NSImageBelow;
                            }
                            else
                            {
                                this.ImagePosition = NSCellImagePosition.NSImageAbove;
                            }
                        }
                    }
                    else
                    {
                        this.ImagePosition = NSCellImagePosition.NSNoImage;
                    }
                }
                if (aDecoder.ContainsValueForKey("NSButtonFlags2"))
                {
                    uint imageScale;
                    uint bFlags2 = (uint)aDecoder.DecodeIntForKey("NSButtonFlags2");           

                    //[self setShowsBorderOnlyWhileMouseInside: (bFlags2 & 0x8)];
                    this.ShowsBorderOnlyWhileMouseInside = Convert.ToBoolean(bFlags2 & 0x8);

                    //[self setBezelStyle: (bFlags2 & 0x7) | ((bFlags2 & 0x20) >> 2)];
                    this.BezelStyle = (NSBezelStyle)((bFlags2 & 0x7) | ((bFlags2 & 0x20) >> 2));

                    //[self setKeyEquivalentModifierMask: ((bFlags2 >> 8) & NSDeviceIndependentModifierFlagsMask)];                    
                    this._keyEquivalentModifierMask = (int)((bFlags2 >> 8) & (uint)NSDeviceIndependentModifierFlagsMasks.NSDeviceIndependentModifierFlagsMask);

                    //NSDeviceIndependentModifierFlagsMask
                    //switch (bFlags2 & (3 << 6))
                    //{
                    //case 2:
                    //imageScale = NSImageScaleProportionallyDown;
                    //break;
                    //case 3:
                    //imageScale = NSImageScaleAxesIndependently;
                    //break;
                    //case 0:
                    //default:
                    //imageScale = NSImageScaleNone;
                    //break;
                    //case 1:
                    //imageScale = NSImageScaleProportionallyUpOrDown;
                    //break;
                    //}
                    //[self setImageScaling: imageScale];


                    switch (bFlags2 & (3 << 6))
                    {
                        case 0:
                        default:
                            imageScale = (uint)NSImageScaling.NSImageScaleNone; break;

                        case 1: imageScale = (uint)NSImageScaling.NSImageScaleProportionallyUpOrDown; break;
                        case 2: imageScale = (uint)NSImageScaling.NSImageScaleProportionallyDown; break;
                        case 3: imageScale = (uint)NSImageScaling.NSImageScaleAxesIndependently; break;
                    }
                }

                #region TODO

                if (aDecoder.ContainsValueForKey("NSAlternateImage"))
                {
                    object image = aDecoder.DecodeObjectForKey("NSAlternateImage");
                    if (image.GetType() == typeof(NSImage))
                    {
                        #region TODO

                        //        if ([NSImage imageNamed: @"NSSwitch"] == image)
                        //              {
                        //                image = [NSImage imageNamed: @"NSHighlightedSwitch"];
                        //                if ([self image] == nil)
                        //                  {
                        //                    [self setImage: [NSImage imageNamed: @"NSSwitch"]];
                        //                  }
                        //              }
                        //            else if ([NSImage imageNamed: @"NSRadioButton"] == image)
                        //              {
                        //                image = [NSImage imageNamed: @"NSHighlightedRadioButton"];
                        //                if ([self image] == nil)
                        //                  {
                        //                    [self setImage: [NSImage imageNamed: @"NSRadioButton"]];
                        //                  }
                        //              }

                        //            [self setAlternateImage: image];

                        #endregion
                    }

                    if (aDecoder.ContainsValueForKey("NSPeriodicDelay"))
                    {
                        delay = aDecoder.DecodeIntForKey("NSPeriodicDelay");
                    }
                    if (aDecoder.ContainsValueForKey("NSPeriodicInterval"))
                    {
                        interval = aDecoder.DecodeIntForKey("NSPeriodicInterval");
                    } 
                    
                    // [self setPeriodicDelay: delay interval: interval];            
                    this.SetPeriodicDelay(delay, interval);                    
                }
                else
                {
                    bool temp;

                    //int version = [aDecoder versionForClassName: @"NSButtonCell"];
                    //TODO
                    
                    //NSString *key = nil;
                    NSString key = null;

                    //[aDecoder decodeValueOfObjCType: @encode(id) at: &key];
                    //TODO

                    //[self setKeyEquivalent: key]; // Set the key equivalent...
                    this.KeyEquivalent = key;

                    #region We do not handle this for now
                                        
                    //    [aDecoder decodeValueOfObjCType: @encode(id) at: &_keyEquivalentFont];
                    //    [aDecoder decodeValueOfObjCType: @encode(id) at: &_altContents];
                    //    [aDecoder decodeValueOfObjCType: @encode(id) at: &_altImage];
                    //    [aDecoder decodeValueOfObjCType: @encode(BOOL) at: &tmp];
                    //    _buttoncell_is_transparent = tmp;
                    //    [aDecoder decodeValueOfObjCType: @encode(unsigned int)
                    //                                 at: &_keyEquivalentModifierMask];
                    //    if (version <= 2)
                    //      {
                    //        _keyEquivalentModifierMask = _keyEquivalentModifierMask << 16;
                    //      }
                    //    [aDecoder decodeValueOfObjCType: @encode(unsigned int)
                    //                                 at: &_highlightsByMask];
                    //    [aDecoder decodeValueOfObjCType: @encode(unsigned int)
                    //                                 at: &_showAltStateMask];

                    //    if (version >= 2)
                    //      {
                    //        [aDecoder decodeValueOfObjCType: @encode(id) at: &_sound];
                    //        [aDecoder decodeValueOfObjCType: @encode(id) at: &_backgroundColor];
                    //        [aDecoder decodeValueOfObjCType: @encode(float) at: &_delayInterval];
                    //        [aDecoder decodeValueOfObjCType: @encode(float) at: &_repeatInterval];
                    //        [aDecoder decodeValueOfObjCType: @encode(unsigned int)
                    //                                     at: &_bezel_style];
                    //        [aDecoder decodeValueOfObjCType: @encode(unsigned int)
                    //                                     at: &_gradient_type];
                    //        [aDecoder decodeValueOfObjCType: @encode(BOOL) at: &tmp];
                    //        _image_dims_when_disabled = tmp;
                    //        [aDecoder decodeValueOfObjCType: @encode(BOOL) at: &tmp];
                    //        _shows_border_only_while_mouse_inside = tmp;
                    //      }
                    //    // Not encoded in non-keyed archive
                    //    _imageScaling = NSImageScaleNone;

                    #endregion
                }

                // Hack to correct a Gorm problem, there "\n" is used instead of "\r".
                if (KeyEquivalent == "\n")
                {
                    this.KeyEquivalent = "\r";
                }
                                
                #endregion                
            }











            //// From Cocotron
            //if (aDecoder.AllowsKeyedCoding)
            //{
            //    uint flags = (uint)aDecoder.DecodeIntForKey("NSButtonFlags");
            //    uint flags2 = (uint)aDecoder.DecodeIntForKey("NSButtonFlags2");

            //    Title = (NSString)aDecoder.DecodeObjectForKey("NSContents");

            //    ImagePosition = NSCellImagePosition.NSNoImage;
            //    if ((flags & 0x00480000) == 0x00400000)
            //        ImagePosition = NSCellImagePosition.NSImageOnly;
            //    else if ((flags & 0x00480000) == 0x00480000)
            //        ImagePosition = NSCellImagePosition.NSImageOverlaps;
            //    else if ((flags & 0x00380000) == 0x00380000)
            //        ImagePosition = NSCellImagePosition.NSImageLeft;
            //    else if ((flags & 0x00380000) == 0x00280000)
            //        ImagePosition = NSCellImagePosition.NSImageRight;
            //    else if ((flags & 0x00380000) == 0x00180000)
            //        ImagePosition = NSCellImagePosition.NSImageBelow;
            //    else if ((flags & 0x00380000) == 0x00080000)
            //        ImagePosition = NSCellImagePosition.NSImageAbove;

            //    HighlightsBy = NSCellMasks.NSNoCellMask;
            //    ShowsStateBy = NSCellMasks.NSNoCellMask;

            //    if ((flags & 0x80000000) > 0)
            //        HighlightsBy |= NSCellMasks.NSPushInCellMask;
            //    if ((flags & 0x40000000) > 0)
            //        ShowsStateBy |= NSCellMasks.NSContentsCellMask;
            //    if ((flags & 0x20000000) > 0)
            //        ShowsStateBy |= NSCellMasks.NSChangeBackgroundCellMask;
            //    if ((flags & 0x10000000) > 0)
            //        ShowsStateBy |= NSCellMasks.NSChangeGrayCellMask;
            //    if ((flags & 0x08000000) > 0)
            //        HighlightsBy |= NSCellMasks.NSContentsCellMask;
            //    if ((flags & 0x04000000) > 0)
            //        HighlightsBy |= NSCellMasks.NSChangeBackgroundCellMask;
            //    if ((flags & 0x02000000) > 0)
            //        HighlightsBy |= NSCellMasks.NSChangeGrayCellMask;

            //    IsBordered = ((flags & 0x00800000) > 0) ? true : false;

            //    BezelStyle = (NSBezelStyle) ((flags2 & 0x7) | (flags2 & 0x20 >> 2));

            //    IsTransparent = ((flags & 0x00008000) > 0) ? true : false;

            //    ImageDimsWhenDisabled = ((flags & 0x00002000) > 0)? false : true;

            //    ShowsBorderOnlyWhileMouseInside = ((flags2 & 0x8) > 0) ? true : false;

            //    //todo NSAlternateImage
            //}


            return this;
        }


        private void SetCellAttribute(NSCellAttribute aParameter, int toValue)
        {
            switch (aParameter)
            {
                case NSCellAttribute.NSPushInCell:
                    if (toValue != 0)
                        _highlightsByMask |= (int)NSCellMasks.NSPushInCellMask;
                    else
                        _highlightsByMask &= ~(int)NSCellMasks.NSPushInCellMask;
                    break;

                    //TODO
            }
        }

        private void SetPeriodicDelay(int delay, int interval)
        {
            this.PeriodicDelay = delay;
            this.PeriodicInterval = interval;
        }


    }
}
