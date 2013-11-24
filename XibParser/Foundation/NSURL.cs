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
using System.IO;

namespace Smartmobili.Cocoa
{

    //https://github.com/stevegeek/cocotron/blob/master/Foundation/NSURL/NSURL.h

    public class NSURL : NSObject
    {
        new public static Class Class = new Class(typeof(NSURL));
        new public static NSURL Alloc() { return new NSURL(); }

        protected NSURL _baseURL;
        protected NSString _string;
        protected NSString _scheme;
        protected NSString _host;
        protected NSNumber _port;
        protected NSString _user;
        protected NSString _password;
        protected NSString _path;
        protected NSString _parameter;
        protected NSString _query;
        protected NSString _fragment;


        public readonly string NSURLFileScheme = "file";

        //NSURL *fileURL = [[NSURL alloc] initFileURLWithPath:filePath];
        //NSURL *fileURL = [NSURL fileURLWithPath:filePath];


        public virtual bool IsFileURL
        {
            get { return isFileURL(); }
        }

        public virtual NSString Path
        {
            get { return GetPath(); }
        }

        public NSURL()
        {}

        public virtual bool isFileURL()
        {
            return true;
        }

        public virtual NSString GetPath()
        {
            return this._PathWithEscapes(false);
        }

        private NSString _PathWithEscapes(bool withEscapes)
        {
            return _path;
        }
//        -initWithScheme:(NSString *)scheme host:(NSString *)host path:(NSString *)path {
//   _scheme=[scheme copy];
//   _host=[host copy];
//   _path=[path copy];
//   return self;
//}

        public static id FileURLWithPath(NSString aPath)
        {
            return NSURL.Alloc().InitFileURLWithPath(aPath);
        }
        public virtual id InitFileURLWithPath(NSString aPath)
        {
            return this.InitWithScheme(NSURLFileScheme, @"localhost", aPath);
        }

        public virtual id InitWithScheme(NSString scheme, NSString host, NSString path)
        {
            id self = this;

            _scheme = scheme;
            _host = host;
            _path = path;

            return self;
        }

        

    }
}
