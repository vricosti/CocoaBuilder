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
    public class NSString : NSObject
    {
        //public override string ClassName  { get { return "NSString"; } }


        public string Value { get; set; }

        public NSString()
        {

        }


        public NSString(string value) : this()
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }


        public static implicit operator NSString(string csString)
        {
            return new NSString(csString);
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
