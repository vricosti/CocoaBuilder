using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSButtonImageSource.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSButtonImageSource.m
    public class NSButtonImageSource : NSObject
    {
        new public static Class Class = new Class(typeof(NSButtonImageSource));
        new public static NSButtonImageSource alloc() { return new NSButtonImageSource(); }

        protected NSString _imageName;

        public virtual NSString ImageName
        {
            get { return imageName(); }
        }


        public virtual id initWithImageNamed(NSString name)
        {
            id self = base.init();
            if (self != null)
            {
                _imageName = name;
            }

            return self;
        }


        public override void encodeWithCoder(NSCoder aCoder)
        {
            if (aCoder.AllowsKeyedCoding)
            {
                aCoder.encodeObjectForKey(_imageName, @"NSImageName");
            }
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (aDecoder.AllowsKeyedCoding)
            {
                _imageName =  (NSString)aDecoder.decodeObjectForKey(@"NSImageName");
            }

            return self;
        }

        public virtual NSString imageName()
        {
            return _imageName;
        }
    }
}
