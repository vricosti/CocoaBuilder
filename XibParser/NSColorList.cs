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
        static NSRecursiveLock _colorListLock = null;

        static NSColorList DefaultSystemColorList = null;
        static NSColorList ThemeColorList = null;


        static NSColorList() { Initialize(); }
        static void Initialize()
        {
            _colorListLock = (NSRecursiveLock)NSRecursiveLock.Alloc().Init();
        }


        public static NSArray AvailableColorLists
        {
            get
            {
                NSArray a = null;

                //FIXME
				NSColor tmp = NSColor.WhiteColor;
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
                st = st && scanner.ScanUpToCharactersFromSet(newlineSet, ref cname);
                if (st == false)
                {
                    //NSLog(@"Unable to read color file at \"%@\" -- unknown format.", _fullFileName);
                    break;
                }

                color = NSColor.ColorWithCalibratedRed(r, g, b, alpha);
                InsertColor(color, cname, (uint)i);
            }

            return (i == nColors);
        }




        public virtual id InitWithName(NSString aName)
        {
            return InitWithName(aName, null);
        }

        public virtual id InitWithName(NSString name, NSString path)
        {
            id self = this;
            NSColorList cl = null;
            bool could_load = false;

            _name = name;
            if (path != null)
            {
                bool isDir = false;
                // previously impl wrongly expected directory containing color file
                // rather than color file; we support this for apps that rely on it
                if ((NSFileManager.DefaultManager.FileExistsAtPath(path, ref isDir) == false) || isDir == true)
                {
                    //NSLog(@"NSColorList -initWithName:fromFile: warning: excluding " @"filename from path (%@) is deprecated.", path);
                    _fullFileName = path.StringByAppendingPathComponent(name).StringByAppendingPathExtension(@"clr");
                }
                else
                {
                    _fullFileName = path;
                }

                // Unarchive the color list

                // TODO [Optm]: Rewrite to initialize directly without unarchiving 
                // in another object
                try
                {
                    //FIXME
                    //cl = (NSColorList) NSUnarchiver.UnarchiveObjectWithFile(_fullFileName);
                }
                catch (Exception ex)
                {
                    cl = null;
                }


                if ((cl != null) && (cl.IsKindOfClass(NSColorList.Class)))
                {
                    could_load = true;

                    _is_editable = NSFileManager.DefaultManager.IsWritableFileAtPath(_fullFileName);

                    _colorDictionary = NSMutableDictionary.DictionaryWithDictionary(cl._colorDictionary);

                    _orderedColorKeys = NSMutableArray.ArrayWithArray(cl._orderedColorKeys);
                }
                else if (NSFileManager.DefaultManager.FileExistsAtPath(path))
                {
                    _colorDictionary = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
                    _orderedColorKeys = (NSMutableArray)NSMutableArray.Alloc().Init();
                    _is_editable = true;

                    if (_ReadTextColorFile(_fullFileName))
                    {
                        could_load = true;
                        _is_editable = NSFileManager.DefaultManager.IsWritableFileAtPath(_fullFileName);
                    }
                    else
                    {
                        _colorDictionary = null;
                        _orderedColorKeys = null;
                    }
                }
            }

            if (could_load == false)
            {
                _fullFileName = null;
                _colorDictionary = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
                _orderedColorKeys = (NSMutableArray)NSMutableArray.Alloc().Init();
                _is_editable = true;
            }

            return self;
        }


        public virtual NSString Name
        {
            get { return _name; }
        }


        public virtual NSArray AllKeys
        {
            get { return NSArray.ArrayWithArray(_orderedColorKeys); }
        }

        public virtual NSColor ColorWithKey(NSString key)
        {
            return (NSColor)_colorDictionary.ObjectForKey(key);
        }

        public virtual void InsertColor(NSColor color, NSString key, uint location)
        {
            NSNotification n = null;

            if (_is_editable == false)
                throw new Exception(@"Color list cannot be edited");

            _colorDictionary.SetObjectForKey(color, key);
            _orderedColorKeys.RemoveObject(key);
            _orderedColorKeys.InsertObject(key, location);


            // We don't support notifs for now ...
            //n = NSNotification.NotificationWithName(@"NSColorListDidChangeNotification", this, null);
            //NSNotificationQueue.DefaultQueue.EnqueueNotification(n, NSPostASAP, NSNotificationCoalescingOnSender, null);
        }

        public virtual void RemoveColorWithKey(NSString key)
        {
            NSNotification n = null;

            if (_is_editable == false)
                throw new Exception(@"Color list cannot be edited");

            _colorDictionary.RemoveObjectForKey(key);
            _orderedColorKeys.RemoveObject(key);

            // We don't support notifs for now ...
            //n = NSNotification.NotificationWithName(@"NSColorListDidChangeNotification", this, null);
            //NSNotificationQueue.DefaultQueue.EnqueueNotification(n, NSPostASAP, NSNotificationCoalescingOnSender, null);
        }

        public virtual void SetColor(NSColor aColor, NSString key)
        {
            NSNotification n = null;

            if (_is_editable == false)
                throw new Exception(@"Color list cannot be edited");
            _colorDictionary.SetObjectForKey(aColor, key);

            if (_orderedColorKeys.ContainsObject(key) == false)
                _orderedColorKeys.AddObject(key);

            // We don't support notifs for now ...
            //n = NSNotification.NotificationWithName(@"NSColorListDidChangeNotification", this, null);
            //NSNotificationQueue.DefaultQueue.EnqueueNotification(n, NSPostASAP, NSNotificationCoalescingOnSender, null);
        }


        public virtual bool IsEditable
        {
            get { return _is_editable; }
        }

        public virtual bool WriteToFile(NSString path)
        {
            NSFileManager fm = NSFileManager.DefaultManager;
            NSString tmpPath = @"";
            bool isDir = false;
            bool success = false;
            bool path_is_standard = true;

            /*
             * We need to initialize before saving, to avoid the new file being 
             * counted as a different list thus making us appear twice
             */
            NSColorList._LoadAvailableColorLists(null);

            if (path == null)
            {
                NSArray paths;

                // FIXME the standard path for saving color lists?
                paths = NSSearchPathForDirectoriesInDomains(
                    NSSearchPathDirectory.NSLibraryDirectory,
                    NSSearchPathDomainMask.NSUserDomainMask, true);

                if (paths.Count == 0)
                {
                    //NSLog (@"Failed to find Library directory for user");
                    return false;	// No directory to save to.
                }
                path = ((NSString)paths.ObjectAtIndex(0)).StringByAppendingPathComponent(@"Colors");
                isDir = true;
            }
            else
            {
                fm.FileExistsAtPath(path, ref isDir);
            }

            if (isDir)
            {
                _fullFileName = path.StringByAppendingPathComponent(_name).StringByAppendingPathExtension(@"clr");
            }
            else // it is a file
            {
                if (path.PathExtension().IsEqualToString(@"clr") == true)
                {
                    _fullFileName = path;
                }
                else
                {
                    _fullFileName = path.StringByDeletingPathExtension().StringByAppendingPathExtension(@"clr");
                }
                path = path.StringByDeletingLastPathComponent();
            }

            // Check if the path is a standard path
            if (path.LastPathComponent().IsEqualToString(@"Colors") == false)
            {
                path_is_standard = false;
            }
            else
            {
                tmpPath = path.StringByDeletingLastPathComponent();
                if (!NSSearchPathForDirectoriesInDomains(NSSearchPathDirectory.NSLibraryDirectory, NSSearchPathDomainMask.NSAllDomainsMask, true).ContainsObject(tmpPath))
                {
                    path_is_standard = false;
                }
            }

            /*
             * If path is standard and it does not exist, try to create it.
             * System standard paths should always be assumed to exist; 
             * this will normally then only try to create user paths.
             */
            if (path_is_standard && (fm.FileExistsAtPath(path) == false))
            {
                NSError err = null;
                if (fm.CreateDirectoryAtPath(path, true, null, ref err))
                {
                    //NSLog (@"Created standard directory %@", path);
                }
                else
                {
                    //NSLog (@"Failed attempt to create directory %@", path);
                }
            }

            //success = [NSArchiver archiveRootObject: self  toFile: _fullFileName];

            if (success && path_is_standard)
            {
                _colorListLock.Lock();
                if (_availableColorLists.ContainsObject(this) == false)
                    _availableColorLists.AddObject(this);


                _colorListLock.Unlock();
                return true;
            }

            return success;
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
                if (ThemeColorList != null)
                {
                    _availableColorLists.AddObject(ThemeColorList);
                }

                /*
                 * Load color lists found in standard paths into the array
                 * FIXME: Check exactly where in the directory tree we should scan.
                 */
                e = NSSearchPathForDirectoriesInDomains(
                    NSSearchPathDirectory.NSLibraryDirectory,
                    NSSearchPathDomainMask.NSAllDomainsMask, true).ObjectEnumerator();

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

                if (DefaultSystemColorList != null)
                {
                    _availableColorLists.AddObject(DefaultSystemColorList);
                }
                _colorListLock.Unlock();
            }
        }

        public static void _SetDefaultSystemColorList(NSColorList aList)
        {
            _colorListLock.Lock();
            if (DefaultSystemColorList != aList)
            {
                if (DefaultSystemColorList != null
                  && _availableColorLists.LastObject() == DefaultSystemColorList)
                {
                    _availableColorLists.RemoveLastObject();
                }
                DefaultSystemColorList = aList;
                _availableColorLists.AddObject(aList);
            }
            _colorListLock.Unlock();
        }

        public static void _SetThemeSystemColorList(NSColorList aList)
        {
            _colorListLock.Lock();
            if (ThemeColorList != aList)
            {
                if (ThemeColorList != null && _availableColorLists.Count > 0
                  && _availableColorLists.ObjectAtIndex(0) == ThemeColorList)
                {
                    _availableColorLists.RemoveObjectAtIndex(0);
                }
                ThemeColorList = aList;
                _availableColorLists.InsertObject(aList, 0);
            }
            _colorListLock.Unlock();
        }



        private static NSArray NSSearchPathForDirectoriesInDomains(
            NSSearchPathDirectory directory,
            NSSearchPathDomainMask domainMask,
            bool expandTilde)
        {
            return (NSArray)NSArray.Alloc().Init();
        }
    }
}
