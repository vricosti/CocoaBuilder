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
    //https://github.com/gnustep/gnustep-base/blob/master/Source/NSDictionary.m

    public class NSDictionary : NSObject, IDictionary<id, id>
    {
        new public static Class Class = new Class(typeof(NSDictionary));
        new public static NSDictionary Alloc() { return new NSDictionary(); }

        protected Dictionary<id, id> _dict = new Dictionary<id, id>();

        //////////////////////////////////////////////////////////////////////////////////
        //      <object class="NSMutableDictionary" key="IBDocument.Metadata">
        //          <string key="NS.key.0">PluginDependencyRecalculationVersion</string>
        //          <integer value="1" key="NS.object.0"/>
        //      </object>
        /////////////////////////////////////////////////////////////////////////////////
        //                              OR
        /////////////////////////////////////////////////////////////////////////////////
        //      <object class="NSMutableDictionary">
        //          <bool key="EncodedWithXMLCoder">YES</bool>
        //          <reference key="dict.sortedKeys" ref="0"/>
        //          <reference key="dict.values" ref="0"/>
        //      </object>
        /////////////////////////////////////////////////////////////////////////////////
        //                              OR
        /////////////////////////////////////////////////////////////////////////////////
        //      <object class="NSMutableDictionary" key="actions">
        //           <bool key="EncodedWithXMLCoder">YES</bool>
        //           <object class="NSArray" key="dict.sortedKeys">
        //               <bool key="EncodedWithXMLCoder">YES</bool>
        //               <string>delete:</string>
        //               <string>download:</string>
        //               <string>view:</string>
        //           </object>
        //           <object class="NSMutableArray" key="dict.values">
        //               <bool key="EncodedWithXMLCoder">YES</bool>
        //               <string>id</string>
        //               <string>id</string>
        //               <string>id</string>
        //           </object>
        //      </object>
        /////////////////////////////////////////////////////////////////////////////////

        

        public NSDictionary()
        {
        }
        

        public NSDictionary(NSArray sortedKeys, NSMutableArray values)
            : this()
        {

        }

        public virtual id InitWithDictionary(NSDictionary aDictionary)
        {
            id self = this;

            foreach (KeyValuePair<id, id> entry in aDictionary)
            {
                this.Add(entry.Key, entry.Value);
            }

            return self;
        }


        public override id InitWithCoder(NSCoder aCoder)
        {
            id self = this;

            if (aCoder.AllowsKeyedCoding)
            {
                id keys = null;
                id objects = null;

                if (aCoder.ContainsValueForKey(@"NS.keys"))
                {
                    keys = ((NSKeyedUnarchiver)aCoder)._DecodeArrayOfObjectsForKey(@"NS.keys");
                    objects = ((NSKeyedUnarchiver)aCoder)._DecodeArrayOfObjectsForKey(@"NS.objects");
                }
                else if (aCoder.ContainsValueForKey(@"dict.sortedKeys"))
                {
                    keys = aCoder.DecodeObjectForKey(@"dict.sortedKeys");
                    objects = aCoder.DecodeObjectForKey(@"dict.values");
                }

                if (keys == null)
                {
                    uint i = 0;
                    NSString key;
                    id val;

                    keys = NSMutableArray.ArrayWithCapacity(2);
                    objects = NSMutableArray.ArrayWithCapacity(2);

                    key = NSString.StringWithFormat(@"NS.object.%u", i);
                    val = ((NSKeyedUnarchiver)aCoder).DecodeObjectForKey(key);

                    while (val != null)
                    {
                        ((NSMutableArray)objects).AddObject(val);
                        key = NSString.StringWithFormat(@"NS.key.%u", i);
                        val = ((NSKeyedUnarchiver)aCoder).DecodeObjectForKey(key);
                        ((NSMutableArray)keys).AddObject(val);
                        i++;
                        key = NSString.StringWithFormat(@"NS.object.%u", i);
                        val = ((NSKeyedUnarchiver)aCoder).DecodeObjectForKey(key);
                    }
                }

                return InitWithObjectsForKeys((NSArray)objects, (NSArray)keys);
                //return [self initWithObjects: objects forKeys: keys];;
            }


            return self;
        }

        public static id DictionaryWithObjectsForKeys(NSArray objects, NSArray keys)
        {
            return (NSDictionary)NSDictionary.Alloc().InitWithObjectsForKeys(objects, keys);
        }

        public virtual id InitWithObjectsForKeys(NSArray objects, NSArray keys)
        {
            id self = this;

            if (objects != null && keys != null && (objects.Count == keys.Count))
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    id key = keys.ObjectAtIndex(i);
                    id val = objects.ObjectAtIndex(i);
                    if (key == null || val == null)
                        throw new ArgumentNullException();

                    this.Add(key, val);
                }
            }

            return self;
        }



        public virtual id InitWithObjectsAndKeys(params id[] objkey)
        {
            NSArray objects = null;
            NSArray keys = null;

            objects = new NSArray(objkey.ToList().Where((c, i) => (i % 2 == 0) && (c != null)));
            keys = new NSArray(objkey.ToList().Where((c, i) => i % 2 != 0));
            
            return InitWithObjectsForKeys(objects, keys);
        }


        public virtual bool IsEqualToDictionary(NSDictionary otherDictionary)
        {
            if (this.Count != otherDictionary.Count)
                return false;

            foreach (KeyValuePair<id, id> kvp in this)
            {
                if (otherDictionary.ContainsKey(kvp.Key))
                {
                    if (!kvp.Value.IsEqual(this[kvp.Key]))
                        return false;
                }
                else
                    return false;
            }

            return true;
        }



        public virtual NSArray AllKeysForObject(id anObject)
        {
            NSArray objs = (NSArray)NSArray.Alloc().Init();

            foreach (KeyValuePair<id, id> kvp in this)
            {
                if (kvp.Value.IsEqual(anObject))
                    objs.Add(kvp.Key);
            }

            return objs;
        }


        public virtual void RemoveObjectsForKeys(NSArray keyArray)
        {
            foreach (id key in keyArray)
            {
                RemoveObjectForKey(key);
            }
        }



        //public override id InitWithCoder(NSCoder decoder)
        //{
        //    base.InitWithCoder(decoder);

        //    //var xElement = decoder.XmlElement;
        //    //var firstElem = xElement.Elements().FirstOrDefault();

        //    //if (firstElem != null)
        //    //{
        //    //    var keyAttr = firstElem.Attributes("key").FirstOrDefault();

        //    //    XElement[] dictSortedKeyElements = new XElement[0];
        //    //    XElement[] dictValueElements = new XElement[0];

        //    //    if (xElement.Name == "object")
        //    //    {
        //    //        if ((xElement.Descendants().Count() > 0 && firstElem.Name == "bool"
        //    //            && keyAttr != null && keyAttr.Value == "EncodedWithXMLCoder" && firstElem.Value == "YES"))
        //    //        {
        //    //            //System.Diagnostics.Debug.WriteLine("NSMutableDictionary");

        //    //            var dictSortedKeyNode = xElement.Elements().Where(e => (string)e.Attribute("key") == "dict.sortedKeys").ToArray();
        //    //            var dictValueNode = xElement.Elements().Where(e => (string)e.Attribute("key") == "dict.values").ToArray();
        //    //            if (dictSortedKeyNode.Count() == 1 && dictValueNode.Count() == 1)
        //    //            {
        //    //                dictSortedKeyElements = dictSortedKeyNode.Elements().Where(e => e.Name != "bool").ToArray();
        //    //                dictValueElements = dictValueNode.Elements().Where(e => e.Name != "bool").ToArray();
        //    //            }
        //    //        }
        //    //        else
        //    //        {
        //    //            dictSortedKeyElements = xElement.Elements().Where(c =>
        //    //               ((string)c.Attribute("key")).StartsWith("NS.key.")).OrderBy(c => (string)c.Attribute("key")).ToArray();

        //    //            dictValueElements = xElement.Elements().Where(c =>
        //    //               ((string)c.Attribute("key")).StartsWith("NS.object.")).OrderBy(c => (string)c.Attribute("key")).ToArray();
        //    //        }

        //    //        if ((dictSortedKeyElements.Count() == dictValueElements.Count()) &&
        //    //           (dictSortedKeyElements.Count() > 0))
        //    //        {
        //    //            for (int i = 0; i < dictSortedKeyElements.Count(); i++)
        //    //            {
        //    //                id keyObj = (id)decoder.Create(dictSortedKeyElements[i]);
        //    //                id valueObj = (id)decoder.Create(dictValueElements[i]);
        //    //                Add(keyObj, valueObj);
        //    //            }
        //    //        }
        //    //    }
        //    //    else if (xElement.Name == "dictionary")
        //    //    {
        //    //        foreach (var xElm in decoder.XmlElement.Elements())
        //    //        {
        //    //            NSString key = xElm.AttributeValueOrDefault("key", null);
        //    //            if (!string.IsNullOrWhiteSpace(key))
        //    //            {
        //    //                id valueObj = (id)decoder.Create(xElm);
        //    //                Add(key, valueObj);
        //    //            }
        //    //        }
        //    //    }



        //    //}
        //    //else
        //    //{
        //    //    System.Diagnostics.Debug.WriteLine("Empty dictionary");
        //    //}

        //    return this;
        //}

        public virtual NSEnumerator KeyEnumerator()
        {
            return (NSEnumerator)NSDictionaryEnumerator.Alloc().InitWithDictionary(this);
        }


        public virtual void SetObjectForKey(id anObject, id aKey)
        {
            if (anObject == null)
                throw new ArgumentNullException("anObject");
            if (aKey == null)
                throw new ArgumentNullException("aKey");

            this[aKey] = anObject;
        }


        public virtual id ObjectForKey(id aKey)
        {
            id obj = null;

            if (this.ContainsKey(aKey))
            {
                obj = this[aKey];
            }

            return obj;
        }

        public virtual void RemoveObjectForKey(id aKey)
        {
            if (aKey == null)
                throw new ArgumentNullException();

            if (this.ContainsKey(aKey))
            {
                _dict.Remove(aKey);
            }
        }



        public void Add(id aKey, id value)
        {
            _dict.Add(aKey, value);
        }

        public bool ContainsKey(id aKey)
        {
            bool contains = false;

            contains = _dict.ContainsKey(aKey);
            
            return contains;
        }

        public ICollection<id> Keys
        {
            get { return _dict.Keys; }
        }

        public virtual bool Remove(id aKey)
        {
            return _dict.Remove(aKey);
        }

        public virtual bool TryGetValue(id aKey, out id value)
        {
            return _dict.TryGetValue(aKey, out value);
        }

        public ICollection<id> Values
        {
            get { return _dict.Values; }
        }

        public id this[id aKey]
        {
            get
            {
                return _dict[aKey];
            }
            set
            {
                _dict[aKey] = value;
            }
        }


        public virtual NSString FileType { get { return fileType(); } }

        public virtual NSString fileType()
        {
            return (NSString)this.ObjectForKey((NSString)"NSFileType");
        }

        public virtual bool WriteToFile(NSString path, bool flag)
        {
            return false;
        }


