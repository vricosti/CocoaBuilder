using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace dotgnu.xml
{
    public static class Extensions
    {
        public static string ToCsString(this IntPtr ptr)
        {
            string s = string.Empty;

            
            if (ptr != IntPtr.Zero)
            {
                var data = new List<byte>();
                var off = 0;

                while (true)
                {
                    var ch = Marshal.ReadByte(ptr, off++);
                    if (ch == 0)
                    {
                        break;
                    }
                    data.Add(ch);
                }

                s = Encoding.UTF8.GetString(data.ToArray());
            }

            return s;
        }
    }
}
