using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IDEEditorDocument : NSDocument
    {
        new public static Class Class = new Class(typeof(IDEEditorDocument));
        new public static IDEEditorDocument alloc() { return new IDEEditorDocument(); }

        //protected DVTDispatchLock _editorDocumentLock;
        //protected DVTExtension _extension;
        //protected DVTFileDataType _ide_hintedFileDataType;
        //protected DVTFilePath _filePath;
        //protected DVTFilePath autosavedContentsFilePath;
        //protected DVTMapTable _readOnlyClientsForRegistrationBacktrace;
        //protected DVTNotificationToken _willRedoChangeNotificationToken;
        //protected DVTNotificationToken _willUndoChangeNotificationToken;
        //protected DVTStackBacktrace _addedToDocumentControllerBacktrace;
        //protected DVTStackBacktrace _autosaveWithImplicitCancellabilityCallerBacktrace;
        //protected DVTStackBacktrace _beginUnlockingBacktrace;
        //protected DVTStackBacktrace _canCloseDocumentCallPriorToClosingDocumentStackBacktrace;
        //protected DVTStackBacktrace _continueActivityCallerBacktrace;
        //protected DVTStackBacktrace _continueAsynchronousWorkOnMainThreadCallerBacktrace;
        //protected DVTStackBacktrace _continueFileAccessCallerBacktrace;
        //protected DVTStackBacktrace _creationBacktrace;
        //protected DVTStackBacktrace _firstPerformActivityMessageBacktrace;
        //protected DVTStackBacktrace _invalidationBacktrace;
        //protected DVTStackBacktrace _lastUndoChangeNotificationBacktrace;
        //protected DVTUndoManager _dvtUndoManager;
        //protected int _readOnlyStatus;
        //protected NSDictionary _willCloseNotificationUserInfo;
        //protected NSMutableArray _pendingChanges;
        //protected NSMutableSet _documentEditors;
        //protected NSURL _ide_representedURL;
        //protected id<DVTCancellable> _closeAfterDelayToken;
        //protected id _filePresenterWriter;
        //protected bool _cachedHasRecentChanges;
        //protected bool _didDisableAutomaticTermination;
        //protected bool _ide_isTemporaryDocument;
        //protected bool _inSetUndoManager;
        //protected bool _inWriteSafelyToURL;
        //protected bool _isAttemptingToRespondToSaveDocumentAction;
        //protected bool _isClosing;
        //protected bool _isClosingForRevert;
        //protected bool _isInvalidated;
        //protected bool _isRespondingToFSChanges;
        //protected bool _isSafeToCallClose;
        //protected bool _isUndoingAfterFailureToUnlockDocument;
        //protected bool _isWritingToDisk;
        //protected bool _shouldAssertIfNotInvalidatedBeforeDealloc;
        //protected bool _trackFileSystemChanges;
        //protected bool _wholeDocumentChanged;
        //protected NSSet _readOnlyClients;
        //protected DVTFilePath _autosavedContentsFilePath;
    }
}
