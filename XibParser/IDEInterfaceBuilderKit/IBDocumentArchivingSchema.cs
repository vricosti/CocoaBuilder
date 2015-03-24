using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public struct EnumerationMap
    {
        public Int64 numValue;
        public NSString strValue;
    }


    public class IBDocumentArchivingSchema : NSObject
    {
        new public static Class Class = new Class(typeof(IBDocumentArchivingSchema));
        new public static IBDocumentArchivingSchema alloc() { return new IBDocumentArchivingSchema(); }

        protected IBMutableIdentityDictionary _classesToElementNames;
        protected NSMutableDictionary _elementNamesToClasses;
        protected NSMutableDictionary _enumerationsByTypeNames;
        protected NSMutableDictionary _bitmasksByTypeNames;
        protected NSMutableDictionary _bitmasksByElementNames;
        protected NSMutableSet _dictionaryNames;
        protected NSMutableSet _groupNames;
        protected NSMutableSet _arrayNames;
        protected NSString _identifier;


        public static IBDocumentArchivingSchema sharedSchemaForSchemaExtensionIdentifier(NSString anIdentifier)
        {
            IBDocumentArchivingSchema ms = (IBDocumentArchivingSchema)IBDocumentArchivingSchema.alloc().init();
           

            return ms;
        }


        public static IBDocumentArchivingSchema schemaForSchemaExtensionPointIdentifier(NSString anIdentifier)
        {
            IBDocumentArchivingSchema ms = (IBDocumentArchivingSchema)IBDocumentArchivingSchema.alloc().init();


            return ms;
        }

//+ (id)sharedSchemaForSchemaExtensionIdentifier:(id)arg1;
//+ (id)schemaForSchemaExtensionPointIdentifier:(id)arg1;
//@property(readonly) NSString *identifier; // @synthesize identifier;
//- (void).cxx_destruct;
//- (id)description;
//- (void)verify;
//- (BOOL)shouldVerify;
//- (id)bitmaskWithElementName:(id)arg1;
//- (id)bitmaskWithTypeName:(id)arg1;
//- (id)enumerationWithTypeName:(id)arg1;
//- (id)elementNameForClass:(Class)arg1;
//- (Class)classForElementName:(id)arg1;
//- (BOOL)isDefinedGroup:(id)arg1;
//- (BOOL)isDefinedArray:(id)arg1;
//- (BOOL)isDefinedDictionary:(id)arg1;
//- (void)addGroup:(id)arg1;
//- (void)addArray:(id)arg1;
//- (void)addDictionary:(id)arg1;
//- (void)addBitmask:(id)arg1;
//- (void)addEnumeration:(id)arg1;
//- (void)addClass:(Class)arg1 withElementName:(id)arg2;
//- (id)initWithIdentifier:(id)arg1;
    }
}
