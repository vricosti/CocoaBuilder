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
        new public static NSButtonImageSource Alloc() { return new NSButtonImageSource(); }

        protected NSString _imageName;

        public virtual NSString ImageName
        {
            get { return imageName(); }
        }


        public virtual id initWithImageNamed(NSString name)
        {
            id self = base.Init();
            if (self != null)
            {
                _imageName = name;
            }

            return self;
        }


        public override void EncodeWithCoder(NSCoder aCoder)
        {
            if (aCoder.AllowsKeyedCoding)
            {
                aCoder.EncodeObjectForKey(_imageName, @"NSImageName");
            }
        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (aDecoder.AllowsKeyedCoding)
            {
                _imageName =  (NSString)aDecoder.DecodeObjectForKey(@"NSImageName");
            }

            return self;
        }

        public virtual NSString imageName()
        {
            return _imageName;
        }
    }
}
