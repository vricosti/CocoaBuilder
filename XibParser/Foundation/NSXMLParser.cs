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
    //https://github.com/gnustep/gnustep-base/blob/master/Headers/Foundation/NSXMLParser.h
    //https://github.com/gnustep/gnustep-base/blob/master/Source/NSXMLParser.m
    public class NSXMLParser : NSObject, IContentHandler
    {
        protected INSXMLParser _nsXmlInterface;

        protected NSData _data;

        protected string _xmlSource = string.Empty;

        protected IXmlReader _saxParser = null;

        protected id _delegate;

        private StringBuilder _builder;

        
        

        // Not used for the moment ...
        // We use an good old Interface instead of reflection (see setDelegate)
        public virtual id Delegate
        {
            get { return _delegate; }
            set { _delegate = value; }
        }

        public void setDelegate(INSXMLParser nsXmlInterface)
        {
            _nsXmlInterface = nsXmlInterface;
            if (_nsXmlInterface != null)
            {
            }
        }



        new public static NSXMLParser alloc()
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
            _builder = new StringBuilder();
        }


        public NSXMLParser initWithData(NSData aData)
        {
            _data = aData;
            if (_data == null || _data.bytes() == null)
                return null;

            _xmlSource = System.Text.Encoding.Default.GetString(_data.bytes());

            return this;
        }


        

        

        public void parse()
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

            MemoryStream stream = new MemoryStream(_data.bytes(), false);
            _saxParser.Parse(new InputSource<Stream>(stream));
        }

        public void Characters(char[] ch, int start, int length)
        {
            // Actually the sax parser is weird because sometimes the string is splitted.
            // So we don't call parserFoundCharacters from here but inside startElement and endElement 
            // through charactersWorkaround()
            if (_nsXmlInterface != null)
            {
                _builder.Append(ch, start, length);

                //The following lines are used to debug the sax parser when using Compose.xib
                //NSString nodeText = _builder.ToString();
                //if (nodeText == "{{357, 418}, {480, 270}}") { System.Diagnostics.Debug.WriteLine("bp"); }
            }
        }

        public void CharactersWorkaround()
        {
            if (_builder.Length > 0)
            {
                _nsXmlInterface.parserFoundCharacters(this, _builder.ToString());
                _builder.Length = 0;
            }
        }


        public void StartElement(string uri, string localName, string qName, IAttributes atts)
        {
            //System.Diagnostics.Debug.WriteLine("startElement");
            if (_nsXmlInterface != null)
            {
                CharactersWorkaround();

                NSMutableDictionary attributeDict = (NSMutableDictionary)NSMutableDictionary.alloc().init();
                for (int indx = 0; indx < atts.Length; indx++)
                {
                    NSString key = (NSString)atts.GetQName(indx);
                    NSString val = (NSString)atts.GetValue(indx);
                    attributeDict.Add(key, val);
                }

                //SEL sel;
                //if (Objc.respondsToSelector(_delegate, "parserDidStartElement", ref sel)) 
                //{
                //    sel.MsgSend(this, localName, uri, qName, attributeDict);
                //}
                _nsXmlInterface.parserDidStartElement(this, localName, uri, qName, attributeDict);
            }
        }

        public void EndElement(string uri, string localName, string qName)
        {
            if (_nsXmlInterface != null)
            {
                CharactersWorkaround();

                _nsXmlInterface.parserDidEndElement(this, localName, uri, qName);
            }
        }

        public void EndPrefixMapping(string prefix)
        {
            System.Diagnostics.Debug.WriteLine("endPrefixMapping");
        }

        public void IgnorableWhitespace(char[] ch, int start, int length)
        {
            System.Diagnostics.Debug.WriteLine("ignorableWhitespace");
        }

        public void ProcessingInstruction(string target, string data)
        {
            System.Diagnostics.Debug.WriteLine("processingInstruction");
        }

        public void SetDocumentLocator(ILocator locator)
        {
            System.Diagnostics.Debug.WriteLine("setDocumentLocator");
        }

        public void SkippedEntity(string name)
        {
            System.Diagnostics.Debug.WriteLine("skippedEntity");
        }

        public void StartDocument()
        {
            System.Diagnostics.Debug.WriteLine("StartDocument");
            _builder.Length = 0;
        }

        public void EndDocument()
        {
            System.Diagnostics.Debug.WriteLine("endDocument");
        }

        

        public void StartPrefixMapping(string prefix, string uri)
        {
            System.Diagnostics.Debug.WriteLine("startPrefixMapping");
        }
    }
}
