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
        Class unarchiverCannotDecodeObjectOfClassName(NSKeyedUnarchiver anUnarchiver, NSString aName, NSArray classNames);
        id unarchiverDidDecodeObject(NSKeyedUnarchiver anUnarchiver, id anObject);
        void unarchiverDidFinish(NSKeyedUnarchiver anUnarchiver);
        void unarchiverWillFinish(NSKeyedUnarchiver anUnarchiver);
        void unarchiverWillReplaceObject(NSKeyedUnarchiver anUnarchiver, id anObject, id newObject);
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
            get { return getDelegate(); }
            set { setDelegate(value); }
        }

        public virtual id getDelegate()
        {
            return _delegate;
        }
        public virtual void setDelegate(id dlgate)
        {
            _delegate = dlgate;
        }


        public override bool AllowsKeyedCoding 
        {
            get
            {
                return true;
            }
        }

        public override bool containsValueForKey(NSString key)
        {
            return false;
        }

        //– initForReadingWithData:

        public virtual id initForReadingWithData(NSData data, ref NSError outError)
        {
            return null;
        }
        public virtual id initForReadingWithData(NSData data, object dummyObject)
        {
            return null;
        }

        public override void decodeArrayOfObjCType<T>(uint count, ref T[] array)
        {
            throw new NotImplementedException();
        }

        public override bool decodeBoolForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override byte[] decodeBytesForKey(NSString key, ref int lengthp)
        {
            throw new NotImplementedException();
        }

        public override object[] decodeBytesWithReturnedLength(ref int numBytes)
        {
            throw new NotImplementedException();
        }

        public override double decodeDoubleForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override float decodeFloatForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override int decodeInt32ForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override long decodeInt64ForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override int decodeIntegerForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override int decodeIntForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override id decodeObject()
        {
            throw new NotImplementedException();
        }

        public override id decodeObjectForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        //public override object DecodeObjectOfClass(Type type, NSString key)
        //{
        //    throw new NotImplementedException();
        //}

        public override NSPoint decodePoint()
        {
            throw new NotImplementedException();
        }

        public override NSPoint decodePointForKey(NSString aKey)
        {
            NSPoint point = new NSPoint();

            NSString val = (NSString)decodeObjectForKey(aKey);
            if (val != null)
            {
                point = (NSPoint)val;
            }

            return point;
        }

        public override id decodePropertyList()
        {
            throw new NotImplementedException();
        }

        public override id decodePropertyListForKey(NSString key)
        {
            throw new NotImplementedException();
        }

        public override NSRect decodeRect()
        {
            throw new NotImplementedException();
        }

        public override NSRect decodeRectForKey(NSString aKey)
        {
            NSRect rect = new NSSize();

            NSString val = (NSString)decodeObjectForKey(aKey);
            if (val != null)
            {
                rect = (NSRect)val;
            }

            return rect;
        }

        public override NSSize decodeSize()
        {
            throw new NotImplementedException();
        }

        public override NSSize decodeSizeForKey(NSString aKey)
        {
            NSSize aSize = new NSSize();

            NSString val = (NSString)decodeObjectForKey(aKey);
            if (val != null)
            {
                aSize = (NSSize)val;
            }

            return aSize;
        }

      

        public override void decodeValueOfObjCType<T>(ref T data)
        {
            throw new NotImplementedException();
        }

        public virtual Class unarchiverCannotDecodeObjectOfClassName(NSKeyedUnarchiver anUnarchiver, NSString aName, NSArray classNames)
        {
            return null;
        }

        public virtual id unarchiverDidDecodeObject(NSKeyedUnarchiver anUnarchiver, id anObject)
        {
            return anObject;
        }


        public virtual void unarchiverDidFinish(NSKeyedUnarchiver anUnarchiver)
        {
            
        }

        public virtual void unarchiverWillFinish(NSKeyedUnarchiver anUnarchiver)
        {
            
        }

        public virtual void unarchiverWillReplaceObject(NSKeyedUnarchiver anUnarchiver, id anObject, id newObject)
        {
            
        }

        public virtual id _decodeArrayOfObjectsForKey(NSString aKey)
        {
            return null;
        }

        public virtual id _decodeArrayOfObjectsForElement(GSXibElement element)
        {
            return null;
        }

        public virtual id _decodeDictionaryOfObjectsForElement(GSXibElement element)
        {
            return null;
        }
    }
}
