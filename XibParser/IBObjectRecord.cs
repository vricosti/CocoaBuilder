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
    public class IBObjectRecord : NSObject
    {
        public int ObjectID { get; set; }

        public object Object { get; set; }

        public object Children { get; set; }

        public object Parent { get; set; }

        public string ObjectName { get; set; }

        public IBObjectRecord()
        {

        }

        public override NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {

            if (aDecoder.AllowsKeyedCoding)
            {
                ObjectID = aDecoder.DecodeIntForKey("objectID");
                Object = aDecoder.DecodeObjectForKey("object");
                Children = aDecoder.DecodeObjectForKey("children");
                Parent = aDecoder.DecodeObjectForKey("parent");
                ObjectName = (NSString)aDecoder.DecodeObjectForKey("objectName");

            }

            return this;
        }



    }
}
