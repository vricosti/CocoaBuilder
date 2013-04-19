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
using System.Xml.Linq;

namespace Smartmobili.Cocoa
{
    public class NSCustomResource : NSObject
    {
        private NSString _className;
        private NSString _resourceName;

        public NSString ClassName 
        { 
            get { return _className; } 
            set { _className = value; } 
        }

        public NSString ResourceName
        { 
            get { return _resourceName; } 
            set { _resourceName = value; } 
        }

        public NSCustomResource()
        {

        }

        //public NSCustomResource(NSObjectDecoder aDecoder)
        //    : base(aDecoder)
        //{
            
        //}

        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {

            NSObject realObject = null;
            if (aDecoder.AllowsKeyedCoding)
            {
                _className = (NSString)aDecoder.DecodeObjectForKey("NSClassName");
                _resourceName = (NSString)aDecoder.DecodeObjectForKey("NSResourceName");

                if (_className == "NSSound")
                {
                    //realObject = null;
                }
                else if (_className == "NSImage")
                {
                    realObject = new NSImage();
                    ((NSImage)realObject).ResourceName = _resourceName;
                }
            }
            else
            {

            }

            return realObject;

        }

        

        // implicit NSString to string conversion operator
        public static implicit operator NSImage(NSCustomResource nsCustomRes)
        {
            if (nsCustomRes == null)
                return null;

            return new NSImage() 
            { 
                Name = nsCustomRes.ResourceName 
            };
        }


    }
}
