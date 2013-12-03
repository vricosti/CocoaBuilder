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
    public class NSXMLParserInfo : NSObject
    {
        public LibXml.xmlSAXHandler saxHandler; //0x04
        public IntPtr parserContext; //0x08
        public uint parserflasg; // 0x0C
        public NSError error; //0x10
        public NSMutableArray namespaces; //0x14
        public id/*NSMapTable*/ slowStringMap; //0x18
        public bool delegateAborted; //0x1C
        public bool haveDetectedEncoding; //0x1D
        public NSData bomChunk; //0x20
        public int chunkSize; //0x24
    }

    public class NSXMLParserWIP : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLParserWIP));
        new public static NSXMLParserWIP Alloc() { return new NSXMLParserWIP(); }

        static volatile bool _isInited;
        static object syncRoot = new Object();

        IntPtr _reserved0; //0x04
        id _delegate; //0x08
        NSXMLParserInfo _reserved1; //0x0C
        IntPtr _reserved2; // 0x10
        IntPtr _reserved3; //0x14


        public virtual id Delegate
        {
            get { return GetDelegate(); }
            set { SetDelegate(value); }
        }
        
        public virtual void SetDelegate(id dlgate) 
        { 
            _delegate = dlgate; 
        }
        
        public virtual id GetDelegate() 
        { 
            return _delegate; 
        }


        public static void SetupLibXml()
        {
            if (_isInited == false)
            {
                lock (syncRoot)
                {
                    if (_isInited == false)
                    {
                        LibXml.xmlInitParser();
                        _isInited = true;
                    }
                }
            }
        }


        public virtual id InitWithData(NSData data)
        {
            id self = this;

            SetupLibXml();

            _reserved1 = (NSXMLParserInfo)NSXMLParserInfo.Alloc().Init();
            _reserved1.saxHandler = new LibXml.xmlSAXHandler();

             //string xmlIn = 
             //    @"<test:Plan xmlns:test='http://test.org/schema'>" + 
             //    "<test:Case name='test1' emptyAttribute='' test:ns_id='auio'>" +
             //    "</test:Case>" + 
             //    "</test:Plan>";

             //LibXml.xmlSAXHandler saxHandler = new LibXml.xmlSAXHandler();

            return self;
        }

        public virtual void Parse()
        {

        }
    }
}
