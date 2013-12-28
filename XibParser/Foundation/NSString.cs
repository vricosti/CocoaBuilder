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

using AT.MIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

    public enum NSStringCompareOptions : uint
    {
        NSDefaultSearch = 0, // Doesn't exist in cocoa

        NSCaseInsensitiveSearch = 1,
        NSLiteralSearch = 2,
        NSBackwardsSearch = 4,
        NSAnchoredSearch = 8,
        NSNumericSearch = 64,
        NSDiacriticInsensitiveSearch = 128,
        NSWidthInsensitiveSearch = 256,
        NSForcedOrderingSearch = 512,
        NSRegularExpressionSearch = 1024
    }


    public class NSString : NSObject, INSNumber, IEquatable<NSString>
    {
        new public static Class Class = new Class(typeof(NSString));
        new public static NSString alloc() { return new NSString(); }
        new public static NSString allocWithZone(object zone) { return new NSString(); }

        private readonly string byteOrderMark = "\ufeff";
        private readonly string byteOrderMarkSwapped = "\ufffe";

        //private static const unichar byteOrderMark = 0xFEFF;
        //private static const unichar byteOrderMarkSwapped = 0xFFFE;


        protected static NSStringEncoding _DefaultStringEncoding;

        public string Value { get; set; }

        public override double doubleValue() { return Value.ToDouble(); }

        public override float floatValue() { return Value.ToFloat(); }

        public override int integerValue() { return Value.ToInt(); }

        public override int intValue() { return Value.ToInt(); }

        public override bool boolValue() 
        {
            //return Value.ConvertFromYesNo();
            uint len = length();

            if (len > 0)
            {
                uint index;
                for (index = 0; index < len; index++)
                {
                    Char c = this.characterAtIndex(index);

                    if (c > 'y')
                    {
                        break;
                    }
                    if (c.ToString().IndexOfAny(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'y', 'Y', 't', 'T' }) != -1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        static NSString() { initialize(); }

        static void initialize()
        {
            _DefaultStringEncoding = NSStringEncoding.NSASCIIStringEncoding;
        }

        static NSCharacterSet rPathSeps = null;
        static NSCharacterSet pathSeps()
        {
            if (rPathSeps == null)
            {
                rPathSeps = NSCharacterSet.characterSetWithCharactersInString("/\\");
            }
            return rPathSeps;
        }


        public NSString()
        {

        }


        public NSString(string value) : this()
        {
            Value = value;
        }

        public NSString mutableCopy()
        {
            return new NSMutableString(this.Value);
        }

		public NSString copy()
		{
			return new NSString (this.Value);
		}

        public static NSString String()
        {
            return new NSString("");
        }

        public static NSString stringWithContentsOfFile(NSString path)
        {
            return (NSString)alloc().initWithContentsOfFile(path);
        }


        

        public virtual id initWithContentsOfFile(NSString path)
        {
            id self = this;

            NSStringEncoding enc = _DefaultStringEncoding;
            NSData d = null;
            uint len = 0;
            byte[] data_bytes = null;

            d = (NSData)NSData.alloc().initWithContentsOfFile(path);
            if (d == null)
                return null;
            len = (uint)d.Length;
            if (len == 0)
            {
                return (NSString)@"";
            }
            data_bytes = d.bytes();
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

            self = initWithData(d,enc);

            return self;
        }

        public virtual NSString lowercaseString()
        {
            return new NSString(this.Value.ToLower());
        }

        private Encoding convertNSStringEncodingToCSharp(NSStringEncoding anEncoding)
        {
            Encoding encoding = Encoding.UTF8;

            switch (anEncoding)
            {
                case NSStringEncoding.NSASCIIStringEncoding:
                    encoding = Encoding.UTF7;
                    break;
                
                //case NSStringEncoding.NSNEXTSTEPStringEncoding:
                //    encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                //    NSLog.log("convertNSStringEncodingToCSharp %s", (NSString)"test");
                //    break;
                
                //case NSStringEncoding.NSJapaneseEUCStringEncoding:
                //    break;

                case NSStringEncoding.NSUTF8StringEncoding:
                    encoding = Encoding.UTF8;
                    break;
                default:
                    throw new NotImplementedException();
                    break;

            }

            return encoding;
        }


        public virtual id initWithData(NSData data, NSStringEncoding encoding)
        {
            return initWithBytes(data.bytes(), (uint)data.Length, encoding);
        }

        public virtual id initWithBytes(IntPtr ptr, uint length, NSStringEncoding anEncoding)
        {
            if (ptr == IntPtr.Zero)
                return null;

            byte[] buffer = new byte[length];
            Marshal.Copy(ptr, buffer, 0, buffer.Length);

            return initWithBytes(buffer, length, anEncoding);

        }
        public virtual id initWithBytes(byte[] bytes, uint length, NSStringEncoding anEncoding)
        {
            id self = this;

            if (length == 0)
            {
                Value = @"";
            }
            else
            {
                Encoding encoding = convertNSStringEncodingToCSharp(anEncoding);
                Value = encoding.GetString(bytes, 0, (int)length);
            }

            return self;
        }

        public virtual id initWithCString(char[] chars)
        {
            id self = this;

            string str = new string(chars);
            this.Value = str;

            return self;
        }

        public virtual id initWithFormat(NSString format, params object[] args)
        {
            id self = this;

            this.Value = Tools.sprintf(format, args);

            return self;
        }

        public virtual id initWithString(NSString aString)
        {
            id self = this;

            if (aString != null)
            {
                Value = aString.Value;
            }

            return self;
        }

        public override id initWithCoder(NSCoder aCoder)
        {
            id self = this;

            if (aCoder.AllowsKeyedCoding)
            {
                if (aCoder.containsValueForKey(@"NS.string"))
                {
                    throw new NotImplementedException("NSString:initWithCoder:");
                    //FIXME
                    //NSString str = (NSString)((NSKeyedUnarchiver)aCoder)._DecodePropertyListForKey(@"NS.string");
                    //self = initWithString(str);
                }
                else if (aCoder.containsValueForKey(@"NS.bytes"))
                {
                    id bytes = ((NSKeyedUnarchiver)aCoder).decodeObjectForKey(@"NS.bytes");
                     if (bytes.isKindOfClass(NSString.Class))
                     {
                         self = initWithString((NSString)bytes);
                     }
                     else
                     {
                         throw new NotImplementedException("NSString:initWithCoder");
                         // FIXME
                         //self = initWithData((NSData)bytes, );
                         //self = [self initWithData: (NSData*)bytes  encoding: NSUTF8StringEncoding];
                     }
                }
            }

            return self;
        }


        public virtual int caseInsensitiveCompare(NSString aString)
        {
            return this.Value.CompareTo(aString.Value);
        }

        public virtual Char characterAtIndex(uint index)
        {
            return this[index];
        }

        public char this[uint index] 
        {
            get
            {
                return this.Value[(int)index];
            }
        }

        public override NSString description()
        {
            NSString self = this;
            return self;
        }

        public override string ToString()
        {
            return this.Value;
        }

        // c# string
        public bool Contains(NSString aString)
        {
            return (this.Value != null) ? this.Value.Contains(aString) : false;
        }

        public NSString Replace(char oldChar, char newChar)
        {
            return (this.Value != null) ? this.Value.Replace(oldChar, newChar) : null;
        }

        public NSString Replace(NSString oldValue, NSString newValue)
        {
            return (this.Value != null) ? this.Value.Replace(oldValue, newValue) : null;
        }

        // objc string

		public virtual NSString stringByAppendingString(NSString aString)
		{
			return this.Value + aString.Value;
		}

        public virtual bool hasPrefix(NSString aString)
        {
            return this.Value.StartsWith(aString);
        }
         public virtual bool hasSuffix(NSString aString)
        {
            return this.Value.EndsWith(aString);
        }


        public virtual NSRange rangeOfCharacterFromSet(NSCharacterSet aSet)
        {
            NSRange all = new NSRange(0, length());
            return rangeOfCharacterFromSet(aSet, (NSStringCompareOptions)0, all);       
        }


        public virtual NSRange rangeOfCharacterFromSet(NSCharacterSet aSet, NSStringCompareOptions mask)
        {
            NSRange all = new NSRange(0, length());
            return rangeOfCharacterFromSet(aSet, mask, all);       
        }

        public virtual NSRange rangeOfCharacterFromSet(NSCharacterSet aSet, NSStringCompareOptions options, NSRange aRange)
        {
            int	i = 0;
            uint	start = 0;
            uint	stop = 0;
            int		step = 0;
            NSRange range = new NSRange();
            uint mask = (uint)options;

            i = (int)this.length();
            if ((mask & (uint)NSStringCompareOptions.NSBackwardsSearch) == (uint)NSStringCompareOptions.NSBackwardsSearch)
            {
                start = (aRange.Location + aRange.Length) - 1;
                stop = aRange.Location - 1; 
                step = -1;
            }
            else
            {
                start = aRange.Location;
                stop = (aRange.Location + aRange.Length); 
                step = 1;
            }
            range.Location = NS.NotFound;
            range.Length = 0;

            for (i = (int)start; i != stop; i += step)
            {
                Char character = characterAtIndex((uint)i);
                if (aSet.characterIsMember(character))
                {
                    range = new NSRange((uint)i, 1);
                    break;
                }
            }

            return range;
        }


        private bool pathSepMember(Char character)
        {
            return (character == '\\' || character == '/');
        }

        public virtual NSString pathExtension()
        {
            return (NSString)System.IO.Path.GetExtension(this.Value);
        }

        public virtual NSString lastPathComponent()
        {
            uint l = this.length();
            NSRange range;
            uint i = 0;

            if (l == 0)
                return @"";

            ///////////////////////////////////////////////////////////////////////////////
            // Warning we don't handle the case "scratch///" the should return "scratch"
            // TODO : do not use GetPathRoot and implements rootOf(this, l);
            ///////////////////////////////////////////////////////////////////////////////
            string pathRoot = System.IO.Path.GetPathRoot(this.Value);
            i = (uint)pathRoot.Length - 1;
            while (l > i && pathSepMember(this[l - 1]) ==  true)
            {
                l--;
            }

            if (i == l)
            {
                if (characterAtIndex(0) == '~' && pathSepMember(characterAtIndex(i - 1)) == true)
                {
                     return substringToIndex(i-1);
                }
                return substringToIndex(i);
            }

            range = rangeOfCharacterFromSet(pathSeps(), NSStringCompareOptions.NSBackwardsSearch, new NSRange(i, l - i));
            if (range.Length > 0)
            {
                // Found separator ... adjust to point to component.
                i = range.Location + range.Length;
            }

            return substringWithRange(new NSRange(i, l-i));
        }


        public virtual NSString stringByDeletingLastPathComponent()
        {
            uint length = 0;

            length = this.length();
            if (length == 0)
            {
                return @"";
            }



            return null;
        }

         public virtual NSString stringByDeletingPathExtension()
        {
            return (NSString)System.IO.Path.GetFileNameWithoutExtension(this.Value);
        }

         public virtual NSString stringByAppendingPathExtension(NSString anExtension)
         {
             return System.IO.Path.Combine(this.Value, anExtension.Value);
         }

        public virtual NSString stringByAppendingPathComponent(NSString aString)
        {
            NSString path = @"";

            if (aString != null && aString.Value != null)
            {
                path = System.IO.Path.Combine(Value, aString);
            }

            return path;
        }

        public virtual uint length()
        {
            return (Value != null) ? (uint)Value.Length : 0;
        }
        
        //public uint Length
        //{
        //    get { return (Value != null) ? (uint)Value.Length : 0; } 
        //}


        public virtual NSData dataUsingEncoding(NSStringEncoding encoding, bool flag = false)
        {
            NSData data = null;
            byte[] buffer = null;

            if (encoding == NSStringEncoding.NSUTF8StringEncoding)
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(this.Value);
            }
            else
            {
                buffer = System.Text.Encoding.ASCII.GetBytes(this.Value);
            }

            data = (NSData)NSData.alloc().initWithBytes(buffer);
            
            //byte[] ntbytes = new byte[data.Length + 1];
            //array.copy(data, 0, ntbytes, 0, data.Length);
            //nullTerminatedData = NSData.alloc().initWithBytes(ntbytes);

            return data;
        }


        public static NSString stringWithFormat(NSString format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return (NSString)NSString.alloc().initWithFormat(format, args);
        }


        public virtual NSRange rangeOfString(NSString aString)
        {
            NSRange aRange = new NSRange(0, (aString != null) ? aString.length() : 0);
            return this.rangeOfString(aString, NSStringCompareOptions.NSDefaultSearch, aRange, null);
        }

        
        public virtual NSRange rangeOfString(NSString aString, NSStringCompareOptions mask)
        {
            NSRange aRange = new NSRange(0, (aString != null) ? aString.length() : 0);
            return this.rangeOfString(aString, mask, aRange, null);
        }

        public virtual NSRange rangeOfString(NSString aString, NSStringCompareOptions mask, NSRange aRange)
        {
            return this.rangeOfString(aString, mask, aRange, null);
        }

        public virtual NSRange rangeOfString(NSString aString, NSStringCompareOptions mask, NSRange aRange, NSLocale locale)
        {
            if (aString == null)
                throw new Exception("NSInvalidArgumentException");
            if (aString == "")
                return NSRange.NotFound;

            int idx = this.Value.IndexOf(aString);
            if (idx == -1)
                return NSRange.NotFound;
            else
                return new NSRange((uint)idx, aString.length());
        }

        public virtual NSString substringWithRange(NSRange aRange)
        {
            if (aRange.Location < 0 || aRange.Location > this.Value.Length - 1)
                throw new IndexOutOfRangeException();
            if (aRange.Length == 0)
                return @"";

            return this.Value.Substring((int)aRange.Location, (int)aRange.Length);
        }

        public NSString substringToIndex(uint anIndex)
        {
            NSString str = null;

            if (anIndex < 0 || anIndex > this.length() - 1)
                throw new ArgumentNullException();

            if (this.Value != null)
            {
                str = (NSString)this.Value.Substring((int)anIndex);
            }

            return str;
        }

        public NSString substringFromIndex(uint anIndex)
        {
            NSString str = "";

            if (anIndex < 0 || anIndex > this.length() - 1)
                throw new ArgumentNullException();

            if (this.Value != null)
            {
                str = (NSString)this.Value.Substring((int)anIndex);
            }

            return str;
        }


        public IntPtr UTF8String()
        {
            IntPtr ptr = IntPtr.Zero;

            if (this.Value != null && this.Value.Length > 0)
            {
                throw new NotImplementedException();
            }

            return ptr;
        }


        public NSString uppercaseString()
        {
            NSString str = null;

            if (this.Value != null)
            {
                str = (NSString)this.Value.ToUpper();
            }

            return str;
        }

        [ObjcMethod("hash")]
        public virtual uint Hash()
        {
            return (uint)GetHashCode();
           
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

		public static NSString FromPoint(NSPoint aPoint)
		{
			//setupCache();
			//if (GSMacOSXCompatibleGeometry() == YES)
				return NSString.stringWithFormat(
				        @"{%g, %g}", aPoint.X, aPoint.Y);
			//else
			//	return [NSStringClass stringWithFormat:
			//	        @"{x = %g; y = %g}", aPoint.x, aPoint.y];
		}

        // implicit NSString to string conversion operator
        public static implicit operator NSSize(NSString nsString)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSString: implicit conversion to NSSize occurred");
#endif
            return NSSize.Create(nsString.Value);
        }

		public static NSString FromSize(NSSize aSize)
		{
		//setupCache();
		//if (GSMacOSXCompatibleGeometry() == YES)
				return NSString.stringWithFormat(
			        @"{%g, %g}", aSize.Width, aSize.Height);
//		else
//				return NSString.stringWithFormat(
//			        @"{width = %g; height = %g}", aSize.Width, aSize.Height);
		}

		public static NSString FromRect(NSRect aRect)
		{
			//setupCache();
			//if (GSMacOSXCompatibleGeometry() == YES)
				return NSString.stringWithFormat(
				        @"{{%g, %g}, {%g, %g}}",
				aRect.Origin.X, aRect.Origin.Y, aRect.Size.Width, aRect.Size.Height);
//			else
//				return [NSStringClass stringWithFormat:
//				        @"{x = %g; y = %g; width = %g; height = %g}",
//				        aRect.origin.x, aRect.origin.y, aRect.size.width, aRect.size.height]; 
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
