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
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSColorSpace.m
    public enum NSColorSpaceModel
    {
        NSUnknownColorSpaceModel = -1,
        NSGrayColorSpaceModel,
        NSRGBColorSpaceModel,
        NSCMYKColorSpaceModel,
        NSLABColorSpaceModel,
        NSDeviceNColorSpaceModel
    }

    public class NSColorSpace : NSObject
    {
        new public static Class Class = new Class(typeof(NSColorSpace));

        private static NSColorSpace _cspDeviceCMYK = null;
        private static NSColorSpace _cspDeviceGray = null;
        private static NSColorSpace _cspDeviceRGB = null;
        private static NSColorSpace _cspGenericCMYK = null;
        private static NSColorSpace _cspGenericGray = null;
        private static NSColorSpace _cspGenericRGB = null;


        protected NSColorSpaceModel _colorSpaceModel;
        protected NSData _iccData;
        protected id _colorSyncProfile;

        //Externs
        public static readonly NSString NSDeviceColorSpaceName = @"NSDeviceColorSpaceName";
        public static readonly NSString NSCalibratedWhiteColorSpace = @"NSCalibratedWhiteColorSpace";
        public static readonly NSString NSCalibratedBlackColorSpace = @"NSCalibratedBlackColorSpace";
        public static readonly NSString NSCalibratedRGBColorSpace = @"NSCalibratedRGBColorSpace";
        public static readonly NSString NSDeviceWhiteColorSpace = @"NSDeviceWhiteColorSpace";
        public static readonly NSString NSDeviceBlackColorSpace = @"NSDeviceBlackColorSpace";
        public static readonly NSString NSDeviceRGBColorSpace = @"NSDeviceRGBColorSpace";
        public static readonly NSString NSDeviceCMYKColorSpace = @"NSDeviceCMYKColorSpace";
        public static readonly NSString NSNamedColorSpace = @"NSNamedColorSpace";
        public static readonly NSString NSPatternColorSpace = @"NSPatternColorSpace";
        public static readonly NSString NSCustomColorSpace = @"NSCustomColorSpace";


        public static NSColorSpace Alloc()
        {
            return new NSColorSpace();
        }

        public virtual id _InitWithColorSpaceModel(NSColorSpaceModel model)
        {
            id self = this;

            if (base.Init() != null)
            {
                _colorSpaceModel = model;
            }

            return self;
        }

        private static NSColorSpace COLORSPACE(ref NSColorSpace csp, NSColorSpaceModel model)
        {
            if (csp == null)
                csp = (NSColorSpace)Alloc()._InitWithColorSpaceModel(model);
            return csp;
        }


        public static NSColorSpace DeviceCMYKColorSpace
        {
            get { return COLORSPACE(ref _cspDeviceCMYK, NSColorSpaceModel.NSCMYKColorSpaceModel); }
        }

        public static NSColorSpace DeviceGrayColorSpace
        {
            get { return COLORSPACE(ref _cspDeviceGray, NSColorSpaceModel.NSGrayColorSpaceModel); }
        }

        public static NSColorSpace DeviceRGBColorSpace
        {
            get { return COLORSPACE(ref _cspDeviceRGB, NSColorSpaceModel.NSRGBColorSpaceModel); }
        }

        public static NSColorSpace GenericCMYKColorSpace
        {
            get { return COLORSPACE(ref _cspGenericCMYK, NSColorSpaceModel.NSCMYKColorSpaceModel); }
        }

        public static NSColorSpace GenericGrayColorSpace
        {
            get { return COLORSPACE(ref _cspGenericGray, NSColorSpaceModel.NSGrayColorSpaceModel); }
        }

        public static NSColorSpace GenericRGBColorSpace
        {
            get { return COLORSPACE(ref _cspGenericRGB, NSColorSpaceModel.NSRGBColorSpaceModel); }
        }

        public virtual id InitWithColorSyncProfile(id prof)
        {
            id self = this;
            if (base.Init() != null)
            {
                _colorSyncProfile = prof;
                _colorSpaceModel = NSColorSpaceModel.NSUnknownColorSpaceModel;
            }
            return self;
        }


        public virtual id InitWithICCProfileData(NSData iccData)
        {
            id self = this;
            if (base.Init() != null)
            {
                _iccData = iccData;
                _colorSpaceModel = NSColorSpaceModel.NSUnknownColorSpaceModel;
            }
            return self;
        }

        public virtual NSColorSpaceModel ColorSpaceModel
        {
            get  { return _colorSpaceModel; }
        }


        public virtual id ColorSyncProfile
        {
            get { return _colorSyncProfile; }
        }

        public virtual NSData ICCProfileData
        {
            get 
            {
                if (_iccData == null)
                {
                    // FIXME: Try to compute this from _colorSyncProfile
                }
                return _iccData;
            }
        }

        public virtual NSString LocalizedName
        {
            get
            {
                switch (_colorSpaceModel)
                {
                    default:
                    case NSColorSpaceModel.NSUnknownColorSpaceModel:
                        return @"unknown";
                    case NSColorSpaceModel.NSGrayColorSpaceModel:
                        return @"Grayscale";
                    case NSColorSpaceModel.NSRGBColorSpaceModel:
                        return @"RGB";
                    case NSColorSpaceModel.NSCMYKColorSpaceModel:
                        return @"CMYK";
                    case NSColorSpaceModel.NSLABColorSpaceModel:
                        return @"LAB";
                    case NSColorSpaceModel.NSDeviceNColorSpaceModel:
                        return @"DeviceN";
                }
            }
        }

        public virtual int NumberOfColorComponents
        {
            get
            {
                 switch (_colorSpaceModel)
                 {
                    default:
                    case NSColorSpaceModel.NSUnknownColorSpaceModel: return 0;
                    case NSColorSpaceModel.NSGrayColorSpaceModel: return 1;
                    case NSColorSpaceModel.NSRGBColorSpaceModel: return 3;
                    case NSColorSpaceModel.NSCMYKColorSpaceModel: return 4;
                    case NSColorSpaceModel.NSLABColorSpaceModel: return 3;	// FIXME
                    case NSColorSpaceModel.NSDeviceNColorSpaceModel: return 3;	// FIXME
                 }
            }
        }

        public override void EncodeWithCoder(NSCoder aCoder)
        {

        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            return self;
        }
    }
}
