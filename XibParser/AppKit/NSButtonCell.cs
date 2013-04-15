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

        public struct WSButtonCellFlags2 
        {
            [BitfieldLength(3)]
            public uint bezelStyle;
            [BitfieldLength(1)]
            public uint showsBorderOnlyWhileMouseInside;
            [BitfieldLength(1)]
            public uint mouseInside;
            [BitfieldLength(1)]
            public uint bezelStyle2;
            [BitfieldLength(2)]
            public uint imageScaling;
            [BitfieldLength(24)]
            public uint keyEquivalentModifierMask;
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

        private uint _highlightsByMask;
        public int HighlightsBy { get { return (int)_highlightsByMask; } set { _highlightsByMask = (uint)value; } }

        private uint _showAltStateMask;
        public int ShowsStateBy { get { return (int)_showAltStateMask; } set { _showAltStateMask = (uint)value; } }

        public bool ImageDimsWhenDisabled { get; set; }

        public string KeyEquivalent { get; set; }

        private uint _keyEquivalentModifierMask;

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
                   
                    this.IsTransparent = Convert.ToBoolean(buttonCellFlags.isTransparent);
                    this.IsBordered = Convert.ToBoolean(buttonCellFlags.isBordered);

                    this.SetCellAttribute(NSCellAttribute.NSPushInCell, (int)buttonCellFlags.isPushin);
                    this.SetCellAttribute(NSCellAttribute.NSCellLightsByBackground, (int)buttonCellFlags.highlightByBackground);
                    this.SetCellAttribute(NSCellAttribute.NSCellLightsByContents, (int)buttonCellFlags.highlightByContents);
                    this.SetCellAttribute(NSCellAttribute.NSCellLightsByGray, (int)buttonCellFlags.highlightByGray);
                    this.SetCellAttribute(NSCellAttribute.NSChangeBackgroundCell, (int)buttonCellFlags.changeBackground);
                    this.SetCellAttribute(NSCellAttribute.NSCellChangesContents, (int)buttonCellFlags.changeContents);
                    this.SetCellAttribute(NSCellAttribute.NSChangeGrayCell, (int)buttonCellFlags.changeGray);

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
                    //uint bFlags2 = (uint)aDecoder.DecodeIntForKey("NSButtonFlags2");
                    //WSButtonCellFlags2 buttonCellFlags2 = PrimitiveConversion.FromLong<WSButtonCellFlags2>(bFlags2);
                    //this.ShowsBorderOnlyWhileMouseInside = Convert.ToBoolean(buttonCellFlags2.showsBorderOnlyWhileMouseInside);
                    //this.BezelStyle = (NSBezelStyle)buttonCellFlags2.bezelStyle;
                    //this._keyEquivalentModifierMask = buttonCellFlags2.keyEquivalentModifierMask;
                    //uint imageScale = buttonCellFlags2.imageScaling;

                    uint imageScale;
                    uint bFlags2 = (uint)aDecoder.DecodeIntForKey("NSButtonFlags2");
                    this.ShowsBorderOnlyWhileMouseInside = Convert.ToBoolean(bFlags2 & 0x8);
                    this.BezelStyle = (NSBezelStyle)((bFlags2 & 0x7) | ((bFlags2 & 0x20) >> 2));
                    this._keyEquivalentModifierMask = (uint)((bFlags2 >> 8) & (uint)NSDeviceIndependentModifierFlagsMasks.NSDeviceIndependentModifierFlagsMask);

                    switch (bFlags2 & (3 << 6))
                    {
                        case 0:
                        default:
                            imageScale = (uint)NSImageScaling.NSImageScaleNone; break;

                        case 1: imageScale = (uint)NSImageScaling.NSImageScaleProportionallyUpOrDown; break;
                        case 2: imageScale = (uint)NSImageScaling.NSImageScaleProportionallyDown; break;
                        case 3: imageScale = (uint)NSImageScaling.NSImageScaleAxesIndependently; break;
                    }

                    this.ImageScaling = (NSImageScaling)imageScale;
                }

                #region TODO

                if (aDecoder.ContainsValueForKey("NSAlternateImage"))
                {
                    object image = aDecoder.DecodeObjectForKey("NSAlternateImage");
                    if (image.GetType() == typeof(NSImage))
                    {
                        #region TODO

                        //if ([NSImage imageNamed: @"NSSwitch"] == image)
                        //      {
                        //        image = [NSImage imageNamed: @"NSHighlightedSwitch"];
                        //        if ([self image] == nil)
                        //          {
                        //            [self setImage: [NSImage imageNamed: @"NSSwitch"]];
                        //          }
                        //      }
                        //    else if ([NSImage imageNamed: @"NSRadioButton"] == image)
                        //      {
                        //        image = [NSImage imageNamed: @"NSHighlightedRadioButton"];
                        //        if ([self image] == nil)
                        //          {
                        //            [self setImage: [NSImage imageNamed: @"NSRadioButton"]];
                        //          }
                        //      }
                        //    [self setAlternateImage: image];

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
                    #region We do not handle this for now

                    bool temp;

                    //int version = [aDecoder versionForClassName: @"NSButtonCell"];
                    //TODO
                    
                    //NSString *key = nil;
                    NSString key = null;

                    //[aDecoder decodeValueOfObjCType: @encode(id) at: &key];
                    //TODO

                    //[self setKeyEquivalent: key]; // Set the key equivalent...
                    this.KeyEquivalent = key;

                    
                                        
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
                 
                #endregion                
            }

            #region From Cocotron

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

            #endregion

            return this;
        }


        private void SetCellAttribute(NSCellAttribute aParameter, int toValue)
        {
            switch (aParameter)
            {
                case NSCellAttribute.NSPushInCell:
                    if (toValue != 0)
                        _highlightsByMask |= (uint)NSCellMasks.NSPushInCellMask;
                    else
                        _highlightsByMask &= ~(uint)NSCellMasks.NSPushInCellMask;
                    break;
                case NSCellAttribute.NSChangeGrayCell:
                    if (toValue != 0)
                        _showAltStateMask |= (uint)NSCellMasks.NSChangeGrayCellMask;
                    else
                        _showAltStateMask &= ~(uint)NSCellMasks.NSChangeGrayCellMask;
                    break;
                case NSCellAttribute.NSChangeBackgroundCell:
                    if (toValue != 0)
                        _showAltStateMask |= (uint)NSCellMasks.NSChangeBackgroundCellMask;
                    else
                        _showAltStateMask &= ~(uint)NSCellMasks.NSChangeBackgroundCellMask;
                    break;
                case NSCellAttribute.NSCellChangesContents:
                    if (toValue != 0)
                        _showAltStateMask |= (uint)NSCellMasks.NSContentsCellMask;
                    else
                        _showAltStateMask &= ~(uint)NSCellMasks.NSContentsCellMask;
                    break;
                case NSCellAttribute.NSCellLightsByGray:
                    if (toValue != 0)
                        _highlightsByMask |= (uint)NSCellMasks.NSChangeGrayCellMask;
                    else
                        _highlightsByMask &= ~(uint)NSCellMasks.NSChangeGrayCellMask;
                    break;
                case NSCellAttribute.NSCellLightsByBackground:
                    if (toValue != 0)
                        _highlightsByMask |= (uint)NSCellMasks.NSChangeBackgroundCellMask;
                    else
                        _highlightsByMask &= ~(uint)NSCellMasks.NSChangeBackgroundCellMask;
                    break;
                case NSCellAttribute.NSCellLightsByContents:
                    if (toValue != 0)
                        _highlightsByMask |= (uint)NSCellMasks.NSContentsCellMask;
                    else
                        _highlightsByMask &= ~(uint)NSCellMasks.NSContentsCellMask;
                    break;

                default:
                    // TODO implement SetCellAttribute inside NSCell
                    // base.SetCellAttribute(aParameter, toValue);
                    break;
                
            }
        }

        private void SetPeriodicDelay(int delay, int interval)
        {
            this.PeriodicDelay = delay;
            this.PeriodicInterval = interval;
        }


    }
}
