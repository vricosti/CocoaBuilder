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
    public class IBCocoaDocument : IBDocument
    {
        new public static Class Class = new Class(typeof(IBCocoaDocument));
        new public static IBDocument Alloc() { return new IBCocoaDocument(); }

        //public virtual override InitForURL(NSURL forUrl, NSURL url, NSString type, ref NSError error)
        //{

        //}
        

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
