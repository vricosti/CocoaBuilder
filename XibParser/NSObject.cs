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
    public class NSObject : NSCoding
    {
        public string Key { get; set; }

        public string RefId { get; set; }

        public string ObjectId { get; set; }

        public bool EncodedWithXMLCoder { get; set; }

        public NSObject()
        {
        }


        //public NSObject(INSObject iNSObject)
        //{

        //}

        public NSObject(string key)
        {
            this.Key = key;
        }

        public virtual void EncodeWithCoder(NSObjectDecoder aCoder)
        {

        }

        public virtual NSObject InitWithCoder(NSObjectDecoder aDecoder)
        {
            var xElement = aDecoder.XmlElement;
            string id = xElement.AttributeValueOrDefault("id", null);
            if (id != null)
            {
                
                RefId = id;
                aDecoder.Document.ListOfReferenceId.Add(id, this);

                //TODO : FIXME
                //if (id == "0")
                //{
                //    System.Diagnostics.Debug.WriteLine("id = 0");
                //}
                
                //var kvp = decoder.Document.ListOfReferenceId.Where(o => o.Key.Equals(id)).FirstOrDefault();
                //if (kvp.Key == null)
                //{
                //    decoder.Document.ListOfReferenceId.Add(id, this);
                //}
                //else
                //{
                //    System.Diagnostics.Debug.WriteLine("TODO: FIXME");
                //}
            }

            return this;
        }


        public bool IsKindOfClass(Class aClass)
        {
            if (aClass == null)
                return false;

            return this.GetType().IsSubclassOf(aClass.InnerType);
        }



        // implicit NSString to string conversion operator
        public static implicit operator string(NSObject nsObj)
        {
            System.Console.WriteLine("NSObject: implicit conversion occurred");
            NSString nsString = nsObj as NSString;
            if (nsString == null)
                throw new Exception("Invalid cast to string");

            return nsString.Value; 
        }

        public static implicit operator NSRect(NSObject nsObj)
        {
            System.Console.WriteLine("NSObject: implicit conversion occurred");
            NSString nsString = nsObj as NSString;
            if (nsString == null)
                throw new Exception("Invalid cast to string");

            return  NSRect.Create(nsString);
        }
    }
}
