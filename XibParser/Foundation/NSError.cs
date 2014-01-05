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
    public class NSError : NSObject
    {
        new public static Class Class = new Class(typeof(NSError));
        new public static NSError alloc() { return new NSError(); }

        public NSError()
        {

        }
        public static void errorWithLocalizedDescription(NSString localizedDescription, int errCode, object qs)
        {

        }

        public static NSError errorWithDomain(NSString domain, int code, NSDictionary userInfo)
        {
            return (NSError)NSError.alloc().initWithDomain(domain, code, userInfo);
        }
        public virtual id initWithDomain(NSString domain, int code, NSDictionary userInfo)
        {
            id self = this;



            return this;
        }

        
    }
}
