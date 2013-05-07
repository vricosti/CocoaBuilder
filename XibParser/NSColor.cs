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

        /**<p>Returns a NSColor in a NSCalibratedWhiteColorSpace space name.
        with white and alpha values set as 0.0 and 1.0 respectively.</p>
        <p>See Also : +colorWithCalibratedWhite:alpha:</p>
        */
        public static NSColor ClearColor
        {
            get { return ColorWithCalibratedWhite(0.0f, 0.0f); }
        }
#if TEMP2        

/**<p>Returns a NSColor in a NSCalibratedRGBColorSpace space name.
  with red, green, blue and alpha values set as 0.0, 1.0, 1.0 and 1.0
  respectively.</p><p>See Also : +colorWithCalibratedRed:green:blue:alpha:</p>
*/
+ (NSColor*) cyanColor
{
  return [self colorWithCalibratedRed: 0.0
			        green: 1.0
				 blue: 1.0
			        alpha: 1.0];
}

/**<p>Returns a NSColor in a NSCalibratedWhiteColorSpace space name.
  with white and alpha values set as NSDarkGray and 1.0 respectively. </p>
  <p>See Also : +colorWithCalibratedWhite:alpha:</p>
*/
+ (NSColor*) darkGrayColor
{
  return [self colorWithCalibratedWhite: NSDarkGray alpha: 1.0];
}

/**<p>Returns a NSColor in a NSCalibratedWhiteColorSpace space name.
  with white and alpha values set as NSGray and 1.0 respectively. </p>
  <p>See Also: +colorWithCalibratedWhite:alpha:</p>
*/
+ (NSColor*) grayColor
{
  return [self colorWithCalibratedWhite: NSGray alpha: 1.0];
}

/**<p>Returns a NSColor in a  NSCalibratedRGBColorSpace space name.
  with red, green, blue and alpha values set as 0.0, 1.0, 0.0 and 1.0
  respectively </p><p>See Also: +colorWithCalibratedRed:green:blue:alpha:</p>
*/
+ (NSColor*) greenColor
{
  return [self colorWithCalibratedRed: 0.0
			        green: 1.0
				 blue: 0.0
			        alpha: 1.0];
}

/**<p>Returns a NSColor in a NSCalibratedWhiteColorSpace space name.
  with white and alpha values set as NSLightGray and 1.0 respectively </p>
  <p>See Also : +colorWithCalibratedRed:green:blue:alpha:</p>
*/
+ (NSColor*) lightGrayColor
{
  return [self colorWithCalibratedWhite: NSLightGray alpha: 1];
}

/**<p>Returns a NSColor in a NSCalibratedRGBColorSpace space name.
  with red, green, blue and alpha values set as 1.0, 0.0, 1.0 and 1.0
  respectively.</p><p>See Also : +colorWithCalibratedRed:green:blue:alpha:</p>
*/
+ (NSColor*) magentaColor
{
  return [self colorWithCalibratedRed: 1.0
			        green: 0.0
				 blue: 1.0
			        alpha: 1.0];
}


/**<p>Returns a NSColor in a NSCalibratedRGBColorSpace space name.
  with red, green, blue and alpha values set as 1.0, 0.5, 0.0 and 1.0
  respectively.</p><p>See Also: +colorWithCalibratedRed:green:blue:alpha:</p>
*/
+ (NSColor*) orangeColor
{
  return [self colorWithCalibratedRed: 1.0
			        green: 0.5
				 blue: 0.0
			        alpha: 1.0];
}


/**<p>Returns a NSColor in a NSCalibratedRGBColorSpace space name.
  with red, green, blue and alpha values set as 0.5, 0.0, 0.5 and 1.0
  respectively.</p><p>See Also : +colorWithCalibratedRed:green:blue:alpha:</p>
*/
+ (NSColor*) purpleColor
{
  return [self colorWithCalibratedRed: 0.5
			        green: 0.0
				 blue: 0.5
			        alpha: 1.0];
}


/**<p>Returns a NSColor in a NSCalibratedRGBColorSpace space name.
  with red, green, blue and alpha values set as 1.0, 0.0, 0.0 and 1.0
  respectively </p><p>See Also: +colorWithCalibratedRed:green:blue:alpha:</p>
*/
+ (NSColor*) redColor
{
  return [self colorWithCalibratedRed: 1.0
			        green: 0.0
				 blue: 0.0
			        alpha: 1.0];
}

/**<p>Returns a NSColor in a NSCalibratedWhiteColorSpace space name.
  with white and alpha values set as NSWhite and 1.0 respectively. </p>
  <p>See Also : +colorWithCalibratedWhite:alpha:</p>
*/
+ (NSColor*) whiteColor
{
  return [self colorWithCalibratedWhite: NSWhite alpha: 1.0];
}


