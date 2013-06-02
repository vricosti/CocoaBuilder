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
    public abstract class NSCoder : NSObject
    {
        public virtual void EncodeValueOfObjCType<T>(ref T data) 
        {}

        public virtual void DecodeValueOfObjCType<T>(ref T data) 
        {}

        public virtual void DecodeValueOfObjCType2<T>(out T data)
        { 
            data = default(T);
        }

        public virtual void EncodeDataObject(NSData data) 
        { }

        public virtual NSData  DecodeDataObject() 
        { 
            return null; 
        }

        public virtual int VersionForClassName(NSString className) 
        { 
            return -1; 
        }

        public virtual void EncodeArrayOfObjCType<T>(uint count, ref T[] array) 
        { }

        public virtual void EncodeBytes(byte[] d, uint l) 
        { }

        public virtual void EncodeConditionalObject(id anObject) 
        {
            EncodeObject(anObject);
        }

        public virtual void EncodeObject(id anObject)
        { 
            EncodeValueOfObjCType<id>(ref anObject);
        }

        public virtual void EncodePropertyList(id plist)
        {
            id anObject = null;
            //anObject = plist ? (id)[NSSerializer serializePropertyList: plist] : nil;
            EncodeValueOfObjCType<id>(ref anObject);
        }

        public virtual void EncodePoint(ref NSPoint point)
        {
            EncodeValueOfObjCType<NSPoint>(ref point);
        }

        public virtual void EncodeRect(NSRect rect)
        {
            EncodeValueOfObjCType<NSRect>(ref rect);
        }

        public virtual void EncodeRootObject(id rootObject)
        {
            EncodeObject(rootObject);
        }

        public virtual void EncodeSize(NSSize size)
        {
            EncodeValueOfObjCType<NSSize>(ref size);
        }

        public virtual void EncodeValuesOfObjCTypes(params NSString[] types)
        {

        }

        public virtual void DecodeArrayOfObjCType<T>(uint count, ref T[] array)
        { }


        public virtual object[] DecodeBytesWithReturnedLength(ref int numBytes)
        {
            return null;
        }

        //- (id)decodeObject
        public virtual id DecodeObject()
        {
            return null;
        }

        //- (id)decodePropertyList
        public virtual id DecodePropertyList()
        {
            return null;
        }

        //- (NSPoint)decodePoint
        public virtual NSPoint DecodePoint()
        {
            NSPoint point = new NSPoint();
            DecodeValueOfObjCType<NSPoint>(ref point);
            return point;
        }


        public virtual NSRect DecodeRect()
        {
            NSRect	rect = new NSRect();
            DecodeValueOfObjCType<NSRect>(ref rect);
            return rect;
        }

        //- (NSSize)decodeSize
        public virtual NSSize DecodeSize()
        {
            NSSize size = new NSSize();
            DecodeValueOfObjCType<NSSize>(ref size);
            return size;
        }

        public virtual void DecodeValuesOfObjCTypes(params string[] types)
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        // Keyed archiving extensions
        ////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool AllowsKeyedCoding
        {
            get { return false; }
        }

        public virtual bool ContainsValueForKey(NSString key)
        {
            return false;
        }

        public virtual bool DecodeBoolForKey(NSString key)
        {
            return false;
        }

        public virtual byte[] DecodeBytesForKey(NSString key, ref int lengthp)
        {
            return null;
        }

        public virtual double DecodeDoubleForKey(NSString key)
        {
            return 0;
        }

        public virtual float DecodeFloatForKey(NSString key)
        {
            return 0;
        }

        public virtual int DecodeIntForKey(NSString key)
        {
            return 0;
        }

        public virtual int DecodeIntegerForKey(NSString key)
        {
            return 0;
        }

        public virtual Int32 DecodeInt32ForKey(NSString key)
        {
            return 0;
        }

        public virtual Int64 DecodeInt64ForKey(NSString key)
        {
            return 0;
        }

        public virtual id DecodeObjectForKey(NSString key)
        {
            return null;
        }

        
        public virtual NSPoint DecodePointForKey(NSString key)
        {
            return new NSPoint();
        }

        //- (NSRect)decodeRectForKey:(NSString *)key
        public virtual NSRect DecodeRectForKey(NSString key)
        {
            return new NSRect();
        }



        //- (NSSize)decodeSizeForKey:(NSString *)key
        public virtual NSSize DecodeSizeForKey(NSString key)
        {
            return new NSSize();
        }

        public virtual id DecodePropertyListForKey(NSString key)
        {
            return null;
        }


        public virtual void EncodeBoolForKey(bool aBool, NSString aKey)
        {

        }

        public virtual void EncodeBytesForKey(byte[] bytes, uint length, NSString aKey)
        {

        }

        public virtual void EncodeConditionalObjectForKey(id anObject, NSString aKey)
        {

        }

        public virtual void EncodeDoubleForKey(double aDouble, NSString aKey)
        {
        }

        public virtual void EncodeFloatForKey(float aDouble, NSString aKey)
        {
        }

        public virtual void EncodeIntForKey(int aDouble, NSString aKey)
        {
        }

        public virtual void EncodeIntegerForKey(int aDouble, NSString aKey)
        {
        }

        public virtual void EncodeInt32ForKey(int aDouble, NSString aKey)
        {
        }

        public virtual void EncodeInt64ForKey(int aDouble, NSString aKey)
        {
        }

        public virtual void EncodeObjectForKey(id anObject, NSString aKey)
        {
        }
        
    }
}
