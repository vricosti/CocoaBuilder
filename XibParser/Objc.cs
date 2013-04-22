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
    public class Class
    {
        public Type InnerType { get; protected set; }

        public Class(Type type)
        {
            InnerType = type;
        }

        //public static bool operator ==(Class cls1, Class cls2)
        //{
        //    if (cls1 == null)
        //        return false;
        //    if (cls2 == null)
        //        return false;

        //    return (cls1.InnerType.Equals(cls2.InnerType));
        //}

        //public static bool operator !=(Class cls1, Class cls2)
        //{
        //    return !(cls1.InnerType.Equals(cls2.InnerType));
        //}

        public override bool Equals(object cls2)
        {
            Class class2 = new Class(cls2.GetType());
            return (this.InnerType.Equals((class2.InnerType)));
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + InnerType.GetHashCode();
            return hash;
        }


        public static Class NSClassFromString(string className)
        {
            Class cls = null;

            Type t = Type.GetType("Smartmobili.Cocoa." + className);
            if (t != null)
            {
                cls = new Class(t);
            }

            return cls;
        }

    }
}
