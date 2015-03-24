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
        new public static IBDocument alloc() { return new IBCocoaDocument(); }

        //public virtual override InitForURL(NSURL url, NSURL contentsUrl, NSString type, ref NSError error)
        //{

        //}
        

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
        
    }
}
