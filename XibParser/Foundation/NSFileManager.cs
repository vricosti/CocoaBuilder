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
    public class NSDirectoryEnumerator : NSEnumerator
    {
        new public static Class Class = new Class(typeof(NSDirectoryEnumerator));
        new public static NSDirectoryEnumerator Alloc() { return new NSDirectoryEnumerator(); }

        public virtual id InitWithPath(NSString aPath)
        {
            id self = this;

            return self;
        }

        public override NSArray AllObjects()
        {
            throw new NotImplementedException();            
            return null;
        }

        public override id NextObject()
        {
            throw new NotImplementedException();

            return null;
        }

    }

    public class NSFileManager : NSObject
    {
        new public static Class Class = new Class(typeof(NSFileManager));
        new public static NSFileManager Alloc() { return new NSFileManager(); }

        private static NSFileManager _defaultFileMgr;


        public static NSFileManager DefaultManager
        {
            get
            {
                if (_defaultFileMgr == null)
                {
                    _defaultFileMgr = (NSFileManager)Alloc().Init();
                }

                return _defaultFileMgr;
            }
        }


        public virtual bool FileExistsAtPath(NSString aPath, ref bool isDirectory)
        {
            bool exists = false;

            if (System.IO.File.Exists(aPath))
            {
                exists = true;
                isDirectory = false;
            }
            else if (System.IO.Directory.Exists(aPath))
            {
                exists = true;
                isDirectory = true;
            }
            
            return exists;
        }


        public virtual NSDirectoryEnumerator EnumeratorAtPath(NSString aPath)
        {
            return (NSDirectoryEnumerator)NSDirectoryEnumerator.Alloc().InitWithPath(aPath);
        }

    }
}
