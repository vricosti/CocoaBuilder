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
        NSNoBezelStyle = 0,

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

    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSButtonCell.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSButtonCell.m
    public class NSButtonCell : NSActionCell
    {
        // Attributes
        NSString _altContents;
        NSImage _altImage;
        NSString _keyEquivalent;
        NSFont _keyEquivalentFont;
        NSSound _sound;
        uint _keyEquivalentModifierMask;
        uint _highlightsByMask;
        uint _showAltStateMask;
        float _delayInterval;
        float _repeatInterval;
        NSBezelStyle _bezel_style;
        NSGradientType _gradient_type;
        NSColor _backgroundColor;
        NSImageScaling _imageScaling;

        // From GNUstep
        public struct GSButtonCellFlags
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

        public struct GSButtonCellFlags2
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


        //
        // Setting the Titles 
        //
        public NSString Title
        {
            get 
            {
                if (null == _contents)
                    return (NSString)@"";

                if (_cell.contents_is_attributed_string.ToBool() == false)
                {
                    return (NSString)_contents;
                }
                else
                {
                    return ((NSAttributedString)_contents).String;
                }
            }

            set
            {
                _contents = value;
                _cell.contents_is_attributed_string = Convert.ToUInt32(false);
                if ((_control_view != null) && _control_view is NSControl)
                {
                    //FIXME
                    //((NSControl)_control_view).UpdateCell(this);
                }
            }
        }


        [ObjcProp("alternateTitle")]
        public NSString AlternateTitle
        {
            get
            {
                return _altContents != null ? _altContents : (NSString)@"";
            }

            set
            {
                _altContents = value;
                if ((_control_view != null) && _control_view is NSControl)
                {
                    //FIXME
                    //((NSControl)_control_view).UpdateCell(this);
                }
            }
        }

        [ObjcProp("attributedAlternateTitle")]
        public NSAttributedString AttributedAlternateTitle
        {
            get
            {
                //FIXME
                return null;
            }

            set
            {
                this.AlternateTitle = value.String;
            }
        }


        //
        // Managing Images 
        //
        [ObjcProp("alternateImage")]
        public NSImage AlternateImage
        {
            get { return _altImage; }
            set
            {
                _altImage = value;
                if ((_control_view != null) && _control_view is NSControl)
                {
                    //FIXME
                    //((NSControl)_control_view).UpdateCell(this);
                }
            }
        }

        [ObjcProp("imagePosition")]
        public NSCellImagePosition ImagePosition
        {
            get { return (NSCellImagePosition)_cell.image_position; }
            set
            {
                NSCellImagePosition aPosition = value;
                _cell.image_position = (uint)aPosition;

                if (_cell.image_position == (uint)NSCellImagePosition.NSNoImage)
                {
                    _cell.type = (uint)NSCellType.NSTextCellType;
                }
                else
                {
                    _cell.type = (uint)NSCellType.NSImageCellType;
                }

                if ((_control_view != null) && _control_view is NSControl)
                {
                    //FIXME
                    //((NSControl)_control_view).UpdateCell(this);
                }
            }
        }

        [ObjcProp("imageScaling")]
        public NSImageScaling ImageScaling
        {
            get { return _imageScaling; }
            set { _imageScaling = value; }
        }


        //Managing the Key Equivalent

        [ObjcProp("keyEquivalent")]
        public NSString KeyEquivalent
        {
            get { return (_keyEquivalent != null) ? _keyEquivalent : (NSString)@""; }
            set
            {
                NSString key = value;
                // FIXME
                //[[GSTheme theme] setKeyEquivalent: key forButtonCell: self];
                _keyEquivalent = key;
            }
        }

        [ObjcProp("keyEquivalentFont")]
        public NSFont KeyEquivalentFont
        {
            get { return _keyEquivalentFont; }
            set
            {
                NSFont fontObj = value;
                _keyEquivalentFont = fontObj;
            }
        }

        [ObjcProp("keyEquivalentModifierMask")]
        public uint KeyEquivalentModifierMask
        {
            get { return _keyEquivalentModifierMask; }
            set
            {
                uint mask = value;
                _keyEquivalentModifierMask = mask;
            }
        }

        //Managing Graphics Attributes

        [ObjcProp("backgroundColor")]
        public NSColor BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        [ObjcProp("transparent", GetName = "isTransparent")]
        public bool Transparent
        {
            get { return Convert.ToBoolean(_cell.subclass_bool_one); }
            set
            {
                bool flag = value;
                _cell.subclass_bool_one = Convert.ToUInt32(flag);

            }
        }

        [ObjcProp("opaque", GetName = "isOpaque", SetName = null)]
        public bool Opaque
        {
            get
            {
                return (_cell.subclass_bool_one == 0) &&
                    Convert.ToBoolean(_cell.is_bordered) &&
                    _bezel_style == NSBezelStyle.NSNoBezelStyle;
            }
        }

        [ObjcProp("bezelStyle")]
        public NSBezelStyle BezelStyle
        {
            get { return _bezel_style; }
            set { _bezel_style = value; }
        }


        [ObjcProp("showsBorderOnlyWhileMouseInside")]
        public bool ShowsBorderOnlyWhileMouseInside
        {
            get { return _cell.subclass_bool_three.ToBool(); }
            set
            {
                if (Convert.ToBoolean(_cell.subclass_bool_three) == value)
                    return;

                _cell.subclass_bool_three = Convert.ToUInt32(value);
            }
        }

        [ObjcProp("gradientType")]
        public NSGradientType GradientType
        {
            get { return _gradient_type; }
            set { _gradient_type = value; }
        }

        [ObjcProp("imageDimsWhenDisabled")]
        public bool ImageDimsWhenDisabled
        {
            get { return _cell.subclass_bool_two.ToBool(); }
            set { _cell.subclass_bool_two = Convert.ToUInt32(value); }
        }


        //Displaying the Cell

        public int HighlightsBy
        {
            get { return (int)_highlightsByMask; }
            set { _highlightsByMask = (uint)value; }
        }

        public int ShowsStateBy
        {
            get { return (int)_showAltStateMask; }
            set { _showAltStateMask = (uint)value; }
        }

        [ObjcProp("buttonType", GetName = null)]
        public NSButtonType ButtonType
        {
            set
            {
                NSButtonType buttonType = value;
                switch (buttonType)
                {
                    case NSButtonType.NSMomentaryLightButton:
                        this.HighlightsBy = (int)NSCellMasks.NSChangeBackgroundCellMask;
                        this.ShowsStateBy = (int)NSCellMasks.NSNoCellMask;
                        this.ImageDimsWhenDisabled = true;
                        break;
                    case NSButtonType.NSMomentaryPushInButton:
                        this.HighlightsBy = (int)(NSCellMasks.NSPushInCellMask | NSCellMasks.NSChangeGrayCellMask);
                        this.ShowsStateBy = (int)NSCellMasks.NSNoCellMask;
                        this.ImageDimsWhenDisabled = true;
                        break;
                    case NSButtonType.NSMomentaryChangeButton:
                        this.HighlightsBy = (int)NSCellMasks.NSContentsCellMask;
                        this.ShowsStateBy = (int)NSCellMasks.NSNoCellMask;
                        this.ImageDimsWhenDisabled = true;
                        break;
                    case NSButtonType.NSPushOnPushOffButton:
                        this.HighlightsBy = (int)(NSCellMasks.NSPushInCellMask | NSCellMasks.NSChangeGrayCellMask);
                        this.ShowsStateBy = (int)NSCellMasks.NSChangeBackgroundCellMask;
                        this.ImageDimsWhenDisabled = true;
                        break;
                    case NSButtonType.NSOnOffButton:
                        this.HighlightsBy = (int)NSCellMasks.NSChangeBackgroundCellMask;
                        this.ShowsStateBy = (int)NSCellMasks.NSChangeBackgroundCellMask;
                        this.ImageDimsWhenDisabled = true;
                        break;
                    case NSButtonType.NSToggleButton:
                        this.HighlightsBy = (int)(NSCellMasks.NSPushInCellMask | NSCellMasks.NSContentsCellMask);
                        this.ShowsStateBy = (int)NSCellMasks.NSContentsCellMask;
                        this.ImageDimsWhenDisabled = true;
                        break;
                    case NSButtonType.NSSwitchButton:
                        this.HighlightsBy = (int)NSCellMasks.NSContentsCellMask;
                        this.ShowsStateBy = (int)NSCellMasks.NSContentsCellMask;
                        this.Image = NSImage.ImageNamed(@"NSSwitch");
                        this.AlternateImage = NSImage.ImageNamed(@"NSHighlightedSwitch");
                        this.ImagePosition = NSCellImagePosition.NSImageLeft;
                        this.Alignment = NSTextAlignment.NSLeftTextAlignment;
                        this.Bordered = false;
                        this.Bezeled = false;
                        this.ImageDimsWhenDisabled = false;
                        break;

                    case NSButtonType.NSRadioButton:
                        this.HighlightsBy = (int)NSCellMasks.NSContentsCellMask;
                        this.ShowsStateBy = (int)NSCellMasks.NSContentsCellMask;
                        this.Image = NSImage.ImageNamed(@"NSRadioButton");
                        this.AlternateImage = NSImage.ImageNamed(@"NSHighlightedRadioButton");
                        this.ImagePosition = NSCellImagePosition.NSImageLeft;
                        this.Alignment = NSTextAlignment.NSLeftTextAlignment;
                        this.Bordered = false;
                        this.Bezeled = false;
                        this.ImageDimsWhenDisabled = false;
                        break;
                }
            }
        }


       //Managing the Sound

        [ObjcProp("sound")]
        public NSSound Sound
        {
            get { return _sound; }
            set { _sound = value; }
        }


        public NSButtonCell()
        {

        }

        public id _Init()
        {
            id self = this;

            // Implicitly performed by allocation:
            //

            this.Alignment = NSTextAlignment.NSCenterTextAlignment;
            _cell.is_bordered = Convert.ToUInt32(true);
            this.ButtonType = NSButtonType.NSMomentaryPushInButton;

            _delayInterval = 0.4f;
            _repeatInterval = 0.075f;
            _keyEquivalentModifierMask = 0;
            _keyEquivalent = @"";
            _altContents = @"";
            _gradient_type = NSGradientType.NSGradientNone;
            this.ImageScaling = NSImageScaling.NSImageScaleNone;


            return self;
        }

        public override id Init()
        {
            id self = this;

            this.InitTextCell("Button");

            return self;
        }

        public override id InitImageCell(NSImage anImage)
        {
            if (base.InitImageCell(anImage) == null)
                return null;

            return this._Init();
        }

        public override id InitTextCell(NSString aString)
        {
            if (base.InitTextCell(aString) == null)
                return null;

            return this._Init();
        }


        //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSButtonCell.m
        public override id InitWithCoder(NSObjectDecoder aDecoder)
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
                    Image = (NSImage)aDecoder.DecodeObjectForKey("NSNormalImage");
                }
                if (aDecoder.ContainsValueForKey("NSAlternateContents"))
                {
                    AlternateTitle = (NSString)aDecoder.DecodeObjectForKey("NSAlternateContents");
                }
                if (aDecoder.ContainsValueForKey("NSButtonFlags"))
                {
                    uint bFlags = (uint)aDecoder.DecodeIntForKey("NSButtonFlags");
                    GSButtonCellFlags buttonCellFlags = PrimitiveConversion.FromLong<GSButtonCellFlags>(bFlags);

                    this.Transparent = Convert.ToBoolean(buttonCellFlags.isTransparent);
                    this.Bordered = Convert.ToBoolean(buttonCellFlags.isBordered);

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
                    //GSButtonCellFlags2 buttonCellFlags2 = PrimitiveConversion.FromLong<GSButtonCellFlags2>(bFlags2);
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
                    if (image != null && image.GetType() == typeof(NSImage))
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

            return this;
        }


        public override void SetCellAttribute(NSCellAttribute aParameter, int toValue)
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
                    base.SetCellAttribute(aParameter, toValue);
                    break;

            }
        }

        public void GetPeriodicDelay(ref float delay, ref float interval)
        {
            delay = _delayInterval;
            interval = _repeatInterval;
        }

        public void SetPeriodicDelay(int delay, int interval)
        {
            _delayInterval = delay;
            _repeatInterval = interval;
        }


    }
}
