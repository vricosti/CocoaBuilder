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

namespace Smartmobili.Cocoa
{
    public class NSObjectDecoder
    {
        //private Dictionary<string, XElement> _classDict = new Dictionary<string, XElement>();
        private List<XElement> _classDict = new List<XElement>();

        public IBDocument Document { get; set; }

        public XElement XmlElement { get; set; }

        public bool AllowsKeyedCoding { get; set; }

        public NSObjectDecoder()
        {

        }


        public NSObjectDecoder(IBDocument ibDocument, XElement xElement)
        {
            Document = ibDocument;
            XmlElement = xElement;
            AllowsKeyedCoding = true;
            
            createDictionary(xElement);
            
        }

        private void createDictionary(XElement xElement)
        {
            if (xElement == null)
                return;

            //var posts = from item in xElement.Descendants().Where(n => (string)n.Attribute("key") != null)
            //            select new
            //            {
            //                Title = (string)item.Element("title"),
            //                Published = (DateTime)item.Element("pubDate")
            //            };

            _classDict = xElement.Elements().Where(n => (string)n.Attribute("key") != null).ToList();

            //Dictionary<string, XElement> test = xElement.Descendants()
            //    .Where<XElement>(n => (string)n.Attribute("key") != null)
            //    .ToDictionary(
            //    n => n.Attribute("key").Value.ToString(),
            //    n => n);


            //var keys = xElement.Descendants().Where(n => (string)n.Attribute("key") != null).Select;
        }


        public object create(XElement xCurrentElement = null)
        {
            id nsObj = null;

            //XmlElement = (xCurrentElement != null) ? xCurrentElement : XmlElement;
            //var xElement = XmlElement;

            var xElement = (xCurrentElement != null) ? xCurrentElement : XmlElement;

            NSString attrClass = string.Empty;
            //string key = (xElement.Attribute("key") != null) ? xElement.Attribute("key").Value : null;
            NSString id = (xElement.Attribute("id") != null) ? xElement.Attribute("id").Value : null;

            switch (xElement.Name.LocalName)
            {
                case "nil": { nsObj = null; break; }

                case "integer": { nsObj = new NSNumber(Convert.ToInt32(xElement.Attribute("value").Value)); break; }
                case "int": { nsObj = new NSNumber(Convert.ToInt32(xElement.Value)) { }; break; }
                
                case "string": { nsObj = new NSString(xElement.Value) { }; break; }
                case "bool": { nsObj = new NSNumber(xElement.Value.ConvertFromYesNo()) { }; break; }

                case "array":
                    {
                        attrClass = xElement.AttributeValueOrDefault("class", "NSArray");
                        nsObj = (id)createFromClassName(xElement, attrClass);
                        break;
                    }

                case "dictionary":
                    {
                        attrClass = xElement.AttributeValueOrDefault("class", "NSDictionary");
                        nsObj = (id)createFromClassName(xElement, attrClass);
                        break;
                    }

                case "object":
                    {
                        attrClass = xElement.AttributeValueOrDefault("class", string.Empty);
                        nsObj = (id)createFromClassName(xElement, attrClass);
                        break;
                    }

                case "reference":
                    {
                        NSString refId = xElement.AttributeValueOrDefault("ref", "");
                        bool foundRefID = Document.ListOfReferenceId.TryGetValue(refId, out nsObj);
                        if (!foundRefID)
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("Unknown ReferenceID {0}", refId));
                        }
                    }
                    break;
                default:

                    System.Diagnostics.Debug.WriteLine(string.Format("Unknown element {0}", xElement.Name.LocalName));
                    break;
            }

           

