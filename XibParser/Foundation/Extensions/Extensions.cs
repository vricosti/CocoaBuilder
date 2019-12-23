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
using System.Runtime.InteropServices;
using System.Text;


namespace Smartmobili.Cocoa
{
    public static class Extensions
    {
        public static bool isEqualToString(this NSString text, NSString text2)
        {
            if (text == null || text2 == null)
                return false;

            return text.Value.Equals(text2.Value);
        }
    }
}


namespace System
{
    

    public static class Extensions
    {
        public static bool isEqualToString(this string text, string text2)
        { 
            if (text == null || text2 == null)
                return false;

            return text.Equals(text2);
        }


        public static bool compare(this byte[] a1, byte[] a2, int len = 0)
        {
            if (a1 == a2)
            {
                return true;
            }
            if ((a1 != null) && (a2 != null))
            {
                if ((len == 0) && (a1.Length != a2.Length))
                {
                    return false;
                }
                for (int i = 0; i < len; i++)
                {
                    if (a1[i] != a2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }


        public static bool ConvertFromYesNo(this string text)
        {
            bool ret = false;

            if (text == null)
                throw new ArgumentException();

            if (text.Equals("YES", StringComparison.CurrentCultureIgnoreCase))
                ret = true;
            else if (text.Equals("NO", StringComparison.CurrentCultureIgnoreCase))
                ret = false;
            else
                throw new ArgumentException();


            return ret;
        }


        public static double ToDouble(this object anObject)
        {
            double ret = 0;
            try { ret = Convert.ToDouble(anObject); }  
            catch (Exception)  {}
            return ret;
        }

        public static float ToFloat(this object anObject)
        {
            float ret = 0;
            try { ret = Convert.ToSingle(anObject); } 
            catch (Exception) { }
            return ret;
        }

        public static int ToInt(this object anObject)
        {
            int ret = 0;
            try { ret = Convert.ToInt32(anObject); }
            catch (Exception) { }
            return ret;
        }

        public static bool ToBool(this object anObject)
        {
            bool ret = false;
            try { ret = Convert.ToBoolean(anObject); }
            catch (Exception) { }
            return ret;
        }

        //public static double ToDouble(this string nsString)
        //{
        //    double ret = 0;
        //    Double.TryParse(nsString, out ret);
        //    return ret;
        //}

        //public static float ToFloat(this string nsString)
        //{
        //    float ret = 0;
        //    Single.TryParse(nsString, out ret);
        //    return ret;
        //}

        //public static int ToInt(this string nsString)
        //{
        //    int ret = 0;
        //    Int32.TryParse(nsString, out ret);
        //    return ret;
        //}

        //public static bool ToBool(this string nsString)
        //{
        //    bool ret = false;
        //    bool.TryParse(nsString, out ret);
        //    return ret;
        //}

        public static IntPtr[] ReadArray(this IntPtr ptr, int size)
        {
            IntPtr[] ret = new IntPtr[size];
            for (int i = 0; i < size; i++)
            {
                ret[i] = Marshal.ReadIntPtr(ptr, i * IntPtr.Size);
            }

            return ret;
        }

        public static IntPtr Inc(this IntPtr ptr, int offSet = 1)
        {
            IntPtr ret = new IntPtr(ptr.ToInt64() + offSet);
            return ret;
        }

        public static IntPtr Dec(this IntPtr ptr, int offSet = -1)
        {
            IntPtr ret = new IntPtr(ptr.ToInt64() + offSet);
            return ret;
        }

        public static IntPtr Deref(this IntPtr ptr)
        {
            var deref = (IntPtr)Marshal.PtrToStructure(ptr, typeof(IntPtr));
            return deref;
        }

        public static T Deref<T>(this IntPtr ptr) where T : struct
        {
            var val = default(T);

            if (typeof(T) == typeof(bool))
            {
                bool bVal = Marshal.ReadByte(ptr) != 0 ? true : false;
                val = (T)Convert.ChangeType(bVal, typeof(bool));
            }
            else if (typeof(T) == typeof(Int32))
            {
                val = (T)Convert.ChangeType(Marshal.ReadInt32(ptr), typeof(Int32));
            }
            else if ((typeof(T) == typeof(Int64)))
            {
                val = (T)Convert.ChangeType(Marshal.ReadInt64(ptr), typeof(Int64));
            }
            else
            {
                val = (T)Convert.ChangeType(Marshal.ReadIntPtr(ptr), typeof(IntPtr));
            }

            return val;
        }



        public static IntPtr DerefInc(this IntPtr ptr, int offset = 0)
        {
            var deref = (IntPtr)Marshal.PtrToStructure(ptr, typeof(IntPtr));
            return deref.Inc(offset);
        }

        public static IntPtr Assign(this IntPtr lhs, IntPtr rhs)
        {
            return IntPtr.Zero;
        }

        public static IntPtr ToIntPtr(this Delegate dlgate)
        {
            return Marshal.GetFunctionPointerForDelegate(dlgate);
        }


        public static int strlen(this IntPtr nativeUtf8)
        {
            int len = 0;
            
            if (nativeUtf8 != IntPtr.Zero)
            {
                while (Marshal.ReadByte(nativeUtf8, len) != 0)
                    ++len;
            }

            return len;
        }


        public static char GetChar(this IntPtr nativeUtf8)
        {
            char chr = '\0';

            if (nativeUtf8 != IntPtr.Zero)
            {
                chr = (char)Marshal.ReadByte(nativeUtf8, 0);
            }

            return chr;
        }

        public static byte[] GetBytes(this IntPtr nativeUtf8)
        {
            int len = 0;
            
            while (Marshal.ReadByte(nativeUtf8, len) != 0) 
                ++len;
            
            if (len == 0) 
                return new byte[1] {0};
            
            byte[] buffer = new byte[len];
            Marshal.Copy(nativeUtf8, buffer, 0, buffer.Length);
            //buffer[buffer.Length - 1] = 0;

            return buffer;
        }

        public static string GetStringFromUTF8(this IntPtr nativeUtf8)
        {
            return Encoding.UTF8.GetString(nativeUtf8.GetBytes());
        }

        
    }
}

namespace System.Xml
{
    public static class Extensions
    {
        public static string GetText(this XmlReader xmlReader)
        {
            string text = string.Empty;

            if (xmlReader.NodeType == XmlNodeType.Element)
            {
                text = xmlReader.ReadElementContentAs(typeof(string), null) as string;
            }

            return text;
        }
    }
}


namespace System.Xml.Linq
{
    public static class Extensions
    {
        public static bool IsProperty(this XElement xElement)
        {
            bool ret = false;

            ret = ((xElement.Name == "string" || xElement.Name == "bool" || xElement.Name == "int")) &&
                (xElement.Attributes().Where(a => a.Name == "key").FirstOrDefault() != null);

            return ret;
        }


        public static object GetProperty(this XElement xElement)
        {
            object prop = null;

            string propType = xElement.Name.LocalName;
            switch (propType)
            {
                case "string":
                    prop = xElement.Value;
                    break;
                case "int":
                    prop = Convert.ToInt32(xElement.Value);
                    break;
                case "bool":
                    prop = Convert.ToBoolean(xElement.Value);
                    break;

                default:
                    throw new NotImplementedException();
            }


            return prop;
        }



        public static string AttributeValueOrDefault(this XElement xElement, string attrName, string defaultValue)
        {
            string val = defaultValue;

            var xAttr = xElement.Attributes().Where(a => a.Name == attrName).FirstOrDefault();
            if (xAttr != null)
                val = xAttr.Value;

            return val;
        }


    }

}
