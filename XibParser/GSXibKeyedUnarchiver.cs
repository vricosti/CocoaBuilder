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

        

        new public static GSXibElement Alloc()
        {
            return new GSXibElement();
        }

        public virtual GSXibElement InitWithTypeAndAttributes(NSString typeName, NSDictionary attribs)
        {
            GSXibElement self = this;

            _type = typeName;
            _attributes = attribs;
            _elements = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
            _values = (NSMutableArray)NSMutableArray.Alloc().Init();

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

        public virtual void AddElement(GSXibElement element)
        {
            _values.AddObject(element);
        }

        public virtual void SetElementForKey(GSXibElement element, NSString key)
        {
            _elements.SetObjectForKey(element, key);
        }

        public virtual NSString AttributeForKey(NSString key)
        {
            return (NSString)_attributes.ObjectForKey(key);
        }

        public virtual GSXibElement ElementForKey(NSString key)
        {
            return (GSXibElement)_elements.ObjectForKey(key);
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

        //static TextWriter _tw;

        public GSXibKeyedUnarchiver(bool shouldCallInit = true)
        {
            if (shouldCallInit)
            {
                Init();
            }
        }

        new public static GSXibKeyedUnarchiver Alloc()
        {
            string strAssemDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string strLogPath = System.IO.Path.Combine(strAssemDir, "smi_parser_foundCharacters.log");
            //_tw = new StreamWriter(strLogPath);

            return new GSXibKeyedUnarchiver(false);
        }

        private NSData PreProcessXib(NSData data)
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


        public GSXibKeyedUnarchiver InitForReadingWithData(NSData data)
        {
            NSData theData = data;

            Objects = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();

            Stack = (NSMutableArray)NSMutableArray.Alloc().Init();

            Decoded = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();

            if (NSClassWrapper.IsInInterfaceBuilder == false)
            {
                theData = PreProcessXib(data);
            }

            NSXMLParser theParser = (NSXMLParser)NSXMLParser.Alloc().InitWithData(theData);
            theParser.SetDelegate(this);

            theParser.Parse();

            //_tw.Close();
            return this;
        }

        public void ParserFoundCharacters(NSXMLParser parser, NSString foundCharacters)
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

        public void ParserDidStartElement(NSXMLParser parser, NSString elementName, NSString namespaceURI, NSString qualifiedName, NSDictionary attributeDict)
        {
            GSXibElement element = GSXibElement.Alloc().InitWithTypeAndAttributes(elementName, attributeDict); //andAttributes: attributeDict];
            NSString key = (NSString)attributeDict.ObjectForKey((NSString)@"key");
            NSString refId = (NSString)attributeDict.ObjectForKey((NSString)@"id");

            if (key != null)
            {
                if (CurrentElement != null)
                {
                    CurrentElement.SetElementForKey(element, key);
                }
            }
            else
            {
                // For Arrays
                if (CurrentElement != null)
                {
                    CurrentElement.AddElement(element);
                }
            }
            if (refId != null)
            {
                Objects.SetObjectForKey(element, refId);
            }

             if (!@"archive".IsEqualToString(elementName) &&
                 !@"data".IsEqualToString(elementName))
             {
                 // only used for the root element
                 // push
                 Stack.AddObject(CurrentElement);
             }

             if (!@"archive".IsEqualToString(elementName))
             {
                 CurrentElement = element;
             }
        }

        public void ParserDidEndElement(NSXMLParser parser, NSString elementName, NSString namespaceURI, NSString qualifiedName)
        {
            if (!@"archive".IsEqualToString(elementName) &&
                 !@"data".IsEqualToString(elementName))
            {
                 // pop
                CurrentElement = (GSXibElement)Stack.LastObject();
                Stack.RemoveLastObject(); 
            }
        }

        public id AllocObjectForClassName(NSString classname)
        {
            id nsObj = null;

            Type t = Type.GetType("Smartmobili.Cocoa." + classname);
            if (t != null)
            {
                nsObj = Activator.CreateInstance(t) as id;
            }

            return nsObj;
        }

        public virtual bool ReplaceObject(id oldObj, id newObj)
        {
            return false;
        }

        //- (id) decodeObjectForXib: (GSXibElement*)element forClassName: (NSString*)classname withID: (NSString*)objID
        public virtual id DecodeObjectForXib(GSXibElement element, NSString classname, NSString objID)
        { 
            GSXibElement last;
            id o, r;
            id dlgate = this.Delegate;

            o = AllocObjectForClassName(classname);
            if (objID != null)
                Decoded.SetObjectForKey(o, objID);

            // push
            last = CurrentElement;
            CurrentElement = element;

            r = Objc.MsgSend(o, "InitWithCoder", this);
            //r = ((NSCoding)o).InitWithCoder(this);

            // pop
            CurrentElement = last;

            if (r != o)
            {
                Objc.MsgSend(dlgate, "UnarchiverWillReplaceObject", this, o, r);
                //((INSKeyedUnarchiverDelegate)dlgate).UnarchiverWillReplaceObject(this, o, r);
                o = r;
                if (objID != null)
                    Decoded.SetObjectForKey(o, objID);

            }

            //r = [o awakeAfterUsingCoder: self];
            r = Objc.MsgSend(o, "AwakeAfterUsingCoder", this);
            if (r != o)
            {
                Objc.MsgSend(dlgate, "UnarchiverWillReplaceObject", this, o, r);
                //((INSKeyedUnarchiverDelegate)dlgate).UnarchiverWillReplaceObject(this, o, r);
                o = r;
                if (objID != null)
                    Decoded.SetObjectForKey(o, objID);

            }


            if (dlgate != null)
            {
                //r = ((INSKeyedUnarchiverDelegate)dlgate).UnarchiverDidDecodeObject(this, o);
                r = Objc.MsgSend(dlgate, "UnarchiverDidDecodeObject", this, o);
                if (r != o)
                {
                    Objc.MsgSend(dlgate, "UnarchiverWillReplaceObject", this, o, r);
                    o = r;
                    if (objID != null)
                        Decoded.SetObjectForKey(o, objID);
                }
            }

            if (objID != null)
            {
                System.Diagnostics.Debug.WriteLine("XIB decoded object {0} for id {1}", o.ToString(), (string)objID);
            }

            return o;
        }

        public virtual id DecodeDictionaryForXib(GSXibElement element, NSString classname, NSString objID)
        {
            id o, r;
            id dlgate = this.Delegate;

            o = AllocObjectForClassName(classname);
            if (objID != null)
                Decoded.SetObjectForKey(o, objID);

             //r = [o initWithDictionary: [self _decodeDictionaryOfObjectsForElement: element]];
            r = ((NSDictionary)o).InitWithDictionary((NSDictionary)_DecodeDictionaryOfObjectsForElement(element));
            if (r != o)
            {
                Objc.MsgSend(dlgate, "UnarchiverWillReplaceObject", this, o, r);
                //((INSKeyedUnarchiverDelegate)dlgate).UnarchiverWillReplaceObject(this, o, r);
                o = r;
                if (objID != null)
                    Decoded.SetObjectForKey(o, objID);
            }

            //r = [o awakeAfterUsingCoder: self];
            r = Objc.MsgSend(o, "AwakeAfterUsingCoder", this);
            if (r != o)
            {
                Objc.MsgSend(dlgate, "UnarchiverWillReplaceObject", this, o, r);
                o = r;
                if (objID != null)
                    Decoded.SetObjectForKey(o, objID);

            }

            if (dlgate != null)
            {
                //r = ((INSKeyedUnarchiverDelegate)dlgate).UnarchiverDidDecodeObject(this, o);
                r = Objc.MsgSend(dlgate, "UnarchiverDidDecodeObject", this, o);
                if (r != o)
                {
                    Objc.MsgSend(dlgate, "UnarchiverWillReplaceObject", this, o, r);
                    o = r;
                    if (objID != null)
                        Decoded.SetObjectForKey(o, objID);
                }
            }

            if (objID != null)
            {
                System.Diagnostics.Debug.WriteLine("XIB decoded object {0} for id {1}", o.ToString(), (string)objID);
            }

            return o;
        }

        public virtual id ObjectForXib(GSXibElement element)
        {
            NSString elementName;
            NSString objID;

            if (element == null)
                return null;

            objID = element.AttributeForKey(@"id");
            if (objID != null)
            {
                id newObj = Decoded.ObjectForKey(objID);
                if (newObj != null)
                {
                    // The object was already decoded as a reference
                    return newObj;
                }
            }

            elementName = element.Type;
            if (@"object".IsEqualToString(elementName))
            {
                NSString classname = element.AttributeForKey(@"class");
                return DecodeObjectForXib(element, classname, objID);
            }
            else if (@"string".IsEqualToString(elementName))
            {
                NSString type = element.AttributeForKey(@"type");
                id newObj = element.Value;

                if (type != null && type.IsEqualToString(@"base64-UTF8"))
                {
                    //FIXME
                    //    NSData d = [newObj dataUsingEncoding: NSASCIIStringEncoding];
                    //    d = [GSMimeDocument decodeBase64: d];
                    //    newObj = AUTORELEASE([[NSString alloc] initWithData: d  encoding: NSUTF8StringEncoding]);
                }

                // empty strings are not nil!
                if (newObj == null)
                    newObj = (NSString)@"";

                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"int".IsEqualToString(elementName))
            {
                id newObj = NSNumber.NumberWithInt(element.Value.IntValue);
                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"double".IsEqualToString(elementName))
            {
                id newObj = NSNumber.NumberWithDouble(element.Value.DoubleValue);
                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"bool".IsEqualToString(elementName))
            {
                //Fixme
                id newObj = NSNumber.NumberWithBool(element.Value.BoolValue);
                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"integer".IsEqualToString(elementName))
            {
                NSString value = element.AttributeForKey(@"value");
                id newObj = NSNumber.NumberWithInteger(value.IntegerValue);
                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"real".IsEqualToString(elementName))
            {
                NSString value = element.AttributeForKey(@"value");
                id newObj = NSNumber.NumberWithFloat(value.FloatValue);
                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"bool".IsEqualToString(elementName))
            {
                NSString value = element.AttributeForKey(@"value");
                id newObj = NSNumber.NumberWithBool(value.BoolValue);
                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"reference".IsEqualToString(elementName))
            {
                NSString refId = element.AttributeForKey(@"ref");


                if (refId == null)
                {
                    return null;
                }
                else
                {
                    id newObj = Decoded.ObjectForKey(refId);
                     // FIXME: We need a marker for nil
                    if (newObj == null)
                    {
                        //NSLog(@"Decoding reference %@", ref);
                        element = (GSXibElement)Objects.ObjectForKey(refId);
                        if (element != null)
                        {
                            // Decode the real object
                            newObj = ObjectForXib(element);
                        }
                    }
                    return newObj;
                }
            }
            else if (@"nil".IsEqualToString(elementName))
            {
                return null;
            }
            else if (@"characters".IsEqualToString(elementName))
            {
                id newObj = element.Value;
                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"bytes".IsEqualToString(elementName))
            {
                id newObj = element.Value.DataUsingEncoding(NSStringEncoding.NSASCIIStringEncoding, false);
                //newObj = GSMimeDocument.DecodeBase64(newObj);
                if (objID != null)
                    Decoded.SetObjectForKey(newObj, objID);

                return newObj;
            }
            else if (@"array".IsEqualToString(elementName))
            {
                NSString classname = element.AttributeForKey(@"class");
                if (classname == null)
                {
                    classname = @"NSArray";
                }
                return DecodeObjectForXib(element, classname, objID);
            }
            else if (@"dictionary".IsEqualToString(elementName))
            {
                NSString classname = element.AttributeForKey(@"class");
                if (classname == null)
                {
                    classname = @"NSDictionary";
                }
                return DecodeDictionaryForXib(element, classname, objID);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Unknown element type {0}", elementName);
            }

            return null;
        }

        public override id _DecodeArrayOfObjectsForKey(NSString aKey)
        {
            // FIXME: This is wrong but the only way to keep the code for
            // [NSArray-initWithCoder:] working
            return _DecodeArrayOfObjectsForElement(CurrentElement);
        }

        public override id _DecodeArrayOfObjectsForElement(GSXibElement element)
        {
            NSArray values = element.Values;
            int max = values.Count;
            id[] list = new id[max];
            int i;

            for (i = 0; i < max; i++)
            {
                list[i] = this.ObjectForXib((GSXibElement)values.ObjectAtIndex(i));
                if (list[i] == null)
                    System.Diagnostics.Trace.WriteLine(string.Format("No object for {0} at index {1}", values.ObjectAtIndex(i).ToString(), i));
            }

            return NSArray.ArrayWithObjects(list);
        }

        public override id _DecodeDictionaryOfObjectsForElement(GSXibElement element)
        {
            NSDictionary elements = element.Elements;
            NSMutableDictionary dict = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();

            foreach (KeyValuePair<id, id> kvp in elements)
            {
                NSString key = (NSString)kvp.Key;
                id obj = this.ObjectForXib((GSXibElement)elements.ObjectForKey(key));
                if (obj == null)
                    System.Diagnostics.Trace.WriteLine(string.Format("No object for {0} at key {1}", elements.ObjectForKey(key).ToString(), key.Value));
                else
                    dict.SetObjectForKey(obj, key);
            }

            return dict;
        }

        public virtual NSString DecodeReferenceForKey(NSString aKey)
        {
            GSXibElement element = CurrentElement.ElementForKey(aKey);
            NSString objID;

            if (element == null)
                return null;

            objID = element.AttributeForKey(@"id");
            if (objID != null)
            {
                return objID;
            }

            objID = element.AttributeForKey(@"ref");
            if (objID != null)
            {
                return objID;
            }


            return null;
        }

        public override bool ContainsValueForKey(NSString aKey)
        {
            GSXibElement element = CurrentElement.ElementForKey(aKey);
            return (element != null);
        }

        public override id DecodeObjectForKey(NSString aKey)
        {
            GSXibElement element = CurrentElement.ElementForKey(aKey);
            if (element == null)
                return null;

            return this.ObjectForXib(element);
        }

        public override bool DecodeBoolForKey(NSString aKey)
         {
             id o = this.DecodeObjectForKey(aKey);
             if (o != null)
             {
                 if (o.IsKindOfClass(NSNumber.Class) == true)
                 {
                     return ((NSNumber)o).BoolValue;
                 }
             }

             return false;
         }

        public override byte[] DecodeBytesForKey(NSString aKey, ref int lengthp)
        {
            id o = this.DecodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.IsKindOfClass(NSData.Class) == true)
                {
                    lengthp = ((NSData)o).Length;
                    return ((NSData)o).Bytes;
                }
            }

            lengthp = 0;
            return null;
        }

        public override double DecodeDoubleForKey(NSString aKey)
        {
            id o = this.DecodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.IsKindOfClass(NSNumber.Class) == true)
                {
                    return ((NSNumber)o).DoubleValue;
                }
            }

            return 0.0;
        }

        public override float DecodeFloatForKey(NSString aKey)
        {
            id o = this.DecodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.IsKindOfClass(NSNumber.Class) == true)
                {
                    return ((NSNumber)o).FloatValue;
                }
            }

            return 0;
        }

        public override int DecodeIntForKey(NSString aKey)
        {
            id o = this.DecodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.IsKindOfClass(NSNumber.Class) == true)
                {
                    return ((NSNumber)o).IntValue;
                }
            }
            return 0;
        }

        public override int DecodeInt32ForKey(NSString aKey)
        {
            return DecodeIntForKey(aKey);
        }

        public override long DecodeInt64ForKey(NSString aKey)
        {
            id o = this.DecodeObjectForKey(aKey);
            if (o != null)
            {
                if (o.IsKindOfClass(NSNumber.Class) == true)
                {
                    return ((NSNumber)o).IntValue;
                }
            }
            return 0;
        }
    }
}
