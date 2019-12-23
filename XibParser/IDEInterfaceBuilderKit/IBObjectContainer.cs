using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{

    public class IBObjectContainer : NSObject
    {
        new public static Class Class = new Class(typeof(IBObjectContainer));
        new public static IBObjectContainer alloc() { return new IBObjectContainer(); }

        protected IBObjectRecordSet _objectRecords;
        protected IBMutableIdentityDictionary _connectionsBySource;
        protected IBMutableIdentityDictionary _connectionsByDestination;
        protected IBMutableIdentityDictionary _objectsToObjectRecords;
        protected IBMutableIdentityDictionary _connectionsToIdentifiers;
        protected NSMutableDictionary _identifiersToConnections;
        protected NSMutableDictionary _objectIDsToObjectRecords;
        protected NSMutableDictionary _unlocalizedProperties;
        protected NSMutableDictionary _localizations;
        protected NSSet _cachedLocalizationKeys;
        protected NSString _activeLocalization;
        protected NSString _sourceID;
        protected NSString _uniqueID;
        protected Int64 _maxID;
        protected bool _usesAutoincrementingIDs;
        protected NSArray _verificationIssues;
        protected IBObjectRecord _rootRecord;
        protected NSObject _rootObject;
        protected IBMemberIDMap _groupContainer;
        protected NSMutableDictionary _memberMetadata;
        protected id _delegate;

    }
    

}
