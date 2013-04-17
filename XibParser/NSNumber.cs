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
    public class NSNumber : NSObject, NSCoding, INSNumber
    {
        private object _number;

        [ObjcPropAttribute("IntValue", SetName=null)]
        public int IntValue
        {
            get 
            {
                return Convert.ToInt32(_number); 
            }
        }

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

         [ObjcMethodAttribute("EncodeWithCoder")]
        public override void EncodeWithCoder(NSObjectDecoder aCoder)
        {
            base.EncodeWithCoder(aCoder);
        }

        [ObjcMethodAttribute("InitWithCoder")]
        public override NSObject InitWithCoder(NSObjectDecoder decoder)
        {
            base.InitWithCoder(decoder);

            _number = decoder.XmlElement.Value;

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
