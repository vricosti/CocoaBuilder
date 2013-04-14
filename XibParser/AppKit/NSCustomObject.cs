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
    public class NSCustomObject : NSObject, NSCoding
    {
        public string NSClassName { get; set; }

        public NSCustomObject()
        {
            
        }

        public override void EncodeWithCoder(NSObjectDecoder aCoder)
        {
            base.EncodeWithCoder(aCoder);
        }

        public override NSObject InitWithCoder(NSObjectDecoder decoder)
        {
            base.InitWithCoder(decoder);

            var xElement = decoder.XmlElement;
            var xNSClassName = xElement.Descendants().Where(c =>
                   ((string)c.Attribute("key")) == "NSClassName").FirstOrDefault();
            if (xNSClassName != null)
            {
                NSClassName = xNSClassName.Value;
            }

            return this;
        }



        public static NSCustomObject Create(NSObjectDecoder decoder)
        {
            NSCustomObject nsObj = new NSCustomObject();

            var xElement = decoder.XmlElement;
            var xNSClassName = xElement.Descendants().Where(c =>
                   ((string)c.Attribute("key")) == "NSClassName").FirstOrDefault();
            if (xNSClassName != null)
            {
                nsObj.NSClassName = xNSClassName.Value;
            }

            return nsObj;
        }


        //public static NSCustomObject Create(XElement xElement)
        //{
        //    NSCustomObject nsObj = new NSCustomObject();

        //    var xNSClassName = xElement.Descendants().Where(c =>
        //           ((string)c.Attribute("key")) == "NSClassName").FirstOrDefault();
        //    if (xNSClassName != null)
        //    {
        //        nsObj.NSClassName = xNSClassName.Value;
        //    }

        //    return nsObj;
        //}


        


    }
}
