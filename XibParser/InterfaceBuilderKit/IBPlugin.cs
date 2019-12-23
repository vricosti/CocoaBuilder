using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBPlugin : NSObject
    {
        new public static Class Class = new Class(typeof(IBPlugin));
        new public static IBPlugin alloc() { return new IBPlugin(); }


        public virtual NSSet supportedTargetRuntimes()
        {
            return NSSet.setWithObject((id)IBTargetRuntime.fallbackTargetRuntime());
        }

        public virtual NSString userPresentableIdentifierForTargetRuntimeIdentifier(NSString targetRuntimeID)
        {
            return targetRuntimeID;
        }

        public virtual id primaryDeclaredDependencyIdentifierForCategory(int cat)
        {
            return null;
        }

    }
}