            return nsObj;
        }



        

        public object resolveReference(object instance, string propertyName, XElement xElement)
        {
            id nsObj = null;

            if (instance == null || string.IsNullOrEmpty(propertyName) || xElement == null)
                return null;

            NSString refId = xElement.AttributeValueOrDefault("ref", "");
            if (!string.IsNullOrWhiteSpace(refId))
            {
                bool foundRefID = Document.ListOfReferenceId.TryGetValue(refId, out nsObj);
                if (!foundRefID)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Unknown ReferenceID {0}", refId));
                    
                    System.Reflection.PropertyInfo prop = instance.GetType().GetProperty(propertyName);
                    Action<object> propAction = (Action<object>)Delegate.CreateDelegate(typeof(Action<object>), instance, prop.GetSetMethod());
                    Document.UnresolvedReferences.Add(refId, propAction);
                    
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Nil Reference"));
            }
            
            return nsObj;
        }



        private object createFromClassName(XElement xElement, string attrClass)
        {
            NSCoding2 nsObj = null;

            NSObjectDecoder decoder = new NSObjectDecoder(Document, xElement);

            System.Diagnostics.Debug.WriteLine(attrClass);

            Type t = Type.GetType("Smartmobili.Cocoa." + attrClass);
            if (t != null)
            {
                nsObj = Activator.CreateInstance(t) as NSCoding2;
                if (nsObj != null)
                {
                    //nsObj = (NSCoding2)nsObj.initWithCoder(decoder);   
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(string.Format(
                        "createFromClassName : this class name {0} doesn't implement NSCoding protocol", attrClass));
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                        string.Format("createFromClassName : Unknown <object> class name {0}", attrClass));
            }

            //switch (attrClass)
            //{
            //    case "IBActionConnection": { nsObj = new IBActionConnection(); nsObj.initWithCoder(decoder); break; }
            //    case "IBActionInfo": { nsObj = new IBActionInfo(); nsObj.initWithCoder(decoder); break; }
            //    case "IBBindingConnection": { nsObj = new IBBindingConnection(); nsObj.initWithCoder(decoder); break; }
            //    case "IBClassDescriber": { nsObj = new IBClassDescriber(); nsObj.initWithCoder(decoder); break; }
            //    case "IBClassDescriptionSource": { nsObj = new IBClassDescriptionSource(); nsObj.initWithCoder(decoder); break; }
            //    case "IBConnectionRecord": { nsObj = new IBConnectionRecord(); nsObj.initWithCoder(decoder); break; }
            //    case "IBMutableOrderedSet": { nsObj = new IBMutableOrderedSet(); nsObj.initWithCoder(decoder); break; }
            //    case "IBObjectContainer": { nsObj = new IBObjectContainer(); nsObj.initWithCoder(decoder); break; }
            //    case "IBObjectRecord": { nsObj = new IBObjectRecord(); nsObj.initWithCoder(decoder); break; }
            //    case "IBOutletConnection": { nsObj = new IBOutletConnection(); nsObj.initWithCoder(decoder); break; }
            //    case "IBPartialClassDescription": { nsObj = new IBPartialClassDescription(); nsObj.initWithCoder(decoder); break; }
            //    case "IBToOneOutletInfo": { nsObj = new IBToOneOutletInfo(); nsObj.initWithCoder(decoder); break; }

            //    case "NSArray": { nsObj = new NSArray(); nsObj.initWithCoder(decoder); break; }
            //    case "NSArrayController": { nsObj = new NSArrayController(); nsObj.initWithCoder(decoder); break; }
            //    case "NSBox": { nsObj = new NSBox(); nsObj.initWithCoder(decoder); break; }
            //    case "NSButton": { ;break; }
            //    case "NSButtonCell": { ;break; }
            //    case "NSClipView": { nsObj = new NSClipView(); nsObj.initWithCoder(decoder); break; }
            //    case "NSCollectionView": { nsObj = new NSCollectionView(); nsObj.initWithCoder(decoder); break; }
            //    case "NSCollectionViewItem": { nsObj = new NSCollectionViewItem(); nsObj.initWithCoder(decoder); break; }
            //    case "NSColor": { ;break; }
            //    case "NSCustomObject": { nsObj = new NSCustomObject(); nsObj.initWithCoder(decoder); break; }
            //    case "NSCustomResource": { nsObj = new NSCustomResource(); nsObj.initWithCoder(decoder); break; }
            //    case "NSCustomView": { nsObj = new NSCustomView(); nsObj.initWithCoder(decoder); break; }
            //    case "NSFont": { ;break; }
            //    case "NSMenu": { nsObj = new NSMenu(); nsObj.initWithCoder(decoder); break; }
            //    case "NSMenuItem": { nsObj = new NSMenuItem(); nsObj.initWithCoder(decoder); break; }
            //    case "NSMutableArray": { nsObj = new NSMutableArray(); nsObj.initWithCoder(decoder); break; }
            //    case "NSMutableDictionary": { nsObj = new NSMutableDictionary(); nsObj.initWithCoder(decoder); break; }
            //    case "NSMutableString": { nsObj = new NSMutableString(); nsObj.initWithCoder(decoder); break; }
            //    case "NSNibBindingConnector": { nsObj = new NSNibBindingConnector(); nsObj.initWithCoder(decoder); break; }
            //    case "NSPopUpButton": { ;break; }
            //    case "NSPopUpButtonCell": { ;break; }
            //    case "NSScroller": { nsObj = new NSScroller(); nsObj.initWithCoder(decoder); break; }
            //    case "NSScrollView": { nsObj = new NSScrollView(); nsObj.initWithCoder(decoder); break; }
            //    case "NSSegmentedCell": { ;break; }
            //    case "NSSegmentedControl": { ;break; }
            //    case "NSSegmentItem": { ;break; }
            //    case "NSTextField": { nsObj = new NSTextField(); nsObj.initWithCoder(decoder); break; }
            //    case "NSTextFieldCell": { nsObj = new NSTextFieldCell(); nsObj.initWithCoder(decoder); break; }
            //    case "NSToolbar": { nsObj = new NSToolbar(); nsObj.initWithCoder(decoder); break; }
            //    case "NSToolbarFlexibleSpaceItem": { nsObj = new NSToolbarFlexibleSpaceItem(); nsObj.initWithCoder(decoder); break; }
            //    case "NSToolbarItem": { nsObj = new NSToolbarItem(); nsObj.initWithCoder(decoder); break; }
            //    case "NSToolbarSeparatorItem": { nsObj = new NSToolbarSeparatorItem(); nsObj.initWithCoder(decoder); break; }
            //    case "NSToolbarSpaceItem": { nsObj = new NSToolbarSpaceItem(); nsObj.initWithCoder(decoder); break; }
            //    case "NSView": { nsObj = new NSView(); nsObj.initWithCoder(decoder); break; }
            //    case "NSViewController": { nsObj = new NSViewController(); nsObj.initWithCoder(decoder); break; }
            //    case "NSWindowTemplate": { nsObj = new NSWindowTemplate(); nsObj.initWithCoder(decoder); break; }
            //    case "NSWindowView": { ;break; }

            //    default:
            //        System.Diagnostics.Debug.WriteLine(
            //            string.Format("Unknown <object> class name {0}", attrClass));
            //        break;
            //}

            return nsObj;
        }


        public id decodeObjectForKey(string keyName)
        {
            id nsObj = null;

            XElement xElm = _classDict.Find(i => (string)i.Attribute("key") == keyName);
            if (xElm != null)
            {
                nsObj = (id)create(xElm);
            }

            return nsObj;
        }


        public void decodeValueOfObjCType(string valueType, ref object data)
        {

        }

        public bool containsValueForKey(NSString keyName)
        {
            return _classDict.Exists(i => (string)i.Attribute("key") == keyName);
        }

        public NSPoint decodePointForKey(string keyName)
        {
            NSPoint nsPoint = new NSPoint();

            XElement xElm = _classDict.Find(i => (string)i.Attribute("key") == keyName);
            if (xElm != null)
            {
                nsPoint = (NSPoint)(NSString)this.create(xElm);
            }

            return nsPoint;
        }

        public NSSize decodeSizeForKey(string keyName)
        {
            NSSize nsSize = new NSSize();

            XElement xElm = _classDict.Find(i => (string)i.Attribute("key") == keyName);
            if (xElm != null)
            {
                nsSize = (NSSize)(NSString)this.create(xElm);
            }

            return nsSize;
        }
        public NSRect decodeRectForKey(string keyName)
        {
            NSRect nsRect = new NSRect();

            XElement xElm = _classDict.Find( i => (string)i.Attribute("key") == keyName);
            if (xElm != null)
            {
                nsRect = (NSRect)(NSString)this.create(xElm);
            }

            return nsRect;
        }

        public int decodeIntForKey(string keyName)
        {
            int ret = 0;

            XElement xElm = _classDict.Find(i => (string)i.Attribute("key") == keyName);
            if (xElm != null)
            {
                ret = (NSNumber)this.create(xElm);
            }

            return ret;
        }

        public bool decodeBoolForKey(string keyName)
        {
            bool ret = false;

            XElement xElm = _classDict.Find(i => (string)i.Attribute("key") == keyName);
            if (xElm != null)
            {
                ret = (NSNumber)this.create(xElm);
            }

            return ret;
        }


        //public static object create(XElement xElement)
        //{
        //    object nsObj = null;

        //    //string key = (xElement.Attribute("key") != null) ? xElement.Attribute("key").Value : null;
        //    switch (xElement.Name.LocalName)
        //    {
        //        case "int": { nsObj = new NSNumber(Convert.ToInt32(xElement.Value)) {  }; break; }
        //        case "string": { nsObj = new NSString(xElement.Value) {  }; break; }
        //        case "bool": { nsObj = new NSNumber(xElement.Value.ConvertFromYesNo()) {  }; break; }
                
        //        case "object": 
        //            {
        //                // TODO : use refection or something like that
        //                string attrClass = xElement.AttributeValueOrDefault("class");
        //                switch (attrClass)
        //                {
        //                    case "NSArray": { nsObj = new NSArray(xElement); break; }
        //                    case "NSArrayController": { ;break; }
        //                    case "NSBox": { ;break; }
        //                    case "NSButton": { ;break; }
        //                    case "NSButtonCell": { ;break; }
        //                    case "NSClipView": { ;break; }
        //                    case "NSCollectionView": { ;break; }
        //                    case "NSColor": { ;break; }
        //                    case "NSCustomObject": { nsObj = new NSCustomObject(xElement); break; }
        //                    case "NSCustomResource": { nsObj = new NSCustomResource(xElement); ;break; }
        //                    case "NSCustomView": { ;break; }
        //                    case "NSFont": { ;break; }
        //                    case "NSMenu": { ;break; }
        //                    case "NSMenuItem": { ;break; }
        //                    case "NSMutableArray": { nsObj = new NSMutableArray(xElement); break; }
        //                    case "NSMutableDictionary": { nsObj = new NSMutableDictionary(xElement); break; }
        //                    case "NSMutableString": { nsObj = new NSMutableString(xElement.Value); break; }
        //                    case "NSPopUpButton": { ;break; }
        //                    case "NSPopUpButtonCell": { ;break; }
        //                    case "NSScroller": { ;break; }
        //                    case "NSScrollView": { ;break; }
        //                    case "NSSegmentedCell": { ;break; }
        //                    case "NSSegmentedControl": { ;break; }
        //                    case "NSSegmentItem": { ;break; }
        //                    case "NSTextField": { ;break; }
        //                    case "NSTextFieldCell": { ;break; }
        //                    case "NSToolbar": { nsObj = new NSToolbar(xElement); break; }
        //                    case "NSToolbarFlexibleSpaceItem": { nsObj = NSToolbarFlexibleSpaceItem.create(xElement); break; }
        //                    case "NSToolbarItem": { nsObj = NSToolbarItem.create(xElement); break; }
        //                    case "NSToolbarSpaceItem": { ;break; }
        //                    case "NSView": { nsObj = new NSView((xElement); ;break; }
        //                    case "NSViewController": { ;break; }
        //                    case "NSWindowTemplate": { nsObj = new NSWindowTemplate((xElement); break; }
        //                    case "NSWindowView": { ;break; }
        //                    case "NSCollectionViewItem": { ;break; }
                           
        //                    default:
        //                        System.Diagnostics.Debug.WriteLine(
        //                            string.Format("Unknown object {0}", attrClass));
        //                        break;

        //                }
        //                break;
        //            }

        //    }

        //    return nsObj;
        //}
    }
}
