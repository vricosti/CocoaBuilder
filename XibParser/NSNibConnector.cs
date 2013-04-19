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
    public class NSNibConnector : NSObject, NSCoding
    {
        public /*id*/object Source { get; set; }

        public /*id*/object Destination { get; set; }

        public string Label { get; set; }

        public NSNibConnector()
        {

        }


        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {
            //base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                Source = aDecoder.DecodeObjectForKey("NSSource");
                Destination = aDecoder.DecodeObjectForKey("NSDestination");
                Label = (NSString)aDecoder.DecodeObjectForKey("NSLabel");
            }
            else
            {
                
            }

            return this;
        }

    }
}
