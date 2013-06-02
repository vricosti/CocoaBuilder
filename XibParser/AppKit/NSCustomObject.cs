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
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/GSNibLoading.m
    //https://github.com/gnustep/gnustep-gui/blob/master/Model/IBClasses.m
    public class NSCustomObject : NSObject, NSCoding2
    {
        new public static Class Class = new Class(typeof(NSCustomObject));

        protected NSString _className;
        protected NSString _extension;
        protected id _object;


        public virtual NSString ClassName 
        {
            get { return _className; }
            set { _className = value; } 
        }

        public virtual NSString Extension
        {
            get { return _extension; }
            set { _extension = value; }
        }

        public virtual id RealObject
        {
            get { return GetRealObject(); }
            set { SetRealObject(value); }
        }

        public NSCustomObject()
        {
            
        }

        public virtual id GetRealObject()
        {
            return _object;
        }

        public virtual void SetRealObject(id obj)
        {
            _object = obj;
        }

        
        public override void EncodeWithCoder(NSCoder aCoder)
        {
            base.EncodeWithCoder(aCoder);
        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (aDecoder.AllowsKeyedCoding)
            {
                _className = (NSString)aDecoder.DecodeObjectForKey(@"NSClassName");
                _extension = (NSString)aDecoder.DecodeObjectForKey(@"NSExtension");
                _object = aDecoder.DecodeObjectForKey(@"NSObject");
            }
            else
            {

            }


            //base.InitWithCoder(decoder);

            //var xElement = decoder.XmlElement;
            //var xNSClassName = xElement.Descendants().Where(c =>
            //       ((string)c.Attribute("key")) == "NSClassName").FirstOrDefault();
            //if (xNSClassName != null)
            //{
            //    _className = xNSClassName.Value;
            //}

            return self;
        }


        public override id AwakeAfterUsingCoder(NSCoder aDecoder)
        {
            id self = this;
#if DEBUG
              //NSLog (@"%x awakeAfterUsingCoder NSCustomObject: className = %@, realObject = %@, extension = %@", self, className, realObject, extension);
#endif

            //FIXME : What is objects ???
            //[objects addObject:self];
            return self;
        }




        public static NSCustomObject Create(NSObjectDecoder decoder)
        {
            NSCustomObject nsObj = new NSCustomObject();

            var xElement = decoder.XmlElement;
            var xNSClassName = xElement.Descendants().Where(c =>
                   ((string)c.Attribute("key")) == "NSClassName").FirstOrDefault();
            if (xNSClassName != null)
            {
                nsObj._className = xNSClassName.Value;
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
