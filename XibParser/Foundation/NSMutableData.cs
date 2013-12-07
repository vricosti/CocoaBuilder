using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSMutableData : NSData
    {
        new public static Class Class = new Class(typeof(NSMutableData));
        new public static NSMutableData Alloc() { return new NSMutableData(); }


        public void AppendData(NSData data)
        {
            if (data == null || data.Length == 0)
                return;

            byte[] tmpData = new byte[this.Length + data.Length];
            Buffer.BlockCopy(this.Bytes, 0, tmpData, 0, this.Bytes.Length);
            Buffer.BlockCopy(data.Bytes, 0, tmpData, this.Bytes.Length, data.Bytes.Length);

            Bytes = tmpData;
        }
    }
}
