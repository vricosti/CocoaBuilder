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

        protected NSString _encoding; //0x14(x86)

        protected NSString _version; //0x18(x86)

        protected NSXMLDTD _docType;

        protected NSArray _children;

        protected bool _childrenHaveMutated;

        protected bool _standalone; //0x25(x86)

        protected NSXMLElement _rootElement;

        protected NSString _URI;

        protected id _extraIvars;

        protected uint _fidelityMask;

        protected NSXMLDocumentContentKind _contentKind;


        static byte[] _sXMLDeclUTF8 = { 0x3C, 0x3F, 0x78, 0x6D, 0x6C };
        static byte[] _sXMLDeclUTF16BE = { 0x00, 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C };
        static byte[] _sXMLDeclUTF16LE = { 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C, 0x00 };
        static byte[] _sXMLDeclUTF8BOM = { 0xEF, 0xBB, 0xBF, 0x3C, 0x3F, 0x78, 0x6D, 0x6C };
        static byte[] _sXMLDeclUTF16BEBOM = { 0xFE, 0xFF, 0x00, 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C };
        static byte[] _sXMLDeclUTF16LEBOM = { 0xFF, 0xFE, 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C, 0x00 };

        public static Class replacementClassForClass(Class currentClass)
        {
            Class result = currentClass;

            if (NSXMLDocument.Class == currentClass)
                result = NSXMLDocument.Class;

            return result;
        }

        

        public override id objectValue()
        {
            return this.stringValue();
        }

        public virtual NSXMLDTD DTD()
        {
            return _docType;
        }

        public virtual void setDTD(NSXMLDTD dtd)
        {
            if (_docType != dtd)
                _docType = dtd;
        }

        public override NSString URI()
        {
            return _URI;
        }

        public override void setURI(NSString uri)
        {
            if (_URI != uri)
                _URI = uri;
        }

        public virtual void setStandalone(bool standalone)
        {
            _standalone = standalone;
        }

        public virtual void setVersion(NSString version)
        {
            if (_version != version)
            {
                _version.release();
                _version = version.copy();
            }
        }

        public virtual void setCharacterEncoding(NSString encoding)
        {
            if (_encoding != encoding)
            {
                _encoding.release();
                _encoding = encoding.copy();
            }
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

       public override uint childCount()
       {
           return this._children.count();
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
        

        public virtual bool _validateWithSchemaAndReturnError(NSError error)
        {
            return false;
        }

        public virtual id _tidyWithData(NSData data, ref NSError error, bool isXML, uint detectedEncoding)
        {
            NSXMLDocument self = (NSXMLDocument)this.init();



            return self;
        }
        
        

        //int __cdecl __43__NSXMLDocument__setContentKindAndEncoding__block_invoke449(int a1, void *a2)
        //{
        //  unsigned __int8 v2; // bl@1
        //  void *v3; // eax@2
        //  void *v4; // esi@3
        //  void *v5; // ecx@4
        //  void *v6; // eax@4
        
        //  v2 = 0;
        //  if ( objc_msgSend(a2, "kind") == (__int32 *)((char *)&stru_0.__sig + 2) )
        //  {
        //    v3 = objc_msgSend(a2, "name");
        //    if ( !objc_msgSend(v3, "caseInsensitiveCompare:", &cfstr_Meta) )
        //    {
        //      v4 = objc_msgSend(a2, "attributes");
        //      if ( objc_msgSend(v4, "indexOfObjectPassingTest:", &__block_literal_global460) != (void *)2147483647 )
        //      {
        //        v5 = objc_msgSend(v4, "indexOfObjectPassingTest:", &__block_literal_global467);
        //        v6 = 0;
        //        if ( v5 != (void *)2147483647 )
        //          v6 = objc_msgSend(v4, "objectAtIndex:", v5);
        //        objc_assign_strongCast(v6, *(_DWORD *)(*(_DWORD *)(a1 + 20) + 4) + 24);
        //        v2 = *(_DWORD *)(*(_DWORD *)(*(_DWORD *)(a1 + 20) + 4) + 24) != 0;
        //      }
        //    }
        //  }
        //  return v2;
        //}

        //int __cdecl __43__NSXMLDocument__setContentKindAndEncoding__block_invoke_2(int a1, void *a2)
        //{
        //  void *v2; // eax@1
        //  void *v3; // eax@1
        //  unsigned __int8 v4; // cl@1
        //  void *v5; // eax@2
        
        //  v2 = objc_msgSend(a2, "name");
        //  v3 = objc_msgSend(v2, "caseInsensitiveCompare:", &cfstr_HttpEquiv);
        //  v4 = 0;
        //  if ( !v3 )
        //  {
        //    v5 = objc_msgSend(a2, "stringValue");
        //    v4 = objc_msgSend(v5, "caseInsensitiveCompare:", &cfstr_ContentType_1) == 0;
        //  }
        //  return v4;
        //}
        
        //bool __cdecl __43__NSXMLDocument__setContentKindAndEncoding__block_invoke_3(int a1, void *a2)
        //{
        //  void *v2; // eax@1
        
        //  v2 = objc_msgSend(a2, "name");
        //  return objc_msgSend(v2, "caseInsensitiveCompare:", &cfstr_Content) == 0;
        //}


        protected virtual void _setContentKindAndEncoding()
        {
            if (this._rootElement != null)
            {
                if (this._rootElement.name().caseInsensitiveCompare("html") != 0)
                    return;
            }
            NSXMLNode nsNode = _rootElement.namespaceForPrefix("");
            if((_docType != null && _docType.systemID().hasPrefix("http://www.w3.org/TR/xhtml1/DTD/xhtml1")) ||
                nsNode.stringValue().isEqualToString("http://www.w3.org/1999/xhtml"))
            {
                _contentKind = NSXMLDocumentContentKind.NSXMLDocumentXHTMLKind;
            }
            else
            {
                _contentKind = NSXMLDocumentContentKind.NSXMLDocumentHTMLKind;
            }
            NSArray children = _rootElement.children();
            if (children != null)
            {
                uint headIndex = children.indexOfObjectPassingTest(i =>
                {
                    bool found = false;
                    NSXMLElement element = i as NSXMLElement;
                    if (element.kind() == NSXMLNodeKind.NSXMLElementKind)
                    {
                        found = (element.name().caseInsensitiveCompare("head") == 0);
                    }
                    return found;
                });
                if(headIndex != NS.NotFound)
                {
                    NSXMLElement headElement = (NSXMLElement)children.objectAtIndex(headIndex);
                    //TO BE CONTINUED ...
                    //For now I don't care about html handling
                }
            }

        }



        

    }
}
