using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBDocumentArchivingSchemaBitmask : NSObject
    {
        new public static Class Class = new Class(typeof(IBDocumentArchivingSchemaBitmask));
        new public static IBDocumentArchivingSchemaBitmask alloc() { return new IBDocumentArchivingSchemaBitmask(); }

        protected EnumerationMap[] _map;
        protected Int64 _mapCount;
        protected NSString _elementName;
        protected NSString _typeName;

        private static volatile IBDocumentArchivingSchemaBitmask _instance;
        private static object _syncRoot = new Object();

        public static IBDocumentArchivingSchemaBitmask sharedInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new IBDocumentArchivingSchemaBitmask();
                }
            }
            return _instance;
        }


        public virtual id initWithTypeName(NSString typeName, NSString elementName, EnumerationMap[] bitmaskMap, Int64 count, bool copy)
        {
            IBDocumentArchivingSchemaBitmask self = this;

            if (copy == false)
            {
                if (base.init() != null)
                {
                    _typeName = typeName;
                    _elementName = elementName;
                    _map = bitmaskMap;
                    _mapCount = count;

                }
            }
            else 
            { 

            }

            return self;
        }

        public virtual NSString elementName()
        {
            return _elementName;
        }

        public virtual NSString typeName()
        {
            return _typeName;
        }

        public virtual NSSet allBitNames()
        {
            NSMutableSet allNames = (NSMutableSet)NSMutableSet.alloc().init().autorelease().retain();

            for (int i = 0; i < _mapCount; i++)
            {
                allNames.addObject(_map[i].strValue);
            }
            
            return allNames;
        }


        public virtual NSArray bitNamesForBitmask(Int64 bitmask)
        {
            NSMutableArray bitNames = NSMutableArray.array();

            for (int i = 0; i < _mapCount; i++)
            {
                if ((bitmask & _map[i].numValue) == _map[i].numValue)
                {
                    bitNames.addObject(_map[i].strValue);
                }
                
            }


            return bitNames;
        }


        public virtual bool decodeBitmask(ref Int64 bitmask, NSSet fromBits)
        {
            bitmask = 0;
            for (int i = 0; i < _mapCount; i++)
            {
                if (fromBits.containsObject(_map[i].strValue))
                {
                    bitmask |= _map[i].numValue;
                }
            }

            return true;
        }

        //+ (id)sharedInstance;
        //@property(readonly) NSString *elementName; // @synthesize elementName;
        //@property(readonly) NSString *typeName; // @synthesize typeName;
        //- (void).cxx_destruct;
        //- (id)allBitNames;
        //- (id)bitNamesForBitmask:(long long)arg1;
        //- (BOOL)decodeBitmask:(unsigned long long *)arg1 fromBits:(id)arg2;
        //- (id)initWithTypeName:(id)arg1 elementName:(id)arg2 bitmaskMap:(CDStruct_6db0658e *)arg3 count:(long long)arg4 copy:(BOOL)arg5;

    }
}
