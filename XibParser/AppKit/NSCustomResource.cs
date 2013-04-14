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
        public string ClassName { get; set; }

        public string ResourceName { get; set; }


        public NSCustomResource()
        {

        }

        //public NSCustomResource(NSObjectDecoder aDecoder)
        //    : base(aDecoder)
        //{
            
        //}

        public override NSObject InitWithCoder(NSObjectDecoder decoder)
        {
            base.InitWithCoder(decoder);

            var xElement = decoder.XmlElement;
            foreach (var xElem in xElement.Elements())
            {
                XAttribute xKeyAttr = xElem.Attribute("key");
                if (xKeyAttr != null && xKeyAttr.Value == "NSClassName")
                {
                    ClassName = xElem.Value;
                }
                else if (xKeyAttr != null && xKeyAttr.Value == "NSResourceName")
                {
                    ResourceName = xElem.Value;
                }
            }

            return this;
        }

        //public static NSCustomResource Create(NSObjectDecoder aDecoder)
        //{
        //    NSCustomResource nsCustomRes = new NSCustomResource();

        //    var xElement = aDecoder.XmlElement;
        //    foreach (var xElem in xElement.Elements())
        //    {
        //        XAttribute xKeyAttr = xElem.Attribute("key");
        //        if (xKeyAttr != null && xKeyAttr.Value == "NSClassName")
        //        {
        //            nsCustomRes.ClassName = xElem.Value;
        //        }
        //        else if (xKeyAttr != null && xKeyAttr.Value == "NSResourceName")
        //        {
        //            nsCustomRes.ResourceName = xElem.Value;
        //        }
        //    }

        //    return nsCustomRes;
        //}

        

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
