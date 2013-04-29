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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Org.System.Xml.Sax;
using Org.System.Xml.Sax.Helpers;
using SaxConsts = Org.System.Xml.Sax.Constants;




namespace Smartmobili.Cocoa
{
    public class NSXMLParser : IContentHandler
    {
        protected INSXMLParser _nsXmlInterface;

        protected NSData _data;

        protected string _xmlSource = string.Empty;

        protected IXmlReader _saxParser = null;

        //protected SaxParser _saxParser;


        public static NSXMLParser Alloc()
        {
            return new NSXMLParser();
        }

        public NSXMLParser()
        {
            // TODO remove the necessity to load this assembly
            string strAssemDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string strAssemPath = System.IO.Path.Combine(strAssemDir, "AElfred.dll");
            Assembly assem = Assembly.LoadFrom(strAssemPath);
            _saxParser = SaxReaderFactory.CreateReader(assem, null);
        }


        public NSXMLParser InitWithData(NSData aData)
        {
            _data = aData;
            if (_data == null || _data.Bytes == null)
                return null;

            _xmlSource = System.Text.Encoding.Default.GetString(_data.Bytes);

            return this;
        }


        public void SetDelegate(INSXMLParser nsXmlInterface)
        {
            _nsXmlInterface = nsXmlInterface;
            if (_nsXmlInterface != null)
            {
            }
        }

        

        public void Parse()
        {
            if (_saxParser == null || _nsXmlInterface == null || _data == null)
                return;

            try
            {
                _saxParser.SetFeature(SaxConsts.NamespacesFeature, true);
                _saxParser.SetFeature(SaxConsts.ExternalGeneralFeature, true);
                _saxParser.SetFeature(SaxConsts.ExternalParameterFeature, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
           
            _saxParser.ContentHandler = this;

            MemoryStream stream = new MemoryStream(_data.Bytes, false);
            _saxParser.Parse(new InputSource<Stream>(stream));
        }

        public void Characters(char[] ch, int start, int length)
        {
            if (_nsXmlInterface != null)
            {
                NSString str = (NSString)new string(ch, start, length);
                
                if (str == "{{357, 418}, {480, 270}}")
                {
                    System.Diagnostics.Debug.WriteLine("bp");
                }
                _nsXmlInterface.ParserFoundCharacters(this, str);
            }
        }

        public void StartElement(string uri, string localName, string qName, IAttributes atts)
        {
            //System.Diagnostics.Debug.WriteLine("StartElement");
            if (_nsXmlInterface != null)
            {
                NSMutableDictionary attributeDict = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
                for (int indx = 0; indx < atts.Length; indx++)
                {
                    NSString key = (NSString)atts.GetQName(indx);
                    NSString val = (NSString)atts.GetValue(indx);
                    attributeDict.Add(key, val);
                }

                _nsXmlInterface.ParserDidStartElement(this, localName, uri, qName, attributeDict);
            }
        }

        public void EndElement(string uri, string localName, string qName)
        {
            if (_nsXmlInterface != null)
            {
                _nsXmlInterface.ParserDidEndElement(this, localName, uri, qName);
            }
        }

        public void EndPrefixMapping(string prefix)
        {
            System.Diagnostics.Debug.WriteLine("EndPrefixMapping");
        }

        public void IgnorableWhitespace(char[] ch, int start, int length)
        {
            System.Diagnostics.Debug.WriteLine("IgnorableWhitespace");
        }

        public void ProcessingInstruction(string target, string data)
        {
            System.Diagnostics.Debug.WriteLine("ProcessingInstruction");
        }

        public void SetDocumentLocator(ILocator locator)
        {
            System.Diagnostics.Debug.WriteLine("SetDocumentLocator");
        }

        public void SkippedEntity(string name)
        {
            System.Diagnostics.Debug.WriteLine("SkippedEntity");
        }

        public void StartDocument()
        {
            System.Diagnostics.Debug.WriteLine("StartDocument");
        }

        public void EndDocument()
        {
            System.Diagnostics.Debug.WriteLine("EndDocument");
        }

        

        public void StartPrefixMapping(string prefix, string uri)
        {
            System.Diagnostics.Debug.WriteLine("StartPrefixMapping");
        }
    }
}
