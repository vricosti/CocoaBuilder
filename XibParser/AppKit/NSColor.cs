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


        static NSMutableDictionary namedColors = null;
        static NSRecursiveLock namedColorLock = null;

        protected NSString _catalog_name;
        protected NSString _color_name;
        protected NSString _cached_name_space;
        protected NSColor _cached_color;


        static GSNamedColor() { Initialize(); }
        new static void Initialize()
        {
            namedColorLock = (NSRecursiveLock)NSRecursiveLock.Alloc().Init();
            namedColors = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
        }

        public virtual NSColor InitWithCatalogName(NSString listName, NSString colorName)
        {
            NSColor self = this;

            NSMutableDictionary d;
            NSColor c;

            _catalog_name = listName;
            _color_name = colorName;
            namedColorLock.Lock();
            d = (NSMutableDictionary)namedColors.ObjectForKey(_catalog_name);
            if (d == null)
            {
                d = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
                namedColors.SetObjectForKey(d, _catalog_name);
            }
            c = (NSColor)d.ObjectForKey(_color_name);
            if (c == null)
            {
                d.SetObjectForKey(this, _color_name);
            }
            else
            {
                self = c;
            }
            namedColorLock.Unlock();

            return self;
        }

        [ObjcProp(GetName = "colorSpaceName")]
        public override NSString ColorSpaceName
        {
            get { return NSColorSpace.NSNamedColorSpace; }
        }

        //[ObjcProp(GetName = "description")]
        public override NSString Description()
        {
            return "";
        }

        [ObjcProp(GetName = "catalogNameComponent")]
        public override NSString CatalogNameComponent
        {
            get { return _catalog_name; }
        }

        [ObjcProp(GetName = "colorNameComponent")]
        public override NSString ColorNameComponent
        {
            get { return _color_name; }
        }

        [ObjcProp(GetName = "localizedCatalogNameComponent")]
        public override NSString LocalizedCatalogNameComponent
        {
            get { return _catalog_name; }
        }

        [ObjcProp(GetName = "localizedColorNameComponent")]
        public override NSString LocalizedColorNameComponent
        {
            get { return _color_name; }
        }

        [ObjcMethod("hash")]
        public override uint Hash()
        {
            return _catalog_name.Hash() + _color_name.Hash();
        }

        public override bool IsEqual(id other)
        {
            if (other == (id)this)
                return true;

            if (other.IsKindOfClass(GSNamedColor.Class) == false
                || ((GSNamedColor)other).CatalogNameComponent.IsEqualToString(_catalog_name) == false
                || ((GSNamedColor)other).ColorNameComponent.IsEqualToString(_color_name) == false)
            {
                return false;
            }
            return true;
        }

        public override NSColor ColorUsingColorSpaceName(NSString colorSpace, NSDictionary deviceDescription)
        {
            NSColorList list;
            NSColor real;

            if (colorSpace == null)
            {
                if (deviceDescription != null)
                    colorSpace = (NSString)deviceDescription.ObjectForKey(NSColorSpace.NSDeviceColorSpaceName);
                // FIXME: If the deviceDescription is nil, we should get it from the
                // current view or printer
                if (colorSpace == null)
                    colorSpace = NSColorSpace.NSCalibratedRGBColorSpace;
            }
            if (colorSpace.IsEqualToString(this.ColorSpaceName))
            {
                return this;
            }

            namedColorLock.Lock();
            if (false == colorSpace.IsEqualToString(_cached_name_space))
            {
                list = NSColorList.ColorListNamed(_catalog_name);
                real = list.ColorWithKey(_color_name);
                _cached_color = real.ColorUsingColorSpaceName(colorSpace, deviceDescription);
                _cached_name_space = colorSpace;
            }
            real = _cached_color;
            namedColorLock.Unlock();

            return real;
        }

        public virtual void Recache()
        {
            namedColorLock.Lock();
            _cached_name_space = null;
            _cached_color = null;
            namedColorLock.Unlock();
        }

    }

    public class GSWhiteColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSWhiteColor));
        new public static GSWhiteColor Alloc() { return new GSWhiteColor(); }

        protected double _white_component;
        protected double _alpha_component;

        public override double AlphaComponent
        {
            get { return _alpha_component; }
        }

        public override double WhiteComponent
        {
            get { return _white_component; }
        }

        public override void GetComponents(ref double[] components)
        {
            components[0] = _white_component;
            components[1] = _alpha_component;
        }

        public override int NumberOfComponents
        {
            get { return 2; }
        }

    }

    public class GSDeviceWhiteColor : GSWhiteColor
    {
        new public static Class Class = new Class(typeof(GSDeviceWhiteColor));
        new public static GSDeviceWhiteColor Alloc() { return new GSDeviceWhiteColor(); }

        public override NSString ColorSpaceName
        {
            get { return NSColorSpace.NSDeviceWhiteColorSpace; ; }
        }



        public virtual NSColor InitWithDeviceWhite(double white, double alpha)
        {
            NSColor self = this;

            if (white < 0.0) white = 0.0;
            else if (white > 1.0) white = 1.0;
            _white_component = white;

            if (alpha < 0.0) alpha = 0.0;
            else if (alpha > 1.0) alpha = 1.0;
            _alpha_component = alpha;

            return self;
        }
    }

    public class GSCalibratedWhiteColor : GSWhiteColor
    {
        new public static Class Class = new Class(typeof(GSCalibratedWhiteColor));
        new public static GSCalibratedWhiteColor Alloc() { return new GSCalibratedWhiteColor(); }


        public override NSString ColorSpaceName
        {
            get { return NSColorSpace.NSCalibratedWhiteColorSpace; ; }
        }

        public virtual NSColor InitWithCalibratedWhite(double white, double alpha)
        {
            NSColor self = this;

            if (white < 0.0) white = 0.0;
            else if (white > 1.0) white = 1.0;
            _white_component = white;

            if (alpha < 0.0) alpha = 0.0;
            else if (alpha > 1.0) alpha = 1.0;
            _alpha_component = alpha;

            return self;
        }
    }

    public class GSDeviceCMYKColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSDeviceCMYKColor));
        new public static GSDeviceCMYKColor Alloc() { return new GSDeviceCMYKColor(); }

        protected double _cyan_component;
        protected double _magenta_component;
        protected double _yellow_component;
        protected double _black_component;
        protected double _alpha_component;

        public override NSString ColorSpaceName
        {
            get { return NSColorSpace.NSDeviceCMYKColorSpace; ; }
        }

        public override double AlphaComponent
        {
            get { return _alpha_component; }
        }

        public override double BlackComponent
        {
            get { return _black_component; }
        }

        public override double CyanComponent
        {
            get { return _cyan_component; }
        }

        public override double MagentaComponent
        {
            get { return _magenta_component; }
        }

        public override double YellowComponent
        {
            get { return _yellow_component; }
        }


        public override void GetComponents(ref double[] components)
        {
            components[0] = _cyan_component;
            components[1] = _magenta_component;
            components[2] = _yellow_component;
            components[3] = _black_component;
            components[4] = _alpha_component;
        }

        public override int NumberOfComponents
        {
            get { return 5; }
        }

        public virtual NSColor InitWithDeviceCyan(double cyan, double magenta, double yellow, double black, double alpha)
        {
            NSColor self = this;

            if (cyan < 0.0) cyan = 0.0;
            else if (cyan > 1.0) cyan = 1.0;
            _cyan_component = cyan;

            if (magenta < 0.0) magenta = 0.0;
            else if (magenta > 1.0) magenta = 1.0;
            _magenta_component = magenta;

            if (yellow < 0.0) yellow = 0.0;
            else if (yellow > 1.0) yellow = 1.0;
            _yellow_component = yellow;

            if (black < 0.0) black = 0.0;
            else if (black > 1.0) black = 1.0;
            _black_component = black;

            if (alpha < 0.0) alpha = 0.0;
            else if (alpha > 1.0) alpha = 1.0;
            _alpha_component = alpha;

            return self;
        }
    }

    public class GSRGBColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSRGBColor));
        new public static GSRGBColor Alloc() { return new GSRGBColor(); }

        protected double _red_component;
        protected double _green_component;
        protected double _blue_component;
        protected double _hue_component;
        protected double _saturation_component;
        protected double _brightness_component;
        protected double _alpha_component;

        public override double AlphaComponent
        {
            get { return _alpha_component; }
        }

        public override double RedComponent
        {
            get { return _red_component; }
        }

        public override double GreenComponent
        {
            get { return _green_component; }
        }

        public override double BlueComponent
        {
            get { return _blue_component; }
        }

        public override double HueComponent
        {
            get { return _hue_component; }
        }

        public override double SaturationComponent
        {
            get { return _saturation_component; }
        }

        public override double BrightnessComponent
        {
            get { return _brightness_component; }
        }

        public override void GetComponents(ref double[] components)
        {
            components[0] = _red_component;
            components[1] = _green_component;
            components[2] = _blue_component;
            components[3] = _alpha_component;
        }

        public override int NumberOfComponents
        {
            get { return 4; }
        }

        public override void GetHueSaturationBrightnessAlpha(ref double hue, ref double saturation, ref double brightness, ref double alpha)
        {
            hue = _hue_component;
            saturation = _saturation_component;
            brightness = _brightness_component;
            alpha = _alpha_component;
        }

        public override void GetRedGreenBlueAlpha(ref double red, ref double green, ref double blue, ref double alpha)
        {

            red = _red_component;
            green = _green_component;
            blue = _blue_component;
            alpha = _alpha_component;
        }

        public override bool IsEqual(id other)
        {
            if (other == (id)this)
                return true;

            if (other.IsKindOfClass(GSRGBColor.Class) == false
                || ((GSRGBColor)other).RedComponent != _red_component
                || ((GSRGBColor)other).GreenComponent != _green_component
                || ((GSRGBColor)other).BlueComponent != _blue_component
                || ((GSRGBColor)other).AlphaComponent != _alpha_component)
            {
                return false;
            }
            return true;
        }
    }

    public class GSDeviceRGBColor : GSRGBColor
    {
        new public static Class Class = new Class(typeof(GSDeviceRGBColor));
        new public static GSDeviceRGBColor Alloc() { return new GSDeviceRGBColor(); }


        public override NSString ColorSpaceName
        {
            get { return NSColorSpace.NSDeviceRGBColorSpace; ; }
        }

        public virtual NSColor InitWithDeviceRed(double red, double green, double blue, double alpha)
        {
            throw new NotImplementedException();
            return null;
        }

        public virtual NSColor InitWithDeviceHue(double hue, double saturation, double brightness, double alpha)
        {
            throw new NotImplementedException();
            return null;
        }
                 

    }

    public class GSCalibratedRGBColor : GSRGBColor
    {
        new public static Class Class = new Class(typeof(GSCalibratedRGBColor));
        new public static GSCalibratedRGBColor Alloc() { return new GSCalibratedRGBColor(); }

        public override NSString ColorSpaceName
        {
            get { return NSColorSpace.NSCalibratedRGBColorSpace; ; }
        }

        public virtual NSColor InitWithCalibratedRed(double red, double green, double blue, double alpha)
        {
            NSColor self = this;

            if (red < 0.0) red = 0.0;
            else if (red > 1.0) red = 1.0;
            _red_component = red;

            if (green < 0.0) green = 0.0;
            else if (green > 1.0) green = 1.0;
            _green_component = green;

            if (blue < 0.0) blue = 0.0;
            else if (blue > 1.0) blue = 1.0;
            _blue_component = blue;

            {
                double r = _red_component;
                double g = _green_component;
                double b = _blue_component;

                if (r == g && r == b)
                {
                    _hue_component = 0;
                    _saturation_component = 0;
                    _brightness_component = r;
                }
                else
                {
                    double H;
                    double V;
                    double Temp;
                    double diff;

                    V = (r > g ? r : g);
                    V = (b > V ? b : V);
                    Temp = (r < g ? r : g);
                    Temp = (b < Temp ? b : Temp);
                    diff = V - Temp;
                    if (V == r)
                    {
                        H = (g - b) / diff;
                    }
                    else if (V == g)
                    {
                        H = (b - r) / diff + 2;
                    }
                    else
                    {
                        H = (r - g) / diff + 4;
                    }
                    if (H < 0)
                    {
                        H += 6;
                    }
                    _hue_component = H / 6;
                    _saturation_component = diff / V;
                    _brightness_component = V;
                }
            }

            if (alpha < 0.0) alpha = 0.0;
            else if (alpha > 1.0) alpha = 1.0;
            _alpha_component = alpha;

            return self;
        }

        public virtual NSColor InitWithCalibratedHue(double hue, double saturation, double brightness, double alpha)
        {
            throw new NotImplementedException();
            return null;
        }
			
        
    }

    public class GSPatternColor : NSColor
    {
        new public static Class Class = new Class(typeof(GSPatternColor));
        new public static GSPatternColor Alloc() { return new GSPatternColor(); }

        protected NSImage _pattern;

        public override NSString ColorSpaceName
        {
            get { return NSColorSpace.NSPatternColorSpace; ; }
        }

        public virtual NSColor InitWithPatternImage(NSImage pattern)
        {
            NSColor self = this;

            _pattern = pattern;

            return self;
        }

        
    }



    public class NSColor : NSObject
    {
        public const double NSBlack = 0.0f;
        public const double NSDarkGray = 0.333f;
        public const double NSGray = 0.5f;
        public const double NSLightGray = 0.667f;
        public const double NSWhite = 1.0f;


        new public static Class Class = new Class(typeof(NSColor));
        new public static NSColor Alloc() { return new NSColor(); }
        static Class NSColorClass;

        static bool Gnustep_gui_ignores_alpha = true;
        static NSColorList SystemColors = null;
        static NSColorList DefaultSystemColors = null;
        static NSMutableDictionary ColorStrings = null;
        static NSMutableDictionary SystemDict = null;


        public NSColor()
        {
        }

        //#if TEMP
        static NSColor() { Initialize(); }
        public static void Initialize()
        {
            NSColorClass = Class;

            // Set the version number
            //[self setVersion: 3);

            // ignore alpha by default
            Gnustep_gui_ignores_alpha = true;

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
            lightGray = NSString.StringWithFormat(@"%g %g %g", (double)NSLightGray, (double)NSLightGray, (double)NSLightGray);
            gray = NSString.StringWithFormat(@"%g %g %g", (double)NSGray, (double)NSGray, (double)NSGray);
            darkGray = NSString.StringWithFormat(@"%g %g %g", (double)NSDarkGray, (double)NSDarkGray, (double)NSDarkGray);
            black = NSString.StringWithFormat(@"%g %g %g", (double)NSBlack, (double)NSBlack, (double)NSBlack);

            ColorStrings = (NSMutableDictionary)NSMutableDictionary.Alloc().InitWithObjectsAndKeys(
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

            SystemColors = NSColorList.ColorListNamed(@"System");
            DefaultSystemColors = (NSColorList)NSColorList.Alloc().InitWithName(@"System");
            NSColorList._SetDefaultSystemColorList(DefaultSystemColors);
            if (SystemColors == null)
            {
                SystemColors = DefaultSystemColors;
            }

            {
                NSEnumerator enumerator;
                NSString key;

                // Set up default system colors

                enumerator = ColorStrings.KeyEnumerator();

                while ((key = (NSString)enumerator.NextObject()) != null)
                {
                    NSColor color;

                    if ((color = SystemColors.ColorWithKey(key)) == null)
                    {
                        NSString aColorString;

                        aColorString = (NSString)ColorStrings.ObjectForKey(key);
                        color = NSColor.ColorFromString(aColorString);

                        //NSCAssert1(color, @"couldn't get default system color %@", key);
                        SystemColors.SetColor(color, key);
                    }
                    if (DefaultSystemColors != SystemColors)
                    {
                        DefaultSystemColors.SetColor(color, key);
                    }
                }
            }

            SystemDict = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
        }


        public static NSColor SystemColorWithName(NSString name)
        {
            NSColor col = (NSColor)SystemDict.ObjectForKey(name);

            if (col == null)
            {
                col = NSColor.ColorWithCatalogName(@"System", name);
                SystemDict.SetObjectForKey(col, name);
            }

            return col;
        }

        public static NSColor ColorWithCalibratedHue(double hue, double saturation, double brightness, double alpha)
        {
            id color;

            color = GSCalibratedRGBColor.Alloc();
            color = ((GSCalibratedRGBColor)color).InitWithCalibratedHue(hue, saturation, brightness, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithCalibratedRed(double red, double green, double blue, double alpha)
        {
            id color;

            color = GSCalibratedRGBColor.Alloc();
            color = ((GSCalibratedRGBColor)color).InitWithCalibratedRed(red, green, blue, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithCalibratedWhite(double white, double alpha)
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

        public static NSColor ColorWithDeviceCyan(double cyan, double magenta, double yellow, double black, double alpha)
        {
            id color;

            color = GSDeviceCMYKColor.Alloc();
            color = ((GSDeviceCMYKColor)color).InitWithDeviceCyan(cyan, magenta, yellow, black, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithDeviceHue(double hue, double saturation, double brightness, double alpha)
        {
            id color;

            color = GSDeviceRGBColor.Alloc();
            color = ((GSDeviceRGBColor)color).InitWithDeviceHue(hue, saturation, brightness, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithDeviceRed(double red, double green, double blue, double alpha)
        {
            id color;

            color = GSDeviceRGBColor.Alloc();
            color = ((GSDeviceRGBColor)color).InitWithDeviceRed(red, green, blue, alpha);

            return (NSColor)color;
        }

        public static NSColor ColorWithDeviceWhite(double white, double alpha)
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
        //#if TEMP2        


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
            get { return Gnustep_gui_ignores_alpha; }
            set { Gnustep_gui_ignores_alpha = value; }
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


        public virtual void GetCyanMagentaYellowBlackAlpha(ref double cyan, ref double magenta, ref double yellow, ref double black, ref double alpha)
        {
            NSException.Raise(@"NSInternalInconsistencyException", @"Called getCyan:magenta:yellow:black:alpha: on non-CMYK colour");
        }


        public virtual void GetHueSaturationBrightnessAlpha(ref double hue, ref double saturation, ref double brightness, ref double alpha)
        {
            NSException.Raise(@"NSInternalInconsistencyException", @"Called getHue:saturation:brightness:alpha: on non-RGB colour");
        }


        public virtual void GetRedGreenBlueAlpha(ref double red, ref double green, ref double blue, ref double alpha)
        {
            NSException.Raise(@"NSInternalInconsistencyException", @"Called getRed:green:blue:alpha: on non-RGB colour");
        }


        public virtual void GetWhiteAlpha(ref double white, ref double alpha)
        {
            NSException.Raise(@"NSInternalInconsistencyException", @"Called getWhite:alpha: on non-grayscale colour");
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

        public virtual double AlphaComponent
        {
            get { return 1.0; }
        }


        public virtual double BlackComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called blackComponent on non-CMYK colour");
                return 0.0;
            }
        }


        public virtual double BlueComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called blueComponent on non-RGB colour");
                return 0.0;
            }
        }


        public virtual double BrightnessComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called brightnessComponent on non-RGB colour");
                return 0.0;
            }
        }

        public virtual NSString CatalogNameComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called catalogNameComponent on colour with name");
                return null;
            }
        }

        public virtual NSString ColorNameComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called colorNameComponent on colour with name");
                return null;
            }
        }


        public virtual double CyanComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called cyanComponent on non-CMYK colour");
                return 0.0;
            }
        }


        public virtual double GreenComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called greenComponent on non-RGB colour");
                return 0.0;
            }
        }


        public virtual double HueComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called hueComponent on non-RGB colour");
                return 0.0;
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


        public virtual double MagentaComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called magentaComponent on non-CMYK colour");
                return 0.0;
            }
        }


        public virtual double RedComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called redComponent on non-RGB colour");
                return 0.0;
            }
        }


        public virtual double SaturationComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called saturationComponent on non-RGB colour");
                return 0.0;
            }
        }


        public virtual double WhiteComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called whiteComponent on non-grayscale colour");
                return 0.0;
            }
        }

        public virtual NSImage PatternImage
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called patternImage on non-pattern colour");
                return null;
            }
        }


        public virtual double YellowComponent
        {
            get
            {
                NSException.Raise(@"NSInternalInconsistencyException", @"Called yellowComponent on non-CMYK colour");
                return 0.0;
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

        public static NSColor ColorWithColorSpace(NSColorSpace space, double[] comp, int number)
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
            double[] floats = new double[nums];
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
                NSException.Raise("NSInternalInconsistencyException", @"Called numberOfComponents on non-standard colour");
                return 0;
            }
        }

        public virtual void GetComponents(ref double[] components)
        {
            NSException.Raise("NSInternalInconsistencyException", @"Called getComponents: on non-standard colour");
        }



        public virtual NSColor BlendedColorWithFraction(double fraction, NSColor aColor)
        {
            NSColor myColor = ColorUsingColorSpaceName(NSColorSpace.NSCalibratedRGBColorSpace);
            NSColor other = aColor.ColorUsingColorSpaceName(NSColorSpace.NSCalibratedRGBColorSpace);
            double mr, mg, mb, ma, or, og, ob, oa, red, green, blue, alpha;

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
            myColor.GetRedGreenBlueAlpha(ref mr, ref mg, ref mb, ref ma);
            or = og = ob = oa = 0;
            other.GetRedGreenBlueAlpha(ref or, ref og, ref ob, ref oa);
            red = fraction * or + (1 - fraction) * mr;
            green = fraction * og + (1 - fraction) * mg;
            blue = fraction * ob + (1 - fraction) * mb;
            alpha = fraction * oa + (1 - fraction) * ma;
            return ColorWithCalibratedRed(red, green, blue, alpha);
            //  return [NSColorClass colorWithCalibratedRed: red green: green blue: blue alpha: alpha];
        }

        public virtual NSColor ColorWithAlphaComponent(double alpha)
        {
            return this;
        }

        public virtual NSColor HighlightWithLevel(double level)
        {
            return BlendedColorWithFraction(level, HighlightColor);
        }

        public virtual NSColor ShadowWithLevel(double level)
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
                        self = NSColor.ColorWithCalibratedRed((double)red, (double)green, (double)blue, (double)alpha);
                    }
                    else
                    {
                        self = NSColor.ColorWithDeviceRed((double)red, (double)green, (double)blue, (double)alpha);
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
                        self = NSColor.ColorWithCalibratedWhite((double)white, (double)alpha);
                    }
                    else
                    {
                        self = NSColor.ColorWithDeviceWhite((double)white, (double)alpha);
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

                    self = NSColor.ColorWithDeviceCyan((double)cyan, (double)magenta, (double)yellow, (double)black, (double)alpha);
                }
                else if (colorSpace == 6)
                {
                    NSString catalog = (NSString)aDecoder.DecodeObjectForKey(@"NSCatalogName");
                    NSString name = (NSString)aDecoder.DecodeObjectForKey(@"NSColorName");
                    //NSColor color = (NSColor)aDecoder.DecodeObjectForKey(@"NSColor");

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
                double red;
                double green;
                double blue;
                double cyan;
                double magenta;
                double yellow;
                double black;
                double hue;
                double saturation;
                double brightness;
                double alpha;
                double white;

                int active_component;
                int valid_components;
                NSString colorspace_name;
                NSString catalog_name;
                NSString color_name;
                bool is_clear;

                // Version 1
                aDecoder.DecodeValueOfObjCType2<double>(out red);
                aDecoder.DecodeValueOfObjCType2<double>(out green);
                aDecoder.DecodeValueOfObjCType2<double>(out blue);
                aDecoder.DecodeValueOfObjCType2<double>(out alpha);
                aDecoder.DecodeValueOfObjCType2<bool>(out is_clear);


                // Version 2
                aDecoder.DecodeValueOfObjCType2<NSString>(out colorspace_name);
                aDecoder.DecodeValueOfObjCType2<NSString>(out catalog_name);
                aDecoder.DecodeValueOfObjCType2<NSString>(out color_name);
                aDecoder.DecodeValueOfObjCType2<double>(out cyan);
                aDecoder.DecodeValueOfObjCType2<double>(out magenta);
                aDecoder.DecodeValueOfObjCType2<double>(out yellow);
                aDecoder.DecodeValueOfObjCType2<double>(out black);
                aDecoder.DecodeValueOfObjCType2<double>(out hue);
                aDecoder.DecodeValueOfObjCType2<double>(out saturation);
                aDecoder.DecodeValueOfObjCType2<double>(out brightness);
                aDecoder.DecodeValueOfObjCType2<double>(out white);

                aDecoder.DecodeValueOfObjCType2<int>(out active_component);
                aDecoder.DecodeValueOfObjCType2<int>(out valid_components);

                if (colorspace_name.IsEqualToString(@"NSDeviceCMYKColorSpace"))
                {
                    self = NSColor.ColorWithDeviceCyan((double)cyan, (double)magenta, (double)yellow, (double)black, (double)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSDeviceWhiteColorSpace"))
                {
                    self = NSColor.ColorWithDeviceWhite((double)white, (double)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSCalibratedWhiteColorSpace"))
                {
                    self = NSColor.ColorWithCalibratedWhite((double)white, (double)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSDeviceRGBColorSpace"))
                {
                    self = NSColor.ColorWithDeviceRed((double)red, (double)green, (double)blue, (double)alpha);
                }
                else if (colorspace_name.IsEqualToString(@"NSCalibratedRGBColorSpace"))
                {
                    self = NSColor.ColorWithCalibratedRed((double)red, (double)green, (double)blue, (double)alpha);
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
            if (str.HasPrefix(@"{"))
            {
                //NSDictionary dict = null;
                //NSString space = null;
                //double alpha = 0;

                //FIXME
                throw new NotImplementedException();
                return null;
            }
            else
            {
                double r, g, b;
                NSScanner scanner = (NSScanner)NSScanner.Alloc().InitWithString(str);

                r = g = b = 0;
                if (scanner.ScanDouble(ref r) &&
                scanner.ScanDouble(ref g) &&
                scanner.ScanDouble(ref b) &&
                scanner.IsAtEnd())
                {

                    return ColorWithCalibratedRed(r, g, b, 1.0);

                }
            }

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





        //#endif //TEMP2

        //#endif //TEMP

        //public static NSColor ColorWithCalibratedRed(double red, double green, double blue, double alpha)
        //{
        //    return null;
        //}

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
