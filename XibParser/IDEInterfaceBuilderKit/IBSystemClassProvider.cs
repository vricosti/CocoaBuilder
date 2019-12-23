using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBSystemClassProvider : IBAbstractClassProvider
    {
        new public static Class Class = new Class(typeof(IBSystemClassProvider));
        new public static IBSystemClassProvider alloc() { return new IBSystemClassProvider(); }

        protected NSMutableDictionary _classNameToPrimaryPartialClassDescriptionMap;
        protected NSMutableDictionary _classNameToPartialClassDescriptionsMap;
        protected NSMutableDictionary _classNameToSubclassesMap;
        protected IBTargetRuntime _targetRuntime;

    }

//+ (id)systemClassProviderForTargetRuntime:(id)arg1;
//- (void).cxx_destruct;
//- (void)loadSystemClassDescriptions;
//- (void)loadSystemClassDescriptionsFromExtensionParameter:(id)arg1;
//- (id)filePathForClassDescriptionsParameter:(id)arg1;
//- (void)addPartialClassDescription:(id)arg1 fromExtensionWithIdentifier:(id)arg2;
//- (void)addKnownSubclass:(id)arg1 toClassNamed:(id)arg2;
//- (id)conflictingClassDescriptionsPreventingIntegrationOfClassDescriptionBecauseOfConflictingSuperClasses:(id)arg1;
//- (BOOL)wouldAddingClassNamed:(id)arg1 withSuperClassNamedIntroduceACycle:(id)arg2;
//- (id)lineageOfClassNamed:(id)arg1;
//- (id)partialClassDescriptionsForClassNamed:(id)arg1;
//- (id)collectionTypeForToManyOutlet:(id)arg1 forClassNamed:(id)arg2;
//- (id)typeForNamedRelation:(id)arg1 ofRelationshipType:(long long)arg2 forClassNamed:(id)arg3;
//- (id)namedRelationsOfRelationshipType:(long long)arg1 forClassNamed:(id)arg2 withLineage:(id)arg3 recursive:(BOOL)arg4;
//- (id)namedRelationsOfRelationshipType:(long long)arg1 forClassNamed:(id)arg2;
//- (id)namedRelationsOfRelationshipType:(long long)arg1;
//- (id)classNamesForClassesWithActionsNamed:(id)arg1;
//- (id)classNames;
//- (id)descendantsOfClassesNamed:(id)arg1;
//- (id)subclassesOfClassNamed:(id)arg1;
//- (id)superclassOfClassNamed:(id)arg1;
//- (BOOL)hasDescriptionOfClassNamed:(id)arg1;
//- (id)partialClassDescriptionsExcludedForEncoding;
//- (id)partialClassDescriptionsForEncodingClassNamed:(id)arg1;
//- (id)initWithTargetRuntime:(id)arg1;

}
