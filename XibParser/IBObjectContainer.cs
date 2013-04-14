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

    public class IBObjectContainer : NSObject
    {
        public NSMutableArray ConnectionRecords { get; set; }

        public IBMutableOrderedSet ObjectRecords { get; set; }

        public NSMutableDictionary FlattenedProperties { get; set; }

        public NSMutableDictionary UnlocalizedProperties { get; set; }

        public object ActiveLocalization { get; set; }

        public NSMutableDictionary Localizations { get; set; }

        public object SourceID { get; set; }

        public int? MaxId { get; set; }


        public IBObjectContainer()
        {
            ConnectionRecords = new NSMutableArray();
            ObjectRecords = new IBMutableOrderedSet();
            FlattenedProperties = new NSMutableDictionary();
            UnlocalizedProperties = new NSMutableDictionary();
            ActiveLocalization = null;
            Localizations = new NSMutableDictionary();
        }

        public override NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {
            if (aDecoder.AllowsKeyedCoding)
            {
                ConnectionRecords = (NSMutableArray)aDecoder.DecodeObjectForKey("connectionRecords");
                ObjectRecords = (IBMutableOrderedSet)aDecoder.DecodeObjectForKey("objectRecords");
                FlattenedProperties = (NSMutableDictionary)aDecoder.DecodeObjectForKey("flattenedProperties");
                UnlocalizedProperties = (NSMutableDictionary)aDecoder.DecodeObjectForKey("unlocalizedProperties");
                Localizations = (NSMutableDictionary)aDecoder.DecodeObjectForKey("localizations");
                SourceID = aDecoder.DecodeObjectForKey("sourceID");
                MaxId = aDecoder.DecodeIntForKey("maxID");
            }


            return this;
        }



    }
}
