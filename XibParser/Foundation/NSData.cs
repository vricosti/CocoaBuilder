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
        new public static NSData alloc() { return new NSData(); }

        protected byte[] _bytes;

        //public byte[] Bytes 
        //{
        //    get { return _bytes; }
        //    protected set { setBytes(value); }
        //}

        public uint Length { get { return length();} }



        public NSData()
        {

        }

        public static NSData data()
        {
            return (NSData)NSData.alloc().initWithBytes(new Byte[0]);
        }

        protected virtual void setBytes(byte[] bytes)
        {
            _bytes = bytes;
        }

        public virtual byte[] bytes()
        {
            return _bytes;
        }

        public virtual uint length()
        {
            return ((bytes() != null) ? (uint)bytes().Length : 0);
        }

        public static NSData dataWithBytes(byte[] bytes)
        {
            return (NSData)NSData.alloc().initWithBytes(bytes);
        }

        public static NSData dataWithContentsOfFile(string path)
        {
            NSError err = null;
            NSData nsData = (NSData)NSData.alloc().initWithContentsOfFile(path, NSDataReadingOptions.NSDataReadingNoOption, ref err);
            return nsData;
        }


        public virtual id initWithBytes(byte[] bytes)
        {
            NSData self = this;

            this.setBytes(bytes);

            return self;
        }

        public virtual id initWithContentsOfFile(string path)
        {
            NSError err = null;
            return initWithContentsOfFile(path, NSDataReadingOptions.NSDataReadingNoOption, ref err);
        }

        public virtual id initWithContentsOfFile(string path, NSDataReadingOptions mask, ref NSError error)
        {
            NSData nsData = this;

            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    this.setBytes(new byte[fs.Length]);
                    fs.Read(bytes(), 0, Convert.ToInt32(fs.Length));
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
