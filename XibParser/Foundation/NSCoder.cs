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
        public virtual void encodeValueOfObjCType<T>(ref T data) 
        {}

        public virtual void decodeValueOfObjCType<T>(ref T data) 
        {}

        public virtual void decodeValueOfObjCType2<T>(out T data)
        { 
            data = default(T);
        }

        public virtual void encodeDataObject(NSData data) 
        { }

        public virtual NSData  decodeDataObject() 
        { 
            return null; 
        }

        public virtual int versionForClassName(NSString className) 
        { 
            return -1; 
        }

        public virtual void encodeArrayOfObjCType<T>(uint count, ref T[] array) 
        { }

        public virtual void encodeBytes(byte[] d, uint l) 
        { }

        public virtual void encodeConditionalObject(id anObject) 
        {
            encodeObject(anObject);
        }

        public virtual void encodeObject(id anObject)
        { 
            encodeValueOfObjCType<id>(ref anObject);
        }


        public virtual void encodeObject(id anObject, NSString aKey)
        {
            encodeValueOfObjCType<id>(ref anObject);
        }

        public virtual void encodePropertyList(id plist)
        {
            id anObject = null;
            //anObject = plist ? (id)[NSSerializer serializePropertyList: plist] : nil;
            encodeValueOfObjCType<id>(ref anObject);
        }

        public virtual void encodePoint(ref NSPoint point)
        {
            encodeValueOfObjCType<NSPoint>(ref point);
        }

        public virtual void encodeRect(NSRect rect)
        {
            encodeValueOfObjCType<NSRect>(ref rect);
        }

        public virtual void encodeRootObject(id rootObject)
        {
            encodeObject(rootObject);
        }

        public virtual void encodeSize(NSSize size)
        {
            encodeValueOfObjCType<NSSize>(ref size);
        }

        public virtual void encodeValuesOfObjCTypes(params NSString[] types)
        {

        }

        public virtual void decodeArrayOfObjCType<T>(uint count, ref T[] array)
        { }


        public virtual object[] decodeBytesWithReturnedLength(ref int numBytes)
        {
            return null;
        }

        //- (id)decodeObject
        public virtual id decodeObject()
        {
            return null;
        }

        //- (id)decodePropertyList
        public virtual id decodePropertyList()
        {
            return null;
        }

        //- (NSPoint)decodePoint
        public virtual NSPoint decodePoint()
        {
            NSPoint point = new NSPoint();
            decodeValueOfObjCType<NSPoint>(ref point);
            return point;
        }


        public virtual NSRect decodeRect()
        {
            NSRect	rect = new NSRect();
            decodeValueOfObjCType<NSRect>(ref rect);
            return rect;
        }

        //- (NSSize)decodeSize
        public virtual NSSize decodeSize()
        {
            NSSize size = new NSSize();
            decodeValueOfObjCType<NSSize>(ref size);
            return size;
        }

        public virtual void decodeValuesOfObjCTypes(params string[] types)
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        // Keyed archiving extensions
        ////////////////////////////////////////////////////////////////////////////////////////////
        public virtual bool AllowsKeyedCoding
        {
            get { return allowsKeyedCoding(); }
        }

        public virtual bool allowsKeyedCoding()
        {
            return false;
        }

        public virtual bool containsValueForKey(NSString key)
        {
            return false;
        }

        public virtual bool decodeBoolForKey(NSString key)
        {
            return false;
        }

        public virtual byte[] decodeBytesForKey(NSString key, ref uint lengthp)
        {
            return null;
        }

        public virtual double decodeDoubleForKey(NSString key)
        {
            return 0;
        }

        public virtual float decodeFloatForKey(NSString key)
        {
            return 0;
        }

        public virtual int decodeIntForKey(NSString key)
        {
            return 0;
        }

        public virtual int decodeIntegerForKey(NSString key)
        {
            return 0;
        }

        public virtual Int32 decodeInt32ForKey(NSString key)
        {
            return 0;
        }

        public virtual Int64 decodeInt64ForKey(NSString key)
        {
            return 0;
        }

        public virtual id decodeObjectForKey(NSString key)
        {
            return null;
        }

        
        public virtual NSPoint decodePointForKey(NSString key)
        {
            return new NSPoint();
        }

        //- (NSRect)decodeRectForKey:(NSString *)key
        public virtual NSRect decodeRectForKey(NSString key)
        {
            return new NSRect();
        }



        //- (NSSize)decodeSizeForKey:(NSString *)key
        public virtual NSSize decodeSizeForKey(NSString key)
        {
            return new NSSize();
        }

        public virtual id decodePropertyListForKey(NSString key)
        {
            return null;
        }


        public virtual void encodeBoolForKey(bool aBool, NSString aKey)
        {

        }

        public virtual void encodeBytesForKey(byte[] bytes, uint length, NSString aKey)
        {

        }

        public virtual void encodeConditionalObjectForKey(id anObject, NSString aKey)
        {

        }

        public virtual void encodeDoubleForKey(double aDouble, NSString aKey)
        {
        }

        public virtual void encodeFloatForKey(float aDouble, NSString aKey)
        {
        }

        public virtual void encodeIntForKey(int aDouble, NSString aKey)
        {
        }

        public virtual void encodeIntegerForKey(int aDouble, NSString aKey)
        {
        }

        public virtual void encodeInt32ForKey(int aDouble, NSString aKey)
        {
        }

        public virtual void encodeInt64ForKey(int aDouble, NSString aKey)
        {
        }

        public virtual void encodeObjectForKey(id anObject, NSString aKey)
        {
        }
        
    }
}
