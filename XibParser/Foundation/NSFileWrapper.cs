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

        protected NSString _filename;
        protected NSString _preferredFilename;
        protected NSMutableDictionary _fileAttributes;
        protected GSFileWrapperType _wrapperType;
        protected id _wrapperData;
        protected id _iconImage;



        public NSFileWrapper()
        {}

        new public static NSFileWrapper alloc()
        {
            return new NSFileWrapper();
        }


        public virtual id initDirectoryWithFileWrappers(NSDictionary docs)
        {
            NSFileWrapper self = this;

            if (base.init() != null)
            {
                NSEnumerator enumerator;
                id key;
                NSFileWrapper wrapper;

                _wrapperType = GSFileWrapperType.GSFileWrapperDirectoryType;
                _wrapperData = NSMutableDictionary.alloc().initWithCapacity((uint)docs.Count);

                enumerator = docs.keyEnumerator();
                while ((key = enumerator.nextObject()) != null)
                {
                    wrapper = (NSFileWrapper)docs.objectForKey(key);

                    if (wrapper.preferredFilename() == null)
                    {
                        wrapper.setPreferredFilename((NSString)key);
                    }
                    ((NSMutableDictionary)_wrapperData).setObjectForKey(wrapper, key);
                }
            }
            return self;
        }

        // init instance of regular file type

        public virtual id initRegularFileWithContents(NSData data)
        {
            if (base.init() != null)
            {
                _wrapperData = data;
                _wrapperType = GSFileWrapperType.GSFileWrapperRegularFileType;
            }
            return this;
        }

        // init instance of symbolic link type

        public virtual id initSymbolicLinkWithDestination(NSString path)
        {
            if (base.init() != null)
            {
                _wrapperData = path;
                _wrapperType = GSFileWrapperType.GSFileWrapperSymbolicLinkType;
            }
            return this;
        }

        /**
         * init an instance from the file, directory, or symbolic link at path.<br /> 
         * This can create a tree of instances with a directory instance at the top
         */

        public virtual id initWithPath(NSString path)
        {

            NSFileManager fm = NSFileManager.DefaultManager;
            NSString fileType;

            //NS.DebugLLog(@"NSFileWrapper", @"initWithPath: %@", path);

            // Store the full path in filename, the specification is unclear in this point
            setFilename(path);
            setPreferredFilename(path.lastPathComponent());
            setFileAttributes(fm.FileAttributesAtPath(path, false));

            fileType = FileAttributes().FileType;
            if (fileType.isEqualToString("NSFileTypeDirectory"))
            {
                NSString filename;
                NSMutableArray fileWrappers = NSMutableArray.array();
                NSArray filenames = fm.DirectoryContentsAtPath(path);
                NSEnumerator enumerator = filenames.objectEnumerator();

                while ((filename = (NSString)enumerator.nextObject()) != null)
                {
                    NSFileWrapper w;

                    w = (NSFileWrapper)NSFileWrapper.alloc().initWithPath(path.stringByAppendingPathComponent(filename));
                    fileWrappers.addObject(w);
                    //RELEASE(w);
                }
                this.initDirectoryWithFileWrappers((NSDictionary)NSMutableDictionary.dictionaryWithObjectsForKeys(fileWrappers, filenames));
            }
            else if (fileType.isEqualToString("NSFileTypeRegular"))
            {
                this.initRegularFileWithContents(NSData.alloc().initWithContentsOfFile(path));
            }
            else if (fileType.isEqualToString("NSFileTypeSymbolicLink"))
            {
                //this.initSymbolicLinkWithDestination(fm.PathContentOfSymbolicLinkAtPath(path));
            }

            return this;
        }

        // init an instance from data in std serial format.  Serial format is the
        // same as that used by NSText's RTFDFromRange: method.  This can 
        // create a tree of instances with a directory instance at the top
        public virtual id initWithSerializedRepresentation(NSData data)
        {
            // FIXME - This should use a serializer. To get that working a helper object 
            // is needed that implements the NSObjCTypeSerializationCallBack protocol.
            // We should add this later, currently the NSArchiver is used.
            // Thanks to Richard, for pointing this out.
            NSFileWrapper wrapper = (NSFileWrapper)NSUnarchiver.UnarchiveObjectWithData(data);

            //RELEASE(this);
            return wrapper;
        }


        public virtual id initWithURL(NSURL url, NSFileWrapperReadingOptions options, ref NSError outError)
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


        public virtual bool writeToFile(NSString path, bool atomicFlag, bool updateFilenamesFlag)
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
                        NSEnumerator enumerator = ((NSDictionary)_wrapperData).keyEnumerator();
                        NSString key;
                        NSError err = null;

                        fm.CreateDirectoryAtPath(path, true, _fileAttributes, ref err);
                        while ((key = (NSString)enumerator.nextObject()) != null)
                        {
                            NSString newPath = path.stringByAppendingPathComponent(key);
                            NSFileWrapper fw = (NSFileWrapper)((NSDictionary)_wrapperData).objectForKey(key);
                            fw.writeToFile(newPath, atomicFlag, updateFilenamesFlag);
                        }
                        success = true;
                        break;
                    }
                case GSFileWrapperType.GSFileWrapperRegularFileType:
                    {
                        if (((NSDictionary)_wrapperData).writeToFile(path, atomicFlag))
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
                setFilename(path.lastPathComponent());
            }

            return success;
        }


        public virtual NSData serializedRepresentation()
        {
            // FIXME - This should use a serializer. To get that working a helper object 
            // is needed that implements the NSObjCTypeSerializationCallBack protocol.
            // We should add this later, currently the NSArchiver is used.
            // Thanks to Richard, for pointing this out.
            return NSArchiver.ArchivedDataWithRootObject(this);
        }


        public virtual void setFilename(NSString filename)
        {
            if (filename == null || filename.isEqualToString(""))
            {
                NSException.raise("NSInternalInconsistencyException", "Empty or nil argument to setFilename: ");
            }
            else
            {
                _filename = filename;
            }
        }


        public virtual NSString filename()
        {
            return _filename;
        }


        public virtual void setPreferredFilename(NSString filename)
        {
            if (filename == null || filename.isEqualToString(""))
            {
                NSException.raise("NSInternalInconsistencyException", "Empty or nil argument to setPreferredFilename: ");
            }
            else
            {
                _preferredFilename = filename;
            }
        }


        public virtual NSString preferredFilename()
        {
            return _preferredFilename;
        }


        public virtual void setFileAttributes(NSDictionary attributes)
        {
            if (_fileAttributes == null)
            {
                _fileAttributes = (NSMutableDictionary)NSMutableDictionary.alloc().init();
            }

            _fileAttributes.addEntriesFromDictionary(attributes);
        }


        public virtual NSDictionary FileAttributes()
        {
            return _fileAttributes;
        }


        public virtual bool isRegularFile()
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


        public virtual bool isDirectory()
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


        public virtual bool isSymbolicLink()
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
        //  if (_iconImage == nil && filename())
        //    {
        //      return NSWorkspace.SharedWorkspace().IconForFile(filename());
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
                    if (FileAttributes().isEqualToDictionary(fm.FileAttributesAtPath(path, false)))
                        return false;
                    break;
                case GSFileWrapperType.GSFileWrapperSymbolicLinkType:
                    if (((NSString)_wrapperData).isEqualToString(fm.PathContentOfSymbolicLinkAtPath(path)))
                        return false;
                    break;
                case GSFileWrapperType.GSFileWrapperDirectoryType:
                    // Has the dictory itself changed?
                    if (!FileAttributes().isEqualToDictionary(fm.FileAttributesAtPath(path, false)))
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
                    if (FileAttributes().isEqualToDictionary(fm.FileAttributesAtPath(path, false)))
                        return false;
                    initWithPath(path);
                    break;
                case GSFileWrapperType.GSFileWrapperSymbolicLinkType:
                    if (FileAttributes().isEqualToDictionary(fm.FileAttributesAtPath(path, false)) &&
                    ((NSString)_wrapperData).isEqualToString(fm.PathContentOfSymbolicLinkAtPath(path)))
                        return false;
                    initWithPath(path);
                    break;
                case GSFileWrapperType.GSFileWrapperDirectoryType:
                    // Has the dictory itself changed?
                    if (!FileAttributes().isEqualToDictionary(fm.FileAttributesAtPath(path, false)))
                    {
                        // FIXME: This is not effizent
                        initWithPath(path);
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
                NSException.raise("NSInternalInconsistencyException", @"Can't invoke blabla on a file wrapper that does not wrap a directory!");
            }
        }

        public virtual NSString addFileWrapper(NSFileWrapper doc)
        {
            NSString key;

            GSFileWrapperDirectoryTypeCheck();

            key = doc.preferredFilename();
            if (key == null || key.isEqualToString(""))
            {
                NSException.raise("NSInvalidArgumentException", "Adding file wrapper with no preferred filename.");
                return null;
            }

            if (((NSDictionary)_wrapperData).objectForKey(key) != null)
            {
                // FIXME - handle duplicate names
            }
            ((NSDictionary)_wrapperData).setObjectForKey(doc, key);

            return key;
        }


        public virtual void removeFileWrapper(NSFileWrapper doc)
        {
            GSFileWrapperDirectoryTypeCheck();

            ((NSDictionary)_wrapperData).removeObjectsForKeys(((NSDictionary)_wrapperData).allKeysForObject(doc));
        }


        public virtual NSDictionary fileWrappers()
        {
            GSFileWrapperDirectoryTypeCheck();

            return (NSDictionary)_wrapperData;
        }


        public virtual NSString keyForFileWrapper(NSFileWrapper doc)
        {
            GSFileWrapperDirectoryTypeCheck();

            return (NSString)((NSDictionary)_wrapperData).allKeysForObject(doc).objectAtIndex(0);
        }


        public virtual NSString addFileWithPath(NSString path)
        {
            NSFileWrapper wrapper;
            GSFileWrapperDirectoryTypeCheck();

            wrapper = (NSFileWrapper)NSFileWrapper.alloc().initWithPath(path);
            if (wrapper != null)
            {
                return addFileWrapper(wrapper);
            }
            else
            {
                return null;
            }
        }


        public virtual NSString addRegularFileWithContents(NSData data, NSString filename)
        {
            NSFileWrapper wrapper;
            GSFileWrapperDirectoryTypeCheck();

            wrapper = (NSFileWrapper)NSFileWrapper.alloc().initRegularFileWithContents(data);
            if (wrapper != null)
            {
                wrapper.setPreferredFilename(filename);
                return addFileWrapper(wrapper);
            }
            else
            {
                return null;
            }
        }


        public virtual NSString addSymbolicLinkWithDestination(NSString path, NSString filename)
        {
            NSFileWrapper wrapper;
            GSFileWrapperDirectoryTypeCheck();

            wrapper = (NSFileWrapper)NSFileWrapper.alloc().initSymbolicLinkWithDestination(path);
            if (wrapper != null)
            {
                wrapper.setPreferredFilename(filename);
                return addFileWrapper(wrapper);
            }
            else
            {
                return null;
            }
        }

        //								
        // Regular file type methods 				  
        //								


        public virtual NSData regularFileContents()
        {
            if (_wrapperType == GSFileWrapperType.GSFileWrapperRegularFileType)
            {
                return (NSData)_wrapperData;
            }
            else
            {
                NSException.raise("NSInternalInconsistencyException", "File wrapper does not wrap regular file.");
            }

            return null;
        }

        //								
        // Symbolic link type methods 				  
        //


        public virtual NSString symbolicLinkDestination()
        {
            if (_wrapperType == GSFileWrapperType.GSFileWrapperSymbolicLinkType)
            {
                return (NSString)_wrapperData;
            }
            else
            {
                NSException.raise("NSInternalInconsistencyException", "File wrapper does not wrap symbolic link.");
            }

            return null;
        }

        //								
        // Archiving 				  
        //


        public override void encodeWithCoder(NSCoder aCoder)
        {
            if (aCoder.AllowsKeyedCoding)
            {
                aCoder.encodeObject(serializedRepresentation(), "NSFileWrapperData");
            }
            else
            {
                int wrapType = (int)_wrapperType;
                aCoder.encodeValueOfObjCType<int>(ref wrapType);
                // Dont store the file name
                aCoder.encodeObject(_preferredFilename);
                aCoder.encodeObject(_fileAttributes);
                aCoder.encodeObject(_wrapperData);
                //aCoder.encodeObject(_iconImage);
            }
        }


        public override id initWithCoder(NSCoder aDecoder)
        {
            if (aDecoder.AllowsKeyedCoding)
            {
                NSData data = (NSData)aDecoder.decodeObjectForKey("NSFileWrapperData");
                return initWithSerializedRepresentation(data);
            }
            else
            {
                int wrapperType;
                NSString preferredFilename;
                NSDictionary fileAttributes;
                id wrapperData;
                //NSImage iconImage;

                aDecoder.decodeValueOfObjCType2<int>(out wrapperType);
                // Dont restore the file name
                preferredFilename = (NSString)aDecoder.decodeObject();
                fileAttributes = (NSDictionary)aDecoder.decodeObject();
                wrapperData = aDecoder.decodeObject();
                //iconImage = aDecoder.decodeObject();

                switch ((GSFileWrapperType)wrapperType)
                {
                    case GSFileWrapperType.GSFileWrapperRegularFileType:
                        {
                            initRegularFileWithContents((NSData)wrapperData);
                            break;
                        }
                    case GSFileWrapperType.GSFileWrapperSymbolicLinkType:
                        {
                            initSymbolicLinkWithDestination((NSString)wrapperData);
                            break;
                        }
                    case GSFileWrapperType.GSFileWrapperDirectoryType:
                        {
                            initDirectoryWithFileWrappers((NSDictionary)wrapperData);
                            break;
                        }
                }

                if (preferredFilename != null)
                {
                    setPreferredFilename(preferredFilename);
                }
                if (fileAttributes != null)
                {
                    setFileAttributes(fileAttributes);
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


