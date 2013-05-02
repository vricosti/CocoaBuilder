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
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSColorList.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSColorList.m
    public class NSColorList : NSObject
    {
        new public static Class Class = new Class(typeof(NSColorList));
        new public static NSColorList Alloc() { return new NSColorList(); }

        protected NSString _name;
        protected NSString _fullFileName;
        protected bool _is_editable;

        // Color Lists are required to be a sort of ordered dictionary
        // For now it is implemented as follows (Scott Christley, 1996):

        // This object contains couples (keys (=color names), values (=colors))
        protected NSMutableDictionary _colorDictionary;

        // This object contains the keys (=color names) in order
        protected NSMutableArray _orderedColorKeys;

        static NSMutableArray _availableColorLists = null;
        //static NSLock _colorListLock = null;

        static NSColorList defaultSystemColorList = null;
        static NSColorList themeColorList = null;

        public static NSArray AvailableColorLists
        {
            get
            {
                NSArray a = null;

                //FIXME
                //NSColor.WhiteColor
                //NSColorList._LoadAvailableColorLists(null);

                return a;
            }
        }


        public virtual bool _ReadTextColorFile(NSString filepath)
        {
            int nColors = 0;
            int method = 0;
            float r = 0;
            float g = 0;
            float b = 0;
            float alpha = 0;
            NSString cname = null;
            int i = 0;
            bool st = false;
            NSColor color = null;
            //NSCharacterSet newlineSet = [NSCharacterSet characterSetWithCharactersInString: @"\n"];
            NSScanner scanner = (NSScanner)NSScanner.ScannerWithString(NSString.StringWithContentsOfFile(_fullFileName));

           if (scanner.ScanInt(ref nColors) == false)
           {
               //NSLog(@"Unable to read color file at \"%@\" -- unknown format.", _fullFileName);
               return false;
           }
            

            return (i == nColors);
        }


    }
}
