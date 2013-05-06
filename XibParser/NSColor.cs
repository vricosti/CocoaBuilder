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
            //[self setVersion: 3];

            // ignore alpha by default
            gnustep_gui_ignores_alpha = true;

            // Load or define the system colour list
            InitSystemColors();

            // ensure user defaults are loaded, then use them and watch for changes.
            DefaultsDidChange(null);


            //[[NSNotificationCenter defaultCenter] addObserver: self selector: @selector(defaultsDidChange:) name: NSUserDefaultsDidChangeNotification object: nil];
            // watch for themes which may provide new system color lists
            //[[NSNotificationCenter defaultCenter] addObserver: self selector: @selector(themeDidActivate:) name: GSThemeDidActivateNotification object: nil];

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
            NSColor col = systemDict.ObjectForKey(name);

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

            color = GSCalibratedRGBColor.Alloc().Init();
            color.InitWithCalibratedHue(hue, saturation, brightness, alpha);
            
            return color;
        }

        public static NSColor ColorWithCalibratedRed(float red, float green, float blue, float alpha)
        {
            id color;

            color = GSCalibratedRGBColor.Alloc().Init();
            color.InitWithCalibratedRed(red, green, blue, alpha);

            return color;
        }

        public static NSColor ColorWithCalibratedWhite(float white, float alpha)
        {
            id color;

            color = GSCalibratedWhiteColor.Alloc().Init();
            color = color.InitWithCalibratedWhite(white, alpha);

            return color;
        }

        public static NSColor ColorWithCatalogName(NSString listName, NSString colorName)
        {
            id color;

            color = GSNamedColor.Alloc().Init();
            color = color.InitWithCatalogName(listName, colorName);

            return color;
        }

        public static NSColor ColorWithDeviceCyan(float cyan, float magenta, float yellow, float black, float alpha)
        {
            id color;

            color = GSDeviceCMYKColor.Alloc().Init();
            color = color.InitWithDeviceCyan(cyan, magenta, yellow, black, alpha);

            return color;
        }

        public static NSColor ColorWithDeviceHue(float hue, float saturation, float brightness, float alpha)
        {
            id color;

            color = GSDeviceRGBColor.Alloc().Init();
            color = color.InitWithDeviceHue(hue, saturation, brightness, alpha);

            return color;
        }

        public static NSColor ColorWithDeviceRed(float red, float green, float blue, float alpha)
        {
            id color;

            color = GSDeviceRGBColor.Alloc().Init();
            color = color.InitWithDeviceRed(red, green, blue, alpha);

            return color;
        }

        public static NSColor ColorWithDeviceWhite(float white, float alpha)
        {
            id color;

            color = GSDeviceWhiteColor.Alloc().Init();
            color = color.InitWithDeviceWhite(white, alpha);

            return color;
        }

        public static NSColor ColorForControlTint(NSControlTint controlTint)
        {
            switch (controlTint)
            {
                default:
                case NSControlTint.NSDefaultControlTint:
                    return ColorForControlTint(currentControlTint);
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
            get { return NSBlueControlTint; }
        }

        public static NSColor ColorWithPatternImage(NSImage image)
        {
            id color;

            color = GSPatternColor.Alloc().Init();
            color = color.InitWithPatternImage(image);

            return (NSColor)color;
        }

#endif
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


        public override id InitWithCoder(NSCoder decoder)
        {
            base.InitWithCoder(decoder);

            //foreach (var xElement in decoder.XmlElement.Elements())
            //{
            //    string key = xElement.Attribute("key").Value;
            //    switch (key)
            //    {

                   

            //        default:
            //            System.Diagnostics.Debug.WriteLine("NSColor : unknown key " + key);
            //            break;

            //    }
            //}

            return this;
        }

      

    }
}
