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
using System.Xml.Linq;
using System.Xml.XPath;

namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/GSXibLoader.m
    public class WSXibKeyedUnarchiver : NSKeyedUnarchiver, INSXMLParser
    {
        public NSMutableDictionary Objects { get; set; }
        
        public NSMutableArray Stack { get; set; }
        
        public XElement CurrentElement { get; set; }

        public NSMutableDictionary Decoded { get; set; }

        protected WSXibKeyedUnarchiver()
        {
        }

        public static WSXibKeyedUnarchiver Alloc()
        {
            return new WSXibKeyedUnarchiver();
        }

        private NSData PreProcessXib(NSData data)
        {
            NSData result = data;

            string xml = System.Text.Encoding.Default.GetString(data.Bytes);
            XDocument xDoc = XDocument.Parse(xml);

            NSMutableDictionary customClassDict = NSMutableDictionary.Alloc().Init();

            var customClassNodes = xDoc.XPathSelectElements("//dictionary[@key=\"flattenedProperties\"]/string[contains(@key,\"CustomClassName\")]", null);
            if (customClassNodes != null && customClassNodes.Count() == 1)
            {

            }
            else
            {
                var flatProps = xDoc.XPathSelectElements("//object[@key=\"flattenedProperties\"]", null);
                if (flatProps != null && flatProps.Count() == 1)
                {
                    var xmlKeys = xDoc.XPathSelectElements("//object[@key=\"flattenedProperties\"]/object[@key=\"dict.sortedKeys\"]/*", null).ToArray();
                    var xmlObjs = xDoc.XPathSelectElements("//object[@key=\"flattenedProperties\"]/object[@key=\"dict.values\"]/*", null).ToArray();

                    for (int index = 0; index < xmlKeys.Count(); index++)
                    {
                        var key = xmlKeys[index].Value;
                        if (key.Contains("CustomClassName"))
                        {
                            customClassDict.Add(key, xmlObjs[index].Value);
                        }
                    }
                }
            }

            if (customClassDict.Count > 0)
            {
                foreach (var kvp in customClassDict)
                {
                    string keyValue = ((string)kvp.Key).Replace(".CustomClassName", "");
                    string className = (string)kvp.Value;
                    string objectRecordXpath = string.Format("//object[@class=\"IBObjectRecord\"]/int[@key=\"objectID\"][text()=\"{0}\"]/../reference", keyValue);

                    var objectRecords = xDoc.XPathSelectElements(objectRecordXpath, null).ToArray();
                    if (objectRecords != null && objectRecords.Count() > 0)
                    {
                        foreach (var record in objectRecords)
                        {
                            if (record.AttributeValueOrDefault("key", "") == "object")
                            {
                                var refId = record.AttributeValueOrDefault("ref", "");
                                var refXpath = string.Format("//object[@id=\"{0}\"]", refId);
                                var classNodes = xDoc.XPathSelectElements(refXpath, null).ToArray();
                                if (classNodes != null && classNodes.Count() > 0)
                                {
                                    Class cls = Class.NSClassFromString(className);

                                    var classNode = classNodes[0];
                                    var classAttr = classNode.Attribute("class");
                                    classAttr.Value = className;

                                    if (cls != null)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }

            // ...
            return result;
        }


        public WSXibKeyedUnarchiver InitForReadingWithData(NSData data)
        {
            NSData theData = data;

            Objects = NSMutableDictionary.Alloc().Init();

            Stack = NSMutableArray.Alloc().Init();

            Decoded = NSMutableDictionary.Alloc().Init();

            if (NSClassWrapper.IsInInterfaceBuilder == false)
            {
                theData = PreProcessXib(data);
            }

            NSXMLParser theParser = NSXMLParser.Alloc().InitWithData(theData);
            theParser.SetDelegate(this);

            theParser.Parse();

            return this;
        }

        public void ParserFoundCharacters(NSXMLParser parser, string foundCharacters)
        {
            throw new NotImplementedException();
        }

        public void ParserDidStartElement(NSXMLParser parser, string elementName, string namespaceURI, string qualifiedName, NSDictionary attributeDict)
        {
            throw new NotImplementedException();
        }

        public void ParserDidEndElement(NSXMLParser parser, string elementName, string namespaceURI, string qualifiedName)
        {
            throw new NotImplementedException();
        }


       


        

        
    }
}
