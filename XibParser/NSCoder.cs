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

        public abstract bool ContainsValueForKey(NSString key);

        public abstract void DecodeArrayOfObjCType(NSString itemType, int count, ref object address);

        public abstract bool DecodeBoolForKey(NSString key);

        //- (const uint8_t *)decodeBytesForKey:(NSString *)key returnedLength:(NSUInteger *)lengthp
        public abstract byte[] DecodeBytesForKey(NSString key, ref int lengthp);

        //- (void *)decodeBytesWithReturnedLength:(NSUInteger *)numBytes
        public abstract object[] DecodeBytesWithReturnedLength(ref int numBytes);

        //- (NSData *)decodeDataObject
        //public abstract NSData();

        //- (double)decodeDoubleForKey:(NSString *)key
        public abstract double DecodeDoubleForKey(NSString key);

        //- (float)decodeFloatForKey:(NSString *)key
        public abstract float DecodeFloatForKey(NSString key);

        //- (int32_t)decodeInt32ForKey:(NSString *)key
        public abstract Int32 DecodeInt32ForKey(NSString key);

        //- (int64_t)decodeInt64ForKey:(NSString *)key
        public abstract Int64 DecodeInt64ForKey(NSString key);

        //- (NSInteger)decodeIntegerForKey:(NSString *)key
        public abstract int DecodeIntegerForKey(NSString key);

        //- (int)decodeIntForKey:(NSString *)key
        public abstract int DecodeIntForKey(NSString key);

        //- (id)decodeObject
        public abstract id DecodeObject();

        //- (id)decodeObjectForKey:(NSString *)key
        public abstract id DecodeObjectForKey(NSString key);

        //- (id)decodeObjectOfClass:(Class)aClass forKey:(NSString *)key
        public abstract object DecodeObjectOfClass(Type type, NSString key);

        //- (id)decodeObjectOfClasses:(NSSet *)classes forKey:(NSString *)key

        //- (NSPoint)decodePoint
        public abstract NSPoint DecodePoint();

        //- (NSPoint)decodePointForKey:(NSString *)key
        public abstract NSPoint DecodePointForKey(NSString key);

        //- (id)decodePropertyList
        public abstract object DecodePropertyList();

        //- (id)decodePropertyListForKey:(NSString *)key
        public abstract object DecodePropertyListForKey(NSString key);

        //- (NSRect)decodeRect
        public abstract NSRect DecodeRect();

        //- (NSRect)decodeRectForKey:(NSString *)key
        public abstract NSRect DecodeRectForKey(NSString key);

        //- (NSSize)decodeSize
        public abstract NSSize DecodeSize();

        //- (NSSize)decodeSizeForKey:(NSString *)key
        public abstract NSSize DecodeSizeForKey(NSString key);

        //- (void)decodeValueOfObjCType:(const char *)valueType at:(void *)data
        public abstract void DecodeValueOfObjCType(NSString valueType, ref object data);

        public abstract void DecodeValueOfObjCType<T>(NSString valueType, ref T data);
    }
}
