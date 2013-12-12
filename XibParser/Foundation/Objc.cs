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
    public class Objc
    {
        public static bool Overridden(Type type, NSString methodName)
        {
            if ((type == null) || (methodName == null))
                return false;

            var mi = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
            if (mi == null) 
                return false;

            var declaringType = mi.DeclaringType.FullName;
            return declaringType.Equals(type.FullName, StringComparison.OrdinalIgnoreCase);

            //return type.GetMethod(methodName).DeclaringType == type;
        }


        public static object MsgSend(id receiver, NSString aString, params object[] args)
        {
            object ret = null;

            if (receiver == null || aString == null)
                return null;

            // receiver is an instance of an object
            if (!(receiver is Class))
            {
                string methodName = (string)aString.Value;
                MethodInfo dynMethod = receiver.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
                if (dynMethod != null)
                {
					/////////////////////////////////////////////////////////
					// Mono is buggy and throw an exception here !!!
					/////////////////////////////////////////////////////////
                    ret = dynMethod.Invoke(receiver, args);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("cannot find {0}", methodName));
                }

            } // receiver is an object representing a class type(Class)
            else
            {
                ret = null;
                Class cls = receiver as Class;
                ret = cls.InnerType.GetProperty(aString).GetValue(ret, null);
            }

            return ret;
        }

        //public static object MsgSend2(id receiver, NSString aString, object getOrSetValue)
        //{
        //    object ret = null;

        //    ret = receiver.GetType().GetProperty(aString).GetValue(receiver, null);

        //    return ret;
        //}



        public static NSString encode(Type type)
        {
            return "";
        }


    }

    public class Class : NSObject
    {
        public Type InnerType { get; protected set; }

        public Class(Type type)
        {
            InnerType = type;
        }


        public NSString ObjcType
        {
            get
            {
                return "";
            }
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
