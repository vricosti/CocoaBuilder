using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using xmlSAXHandlerPtr = System.IntPtr;
using NodePtr = System.IntPtr;
using DocPtr = System.IntPtr;
using NsPtr = System.IntPtr;
using AttrPtr = System.IntPtr;
using DtdPtr = System.IntPtr;

namespace Smartmobili.Cocoa
{
    public enum ElementType 
    {
        ELEMENT_NODE,
        ATTRIBUTE_NODE,
        TEXT_NODE,
        CDATA_SECTION_NODE,
        ENTITY_REF_NODE,
        ENTITY_NODE,
        PI_NODE,
        COMMENT_NODE,
        DOCUMENT_NODE,
        DOCUMENT_TYPE_NODE,
        DOCUMENT_FRAG_NODE,
        NOTATION_NODE,
        HTML_DOCUMENT_NODE,
        DTD_NODE,
        ELEMENT_DECL,
        ATTRIBUTE_DECL,
        ENTITY_DECL,
        NAMESPACE_DECL,
        XINCLUDE_START,
        XINCLUDE_END,
        DOCB_DOCUMENT_NODE,
    }

    public enum XmlElementType
    {
        XML_ELEMENT_UNDEF = 0,
        XML_ELEMENT_NODE = 1,
        XML_ATTRIBUTE_NODE = 2,
        XML_TEXT_NODE = 3,
        XML_CDATA_SECTION_NODE = 4,
        XML_ENTITY_REF_NODE = 5,
        XML_ENTITY_NODE = 6,
        XML_PI_NODE = 7,
        XML_COMMENT_NODE = 8,
        XML_DOCUMENT_NODE = 9,
        XML_DOCUMENT_TYPE_NODE = 10,
        XML_DOCUMENT_FRAG_NODE = 11,
        XML_NOTATION_NODE = 12,
        XML_HTML_DOCUMENT_NODE = 13,
        XML_DTD_NODE = 14,
        XML_ELEMENT_DECL = 15,
        XML_ATTRIBUTE_DECL = 16,
        XML_ENTITY_DECL = 17,
        XML_NAMESPACE_DECL = 18,
        XML_XINCLUDE_START = 19,
        XML_XINCLUDE_END = 20
    }

    public unsafe struct Entity
    {

    }

    public unsafe struct Enumeration
    {

    }

    public unsafe struct SAXLocator
    {

    }

    public unsafe struct Error
    {

    }


    public class ElementContent : IDisposable
    {
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }

    public unsafe class Ns : IDisposable
    {
        private bool disposed = false;

        public    Ns                        next ;
        public    ElementType               type ;
        public    string                    href ;
        public    string                    prefix ;
        public    void**                    _private ;
        public    Doc                       context ;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }

    public unsafe class Node : IDisposable
    {
        private bool disposed = false;

        public void** _private;
        public ElementType type;
        public string name;
        public NodePtr children;
        public NodePtr last;
        public NodePtr parent;
        public NodePtr next;
        public NodePtr prev;
        public DocPtr doc;
        public NsPtr ns;
        public string content;
        public AttrPtr properties;
        public NsPtr ns_def;
        public ushort line;
        public ushort extra;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }

    public unsafe class Doc : IDisposable
    {
        private bool disposed = false;

        public void** _private;
        public ElementType type;
        public string name;
        public NodePtr children;
        public NodePtr last;
        public NodePtr parent;
        public NodePtr next;
        public NodePtr prev;
        public DocPtr doc;
        public int compression;
        public int standalone;
        public DtdPtr int_subset;
        public DtdPtr ext_subset;
        public NsPtr old_ns;
        public string version;
        public string encoding;
        public string url;
        public int charset;


        public Doc(string version) { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

    }


    

   


