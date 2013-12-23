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
        new public static NSInputStream alloc() { return new NSInputStream(); }

        protected NSData _data;
        protected Stream _stream;
       
        public static NSInputStream inputStreamWithData(NSData data)
        {
            return (NSInputStream)alloc().initWithData(data);
        }

        public virtual id initWithData(NSData data)
        {
            id self = this;

            _data = data;
            
            return self;
        }


        public override void open()
        {
            _stream = new MemoryStream(_data.Bytes);
        }

        public virtual int read(byte[] buffer, uint maxLength)
        {
            int bytesRead = 0;
            
            try
            {
               bytesRead = _stream.Read(buffer, 0, (int)maxLength);
            }
            catch (Exception ex)
            {
                bytesRead = -1;
            }

            return bytesRead;
        }


        public override void close()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream = null;
            }
        }
    }
}
