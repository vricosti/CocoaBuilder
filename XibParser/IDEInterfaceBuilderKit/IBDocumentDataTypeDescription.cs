using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBDocumentDataTypeDescription : IBAbstractFileDataTypeDescription
    {
        new public static Class Class = new Class(typeof(IBDocumentDataTypeDescription));
        new public static IBDocumentDataTypeDescription alloc() { return new IBDocumentDataTypeDescription(); }


        protected bool _writable;
        protected NSString _platformID;
        protected NSString _targetRuntimeID;
        protected NSString _archiveContentFileName;
        protected Class _documentClass;
        protected NSSet _archiveTypes;
        protected NSString _fileType;
        protected Class _foriengContentImporter;

        public virtual id initWithExtension(NSString extension)
        {
            //id self = base.initWithExtension(extension);
            //if (self != null)
            //{
            //    _archiveContentFileName = (NSString)this.valueForKeyPath("archiveInfo.contentFileName");
            //    _archiveTypes = (NSSet)this.valueForKeyPath("archiveInfo.validTypes.identifier");
            //    _documentClass = (Class)this.valueForKey("documentClass");
            //    _platformID = (NSString)this.valueForKey("platform");
            //    _targetRuntimeID = (NSString)this.valueForKey("targetRuntime");
            //    _writable = ((NSNumber)this.valueForKey("writable")).boolValue();
            //    _foriengContentImporter = (Class)this.valueForKey("foriengContentImporter");
               
            //}

            return self;
        }

    }
}
