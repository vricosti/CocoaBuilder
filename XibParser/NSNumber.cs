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
    public class NSNumber : NSObject, NSCoding2, INSNumber, IEquatable<NSNumber>
    {
        new public static Class Class = new Class(typeof(NSNumber));

        protected object _number;

        

        [ObjcPropAttribute("DoubleValue", SetName = null)]
        public double DoubleValue { get { return _number.ToDouble(); } }

        [ObjcPropAttribute("FloatValue", SetName = null)]
        public float FloatValue { get { return _number.ToFloat(); } }

        [ObjcPropAttribute("IntegerValue", SetName = null)]
        public int IntegerValue { get { return _number.ToInt(); } }

        [ObjcPropAttribute("IntValue", SetName = null)]
        public int IntValue { get { return _number.ToInt(); } }

        [ObjcPropAttribute("BoolValue", SetName = null)]
        public bool BoolValue { get { return _number.ToBool(); } }

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



         [ObjcMethodAttribute("EncodeWithCoder")]
        public override void EncodeWithCoder(NSObjectDecoder aCoder)
        {
            base.EncodeWithCoder(aCoder);
        }

        [ObjcMethodAttribute("InitWithCoder")]
        public override id InitWithCoder(NSCoder decoder)
        {
            base.InitWithCoder(decoder);

            //_number = decoder.XmlElement.Value;

            return this;
        }

        [ObjcMethodAttribute("NumberWithBool")]
        public static NSNumber NumberWithBool(bool aBool)
        {
            return new NSNumber(aBool);
        }

        [ObjcMethodAttribute("NumberWithInt")]
        public static NSNumber NumberWithInt(int aInt)
        {
            return new NSNumber(aInt);
        }

        [ObjcMethodAttribute("NumberWithInteger")]
        public static NSNumber NumberWithInteger(int aInt)
        {
            return new NSNumber(aInt);
        }

        [ObjcMethodAttribute("NumberWithDouble")]
        public static NSNumber NumberWithDouble(double aDouble)
        {
            return new NSNumber(aDouble);
        }

        [ObjcMethodAttribute("NumberWithFloat")]
        public static NSNumber NumberWithFloat(float aFloat)
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
