using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBLibraryController : NSObject
    {
        new public static Class Class = new Class(typeof(IBLibraryController));
        new public static IBLibraryController alloc() { return new IBLibraryController(); }

        //protected IBLibraryObjectTemplate _draggedLazyTemplate;
        protected IBCFMutableDictionary _assetCategoriesByTargetRuntime;
        protected NSString _currentDragMarker;
        protected NSArray _assetsPendingInsertion;
        protected NSImageView _detailImageView;
        protected NSMutableSet _skippedUserTemplateDocuments;
        protected bool _skippedPluginDependencyResultionForCust;
        protected NSMapTable _objectTemplateImageCache;
        protected IBCFMutableDictionary _directClassNameToObjectTemplateTable;
        protected IBCFMutableDictionary _topLevelClassesByTargetRuntime;
        protected NSMutableDictionary _classResources;
        //protected IBLibraryTabbedAssetDetailView _classAssetDetailView;
        protected NSArray _savedMenuItems;
        //protected IBClassCreationController _currentNewClassController;
        protected NSString _classNameToSelectAfterSync;
        protected bool _shouldSwitchCategoriesAfterSync;
    }
}
