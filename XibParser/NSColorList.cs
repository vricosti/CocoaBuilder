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
        static NSLock _colorListLock = new NSLock();

        static NSColorList defaultSystemColorList = null;
        static NSColorList themeColorList = null;


        public static NSArray AvailableColorLists
        {
            get
            {
                NSArray a = null;

                //FIXME
                //NSColor.WhiteColor
                NSColorList._LoadAvailableColorLists(null);

                return a;
            }
        }


        public static NSColorList ColorListNamed(NSString name)
        {
            NSColorList r;
            NSEnumerator e;

            // Serialize access to color list
            _colorListLock.Lock();

            NSColorList._LoadAvailableColorLists(null);
            e = _availableColorLists.ObjectEnumerator();

            while ((r = (NSColorList)e.NextObject()) != null)
            {
                if (r.Name.IsEqualToString(name))
                {
                    break;
                }
            }

            _colorListLock.Unlock();

            return r;
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
            NSCharacterSet newlineSet = NSCharacterSet.CharacterSetWithCharactersInString(@"\n");
            NSScanner scanner = (NSScanner)NSScanner.ScannerWithString(NSString.StringWithContentsOfFile(_fullFileName));

            if (scanner.ScanInt(ref nColors) == false)
            {
                //NSLog(@"Unable to read color file at \"%@\" -- unknown format.", _fullFileName);
                return false;
            }

            for (i = 0; i < nColors; i++)
            {
                if (scanner.ScanInt(ref method) == false)
                {
                    //NSLog(@"Unable to read color file at \"%@\" -- unknown format.", _fullFileName);
                    break;
                }
                //FIXME- replace this by switch on method to different
                //       NSColor initializers
                if (method != 0)
                {
                    //NSLog(@"Unable to read color file at \"%@\" -- only RGBA form " @"supported.", _fullFileName);
                    break;
                }
                st = scanner.ScanFloat(ref r);
                st = st && scanner.ScanFloat(ref g);
                st = st && scanner.ScanFloat(ref b);
                st = st && scanner.ScanFloat(ref alpha);
                //st = st && [scanner scanUpToCharactersFromSet: newlineSet intoString: &cname];
                if (st == false)
                {
                    //NSLog(@"Unable to read color file at \"%@\" -- unknown format.", _fullFileName);
                    break;
                }
                //color = [NSColor colorWithCalibratedRed: r green: g blue: b alpha: alpha];
                //[self insertColor: color key: cname atIndex: i];
            }

            return (i == nColors);
        }

        private static NSArray NSSearchPathForDirectoriesInDomains(
            NSSearchPathDirectory directory, 
            NSSearchPathDomainMask domainMask, 
            bool expandTilde)
        {
            return null;
        }


        public virtual id InitWithName(NSString aName)
        {
            return InitWithName(aName, null);
        }

        public virtual id InitWithName(NSString aName, NSString aPath)
        {
            id self = this;

            return self;
        }


        public virtual NSString Name
        {
            get { return _name; }
        }

        private static void _LoadAvailableColorLists(NSNotification aNotification)
        {
            _colorListLock.Lock();

            /* FIXME ... we should ensure that we get housekeeping notifications */
            if (_availableColorLists != null && aNotification == null)
            {
                // Nothing to do ... already loaded
                _colorListLock.Unlock();
            }
            else
            {
                NSString dir;
                NSString file;
                NSEnumerator e;
                NSFileManager fm = NSFileManager.DefaultManager;
                NSDirectoryEnumerator de;
                NSColorList newList;

                if (_availableColorLists == null)
                {
                    // Create the global array of color lists
                    _availableColorLists = (NSMutableArray)NSMutableArray.Alloc().Init();
                }
                else
                {
                    _availableColorLists.RemoveAllObjects(); ;
                }

                /*
                 * Keep any pre-loaded system color list.
                 */
                if (themeColorList != null)
                {
                    _availableColorLists.AddObject(themeColorList);
                }

                /*
                 * Load color lists found in standard paths into the array
                 * FIXME: Check exactly where in the directory tree we should scan.
                 */
                e = NSSearchPathForDirectoriesInDomains(NSSearchPathDirectory.NSLibraryDirectory, NSSearchPathDomainMask.NSAllDomainsMask, true).ObjectEnumerator();

                while ((dir = (NSString)e.NextObject()) != null)
                {
                    bool flag = false;

                    dir = dir.StringByAppendingPathComponent(@"Colors");
                    if (!fm.FileExistsAtPath(dir, ref flag) || !flag)
                    {
                        // Only process existing directories
                        continue;
                    }

                    de = fm.EnumeratorAtPath(dir);
                    while ((file = (NSString)de.NextObject()) != null)
                    {
                        if (file.PathExtension().IsEqualToString(@"clr"))
                        {
                            NSString name;

                            name = file.StringByDeletingPathExtension();
                            newList = (NSColorList)NSColorList.Alloc().InitWithName(name, dir.StringByAppendingPathComponent(file));
                            _availableColorLists.AddObject(newList);
                        }
                    }
                }

                if (defaultSystemColorList != null)
                {
                    _availableColorLists.AddObject(defaultSystemColorList);
                }
                _colorListLock.Unlock();
            }
        }
    }
}
