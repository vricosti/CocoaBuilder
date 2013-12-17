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
        new public static NSDictionary alloc() { return new NSDictionary(); }

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

        public static NSDictionary dictionary()
        {
            return (NSDictionary)NSDictionary.alloc().init();
        }

        public virtual id initWithDictionary(NSDictionary aDictionary)
        {
            id self = this;

            foreach (KeyValuePair<id, id> entry in aDictionary)
            {
                this.Add(entry.Key, entry.Value);
            }

            return self;
        }


        public override id initWithCoder(NSCoder aCoder)
        {
            id self = this;

            if (aCoder.AllowsKeyedCoding)
            {
                id keys = null;
                id objects = null;

                if (aCoder.containsValueForKey(@"NS.keys"))
                {
                    keys = ((NSKeyedUnarchiver)aCoder)._decodeArrayOfObjectsForKey(@"NS.keys");
                    objects = ((NSKeyedUnarchiver)aCoder)._decodeArrayOfObjectsForKey(@"NS.objects");
                }
                else if (aCoder.containsValueForKey(@"dict.sortedKeys"))
                {
                    keys = aCoder.decodeObjectForKey(@"dict.sortedKeys");
                    objects = aCoder.decodeObjectForKey(@"dict.values");
                }

                if (keys == null)
                {
                    uint i = 0;
                    NSString key;
                    id val;

                    keys = NSArray.arrayWithCapacity(2);
                    objects = NSArray.arrayWithCapacity(2);

                    key = NSString.stringWithFormat(@"NS.object.%u", i);
                    val = ((NSKeyedUnarchiver)aCoder).decodeObjectForKey(key);

                    while (val != null)
                    {
                        ((NSArray)objects).addObject(val);
                        key = NSString.stringWithFormat(@"NS.key.%u", i);
                        val = ((NSKeyedUnarchiver)aCoder).decodeObjectForKey(key);
                        ((NSArray)keys).addObject(val);
                        i++;
                        key = NSString.stringWithFormat(@"NS.object.%u", i);
                        val = ((NSKeyedUnarchiver)aCoder).decodeObjectForKey(key);
                    }
                }

                return initWithObjectsForKeys((NSArray)objects, (NSArray)keys);
                //return [self initWithObjects: objects forKeys: keys];;
            }


            return self;
        }

        public static id dictionaryWithObjectsForKeys(NSArray objects, NSArray keys)
        {
            return (NSDictionary)NSDictionary.alloc().initWithObjectsForKeys(objects, keys);
        }

        public virtual id initWithObjectsForKeys(NSArray objects, NSArray keys)
        {
            id self = this;

            if (objects != null && keys != null && (objects.Count == keys.Count))
            {
                for (uint i = 0; i < objects.count(); i++)
                {
                    id key = keys.objectAtIndex(i);
                    id val = objects.objectAtIndex(i);
                    if (key == null || val == null)
                        throw new ArgumentNullException();

                    this.Add(key, val);
                }
            }

            return self;
        }



        public virtual id initWithObjectsAndKeys(params id[] objkey)
        {
            NSArray objects = null;
            NSArray keys = null;

            objects = new NSArray(objkey.ToList().Where((c, i) => (i % 2 == 0) && (c != null)));
            keys = new NSArray(objkey.ToList().Where((c, i) => i % 2 != 0));
            
            return initWithObjectsForKeys(objects, keys);
        }


        public virtual bool isEqualToDictionary(NSDictionary otherDictionary)
        {
            if (this.Count != otherDictionary.Count)
                return false;

            foreach (KeyValuePair<id, id> kvp in this)
            {
                if (otherDictionary.ContainsKey(kvp.Key))
                {
                    if (!kvp.Value.isEqual(this[kvp.Key]))
                        return false;
                }
                else
                    return false;
            }

            return true;
        }



        public virtual NSArray allKeysForObject(id anObject)
        {
            NSArray objs = (NSArray)NSArray.alloc().init();

            foreach (KeyValuePair<id, id> kvp in this)
            {
                if (kvp.Value.isEqual(anObject))
                    objs.Add(kvp.Key);
            }

            return objs;
        }


        public virtual void removeObjectsForKeys(NSArray keyArray)
        {
            foreach (id key in keyArray)
            {
                removeObjectForKey(key);
            }
        }



        //public override id initWithCoder(NSCoder decoder)
        //{
        //    base.initWithCoder(decoder);

        //    //var xElement = decoder.XmlElement;
        //    //var firstElem = xElement.elements().FirstOrDefault();

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

        //    //            var dictSortedKeyNode = xElement.elements().Where(e => (string)e.Attribute("key") == "dict.sortedKeys").ToArray();
        //    //            var dictValueNode = xElement.elements().Where(e => (string)e.Attribute("key") == "dict.values").ToArray();
        //    //            if (dictSortedKeyNode.Count() == 1 && dictValueNode.Count() == 1)
        //    //            {
        //    //                dictSortedKeyElements = dictSortedKeyNode.elements().Where(e => e.Name != "bool").ToArray();
        //    //                dictValueElements = dictValueNode.elements().Where(e => e.Name != "bool").ToArray();
        //    //            }
        //    //        }
        //    //        else
        //    //        {
        //    //            dictSortedKeyElements = xElement.elements().Where(c =>
        //    //               ((string)c.Attribute("key")).StartsWith("NS.key.")).OrderBy(c => (string)c.Attribute("key")).ToArray();

        //    //            dictValueElements = xElement.elements().Where(c =>
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
        //    //        foreach (var xElm in decoder.XmlElement.elements())
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

        public virtual NSEnumerator keyEnumerator()
        {
            return (NSEnumerator)NSDictionaryEnumerator.alloc().InitWithDictionary(this);
        }


        public virtual void setObjectForKey(id anObject, id aKey)
        {
            if (anObject == null)
                throw new ArgumentNullException("anObject");
            if (aKey == null)
                throw new ArgumentNullException("aKey");

            this[aKey] = anObject;
        }


        public virtual id objectForKey(id aKey)
        {
            id obj = null;

            if (this.ContainsKey(aKey))
            {
                obj = this[aKey];
            }

            return obj;
        }

        public virtual void removeObjectForKey(id aKey)
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
            return (NSString)this.objectForKey((NSString)"NSFileType");
        }

        public virtual bool writeToFile(NSString path, bool flag)
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
        new public static NSDictionaryEnumerator alloc() { return new NSDictionaryEnumerator(); }

        protected NSDictionary _dict;
        protected int _curIndex;

        public id InitWithDictionary(NSDictionary aDictionary)
        {
            id self = this;

            _dict = aDictionary;
            _curIndex = 0;

            return self;
        }


        public override id nextObject()
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
