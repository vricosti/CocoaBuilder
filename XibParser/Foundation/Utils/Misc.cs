using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Smartmobili.Cocoa
{

    public static class InteropHelper
    {
        public static T GetDelegateForFunctionPointer<T>(IntPtr addr) where T : class
        {
            System.Delegate fn_ptr = Marshal.GetDelegateForFunctionPointer(addr, typeof(T));
            return fn_ptr as T;
        }
    }

    public static class ByteUtil
    {

        public static int compare(this byte[] a1, byte[] a2, int len = 0)
        {
            if (a1 == a2)
            {
                return 0;
            }
            if ((a1 != null) && (a2 != null))
            {
                if ((len == 0) && (a1.Length != a2.Length))
                {
                    return 1;
                }
                for (int i = 0; i < len; i++)
                {
                    if (a1[i] != a2[i])
                    {
                        return 1;
                    }
                }
                return 0;
            }
            return 1;
        }
    }
}
