using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class DVTPrimitiveFileDataType : DVTFileDataType
    {
        new public static Class Class = new Class(typeof(DVTPrimitiveFileDataType));
        new public static DVTPrimitiveFileDataType alloc() { return new DVTPrimitiveFileDataType(); }

        protected UInt64 _number;
        protected NSString _identifier;
        protected NSString _displayName;

        //- (id)description;
        //- (id)stringRepresentation;
        //- (id)secondaryFileDataTypes;
        //- (id)primaryFileDataType;
        //- (id)displayName;
        //- (id)identifier;
        //- (BOOL)isEqual:(id)arg1;
        //- (id)init;
        //- (id)initWithIdentifier:(id)arg1;
        //- (id)initWithIdentifier:(id)arg1 displayName:(id)arg2 conformingToTypes:(id)arg3;
    }
}
