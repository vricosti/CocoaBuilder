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

        public NSMutableDictionary Metadata { get; set; }

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

        //public override bool ReadFromFileWrapper2(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        //{
        //}
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
        
    }
}
