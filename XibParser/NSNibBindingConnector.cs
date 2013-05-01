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

    public class NSNibBindingConnector : NSNibConnector
    {
        public string Binding { get; set; }

        public string KeyPath { get; set; }

        public int NSNibBindingConnectorVersion { get; set; }

        public NSMutableDictionary Options { get; set; }

        public NSNibBindingConnector()
        {

        }


        public override id InitWithCoder(NSCoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            //////////////////////////////////////////////////////////////////////////////
            //<object class="NSNibBindingConnector" key="connector">
            //    <reference key="NSSource" ref="898315540"/>
            //    <reference key="NSDestination" ref="622487602"/>
            //    <string key="NSLabel">content: arrangedObjects</string>
            //    <string key="NSBinding">content</string>
            //    <string key="NSKeyPath">arrangedObjects</string>
            //    <int key="NSNibBindingConnectorVersion">2</int>
            //</object>
            //////////////////////////////////////////////////////////////////////////////

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.DecodeIntForKey("NSNibBindingConnectorVersion") != 2)
                {
                    return null;
                }

                Binding = (NSString)aDecoder.DecodeObjectForKey("NSBinding");
                KeyPath = (NSString)aDecoder.DecodeObjectForKey("NSKeyPath");
                Options = (NSMutableDictionary)aDecoder.DecodeObjectForKey("NSOptions");

            }
            else
            {

            }

            return this;
        }

    }



    
}
