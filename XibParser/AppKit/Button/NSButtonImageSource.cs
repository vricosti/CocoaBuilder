using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSButtonImageSource : NSObject
    {
        new public static Class Class = new Class(typeof(NSButtonImageSource));
        new public static NSButtonImageSource Alloc() { return new NSButtonImageSource(); }

        protected NSString _imageName;
        protected NSMutableDictionary _images;

        private static NSMutableDictionary sources = null;

        public static id ButtonImageSourceWithName(NSString name)
        {
            NSButtonImageSource	source;

            source = (NSButtonImageSource)sources.ObjectForKey(name);
            if (source == null)
            {
                source = NSButtonImageSource.Alloc();
                source._imageName = name;
                source._images = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
                sources.SetObjectForKey(source, source._imageName);
            }
            return source;
        }

        static NSButtonImageSource() { Initialize(); }
        static void Initialize()
        {
            if (sources == null)
            {
                sources = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
            }
        }

        public override void EncodeWithCoder(NSCoder aCoder)
        {
            //FIXME
        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (aDecoder.AllowsKeyedCoding)
            {
                NSString name =  (NSString)aDecoder.DecodeObjectForKey(@"NSImageName");
                self = ButtonImageSourceWithName(name);
            }

            return self;
        }
    }
}
