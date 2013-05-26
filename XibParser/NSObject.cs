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
    public static class ObjConstants
    {
        public const int NO = 0;
        public const int YES = 1;
    }

    public class NSObject : id, NSCoding2
    {
        public static Class Class = new Class(typeof(NSObject));
        public static NSObject Alloc()  { return new NSObject(); }
        //public static id New() { return Alloc().Init(); }

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


        public virtual NSString Description()
        {
            return this.ToString();
        }

        public virtual void EncodeWithCoder(NSCoder aCoder)
        {

        }

        public virtual id InitWithCoder(NSCoder aDecoder)
        {
            //var xElement = aDecoder.XmlElement;
            //NSString id = xElement.AttributeValueOrDefault("id", null);
            //if (id != null)
            //{
            //    RefId = id;
            //    if (!aDecoder.Document.ListOfReferenceId.ContainsKey(id))
            //    {
            //        aDecoder.Document.ListOfReferenceId.Add(id, this);
            //    }
            //}

            return this;
        }

        public virtual id AwakeAfterUsingCoder(NSCoder aDecoder)
        {
            id self = this;
            return self;
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
