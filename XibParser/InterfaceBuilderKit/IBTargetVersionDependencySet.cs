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

        public virtual void setPluginDeclaredDependencies(NSMutableDictionary pluginDeclaredDeps)
        {
            if (_pluginDeclaredDependencies != pluginDeclaredDeps)
            {
                if (_pluginDeclaredDependencies != null)
                {
                    if (_pluginDeclaredDependencies.isEqual(pluginDeclaredDeps))
                        return;
                    _pluginDeclaredDependencies.release();
                }
                _pluginDeclaredDependencies = (NSMutableDictionary)pluginDeclaredDeps.mutableCopy();
            }
        }

        public virtual void setPluginDeclaredDependencyDefaults(NSMutableDictionary pluginDeclaredDepDefaults)
        {
            if (_pluginDeclaredDependencyDefaults != pluginDeclaredDepDefaults)
            {
                if (_pluginDeclaredDependencyDefaults != null)
                {
                    if (_pluginDeclaredDependencyDefaults.isEqual(pluginDeclaredDepDefaults))
                        return;
                    _pluginDeclaredDependencyDefaults.release();
                }
                _pluginDeclaredDependencyDefaults = (NSMutableDictionary)pluginDeclaredDepDefaults.mutableCopy();
            }
        }

        


    }
}
