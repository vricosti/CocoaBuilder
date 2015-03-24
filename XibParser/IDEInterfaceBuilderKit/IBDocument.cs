using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    //@interface IBDocument : IDEEditorDocument <IBXMLCoderDelegate, IBXMLDecoderDelegate, IDEDocumentStructureProviding, IBObjectContainerArchivingDelegate, IDENavigableItemArchivableRepresentationSupport, IDEMediaLibraryDelegate, DVTFileDataTypeDetector, IBAutolayoutInfoProvider, IBAutolayoutFrameDeciderDelegate>
    public class IBDocument : IDEEditorDocument
    {
        new public static Class Class = new Class(typeof(IBDocument));
        new public static IBDocument alloc() { return new IBDocument(); }

        protected IBTargetRuntime _targetRuntime;
        protected NSString _uniqueID;
        protected NSMutableDictionary _documentMetadata;
        protected IBObjectContainer _objectContainer;
        protected NSArray _ideTopLevelStructureObjects;
        protected IBMutableIdentityDictionary _memberToMemberWrapperMap;
        protected NSMutableSet _membersWithAutoNullifyingNonChildRelationshipKeyPaths;
        protected Int64 _delayExecutionOfDidChangeChildWrapperBlocksNestingCount;
        protected NSMutableOrderedSet _delayedDidChangeChildWrapperMemberWrappers;
        protected bool _isFlushingUpdatesToDelayedDidChangeChildWrapperMemberWrappers;
        protected IBMutableIdentityDictionary _explicitDefaultVersionsForDocumentDependencies;
        protected IBMutableIdentityDictionary _explicitVersionsForDocumentDependencies;
        protected NSDictionary _lastSavedPluginVersionsForDependentPlugins;
        protected NSString _lastSavedSystemVersion;
        protected NSString _lastSavedInterfaceBuilderVersion;
        protected NSNumber _cachedDevelopmentVersion;
        protected NSNumber _cachedSystemVersion;
        protected NSDictionary _additionalInstantiationInformation;
        //protected DVTPerformanceMetric _documentLoadingMetric;
        protected NSString _classNameThatPreventedDecode;
        protected NSURL _verificationMessagesSourceURL;
        protected NSArray _verificationMessages;
        protected id _previousXmlDecoderHints;
        protected bool _unarchiving;
        protected bool _closing;
        protected bool _transientPasteboardDocument;
        protected bool _wasUnarchivedWithDocumentUnarchiver;
        //protected IBMutableIdentityDictionary _effectivePropertyAccessControlCache;
        protected bool _shouldDisplayLockedMemberAlertForNextAutomaticUndo;
        protected id _memberWarnedForLockedPropertyChange;
        protected Int64 _defaultPropertyAccessControl;
        protected Int64 _localizationMode;
        protected bool _switchingLocalizations;
        //protected DVTObservingToken _headerScanningClassProviderFirstCompletionObserverToken;
        //protected DVTObservingToken _indexClassProviderIndexObservingToken;
        protected bool _haveClearedLocalDescriptionsClassProvider;
        //protected IBClassesDebuggerController _classesDebugger;
        protected IBLocalDescriptionsClassProvider _localDescriptionsClassProvider;
        protected IBUserDefinedActionClassProvider _userDefinedActionClassProvider;
        //protected IBHeaderScanningClassProvider _headerScanningClassProvider;
        //protected IBUnsavedFilesClassProvider _unsavedFilesClassProvider;
        protected IBSystemClassProvider _systemClassProvider;
        //protected IBIndexClassProvider _indexClassProvider;
        protected IBClassDescriber _classDescriber;
        //protected DVTTask _simulationTask;
        protected Int64 _undoableEditingActionNameTypePriority;
        protected bool _undoableEditingActionNameOpenedUndoGroup;
        //protected IBMutableIdentityDictionary _undoBlocks;
        protected bool _logUndoActions;
        //protected DVTObservingToken _undoManagerObservations;
        //protected IBDocumentArbitrationStackEntry _arbitrationStackEntryPushedDuringDidStartEditing;
        protected bool _ibDocumentIsClosing;
        protected NSMutableSet _editorViewControllers;
        //protected IBMutableIdentityDictionary _workspaceDocumentsByEditorViewController;
        protected Int64 _nextObjectObserverKey;
        protected bool _topLevelViewsShouldMaintainOriginalFrameWhenMovedToTheTopLevel;
        //protected DVTDelayedInvocation _optimisticallyDropRecomputableEditorStateInvocation;
        protected NSMutableSet _containerInterfaceBuilderDocumentFilePaths;
        //protected IDEContainer _resourceProvidingContainer;
        //protected id <DVTInvalidation> _containerDocumentFileNamesObservation;
        //protected IBResourceManager _resourceManager;
        //protected IDEMediaResourceVariantContext _variantContextForMediaLibrary;
        //protected IBMutableIdentityDictionary _objectsToDesignTimeRecensionTokens;
        //protected IBMutableIdentityDictionary _membersToWarnings;
        protected NSMutableSet _parentsWithPendingChildWarningCalculations;
        protected NSMutableSet _membersWithPendingWarningCalculations;
        protected NSMutableSet _classNamesWithPendingWarningCalculations;
        //protected DVTDelayedInvocation _warningsInvocation;
        protected bool _warningsEnabled;
        protected bool _allWarningsAreInvalid;
        protected Int64 _autolayoutAutomaticConstraintInvalidationPreventionNestingCount;
        protected Int64 _autolayoutAutomaticConstraintUpdatingPreventionNestingCount;
        //protected IBMutableIdentityDictionary _cachedAutolayoutStatusIncludingDescendantsByArbitrationUnitRoot;
        //protected IBMutableIdentityDictionary _autolayoutStatusByArbitrationUnitRoot;
        protected NSCountedSet _keepReferencingConstraintsNestingCountsByView;
        protected bool _assertDuringConstraintUpdatingNestingCount;
        protected NSMutableArray _autolayoutStatusChangeObservers;
        protected NSMutableArray _developmentTargetChangeObservers;
        protected NSMutableArray _autolayoutArbitrationStack;
        protected bool _automaticConstraintUpdatingDisabled;
        protected bool _isRunningArbitrationOfUnit;
        protected bool _useAutolayout;
        //protected IBFileBuildSettingsSnapshot _cachedBuildSettingsSnapshot;
        protected UInt64 _cachedBuildSettingsSnapshotNestingCount;
        protected NSMutableArray _blockingAutosaveErrors;
        //protected DVTNotificationToken _warningSeverityToken;
        protected bool _previouslyAttemptedUpgradeToXcode5;



        public void setClassNameThatPreventedDecode(NSString aClassNameThatPreventedDecode)
        {
            _classNameThatPreventedDecode = aClassNameThatPreventedDecode;
        }

        public virtual id initForURL(NSURL forUrl, NSURL url, NSString type, ref NSError error)
        {
            id self = base.initForURL(forUrl, url, type, ref error);
            if (self != null)
            {

            }

            return self;
        }

        public delegate bool InvokeWithUndoSuppressedDelegate(NSURL url, NSString typeName, ref NSError outError);

        public override bool readFromURL(NSURL url, NSString typeName, ref NSError outError)
        {
            bool bRet = false;

            InvokeWithUndoSuppressedDelegate myDelegate = _readFromURL;
            bRet = invokeWithUndoSuppressed(myDelegate);
   
            return bRet;
        }

        private bool _readFromURL(NSURL url, NSString typeName, ref NSError outError)
        {
            bool bRet = false;
            this.setClassNameThatPreventedDecode(null);
            //
            bRet = base.readFromURL(url, typeName, ref outError);


            return false;
        }

        public virtual bool invokeWithUndoSuppressed(InvokeWithUndoSuppressedDelegate delgate)
        {
            return false;
        }


        public override bool readFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            return false;
        }




    }
    

}
