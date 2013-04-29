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
    //https://github.com/gnustep/gnustep-base/blob/master/Headers/Foundation/NSKeyedArchiver.h
    //https://github.com/gnustep/gnustep-base/blob/master/Source/NSKeyedUnarchiver.m
    public class NSKeyedUnarchiver : NSCoder
    {
        new public static Class Class = new Class(typeof(NSKeyedUnarchiver));
        protected id _delegate;

        public NSKeyedUnarchiver()
        {

        }


        public static Class ClassForClassName(NSString codedName)
        {
            Class cls = null;

            return cls;
        }


        public virtual id Delegate
        {
            get { return _delegate; }
            set { _delegate = value; }
        }

        public override bool AllowsKeyedCoding 
        {
            get
            {
                return true;
            }
        }

        public override bool ContainsValueForKey(string key)
        {
            return false;
        }



        public override void DecodeArrayOfObjCType(string itemType, int count, ref object address)
        {
            throw new NotImplementedException();
        }

        public override bool DecodeBoolForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override byte[] DecodeBytesForKey(string key, ref int lengthp)
        {
            throw new NotImplementedException();
        }

        public override object[] DecodeBytesWithReturnedLength(ref int numBytes)
        {
            throw new NotImplementedException();
        }

        public override bool DecodeDoubleForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override bool DecodeFloatForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override int decodeInt32ForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override long DecodeInt64ForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override int DecodeIntegerForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override int DecodeIntForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override object DecodeObject(string key)
        {
            throw new NotImplementedException();
        }

        public override object DecodeObjectForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override object DecodeObjectOfClass(Type type, string key)
        {
            throw new NotImplementedException();
        }

        public override NSPoint DecodePoint()
        {
            throw new NotImplementedException();
        }

        public override NSPoint DecodePointForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override object DecodePropertyList()
        {
            throw new NotImplementedException();
        }

        public override object DecodePropertyListForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override NSRect DecodeRect()
        {
            throw new NotImplementedException();
        }

        public override NSRect DecodeRectForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override NSSize DecodeSize()
        {
            throw new NotImplementedException();
        }

        public override NSSize DecodeSizeForKey(string key)
        {
            throw new NotImplementedException();
        }

        public override void decodeValueOfObjCType(string valueType, ref object data)
        {
            throw new NotImplementedException();
        }
    }
}
