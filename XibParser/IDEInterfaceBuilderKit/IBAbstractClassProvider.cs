using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBAbstractClassProvider : NSObject
    {
        bool _dataSourceDidChangeDuringUpdateIgnoreBlock;
        //DVTHashTable _weakObservers;
        bool _inIgnoreUpdatesBlock;
        bool _dataSourceProvidedDataOnce;
        //DVTDispatchLock* ivarAccessLock;
    }


//    - (void).cxx_destruct;
//- (void)notifyObserversOfModifiedClasses:(id)arg1;
//- (id)addObserver:(id)arg1;
//- (void)removeObserver:(id)arg1;
//- (void)ignoreUpdatesDuring:(id)arg1;
//- (void)dataSourceDidChange;
//- (void)setDataSourceDidChange;
//- (void)setDataSourceDidChangeDuringUpdateIgnoreBlock:(BOOL)arg1;
//- (BOOL)dataSourceDidChangeDuringUpdateIgnoreBlock;
//- (void)setInIgnoreUpdatesBlock:(BOOL)arg1;
//- (BOOL)inIgnoreUpdatesBlock;
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
//@property BOOL dataSourceProvidedDataOnce;
//- (void)integrateDocumentDecodedPartialClassDescriptions:(id)arg1;
//@property(readonly) NSString *abbreviationForPrintingClassDescriberDescription;
//- (id)partialClassDescriptionsExcludedForEncoding;
//- (BOOL)shouldPreserveClassDescriptionSourceForEncodedClassDescriptions:(id)arg1;
//- (id)additionalNonDocumentReferencedPartialClassDescriptionsForEncoding;
//- (id)partialClassDescriptionsForEncodingClassNamed:(id)arg1;
//- (id)init;
}
