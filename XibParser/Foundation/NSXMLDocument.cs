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


namespace Smartmobili.Cocoa
{
    public enum NSXMLDocumentContentKind
    {
        NSXMLDocumentXMLKind = 0,
        NSXMLDocumentXHTMLKind,
        NSXMLDocumentHTMLKind,
        NSXMLDocumentTextKind
    }

    public class NSXMLDocument : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLDocument));
        new public static NSXMLDocument alloc() { return new NSXMLDocument(); }

        protected NSString _encoding;

        protected NSString _version;

        protected NSXMLDTD _docType;

        protected NSArray _children;

        protected bool _childrenHaveMutated;

        protected bool _standalone;

        protected NSXMLElement _rootElement;

        protected NSString _URI;

        protected id _extraIvars;

        protected uint _fidelityMask;

        protected NSXMLDocumentContentKind _contentKind;


        static const byte[] _sXMLDeclUTF8 = { 0x3C, 0x3F, 0x78, 0x6D, 0x6C };
        static const byte[] _sXMLDeclUTF16BE = { 0x00, 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C };
        static const byte[] _sXMLDeclUTF16LE = { 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C, 0x00 };
        static const byte[] _sXMLDeclUTF8BOM = { 0xEF, 0xBB, 0xBF, 0x3C, 0x3F, 0x78, 0x6D, 0x6C };
        static const byte[] _sXMLDeclUTF16BEBOM = { 0xFE, 0xFF, 0x00, 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C };
        static const byte[] _sXMLDeclUTF16LEBOM = { 0xFF, 0xFE, 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C, 0x00 };




        public virtual void setCharacterEncoding(NSString encoding)
        {
            _encoding = encoding;
        }

        public virtual NSString characterEncoding()
        {
            return _encoding;
        } 
       public virtual void setRootElement(NSXMLElement rootElement)
       {
           _rootElement = rootElement;
       }

       public virtual NSXMLElement rootElement()
       {
           return _rootElement;
       }

       public virtual void setDocumentContentKind(NSXMLDocumentContentKind kind)
       {
           _contentKind = kind;
       }

       public virtual NSXMLDocumentContentKind documentContentKind()
       {
           return _contentKind;
       }

       
       

       public override id init()
       {
           id self = base.init();

           if (self != null)
           {
               self = this;
               this._kind = NSXMLNodeKind.NSXMLDocumentKind;
               this._fidelityMask = 0x0;
               this._contentKind = 0x0;
           }

           return self;
       }

        public virtual id initWithRootElement(NSXMLElement rootElement)
        {
            id self = this.init();
            if (self != null)
            {
                this.setRootElement(rootElement);
            }

            return self;
        }

        

        protected virtual id _initWithData(NSData data, NSString encoding, uint mask, ref NSError error)
        {
            this.setCharacterEncoding(encoding);
            return initWithData(data, mask, ref error);
        }

        public virtual id initWithData(NSData data, uint mask, ref NSError error)
        {
            id self = null;

            if (data == null)
            {
                NSException.raise("NSInvalidArgumentException", "");
            }

            if (data.length() == 0)
            {
                return null;
            }

            bool isXML = true;
            uint encoding = 0xFFFFFFFF;
            if ((mask & 0x200) != 0)
            {
                var bytes = data.bytes();
                if (data.length() >= 12)
                {
                    if (ByteUtil.compare(_sXMLDeclUTF8, bytes, 5) != 0)
                    {
                        encoding = 0x10000100;
                        if (ByteUtil.compare(_sXMLDeclUTF16BE, bytes, 10) != 0)
                        {
                            if (ByteUtil.compare(_sXMLDeclUTF16LE, bytes, 10) != 0)
                            {
                                 if (ByteUtil.compare(_sXMLDeclUTF8BOM, bytes, 8) != 0)
                                 {
                                     if (ByteUtil.compare(_sXMLDeclUTF16BEBOM, bytes, 12) != 0)
                                     {
                                         int cmpRet = ByteUtil.compare(_sXMLDeclUTF16LEBOM, bytes, 12);
                                         isXML = (cmpRet == 0);
                                         encoding = 0x14000100; // FIXME not sure about that
                                     }
                                 }
                                 else
                                 {
                                     encoding = 0x8000100;
                                 }
                            }
                            else
                            {
                                encoding = 0x14000100;
                            }
                        }
                    }
                }
            }

            if (((mask & 0x4) != 0) || (((mask & (uint)NSXMLNodeOptions.NSXMLDocumentTidyXML) != 0) && isXML))
            {
                if (NSXMLTidy.isLoaded() == false)
                    NSXMLTidy.loadTidy();
                if (NSXMLTidy.isLoaded() == false)
                    return null;

                NSXMLDocument doc = (NSXMLDocument)_tidyWithData(data, ref error, isXML, encoding);
                self = doc;
                doc._setContentKindAndEncoding();
                if (isXML)
                    setDocumentContentKind(NSXMLDocumentContentKind.NSXMLDocumentXMLKind);
                if (doc.characterEncoding() == null)
                    doc.setCharacterEncoding(_encoding);
            }
            else
            {
                NSXMLTreeReader treeReader = (NSXMLTreeReader)NSXMLTreeReader.alloc().initWithData(data, Class, mask, ref error);
                NSXMLDocument doc = (NSXMLDocument)treeReader.parse();
                if(doc != null)
                {
                    if (doc.kind() == NSXMLNodeKind.NSXMLDocumentKind)
                    {
                        self = doc.retain();
                        doc._fidelityMask = mask;
                        doc._setContentKindAndEncoding();
                        if (doc.characterEncoding() == null)
                            doc.setCharacterEncoding(_encoding);
                    }
                }
            }

            return self;
        }


        public virtual id _tidyWithData(NSData data, ref NSError error, bool isXML, uint detectedEncoding)
        {
            NSXMLDocument self = (NSXMLDocument)this.init();



            return self;
        }
        
        protected virtual void _setContentKindAndEncoding()
        {
            if (this._rootElement != null)
            {
                if (this._rootElement.name().caseInsensitiveCompare("html") != 0)
                    return;
            }
        }



        public override uint childCount()
        {
            return this._children.count();
        }

    }
}
