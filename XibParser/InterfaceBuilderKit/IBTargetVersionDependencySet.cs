using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBTargetVersionDependencySet : NSObject
    {
        new public static Class Class = new Class(typeof(IBTargetVersionDependencySet));
        new public static IBTargetVersionDependencySet alloc() { return new IBTargetVersionDependencySet(); }

        protected IBDocument _document;
        protected NSMutableDictionary _pluginDeclaredDependencies;
        protected NSMutableDictionary _pluginDeclaredDependencyDefaults;
        int _category;

    }
}