/**<p>Returns a NSColor in a NSCalibratedRGBColorSpace space name.
  with red, green, blue and alpha values set as 1.0, 0.0, 0.0 and 1.0
  respectively.</p><p>See Also : +colorWithCalibratedRed:green:blue:alpha:</p>
*/
+ (NSColor*) yellowColor
{
  return [self colorWithCalibratedRed: 1.0
			        green: 1.0
				 blue: 0.0
			        alpha: 1.0];
}


/** Returns whether TODO
 *<p>See Also: +setIgnoresAlpha:</p>
 */
+ (BOOL) ignoresAlpha
{
  return gnustep_gui_ignores_alpha;
}

/** TODO
 *<p>See Also: +ignoresAlpha</p>
 */
+ (void) setIgnoresAlpha: (BOOL)flag
{
  gnustep_gui_ignores_alpha = flag;
}

/**<p>Returns the NSColor on the NSPasteboard pasteBoard
   or nil if it does not exists.</p><p>See Also: -writeToPasteboard:</p>
 */
+ (NSColor*) colorFromPasteboard: (NSPasteboard *)pasteBoard
{
  NSData *colorData = [pasteBoard dataForType: NSColorPboardType];

  // FIXME: This should better use the description format
  if (colorData != nil)
    return [NSUnarchiver unarchiveObjectWithData: colorData];

  return nil;
}

//
// System colors stuff.
//
+ (NSColor*) alternateSelectedControlColor
{
  return systemColorWithName(@"alternateSelectedControlColor");
}

+ (NSColor*) alternateSelectedControlTextColor
{
  return systemColorWithName(@"alternateSelectedControlTextColor");
}

+ (NSColor*) controlBackgroundColor
{
  return systemColorWithName(@"controlBackgroundColor");
}

+ (NSColor*) controlColor
{
  return systemColorWithName(@"controlColor");
}

+ (NSColor*) controlHighlightColor
{
  return systemColorWithName(@"controlHighlightColor");
}

+ (NSColor*) controlLightHighlightColor
{
  return systemColorWithName(@"controlLightHighlightColor");
}

+ (NSColor*) controlShadowColor
{
  return systemColorWithName(@"controlShadowColor");
}

+ (NSColor*) controlDarkShadowColor
{
  return systemColorWithName(@"controlDarkShadowColor");
}

+ (NSColor*) controlTextColor
{
  return systemColorWithName(@"controlTextColor");
}

+ (NSColor*) disabledControlTextColor
{
  return systemColorWithName(@"disabledControlTextColor");
}

+ (NSColor*) gridColor
{
  return systemColorWithName(@"gridColor");
}

+ (NSColor*) headerColor
{
  return systemColorWithName(@"headerColor");
}

+ (NSColor*) headerTextColor
{
  return systemColorWithName(@"headerTextColor");
}

+ (NSColor*) highlightColor
{
  return systemColorWithName(@"highlightColor");
}

+ (NSColor*) keyboardFocusIndicatorColor
{
  return systemColorWithName(@"keyboardFocusIndicatorColor");
}

+ (NSColor*) knobColor
{
  return systemColorWithName(@"knobColor");
}

+ (NSColor*) scrollBarColor
{
  return systemColorWithName(@"scrollBarColor");
}

+ (NSColor*) secondarySelectedControlColor
{
  return systemColorWithName(@"secondarySelectedControlColor");
}

+ (NSColor*) selectedControlColor
{
  return systemColorWithName(@"selectedControlColor");
}

+ (NSColor*) selectedControlTextColor
{
  return systemColorWithName(@"selectedControlTextColor");
}

+ (NSColor*) selectedKnobColor
{
  return systemColorWithName(@"selectedKnobColor");
}

+ (NSColor*) selectedMenuItemColor
{
  return systemColorWithName(@"selectedMenuItemColor");
}

+ (NSColor*) selectedMenuItemTextColor
{
  return systemColorWithName(@"selectedMenuItemTextColor");
}

+ (NSColor*) selectedTextBackgroundColor
{
  return systemColorWithName(@"selectedTextBackgroundColor");
}

+ (NSColor*) selectedTextColor
{
  return systemColorWithName(@"selectedTextColor");
}

+ (NSColor*) shadowColor
{
  return systemColorWithName(@"shadowColor");
}

+ (NSColor*) textBackgroundColor
{
  return systemColorWithName(@"textBackgroundColor");
}

+ (NSColor*) textColor
{
  return systemColorWithName(@"textColor");
}

