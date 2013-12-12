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
    public class NSMutableArray : NSArray 
    {
        new public static Class Class = new Class(typeof(NSMutableArray));

        public NSMutableArray()
        {
            
        }

        new public static NSMutableArray alloc()
        {
            return new NSMutableArray();
        }

        public override id init()
        {
            return this;
        }


		public static NSMutableArray array()
		{
			return (NSMutableArray)NSMutableArray.alloc().init();
		}

        new public static NSMutableArray arrayWithArray(NSArray anArray)
        {
            return (NSMutableArray)alloc().initWithArray(anArray);
        }

		public virtual void removeObjectIdenticalTo(id anObject)
		{
			uint	i;

			if (anObject == null)
			{
				//NSWarnMLog(@"attempt to remove nil object");
				return;
			}
			i = (uint)this.Count;
			if (i > 0)
			{
				while (i-- > 0)
				{
					id	o = this.objectAtIndex((int)i);

					if (o == anObject)
					{
						this.removeObjectAtIndex(i);
					}
				}
			}
		}
		

        public virtual void removeLastObject()
        {
            if (this.Count == 0)
                throw new ArgumentException();

            _list.RemoveAt(this.Count - 1);
        }

        public virtual void removeObject(id anObject)
        {
            uint foundIndex = indexOfObject(anObject);
            if (foundIndex != NS.NotFound)
            {
                removeObjectAtIndex(foundIndex);
            }
        }


        public virtual void removeAllObjects()
        {
            _list.Clear();
        }

        public virtual void removeObjectAtIndex(uint anIndex)
        {
            _list.RemoveAt((int)anIndex);
        }

        public virtual void removeObjectsInRange(NSRange aRange)
        {
            uint i;
            uint s = aRange.Location;
            uint c = (uint)this.Count;

            i = aRange.Location + aRange.Length;

            if (c < i)
                i = c;

            if (i > s)
            {
                while (i-- > s)
                {
                    this.removeObjectAtIndex(i);
                }
            }
        }

        public virtual void insertObject(id anObject, uint index)
        {
            if (anObject == null)
                throw new ArgumentNullException();

            _list.Insert((int)index, anObject);
        }


        //public NSMutableArray(NSObjectDecoder aDecoder)
        //    : base(aDecoder)
        //{
            
        //}

        

        //new public static NSMutableArray Create(NSObjectDecoder aDecoder)
        //{
        //    NSMutableArray nsArray = new NSMutableArray();

        //    var xElement = aDecoder.XmlElement;
            
        //    var nodes = xElement.elements();
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
