using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBDocumentContentDescription : NSObject
    {
        new public static Class Class = new Class(typeof(IBDocumentContentDescription));
        new public static IBDocumentContentDescription alloc() { return new IBDocumentContentDescription(); }

        protected IBDocumentDataTypeDescription _fileDataTypeDescription;
        protected IBDocumentXMLArchiveTypeDescription _xmlArchiveTypeDescription;

    }

}   

