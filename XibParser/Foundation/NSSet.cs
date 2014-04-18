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
    public class NSSet : NSObject
    {
        new public static Class Class = new Class(typeof(NSSet));
        new public static NSSet alloc() { return new NSSet(); }

        protected HashSet<id> _sets;


        public static NSSet setWithObject(id obj)
        {
            return (NSSet)NSSet.alloc().initWithObjects(obj);
        }


        public virtual id initWithObjects(params id[] objects)
        {
            id self = this;

            if (objects == null)
                return null;

            _sets = new HashSet<id>(objects.Where(x => x != null).ToArray());

            return self;
        }

        public virtual bool containsObject(id anObject)
        {
            return _sets.Contains(anObject);
        }

        public id member(id anObject)
        {
            if (anObject == null)
                return null;

            if (_sets.Contains(anObject))
                return anObject;
            else 
                return null;
        }
        public virtual void addObject(id anObject)
        { }

        public virtual void addObjectsFromArray(NSArray anArray)
        { }

        public virtual void removeObject(id anObject)
        { }
        public virtual void removeAllObjects()
        { }
    }


    public class NSMutableSet : NSSet
    {
        new public static Class Class = new Class(typeof(NSMutableSet));
        new public static NSMutableSet alloc() { return new NSMutableSet(); }

        public override void addObject(id anObject)
        {
            _sets.Add(anObject);
        }

        public override void addObjectsFromArray(NSArray anArray)
        { 
            foreach(id item in anArray)
            {
                addObject(item);
            }
        }

        public override void removeObject(id anObject)
        {
            _sets.Remove(anObject);
        }
        public override void removeAllObjects()
        {
            _sets.Clear();
        }
        
    }

    public class NSCountedSet : NSMutableSet
    {
        new public static Class Class = new Class(typeof(NSCountedSet));
        new public static NSCountedSet alloc() { return new NSCountedSet(); }

        public virtual id initWithArray(NSArray anArray)
        {
            return null;
        }

        public virtual id initWithCapacity(UInt32/*NSUInteger*/ numItems)
        {
            return null;
        }

        public virtual id initWithSet(NSSet aSet)
        {
            return null;
        }

        public virtual void addObject(id anObject)
        {

        }


        //- (NSUInteger)countForObject:(id)anObject
    }

    public class NSOrderedSet : NSObject
    {
        new public static Class Class = new Class(typeof(NSOrderedSet));
        new public static NSOrderedSet alloc() { return new NSOrderedSet(); }
    }


    public class NSMutableOrderedSet : NSOrderedSet
    {
        new public static Class Class = new Class(typeof(NSMutableOrderedSet));
        new public static NSMutableOrderedSet alloc() { return new NSMutableOrderedSet(); }
    }
    
}
