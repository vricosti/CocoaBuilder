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
    public enum NSDataReadingOptions : ulong
    {
        NSDataReadingNoOption = 0,
        NSDataReadingMappedIfSafe = 1UL << 0,
        NSDataReadingUncached = 1UL << 1,
        NSDataReadingMappedAlways = 1UL << 3,
    };


    public class NSData : NSObject
    {
        new public static Class Class = new Class(typeof(NSData));
        new public static NSData Alloc() { return new NSData(); }

        public byte[] Bytes { get; protected set;}


        public int Length 
        { 
            get
            {
                return Bytes != null ? Bytes.Length : 0;
            }
        }

        public NSData()
        {

        }

        public static NSData Data()
        {
            return NSData.Alloc().InitWithBytes(new Byte[0]);
        }


        public static NSData DataWithBytes(byte[] bytes)
        {
            return NSData.Alloc().InitWithBytes(bytes);
        }

        public static NSData DataWithContentsOfFile(string path)
        {
            NSError err = null;
            NSData nsData = NSData.Alloc().InitWithContentsOfFile(path, NSDataReadingOptions.NSDataReadingNoOption, ref err);
            return nsData;
        }


        public NSData InitWithBytes(byte[] bytes)
        {
            NSData self = this;

            this.Bytes = bytes;

            return self;
        }

        public NSData InitWithContentsOfFile(string path)
        {
            NSError err = null;
            return InitWithContentsOfFile(path, NSDataReadingOptions.NSDataReadingNoOption, ref err);
        }

        public NSData InitWithContentsOfFile(string path, NSDataReadingOptions mask, ref NSError error)
        {
            NSData nsData = this;

            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    Bytes = new byte[fs.Length];
                    fs.Read(Bytes, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                }

            }
            catch (Exception)
            {
                nsData = null;
            }

            return nsData;
        }


        

    }
}
