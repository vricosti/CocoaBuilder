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
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSFileWrapper.m

    public enum GSFileWrapperType
    {
        GSFileWrapperDirectoryType,
        GSFileWrapperRegularFileType,
        GSFileWrapperSymbolicLinkType
    }

    public enum NSFileWrapperReadingOptions
    {
        NSFileWrapperReadingDefault,
        NSFileWrapperReadingImmediate = 1 << 0,
        NSFileWrapperReadingWithoutMapping = 1 << 1,
    }

    public class NSFileWrapper : NSObject
    {
        new public static Class Class = new Class(typeof(NSMutableArray));

        NSString _filename;
        NSString _preferredFilename;
        NSMutableDictionary _fileAttributes;
        GSFileWrapperType _wrapperType;
        id _wrapperData;
        //NSImage	_iconImage;



        public NSFileWrapper()
        {}

        new public static NSFileWrapper Alloc()
        {
            return new NSFileWrapper();
        }


        public virtual id InitDirectoryWithFileWrappers(NSDictionary docs)
        {
            NSFileWrapper self = this;

            if (base.Init() != null)
            {
                NSEnumerator enumerator;
                id key;
                NSFileWrapper wrapper;

                _wrapperType = GSFileWrapperType.GSFileWrapperDirectoryType;
                _wrapperData = NSMutableDictionary.Alloc().InitWithCapacity((uint)docs.Count);

                enumerator = docs.KeyEnumerator();
                while ((key = enumerator.NextObject()) != null)
                {
                    wrapper = (NSFileWrapper)docs.ObjectForKey(key);

                    if (wrapper.PreferredFilename() == null)
                    {
                        wrapper.SetPreferredFilename((NSString)key);
                    }
                    ((NSMutableDictionary)_wrapperData).SetObjectForKey(wrapper, key);
                }
            }
            return self;
        }

        // Init instance of regular file type

        public virtual id InitRegularFileWithContents(NSData data)
        {
            if (base.Init() != null)
            {
                _wrapperData = data;
                _wrapperType = GSFileWrapperType.GSFileWrapperRegularFileType;
            }
            return this;
        }

        // Init instance of symbolic link type

        public virtual id InitSymbolicLinkWithDestination(NSString path)
        {
            if (base.Init() != null)
            {
                _wrapperData = path;
                _wrapperType = GSFileWrapperType.GSFileWrapperSymbolicLinkType;
            }
            return this;
        }

        /**
         * Init an instance from the file, directory, or symbolic link at path.<br /> 
         * This can create a tree of instances with a directory instance at the top
         */

        public virtual id InitWithPath(NSString path)
        {

            NSFileManager fm = NSFileManager.DefaultManager;
            NSString fileType;

            //NS.DebugLLog(@"NSFileWrapper", @"initWithPath: %@", path);

            // Store the full path in filename, the specification is unclear in this point
            SetFilename(path);
            SetPreferredFilename(path.LastPathComponent());
            SetFileAttributes(fm.FileAttributesAtPath(path, false));

            fileType = FileAttributes().FileType;
            if (fileType.IsEqualToString("NSFileTypeDirectory"))
            {
                NSString filename;
                NSMutableArray fileWrappers = NSMutableArray.Array();
                NSArray filenames = fm.DirectoryContentsAtPath(path);
                NSEnumerator enumerator = filenames.ObjectEnumerator();

                while ((filename = (NSString)enumerator.NextObject()) != null)
                {
                    NSFileWrapper w;

                    w = (NSFileWrapper)NSFileWrapper.Alloc().InitWithPath(path.StringByAppendingPathComponent(filename));
                    fileWrappers.AddObject(w);
                    //RELEASE(w);
                }
                this.InitDirectoryWithFileWrappers((NSDictionary)NSMutableDictionary.DictionaryWithObjectsForKeys(fileWrappers, filenames));
            }
            else if (fileType.IsEqualToString("NSFileTypeRegular"))
            {
                this.InitRegularFileWithContents(NSData.Alloc().InitWithContentsOfFile(path));
            }
            else if (fileType.IsEqualToString("NSFileTypeSymbolicLink"))
            {
                //this.InitSymbolicLinkWithDestination(fm.PathContentOfSymbolicLinkAtPath(path));
            }

            return this;
        }

        // Init an instance from data in std serial format.  Serial format is the
        // same as that used by NSText's RTFDFromRange: method.  This can 
        // create a tree of instances with a directory instance at the top
        public virtual id InitWithSerializedRepresentation(NSData data)
        {
            // FIXME - This should use a serializer. To get that working a helper object 
            // is needed that implements the NSObjCTypeSerializationCallBack protocol.
            // We should add this later, currently the NSArchiver is used.
            // Thanks to Richard, for pointing this out.
            NSFileWrapper wrapper = (NSFileWrapper)NSUnarchiver.UnarchiveObjectWithData(data);

            //RELEASE(this);
            return wrapper;
        }


        public virtual id InitWithURL(NSURL url, NSFileWrapperReadingOptions options, ref NSError outError)
        {

            return this;
        }



        public virtual void Dealloc()
        {

        }

        //
        // General methods 
        //

        // write instance to disk at path; if directory type, this
        // method is recursive; if updateFilenamesFlag is YES, the wrapper
        // will be updated with the name used in writing the file


        public virtual bool WriteToFile(NSString path, bool atomicFlag, bool updateFilenamesFlag)
        {
            NSFileManager fm = NSFileManager.DefaultManager;
            bool success = false;

            //NSDebugLLog(@"NSFileWrapper",
            //            @"writeToFile: %@ atomically: updateFilenames: ", path);

            switch (_wrapperType)
            {
                case GSFileWrapperType.GSFileWrapperDirectoryType:
                    {
                        // FIXME - more robust save proceedure when atomicFlag set
                        NSEnumerator enumerator = ((NSDictionary)_wrapperData).KeyEnumerator();
                        NSString key;
                        NSError err = null;

                        fm.CreateDirectoryAtPath(path, true, _fileAttributes, ref err);
                        while ((key = (NSString)enumerator.NextObject()) != null)
                        {
                            NSString newPath = path.StringByAppendingPathComponent(key);
                            NSFileWrapper fw = (NSFileWrapper)((NSDictionary)_wrapperData).ObjectForKey(key);
                            fw.WriteToFile(newPath, atomicFlag, updateFilenamesFlag);
                        }
                        success = true;
                        break;
                    }
                case GSFileWrapperType.GSFileWrapperRegularFileType:
                    {
                        if (((NSDictionary)_wrapperData).WriteToFile(path, atomicFlag))
                            success = fm.ChangeFileAttributes(_fileAttributes, path);
                        break;
                    }
                //case GSFileWrapperType.GSFileWrapperSymbolicLinkType:
                //    {
                //        success = fm.CreateSymbolicLinkAtPath(path, _wrapperData);
                //        break;
                //    }
            }
            if (success && updateFilenamesFlag)
            {
                SetFilename(path.LastPathComponent());
            }

            return success;
        }


        public virtual NSData SerializedRepresentation()
        {
            // FIXME - This should use a serializer. To get that working a helper object 
            // is needed that implements the NSObjCTypeSerializationCallBack protocol.
            // We should add this later, currently the NSArchiver is used.
            // Thanks to Richard, for pointing this out.
            return NSArchiver.ArchivedDataWithRootObject(this);
        }


        public virtual void SetFilename(NSString filename)
        {
            if (filename == null || filename.IsEqualToString(""))
            {
                NSException.Raise("NSInternalInconsistencyException", "Empty or nil argument to setFilename: ");
            }
            else
            {
                _filename = filename;
            }
        }


        public virtual NSString Filename()
        {
            return _filename;
        }


        public virtual void SetPreferredFilename(NSString filename)
        {
            if (filename == null || filename.IsEqualToString(""))
            {
                NSException.Raise("NSInternalInconsistencyException", "Empty or nil argument to setPreferredFilename: ");
            }
            else
            {
                _preferredFilename = filename;
            }
        }


        public virtual NSString PreferredFilename()
        {
            return _preferredFilename;
        }


        public virtual void SetFileAttributes(NSDictionary attributes)
        {
            if (_fileAttributes == null)
            {
                _fileAttributes = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
            }

            _fileAttributes.AddEntriesFromDictionary(attributes);
        }


        public virtual NSDictionary FileAttributes()
        {
            return _fileAttributes;
        }


        public virtual bool IsRegularFile()
        {
            if (_wrapperType == GSFileWrapperType.GSFileWrapperRegularFileType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public virtual bool IsDirectory()
        {
            if (_wrapperType == GSFileWrapperType.GSFileWrapperDirectoryType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public virtual bool IsSymbolicLink()
        {
            if (_wrapperType == GSFileWrapperType.GSFileWrapperSymbolicLinkType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //public virtual void SetIcon(NSImage icon) {
        //  ASSIGN(_iconImage, icon);
        //}


        //public virtual NSImage Icon() {
        //  if (_iconImage == nil && Filename())
        //    {
        //      return NSWorkspace.SharedWorkspace().IconForFile(Filename());
        //    }
        //  else
        //    {
        //      return _iconImage;
        //    }
        //}


        public virtual bool NeedsToBeUpdatedFromPath(NSString path)
        {
            NSFileManager fm = NSFileManager.DefaultManager;

            switch (_wrapperType)
            {
                case GSFileWrapperType.GSFileWrapperRegularFileType:
                    if (FileAttributes().IsEqualToDictionary(fm.FileAttributesAtPath(path, false)))
                        return false;
                    break;
                case GSFileWrapperType.GSFileWrapperSymbolicLinkType:
                    if (((NSString)_wrapperData).IsEqualToString(fm.PathContentOfSymbolicLinkAtPath(path)))
                        return false;
                    break;
                case GSFileWrapperType.GSFileWrapperDirectoryType:
                    // Has the dictory itself changed?
                    if (!FileAttributes().IsEqualToDictionary(fm.FileAttributesAtPath(path, false)))
                        return true;

                    // FIXME - for directory wrappers, we have to check if all the files are still there, 
                    // if they have the same attributes and if any new files have been added. 
                    // And this recursive for all included file wrappers

                    return false;
                    break;
            }

            return true;
        }


        public virtual bool UpdateFromPath(NSString path)
        {
            NSFileManager fm = NSFileManager.DefaultManager;

            switch (_wrapperType)
            {
                case GSFileWrapperType.GSFileWrapperRegularFileType:
                    if (FileAttributes().IsEqualToDictionary(fm.FileAttributesAtPath(path, false)))
                        return false;
                    InitWithPath(path);
                    break;
                case GSFileWrapperType.GSFileWrapperSymbolicLinkType:
                    if (FileAttributes().IsEqualToDictionary(fm.FileAttributesAtPath(path, false)) &&
                    ((NSString)_wrapperData).IsEqualToString(fm.PathContentOfSymbolicLinkAtPath(path)))
                        return false;
                    InitWithPath(path);
                    break;
                case GSFileWrapperType.GSFileWrapperDirectoryType:
                    // Has the dictory itself changed?
                    if (!FileAttributes().IsEqualToDictionary(fm.FileAttributesAtPath(path, false)))
                    {
                        // FIXME: This is not effizent
                        InitWithPath(path);
                        return true;
                    }
                    // FIXME - for directory wrappers, we have to check if all the files are still there, 
                    // if they have the same attributes and if any new files have been added. 
                    // And this recursive for all included file wrappers

                    return false;
                    break;
            }

            return true;
        }

        //
        // Directory type methods 
        //


        private void GSFileWrapperDirectoryTypeCheck()
        {
            if (_wrapperType != GSFileWrapperType.GSFileWrapperDirectoryType)
            {
                NSException.Raise("NSInternalInconsistencyException", @"Can't invoke blabla on a file wrapper that does not wrap a directory!");
            }
        }

        public virtual NSString AddFileWrapper(NSFileWrapper doc)
        {
            NSString key;

            GSFileWrapperDirectoryTypeCheck();

            key = doc.PreferredFilename();
            if (key == null || key.IsEqualToString(""))
            {
                NSException.Raise("NSInvalidArgumentException", "Adding file wrapper with no preferred filename.");
                return null;
            }

            if (((NSDictionary)_wrapperData).ObjectForKey(key) != null)
            {
                // FIXME - handle duplicate names
            }
            ((NSDictionary)_wrapperData).SetObjectForKey(doc, key);

            return key;
        }


        public virtual void RemoveFileWrapper(NSFileWrapper doc)
        {
            GSFileWrapperDirectoryTypeCheck();

            ((NSDictionary)_wrapperData).RemoveObjectsForKeys(((NSDictionary)_wrapperData).AllKeysForObject(doc));
        }


        public virtual NSDictionary FileWrappers()
        {
            GSFileWrapperDirectoryTypeCheck();

            return (NSDictionary)_wrapperData;
        }


        public virtual NSString KeyForFileWrapper(NSFileWrapper doc)
        {
            GSFileWrapperDirectoryTypeCheck();

            return (NSString)((NSDictionary)_wrapperData).AllKeysForObject(doc).ObjectAtIndex(0);
        }


        public virtual NSString AddFileWithPath(NSString path)
        {
            NSFileWrapper wrapper;
            GSFileWrapperDirectoryTypeCheck();

            wrapper = (NSFileWrapper)NSFileWrapper.Alloc().InitWithPath(path);
            if (wrapper != null)
            {
                return AddFileWrapper(wrapper);
            }
            else
            {
                return null;
            }
        }


        public virtual NSString AddRegularFileWithContents(NSData data, NSString filename)
        {
            NSFileWrapper wrapper;
            GSFileWrapperDirectoryTypeCheck();

            wrapper = (NSFileWrapper)NSFileWrapper.Alloc().InitRegularFileWithContents(data);
            if (wrapper != null)
            {
                wrapper.SetPreferredFilename(filename);
                return AddFileWrapper(wrapper);
            }
            else
            {
                return null;
            }
        }


        public virtual NSString AddSymbolicLinkWithDestination(NSString path, NSString filename)
        {
            NSFileWrapper wrapper;
            GSFileWrapperDirectoryTypeCheck();

            wrapper = (NSFileWrapper)NSFileWrapper.Alloc().InitSymbolicLinkWithDestination(path);
            if (wrapper != null)
            {
                wrapper.SetPreferredFilename(filename);
                return AddFileWrapper(wrapper);
            }
            else
            {
                return null;
            }
        }

        //								
        // Regular file type methods 				  
        //								


        public virtual NSData RegularFileContents()
        {
            if (_wrapperType == GSFileWrapperType.GSFileWrapperRegularFileType)
            {
                return (NSData)_wrapperData;
            }
            else
            {
                NSException.Raise("NSInternalInconsistencyException", "File wrapper does not wrap regular file.");
            }

            return null;
        }

        //								
        // Symbolic link type methods 				  
        //


        public virtual NSString SymbolicLinkDestination()
        {
            if (_wrapperType == GSFileWrapperType.GSFileWrapperSymbolicLinkType)
            {
                return (NSString)_wrapperData;
            }
            else
            {
                NSException.Raise("NSInternalInconsistencyException", "File wrapper does not wrap symbolic link.");
            }

            return null;
        }

        //								
        // Archiving 				  
        //


        public override void EncodeWithCoder(NSCoder aCoder)
        {
            if (aCoder.AllowsKeyedCoding)
            {
                aCoder.EncodeObject(SerializedRepresentation(), "NSFileWrapperData");
            }
            else
            {
                int wrapType = (int)_wrapperType;
                aCoder.EncodeValueOfObjCType<int>(ref wrapType);
                // Dont store the file name
                aCoder.EncodeObject(_preferredFilename);
                aCoder.EncodeObject(_fileAttributes);
                aCoder.EncodeObject(_wrapperData);
                //aCoder.EncodeObject(_iconImage);
            }
        }


        public override id InitWithCoder(NSCoder aDecoder)
        {
            if (aDecoder.AllowsKeyedCoding)
            {
                NSData data = (NSData)aDecoder.DecodeObjectForKey("NSFileWrapperData");
                return InitWithSerializedRepresentation(data);
            }
            else
            {
                int wrapperType;
                NSString preferredFilename;
                NSDictionary fileAttributes;
                id wrapperData;
                //NSImage iconImage;

                aDecoder.DecodeValueOfObjCType2<int>(out wrapperType);
                // Dont restore the file name
                preferredFilename = (NSString)aDecoder.DecodeObject();
                fileAttributes = (NSDictionary)aDecoder.DecodeObject();
                wrapperData = aDecoder.DecodeObject();
                //iconImage = aDecoder.DecodeObject();

                switch ((GSFileWrapperType)wrapperType)
                {
                    case GSFileWrapperType.GSFileWrapperRegularFileType:
                        {
                            InitRegularFileWithContents((NSData)wrapperData);
                            break;
                        }
                    case GSFileWrapperType.GSFileWrapperSymbolicLinkType:
                        {
                            InitSymbolicLinkWithDestination((NSString)wrapperData);
                            break;
                        }
                    case GSFileWrapperType.GSFileWrapperDirectoryType:
                        {
                            InitDirectoryWithFileWrappers((NSDictionary)wrapperData);
                            break;
                        }
                }

                if (preferredFilename != null)
                {
                    SetPreferredFilename(preferredFilename);
                }
                if (fileAttributes != null)
                {
                    SetFileAttributes(fileAttributes);
                }
                //if (iconImage != null)
                //{
                //    SetIcon(iconImage);
                //}
            }
            return this;
        }
    }
}