+ (NSColor *)windowBackgroundColor
{
  return systemColorWithName(@"windowBackgroundColor");
}

+ (NSColor*) windowFrameColor
{
  return systemColorWithName(@"windowFrameColor");
}

+ (NSColor*) windowFrameTextColor
{
  return systemColorWithName(@"windowFrameTextColor");
}

+ (NSArray*) controlAlternatingRowBackgroundColors
{
  return [NSArray arrayWithObjects: systemColorWithName(@"rowBackgroundColor"), 
		  systemColorWithName(@"alternateRowBackgroundColor"), nil];
}

////////////////////////////////////////////////////////////
//
// Instance methods
//

- (id) copyWithZone: (NSZone*)aZone
{
  if (NSShouldRetainWithZone(self, aZone))
    {
      return RETAIN(self);
    }
  else
    {
      return NSCopyObject(self, 0, aZone);
    }
}

- (NSString*) description
{
  [self subclassResponsibility: _cmd];
  return nil;
}

/**<p>Gets the cyan, magenta, yellow,black and alpha values from the NSColor.
 Raises a NSInternalInconsistencyException if the NSColor is not a CYMK color
 </p>
 */
- (void) getCyan: (CGFloat*)cyan
	 magenta: (CGFloat*)magenta
	  yellow: (CGFloat*)yellow
	   black: (CGFloat*)black
	   alpha: (CGFloat*)alpha
{
  [NSException raise: NSInternalInconsistencyException
    format: @"Called getCyan:magenta:yellow:black:alpha: on non-CMYK colour"];
}

/**<p>Gets the hue, saturation, brightness and alpha values from the NSColor.
 Raises a NSInternalInconsistencyException if the NSColor is not a RGB color
 </p>
 */
- (void) getHue: (CGFloat*)hue
     saturation: (CGFloat*)saturation
     brightness: (CGFloat*)brightness
	  alpha: (CGFloat*)alpha
{
  [NSException raise: NSInternalInconsistencyException
    format: @"Called getHue:saturation:brightness:alpha: on non-RGB colour"];
}

/**<p>Gets the red, green, blue and alpha values from the NSColor.
 Raises a NSInternalInconsistencyException if the NSColor is not a RGB color
 </p>
 */
-(void) getRed: (CGFloat*)red
	  green: (CGFloat*)green
	   blue: (CGFloat*)blue
	  alpha: (CGFloat*)alpha
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called getRed:green:blue:alpha: on non-RGB colour"];
}

/**<p>Gets the white alpha values from the NSColor.
 Raises a NSInternalInconsistencyException if the NSColor is not a 
 greyscale color</p>
 */
- (void) getWhite: (CGFloat*)white
	    alpha: (CGFloat*)alpha
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called getWhite:alpha: on non-grayscale colour"];
}

- (BOOL) isEqual: (id)other
{
  if (other == self)
    return YES;
  if ([other isKindOfClass: NSColorClass] == NO)
    return NO;
  else
    {
      [self subclassResponsibility: _cmd];
      return NO;
    }
}

/** <p>Returns the alpha component. </p>
 */
- (CGFloat) alphaComponent
{
  return 1.0;
}

/** <p>Returns the black component. Raises a NSInternalInconsistencyException
    if NSColor is not a CMYK color.</p>
 */
- (CGFloat) blackComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called blackComponent on non-CMYK colour"];
  return 0.0;
}

/** <p>Returns the blue component. Raises a NSInternalInconsistencyException
    if NSColor is not a RGB color.</p>
 */
- (CGFloat) blueComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called blueComponent on non-RGB colour"];
  return 0.0;
}

/** <p>Returns the brightness component. Raises a 
    NSInternalInconsistencyException if NSColor space is not a RGB color</p>
*/
- (CGFloat) brightnessComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called brightnessComponent on non-RGB colour"];
  return 0.0;
}

- (NSString *) catalogNameComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called catalogNameComponent on colour with name"];
  return nil;
}

- (NSString *) colorNameComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called colorNameComponent on colour with name"];
  return nil;
}

/** <p>Returns the cyan component. Raises a  NSInternalInconsistencyException 
    if NSColor is not a CYMK color</p>
*/
- (CGFloat) cyanComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called cyanComponent on non-CMYK colour"];
  return 0.0;
}

/** <p>Returns the green component. Raises a  NSInternalInconsistencyException 
    if NSColor is not a RGB color</p>
*/
- (CGFloat) greenComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called greenComponent on non-RGB colour"];
  return 0.0;
}