    public static class LibXml
    {
        public const UInt32 XML_SAX2_MAGIC = 0xDEEDBEAF;


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void internalSubsetSAXFunc(IntPtr ctx, string name, string ExternalID, string SystemID);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int isStandaloneSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int hasInternalSubsetSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int hasExternalSubsetSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate Entity* resolveEntitySAXFunc(IntPtr ctx, string publicId, string systemId);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate Entity* getEntitySAXFunc(IntPtr ctx, string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void entityDeclSAXFunc(IntPtr ctx, string name, int type, string publicId, string systemId, string content);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void notationDeclSAXFunc(IntPtr ctx, string name, string publicId, string systemId);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void attributeDeclSAXFunc(IntPtr ctx, string elem, string fullname, int type, int def, string defaultValue, Enumeration* tree);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void elementDeclSAXFunc(IntPtr ctx, string name, int type, ElementContent content);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void unparsedEntityDeclSAXFunc(IntPtr ctx, string name, string publicId, string systemId, string notationName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void setDocumentLocatorSAXFunc(IntPtr ctx, SAXLocator* loc);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void startDocumentSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void endDocumentSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void startElementSAXFunc(IntPtr ctx, string name, string[] atts);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void endElementSAXFunc(IntPtr ctx, string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void referenceSAXFunc(IntPtr ctx, string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void charactersSAXFunc(IntPtr ctx, string ch, int len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void ignorableWhitespaceSAXFunc(IntPtr ctx, string ch, int len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void processingInstructionSAXFunc(IntPtr ctx, string target, string data);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void commentSAXFunc(IntPtr ctx, string value);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void warningSAXFunc(IntPtr ctx, string msg, params IntPtr[] prms);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void errorSAXFunc(IntPtr ctx, string msg, params IntPtr[] prms);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void fatalErrorSAXFunc(IntPtr ctx, string msg, params IntPtr[] prms);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate Entity* getParameterEntitySAXFunc(IntPtr ctx, string name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void cdataBlockSAXFunc(IntPtr ctx, string value, int len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void externalSubsetSAXFunc(IntPtr ctx, string name, string ExternalID, string SystemID);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void startElementNsSAX2Func(IntPtr ctx, string localname, string prefix, string URI,
        int nb_namespaces, string[] namespaces, int nb_attributes, int nb_defaulted, string[] attributes);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void endElementNsSAX2Func(IntPtr ctx, string localname, string prefix, string URI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void xmlStructuredErrorFunc(IntPtr ctx, Error* error);

        //int sizeOfV1 = sizeof(xmlSAXHandlerV1); => 112 bytes
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct xmlSAXHandlerV1
        {
            public IntPtr internalSubset;
            public IntPtr isStandalone;
            public IntPtr hasInternalSubset;
            public IntPtr hasExternalSubset;
            public IntPtr resolveEntity;
            public IntPtr getEntity;
            public IntPtr entityDecl;
            public IntPtr notationDecl;
            public IntPtr attributeDecl;
            public IntPtr elementDecl;
            public IntPtr unparsedEntityDecl;
            public IntPtr setDocumentLocator;
            public IntPtr startDocument;
            public IntPtr endDocument;
            public IntPtr startElement;
            public IntPtr endElement;
            public IntPtr reference;
            public IntPtr characters;
            public IntPtr ignorableWhitespace;
            public IntPtr processingInstruction;
            public IntPtr comment;
            public IntPtr warning;
            public IntPtr error;
            public IntPtr fatalError; /* unused error() get all the errors */
            public IntPtr getParameterEntity;
            public IntPtr cdataBlock;
            public IntPtr externalSubset;
            public UInt32 initialized;
        }

        //int sizeOfV2 = sizeof(xmlSAXHandler); => 128 bytes
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct xmlSAXHandler
        {
            public IntPtr /*internalSubsetSAXFunc*/ internalSubset;
            public IntPtr /*isStandaloneSAXFunc*/ isStandalone;
            public IntPtr /*hasInternalSubsetSAXFunc*/  hasInternalSubset;
            public IntPtr /*hasExternalSubsetSAXFunc*/ hasExternalSubset;
            public IntPtr /*resolveEntitySAXFunc*/ resolveEntity;
            public IntPtr /*getEntitySAXFunc*/ getEntity;
            public IntPtr /*entityDeclSAXFunc*/ entityDecl;
            public IntPtr /*notationDeclSAXFunc*/ notationDecl;
            public IntPtr /*attributeDeclSAXFunc*/ attributeDecl;
            public IntPtr /*elementDeclSAXFunc*/ elementDecl;
            public IntPtr /*unparsedEntityDeclSAXFunc*/ unparsedEntityDecl;
            public IntPtr /*setDocumentLocatorSAXFunc*/ setDocumentLocator;
            public IntPtr /*startDocumentSAXFunc*/ startDocument;
            public IntPtr /*endDocumentSAXFunc*/ endDocument;
            public IntPtr /*startElementSAXFunc*/ startElement;
            public IntPtr /*endElementSAXFunc*/ endElement;
            public IntPtr /*referenceSAXFunc*/ reference;
            public IntPtr /*charactersSAXFunc*/ characters;
            public IntPtr /*ignorableWhitespaceSAXFunc*/ ignorableWhitespace;
            public IntPtr /*processingInstructionSAXFunc*/ processingInstruction;
            public IntPtr /*commentSAXFunc*/ comment;
            public IntPtr /*warningSAXFunc*/ warning;
            public IntPtr /*errorSAXFunc*/ error;
            public IntPtr /*fatalErrorSAXFunc*/ fatalError; /* unused error() get all the errors */
            public IntPtr /*getParameterEntitySAXFunc*/ getParameterEntity;
            public IntPtr /*cdataBlockSAXFunc*/ cdataBlock;
            public IntPtr /*externalSubsetSAXFunc*/ externalSubset;
            public UInt32 initialized;
            /* The following fields are extensions available only on version 2 */
            public IntPtr /*void* */ _private;
            public IntPtr /*startElementNsSAX2Func*/ startElementNs;
            public IntPtr /*endElementNsSAX2Func*/ endElementNs;
            public IntPtr /*xmlStructuredErrorFunc*/ serror;
        }

       

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlInitParser();
        
        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlCleanupParser();

        [DllImport("libxml2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlSAXUserParseFile(xmlSAXHandlerPtr sax, IntPtr user_data, [MarshalAs(UnmanagedType.LPArray)] byte[] filename);
        
        [DllImport("libxml2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlSAXUserParseMemory(xmlSAXHandlerPtr sax, IntPtr user_data, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int size);
        
        [DllImport("libxml2", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlStrlen([MarshalAs(UnmanagedType.LPArray)] byte[] str);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlParseFile(String file);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlParseMemory(String file, int size);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern XmlElementType _xmlNodeGetElementType(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern String _xmlNodeGetName(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNodeGetNs(IntPtr node);

        ///* Navigation */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNodeGetPrevSibling(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNodeGetNextSibling(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNodeGetFirstChild(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNodeGetLastChild(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNodeGetDocument(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlElementGetFirstAttr(IntPtr node);

        ///* String Dumpers */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern String _xmlNodeDump(IntPtr node, int level, int format);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern String _xmlDocDump(IntPtr doc, int format);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern String _xmlNodeGetContent(IntPtr node);

        ///* Namespaces */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern String _xmlNsGetHref(IntPtr ns);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern String _xmlNsGetPrefix(IntPtr ns);

        ///* Attributes */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern String _xmlElementGetAttrValue(IntPtr node, String name);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlElementGetAttr(IntPtr node, String name);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern int _xmlElementSetAttrValue(IntPtr node, String name, String value);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlElementSetAttr(IntPtr node, String name, String value);

        ///* Cache nodes to prevent double freeing */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern bool _xmlNodeCache(IntPtr node, Object nodeObj);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern bool _xmlNodeUncache(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern bool _xmlNodeIsCached(IntPtr node);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern Object _xmlNodeGetCached(IntPtr node);

        ///* object construction */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNewDoc(String version);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNewNode(IntPtr ns, String name);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNewText(String content);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNewCDataBlock(String content, int len);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNewComment(String content);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlNewPI(String name, String content);

        ///* Adding to the tree */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr _xmlAddChild(IntPtr parent, IntPtr child);
        ///* adjusting the tree */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern void _xmlNodeNormalize(IntPtr node);

        ///* compression support ie my favourite */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern int _xmlDocGetCompression(IntPtr doc);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern int _xmlParserGetCompression();

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern void _xmlDocSetCompression(IntPtr doc, int level);

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern void _xmlParserSetCompression(int gzip);

        ///* file output */

        //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern void _xmlDocSaveToFile(IntPtr doc, String filename, int format);


    }
}
