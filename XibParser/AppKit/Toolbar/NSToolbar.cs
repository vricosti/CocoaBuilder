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
using System.Xml.Linq;

namespace Smartmobili.Cocoa
{
    public enum NSToolbarDisplayMode
    { 
        NSToolbarDisplayModeDefault,
        NSToolbarDisplayModeIconAndLabel,
        NSToolbarDisplayModeIconOnly,
        NSToolbarDisplayModeLabelOnly
    }

    public enum NSToolbarSizeMode
    {
        NSToolbarSizeModeDefault,
        NSToolbarSizeModeRegular,
        NSToolbarSizeModeSmall
    }

    public class NSToolbar : NSObject
    {
        new public static Class Class = new Class(typeof(NSToolbar));

        public NSString Identifier { get; set; }

        public string Delegate { get; set; }

        public bool PrefersToBeShown { get; set; }

        public bool ShowsBaselineSeparator { get; set; }

        public bool AllowsUserCustomization { get; set; }

        public bool AutosavesConfiguration { get; set; }

        public NSToolbarDisplayMode DisplayMode { get; set; }

        public NSToolbarSizeMode SizeMode { get; set; }

        public NSMutableDictionary IBIdentifiedItems { get; set; }

        public NSArray IBAllowedItems { get; set; }

        public NSArray IBDefaultItems { get; set; }

        public NSArray IBSelectableItems { get; set; }
        

        //public static NSToolbar Create(XElement xElement)
        //{
        //    NSToolbar nsToolBar = new NSToolbar();

        //    var elements = xElement.elements();
        //    foreach (var elm in elements)
        //    {
        //        nsToolBar.Decode(elm);
        //    }

        //    return nsToolBar;
        //}

        public NSToolbar()
        {

        }


        //public NSToolbar(NSObjectDecoder aDecoder)
        //    : base(aDecoder)
        //{
           
        //}

        public override id initWithCoder(NSCoder aDecoder)
        {
            base.initWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                Identifier = (NSString)aDecoder.decodeObjectForKey("NSToolbarIdentifier");
                Delegate = (NSString)aDecoder.decodeObjectForKey("NSToolbarDelegate");
                PrefersToBeShown = aDecoder.decodeBoolForKey("NSToolbarPrefersToBeShown");
                ShowsBaselineSeparator = aDecoder.decodeBoolForKey("NSToolbarShowsBaselineSeparator");
                AllowsUserCustomization = aDecoder.decodeBoolForKey("NSToolbarAllowsUserCustomization");
                AutosavesConfiguration = aDecoder.decodeBoolForKey("NSToolbarAutosavesConfiguration");
                SizeMode = (NSToolbarSizeMode)aDecoder.decodeIntForKey("NSToolbarSizeMode");
                IBIdentifiedItems = (NSMutableDictionary)aDecoder.decodeObjectForKey("NSToolbarIBIdentifiedItems");
                IBAllowedItems = (NSArray)aDecoder.decodeObjectForKey("NSToolbarIBAllowedItems");
                IBDefaultItems = (NSArray)aDecoder.decodeObjectForKey("NSToolbarIBDefaultItems");
                IBSelectableItems = (NSArray)aDecoder.decodeObjectForKey("NSToolbarIBSelectableItems");
            }

            //foreach (var xElement in decoder.XmlElement.elements())
            //{
            //    string key = xElement.Attribute("key").Value;
            //    switch (key)
            //    {
            //        case "NSToolbarIdentifier": { Identifier = (NSString)decoder.Create(xElement); break; }
            //        case "NSToolbarDelegate": { Delegate = (NSString)decoder.Create(xElement); break; }
            //        case "NSToolbarPrefersToBeShown": { PrefersToBeShown = (NSNumber)decoder.Create(xElement); break; }
            //        case "NSToolbarShowsBaselineSeparator": { ShowsBaselineSeparator = (NSNumber)decoder.Create(xElement); break; }
            //        case "NSToolbarAllowsUserCustomization": { AllowsUserCustomization = (NSNumber)decoder.Create(xElement); break; }
            //        case "NSToolbarAutosavesConfiguration": { AutosavesConfiguration = (NSNumber)decoder.Create(xElement); break; }
            //        case "NSToolbarSizeMode": { SizeMode = (NSToolbarSizeMode)(int)(NSNumber)decoder.Create(xElement); break; }
            //        case "NSToolbarIBIdentifiedItems": { IBIdentifiedItems = (NSMutableDictionary)decoder.Create(xElement); break; }
            //        case "NSToolbarIBAllowedItems": { IBAllowedItems = (NSArray)decoder.Create(xElement); break; }
            //        case "NSToolbarIBDefaultItems": { IBDefaultItems = (NSArray)decoder.Create(xElement); break; }
            //        case "NSToolbarIBSelectableItems": { IBSelectableItems = (NSArray)decoder.Create(xElement); break; }
            //    }
            //}

            return this;
        }


        //public static NSToolbar Create(NSObjectDecoder aDecoder)
        //{
        //    NSToolbar nsToolBar = new NSToolbar();

        //    var xElement = aDecoder.XmlElement;
        //    var elements = xElement.elements();
        //    foreach (var elm in elements)
        //    {
        //        aDecoder.XmlElement = elm;
        //        nsToolBar.Decode(aDecoder);
        //    }

        //    return nsToolBar;
        //}

        //protected void Decode(NSObjectDecoder aDecoder)
        //{
        //    var xElement = aDecoder.XmlElement;
        //    string key = xElement.Attribute("key").Value;
        //    switch (key)
        //    {
        //        case "NSToolbarIdentifier": { Identifier = (NSMutableString)aDecoder.Create(xElement); break; }
        //        case "NSToolbarDelegate": { Delegate = (NSString)aDecoder.Create(xElement); break; }
        //        case "NSToolbarPrefersToBeShown": { PrefersToBeShown = (NSNumber)aDecoder.Create(xElement); break; }
        //        case "NSToolbarShowsBaselineSeparator": { ShowsBaselineSeparator = (NSNumber)aDecoder.Create(xElement); break; }
        //        case "NSToolbarAllowsUserCustomization": { AllowsUserCustomization = (NSNumber)aDecoder.Create(xElement); break; }
        //        case "NSToolbarAutosavesConfiguration": { AutosavesConfiguration = (NSNumber)aDecoder.Create(xElement); break; }
        //        case "NSToolbarSizeMode": { SizeMode = (NSToolbarSizeMode)(int)(NSNumber)aDecoder.Create(xElement); break; }
        //        case "NSToolbarIBIdentifiedItems": { IBIdentifiedItems = (NSMutableDictionary)aDecoder.Create(xElement); break; }
        //        case "NSToolbarIBAllowedItems": { IBAllowedItems = (NSArray)aDecoder.Create(xElement); break; }
        //        case "NSToolbarIBDefaultItems": { IBDefaultItems = (NSArray)aDecoder.Create(xElement); break; }
        //        case "NSToolbarIBSelectableItems": { IBSelectableItems = (NSArray)aDecoder.Create(xElement); break; }
        //    }

        //    //return this;
        //}
    }
}
