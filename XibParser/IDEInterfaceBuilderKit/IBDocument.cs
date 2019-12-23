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

        NSMutableDictionary _documentMetadata;
        NSArray _ideTopLevelStructureObjects;
        IBMutableIdentityDictionary _memberToMemberWrapperMap;
        NSMutableSet _membersWithAutoNullifyingNonChildRelationshipKeyPaths;
        Int64 _delayExecutionOfDidChangeChildWrapperBlocksNestingCount;
        NSMutableOrderedSet _delayedDidChangeChildWrapperMemberWrappers;
        bool _isFlushingUpdatesToDelayedDidChangeChildWrapperMemberWrappers;
        IBMutableIdentityDictionary _explicitDefaultVersionsForDocumentDependencies;
        IBMutableIdentityDictionary _explicitVersionsForDocumentDependencies;
        NSMutableDictionary _documentDependencyChangeObservers;
        NSNumber _cachedDevelopmentVersion;
        NSNumber _cachedSystemVersion;
        bool _unarchiving;
        bool _unarchivingForPasteBoard;
        Int64 _defaultPropertyAccessControl;
        //IBClassesDebuggerController _classesDebugger;
        //id <DVTInvalidation> _sourceCodeClassProviderObserverToken;
        //IBIndexClassDescriber _indexClassDescriber;
        Int64 _undoableEditingActionNameTypePriority;
        bool _undoableEditingActionNameOpenedUndoGroup;
        IBMutableIdentityDictionary _undoBlocks;
        bool _logUndoActions;
        //DVTObservingToken _undoManagerObservations;
        bool _ibDocumentIsClosing;
        IBMutableIdentityDictionary _memberChangeObservers;
        NSMutableDictionary _uniquedMemberChangeObservers;
        Int64 _nextMemberChangeObserverKey;
        NSMutableSet _editorViewControllers;
        IBMutableIdentityDictionary _workspaceDocumentsByEditorViewController;
        //DVTDelayedInvocation _optimisticallyDropRecomputableEditorStateInvocation;
        NSMutableSet _containerInterfaceBuilderDocumentFilePaths;
        //id <DVTInvalidation> _containerDocumentFileNamesObservation;
        //id <DVTInvalidation> _containerFontsObservation;
        //IDEMediaResourceVariantContext _variantContextForMediaLibrary;
        //IBDocumentIssueGenerator _issueGenerator;
        IBMutableIdentityDictionary _objectsToDesignTimeRecensionTokens;
        //IBFileBuildSettingsSnapshot _cachedBuildSettingsSnapshot;
        UInt64 _cachedBuildSettingsSnapshotNestingCount;
        NSMutableArray _blockingAutosaveErrors;
        //DVTNotificationToken _editorDocumentDidSaveToken;
        //IBDocumentAutolayoutManager _autolayoutManager;
        Int64 _configurationPropertyForwardingDirection;
        IBMutableIdentityDictionary _overriddenTargetConfigurationByMemberForAllAttributes;
        IBMutableIdentityDictionary _overriddenTargetConfigurationByMemberThenAttribute;
        IBMutableIdentityDictionary _propertiesByMemberWithBlockedChangeForwarding;
        Int64 _disableXMLFormattingCount;
        NSArray _objectsWithRepairedMemberIDs;
        bool _previouslyAttachedToEditor;
        bool _usesConfigurations;
        bool _wasUnarchivedWithDocumentUnarchiver;
        bool _transientPasteboardDocument;
        bool _previouslyAttemptedUpgradeToXcode5;
        bool _topLevelViewsShouldMaintainOriginalFrameWhenMovedToTheTopLevel;
        bool _launchScreen;
        bool _shouldDisplayLockedMemberAlertForNextAutomaticUndo;
        bool _markDocumentAsCleanAndDiscardUndoableEventsAtEndOfUndoGroup;
        NSString _defaultModuleName;
        IBClassDescriber _classDescriber;
        IBSystemClassProvider _systemClassProvider;
        IBAbstractClassProvider _mockClassProvider;
        IBUserDefinedActionClassProvider _userDefinedActionClassProvider;
        IBTargetRuntime _targetRuntime;
        //IBPlatform _platform;
        //IBMemberConfiguration _editedMemberConfiguration;
        //IDEWorkspaceDocument _explicitWorkspaceDocument;
        //IBResourceManager _resourceManager;
        //IDEContainer _resourceProvidingContainer;
        NSString _uniqueID;
        NSDictionary _additionalInstantiationInformation;
        NSArray _verificationMessages;
        NSURL _verificationMessagesSourceURL;
        NSString _lastSavedSystemVersion;
        NSString _lastSavedInterfaceBuilderVersion;
        NSDictionary _lastSavedPluginVersionsForDependentPlugins;
        //IBDocumentLiveViewsDispatcher _liveViewsDispatcher;
        IBObjectContainer _objectContainer;
        //IBDocumentPlatformAdapter _platformAdapter;
        id _previousXmlDecoderHints;
        NSString _classNameThatPreventedDecode;
        //DVTPerformanceMetric _documentLoadingMetric;
        IBMutableIdentityDictionary _effectivePropertyAccessControlCache;
        NSObject _memberWarnedForLockedPropertyChange;
        //IBSourceCodeClassProvider _sourceCodeClassProvider;
        //IBTemporaryPasteboardClassProvider _temporaryPasteboardClassProvider;
        //struct CGSize _canvasLayoutPositioningScale;



        public void setClassNameThatPreventedDecode(NSString aClassNameThatPreventedDecode)
        {
            _classNameThatPreventedDecode = aClassNameThatPreventedDecode;
        }


        NSArray verificationMessages()
        {
            return this._verificationMessages;
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
            //this.setClassNameThatPreventedDecode(null);

            Int64 decodingStyle = 0;
            bRet = decodeContentsOfURL(url, typeName, ref decodingStyle, ref outError);
            if (bRet)
            {
                appendVerificationMessages(performVerification());
            }

            return false;
        }


        bool decodeContentsOfURL(NSURL url, NSString ofType, ref Int64 decodingStyle, ref NSError error)
        {
            return false;
        }

        //function methImpl_IBDocument_decodeContentsOfURL_ofType_decodingStyle_error_ {


        public virtual bool invokeWithUndoSuppressed(InvokeWithUndoSuppressedDelegate delgate)
        {
            return false;
        }


        NSArray performVerification()
        {
            NSArray messages = null;

            return messages;
        }


        void appendVerificationMessages(NSArray theMessages)
        {
            //NSArray curMessages = verificationMessages();
            //if (curMessages != null)
            //{
            //    NSArray allMessages = curMessages.arrayByAddingObjectsFromArray(curMessages);
            //    this.setVerificationMessages(allMessages);
            //}
            //else
            //{
            //    this.setVerificationMessages(theMessages);
            //}
        }

        public override bool readFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            return false;
        }




    }


}
