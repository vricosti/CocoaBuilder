/*
* XibParser.
* Copyright (C) 2013 Smartmobili SARL
* 
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License as published by the Free Software Foundation; either
* version 2 of the License, or (at your option) any later version.
* 
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
* 
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, write to the
* Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
* Boston, MA  02110-1301, USA. 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;


namespace Smartmobili.Cocoa
{
   

    public class IBDocument : NSDocument
    {
        new public static Class Class = new Class(typeof(IBDocument));
        new public static IBDocument alloc() { return new IBDocument(); }

        private static int _documentWindowUsesStaticOrigin = -1;

        protected IBDocumentStorage _storage; // 0x34(x86) - 0x68(x64)

        static IBDocument() { initialize(); }
        new static void initialize()
        {
            //FIXME
        }

        public static bool shouldUpdateSourceFileRelativePaths()
        {
            return false;
        }

        public static IBPlugin plugin()
        {
            NSException.raise("plugin is abstract", "");
            return null;
        }

        public NSMutableDictionary ListOfReferenceId { get; protected set; }

        public Dictionary<NSString, Action<object>> UnresolvedReferences { get; protected set; }

        // Properties are in the same order as found inside a Xib
        public int SystemTarget { get; set; }

        public NSString SystemVersion { get; set; }

        public NSString InterfaceBuilderVersion { get; set; }

        public NSString AppKitVersion { get; set; }

        public NSString HIToolboxVersion { get; set; }

        public NSMutableDictionary PluginVersions { get; set; }

        public NSArray IntegratedClassDependencies { get; set; }

        public NSArray PluginDependencies { get; set; }

        public NSDictionary Metadata { get; set; }

        public NSMutableArray RootObjects { get; set; }

        public IBObjectContainer Objects { get; set; }

        public IBClassDescriber Classes { get; set; }

        public int LocalizationMode { get; set; }

        public NSString TargetRuntimeIdentifier { get; set; }

        public NSMutableDictionary PluginDeclaredDevelopmentDependencies { get; set; }

        public bool PluginDeclaredDependenciesTrackSystemTargetVersion { get; set; }

        public int DefaultPropertyAccessControl { get; set; }

        public NSMutableDictionary LastKnownImageSizes { get; set; }


        public virtual IBObjectContainer objectContainer() { return _storage.objectContainer; }
        public virtual void setObjectContainer(IBObjectContainer ibObjectContainer) 
        { 
            if (_storage.objectContainer == null)
            {
                _storage.objectContainer = (IBObjectContainer)ibObjectContainer.retain();
                //_storage.objectContainer.setDelegate(this);
                NSMutableArray objects = _storage.objectContainer.objects();

            }
        }

        public virtual void setTargetRuntime(IBTargetRuntime targetRuntime)  
        {
            if (_storage.targetRuntime != targetRuntime)
            {
                _storage.targetRuntime.release();
                _storage.targetRuntime = (IBTargetRuntime)targetRuntime.retain();
                NSDocument activeDocument = ((IBDocumentController)IBDocumentController.sharedDocumentController()).activeDocument();
                if (activeDocument == this)
                {
                    IBTargetRuntime.setLastActiveTargetRuntime(this.targetRuntime());
                }
            }
        }
        public virtual IBTargetRuntime targetRuntime() { return _storage.targetRuntime; }

        public virtual void setDocumentMetadata(NSDictionary metadata) 
        {
            if (_storage.metadata != metadata)
            {
                _storage.metadata.release();
                _storage.metadata = metadata; //metadata.mutableCopy();
            }
        }
        public virtual NSDictionary documentMetadata() { return _storage.metadata; }


        public virtual void setClassDescriber(IBClassDescriber classDescriber) 
        { 
            if (_storage.classDescriber == null)
            {
                _storage.classDescriber = (IBClassDescriber)classDescriber.retain();
                _storage.classDescriber.setDocument(this);
            }
        }
        public virtual IBClassDescriber classDescriber() { return _storage.classDescriber; }


        //protected virtual void setGenericReleaseCopy()

        public virtual void setLastSavedSystemVersion(NSString lastSavedSystemVersion) 
        {
            if (_storage.lastSavedSystemVersion != lastSavedSystemVersion)
            {
                _storage.lastSavedSystemVersion.release();
                _storage.lastSavedSystemVersion = lastSavedSystemVersion.copy();
            }
        }
        public virtual NSString lastSavedSystemVersion() { return (NSString)_storage.lastSavedSystemVersion.retain().autorelease(); }


        public virtual void setLastSavedInterfaceBuilderVersion(NSString lastSavedInterfaceBuilderVersion) 
        {
            if (_storage.lastSavedInterfaceBuilderVersion != lastSavedInterfaceBuilderVersion)
            {
                _storage.lastSavedInterfaceBuilderVersion.release();
                _storage.lastSavedInterfaceBuilderVersion = lastSavedInterfaceBuilderVersion.copy();
            } 
        }
        public virtual NSString lastSavedInterfaceBuilderVersion() { return (NSString)_storage.lastSavedInterfaceBuilderVersion.retain().autorelease(); }

       
        public virtual void setLastSavedAppKitVersion(NSString lastSavedAppKitVersion) 
        {
            if (_storage.lastSavedAppKitVersion != lastSavedAppKitVersion)
            {
                _storage.lastSavedAppKitVersion.release();
                _storage.lastSavedAppKitVersion = lastSavedAppKitVersion.copy();
            }  
        }
        public virtual NSString lastSavedAppKitVersion() { return (NSString)_storage.lastSavedAppKitVersion.retain().autorelease(); }


        public virtual void setLastSavedHIToolboxVersion(NSString lastSavedHIToolboxVersion)
        {
            if (_storage.lastSavedHIToolboxVersion != lastSavedHIToolboxVersion)
            {
                _storage.lastSavedHIToolboxVersion.release();
                _storage.lastSavedHIToolboxVersion = lastSavedHIToolboxVersion.copy();
            }
        }
        public virtual NSString lastSavedHIToolboxVersion() { return (NSString)_storage.lastSavedHIToolboxVersion.retain().autorelease(); }

        private NSDictionary _lastSavedpluginVersionsForDependedPlugins;
        public virtual void setLastSavedPluginVersionsForDependedPlugins(NSDictionary pluginVersions) 
        { 
            _lastSavedpluginVersionsForDependedPlugins = pluginVersions; 
        }

        public virtual NSDictionary lastSavedPluginVersionsForDependedPlugins() { return _lastSavedpluginVersionsForDependedPlugins; }

        private NSString _lastKnownRelativeProjectPath;
        public virtual void setLastKnownRelativeProjectPath(NSString lastKnownRelativeProjectPath) { _lastKnownRelativeProjectPath = lastKnownRelativeProjectPath; }
        public virtual NSString lastKnownRelativeProjectPath() { return _lastKnownRelativeProjectPath; }

        private NSMutableDictionary _lastKnownImageSizes;
        public virtual void setLastKnownImageSizes(NSMutableDictionary lastKnownImageSizes) { _lastKnownImageSizes = lastKnownImageSizes; }
        public virtual NSMutableDictionary lastKnownImageSizes() { return _lastKnownImageSizes; }

        private NSMutableArray _objectIDsToOpen;
        public virtual void setObjectIDsToOpen(NSMutableArray objectIDsToOpen) { _objectIDsToOpen = objectIDsToOpen; }
        public virtual NSMutableArray objectIDsToOpen() { return _objectIDsToOpen; }

        

        private int _localizationMode;
        public virtual void setLocalizationMode(int localizationMode) { _localizationMode = localizationMode; }
        public virtual int localizationMode() { return _localizationMode; }

        private int _defaultPropertyAccessControl;
        public virtual void setDefaultPropertyAccessControl(int defaultPropertyAccessControl) { _defaultPropertyAccessControl = defaultPropertyAccessControl; }
        public virtual int defaultPropertyAccessControl() { return _defaultPropertyAccessControl; }




        public virtual void setPluginDeclaredDependencies(NSMutableDictionary pluginDeclaredDeps, int category)
        {
            IBTargetVersionDependencySet targetVersionDepSet = null;
            if (category == 0)
                targetVersionDepSet = _storage.pluginDeclaredDependenciesCat0;
            else if (category == 1)
                targetVersionDepSet = _storage.pluginDeclaredDependenciesCat1;

            targetVersionDepSet.setPluginDeclaredDependencies(pluginDeclaredDeps);
        }

        public virtual void setPluginDeclaredDependencyDefaults(NSMutableDictionary pluginDeclaredDeps, int category)
        {
            IBTargetVersionDependencySet targetVersionDepSet = null;
            if (category == 0)
                targetVersionDepSet = _storage.pluginDeclaredDependenciesCat0;
            else if (category == 1)
                targetVersionDepSet = _storage.pluginDeclaredDependenciesCat1;

            targetVersionDepSet.setPluginDeclaredDependencyDefaults(pluginDeclaredDeps);
        }


        
        public virtual void setPreviousXmlDecoderHints(NSMutableDictionary previousXmlDecoderHints)
        {
            if (_storage.previousXmlDecoderHints != previousXmlDecoderHints)
            {
                _storage.previousXmlDecoderHints.release();
                _storage.previousXmlDecoderHints = (NSMutableDictionary)previousXmlDecoderHints.retain();
            }
            
        }

        public virtual void setClassNameThatPreventedDecode(NSString classNameThatPreventedDecode)
        {
            if (_storage.classNameThatPreventedDecode != classNameThatPreventedDecode)
            {
                _storage.classNameThatPreventedDecode.release();
                _storage.classNameThatPreventedDecode = classNameThatPreventedDecode.copy();
            }
            
        }
        
        public virtual void willDecodeWithKeyedDecoder(NSKeyedUnarchiver decoder)
        {
            this.setClassNameThatPreventedDecode(null);
            decoder.setDelegate(this);
        }

        public virtual bool resolvePluginDependencies(NSArray plugins, ref NSError error)
        {
            return true;
        }

        //targetRuntimeWithIdentifier:fromDocumentOfType:error:
        protected virtual IBTargetRuntime targetRuntimeWithIdentifier(NSString targetRuntimeID, NSString type, ref NSError error)
        {
            IBTargetRuntime targetRuntime = null;

            if (targetRuntimeID != null)
            {
                targetRuntime = IBTargetRuntime.targetRuntimeWithIdentifier(targetRuntimeID);
                if (targetRuntime == null)
                {
                    NSString id = ((IBPlugin)Objc.MsgSend(Class, "plugin")).userPresentableIdentifierForTargetRuntimeIdentifier(targetRuntimeID);
                    NSString errMsg = NSString.stringWithFormat("This version of Coconut Builder does not support documents targeting %@.", id);
                    //
                }
            }
            else
            {
                targetRuntime = (IBTargetRuntime)Objc.MsgSend(Class, "defaultTargetRuntime");
            }
            
            IBPlugin plugin = (IBPlugin)Objc.MsgSend(Class, "plugin");
            if(!plugin.supportedTargetRuntimes().containsObject(targetRuntime))
            {
                targetRuntime = null;
                //Error...
            }


            return targetRuntime ;
        }

        protected static IBCFMutableDictionary _unarchiversToDocumentContexts = null;
        protected static id _IBDocumentContextForUnarchiver(NSCoder decoder)
        {
            if (_unarchiversToDocumentContexts == null)
            {
                _unarchiversToDocumentContexts = (IBCFMutableDictionary)alloc().init();
            }

            return _unarchiversToDocumentContexts.objectForKey(decoder);
        }



        
        public virtual int documentWindowUsesStaticOrigin()
        {
            int result = _documentWindowUsesStaticOrigin;
            if (result == -1)
            {
                NSUserDefaults defaults = NSUserDefaults.standardUserDefaults();
                if (defaults.hasPreferenceForKey("IBDocument.DocumentWindowUsesStaticOrigin"))
                    result = defaults.boolForKey("IBDocument.DocumentWindowUsesStaticOrigin") ? 1 : 0;
                else
                    result = 0;
            }

            return result;
        }

        

#if true
        public override bool readFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            NSData data = fileWrapper.regularFileContents();
            if (data != null)
            {
                var unarc = GSXibKeyedUnarchiver.alloc().initForReadingWithData(data);
                SystemTarget = unarc.decodeIntForKey(@"IBDocument.SystemTarget");
                SystemVersion = (NSString)unarc.decodeObjectForKey(@"IBDocument.SystemVersion");
                InterfaceBuilderVersion = (NSString)unarc.decodeObjectForKey(@"IBDocument.InterfaceBuilderVersion");
                AppKitVersion = (NSString)unarc.decodeObjectForKey(@"IBDocument.AppKitVersion");
                HIToolboxVersion = (NSString)unarc.decodeObjectForKey(@"IBDocument.HIToolboxVersion");
                PluginVersions = (NSMutableDictionary)unarc.decodeObjectForKey(@"IBDocument.PluginVersions");
                IntegratedClassDependencies = (NSArray)unarc.decodeObjectForKey(@"IBDocument.IntegratedClassDependencies");
                PluginDependencies = (NSArray)unarc.decodeObjectForKey(@"IBDocument.PluginDependencies");
                Metadata = (NSMutableDictionary)unarc.decodeObjectForKey(@"IBDocument.Metadata");
                RootObjects = (NSMutableArray)unarc.decodeObjectForKey(@"IBDocument.RootObjects");
                Objects = (IBObjectContainer)unarc.decodeObjectForKey(@"IBDocument.Objects");
                Classes = (IBClassDescriber)unarc.decodeObjectForKey(@"IBDocument.Classes");
                LocalizationMode = unarc.decodeIntForKey(@"IBDocument.LocalizationMode");
                TargetRuntimeIdentifier = (NSString)unarc.decodeObjectForKey(@"IBDocument.TargetRuntimeIdentifier");
                PluginDeclaredDevelopmentDependencies = (NSMutableDictionary)unarc.decodeObjectForKey(@"IBDocument.PluginDeclaredDevelopmentDependencies");
                PluginDeclaredDependenciesTrackSystemTargetVersion = unarc.decodeBoolForKey(@"IBDocument.PluginDeclaredDependenciesTrackSystemTargetVersion");
                DefaultPropertyAccessControl = unarc.decodeIntForKey(@"IBDocument.DefaultPropertyAccessControl");
                LastKnownImageSizes = (NSMutableDictionary)unarc.decodeObjectForKey(@"IBDocument.LastKnownImageSizes");
            }

            return true;
        }
#else

        public virtual void setVersion(NSNumber aNumber, id pluginId, int cat)
        {

        }




        public virtual NSError decodeDocumentOfType(NSString typeName, NSCoder decoder)
        {
            NSError error = null;

            if (decoder.containsValueForKey("IBDocument.PluginDependencies") ||
                decoder.containsValueForKey("IBDocument.PaletteDependencies"))
            {
                NSArray pluginDeps = (NSArray)decoder.decodeObjectForKey("IBDocument.PluginDependencies");
                if (pluginDeps == null)
                {
                    pluginDeps = (NSArray)decoder.decodeObjectForKey("IBDocument.PaletteDependencies");
                }

                if (this.resolvePluginDependencies(pluginDeps, ref error))
                {
                    NSString targetRuntimeId = (NSString)decoder.decodeObjectForKey("IBDocument.TargetRuntimeIdentifier");
                    IBTargetRuntime targetRuntime = this.targetRuntimeWithIdentifier(targetRuntimeId, typeName, ref error);
                    if (error == null)
                    {
                        NSMutableDictionary lastKnowImagesSizes = (NSMutableDictionary)decoder.decodeObjectForKey("IBDocument.LastKnownImageSizes");
                        if (lastKnowImagesSizes != null)
                        {
                            NSMutableDictionary docContext = (NSMutableDictionary)_IBDocumentContextForUnarchiver(decoder);
                            docContext.setObjectForKey(lastKnowImagesSizes, (NSString)"IBDocumentImageResourceNamesToSizesMap");
                        }

                        this.setTargetRuntime(targetRuntime);
                        this.setObjectIDsToOpen((NSMutableArray)decoder.decodeObjectForKey("IBDocument.EditedObjectIDs"));
                        NSDictionary meta = (NSDictionary)decoder.decodeObjectForKey("IBDocument.Metadata");
                        this.setDocumentMetadata((meta != null) ? meta : NSDictionary.dictionary());
                        this.setClassDescriber((IBClassDescriber)decoder.decodeObjectForKey(@"IBDocument.Classes"));
                        this.setLastSavedSystemVersion((NSString)decoder.decodeObjectForKey(@"IBDocument.SystemVersion"));
                        this.setLastSavedInterfaceBuilderVersion((NSString)decoder.decodeObjectForKey(@"IBDocument.InterfaceBuilderVersion"));
                        this.setLastSavedAppKitVersion((NSString)decoder.decodeObjectForKey(@"IBDocument.AppKitVersion"));
                        this.setLastSavedHIToolboxVersion((NSString)decoder.decodeObjectForKey(@"IBDocument.HIToolboxVersion"));
                        this.setLastSavedPluginVersionsForDependedPlugins((NSDictionary)decoder.decodeObjectForKey(@"IBDocument.PluginVersions"));
                        this.setLastKnownRelativeProjectPath((NSString)decoder.decodeObjectForKey(@"IBDocument.LastKnownRelativeProjectPath"));
                        this.setPluginDeclaredDependencies((NSMutableDictionary)decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDependencies"), 0);
                        this.setPluginDeclaredDependencyDefaults((NSMutableDictionary)decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDependencyDefaults"), 0);
                        this.setPluginDeclaredDependencies((NSMutableDictionary)decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDevelopmentDependencies"), 1);
                        this.setPluginDeclaredDependencyDefaults((NSMutableDictionary)decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDevelopmentDependencyDefaults"), 1);
                        int lastSavedIBVersion = this.lastSavedInterfaceBuilderVersion().integerValue();
                        if (lastSavedIBVersion <= 714)
                        {
                            NSNumber version = NSNumber.numberWithInteger(3000);
                            IBPlugin plugin = (IBPlugin)Objc.MsgSend(Class, "plugin");
                            this.setVersion(version, plugin.primaryDeclaredDependencyIdentifierForCategory(1), 1);
                        }

                        bool pluginDeclaredDepsTrackSV = decoder.decodeBoolForKey(@"IBDocument.PluginDeclaredDependenciesTrackSystemTargetVersion");
                        if (!pluginDeclaredDepsTrackSV && decoder.containsValueForKey(@"IBDocument.SystemTarget"))
                        {
                            NSNumber sysTargetVersion = NSNumber.numberWithInteger(decoder.decodeIntegerForKey(@"IBDocument.SystemTarget"));
                            IBPlugin plugin = (IBPlugin)Objc.MsgSend(Class, "plugin");
                            this.setVersion(sysTargetVersion, plugin.primaryDeclaredDependencyIdentifierForCategory(0), 0);
                        }
                        this.setLocalizationMode(decoder.decodeIntegerForKey(@"IBDocument.SystemTarget"));

                        if (decoder.containsValueForKey("IBDocument.defaultPropertyAccessControl"))
                        {
                            this.setDefaultPropertyAccessControl(decoder.decodeIntegerForKey(@"IBDocument.defaultPropertyAccessControl"));
                        }

                        if (Convert.ToBoolean(this.documentWindowUsesStaticOrigin()))
                        {
                            NSPoint origin = decoder.decodePointForKey("IBDocument.DocumentWindowStaticOrigin");
                            //this.setDocumentWindowStaticOrigin();
                        }
                        else
                        {
                            //this.setDocumentWindowStaticOrigin();
                        }

                        if (error == null)
                        {
                            IBObjectContainer ibObjContainer = (IBObjectContainer)decoder.decodeObjectForKey(@"IBDocument.Objects");
                            this.setObjectContainer((ibObjContainer != null) ? ibObjContainer : (IBObjectContainer)IBObjectContainer.alloc().init());
                        }
                        else
                        {
                            //return
                        }
                    }
                }
            }
            else
            {

            }

            return error;
        }

        public virtual NSDictionary additionalInstantiationInformation()
        {
            return (NSDictionary)NSDictionary.alloc().init();
        }

        public override bool readFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            //IBAsyncXMLDecoderWrapper asyncXMLDecoder = null;
            //NSDictionary additionalInstanceInfo = this.additionalInstantiationInformation();
            //asyncXMLDecoder = (IBAsyncXMLDecoderWrapper)additionalInstanceInfo.objectForKey((NSString)"IBAsyncXMLDecoderWrapper");
            

            NSData data = fileWrapper.regularFileContents();
            IBXMLDecoder decoder = (IBXMLDecoder)IBXMLDecoder.alloc().initForReadingWithData(data, ref outError);
            if (decoder != null)
            {
                this.willDecodeWithKeyedDecoder(decoder);
                outError = this.decodeDocumentOfType(typeName, decoder);
                decoder.finishDecoding();
                this.setPreviousXmlDecoderHints(decoder.hintsForFutureXMLCoder());
            }
            this.undoManager().removeAllActions();
            
            return outError == null;
        }


#endif
    }

   

}
