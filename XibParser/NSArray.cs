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
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Collections;

namespace Smartmobili.Cocoa
{
    public class NSArray : NSObject, IList<object>
    {
        private readonly IList<object> _list = new List<object>();

        public NSArray()
        {
           
        }

        //public NSArray(NSObjectDecoder aDecoder) : base(aDecoder)
        //{
           
        //}

        public override id InitWithCoder(NSObjectDecoder decoder)
        {
            base.InitWithCoder(decoder);

            var xElement = decoder.XmlElement;
            var nodes = xElement.Elements();
            foreach (var node in nodes)
            {
                if (node.Name == "bool" && node.Attribute("key") != null)
                    continue;

                Add(decoder.Create(node));
            }

            return this;
        }


        //public static NSArray Create(NSObjectDecoder aDecoder)
        //{
        //    NSArray nsArray = new NSArray();

        //    var xElement = aDecoder.XmlElement;
        //    var nodes = xElement.Elements();
        //    foreach (var node in nodes)
        //    {
        //        if (node.Name == "bool" && node.Attribute("key") != null)
        //            continue;

        //        nsArray.Add(aDecoder.Create(node));

        //    }

        //    return nsArray;
        //}

        //public static NSArray Create(XElement xElement)
        //{
        //    NSArray nsArray = new NSArray();

        //    var nodes = xElement.Elements();
        //    foreach (var node in nodes)
        //    {
        //        if (node.Name == "bool" && node.Attribute("key") != null)
        //            continue;

        //        nsArray.Add(NSObjectDecoder.Create(node));

        //    }

        //    return nsArray;
        //}


        //public static NSArray Parse(IOrderedEnumerable<XElement> dictSortedKeys)
        //{
        //    NSArray nsArray = new NSArray();

        //    foreach (XElement xElement in dictSortedKeys)
        //    {
        //        object nsObj = NSObjectDecoder.Create(xElement);
        //        nsArray.Add(nsObj);
        //    }

        //    return nsArray;
        //}




        #region Implementation of IEnumerable

        public IEnumerator<object> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<NSObject>

        public void Add(object item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(object item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(object[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(object item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return _list.IsReadOnly; }
        }

        #endregion

        #region Implementation of IList<INSObject>

        public int IndexOf(object item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, object item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public object this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        #endregion

        #region Your Added Stuff

       

        #endregion
    }



    //public class NSArray : List<object>
    //{

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="xmlReader"></param>
    //    /// <returns></returns>
    //    public static NSArray Create(XElement xElement)
    //    {
    //        NSArray nsArray = new NSArray();

    //        var descendants = xElement.Descendants();
    //        foreach (var desc in descendants)
    //        {
    //            if (desc.Name == "bool" && desc.Attribute("key") != null)
    //                continue;
                
    //            nsArray.Add(desc.Value);

    //        }


    //        //var descNodes = xElement.DescendantNodes();


    //        //  <array key="IBDocument.PluginDependencies">
    //        //      <string>com.apple.InterfaceBuilder.IBCocoaTouchPlugin</string>
    //        //  </array>
    //        //              OR
    //        //  <object class="NSArray" key="IBDocument.PluginDependencies">
    //        //      <bool key="EncodedWithXMLCoder">YES</bool>
    //        //      <string>com.apple.InterfaceBuilder.CocoaPlugin</string>
    //        //  </object>


    //        return nsArray;
    //    }
    //}
}
