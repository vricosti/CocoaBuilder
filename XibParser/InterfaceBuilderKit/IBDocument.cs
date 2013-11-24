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
        new public static IBDocument Alloc() { return new IBDocument(); }

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

        

        public override bool ReadFromFileWrapper(NSFileWrapper fileWrapper, NSString typeName, ref NSError outError)
        {
            NSData data = fileWrapper.RegularFileContents();
            if (data != null)
            {
                var unarc = GSXibKeyedUnarchiver.Alloc().InitForReadingWithData(data);
                SystemTarget = unarc.DecodeIntForKey(@"IBDocument.SystemTarget");
                SystemVersion = (NSString)unarc.DecodeObjectForKey(@"IBDocument.SystemVersion");
                InterfaceBuilderVersion = (NSString)unarc.DecodeObjectForKey(@"IBDocument.InterfaceBuilderVersion");
                AppKitVersion = (NSString)unarc.DecodeObjectForKey(@"IBDocument.AppKitVersion");
                HIToolboxVersion = (NSString)unarc.DecodeObjectForKey(@"IBDocument.HIToolboxVersion");
                PluginVersions = (NSMutableDictionary)unarc.DecodeObjectForKey(@"IBDocument.PluginVersions");
                IntegratedClassDependencies = (NSArray)unarc.DecodeObjectForKey(@"IBDocument.IntegratedClassDependencies");
                PluginDependencies = (NSArray)unarc.DecodeObjectForKey(@"IBDocument.PluginDependencies");
                Metadata = (NSMutableDictionary)unarc.DecodeObjectForKey(@"IBDocument.Metadata");
                RootObjects = (NSMutableArray)unarc.DecodeObjectForKey(@"IBDocument.RootObjects");
                Objects = (IBObjectContainer)unarc.DecodeObjectForKey(@"IBDocument.Objects");
                Classes = (IBClassDescriber)unarc.DecodeObjectForKey(@"IBDocument.Classes");
                LocalizationMode = unarc.DecodeIntForKey(@"IBDocument.LocalizationMode");
                TargetRuntimeIdentifier = (NSString)unarc.DecodeObjectForKey(@"IBDocument.TargetRuntimeIdentifier");
                PluginDeclaredDevelopmentDependencies = (NSMutableDictionary)unarc.DecodeObjectForKey(@"IBDocument.PluginDeclaredDevelopmentDependencies");
                PluginDeclaredDependenciesTrackSystemTargetVersion = unarc.DecodeBoolForKey(@"IBDocument.PluginDeclaredDependenciesTrackSystemTargetVersion");
                DefaultPropertyAccessControl = unarc.DecodeIntForKey(@"IBDocument.DefaultPropertyAccessControl");
                LastKnownImageSizes = (NSMutableDictionary)unarc.DecodeObjectForKey(@"IBDocument.LastKnownImageSizes");
            }

            return true;
        }
        
    }
}
