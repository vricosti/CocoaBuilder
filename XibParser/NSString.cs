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
    public enum NSStringEncoding : uint
    {
        NSASCIIStringEncoding = 1,
        NSNEXTSTEPStringEncoding = 2,
        NSJapaneseEUCStringEncoding = 3,
        NSUTF8StringEncoding = 4,
        NSISOLatin1StringEncoding = 5,
        NSSymbolStringEncoding = 6,
        NSNonLossyASCIIStringEncoding = 7,
        NSShiftJISStringEncoding = 8,
        NSISOLatin2StringEncoding = 9,
        NSUnicodeStringEncoding = 10,
        NSWindowsCP1251StringEncoding = 11,
        NSWindowsCP1252StringEncoding = 12,
        NSWindowsCP1253StringEncoding = 13,
        NSWindowsCP1254StringEncoding = 14,
        NSWindowsCP1250StringEncoding = 15,
        NSISO2022JPStringEncoding = 21,
        NSMacOSRomanStringEncoding = 30,
        NSUTF16StringEncoding = NSUnicodeStringEncoding,
        NSUTF16BigEndianStringEncoding = 0x90000100,
        NSUTF16LittleEndianStringEncoding = 0x94000100,
        NSUTF32StringEncoding = 0x8c000100,
        NSUTF32BigEndianStringEncoding = 0x98000100,
        NSUTF32LittleEndianStringEncoding = 0x9c000100,
        NSProprietaryStringEncoding = 65536
    }


    public class NSString : NSObject, INSNumber, IEquatable<NSString>
    {
        new public static Class Class = new Class(typeof(NSString));
        new public static NSString Alloc() { return new NSString(); }

        private readonly string byteOrderMark = "\ufeff";
        private readonly string byteOrderMarkSwapped = "\ufffe";

        //private static const unichar byteOrderMark = 0xFEFF;
        //private static const unichar byteOrderMarkSwapped = 0xFFFE;


        protected static NSStringEncoding _DefaultStringEncoding;

        public string Value { get; set; }

        [ObjcPropAttribute("DoubleValue", SetName = null)]
        public double DoubleValue { get  { return Value.ToDouble(); } }

        [ObjcPropAttribute("FloatValue", SetName = null)]
        public float FloatValue { get { return Value.ToFloat(); } }
        
        [ObjcPropAttribute("IntValue", SetName = null)]
        public int IntValue { get { return Value.ToInt(); } }
        
        [ObjcPropAttribute("IntegerValue", SetName = null)]
        public int IntegerValue { get { return Value.ToInt(); } }

        [ObjcPropAttribute("IntegerValue", SetName = null)]
        public bool BoolValue { get { return Value.ToBool(); } }


        static NSString() { Initialize(); }

        new static void Initialize()
        {
            _DefaultStringEncoding = NSStringEncoding.NSASCIIStringEncoding;
        }



        public NSString()
        {

        }


        public NSString(string value) : this()
        {
            Value = value;
        }

        public static NSString StringWithContentsOfFile(NSString path)
        {
            return (NSString)Alloc().InitWithContentsOfFile(path);
        }

        public virtual id InitWithContentsOfFile(NSString path)
        {
            id self = this;

            NSStringEncoding enc = _DefaultStringEncoding;
            NSData d = null;
            uint len = 0;
            byte[] data_bytes = null;

            d = NSData.Alloc().InitWithContentsOfFile(path);
            if (d == null)
                return null;
            len = (uint)d.Length;
            if (len == 0)
            {
                return (NSString)@"";
            }
            data_bytes = d.Bytes;
            if (data_bytes != null && len >= 2)
            {
                string data_ucs2chars = Encoding.UTF8.GetString(data_bytes);
                if (data_ucs2chars.StartsWith(byteOrderMark) || 
                    data_ucs2chars.StartsWith(byteOrderMarkSwapped))
                {
                    enc = NSStringEncoding.NSUnicodeStringEncoding;
                }
                else if (len >= 3
                    && data_bytes[0] == 0xEF
                    && data_bytes[1] == 0xBB
                    && data_bytes[2] == 0xBF)
                {
                    enc = NSStringEncoding.NSUTF8StringEncoding;
                }
            }

            self = InitWithData(d,enc);

            return self;
        }

        public virtual id InitWithData(NSData data, NSStringEncoding encoding)
        {
            return InitWithBytes(data.Bytes, (uint)data.Length, encoding);
        }

        public virtual id InitWithBytes(byte[] bytes, uint length, NSStringEncoding encoding)
        {
            id self = this;

            if (length == 0)
            {
                Value = @"";
            }
            else
            {
                Value = Encoding.UTF8.GetString(bytes, 0, (int)length);
            }

            return self;
        }


        public virtual id InitWithString(NSString aString)
        {
            id self = this;

            if (aString != null)
            {
                Value = aString.Value;
            }

            return self;
        }

        public override id InitWithCoder(NSCoder aCoder)
        {
            id self = this;

            if (aCoder.AllowsKeyedCoding)
            {
                if (aCoder.ContainsValueForKey(@"NS.string"))
                {
                    throw new NotImplementedException("NSString:InitWithCoder:");
                    //FIXME
                    //NSString str = (NSString)((NSKeyedUnarchiver)aCoder)._DecodePropertyListForKey(@"NS.string");
                    //self = InitWithString(str);
                }
                else if (aCoder.ContainsValueForKey(@"NS.bytes"))
                {
                    id bytes = ((NSKeyedUnarchiver)aCoder).DecodeObjectForKey(@"NS.bytes");
                     if (bytes.IsKindOfClass(NSString.Class))
                     {
                         self = InitWithString((NSString)bytes);
                     }
                     else
                     {
                         throw new NotImplementedException("NSString:InitWithCoder");
                         // FIXME
                         //self = InitWithData((NSData)bytes, );
                         //self = [self initWithData: (NSData*)bytes  encoding: NSUTF8StringEncoding];
                     }
                }
            }

            return self;
        }




        public override string ToString()
        {
            return Value;
        }

        // c# string
        public bool Contains(NSString aString)
        {
          return (Value != null) ? Value.Contains(aString) : false;
        }

        public NSString Replace(char oldChar, char newChar)
        {
            return (Value != null) ? Value.Replace(oldChar, newChar) : null;
        }

        public NSString Replace(NSString oldValue, NSString newValue)
        {
            return (Value != null) ? Value.Replace(oldValue, newValue) : null;
        }

        // objc string
        public uint Length
        {
            get { return (Value != null) ? (uint)Value.Length : 0; } 
        }


        public virtual NSData DataUsingEncoding(NSStringEncoding encoding, bool flag = false)
        {
            return null;
        }


        public static NSString StringWithFormat(NSString format, params object[] args)
        {
            NSString str = new NSString();

            if (format == null)
                throw new ArgumentNullException("format");


            return str;
        }


        public NSRange RangeOfString(NSString aString)
        {
            if (aString == null)
                throw new ArgumentNullException("aString");

            int idx = this.Value.IndexOf(aString);
            if (idx == -1)
                return new NSRange(0, 0);
            else
                return new NSRange((uint)idx, aString.Length);
        }

        public NSString SubstringToIndex(uint anIndex)
        {
            NSString str = null;

            if (anIndex < 0 || anIndex > this.Length - 1)
                throw new ArgumentNullException();

            if (this.Value != null)
            {
                str = (NSString)this.Value.Substring((int)anIndex);
            }

            return str;
        }

        public NSString SubstringFromIndex(uint anIndex)
        {
            NSString str = "";

            if (anIndex < 0 || anIndex > this.Length - 1)
                throw new ArgumentNullException();

            if (this.Value != null)
            {
                str = (NSString)this.Value.Substring((int)anIndex);
            }

            return str;
        }



        public NSString UppercaseString()
        {
            NSString str = null;

            if (this.Value != null)
            {
                str = (NSString)this.Value.ToUpper();
            }

            return str;
        }



        # region IEquatable

        public override bool Equals(object obj)
        {
            return Equals(obj as NSString);
        }

        public bool Equals(NSString obj)
        {
            return obj != null && obj.Value.Equals(this.Value);
        }

        #endregion //IEquatable

        public override int GetHashCode()
        {
            return (Value != null) ? Value.GetHashCode() : base.GetHashCode();
        }


        public static implicit operator NSString(string csString)
        {
            return (csString != null) ? new NSString(csString) : null;
        }

        // implicit NSString to string conversion operator
        public static implicit operator string(NSString nsString)  
        {
            if (nsString == null)
                return null;
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSString: implicit conversion to string occurred");
#endif
            return nsString.Value;  // implicit conversion
        }


        public static implicit operator NSPoint(NSString nsString)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSString: implicit conversion to NSPoint occurred");
#endif
            return NSPoint.Create(nsString.Value);
        }

        // implicit NSString to string conversion operator
        public static implicit operator NSSize(NSString nsString)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSString: implicit conversion to NSSize occurred");
#endif
            return NSSize.Create(nsString.Value);
        }

        // implicit NSString to string conversion operator
        public static implicit operator NSRect(NSString nsString)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSString: implicit conversion to NSRect occurred");
#endif
            return NSRect.Create(nsString.Value); 
        }

       

    }
}
