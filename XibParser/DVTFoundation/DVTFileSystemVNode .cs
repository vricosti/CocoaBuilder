using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class DVTFileSystemVNode : NSObject
    {
        new public static Class Class = new Class(typeof(DVTFileSystemVNode));
        new public static DVTFileSystemVNode alloc() { return new DVTFileSystemVNode(); }


         protected NSMutableDictionary _derivedInfoDict;
         //protected DVTPointerArray _filePaths;
         protected DVTFilePath _filePath;
         protected Int64 _inodeNumber;
         protected Int64 _fileSize;
         protected UInt64 _statFlags;
         protected Int64 _posixModificationTime;
         protected UInt32 _statUid;
         protected UInt32 _statGid;
         protected Int32 _deviceNumber;
         protected UInt32 _statMode;
    }
}
