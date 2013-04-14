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
        public abstract bool AllowsKeyedCoding { get; }

        public abstract bool ContainsValueForKey(string key);

        public abstract void DecodeArrayOfObjCType(string itemType, int count, ref object address);

        public abstract bool DecodeBoolForKey(string key);

        //- (const uint8_t *)decodeBytesForKey:(NSString *)key returnedLength:(NSUInteger *)lengthp
        public abstract byte[] DecodeBytesForKey(string key, ref int lengthp);

        //- (void *)decodeBytesWithReturnedLength:(NSUInteger *)numBytes
        public abstract object[] DecodeBytesWithReturnedLength(ref int numBytes);

        //- (NSData *)decodeDataObject
        //public abstract NSData();

        //- (double)decodeDoubleForKey:(NSString *)key
        public abstract bool DecodeDoubleForKey(string key);

        //- (float)decodeFloatForKey:(NSString *)key
        public abstract bool DecodeFloatForKey(string key);

        //- (int32_t)decodeInt32ForKey:(NSString *)key
        public abstract Int32 decodeInt32ForKey(string key);

        //- (int64_t)decodeInt64ForKey:(NSString *)key
        public abstract Int64 DecodeInt64ForKey(string key);

        //- (NSInteger)decodeIntegerForKey:(NSString *)key
        public abstract int DecodeIntegerForKey(string key);

        //- (int)decodeIntForKey:(NSString *)key
        public abstract int DecodeIntForKey(string key);

        //- (id)decodeObject
        public abstract object DecodeObject(string key);

        //- (id)decodeObjectForKey:(NSString *)key
        public abstract object DecodeObjectForKey(string key);

        //- (id)decodeObjectOfClass:(Class)aClass forKey:(NSString *)key
        public abstract object DecodeObjectOfClass(Type type, string key);

        //- (id)decodeObjectOfClasses:(NSSet *)classes forKey:(NSString *)key

        //- (NSPoint)decodePoint
        public abstract NSPoint DecodePoint();

        //- (NSPoint)decodePointForKey:(NSString *)key
        public abstract NSPoint DecodePointForKey(string key);

        //- (id)decodePropertyList
        public abstract object DecodePropertyList();

        //- (id)decodePropertyListForKey:(NSString *)key
        public abstract object DecodePropertyListForKey(string key);

        //- (NSRect)decodeRect
        public abstract NSRect DecodeRect();

        //- (NSRect)decodeRectForKey:(NSString *)key
        public abstract NSRect DecodeRectForKey(string key);

        //- (NSSize)decodeSize
        public abstract NSSize DecodeSize();

        //- (NSSize)decodeSizeForKey:(NSString *)key
        public abstract NSSize DecodeSizeForKey(string key);

        //- (void)decodeValueOfObjCType:(const char *)valueType at:(void *)data
        public abstract void decodeValueOfObjCType(string valueType, ref object data);




    }
}
