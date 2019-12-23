using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBClassDescriptionBasedClassProvider : IBAbstractClassProvider
    {
        new public static Class Class = new Class(typeof(IBClassDescriptionBasedClassProvider));
        new public static IBClassDescriptionBasedClassProvider alloc() { return new IBClassDescriptionBasedClassProvider(); }

        protected NSMutableDictionary _compositeDescriptions;
        protected NSMutableDictionary _classNamesToSubclasseNames;
        //protected DVTDispatchLock _dataAccessLock;
        protected IBTargetRuntime _targetRuntime;
    }
}
