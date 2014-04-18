using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBUserDefinedActionClassProvider : IBAbstractClassProvider
    {
        new public static Class Class = new Class(typeof(IBUserDefinedActionClassProvider));
        new public static IBUserDefinedActionClassProvider alloc() { return new IBUserDefinedActionClassProvider(); }

        protected NSDictionary _actionSelectorToTypeMap;
    }

//    - (void).cxx_destruct;
//- (id)collectionTypeForToManyOutlet:(id)arg1 forClassNamed:(id)arg2;
//- (id)typeForNamedRelation:(id)arg1 ofRelationshipType:(long long)arg2 forClassNamed:(id)arg3;
//- (id)namedRelationsOfRelationshipType:(long long)arg1 forClassNamed:(id)arg2 withLineage:(id)arg3 recursive:(BOOL)arg4;
//- (id)namedRelationsOfRelationshipType:(long long)arg1;
//- (id)classNamesForClassesWithActionsNamed:(id)arg1;
//- (id)classNames;
//- (id)descendantsOfClassesNamed:(id)arg1;
//- (id)subclassesOfClassNamed:(id)arg1;
//- (id)superclassOfClassNamed:(id)arg1;
//- (BOOL)hasDescriptionOfClassNamed:(id)arg1;
//@property(copy) NSDictionary *actionSelectorToTypeMap;
//- (void)integrateDocumentDecodedPartialClassDescriptions:(id)arg1;
//- (BOOL)shouldPreserveClassDescriptionSourceForEncodedClassDescriptions:(id)arg1;
//- (id)additionalNonDocumentReferencedPartialClassDescriptionsForEncoding;
//- (id)partialClassDescriptionsForEncodingClassNamed:(id)arg1;
//- (id)init;
}
