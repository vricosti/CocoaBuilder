using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    [System.AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ObjcMethodAttribute : System.Attribute
    {
        public ObjcMethodAttribute(string methodName)
        {
            if (!string.IsNullOrEmpty(methodName))
            {
                MethodName = methodName;
            }
        }

        public string MethodName;
    }


    [System.AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class ObjcPropAttribute : System.Attribute
    {
        public ObjcPropAttribute()
        {
            GetName = string.Empty;
            SetName = string.Empty;
        }

        public ObjcPropAttribute(string attrName) : this()
        {
            if (!string.IsNullOrEmpty(attrName))
            {
                GetName = Char.ToLowerInvariant(attrName[0]) + attrName.Substring(1);
                SetName = "set" + attrName;
            }
        }

        public string GetName;

        public string SetName;
    }


}
