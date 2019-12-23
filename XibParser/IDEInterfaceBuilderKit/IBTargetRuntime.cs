using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBTargetRuntime : NSObject
    {
        new public static Class Class = new Class(typeof(IBTargetRuntime));
        new public static IBTargetRuntime alloc() { return new IBTargetRuntime(); }

        protected NSSet _objectLibraryTemplateExtensions;
        protected NSSet _documentDependencies;
        protected NSSet _classDescriptionsExtensions;
        protected NSSet _embeddingPolicyExtensions;
        protected NSSet _buildEnvironmentVerifiers;
        protected NSSet _connectionExtensions;
        protected bool _installedColorList;


        //public static id targetRuntimeWithArchiveIdentifier()


//        + (id)targetRuntimeWithArchiveIdentifier:(id)arg1 variantIdentifier:(id)arg2;
//+ (id)targetRuntimeWithIdentifier:(id)arg1;
//+ (id)sharedTargetRuntime;
//+ (void)registerSharedInstance;
//- (void).cxx_destruct;
//- (id)displayNameForPrimaryUserInterface;
//- (id)nextTargetRuntime;
//- (id)alternateTargetRuntimes;
//@property(readonly) id <DVTFontTextFieldDataSource> fontDataSource;
//- (void)installColorListIfNeeded;
//- (id)colorList;
//- (id)connectionClasses;
//- (id)connectionExtensionForConnectionClass:(Class)arg1;
//- (id)buildEnvironmentVerifiers;
//- (id)connectionExtensions;
//- (id)embeddingPolicyExtensions;
//- (id)classDescriptionsExtensions;
//- (id)documentDependencyForType:(id)arg1;
//- (id)documentDependencies;
//- (id)objectLibraryTemplateExtensions;
//- (id)icon;
//- (Class)documentClass;
//- (id)operatingSystemName;
//- (id)windowPasteboardType;
//- (id)viewPasteboardType;
//- (id)objectPasteboardType;
//- (id)archiveVariantIdentifier;
//- (id)archiveIdentifier;
//- (id)identifier;




    }
}
