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

    public interface INSKeyedUnarchiverDelegate
    {
        Class UnarchiverCannotDecodeObjectOfClassName(NSKeyedUnarchiver anUnarchiver, NSString aName, NSArray classNames);
        id UnarchiverDidDecodeObject(NSKeyedUnarchiver anUnarchiver, id anObject);
        void UnarchiverDidFinish(NSKeyedUnarchiver anUnarchiver);
        void UnarchiverWillFinish(NSKeyedUnarchiver anUnarchiver);
        void UnarchiverWillReplaceObject(NSKeyedUnarchiver anUnarchiver, id anObject, id newObject);
    }


    public class NSKeyedUnarchiver : NSCoder, INSKeyedUnarchiverDelegate
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

        public override bool ContainsValueForKey(NSString key)
        {
            return false;
        }

        //– initForReadingWithData:

        public virtual id InitForReadingWithData(NSData data, ref NSError outError)
        {
            return null;
        }
        public virtual id InitForReadingWithData(NSData data, object dummyObject)
        {
            return null;
        }

        public override void DecodeArrayOfObjCType<T>(uint count, ref T[] array)
        {
            throw new NotImplementedException();
        }

        public override bool DecodeBoolForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override byte[] DecodeBytesForKey(NSString key, ref int lengthp)
        {
            throw new NotImplementedException();
        }

        public override object[] DecodeBytesWithReturnedLength(ref int numBytes)
        {
            throw new NotImplementedException();
        }

        public override double DecodeDoubleForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override float DecodeFloatForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override int DecodeInt32ForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override long DecodeInt64ForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override int DecodeIntegerForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override int DecodeIntForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override id DecodeObject()
        {
            throw new NotImplementedException();
        }

        public override id DecodeObjectForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        //public override object DecodeObjectOfClass(Type type, NSString key)
        //{
        //    throw new NotImplementedException();
        //}

        public override NSPoint DecodePoint()
        {
            throw new NotImplementedException();
        }

        public override NSPoint DecodePointForKey(NSString aKey)
        {
            NSPoint point = new NSPoint();

            NSString val = (NSString)DecodeObjectForKey(aKey);
            if (val != null)
            {
                point = (NSPoint)val;
            }

            return point;
        }

        public override id DecodePropertyList()
        {
            throw new NotImplementedException();
        }

        public override id DecodePropertyListForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override NSRect DecodeRect()
        {
            throw new NotImplementedException();
        }

        public override NSRect DecodeRectForKey(NSString aKey)
        {
            NSRect rect = new NSSize();

            NSString val = (NSString)DecodeObjectForKey(aKey);
            if (val != null)
            {
                rect = (NSRect)val;
            }

            return rect;
        }

        public override NSSize DecodeSize()
        {
            throw new NotImplementedException();
        }

        public override NSSize DecodeSizeForKey(NSString aKey)
        {
            NSSize aSize = new NSSize();

            NSString val = (NSString)DecodeObjectForKey(aKey);
            if (val != null)
            {
                aSize = (NSSize)val;
            }

            return aSize;
        }

      

        public override void DecodeValueOfObjCType<T>(ref T data)
        {
            throw new NotImplementedException();
        }

        public virtual Class UnarchiverCannotDecodeObjectOfClassName(NSKeyedUnarchiver anUnarchiver, NSString aName, NSArray classNames)
        {
            return null;
        }

        public virtual id UnarchiverDidDecodeObject(NSKeyedUnarchiver anUnarchiver, id anObject)
        {
            return anObject;
        }


        public virtual void UnarchiverDidFinish(NSKeyedUnarchiver anUnarchiver)
        {
            
        }

        public virtual void UnarchiverWillFinish(NSKeyedUnarchiver anUnarchiver)
        {
            
        }

        public virtual void UnarchiverWillReplaceObject(NSKeyedUnarchiver anUnarchiver, id anObject, id newObject)
        {
            
        }

        public virtual id _DecodeArrayOfObjectsForKey(NSString aKey)
        {
            return null;
        }

        public virtual id _DecodeArrayOfObjectsForElement(GSXibElement element)
        {
            return null;
        }

        public virtual id _DecodeDictionaryOfObjectsForElement(GSXibElement element)
        {
            return null;
        }
    }
}