/** <p>Returns the hue component. Raises a  NSInternalInconsistencyException 
    if NSColor is not a RGB color</p>
*/
- (CGFloat) hueComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called hueComponent on non-RGB colour"];
  return 0.0;
}

- (NSString *) localizedCatalogNameComponent
{
  [NSException raise: NSInternalInconsistencyException
    format: @"Called localizedCatalogNameComponent on colour with name"];
  return nil;
}

- (NSString *) localizedColorNameComponent
{
  [NSException raise: NSInternalInconsistencyException
    format: @"Called localizedColorNameComponent on colour with name"];
  return nil;
}

/** <p>Returns the magenta component. Raises a  
    NSInternalInconsistencyException  if NSColor is not a CMYK color</p>
*/
- (CGFloat) magentaComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called magentaComponent on non-CMYK colour"];
  return 0.0;
}

/** <p>Returns the red component. Raises a NSInternalInconsistencyException  
    if NSColor is not a RGB color</p>
*/
- (CGFloat) redComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called redComponent on non-RGB colour"];
  return 0.0;
}

/** <p>Returns the saturation component. Raises a
    NSInternalInconsistencyException if NSColor is not a RGB color</p>
*/
- (CGFloat) saturationComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called saturationComponent on non-RGB colour"];
  return 0.0;
}

/** <p>Returns the white component. Raises a NSInternalInconsistencyException  
    if NSColor is not a grayscale color</p>
*/
- (CGFloat) whiteComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called whiteComponent on non-grayscale colour"];
  return 0.0;
}

- (NSImage*) patternImage
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called patternImage on non-pattern colour"];
  return nil;
}

/** <p>Returns the yellow component. Raises a NSInternalInconsistencyException
    if NSColor is not a RGB color</p>
*/
- (CGFloat) yellowComponent
{
  [NSException raise: NSInternalInconsistencyException
	      format: @"Called yellowComponent on non-CMYK colour"];
  return 0.0;
}

//
// Converting to Another Color Space
//
- (NSString *) colorSpaceName
{
  [self subclassResponsibility: _cmd];
  return nil;
}

- (NSColor*) colorUsingColorSpaceName: (NSString *)colorSpace
{
  return [self colorUsingColorSpaceName: colorSpace
				 device: nil];
}

- (NSColor*) colorUsingColorSpaceName: (NSString *)colorSpace
			       device: (NSDictionary *)deviceDescription
{
  if (colorSpace == nil)
    {
      if (deviceDescription != nil)
	colorSpace = [deviceDescription objectForKey: NSDeviceColorSpaceName];
      if (colorSpace == nil)
        colorSpace = NSDeviceRGBColorSpace;
    }
  if ([colorSpace isEqualToString: [self colorSpaceName]])
    {
      return self;
    }

  if ([colorSpace isEqualToString: NSNamedColorSpace])
    {
      // FIXME: We cannot convert to named color space.
      return nil;
    }

  [self subclassResponsibility: _cmd];

  return nil;
}

+ (NSColor *) colorWithColorSpace: (NSColorSpace *)space
                       components: (const CGFloat *)comp
                            count: (NSInteger)number
{
  // FIXME
  if (space == [NSColorSpace genericRGBColorSpace] && (number == 4)) 
    {
      return [self colorWithCalibratedRed: comp[0]
                   green: comp[1]
                   blue: comp[2]
                   alpha: comp[3]];
    }
  if (space == [NSColorSpace deviceRGBColorSpace] && (number == 4)) 
    {
      return [self colorWithDeviceRed: comp[0]
                   green: comp[1]
                   blue: comp[2]
                   alpha: comp[3]];
    }
  if (space == [NSColorSpace genericGrayColorSpace] && (number == 2)) 
    {
      return [NSColor colorWithCalibratedWhite: comp[0] alpha: comp[1]];
    }
  if (space == [NSColorSpace deviceGrayColorSpace] && (number == 2)) 
    {
      return [NSColor colorWithDeviceWhite: comp[0] alpha: comp[1]];
    }
  if (space == [NSColorSpace genericCMYKColorSpace] && (number == 5)) 
    {
      return [NSColor colorWithDeviceCyan: comp[0] 
                      magenta: comp[1]
                      yellow: comp[2] 
                      black: comp[3] 
                      alpha: comp[4]];
    }
  if (space == [NSColorSpace deviceCMYKColorSpace] && (number == 5)) 
    {
      return [NSColor colorWithDeviceCyan: comp[0] 
                      magenta: comp[1]
                      yellow: comp[2] 
                      black: comp[3] 
                      alpha: comp[4]];
    }

  return nil;
}
#endif //TEMP2

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
