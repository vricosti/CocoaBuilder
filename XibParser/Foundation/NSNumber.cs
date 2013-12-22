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
    public class NSNumber : NSObject, NSCoding2, IEquatable<NSNumber>
    {
        new public static Class Class = new Class(typeof(NSNumber));

        protected object _number;

        
        public override double doubleValue() { return _number.ToDouble(); }

        public override float floatValue() { return _number.ToFloat(); }

        public override int integerValue() { return _number.ToInt(); }

        public override int intValue() { return _number.ToInt(); }

        public override bool boolValue() { return _number.ToBool(); }

        public NSNumber()
        {
        }

        public NSNumber(bool boolean) 
            : this()
        {
            _number = boolean;
        }

        public NSNumber(Int32 integer)
            : this()
        {
            _number = integer;
        }

        public NSNumber(UInt32 integer)
            : this()
        {
            _number = integer;
        }

        public NSNumber(double aDouble)
        {
            _number = aDouble;
        }

        public NSNumber(float aFloat)
        {
            _number = aFloat;
        }

        # region IEquatable

        public override bool Equals(object obj)
        {
            return Equals(obj as NSNumber);
        }

        public bool Equals(NSNumber obj)
        {
            return obj != null && obj._number.Equals(this._number);
        }

        #endregion //IEquatable

        public override int GetHashCode()
        {
            return (_number != null) ? _number.GetHashCode() : base.GetHashCode();
        }



         [ObjcMethodAttribute("encodeWithCoder")]
        public override void encodeWithCoder(NSCoder aCoder)
        {
            base.encodeWithCoder(aCoder);
        }

        [ObjcMethodAttribute("initWithCoder")]
        public override id initWithCoder(NSCoder decoder)
        {
            base.initWithCoder(decoder);

            //_number = decoder.XmlElement.Value;

            return this;
        }

        [ObjcMethodAttribute("numberWithBool")]
        public static NSNumber numberWithBool(bool aBool)
        {
            return new NSNumber(aBool);
        }

        [ObjcMethodAttribute("numberWithInt")]
        public static NSNumber numberWithInt(int aInt)
        {
            return new NSNumber(aInt);
        }

        [ObjcMethodAttribute("numberWithInteger")]
        public static NSNumber numberWithInteger(int aInt)
        {
            return new NSNumber(aInt);
        }

        [ObjcMethodAttribute("numberWithDouble")]
        public static NSNumber numberWithDouble(double aDouble)
        {
            return new NSNumber(aDouble);
        }

        [ObjcMethodAttribute("numberWithFloat")]
        public static NSNumber numberWithFloat(float aFloat)
        {
            return new NSNumber(aFloat);
        }

        // implicit NSNumber to int conversion operator

        public static implicit operator Nullable<Int32>(NSNumber nsNumber)
        {
            if (nsNumber == null)
                return null;
            else
                return Convert.ToInt32(nsNumber._number);
        }

        public static implicit operator Int32(NSNumber nsNumber)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSNumber: implicit int conversion occurred");
#endif
            if (nsNumber._number.GetType() != typeof(Int32) &&
                nsNumber._number.GetType() != typeof(UInt32))
                throw new Exception("Invalid cast");

            return Convert.ToInt32(nsNumber._number);
        }


        public static implicit operator Nullable<UInt32>(NSNumber nsNumber)
        {
            if (nsNumber == null)
                return null;
            else
                return Convert.ToUInt32(nsNumber._number);
        }

        public static implicit operator UInt32(NSNumber nsNumber)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSNumber: implicit int conversion occurred");
#endif
            if (nsNumber._number.GetType() != typeof(Int32) &&
                nsNumber._number.GetType() != typeof(UInt32))
                throw new Exception("Invalid cast");

            return Convert.ToUInt32(nsNumber._number);
        }


        // implicit NSNumber to bool conversion operator
        public static implicit operator bool(NSNumber nsNumber)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSNumber: implicit bool conversion occurred");
#endif
            if (nsNumber._number.GetType() != typeof(bool))
                throw new Exception("Invalid cast");

            return Convert.ToBoolean(nsNumber._number);
        }







       
    }
}
