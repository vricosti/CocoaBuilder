using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    // size : 0x114(x86) - 0x228(x64)
    public struct IBDocumentStorage
    {
        public IBObjectContainer objectContainer; //0x08(x86) - 0x10(0x64)
        public NSMutableSet unknown0C; //0x0C(x86) - ???(x64)
        public NSMutableSet unknown10; //0x0C(x86) - ???(x64)
        public NSMutableSet unknown14; //0x0C(x86) - ???(x64)
        public NSString lastSavedSystemVersion; //0x20(x86) - 0x40(x64)
        public IBClassDescriber classDescriber; //0x44(x86) - 0x88(x64)
        public IBCFMutableDictionary unknown0;  //0xA0(x86) - ???(x64)
        public NSDictionary metadata; //0xA8(x86) - 0x150(x64)
        public NSMutableDictionary previousXmlDecoderHints; //0x8C(x86) - 0x118(x64)
        public IBTargetRuntime targetRuntime; //0x10C(x86) - 0x218(x64) 
        public NSString lastSavedInterfaceBuilderVersion; //0x24(x86) - 0x48(x64)
        public NSString lastSavedAppKitVersion; //0x28(x86) - 0x50(x64)
        public NSString lastSavedHIToolboxVersion; //0x2C(x86) - 0x58(x64)
        public NSDictionary lastSavedpluginVersionsForDependedPlugins; //0x30(x86) - 0x60(x64)
        //IBLocalizationInfoController ibLocalizationInfoController; //0x38(x86) - ???(x64)
        //IBEditorManager ibEditorManager; //0x48(x86) - ???(x64)
        public IBCFMutableDictionary unknown1; //0x4C(x86) - ???(x64)
        public NSString uniqueID; //0x50(x86) - ???(x64)
        public NSMutableArray objectIDsToOpen; //0x5C(x86) - 0xB8(x64)
        public NSString classNameThatPreventedDecode; //0x60(x86) - ???(x64)
        public int localizationMode; //0x68(x86) - 0x0D0(x64)
        public int defaultPropertyAccessControl; //0x98(x86) - 0x130(x64)
        public NSMutableDictionary unknownA8; //0xA8(x86) - ???(x64)
        public IBCFMutableDictionary unknownB0; //0xB0(x86) - ???(x64)
        public NSMutableDictionary unknownB4; //0xB4(x86) - ???(x64)
        public NSDictionary unknownC4; //0xC4(x86) - ???(x64)
        public bool isAutomaticProjectSynchronizationEnabled; //0xC8(x86) - ???(x64)
        public IBTargetVersionDependencySet pluginDeclaredDependenciesCat0; //0xF0(x86) - 0x1E0(x64)
        public IBTargetVersionDependencySet pluginDeclaredDependenciesCat1; //0xF4(x86) - 0x1E8(x64)
        public NSString lastKnownRelativeProjectPath; //0x0D0(x86) - 0x1A0(x64)
        public NSMutableDictionary unknown110; //0x110(x86) - ???(x64)
    }
}
