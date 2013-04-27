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

namespace Smartmobili.Cocoa
{
    public class NSXMLParser
    {
        protected INSXMLParser _nsXmlInterface;

        protected NSData _data;

        //protected SaxParser _saxParser;


        public static NSXMLParser Alloc()
        {
            return new NSXMLParser();
        }


        public NSXMLParser InitWithData(NSData aData)
        {
            _data = aData;

            //string xml = "Convert _data.Bytes into string";
            //_saxParser = new SaxParser(xmlText);
            //_saxParser.StartElement += new SaxParserDelegate(this.StartElement);
            //_saxParser.EndElement += new SaxParserDelegate(this.StartElement);

            return this;
        }


        public void SetDelegate(INSXMLParser nsXmlInterface)
        {
            _nsXmlInterface = nsXmlInterface;
        }

        

        public void Parse()
        {
            //if (_saxParser == null || _nsXmlInterface == null)
            //    return;

            //_saxParser.Parse();
        }


        //public virtual void StartElement(string uri, string localName, string qName, IAttributes atts)
        //{
        //    // You should call ParserDidStartElement
        //    if (_nsXmlInterface != null)
        //    {
        //        NSMutableDictionary attributeDict = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
        //        for (int indx = 0; indx < atts.Length; indx++)
        //        {
        //            NSString key = (NSString)atts.GetQName(indx);
        //            NSString val = (NSString)atts.GetValue(indx);
        //            attributeDict.Add(key, val);
        //        }

        //        _nsXmlInterface.ParserDidStartElement(this, localName, uri, qName, attributeDict);
        //    }
        //}
        



    }
}
