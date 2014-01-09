using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBTargetRuntime : NSObject
    {
        new public static Class Class = new Class(typeof(IBTargetRuntime));
        new public static IBTargetRuntime alloc() { return new IBTargetRuntime(); }

        private static IBTargetRuntime _lastActiveTargetRuntime;

        private static IBCFMutableDictionary _classesToSharedTargetRuntimes;

        public static void setLastActiveTargetRuntime(IBTargetRuntime lastActiveTargetRuntime)
        {
            if (_lastActiveTargetRuntime != lastActiveTargetRuntime)
            {
                _lastActiveTargetRuntime = lastActiveTargetRuntime;
                //IBLibraryController.sharedInstance().activeRuntimeChanged();
                //NSUserDefaults.standardUserDefaults().setObjectForKey(_lastActiveTargetRuntime.identifier(), "PreviousActiveRuntime");
            }
        }


        public static IBTargetRuntime sharedTargetRuntime()
        {
            if (_classesToSharedTargetRuntimes == null)
            {
                _classesToSharedTargetRuntimes = (IBCFMutableDictionary)IBCFMutableDictionary.alloc().init();
            }

            return (IBTargetRuntime)_classesToSharedTargetRuntimes.objectForKey(Class);
        }

        public static IBTargetRuntime fallbackTargetRuntime()
        {
            Class cocoaTargetRuntimeClass = Class.NSClassFromString("IBCocoaTargetRuntime");
            return (IBTargetRuntime)Objc.MsgSend(cocoaTargetRuntimeClass, "sharedTargetRuntime");
        }



        public static IBTargetRuntime targetRuntimeWithIdentifier(NSString targetRuntimeID)
        {
            //NSString id = ((IBPlugin)Objc.MsgSend(Class, "plugin")).userPresentableIdentifierForTargetRuntimeIdentifier(targetRuntimeID);
            return null;
        }


        public virtual NSString identifier()
        {
            return null;
        }
    }
}
