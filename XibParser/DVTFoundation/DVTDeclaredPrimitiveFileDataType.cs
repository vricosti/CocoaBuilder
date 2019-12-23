using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class DVTDeclaredPrimitiveFileDataType : DVTPrimitiveFileDataType
    {
        new public static Class Class = new Class(typeof(DVTDeclaredPrimitiveFileDataType));
        new public static DVTDeclaredPrimitiveFileDataType alloc() { return new DVTDeclaredPrimitiveFileDataType(); }

        public bool isDeclaredType()
        {
            return false;
        }



        //- (id)initWithIdentifier:(id)arg1 displayName:(id)arg2 conformingToTypes:(id)arg3;


    }
}
