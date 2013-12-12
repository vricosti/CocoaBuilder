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
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSDocument.m

    public class NSDocument : NSObject
    {
        new public static Class Class = new Class(typeof(NSDocument));
        new public static NSDocument alloc() { return new NSDocument(); }

        protected NSWindow _window;		// Outlet for the single window case
        protected NSMutableArray _window_controllers;	// WindowControllers for this document
        protected NSURL _file_url;		// Save location as URL
        protected NSString _file_name;		// Save location
        protected NSString _file_type;		// file/document type
        //NSDate _file_modification_date;// file modification date
        protected NSString _last_component_file_name; // file name last component
        protected NSURL _autosaved_file_url;	// Autosave location as URL
        //NSPrintInfo* _print_info;		// print info record
        protected id _printOp_delegate;	// delegate and selector called
        protected SEL _printOp_didRunSelector;//   after modal print operation
        protected NSView _save_panel_accessory;	// outlet for the accessory save-panel view
        protected NSPopUpButton _spa_button;     	// outlet for "the File Format:" button in the save panel.
        protected NSString _save_type;             // the currently selected extension.
        //NSUndoManager* _undo_manager;		// Undo manager for this document
        protected long _change_count;		// number of time the document has been changed
        protected long _autosave_change_count;	// number of time the document has been changed since the last autosave
        protected int _document_index;


        public virtual NSString FileType 
        {
            get { return GetFileType(); }
            set { SetFileType(value); }
        }

        public virtual NSURL FileURL
        {
            get { return GetFileURL(); }
            set { SetFileURL(value); }
        }

        public override id init()
        {
            id self = this;

            return self;
        }

        public virtual id InitWithType(NSString type, ref NSError error)
        {
            id self = this.init();
            if (self != null)
            {
                this.SetFileType(type);
            }

            return self;
        }


        

        //-(id)initForURL:(NSURL *)absoluteDocumentURL withContentsOfURL:(NSURL *)absoluteDocumentContentsURL ofType:(NSString *)typeName error:(NSError **)outError
        //-[IBCocoaDocument initForURL:withContentsOfURL:ofType:error:]
        public virtual id InitForURL(NSURL forUrl, NSURL url, NSString type, ref NSError error)
        {
            id self = this.InitWithType(type, ref error);
            if (self != null)
            {
                this.SetFileType(type);
                if (forUrl != null)
                    this.SetFileURL(forUrl);
                if (this.ReadFromURL(url, type, ref error))
                {
                    if (!url.isEqual(forUrl))
                    {
                        //
                    }
                }
            }

            return self;
        }
        public virtual id InitForURL(NSURL absoluteURL, NSURL absoluteDocumentContentsURL, NSString typeName, object dummyNullable)
        {
            NSError outError = null;
            return this.InitForURL(absoluteURL, absoluteDocumentContentsURL, typeName, ref outError);
        }

        public virtual id InitWithContentsOfURL(NSURL url, NSString type, ref NSError error)
        {
            id self = null;

            self = this.InitForURL(url, url, type, ref error);
            //this.SetFileModificationDate(NSDate.Date);

             return self;
        }

        public virtual NSString GetFileType()
        {
            return _file_type;
        }

        public virtual void SetFileType(NSString type)
        {
            _file_type = type;
        }

        public virtual NSURL GetFileURL()
        {
            return _file_url;
        }

        public virtual void SetFileURL(NSURL url)
        {
            _file_url = url;

            _file_name = (url != null && url.IsFileURL ? url.Path : null);
            this.SetLastComponentOfFileName(_file_url.Path.lastPathComponent());
        }

        public virtual void SetLastComponentOfFileName(NSString str)
        {
            _last_component_file_name = str;
            //[[self windowControllers] makeObjectsPerformSelector:@selector(synchronizeWindowTitleWithDocumentName)];
        }

        public virtual bool LoadDataRepresentation(NSData data, NSString type)
        {
            NSException.raise("NSInternalInconsistencyException", @"%@ must implement %@",
                NS.StringFromClass(NSDocument.Class), SEL.StringFromSelector(new SEL("LoadDataRepresentation")));
            return false;
        }

        public virtual bool LoadFileWrapperRepresentation(NSFileWrapper wrapper, NSString type)
        {
            if (wrapper.isRegularFile())
            {
                return this.LoadDataRepresentation(wrapper.regularFileContents(), type);
            }

            /*
             * This even happens on a symlink.  May want to use
             * -stringByResolvingAllSymlinksInPath somewhere, but Apple doesn't.
             */
            NS.Log(@"%@ must be overridden if your document deals with file packages.", "LoadFileWrapperRepresentation");

            return false;
        }

        public virtual bool ReadFromFile(NSString fileName, NSString type)
        {
            NSFileWrapper wrapper = (NSFileWrapper)NSFileWrapper.alloc().initWithPath(fileName);
            return this.LoadFileWrapperRepresentation(wrapper, type);
        }

        public virtual bool ReadFromURL(NSURL url, NSString typeName, object unused = null)
        {
            NSError err = null;
            return this.ReadFromURL(url, typeName, ref err);
        }

        public virtual bool ReadFromURL(NSURL url, NSString typeName, ref NSError outError)
        {

            if (url.isFileURL())
            {
                NSString fileName = url.Path;

                if (Objc.Overridden(this.GetType(), "ReadFromFile"))
                {
                    outError = null;
                    return this.ReadFromFile(fileName, typeName);
                }
                else
                {
                    NSFileWrapper wrapper = (NSFileWrapper)NSFileWrapper.alloc().initWithPath(fileName);
                    return this.readFromFileWrapper(wrapper, typeName, ref outError);
                }
            }
            else
            {
                //FIXME
                //return this.ReadFromData(url.ResourceDataUsingCache(true), typeName, ref outError);
                return false;
            }
        }


        public virtual bool ReadFromData(NSData data, NSString typeName, ref NSError outError)
        {
            if (Objc.Overridden(this.GetType(), "LoadDataRepresentation"))
            {
                outError = null;
                return this.LoadDataRepresentation(data, typeName);
            }

            NSException.raise("NSInternalInconsistencyException", @"%@ must implement %@",
                NS.StringFromClass(NSDocument.Class), SEL.StringFromSelector(new SEL("LoadDataRepresentation")));
            return false;
        }


        public virtual bool readFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            if (Objc.Overridden(this.GetType(), "LoadFileWrapperRepresentation"))
            {
                outError = null;
                return this.LoadFileWrapperRepresentation(fileWrapper, typeName);
            }

            if (fileWrapper.isRegularFile())
            {
                return this.ReadFromData(fileWrapper.regularFileContents(), typeName, ref outError);
            }


            outError = null;
            return false;
        }

        

    }
}
