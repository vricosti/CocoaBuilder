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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Smartmobili.Cocoa
{

    public class NSMutableDictionary : NSDictionary
    {
        new public static Class Class = new Class(typeof(NSMutableDictionary));

        new public static NSMutableDictionary alloc()
        {
            return new NSMutableDictionary();
        }

        public NSMutableDictionary()
        {

        }

       
        

        public static NSMutableDictionary dictionaryWithDictionary(NSDictionary anotherDic)
        {
            return (NSMutableDictionary)alloc().initWithDictionary(anotherDic);
        }

        public virtual id initWithCapacity(uint numitems)
        {
            id self = this;

            _dict = new Dictionary<id, id>((int)numitems);

            return self;
        }

        public virtual id mutableCopy()
        {
            return this;
        }

        public virtual void addEntriesFromDictionary(NSDictionary otherDictionary)
        {
            NSEnumerator enumerator = otherDictionary.keyEnumerator();
            id aKey = null;
            while ( (aKey = enumerator.nextObject()) != null) 
            {
                id value = otherDictionary.objectForKey(aKey);
                this.setObjectForKey(value, aKey);
            }
        }
       
    }
}
