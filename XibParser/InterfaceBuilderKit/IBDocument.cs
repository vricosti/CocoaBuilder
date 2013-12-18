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
    internal struct IBDocumentStorage
    {
        id objectContainer; //0x10
    }


    public class IBDocument : NSDocument
    {
        new public static Class Class = new Class(typeof(IBDocument));
        new public static IBDocument alloc() { return new IBDocument(); }

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

        protected IBObjectContainer _ibObjectContainer;
        public virtual void setObjectContainer(IBObjectContainer ibObjectContainer) { _ibObjectContainer = ibObjectContainer; }

        private NSString _targetRuntime;
        public virtual void setTargetRuntime(NSString targetRuntime)  { _targetRuntime = targetRuntime; }
        public virtual NSString targetRuntime() { return _targetRuntime; }

        private NSDictionary _metadata;
        public virtual void setDocumentMetadata(NSDictionary metadata) { _metadata = metadata; }
        public virtual NSDictionary documentMetadata() { return _metadata; }

        private IBClassDescriber _classDescriber;
        public virtual void setClassDescriber(IBClassDescriber classDescriber) { _classDescriber = classDescriber; }
        public virtual IBClassDescriber classDescriber() { return _classDescriber; }
            
        private NSString _systemVersion;
        public virtual void setLastSavedSystemVersion(NSString systemVersion) { _systemVersion = systemVersion; }
        public virtual NSString lastSavedSystemVersion() { return _systemVersion; }

        private NSString _interfaceBuilderVersion;
        public virtual void setLastSavedInterfaceBuilderVersion(NSString interfaceBuilderVersion) { _interfaceBuilderVersion = interfaceBuilderVersion; }
        public virtual NSString lastSavedInterfaceBuilderVersion() { return _interfaceBuilderVersion; }

        private NSString _appKitVersion;
        public virtual void setLastSavedAppKitVersion(NSString appKitVersion) { _appKitVersion = appKitVersion; }
        public virtual NSString lastSavedAppKitVersion() { return _appKitVersion; }

        private NSString _HIToolboxVersion;
        public virtual void setLastSavedHIToolboxVersion(NSString HIToolboxVersion) { _HIToolboxVersion = HIToolboxVersion; }
        public virtual NSString lastSavedHIToolboxVersion() { return _HIToolboxVersion; }

        private NSDictionary _pluginVersions;
        public virtual void setLastSavedPluginVersionsForDependedPlugins(NSDictionary pluginVersions) { _pluginVersions = pluginVersions; }
        public virtual NSDictionary lastSavedPluginVersionsForDependedPlugins() { return _pluginVersions; }

        private NSString _lastKnownRelativeProjectPath;
        public virtual void setLastKnownRelativeProjectPath(NSString lastKnownRelativeProjectPath) { _lastKnownRelativeProjectPath = lastKnownRelativeProjectPath; }
        public virtual NSString lastKnownRelativeProjectPath() { return _lastKnownRelativeProjectPath; }
        

        public virtual void setPluginDeclaredDependencies(NSArray pluginDeclaredDeps, int category)
        {

        }

        public virtual void setPluginDeclaredDependencyDefaults(NSArray pluginDeclaredDeps, int category)
        {

        }

        public virtual void setPluginDeclaredDependencyDefaults(NSArray pluginDeclaredDepsDefault)
        {

        }

        public virtual void setPreviousXmlDecoderHints(id xmlDecoder)
        {

        }

        //hintsForFutureXMLCoder


            //        function methImpl_IBDocument_readFromFileWrapper_ofType_error_ {
//    rdx = fileWrapper, 
//    rcx = type, 
//    r8 = error

//    r14 = r8;
//    var_0 = rcx;
//    r13 = rdx;
//    r12 = rdi;
//    r15 = *objc_msgSend;
//    rax = [rdi additionalInstantiationInformation];
//    rax = [rax objectForKey:@"IBAsyncXMLDecoderWrapper"];
//    rax = [rax initializedDecoder];
//    if (rax != 0x0) goto loc_12dee;
//    goto loc_12d9f;

//loc_12dee:
//    r15 = *objc_msgSend;
//    (r15)(r12, @selector(willDecodeWithKeyedDecoder:), rbx);
//    rax = (r15)(r12, @selector(decodeDocumentOfType:withCoder:), var_0, rbx);
//    *r14 = rax;
//    (r15)(rbx, @selector(finishDecoding));
//    rax = (r15)(rbx, @selector(hintsForFutureXMLCoder));
//    (r15)(r12, @selector(setPreviousXmlDecoderHints:), rax);

//loc_12e3f:
//    r15 = *objc_msgSend;
//    rax = (r15)(r12, @selector(undoManager));
//    (r15)(rax, @selector(removeAllActions));
//    rax = (*r14 == 0x0 ? 0xff : 0x0) & 0xff;
//    return rax;

//loc_12d9f:
//    r15 = *objc_msgSend;
//    rax = (r15)(r13, @selector(regularFileContents));
//    rbx = rax;
//    rax = [IBXMLDecoder alloc];
//    rax = (r15)(rax, @selector(initForReadingWithData:error:), rbx, r14);
//    rax = [rax autorelease];
//    if (rax == 0x0) goto loc_12e3f;
//    goto loc_12dee;
//}

        public virtual void willDecodeWithKeyedDecoder(NSKeyedUnarchiver decoder)
        {
            decoder.setDelegate(this);
        }

        public virtual bool resolvePluginDependencies(NSArray plugins, ref NSError error)
        {
            return true;
        }

        //targetRuntimeWithIdentifier:fromDocumentOfType:error:
        protected virtual bool targetRuntimeWithIdentifier(NSString id, NSString type, ref NSError error)
        {
            return true;
        }

        protected IBCFMutableDictionary unarchiversToDocumentContexts = null;
        public virtual id _IBDocumentContextForUnarchiver(NSCoder decoder)
        {
            id obj = null;

            if (unarchiversToDocumentContexts == null)
            {
                unarchiversToDocumentContexts = (IBCFMutableDictionary)alloc().init();
            }

            return unarchiversToDocumentContexts.objectForKey(decoder);
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
        public virtual bool decodeDocumentOfType(NSString typeName, NSCoder decoder)
        {
            bool ret = false;

            NSError error = null;

            if (decoder.containsValueForKey("IBDocument.PluginDependencies"))
            {
                NSArray pluginDeps = (NSArray)decoder.decodeObjectForKey("IBDocument.PluginDependencies");
                if (pluginDeps == null)
                {
                    pluginDeps = (NSArray)decoder.decodeObjectForKey("IBDocument.PaletteDependencies");
                }

                if (this.resolvePluginDependencies(pluginDeps, ref error) == false)
                    return false;
            }
            else
            {
                if (decoder.containsValueForKey("IBDocument.PaletteDependencies"))
                {

                }
                else
                {
                     if (decoder.containsValueForKey("GDocument.PluginDependencies"))
                     {
                         ret = false;
                     }
                }
            }

            NSString targetRuntimeId = (NSString)decoder.decodeObjectForKey("IBDocument.TargetRuntimeIdentifier");
            this.targetRuntimeWithIdentifier(targetRuntimeId, typeName, ref error);
            if (error == null)
            {
                NSMutableDictionary lastKnowImagesSizes = (NSMutableDictionary)decoder.decodeObjectForKey("IBDocument.LastKnownImageSizes");
                if (lastKnowImagesSizes != null)
                {
                    //eax = _IBDocumentContextForUnarchiver(decoder);
                    //[eax setObject:lastKnowImagesSizes forKey:@"IBDocumentImageResourceNamesToSizesMap"];
                }
                this.setTargetRuntime(targetRuntimeId);
                //this.setObjectIDsToOpen(decoder.decodeObjectForKey("IBDocument.EditedObjectIDs"));
                NSDictionary meta = (NSDictionary)decoder.decodeObjectForKey("IBDocument.Metadata");
                this.setDocumentMetadata((meta != null) ? meta : NSDictionary.dictionary());
                this.setClassDescriber((IBClassDescriber)decoder.decodeObjectForKey(@"IBDocument.Classes"));
                this.setLastSavedSystemVersion((NSString)decoder.decodeObjectForKey(@"IBDocument.SystemVersion"));
                this.setLastSavedInterfaceBuilderVersion((NSString)decoder.decodeObjectForKey(@"IBDocument.InterfaceBuilderVersion"));
                this.setLastSavedAppKitVersion((NSString)decoder.decodeObjectForKey(@"IBDocument.AppKitVersion"));
                this.setLastSavedHIToolboxVersion((NSString)decoder.decodeObjectForKey(@"IBDocument.HIToolboxVersion"));
                this.setLastSavedPluginVersionsForDependedPlugins((NSDictionary)decoder.decodeObjectForKey(@"IBDocument.PluginVersions"));
                this.setLastKnownRelativeProjectPath((NSString)decoder.decodeObjectForKey(@"IBDocument.LastKnownRelativeProjectPath"));
                this.setPluginDeclaredDependencies ((NSArray)decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDependencies"),0);
                this.setPluginDeclaredDependencyDefaults((NSArray)decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDependencyDefaults") ,0);
                this.setPluginDeclaredDependencies((NSArray)decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDevelopmentDependencies"), 1);
                this.setPluginDeclaredDependencyDefaults((NSArray)decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDevelopmentDependencyDefaults"), 1);
                int lastSavedVersion = this.lastSavedInterfaceBuilderVersion().integerValue();
                if (lastSavedVersion <= 714)
                { 
                    //    NSNumber version = NSNumber.numberWithInteger(3000);
                    //    esi = version;
                    //    eax = [var_self class];
                    //    eax = [eax plugin];
                    //    eax = [eax primaryDeclaredDependencyIdentifierForCategory:0x1];
                    //    [var_self setVersion:esi forPluginDeclaredDependency:eax forCategory:0x1];
                }

                bool pluginDeclaredDepsTrackSV = decoder.decodeBoolForKey(@"IBDocument.PluginDeclaredDependenciesTrackSystemTargetVersion");
                if (pluginDeclaredDepsTrackSV == false)
                {
                   if(decoder.containsValueForKey(@"IBDocument.SystemTarget") != false)
                    {

                    }
                }
                if(error == null)
                {
                    IBObjectContainer ibObjContainer = (IBObjectContainer)decoder.decodeObjectForKey(@"IBDocument.Objects");
                    this.setObjectContainer((ibObjContainer != null) ? ibObjContainer : (IBObjectContainer)IBObjectContainer.alloc().init() );
                    ret = true;
                }
                else
                {

                }

            }


            return ret;
        }

        public override bool readFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            bool ret = false;

            NSData data = fileWrapper.regularFileContents();
            IBXMLDecoder decoder = (IBXMLDecoder)IBXMLDecoder.alloc().initForReadingWithData(data, ref outError);
            if (decoder != null)
            {
                this.willDecodeWithKeyedDecoder(decoder);
                ret = this.decodeDocumentOfType(typeName, decoder);
                decoder.finishDecoding();

        //    (r15)(r12, @selector(setPreviousXmlDecoderHints:), (r15)(rbx, @selector(hintsForFutureXMLCoder));
            }

            return ret;
        }


#endif
    }

   

}
