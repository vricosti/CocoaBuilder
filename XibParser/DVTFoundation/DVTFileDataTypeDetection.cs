using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class DVTFileDataTypeDetection : NSObject
    {
        new public static Class Class = new Class(typeof(DVTFileDataTypeDetection));
        new public static DVTFileDataTypeDetection alloc() { return new DVTFileDataTypeDetection(); }


        //+ (id)guessFileDataTypeForFileAtPath:(id)arg1 bestCurrentGuessedFileDataType:(id)arg2;
        //+ (id)_guessFileDataTypeUsingMagicForFileAtPath:(id)arg1 bestCurrentGuessedFileDataType:(id)arg2 fileLength:(unsigned long long)arg3 fileBytes:(const char *)arg4;
        //+ (id)_magicCaches;


    }
}
