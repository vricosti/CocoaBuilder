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
    public class NSDictionary : NSObject, IDictionary<object, object>
    {
        protected Dictionary<object, object> _dict = new Dictionary<object, object>();

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
            Init();
        }

        public NSDictionary(NSArray sortedKeys, NSMutableArray values)
            : this()
        {

        }

        public static NSDictionary Alloc()
        {
            return new NSDictionary();
        }

        public NSDictionary Init()
        {
            return this;
        }


        public override NSObject InitWithCoder(NSObjectDecoder decoder)
        {
            base.InitWithCoder(decoder);

            var xElement = decoder.XmlElement;
            var firstElem = xElement.Elements().FirstOrDefault();

            if (firstElem != null)
            {
                var keyAttr = firstElem.Attributes("key").FirstOrDefault();

                XElement[] dictSortedKeyElements = new XElement[0];
                XElement[] dictValueElements = new XElement[0];

                if (xElement.Name == "object")
                {
                    if ((xElement.Descendants().Count() > 0 && firstElem.Name == "bool"
                        && keyAttr != null && keyAttr.Value == "EncodedWithXMLCoder" && firstElem.Value == "YES"))
                    {
                        //System.Diagnostics.Debug.WriteLine("NSMutableDictionary");

                        var dictSortedKeyNode = xElement.Elements().Where(e => (string)e.Attribute("key") == "dict.sortedKeys").ToArray();
                        var dictValueNode = xElement.Elements().Where(e => (string)e.Attribute("key") == "dict.values").ToArray();
                        if (dictSortedKeyNode.Count() == 1 && dictValueNode.Count() == 1)
                        {
                            dictSortedKeyElements = dictSortedKeyNode.Elements().Where(e => e.Name != "bool").ToArray();
                            dictValueElements = dictValueNode.Elements().Where(e => e.Name != "bool").ToArray();
                        }
                    }
                    else
                    {
                        dictSortedKeyElements = xElement.Elements().Where(c =>
                           ((string)c.Attribute("key")).StartsWith("NS.key.")).OrderBy(c => (string)c.Attribute("key")).ToArray();

                        dictValueElements = xElement.Elements().Where(c =>
                           ((string)c.Attribute("key")).StartsWith("NS.object.")).OrderBy(c => (string)c.Attribute("key")).ToArray();
                    }

                    if ((dictSortedKeyElements.Count() == dictValueElements.Count()) &&
                       (dictSortedKeyElements.Count() > 0))
                    {
                        for (int i = 0; i < dictSortedKeyElements.Count(); i++)
                        {
                            object keyObj = decoder.Create(dictSortedKeyElements[i]);
                            object valueObj = decoder.Create(dictValueElements[i]);
                            Add(keyObj, valueObj);
                        }
                    }
                }
                else if (xElement.Name == "dictionary")
                {
                    foreach (var xElm in decoder.XmlElement.Elements())
                    {
                        string key = xElm.AttributeValueOrDefault("key", null);
                        if (!string.IsNullOrWhiteSpace(key))
                        {
                            object valueObj = decoder.Create(xElm);
                            Add(key, valueObj);
                        }
                    }
                }



            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Empty dictionary");
            }

            return this;
        }

        

        public void Add(object key, object value)
        {
            _dict.Add(key, value);
        }

        public bool ContainsKey(object key)
        {
            return _dict.ContainsKey(key);
        }

        public ICollection<object> Keys
        {
            get { return _dict.Keys; }
        }

        public bool Remove(object key)
        {
            return _dict.Remove(key);
        }

        public bool TryGetValue(object key, out object value)
        {
            return _dict.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return _dict.Values; }
        }

        public object this[object key]
        {
            get
            {
                return _dict[key];
            }
            set
            {
                _dict[key] = value;
            }
        }


        #region ICollection<KeyValuePair<object,object>> Members

        public void Add(KeyValuePair<object, object> item)
        {
            _dict.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dict.Clear();
        }

        public bool Contains(KeyValuePair<object, object> item)
        {
            return (_dict.ContainsKey(item.Key) && _dict.ContainsValue(item.Value));
        }

        public void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex)
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

        public bool Remove(KeyValuePair<object, object> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<object, object>> Members

        public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
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
}