//– fileCreationDate
//– fileExtensionHidden
//– fileGroupOwnerAccountID
//– fileGroupOwnerAccountName
//– fileHFSCreatorCode
//– fileHFSTypeCode
//– fileIsAppendOnly
//– fileIsImmutable
//– fileModificationDate
//– fileOwnerAccountID
//– fileOwnerAccountName
//– filePosixPermissions
//– fileSize
//– fileSystemFileNumber
//– fileSystemNumber
//– fileType



        #region ICollection<KeyValuePair<id,id>> Members

        public void Add(KeyValuePair<id, id> item)
        {
            _dict.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dict.Clear();
        }

        public bool Contains(KeyValuePair<id, id> item)
        {
            return (_dict.ContainsKey(item.Key) && _dict.ContainsValue(item.Value));
        }

        public void CopyTo(KeyValuePair<id, id>[] array, int arrayIndex)
        {
            //Could be done but you prolly could figure this out yourself;
            throw new Exception("do not use");
        }

        public int Count
        {
            get { return _dict.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<id, id> item)
        {
            throw new NotImplementedException();
        }

        #endregion


        public override int GetHashCode()
        {
            return (_dict != null) ? _dict.GetHashCode() : base.GetHashCode();
        }


        #region IEnumerable<KeyValuePair<id, id>> Members

        public IEnumerator<KeyValuePair<id, id>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        #endregion
    }

    public class NSDictionaryEnumerator : NSEnumerator
    {
        new public static NSDictionaryEnumerator Alloc() { return new NSDictionaryEnumerator(); }

        protected NSDictionary _dict;
        protected int _curIndex;

        public id InitWithDictionary(NSDictionary aDictionary)
        {
            id self = this;

            _dict = aDictionary;
            _curIndex = 0;

            return self;
        }


        public override id NextObject()
        {
            id nextObj = null;

            if (_dict == null)
                 return null;

             if (_curIndex < _dict.Count)
             {
                 var kvp = _dict.ElementAt(_curIndex++);
                 nextObj = kvp.Key;
             }

            return nextObj;
        }

    }
}
