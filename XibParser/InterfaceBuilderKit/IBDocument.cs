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


        public virtual bool decodeDocumentOfType(NSString typeName, NSCoder decoder)
        {
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
                //this.setTargetRuntime(targetRuntimeId);
                //this.setObjectIDsToOpen(decoder.decodeObjectForKey("IBDocument.EditedObjectIDs"));
                //NSDictionary meta = (NSDictionary)decoder.decodeObjectForKey("IBDocument.Metadata");
                //this.setDocumentMetadata((meta != null) ? meta : NSDictionary.dictionary());
                //this.setClassDescriber(decoder.decodeObjectForKey(@"IBDocument.Classes"));
                //this.setLastSavedSystemVersion(decoder.decodeObjectForKey(@"IBDocument.SystemVersion"));
                //this.setLastSavedInterfaceBuilderVersion(decoder.decodeObjectForKey(@"IBDocument.InterfaceBuilderVersion"));
                //this.setLastSavedAppKitVersion(decoder.decodeObjectForKey(@"IBDocument.AppKitVersion"));
                //this.setLastSavedHIToolboxVersion(decoder.decodeObjectForKey(@"IBDocument.HIToolboxVersion"));
                //this.setLastSavedPluginVersionsForDependedPlugins(decoder.decodeObjectForKey(@"IBDocument.PluginVersions"));
                //this.setLastKnownRelativeProjectPath(decoder.decodeObjectForKey(@"IBDocument.LastKnownRelativeProjectPath"));
                //this.setPluginDeclaredDependencies(decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDependencies"),0));
                //this.setPluginDeclaredDependencyDefaults(decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDependencyDefaults") ,0));
                //this.setPluginDeclaredDependencies(decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDevelopmentDependencies"), 1));
                //this.setPluginDeclaredDependencyDefaults(decoder.decodeObjectForKey(@"IBDocument.PluginDeclaredDevelopmentDependencyDefaults"), 1));
                //int lastSavedVersion = this.lastSavedInterfaceBuilderVersion().integerValue();
            }


            return false;
        }

#if true
        public override bool readFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            //IBXMLDecoder xmlDec = (IBXMLDecoder)(IBXMLDecoder.alloc()).initForReadingWithData(fileWrapper.regularFileContents(), null);

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
        public override bool readFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            bool ret = false;

            NSData data = fileWrapper.regularFileContents();
            IBXMLDecoderApple decoder = (IBXMLDecoderApple)IBXMLDecoderApple.alloc().initForReadingWithData(data, ref outError);
            if (decoder != null)
            {
                this.willDecodeWithKeyedDecoder(decoder);
                ret = this.decodeDocumentOfType(typeName, decoder);
                decoder.finishDecoding();
            }

            return ret;
        }


#endif
    }

   

}
