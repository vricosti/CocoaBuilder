using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBDocumentArchivingSchemaEnumeration : NSObject
    {
        new public static Class Class = new Class(typeof(IBDocumentArchivingSchemaEnumeration));
        new public static IBDocumentArchivingSchemaEnumeration alloc() { return new IBDocumentArchivingSchemaEnumeration(); }

        protected EnumerationMap[] _map;
        protected Int64 _mapCount;
        protected UInt64 _fallbackValue;
        protected NSString _typeName;

        private static volatile IBDocumentArchivingSchemaEnumeration _instance;
        private static object _syncRoot = new Object();

        public static IBDocumentArchivingSchemaEnumeration sharedInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new IBDocumentArchivingSchemaEnumeration();
                }
            }
            return _instance;
        }

        public virtual id initWithTypeName(NSString typeName, EnumerationMap[] enumerationMap, Int64 count, bool copy, Int64 fallbackValue)
        {
            IBDocumentArchivingSchemaEnumeration self = this;

            if (base.init() != null)
            {
                _typeName = typeName;
                _map = enumerationMap;
                _mapCount = count;
            }

            return self;
        }

        public virtual NSString stringForValue(Int64 anIntValue)
        {
            NSString str = "";

            if (_mapCount != 0)
            {
                for (int i = 0; i < _mapCount; i++)
                {
                    if (_map[i].numValue == anIntValue)
                    {
                        str = _map[i].strValue;
                        break;
                    }
                }
            }

            return str;
        }

        public virtual bool decodeValue(ref Int64 anIntValue, NSString fromString)
        {
            bool bRet = false;

            if (_mapCount != 0)
            {
                for (int i = 0; i < _mapCount; i++)
                {
                    if (_map[i].strValue == fromString)
                    {
                        anIntValue = _map[i].numValue;
                        bRet = true;
                        break;
                    }
                }
            }

            return bRet;
        }
    }
}
