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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/Additions/GNUstepGUI/GSXibLoading.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/GSXibLoader.m
    public class GSXibElement : NSObject
    {
        new public static Class Class = new Class(typeof(GSXibElement));

        NSString _type;
        NSDictionary _attributes;
        NSString _value;
        NSMutableDictionary _elements;
        NSMutableArray _values;

        

        new public static GSXibElement alloc()
        {
            return new GSXibElement();
        }

        public virtual GSXibElement initWithTypeAndAttributes(NSString typeName, NSDictionary attribs)
        {
            GSXibElement self = this;

            _type = typeName;
            _attributes = attribs;
            _elements = (NSMutableDictionary)NSMutableDictionary.alloc().init();
            _values = (NSMutableArray)NSMutableArray.alloc().init();

            return self;
        }


        public virtual NSString Type
        {
            get { return _type; }
        }

        public virtual NSString Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public virtual NSDictionary Elements
        {
            get { return _elements; }
        }

        public virtual NSArray Values
        {
            get { return _values; }
        }

        public virtual void addElement(GSXibElement element)
        {
            _values.addObject(element);
        }

        public virtual void setElementForKey(GSXibElement element, NSString key)
        {
            _elements.setObjectForKey(element, key);
        }

        public virtual NSString attributeForKey(NSString key)
        {
            return (NSString)_attributes.objectForKey(key);
        }

        public virtual GSXibElement elementForKey(NSString key)
        {
            return (GSXibElement)_elements.objectForKey(key);
        }

        public override int GetHashCode()
        {
            int hash1 = (_type != null) ? _type.GetHashCode() : 0;
            int hash2 = (_attributes != null) ? _attributes.GetHashCode() : 0;
            int hash3 = (_value != null) ? _value.GetHashCode() : 0;
            int hash4 = (_elements != null) ? _elements.GetHashCode() : 0;
            int hash5 = (_values != null) ? _values.GetHashCode() : 0;

            int hash = 13;
            hash = (hash * 7) + hash1;
            hash = (hash * 7) + hash2;
            hash = (hash * 7) + hash3;
            hash = (hash * 7) + hash4;
            hash = (hash * 7) + hash5;

            return hash;
        }
    }




    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/Additions/GNUstepGUI/GSXibLoading.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/GSXibLoader.m
    public class GSXibKeyedUnarchiver : NSKeyedUnarchiver, INSXMLParser
    {
        public NSMutableDictionary Objects { get; set; }
        
        public NSMutableArray Stack { get; set; }

        public GSXibElement CurrentElement { get; set; }

        public NSMutableDictionary Decoded { get; set; }

        private List<Assembly> CocoaAssemblies { get; set; }

        public GSXibKeyedUnarchiver(bool shouldCallInit = true)
        {
            if (shouldCallInit)
            {
                init();
            }

            CocoaAssemblies = new List<Assembly>();
           
            //Assembly asm = Assembly.Load("Smartmobili.Cocoa.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            CocoaAssemblies.Add(Assembly.Load("Smartmobili.Cocoa.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
            CocoaAssemblies.Add(Assembly.Load("Smartmobili.Cocoa.AppKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
            CocoaAssemblies.Add(Assembly.Load("Smartmobili.Cocoa.InterfaceBuilderKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
        }

        new public static GSXibKeyedUnarchiver alloc()
        {
            string strAssemDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string strLogPath = System.IO.Path.Combine(strAssemDir, "smi_parser_foundCharacters.log");
            //_tw = new StreamWriter(strLogPath);

            return new GSXibKeyedUnarchiver(false);
        }

        private NSData preProcessXib(NSData data)
        {
            NSData result = data;

            string xml = System.Text.Encoding.Default.GetString(data.Bytes);
            XDocument xDoc = XDocument.Parse(xml);

            NSMutableDictionary customClassDict = new NSMutableDictionary();

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
                        NSString key = xmlKeys[index].Value;
                        if (key.Contains("CustomClassName"))
                        {
                            customClassDict.Add(key, (NSString)xmlObjs[index].Value);
                        }
                    }
                }
            }

            if (customClassDict.Count > 0)
            {
                foreach (var kvp in customClassDict)
                {
                    NSString keyValue = ((NSString)kvp.Key).Replace(".CustomClassName", "");
                    NSString className = (NSString)kvp.Value;
                    NSString objectRecordXpath = string.Format("//object[@class=\"IBObjectRecord\"]/int[@key=\"objectID\"][text()=\"{0}\"]/../reference", keyValue);

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


        public GSXibKeyedUnarchiver initForReadingWithData(NSData data)
        {
            NSData theData = data;

            Objects = (NSMutableDictionary)NSMutableDictionary.alloc().init();

            Stack = (NSMutableArray)NSMutableArray.alloc().init();

            Decoded = (NSMutableDictionary)NSMutableDictionary.alloc().init();

            if (NSClassWrapper.IsInInterfaceBuilder == false)
            {
                theData = preProcessXib(data);
            }

            NSXMLParser theParser = (NSXMLParser)NSXMLParser.alloc().initWithData(theData);
            theParser.setDelegate(this);

            theParser.parse();

            //_tw.close();
            return this;
        }

        public void parserFoundCharacters(NSXMLParser parser, NSString foundCharacters)
        {
            //string chars = foundCharacters.Value.Replace("\n", "\\n");
            //chars = chars.Replace("\r", "\\r");
            //chars = chars.Replace("\t", "\\t");
            //_tw.WriteLine("parser:foundCharacters: " + chars);

            if (CurrentElement != null)
            {
                CurrentElement.Value = foundCharacters;
            }
        }

        public void parserDidStartElement(NSXMLParser parser, NSString elementName, NSString namespaceURI, NSString qualifiedName, NSDictionary attributeDict)
        {
            GSXibElement element = GSXibElement.alloc().initWithTypeAndAttributes(elementName, attributeDict); //andAttributes: attributeDict];
            NSString key = (NSString)attributeDict.objectForKey((NSString)@"key");
            NSString refId = (NSString)attributeDict.objectForKey((NSString)@"id");

            if (key != null)
            {
                if (CurrentElement != null)
                {
                    CurrentElement.setElementForKey(element, key);
                }
            }
            else
            {
                // For Arrays
                if (CurrentElement != null)
                {
                    CurrentElement.addElement(element);
                }
            }
            if (refId != null)
            {
                Objects.setObjectForKey(element, refId);
            }

             if (!@"archive".isEqualToString(elementName) &&
                 !@"data".isEqualToString(elementName))
             {
                 // only used for the root element
                 // push
                 Stack.addObject(CurrentElement);
             }

             if (!@"archive".isEqualToString(elementName))
             {
                 CurrentElement = element;
             }
        }

        public void parserDidEndElement(NSXMLParser parser, NSString elementName, NSString namespaceURI, NSString qualifiedName)
        {
            if (!@"archive".isEqualToString(elementName) &&
                 !@"data".isEqualToString(elementName))
            {
                 // pop
                CurrentElement = (GSXibElement)Stack.lastObject();
                Stack.removeLastObject(); 
            }
        }

        public id allocObjectForClassName(NSString classname)
        {
            id nsObj = null;

            var type = CocoaAssemblies
               .SelectMany(t => t.GetTypes())
               .Where(c => c.Name == classname)
               .FirstOrDefault();

            //var type = AppDomain.CurrentDomain.GetAssemblies()
            //    .Where(a => a.FullName.StartsWith("Smartmobili.Cocoa"))
            //    .SelectMany(t => t.GetTypes())
            //    .Where(c => c.Name == classname)
            //    .FirstOrDefault();

            //var type =
            //(from a in AppDomain.CurrentDomain.GetAssemblies()
            // where a.FullName.StartsWith("Smartmobili.Cocoa")
            // from t in a.GetTypes()
            // where t.Name == classname
            // select t).FirstOrDefault();

            if (type != null)
            {
                nsObj = Activator.CreateInstance(type) as id;
            }

            return nsObj;
        }

        public virtual bool ReplaceObject(id oldObj, id newObj)
        {
            return false;
        }

        //- (id) decodeObjectForXib: (GSXibElement*)element forClassName: (NSString*)classname withID: (NSString*)objID
        public virtual id decodeObjectForXib(GSXibElement element, NSString classname, NSString objID)
        { 
            GSXibElement last;
            id o, r;
            id dlgate = this.Delegate;

            o = allocObjectForClassName(classname);
            if (objID != null)
                Decoded.setObjectForKey(o, objID);

            // push
            last = CurrentElement;
            CurrentElement = element;

            r = (id)Objc.MsgSend(o, "initWithCoder", this);
            //r = ((NSCoding)o).initWithCoder(this);

            // pop
            CurrentElement = last;

            if (r != o)
            {
                Objc.MsgSend(dlgate, "unarchiverWillReplaceObject", this, o, r);
                //((INSKeyedUnarchiverDelegate)dlgate).unarchiverWillReplaceObject(this, o, r);
                o = r;
                if (objID != null)
                    Decoded.setObjectForKey(o, objID);

            }

            //r = [o awakeAfterUsingCoder: self];
            r = (id)Objc.MsgSend(o, "AwakeAfterUsingCoder", this);
            if (r != o)
            {
                Objc.MsgSend(dlgate, "unarchiverWillReplaceObject", this, o, r);
                //((INSKeyedUnarchiverDelegate)dlgate).unarchiverWillReplaceObject(this, o, r);
                o = r;
                if (objID != null)
                    Decoded.setObjectForKey(o, objID);

            }


            if (dlgate != null)
            {
                //r = ((INSKeyedUnarchiverDelegate)dlgate).unarchiverDidDecodeObject(this, o);
                r = (id)Objc.MsgSend(dlgate, "unarchiverDidDecodeObject", this, o);
                if (r != o)
                {
                    Objc.MsgSend(dlgate, "unarchiverWillReplaceObject", this, o, r);
                    o = r;
                    if (objID != null)
                        Decoded.setObjectForKey(o, objID);
                }
            }

            if (objID != null)
            {
                System.Diagnostics.Debug.WriteLine("XIB decoded object {0} for id {1}", o.ToString(), (string)objID);
            }

            return o;
        }

        public virtual id decodeDictionaryForXib(GSXibElement element, NSString classname, NSString objID)
        {
            id o, r;
            id dlgate = this.Delegate;

            o = allocObjectForClassName(classname);
            if (objID != null)
                Decoded.setObjectForKey(o, objID);

             //r = [o initWithDictionary: [self _decodeDictionaryOfObjectsForElement: element]];
            r = ((NSDictionary)o).initWithDictionary((NSDictionary)_decodeDictionaryOfObjectsForElement(element));
            if (r != o)
            {
                Objc.MsgSend(dlgate, "unarchiverWillReplaceObject", this, o, r);
                //((INSKeyedUnarchiverDelegate)dlgate).unarchiverWillReplaceObject(this, o, r);
                o = r;
                if (objID != null)
                    Decoded.setObjectForKey(o, objID);
            }

            //r = [o awakeAfterUsingCoder: self];
            r = (id)Objc.MsgSend(o, "AwakeAfterUsingCoder", this);
            if (r != o)
            {
                Objc.MsgSend(dlgate, "unarchiverWillReplaceObject", this, o, r);
                o = r;
                if (objID != null)
                    Decoded.setObjectForKey(o, objID);

            }

            if (dlgate != null)
            {
                //r = ((INSKeyedUnarchiverDelegate)dlgate).unarchiverDidDecodeObject(this, o);
                r = (id)Objc.MsgSend(dlgate, "unarchiverDidDecodeObject", this, o);
                if (r != o)
                {
                    Objc.MsgSend(dlgate, "unarchiverWillReplaceObject", this, o, r);
                    o = r;
                    if (objID != null)
                        Decoded.setObjectForKey(o, objID);
                }
            }

            if (objID != null)
            {
                System.Diagnostics.Debug.WriteLine("XIB decoded object {0} for id {1}", o.ToString(), (string)objID);
            }

            return o;
        }

        public virtual id objectForXib(GSXibElement element)
        {
            NSString elementName;
            NSString objID;

            if (element == null)
                return null;

            objID = element.attributeForKey(@"id");
            if (objID != null)
            {
                id newObj = Decoded.objectForKey(objID);
                if (newObj != null)
                {
                    // The object was already decoded as a reference
                    return newObj;
                }
            }

            elementName = element.Type;
            if (@"object".isEqualToString(elementName))
            {
                NSString classname = element.attributeForKey(@"class");
                return decodeObjectForXib(element, classname, objID);
            }
            else if (@"string".isEqualToString(elementName))
            {
                NSString type = element.attributeForKey(@"type");
                id newObj = element.Value;

                if (type != null && type.isEqualToString(@"base64-UTF8"))
                {
                     NSData d = ((NSString)newObj).dataUsingEncoding(NSStringEncoding.NSASCIIStringEncoding, false);
                     d = GSMimeDocument.decodeBase64((NSData)d);
                     newObj = NSString.alloc().initWithData(d, NSStringEncoding.NSUTF8StringEncoding);
                }

                // empty strings are not nil!
                if (newObj == null)
                    newObj = (NSString)@"";

                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"int".isEqualToString(elementName))
            {
                id newObj = NSNumber.numberWithInt(element.Value.IntValue);
                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"double".isEqualToString(elementName))
            {
                id newObj = NSNumber.numberWithDouble(element.Value.DoubleValue);
                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"bool".isEqualToString(elementName))
            {
                //Fixme
                id newObj = NSNumber.numberWithBool(element.Value.BoolValue);
                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"integer".isEqualToString(elementName))
            {
                NSString value = element.attributeForKey(@"value");
                id newObj = NSNumber.numberWithInteger(value.IntegerValue);
                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"real".isEqualToString(elementName))
            {
                NSString value = element.attributeForKey(@"value");
                id newObj = NSNumber.numberWithFloat(value.FloatValue);
                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"bool".isEqualToString(elementName))
            {
                NSString value = element.attributeForKey(@"value");
                id newObj = NSNumber.numberWithBool(value.BoolValue);
                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"reference".isEqualToString(elementName))
            {
                NSString refId = element.attributeForKey(@"ref");


                if (refId == null)
                {
                    return null;
                }
                else
                {
                    id newObj = Decoded.objectForKey(refId);
                     // FIXME: We need a marker for nil
                    if (newObj == null)
                    {
                        //NSLog(@"Decoding reference %@", ref);
                        element = (GSXibElement)Objects.objectForKey(refId);
                        if (element != null)
                        {
                            // Decode the real object
                            newObj = objectForXib(element);
                        }
                    }
                    return newObj;
                }
            }
            else if (@"nil".isEqualToString(elementName))
            {
                return null;
            }
            else if (@"characters".isEqualToString(elementName))
            {
                id newObj = element.Value;
                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"bytes".isEqualToString(elementName))
            {
                id newObj = element.Value.dataUsingEncoding(NSStringEncoding.NSASCIIStringEncoding, false);
                newObj = GSMimeDocument.decodeBase64((NSData)newObj);

                //string encodedData = Encoding.ASCII.GetString(((NSData)newObj).Bytes);
                //byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
                //objID = new NSString(System.Text.Encoding.ASCII.GetString(encodedDataAsBytes));
                
                //newObj = GSMimeDocument.decodeBase64(newObj);
                if (objID != null)
                    Decoded.setObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"array".isEqualToString(elementName))
            {
                NSString classname = element.attributeForKey(@"class");
                if (classname == null)
                {
                    classname = @"NSArray";
                }
                return decodeObjectForXib(element, classname, objID);
            }
            else if (@"dictionary".isEqualToString(elementName))
            {
                NSString classname = element.attributeForKey(@"class");
                if (classname == null)
                {
                    classname = @"NSDictionary";
                }
                return decodeDictionaryForXib(element, classname, objID);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Unknown element type {0}", elementName);
            }

            return null;
        }

        public override id _decodeArrayOfObjectsForKey(NSString aKey)
        {
            // FIXME: This is wrong but the only way to keep the code for
            // [NSArray-initWithCoder:] working
            return _decodeArrayOfObjectsForElement(CurrentElement);
        }

        public override id _decodeArrayOfObjectsForElement(GSXibElement element)
        {
            NSArray values = element.Values;
            int max = values.Count;
            id[] list = new id[max];
            int i;

            for (i = 0; i < max; i++)
            {
                list[i] = this.objectForXib((GSXibElement)values.objectAtIndex(i));
                if (list[i] == null)
                    System.Diagnostics.Trace.WriteLine(string.Format("No object for {0} at index {1}", values.objectAtIndex(i).ToString(), i));
            }

            return NSArray.arrayWithObjects(list);
        }

        public override id _decodeDictionaryOfObjectsForElement(GSXibElement element)
        {
            NSDictionary elements = element.Elements;
            NSEnumerator en;
            NSString key;
            NSMutableDictionary dict;

            dict = (NSMutableDictionary)NSMutableDictionary.alloc().init();
            en = elements.keyEnumerator();
            while ((key = (NSString)en.nextObject()) != null)
            {
                id obj = this.objectForXib((GSXibElement)elements.objectForKey(key));
                if (obj == null)
                    System.Diagnostics.Trace.WriteLine(string.Format("No object for {0} at key {1}", elements.objectForKey(key).ToString(), key.Value));
                else
                    dict.setObjectForKey(obj, key);
            }

            return dict;
        }

        public virtual NSString decodeReferenceForKey(NSString aKey)
        {
            GSXibElement element = CurrentElement.elementForKey(aKey);
            NSString objID;

            if (element == null)
                return null;

            objID = element.attributeForKey(@"id");
            if (objID != null)
            {
                return objID;
            }

            objID = element.attributeForKey(@"ref");
            if (objID != null)
            {
                return objID;
            }


            return null;
        }

        public override bool containsValueForKey(NSString aKey)
        {
            GSXibElement element = CurrentElement.elementForKey(aKey);
            return (element != null);
        }

        public override id decodeObjectForKey(NSString aKey)
        {
            GSXibElement element = CurrentElement.elementForKey(aKey);
            if (element == null)
                return null;

            return this.objectForXib(element);
        }

        public override bool decodeBoolForKey(NSString aKey)
         {
             id o = this.decodeObjectForKey(aKey);
             if (o != null)
             {
                 if (o.isKindOfClass(NSNumber.Class) == true)
                 {
                     return ((NSNumber)o).BoolValue;
                 }
             }

             return false;
         }

        public override byte[] decodeBytesForKey(NSString aKey, ref int lengthp)
        {
            id o = this.decodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.isKindOfClass(NSData.Class) == true)
                {
                    lengthp = ((NSData)o).Length;
                    return ((NSData)o).Bytes;
                }
            }

            lengthp = 0;
            return null;
        }

        public override double decodeDoubleForKey(NSString aKey)
        {
            id o = this.decodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.isKindOfClass(NSNumber.Class) == true)
                {
                    return ((NSNumber)o).DoubleValue;
                }
            }

            return 0.0;
        }

        public override float decodeFloatForKey(NSString aKey)
        {
            id o = this.decodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.isKindOfClass(NSNumber.Class) == true)
                {
                    return ((NSNumber)o).FloatValue;
                }
            }

            return 0;
        }

        public override int decodeIntForKey(NSString aKey)
        {
            id o = this.decodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.isKindOfClass(NSNumber.Class) == true)
                {
                    return ((NSNumber)o).IntValue;
                }
            }
            return 0;
        }

        public override int decodeInt32ForKey(NSString aKey)
        {
            return decodeIntForKey(aKey);
        }

        public override long decodeInt64ForKey(NSString aKey)
        {
            id o = this.decodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.isKindOfClass(NSNumber.Class) == true)
                {
                    return ((NSNumber)o).IntValue;
                }
            }
            return 0;
        }
    }
}
