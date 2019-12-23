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
using System.Reflection;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class id : INSNumber
    {
        protected bool _isInited;

        public id()
        {
        }

        public void RELEASE(object obj)
        {

        }

        public void release()
        {
        }

        public T retain<T>() where T : class
        {
            //return (T)Convert.ChangeType(, typeof(this.GetType()));
            return this as T;
        }

        public id retain()
        {
            // DOES NOTHING - JUST TO BE CONSISTENT WITH ORIGINAL COCOA
            return this;
        }

        public id autorelease()
        {
            // DOES NOTHING - JUST TO BE CONSISTENT WITH ORIGINAL COCOA
            return this;
        }

        public virtual Class GetClass()
        {
            return new Class(this.GetType());
        }

        public virtual id init()
        {
            id self = this;

            if (this.GetType() == typeof(id))
                self = null;

            return self;
        }

        public virtual NSString description()
        {
            return "id";
        }

        public virtual NSString stringValue()
        {
            return this.ToString();
        }

        public virtual bool isEqual(id otherObj)
        {
            return this.Equals(otherObj);
        }


        public bool respondsToSelector(SEL aSelector)
        {
            bool ret = false;
            string methodName = (string)aSelector.SelectorName;
            MethodInfo dynMethod = this.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            if (dynMethod != null)
            {
                ret = true;
            }

            return ret;
        }

        public id performSelector(SEL aSelector)
        {
            return null;
        }


        public id performSelector(SEL aSelector, id anObject)
        {
            return null;
        }


        public bool isKindOfClass(Class aClass)
        {
            bool isKindOfClass = false;

            if (aClass == null)
                return false;

            isKindOfClass = this.GetType().IsAssignableFrom(aClass.InnerType);

            //isKindOfClass =  this.GetType().Equals(aClass.InnerType);
            //isKindOfClass =  this.GetType().IsSubclassOf(aClass.InnerType);

            return isKindOfClass;
        }

        // Inside code I prefer to call those methods directly without having to write Objc.MsgSend(...)
        // So I am declaring the id base class as implementing those interfaces
        #region INSNumber
        public virtual double doubleValue()
        {
            throw new InvalidOperationException();
            return 0;
        }

        public virtual float floatValue()
        {
            throw new InvalidOperationException();
            return 0;
        }
        
        public virtual int intValue()
        {
            throw new InvalidOperationException();
            return 0;
        }
        
        public virtual int integerValue()
        {
            throw new InvalidOperationException();
            return 0;
        }
        
        public virtual bool boolValue()
        {
            throw new InvalidOperationException();
            return false;
        }

        public virtual uint unsignedIntegerValue()
        {
            throw new InvalidOperationException();
            return 0;
        }

        #endregion
    }
}
