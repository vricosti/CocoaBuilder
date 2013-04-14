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
    public class IBDocument
    {
        public NSMutableDictionary ListOfReferenceId { get; protected set; }

        public Dictionary<string, Action<object>> UnresolvedReferences { get; protected set; }

        // Properties are in the same order as found inside a Xib
        public int SystemTarget { get; set; }

        public string SystemVersion { get; set; }

        public string InterfaceBuilderVersion { get; set; }

        public string AppKitVersion { get; set; }

        public string HIToolboxVersion { get; set; }

        public NSMutableDictionary PluginVersions { get; set; }

        public NSArray IntegratedClassDependencies { get; set; }

        public NSArray PluginDependencies { get; set; }

        public NSMutableDictionary Metadata { get; set; }

        public NSMutableArray RootObjects { get; set; }

        public IBObjectContainer Objects { get; set; }

        public IBClassDescriber Classes { get; set; }

        public int LocalizationMode { get; set; }

        public string TargetRuntimeIdentifier { get; set; }

        public NSMutableDictionary PluginDeclaredDevelopmentDependencies { get; set; }

        public bool PluginDeclaredDependenciesTrackSystemTargetVersion { get; set; }

        public int DefaultPropertyAccessControl { get; set; }

        public NSMutableDictionary LastKnownImageSizes { get; set; }


        public IBDocument()
        {
            this.ListOfReferenceId = new NSMutableDictionary();
            this.UnresolvedReferences = new Dictionary<string, Action<object>>();
        }


        public static void Parse(IBDocument ibDocument, XElement xElement)
        {
            NSObjectDecoder nsObjDecoder = new NSObjectDecoder(ibDocument, xElement);

            string keyVal = xElement.Attribute("key").Value.Substring(11);
            switch (keyVal)
            {
                case "SystemTarget":
                    {
                        //ibDocument.SystemTarget = (NSNumber)nsObjDecoder.Create(xElement);
                        ibDocument.SystemTarget = (NSNumber)nsObjDecoder.Create();
                    }
                    break;
                case "SystemVersion":
                    {
                        ibDocument.SystemVersion = (NSString)nsObjDecoder.Create();
                    }
                    break;
                case "InterfaceBuilderVersion":
                    {
                        ibDocument.InterfaceBuilderVersion = (NSString)nsObjDecoder.Create();
                    }
                    break;
                case "AppKitVersion":
                    {
                        ibDocument.AppKitVersion = (NSString)nsObjDecoder.Create();
                    }
                    break;
                case "HIToolboxVersion":
                    {
                        ibDocument.HIToolboxVersion = (NSString)nsObjDecoder.Create();
                    }
                    break;
                case "PluginVersions":
                    {
                        ibDocument.PluginVersions = (NSMutableDictionary)nsObjDecoder.Create();
                    }
                    break;
                case "IntegratedClassDependencies":
                    {
                        ibDocument.IntegratedClassDependencies = (NSArray)nsObjDecoder.Create();
                    }
                    break;
                case "PluginDependencies":
                    {
                        ibDocument.PluginDependencies = (NSArray)nsObjDecoder.Create();
                    }
                    break;
                case "Metadata":
                    {
                        ibDocument.Metadata = (NSMutableDictionary)nsObjDecoder.Create();
                    }
                    break;
                case "RootObjects":
                    {
                        ibDocument.RootObjects = (NSMutableArray)nsObjDecoder.Create();
                        ibDocument.ResolveReferences();
                    }
                    break;
                case "Objects":
                    {
                        ibDocument.Objects = (IBObjectContainer)nsObjDecoder.Create();
                    }
                    break;
                case "Classes":
                    {
                        ibDocument.Classes = (IBClassDescriber)nsObjDecoder.Create();
                    }
                    break;
                case "localizationMode":
                    {
                        ibDocument.LocalizationMode = (NSNumber)nsObjDecoder.Create();
                    }
                    break;
                case "TargetRuntimeIdentifier":
                    {
                        ibDocument.TargetRuntimeIdentifier = (NSString)nsObjDecoder.Create();
                    }
                    break;
                case "PluginDeclaredDevelopmentDependencies":
                    {
                        ibDocument.PluginDeclaredDevelopmentDependencies = (NSMutableDictionary)nsObjDecoder.Create();
                    }
                    break;
                case "PluginDeclaredDependenciesTrackSystemTargetVersion":
                    {
                        ibDocument.PluginDeclaredDependenciesTrackSystemTargetVersion = (NSNumber)nsObjDecoder.Create();
                    }
                    break;
                case "defaultPropertyAccessControl":
                     {
                         ibDocument.DefaultPropertyAccessControl = (NSNumber)nsObjDecoder.Create();
                    }
                    break;
                case "LastKnownImageSizes":
                    {
                        ibDocument.LastKnownImageSizes = (NSMutableDictionary)nsObjDecoder.Create();
                    }
                    break;
                    
                default:
                    System.Diagnostics.Debug.WriteLine("IBDocument : unknown key " + keyVal);
                    break;
            }
        }

        private void ResolveReferences()
        {
            foreach (var kvp in UnresolvedReferences)
            {
                string refId = kvp.Key;
                Action<object> propAction = kvp.Value;

                object nsObj = null;
                if (ListOfReferenceId.TryGetValue(refId, out nsObj))
                {
                    propAction(nsObj);
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine(string.Format(""))
                    //throw new Exception("Invalid Reference identifier");
                }
            }
        }
    }
}
