using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSInputStream : NSStream
    {
        new public static Class Class = new Class(typeof(NSInputStream));
        new public static NSInputStream Alloc() { return new NSInputStream(); }

        protected NSData _data;
        protected Stream _stream;
       
        public static NSInputStream InputStreamWithData(NSData data)
        {
            return (NSInputStream)Alloc().InitWithData(data);
        }

        public virtual id InitWithData(NSData data)
        {
            id self = this;

            _data = data;
            
            return self;
        }


        public override void Open()
        {
            _stream = new MemoryStream(_data.Bytes);
        }

        public virtual int Read(byte[] buffer, int maxLength)
        {
            int bytesRead = 0;
            
            try
            {
               bytesRead = _stream.Read(buffer, 0, maxLength);
            }
            catch (Exception ex)
            {
                bytesRead = -1;
            }

            return bytesRead;
        }


        public override void Close()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream = null;
            }
        }
    }
}
