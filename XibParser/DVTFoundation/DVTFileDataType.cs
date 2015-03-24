using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class DVTFileDataType : NSObject
    {
        new public static Class Class = new Class(typeof(DVTFileDataType));
        new public static DVTFileDataType alloc() { return new DVTFileDataType(); }

        //@property(readonly) NSString *displayName;
        //@property(readonly) NSString *identifier;

        public static DVTFileDataType fileDataTypeWithIdentifier(NSString anIdentifier)
        {
            return null;

        }


    }
}
