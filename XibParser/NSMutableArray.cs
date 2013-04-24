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
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Smartmobili.Cocoa
{
    public class NSMutableArray : NSArray //: NSArray
    {
        public NSMutableArray()
        {
            
        }

        public static NSMutableArray Alloc()
        {
            return new NSMutableArray();
        }

        public NSMutableArray Init()
        {
            return this;
        }


        public virtual void RemoveLastObject()
        {
            if (this.Count == 0)
                throw new ArgumentException();

            _list.RemoveAt(this.Count - 1);
        }

        //public NSMutableArray(NSObjectDecoder aDecoder)
        //    : base(aDecoder)
        //{
            
        //}

        

        //new public static NSMutableArray Create(NSObjectDecoder aDecoder)
        //{
        //    NSMutableArray nsArray = new NSMutableArray();

        //    var xElement = aDecoder.XmlElement;
            
        //    var nodes = xElement.Elements();
        //    foreach (var node in nodes)
        //    {
        //        if (node.Name == "bool" && node.Attribute("key") != null)
        //            continue;

        //        nsArray.Add(aDecoder.Create(node));

        //    }

        //    return nsArray;
        //}
    }
}
