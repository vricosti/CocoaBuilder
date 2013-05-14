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

    public class GSNamedColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSNamedColor));
        new public static GSNamedColor Alloc() { return new GSNamedColor(); }

        protected NSString _catalog_name;
        protected NSString _color_name;
        protected NSString _cached_name_space;
        protected NSColor _cached_color;
    }

    public class GSWhiteColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSWhiteColor));
        new public static GSWhiteColor Alloc() { return new GSWhiteColor(); }

        protected float _white_component;
        protected float _alpha_component;
    }

    public class GSDeviceWhiteColor : GSWhiteColor
    {
        new public static Class Class = new Class(typeof(GSDeviceWhiteColor));
        new public static GSDeviceWhiteColor Alloc() { return new GSDeviceWhiteColor(); }


    }

    public class GSCalibratedWhiteColor : GSWhiteColor
    {
        new public static Class Class = new Class(typeof(GSCalibratedWhiteColor));
        new public static GSCalibratedWhiteColor Alloc() { return new GSCalibratedWhiteColor(); }
    }

    public class GSDeviceCMYKColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSDeviceCMYKColor));
        new public static GSDeviceCMYKColor Alloc() { return new GSDeviceCMYKColor(); }
    }

    public class GSRGBColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSRGBColor));
        new public static GSRGBColor Alloc() { return new GSRGBColor(); }

        protected float _red_component;
        protected float _green_component;
        protected float _blue_component;
        protected float _hue_component;
        protected float _saturation_component;
        protected float _brightness_component;
        protected float _alpha_component;
    }

    public class GSDeviceRGBColor : GSRGBColor
    {
        new public static Class Class = new Class(typeof(GSDeviceRGBColor));
        new public static GSDeviceRGBColor Alloc() { return new GSDeviceRGBColor(); }
    }

    public class GSCalibratedRGBColor : GSRGBColor
    {
        new public static Class Class = new Class(typeof(GSCalibratedRGBColor));
        new public static GSCalibratedRGBColor Alloc() { return new GSCalibratedRGBColor(); }
    }

    public class GSPatternColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSPatternColor));
        new public static GSPatternColor Alloc() { return new GSPatternColor(); }
    }



    public class NSColor : NSObject
    {
        public const float NSBlack = 0.0f;
        public const float NSDarkGray = 0.333f;
        public const float NSGray = 0.5f;
        public const float NSLightGray = 0.667f;
        public const float NSWhite = 1.0f;


        new public static Class Class = new Class(typeof(NSColor));
        new public static NSColor Alloc() { return new NSColor(); }
        static Class NSColorClass;

        static bool gnustep_gui_ignores_alpha = true;
        static NSColorList systemColors = null;
        static NSColorList defaultSystemColors = null;
        static NSMutableDictionary colorStrings = null;
        static NSMutableDictionary systemDict = null;


        public NSColor()
        {
        }

        #if TEMP
        static NSColor() { Initialize(); }
        public static void Initialize()
        {
            NSColorClass = Class;

            // Set the version number
            //[self setVersion: 3);

            // ignore alpha by default
            gnustep_gui_ignores_alpha = true;

            // Load or define the system colour list
            InitSystemColors();

            // ensure user defaults are loaded, then use them and watch for changes.
            DefaultsDidChange(null);


            //[[NSNotificationCenter defaultCenter] addObserver: self selector: @selector(defaultsDidChange:) name: NSUserDefaultsDidChangeNotification object: null);
            // watch for themes which may provide new system color lists
            //[[NSNotificationCenter defaultCenter] addObserver: self selector: @selector(themeDidActivate:) name: GSThemeDidActivateNotification object: null);

        }

        public static void InitSystemColors()
        {
            NSString white;
            NSString lightGray;
            NSString gray;
            NSString darkGray;
            NSString black;

            // Set up a dictionary containing the names of all the system colors
            // as keys and with colors in string format as values.
            white = NSString.StringWithFormat(@"%g %g %g", (double)NSWhite, (double)NSWhite, (double)NSWhite);
            lightGray = NSString.StringWithFormat(@"%g %g %g", (double)NSWhite, (double)NSWhite, (double)NSWhite);
            gray = NSString.StringWithFormat(@"%g %g %g", (double)NSWhite, (double)NSWhite, (double)NSWhite);
            darkGray = NSString.StringWithFormat(@"%g %g %g", (double)NSWhite, (double)NSWhite, (double)NSWhite);
            black = NSString.StringWithFormat(@"%g %g %g", (double)NSWhite, (double)NSWhite, (double)NSWhite);

            colorStrings = (NSMutableDictionary)NSMutableDictionary.Alloc().InitWithObjectsAndKeys(
             lightGray, (NSString)@"controlBackgroundColor",
             lightGray, (NSString)@"controlColor",
             lightGray, (NSString)@"controlHighlightColor",
             white, (NSString)@"controlLightHighlightColor",
             darkGray, (NSString)@"controlShadowColor",
             black, (NSString)@"controlDarkShadowColor",
             black, (NSString)@"controlTextColor",
             darkGray, (NSString)@"disabledControlTextColor",
             gray, (NSString)@"gridColor",
             lightGray, (NSString)@"headerColor",
             black, (NSString)@"headerTextColor",
             white, (NSString)@"highlightColor",
             black, (NSString)@"keyboardFocusIndicatorColor",
             lightGray, (NSString)@"knobColor",
             gray, (NSString)@"scrollBarColor",
             white, (NSString)@"selectedControlColor",
             black, (NSString)@"selectedControlTextColor",
             lightGray, (NSString)@"selectedKnobColor",
             white, (NSString)@"selectedMenuItemColor",
             black, (NSString)@"selectedMenuItemTextColor",
             lightGray, (NSString)@"selectedTextBackgroundColor",
             black, (NSString)@"selectedTextColor",
             black, (NSString)@"shadowColor",
             white, (NSString)@"textBackgroundColor",
             black, (NSString)@"textColor",
             lightGray, (NSString)@"windowBackgroundColor",
             black, (NSString)@"windowFrameColor",
             white, (NSString)@"windowFrameTextColor",
             black, (NSString)@"alternateSelectedControlColor",
             white, (NSString)@"alternateSelectedControlTextColor",
             white, (NSString)@"rowBackgroundColor",
             lightGray, (NSString)@"alternateRowBackgroundColor",
             lightGray, (NSString)@"secondarySelectedControlColor",
                //gray, (NSString)@"windowFrameColor",
                //black, (NSString)@"windowFrameTextColor",
             null);

            systemColors = NSColorList.ColorListNamed(@"System");
            defaultSystemColors = (NSColorList)NSColorList.Alloc().InitWithName(@"System");
            NSColorList._SetDefaultSystemColorList(defaultSystemColors);
            if (systemColors == null)
            {
                systemColors = defaultSystemColors;
            }

            {
                NSEnumerator enumerator;
                NSString key;

                // Set up default system colors

                enumerator = colorStrings.KeyEnumerator();

                while ((key = (NSString)enumerator.NextObject()) != null)
                {
                    NSColor color;

                    if ((color = systemColors.ColorWithKey(key)) == null)
                    {
                        NSString aColorString;

                        aColorString = (NSString)colorStrings.ObjectForKey(key);
                        color = NSColor.ColorFromString(aColorString);

                        //NSCAssert1(color, @"couldn't get default system color %@", key);
                        systemColors.SetColor(color, key);
                    }
                    if (defaultSystemColors != systemColors)
                    {
                        defaultSystemColors.SetColor(color, key);
                    }
                }
            }

            systemDict = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
        }


        public static NSColor SystemColorWithName(NSString name)
        {
            NSColor col = SystemDict.ObjectForKey(name);

            if (col == null)
            {
                col = NSColor.ColorWithCatalogName(@"System", name);
                systemDict.SetObjectForKey(col, name);
            }

            return col;
        }

        public static NSColor ColorWithCalibratedHue(float hue, float saturation, float brightness, float alpha)
        {
            id color;

            color = GSCalibratedRGBColor.Alloc();
            color = ((GSCalibratedRGBColor)color).InitWithCalibratedHue(hue, saturation, brightness, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithCalibratedRed(float red, float green, float blue, float alpha)
        {
            id color;

            color = GSCalibratedRGBColor.Alloc();
            color = ((GSCalibratedRGBColor)color).InitWithCalibratedRed(red, green, blue, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithCalibratedWhite(float white, float alpha)
        {
            id color;

            color = GSCalibratedWhiteColor.Alloc();
            color = ((GSCalibratedWhiteColor)color).InitWithCalibratedWhite(white, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithCatalogName(NSString listName, NSString colorName)
        {
            id color;

            color = GSNamedColor.Alloc();
            color = ((GSNamedColor)color).InitWithCatalogName(listName, colorName);

            return (NSColor)color;
        }

        public static NSColor ColorWithDeviceCyan(float cyan, float magenta, float yellow, float black, float alpha)
        {
            id color;

            color = GSDeviceCMYKColor.Alloc();
            color = ((GSDeviceCMYKColor)color).InitWithDeviceCyan(cyan, magenta, yellow, black, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithDeviceHue(float hue, float saturation, float brightness, float alpha)
        {
            id color;

            color = GSDeviceRGBColor.Alloc();
            color = ((GSDeviceRGBColor)color).InitWithDeviceHue(hue, saturation, brightness, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithDeviceRed(float red, float green, float blue, float alpha)
        {
            id color;

            color = GSDeviceRGBColor.Alloc();
            color = ((GSDeviceRGBColor)color).InitWithDeviceRed(red, green, blue, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithDeviceWhite(float white, float alpha)
        {
            id color;

            color = GSDeviceWhiteColor.Alloc();
            color = ((GSDeviceWhiteColor)color).InitWithDeviceWhite(white, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorForControlTint(NSControlTint controlTint)
        {
            switch (controlTint)
            {
                default:
                case NSControlTint.NSDefaultControlTint:
                    return ColorForControlTint(CurrentControlTint);
                case NSControlTint.NSGraphiteControlTint:
                // FIXME
                case NSControlTint.NSClearControlTint:
                // FIXME
                case NSControlTint.NSBlueControlTint:
                    return NSColor.BlueColor;
            }
        }

        public static NSControlTint CurrentControlTint
        {
            // FIXME: should be made a system setting
            get { return NSControlTint.NSBlueControlTint; }
        }

        public static NSColor ColorWithPatternImage(NSImage image)
        {
            id color;

            color = GSPatternColor.Alloc();
            color = ((GSPatternColor)color).InitWithPatternImage(image);

            return (NSColor)color;
        }

        public static NSColor BlackColor
        {
            get { return ColorWithCalibratedWhite(NSBlack, 1.0f); }
        }

        public static NSColor BlueColor
        {
            get { return ColorWithCalibratedRed(0.0f, 0.0f, 1.0f, 1.0f); }
        }

        public static NSColor BrownColor
        {
            get { return ColorWithCalibratedRed(0.6f, 0.4f, 0.2f, 1.0f); }
        }


        public static NSColor ClearColor
        {
            get { return ColorWithCalibratedWhite(0.0f, 0.0f); }
        }
        #if TEMP2        


        public static NSColor CyanColor
        {
            get { return ColorWithCalibratedRed(0.0f, 1.0f, 1.0f, 1.0f); }
        }


        public static NSColor DarkGrayColor
        {
            get { return ColorWithCalibratedWhite(NSDarkGray, 1.0f); }
        }


        public static NSColor GrayColor
        {
            get { return ColorWithCalibratedWhite(NSGray, 1.0f); }
        }


        public static NSColor GreenColor
        {
            get { return ColorWithCalibratedRed(0.0f, 1.0f, 0.0f, 1.0f); }
        }


        public static NSColor LightGrayColor
        {
            get { return ColorWithCalibratedWhite(NSLightGray, 1.0f); }
        }


        public static NSColor MagentaColor
        {
            get { return ColorWithCalibratedRed(1.0f, 0.0f, 1.0f, 1.0f); }
        }

        public static NSColor OrangeColor
        {
            get { return ColorWithCalibratedRed(1.0f, 0.5f, 0.0f, 1.0f); }
        }


        public static NSColor PurpleColor
        {
            get { return ColorWithCalibratedRed(0.5f, 0.0f, 0.5f, 1.0f); }
        }



        public static NSColor RedColor
        {
            get { return ColorWithCalibratedRed(1.0f, 0.0f, 0.0f, 1.0f); }
        }

        public static NSColor WhiteColor
        {
            get { return ColorWithCalibratedWhite(NSWhite, 1.0f); }
        }


        public static NSColor YellowColor
        {
            get { return ColorWithCalibratedRed(1.0f, 1.0f, 0.0f, 1.0f); }
        }



        public static bool IgnoresAlpha
        {
            get { return gnustep_gui_ignores_alpha; }
            set { gnustep_gui_ignores_alpha = value; }
        }




        //public static NSColor colorFromPasteboard(NSPasteboard pasteBoard)
        //{
        //  NSData *colorData = [pasteBoard dataForType(NSColorPboardType);

        //  // FIXME(This should better use the description format
        //  if (colorData != null)
        //    return [NSUnarchiver unarchiveObjectWithData(colorData);

        //  return null;
        //}

        //
        // System colors stuff.
        //
        public static NSColor AlternateSelectedControlColor
        {
            get { return SystemColorWithName(@"alternateSelectedControlColor"); }
        }

        public static NSColor AlternateSelectedControlTextColor
        {
            get { return SystemColorWithName(@"alternateSelectedControlTextColor"); }
        }

        public static NSColor ControlBackgroundColor
        {
            get { return SystemColorWithName(@"controlBackgroundColor"); }
        }

        public static NSColor ControlColor
        {
            get { return SystemColorWithName(@"controlColor"); }
        }

        public static NSColor ControlHighlightColor
        {
            get { return SystemColorWithName(@"controlHighlightColor"); }
        }

        public static NSColor ControlLightHighlightColor
        {
            get { return SystemColorWithName(@"controlLightHighlightColor"); }
        }

        public static NSColor ControlShadowColor
        {
            get { return SystemColorWithName(@"controlShadowColor"); }
        }

        public static NSColor ControlDarkShadowColor
        {
            get { return SystemColorWithName(@"controlDarkShadowColor"); }
        }

        public static NSColor ControlTextColor
        {
            get { return SystemColorWithName(@"controlTextColor"); }
        }

        public static NSColor disabledControlTextColor
        {
            get { return SystemColorWithName(@"disabledControlTextColor"); }
        }

        public static NSColor gridColor
        {
            get { return SystemColorWithName(@"gridColor"); }
        }

        public static NSColor headerColor
        {
            get { return SystemColorWithName(@"headerColor"); }
        }

        public static NSColor headerTextColor
        {
            get { return SystemColorWithName(@"headerTextColor"); }
        }

        public static NSColor HighlightColor
        {
            get { return SystemColorWithName(@"highlightColor"); }
        }

        public static NSColor keyboardFocusIndicatorColor
        {
            get { return SystemColorWithName(@"keyboardFocusIndicatorColor"); }
        }

        public static NSColor knobColor
        {
            get { return SystemColorWithName(@"knobColor"); }
        }

        public static NSColor scrollBarColor
        {
            get { return SystemColorWithName(@"scrollBarColor"); }
        }

        public static NSColor secondarySelectedControlColor
        {
            get { return SystemColorWithName(@"secondarySelectedControlColor"); }
        }

        public static NSColor selectedControlColor
        {
            get { return SystemColorWithName(@"selectedControlColor"); }
        }

        public static NSColor selectedControlTextColor
        {
            get { return SystemColorWithName(@"selectedControlTextColor"); }
        }

        public static NSColor selectedKnobColor
        {
            get { return SystemColorWithName(@"selectedKnobColor"); }
        }

        public static NSColor selectedMenuItemColor
        {
            get { return SystemColorWithName(@"selectedMenuItemColor"); }
        }

        public static NSColor selectedMenuItemTextColor
        {
            get { return SystemColorWithName(@"selectedMenuItemTextColor"); }
        }

        public static NSColor selectedTextBackgroundColor
        {
            get { return SystemColorWithName(@"selectedTextBackgroundColor"); }
        }

        public static NSColor selectedTextColor
        {
            get { return SystemColorWithName(@"selectedTextColor"); }
        }

        public static NSColor ShadowColor
        {
            get { return SystemColorWithName(@"shadowColor"); }
        }

        public static NSColor textBackgroundColor
        {
            get { return SystemColorWithName(@"textBackgroundColor"); }
        }

        public static NSColor textColor
        {
            get { return SystemColorWithName(@"textColor"); }
        }

        public static NSColor WindowBackgroundColor
        {
            get { return SystemColorWithName(@"windowBackgroundColor"); }
        }

        public static NSColor windowFrameColor
        {
            get { return SystemColorWithName(@"windowFrameColor"); }
        }

        public static NSColor windowFrameTextColor
        {
            get { return SystemColorWithName(@"windowFrameTextColor"); }
        }

        public static NSArray ControlAlternatingRowBackgroundColors
        {
            get { return NSArray.ArrayWithObjects(SystemColorWithName(@"rowBackgroundColor"), SystemColorWithName(@"alternateRowBackgroundColor"), null); }
        }

        ////////////////////////////////////////////////////////////
        //
        // Instance methods
        //



        //- (NSString*) description
        //{
        //  [self subclassResponsibility(_cmd);
        //  return null;
        //}


        public virtual void GetCyan(ref float cyan, ref float magenta, ref float yellow, ref float black, ref float alpha)
        {
            //[NSException raise: NSInternalInconsistencyException format: @"Called getCyan:magenta:yellow:black:alpha: on non-CMYK colour");
        }


        public virtual void GetHue(ref float hue, ref float saturation, ref float brightness, ref float alpha)
        {
            //[NSException raise: NSInternalInconsistencyException format: @"Called getHue:saturation:brightness:alpha: on non-RGB colour");
        }


        public virtual void GetRed(ref float red, ref float green, ref float blue, ref float alpha)
        {
            //[NSException raise: NSInternalInconsistencyException format: @"Called getRed:green:blue:alpha: on non-RGB colour");
        }


        public virtual void GetWhite(ref float white, ref float alpha)
        {
            //[NSException raise: NSInternalInconsistencyException format: @"Called getWhite:alpha: on non-grayscale colour");
        }

        public virtual bool IsEqual(id other)
        {
            if (other == this)
                return true;
            if (other.IsKindOfClass(NSColorClass) == true)
                return false;
            else
            {
                //[self subclassResponsibility: _cmd);
                return true;
            }
        }

        public virtual float AlphaComponent
        {
            get { return 1.0f; }
        }


        public virtual float BlackComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called blackComponent on non-CMYK colour");
                return 0.0f;
            }
        }


        public virtual float BlueComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called blueComponent on non-RGB colour");
                return 0.0f;
            }
        }


        public virtual float BrightnessComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called brightnessComponent on non-RGB colour");
                return 0.0f;
            }
        }

        public virtual NSString CatalogNameComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called catalogNameComponent on colour with name");
                return null;
            }
        }

        public virtual NSString ColorNameComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called colorNameComponent on colour with name");
                return null;
            }
        }


        public virtual float CyanComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called cyanComponent on non-CMYK colour");
                return 0.0f;
            }
        }


        public virtual float GreenComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called greenComponent on non-RGB colour");
                return 0.0f;
            }
        }


        public virtual float HueComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called hueComponent on non-RGB colour");
                return 0.0f;
            }
        }

        public virtual NSString LocalizedCatalogNameComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called localizedCatalogNameComponent on colour with name");
                return null;
            }
        }

        public virtual NSString LocalizedColorNameComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called localizedColorNameComponent on colour with name");
                return null;
            }
        }


        public virtual float MagentaComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called magentaComponent on non-CMYK colour");
                return 0.0f;
            }
        }


        public virtual float RedComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called redComponent on non-RGB colour");
                return 0.0f;
            }
        }


        public virtual float SaturationComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called saturationComponent on non-RGB colour");
                return 0.0f;
            }
        }


        public virtual float WhiteComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called whiteComponent on non-grayscale colour");
                return 0.0f;
            }
        }

        public virtual NSImage PatternImage
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called patternImage on non-pattern colour");
                return null;
            }
        }


        public virtual float YellowComponent
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called yellowComponent on non-CMYK colour");
                return 0.0f;
            }
        }

        //
        // Converting to Another Color Space
        //
        public virtual NSString ColorSpaceName
        {
            get
            {
                //[self subclassResponsibility: _cmd);
                return null;
            }
        }

        public virtual NSColor ColorUsingColorSpaceName(NSString colorSpace)
        {
            return ColorUsingColorSpaceName(colorSpace, null);
        }


        public virtual NSColor ColorUsingColorSpaceName(NSString colorSpace, NSDictionary deviceDescription)
        {
            if (colorSpace == null)
            {
                if (deviceDescription != null)
                    colorSpace = (NSString)deviceDescription.ObjectForKey(NSColorSpace.NSDeviceColorSpaceName);
                if (colorSpace == null)
                    colorSpace = NSColorSpace.NSDeviceRGBColorSpace;
            }
            if (colorSpace.IsEqualToString(ColorSpaceName))
            {
                return this;
            }

            if (colorSpace.IsEqualToString(NSColorSpace.NSNamedColorSpace))
            {
                // FIXME: We cannot convert to named color space.
                return null;
            }

            //[self subclassResponsibility: _cmd);

            return null;
        }

        public static NSColor ColorWithColorSpace(NSColorSpace space, float[] comp, int number)
        {
            if (space == NSColorSpace.GenericRGBColorSpace && (number == 4))
            {
                return ColorWithCalibratedRed(comp[0], comp[1], comp[2], comp[3]);
            }
            if (space == NSColorSpace.DeviceRGBColorSpace && (number == 4))
            {
                return ColorWithDeviceRed(comp[0], comp[1], comp[2], comp[3]);
            }
            if (space == NSColorSpace.GenericGrayColorSpace && (number == 2))
            {
                return NSColor.ColorWithCalibratedWhite(comp[0], comp[1]);
            }
            if (space == NSColorSpace.DeviceGrayColorSpace && (number == 2))
            {
                return NSColor.ColorWithDeviceWhite(comp[0], comp[1]);
            }
            if (space == NSColorSpace.GenericCMYKColorSpace && (number == 5))
            {
                return NSColor.ColorWithDeviceCyan(comp[0], comp[1], comp[2], comp[3], comp[4]);
            }
            if (space == NSColorSpace.DeviceCMYKColorSpace && (number == 5))
            {
                return NSColor.ColorWithDeviceCyan(comp[0], comp[1], comp[2], comp[3], comp[4]);
            }

            return null;
        }

        public virtual NSColorSpace ColorSpace
        {
            get
            {
                NSString name = ColorSpaceName;

                if (name.IsEqualToString(NSColorSpace.NSCalibratedRGBColorSpace))
                    return NSColorSpace.GenericRGBColorSpace;
                if (name.IsEqualToString(NSColorSpace.NSDeviceRGBColorSpace))
                    return NSColorSpace.DeviceRGBColorSpace;
                if (name.IsEqualToString(NSColorSpace.NSCalibratedBlackColorSpace)
                  || name.IsEqualToString(NSColorSpace.NSCalibratedWhiteColorSpace))
                    return NSColorSpace.GenericGrayColorSpace;
                if (name.IsEqualToString(NSColorSpace.NSDeviceBlackColorSpace)
                  || name.IsEqualToString(NSColorSpace.NSDeviceWhiteColorSpace))
                    return NSColorSpace.DeviceGrayColorSpace;
                if (name.IsEqualToString(NSColorSpace.NSDeviceCMYKColorSpace))
                    return NSColorSpace.DeviceCMYKColorSpace;

                return null;
            }
        }






        public virtual NSColor ColorUsingColorSpace(NSColorSpace space)
        {
            NSString colorSpaceName;

            if (space == ColorSpace)
            {
                return this;
            }

            switch (space.ColorSpaceModel)
            {
                default:
                case NSColorSpaceModel.NSUnknownColorSpaceModel:
                    return null;
                case NSColorSpaceModel.NSGrayColorSpaceModel:
                    colorSpaceName = NSColorSpace.NSDeviceWhiteColorSpace;
                    break;
                case NSColorSpaceModel.NSRGBColorSpaceModel:
                    colorSpaceName = NSColorSpace.NSDeviceRGBColorSpace;
                    break;
                case NSColorSpaceModel.NSCMYKColorSpaceModel:
                    colorSpaceName = NSColorSpace.NSDeviceCMYKColorSpace;
                    break;
                case NSColorSpaceModel.NSLABColorSpaceModel:
                    return null;
                case NSColorSpaceModel.NSDeviceNColorSpaceModel:
                    return null;
            }
            return ColorUsingColorSpaceName(colorSpaceName, null);
        }

       
        public virtual uint Hash()
        {
            int nums = NumberOfComponents;
            float[] floats = new float[nums];
            uint h = 0;
            uint i;

            GetComponents(ref floats);
            for (i = 0; i < floats.Length; i++)
            {
                h = (h << 5) + h + (uint)floats[i];
            }

            return h;
        }

        public virtual int NumberOfComponents
        {
            get
            {
                //[NSException raise: NSInternalInconsistencyException format: @"Called numberOfComponents on non-standard colour"];
                return 0;
            }
        }

        public virtual void GetComponents(ref float[] components)
        {
            NSException.Raise("NSInternalInconsistencyException", @"Called getComponents: on non-standard colour");
        }



        public virtual NSColor BlendedColorWithFraction(float fraction, NSColor aColor)
        {
            NSColor myColor = ColorUsingColorSpaceName(NSColorSpace.NSCalibratedRGBColorSpace);
            NSColor other = aColor.ColorUsingColorSpaceName(NSColorSpace.NSCalibratedRGBColorSpace);
            float mr, mg, mb, ma, or, og, ob, oa, red, green, blue, alpha;

            if (fraction <= 0.0f)
            {
                return this;
            }
            if (fraction >= 1.0f)
            {
                return aColor;
            }
            if (myColor == null || other == null)
            {
                return null;
            }

            mr = mg = mb = ma = 0;
            myColor.GetRed(ref mr, ref mg, ref mb, ref ma);
            or = og = ob = oa = 0;
            other.GetRed(ref or, ref og, ref ob, ref oa);
            red = fraction * or + (1 - fraction) * mr;
            green = fraction * og + (1 - fraction) * mg;
            blue = fraction * ob + (1 - fraction) * mb;
            alpha = fraction * oa + (1 - fraction) * ma;
            return ColorWithCalibratedRed(red, green, blue, alpha);
            //  return [NSColorClass colorWithCalibratedRed: red green: green blue: blue alpha: alpha];
        }

        public virtual NSColor ColorWithAlphaComponent(float alpha)
        {
            return this;
        }

        public virtual NSColor HighlightWithLevel(float level)
        {
            return BlendedColorWithFraction(level, HighlightColor);
        }

        public virtual NSColor ShadowWithLevel(float level)
        {
            return BlendedColorWithFraction(level, ShadowColor);
        }

        public virtual void WriteToPasteboard(NSPasteboard pasteBoard)
        {
            //NSData colorData = [NSArchiver archivedDataWithRootObject: self];

            //if (colorData != null)
            //    [pasteBoard setData: colorData forType: NSColorPboardType];
        }


        //
        // Drawing
        //
        public virtual void DrawSwatchInRect(NSRect rect)
        {
            
        }

        public virtual void Set()
        {
            ColorUsingColorSpaceName(NSColorSpace.NSDeviceRGBColorSpace).Set();
        }

        public virtual void SetFill()
        {
            ColorUsingColorSpaceName(NSColorSpace.NSDeviceRGBColorSpace).SetFill();
        }

        public virtual void SetStroke()
        {
            ColorUsingColorSpaceName(NSColorSpace.NSDeviceRGBColorSpace).SetStroke();
        }

        public override void EncodeWithCoder(NSCoder aCoder)
        {

        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (aDecoder.AllowsKeyedCoding)
            {
                int colorSpace = aDecoder.DecodeIntForKey(@"NSColorSpace");

                if ((colorSpace == 1) || (colorSpace == 2))
                {
                    int length = 0;
	                byte[] data = null;
	                double red = 0.0;
	                double green = 0.0;
	                double blue = 0.0;
	                double alpha = 1.0;
	                NSString str = null;
	                NSScanner scanner = null;

                    if (aDecoder.ContainsValueForKey(@"NSRGB"))
                    {
                        data = aDecoder.DecodeBytesForKey(@"NSRGB", ref length);
                        str = (NSString)NSString.Alloc().InitWithBytes(data, (uint)data.Length, NSStringEncoding.NSASCIIStringEncoding);
                        scanner = (NSScanner)NSScanner.Alloc().InitWithString(str);
                        scanner.ScanDouble(ref red);
                        scanner.ScanDouble(ref green);
                        scanner.ScanDouble(ref blue);
                        scanner.ScanDouble(ref alpha);
                    }
                    if (colorSpace == 1)
                    {
                        self = NSColor.ColorWithCalibratedRed((float)red, (float)green, (float)blue, (float)alpha);
                    }
                    else
                    {
                        self = NSColor.ColorWithDeviceRed((float)red, (float)green, (float)blue, (float)alpha);
                    }
                }
                else if ((colorSpace == 3) || (colorSpace == 4))
                {
                    int length = 0;
                    byte[] data = null;
                    double white = 0.0;
                    double alpha = 0.0;
                    NSString str = null;
                    NSScanner scanner = null;

                    if (aDecoder.ContainsValueForKey(@"NSWhite"))
                    {
                        data = aDecoder.DecodeBytesForKey(@"NSWhite", ref length);
                        str = (NSString)NSString.Alloc().InitWithBytes(data, (uint)data.Length, NSStringEncoding.NSASCIIStringEncoding);
                        scanner = (NSScanner)NSScanner.Alloc().InitWithString(str);
                        scanner.ScanDouble(ref white);
                        scanner.ScanDouble(ref alpha);
                    }
                    if (colorSpace == 3)
                    {
                        self = NSColor.ColorWithCalibratedWhite((float)white, (float)alpha);
                    }
                    else
                    {
                        self = NSColor.ColorWithDeviceWhite((float)white, (float)alpha);
                    }
                }
                else if (colorSpace == 5)
                {
                    int length = 0;
                    byte[] data = null;
                    double cyan = 0.0;
                    double yellow = 0.0;
                    double magenta = 0.0;
                    double black = 0.0;
                    double alpha = 1.0;
                    NSString str = null;
                    NSScanner scanner = null;

                    if (aDecoder.ContainsValueForKey(@"NSCYMK"))
                    {
                        data = aDecoder.DecodeBytesForKey(@"NSCYMK", ref length);
                        str = (NSString)NSString.Alloc().InitWithBytes(data, (uint)data.Length, NSStringEncoding.NSASCIIStringEncoding);
                        scanner = (NSScanner)NSScanner.Alloc().InitWithString(str);
                        scanner.ScanDouble(ref cyan);
                        scanner.ScanDouble(ref yellow);
                        scanner.ScanDouble(ref magenta);
                        scanner.ScanDouble(ref black);
                        scanner.ScanDouble(ref alpha);
                    }

                    self = NSColor.ColorWithDeviceCyan((float)cyan, (float)magenta, (float)yellow, (float)black, (float)alpha);
                }
                else if (colorSpace == 6)
                {
                    NSString catalog = (NSString)aDecoder.DecodeObjectForKey(@"NSCatalogName");
                    NSString name = (NSString)aDecoder.DecodeObjectForKey(@"NSColorName");
                    //NSColor *color = [aDecoder decodeObjectForKey: @"NSColor"];

                    self = NSColor.ColorWithCatalogName(catalog, name);
                }
                else if (colorSpace == 10)
                {
                    NSImage image = (NSImage)aDecoder.DecodeObjectForKey(@"NSImage");

                    self = NSColor.ColorWithPatternImage(image);
                }

                return self;
            }
            else if (aDecoder.VersionForClassName(@"NSColor") < 3)
            {
                float red;
                float green;
                float blue;
                float cyan;
                float magenta;
                float yellow;
                float black;
                float hue;
                float saturation;
                float brightness;
                float alpha;
                float white;

                int active_component;
                int valid_components;
                NSString colorspace_name;
                NSString catalog_name;
                NSString color_name;
                bool is_clear;

                // Version 1
                aDecoder.DecodeValueOfObjCType2<float>(out red);
                aDecoder.DecodeValueOfObjCType2<float>(out green);
                aDecoder.DecodeValueOfObjCType2<float>(out blue);
                aDecoder.DecodeValueOfObjCType2<float>(out alpha);
                aDecoder.DecodeValueOfObjCType2<bool>(out is_clear);


                // Version 2
                aDecoder.DecodeValueOfObjCType2<NSString>(out colorspace_name);
                aDecoder.DecodeValueOfObjCType2<NSString>(out catalog_name);
                aDecoder.DecodeValueOfObjCType2<NSString>(out color_name);
                aDecoder.DecodeValueOfObjCType2<float>(out cyan);
                aDecoder.DecodeValueOfObjCType2<float>(out magenta);
                aDecoder.DecodeValueOfObjCType2<float>(out yellow);
                aDecoder.DecodeValueOfObjCType2<float>(out black);
                aDecoder.DecodeValueOfObjCType2<float>(out hue);
                aDecoder.DecodeValueOfObjCType2<float>(out saturation);
                aDecoder.DecodeValueOfObjCType2<float>(out brightness);
                aDecoder.DecodeValueOfObjCType2<float>(out white);

                aDecoder.DecodeValueOfObjCType2<int>(out active_component);
                aDecoder.DecodeValueOfObjCType2<int>(out valid_components);

                if (colorspace_name.IsEqualToString(@"NSDeviceCMYKColorSpace"))
                {
                    self = NSColor.ColorWithDeviceCyan((float)cyan, (float)magenta, (float)yellow, (float)black, (float)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSDeviceWhiteColorSpace"))
                {
                    self = NSColor.ColorWithDeviceWhite((float)white, (float)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSCalibratedWhiteColorSpace"))
                {
                    self = NSColor.ColorWithCalibratedWhite((float)white, (float)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSDeviceRGBColorSpace"))
                {
                    self = NSColor.ColorWithDeviceRed((float)red, (float)green, (float)blue, (float)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSCalibratedRGBColorSpace"))
                {
                    self = NSColor.ColorWithCalibratedRed((float)red, (float)green, (float)blue, (float)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSNamedColorSpace"))
                {
                    self = NSColor.ColorWithCatalogName(catalog_name, color_name);
                }
                return self;
            }
            else
            {
                 NSString	csName = (NSString)aDecoder.DecodeObject();

                 if (csName.IsEqualToString(@"NSDeviceCMYKColorSpace"))
                 {
                     self = GSDeviceCMYKColor.Alloc().InitWithCoder(aDecoder);
                 }
                 else if (csName.IsEqualToString(@"NSDeviceRGBColorSpace"))
                 {
                     self = GSDeviceRGBColor.Alloc().InitWithCoder(aDecoder);
                 }
                 else if (csName.IsEqualToString(@"NSDeviceWhiteColorSpace"))
                 {
                     self = GSDeviceWhiteColor.Alloc().InitWithCoder(aDecoder);
                 }
                 else if (csName.IsEqualToString(@"NSCalibratedWhiteColorSpace"))
                 {
                     self = GSCalibratedWhiteColor.Alloc().InitWithCoder(aDecoder);
                 }
                 else if (csName.IsEqualToString(@"NSCalibratedRGBColorSpace"))
                 {
                     self = GSCalibratedRGBColor.Alloc().InitWithCoder(aDecoder);
                 }
                 else if (csName.IsEqualToString(@"NSNamedColorSpace"))
                 {
                     self = GSNamedColor.Alloc().InitWithCoder(aDecoder);
                 }
                 else
                 {
                     System.Diagnostics.Trace.WriteLine("Unknown colorspace name in decoded color");
                     return null;
                 }

                 return self;
            }
        }

        private static NSColor ColorFromString(NSString str)
        {
            return null;
        }

        private static void DefaultsDidChange(NSNotification notification)
        {
            //FIXME
        }

        private static void ThemeDidActivate(NSNotification notification)
        {
            //FIXME
        }





        #endif //TEMP2

        #endif //TEMP

        public static NSColor ColorWithCalibratedRed(float red, float green, float blue, float alpha)
        {
            return null;
        }

        public static NSColor TextColor
        {
            get { return new NSColor(); }
        }


        public static NSColor TextBackgroundColor
        {
            get { return new NSColor(); }
        }



    }
}
