using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLChildren : NSMutableArray
    {
        new public static Class Class = new Class(typeof(NSXMLChildren));
        new public static NSXMLChildren alloc() { return new NSXMLChildren(); }

    
        protected NSMutableArray _array;
        protected bool _isStale;


        public override id init()
        {
            id self = this;

            this._isStale = false;
            this._array = (NSMutableArray)NSMutableArray.alloc().initWithCapacity(5);

            return self;
        }

        public virtual id initWithMutableArray(NSMutableArray anArray)
        {
            id self = this;

            this._isStale = false;
            this._array = anArray;

            return self;
        }

        public virtual void makeStale()
        {
            this._isStale = true;
        }

       

        public override uint count()
        {
            return this._array.count();
        }

        public override id objectAtIndex(uint index)
        {
            return _array.objectAtIndex(index);
        }

        


        

        public virtual void reallyAddObject(id anObject)
        {
            if (_isStale == true)
            {
                NSMutableArray array = NSMutableArray.arrayWithArray(_array);
                array.addObject(anObject);
                this.initWithMutableArray(array);
            }
            else
            {
                _array.addObject(anObject);
            }
        }


        public virtual void reallyInsertObject(id anObject, uint index)
        {
            if (_isStale == true)
            {
                NSMutableArray array = NSMutableArray.arrayWithArray(_array);
                array.insertObject(anObject, index);
                this.initWithMutableArray(array);

            }
            else
            {
                _array.insertObject(anObject, index);
            }
        }

        public virtual void reallyRemoveAllObjects()
        {
            if (_isStale == true)
            {
                this.init();

            }
            else
            {
                _array.removeAllObjects();
            }
        }


        public virtual void reallyRemoveObjectAtIndex(uint anIndex)
        {
            if (_isStale == true)
            {
                NSMutableArray array = NSMutableArray.arrayWithArray(_array);
                array.removeObjectAtIndex(anIndex);
                this.initWithMutableArray(array);
            }
            else
            {
                _array.removeObjectAtIndex(anIndex);
            }
        }

         public virtual void reallyRemoveObject(id anObject)
        {
            if (_isStale == true)
            {
                NSMutableArray array = NSMutableArray.arrayWithArray(_array);
                array.removeObject(anObject);
                this.initWithMutableArray(array);
            }
            else
            {
                _array.removeObject(anObject);
            }
        }

        public virtual void reallyReplaceObjectAtIndex(uint anIndex, id anObject)
        {
            if (_isStale == true)
            {
                NSMutableArray array = NSMutableArray.arrayWithArray(_array);
                array.replaceObjectAtIndex(anIndex, anObject);
                this.initWithMutableArray(array);
            }
            else
            {
                _array.replaceObjectAtIndex(anIndex, anObject);
            }
        }

        public override void addObject(id anObject)
        { }

        public override void addObjectsFromArray(NSArray NSArray)
        { }

        public override void insertObject(id anObject, uint index)
        { }

        //FIXME
        //public override void insertObjects(NSArray objects, NSIndexSet indexes)
        //{
        //    return;
        //}

        public override void removeAllObjects()
        { }

        public override void removeLastObject()
        { }

        public override void removeObjectAtIndex(uint anIndex)
        { }

        public override void removeObjectIdenticalTo(id anObject)
        { }

        public override void removeObjectIdenticalTo(id anObject, NSRange aRange)
        { }

        public override void removeObject(id anObject)
        { }

        public override void removeObject(id anObject, NSRange aRange)
        { }

        public override void removeObjectsAtIndexes(NSIndexSet indexes)
        { }


        

    }
}
