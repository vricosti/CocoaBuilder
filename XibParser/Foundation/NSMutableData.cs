using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSMutableData : NSData
    {
        new public static Class Class = new Class(typeof(NSMutableData));
        new public static NSMutableData alloc() { return new NSMutableData(); }


        public void appendData(NSData data)
        {
            if (data == null || data.Length == 0)
                return;

            byte[] tmpData = new byte[this.Length + data.Length];
            Buffer.BlockCopy(this.bytes(), 0, tmpData, 0, this.bytes().Length);
            Buffer.BlockCopy(data.bytes(), 0, tmpData, this.bytes().Length, data.bytes().Length);

            _bytes = tmpData;
        }
    }
}
