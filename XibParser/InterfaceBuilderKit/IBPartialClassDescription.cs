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
    public class IBPartialClassDescription : NSObject
    {
        public string ClassName { get; set; }

        public string SuperClassName { get; set; }

        public NSMutableDictionary Actions { get; set; }

        public NSMutableDictionary ActionInfosByName { get; set; }

        public NSMutableDictionary Outlets { get; set; }

        public NSMutableDictionary ToOneOutletInfosByName { get; set; }

        public IBClassDescriptionSource SourceIdentifier { get; set; }


        public IBPartialClassDescription()
        {

        }

        //<object class="IBPartialClassDescription">
        //<string key="className">ComposeController</string>
        //            <string key="superclassName">NSWindowController</string>
        //            <object class="NSMutableDictionary" key="actions">
        //                <bool key="EncodedWithXMLCoder">YES</bool>
        //                <object class="NSArray" key="dict.sortedKeys">
        //                    <bool key="EncodedWithXMLCoder">YES</bool>
        //                    <string>attachmentUploaded:</string>
        //                    <string>saveAsDraftButtonClickedAction:</string>
        //                    <string>sendButtonClickedAction:</string>
        //                    <string>toogleFormatBar:</string>
        //                </object>
        //                <object class="NSMutableArray" key="dict.values">
        //                    <bool key="EncodedWithXMLCoder">YES</bool>
        //                    <string>id</string>
        //                    <string>id</string>
        //                    <string>id</string>
        //                    <string>id</string>
        //                </object>
        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            base.initWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                ClassName = (NSString)aDecoder.decodeObjectForKey("className");
                SuperClassName = (NSString)aDecoder.decodeObjectForKey("superclassName");
                Actions = (NSMutableDictionary)aDecoder.decodeObjectForKey("actions");
                ActionInfosByName = (NSMutableDictionary)aDecoder.decodeObjectForKey("actionInfosByName");
                Outlets = (NSMutableDictionary)aDecoder.decodeObjectForKey("outlets");
                ToOneOutletInfosByName = (NSMutableDictionary)aDecoder.decodeObjectForKey("toOneOutletInfosByName");
                SourceIdentifier = (IBClassDescriptionSource)aDecoder.decodeObjectForKey("sourceIdentifier");

            }

            return self;
        }

       
       
    }
}
