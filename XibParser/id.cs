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
    public class id
    {
        protected bool _isInited;

        public id()
        {
        }

        public virtual Class GetClass()
        {
            return new Class(this.GetType());
        }

        public virtual id Init()
        {
            id self = this;

            return self;
        }

        public bool RespondsToSelector(SEL aSelector)
        {
            return false;
        }

        public id PerformSelector(SEL aSelector)
        {
            return null;
        }


        public id PerformSelector(SEL aSelector, id anObject)
        {
            return null;
        }


        public bool IsKindOfClass(Class aClass)
        {
            bool isKindOfClass = false;

            if (aClass == null)
                return false;

            isKindOfClass = this.GetType().IsAssignableFrom(aClass.InnerType);

            //isKindOfClass =  this.GetType().Equals(aClass.InnerType);
            //isKindOfClass =  this.GetType().IsSubclassOf(aClass.InnerType);

            return isKindOfClass;
        }
    }
}
