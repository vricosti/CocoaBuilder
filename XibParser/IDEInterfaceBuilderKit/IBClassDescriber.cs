using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBClassDescriber
    {
        //DVTDelayedInvocation classDataIsUpdatingDelayedInvocation;
        //NSSet readonlyPartialsToIntegrateWhenClassProvidersAreFirstSet;
        //NSSet classProviderObservingTokens;
        //DVTDispatchLock ivarAccessLock;
        //DVTHashTable weakObservers;
        bool inIgnoreUpdatesBlock;
        bool classDataIsUpdating;
        NSArray classProviders;
        IBDocument document;
    }
}
