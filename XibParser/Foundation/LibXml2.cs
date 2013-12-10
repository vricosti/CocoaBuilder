using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using xmlParserCtxtPtr = System.IntPtr;
using xmlSAXHandlerPtr = System.IntPtr;
using NodePtr = System.IntPtr;
using DocPtr = System.IntPtr;
using NsPtr = System.IntPtr;
using AttrPtr = System.IntPtr;
using DtdPtr = System.IntPtr;
using xmlEntityPtr = System.IntPtr;

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

    //public unsafe struct Entity
    //{

    //}

    //public unsafe struct Enumeration
    //{

    //}

    //public unsafe struct SAXLocator
    //{

    //}

    //public unsafe struct Error
    //{

    //}


    //public class ElementContent : IDisposable
    //{
    //    private bool disposed = false;

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //    }
    //}

    //public unsafe class Ns : IDisposable
    //{
    //    private bool disposed = false;

    //    public    Ns                        next ;
    //    public    ElementType               type ;
    //    public    string                    href ;
    //    public    string                    prefix ;
    //    public    void**                    _private ;
    //    public    Doc                       context ;

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //    }
    //}

    //public unsafe class Node : IDisposable
    //{
    //    private bool disposed = false;

    //    public void** _private;
    //    public ElementType type;
    //    public string name;
    //    public NodePtr children;
    //    public NodePtr last;
    //    public NodePtr parent;
    //    public NodePtr next;
    //    public NodePtr prev;
    //    public DocPtr doc;
    //    public NsPtr ns;
    //    public string content;
    //    public AttrPtr properties;
    //    public NsPtr ns_def;
    //    public ushort line;
    //    public ushort extra;

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //    }
    //}

    //public unsafe class Doc : IDisposable
    //{
    //    private bool disposed = false;

    //    public void** _private;
    //    public ElementType type;
    //    public string name;
    //    public NodePtr children;
    //    public NodePtr last;
    //    public NodePtr parent;
    //    public NodePtr next;
    //    public NodePtr prev;
    //    public DocPtr doc;
    //    public int compression;
    //    public int standalone;
    //    public DtdPtr int_subset;
    //    public DtdPtr ext_subset;
    //    public NsPtr old_ns;
    //    public string version;
    //    public string encoding;
    //    public string url;
    //    public int charset;


    //    public Doc(string version) { }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //    }

    //}


    

   


    public static class LibXml
    {
        public const UInt32 XML_SAX2_MAGIC = 0xDEEDBEAF;

        public enum XmlParserOption
        {
            XML_PARSE_RECOVER = 1, //: recover on errors
            XML_PARSE_NOENT = 2, //: substitute entities
            XML_PARSE_DTDLOAD = 4, //: load the external subset
            XML_PARSE_DTDATTR = 8, //: default DTD attributes
            XML_PARSE_DTDVALID = 16, //: validate with the DTD
            XML_PARSE_NOERROR = 32, //: suppress error reports
            XML_PARSE_NOWARNING = 64, //: suppress warning reports
            XML_PARSE_PEDANTIC = 128, //: pedantic error reporting
            XML_PARSE_NOBLANKS = 256, //: remove blank nodes
            XML_PARSE_SAX1 = 512, //: use the SAX1 interface internally
            XML_PARSE_XINCLUDE = 1024, //: Implement XInclude substitition
            XML_PARSE_NONET = 2048, //: Forbid network access
            XML_PARSE_NODICT = 4096, //: Do not reuse the context dictionnary
            XML_PARSE_NSCLEAN = 8192, //: remove redundant namespaces declarations
            XML_PARSE_NOCDATA = 16384, //: merge CDATA as text nodes
            XML_PARSE_NOXINCNODE = 32768, //: do not generate XINCLUDE START/END nodes
            XML_PARSE_COMPACT = 65536, //: compact small text nodes; no modification of the tree allowed afterwards (will possibly crash if you try to modify the tree)
            XML_PARSE_OLD10 = 131072, //: parse using XML-1.0 before update 5
            XML_PARSE_NOBASEFIX = 262144, //: do not fixup XINCLUDE xml:base uris
            XML_PARSE_HUGE = 524288, //: relax any hardcoded limit from the parser
            XML_PARSE_OLDSAX = 1048576, //: parse using SAX2 interface before 2.7.0
            XML_PARSE_IGNORE_ENC = 2097152, //: ignore internal document encoding hint
            XML_PARSE_BIG_LINES = 4194304, //: Store big lines numbers in text PSVI field
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void internalSubsetSAXFunc(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int isStandaloneSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int hasInternalSubsetSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate int hasExternalSubsetSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate IntPtr resolveEntitySAXFunc(IntPtr ctx, IntPtr publicId, IntPtr systemId);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate IntPtr getEntitySAXFunc(IntPtr ctx, IntPtr name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void entityDeclSAXFunc(IntPtr ctx, IntPtr name, int type, IntPtr publicId, IntPtr systemId, IntPtr content);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void notationDeclSAXFunc(IntPtr ctx, IntPtr name, IntPtr publicId, IntPtr systemId);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void attributeDeclSAXFunc(IntPtr ctx, IntPtr elem, IntPtr fullname, int type, int def, IntPtr defaultValue, IntPtr tree);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void elementDeclSAXFunc(IntPtr ctx, IntPtr name, int type, IntPtr content);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void unparsedEntityDeclSAXFunc(IntPtr ctx, IntPtr name, IntPtr publicId, IntPtr systemId, IntPtr notationName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void setDocumentLocatorSAXFunc(IntPtr ctx, IntPtr loc);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void startDocumentSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void endDocumentSAXFunc(IntPtr ctx);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void startElementSAXFunc(IntPtr ctx, IntPtr name, string[] atts);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void endElementSAXFunc(IntPtr ctx, IntPtr name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void referenceSAXFunc(IntPtr ctx, IntPtr name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void charactersSAXFunc(IntPtr ctx, IntPtr ch, int len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void ignorableWhitespaceSAXFunc(IntPtr ctx, IntPtr ch, int len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void processingInstructionSAXFunc(IntPtr ctx, IntPtr target, IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void commentSAXFunc(IntPtr ctx, IntPtr value);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void warningSAXFunc(IntPtr ctx, IntPtr msg, params IntPtr[] prms);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void errorSAXFunc(IntPtr ctx, IntPtr msg, params IntPtr[] prms);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void fatalErrorSAXFunc(IntPtr ctx, IntPtr msg, params IntPtr[] prms);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate IntPtr getParameterEntitySAXFunc(IntPtr ctx, IntPtr name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void cdataBlockSAXFunc(IntPtr ctx, IntPtr value, int len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void externalSubsetSAXFunc(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void startElementNsSAX2Func(IntPtr ctx, IntPtr localname, IntPtr prefix, IntPtr URI,
        int nb_namespaces, string[] namespaces, int nb_attributes, int nb_defaulted, string[] attributes);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void endElementNsSAX2Func(IntPtr ctx, IntPtr localname, IntPtr prefix, IntPtr URI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void xmlStructuredErrorFunc(IntPtr ctx, IntPtr error);



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

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlSAXUserParseFile(xmlSAXHandlerPtr sax, IntPtr user_data, [MarshalAs(UnmanagedType.LPArray)] byte[] filename);
        
        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlSAXUserParseMemory(xmlSAXHandlerPtr sax, IntPtr user_data, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int size);
        
        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlStrlen( IntPtr str);


        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlParseChunk(xmlParserCtxtPtr ctxt, [MarshalAs(UnmanagedType.LPArray)] byte[] chunk, int size, int terminate);


        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlSetStructuredErrorFunc(IntPtr ctx, IntPtr /*xmlStructuredErrorFunc*/ handler);


        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlParserCtxtPtr xmlCreatePushParserCtxt(xmlSAXHandlerPtr sax, IntPtr user_data,
            [MarshalAs(UnmanagedType.LPArray)] byte[] chunk, int size, [MarshalAs(UnmanagedType.LPArray)] byte[] filename);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlCtxtUseOptions(xmlParserCtxtPtr ctxt, int options);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlFreeEnumeration(IntPtr/*xmlEnumerationPtr*/ cur);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlEntityPtr xmlGetPredefinedEntity(IntPtr pName);



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //SAX2 - http://www.xmlsoft.org/html/libxml-SAX2.html
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlSAX2ExternalSubset(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlSAX2UnparsedEntityDecl(IntPtr ctx, IntPtr name, IntPtr pPublicId, IntPtr pSystemId, IntPtr notationName);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlSAX2EntityDecl(IntPtr ctx, IntPtr pName, int type, IntPtr pPublicId, IntPtr pSystemId, IntPtr pContent);
        
        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlEntityPtr xmlSAX2GetEntity(IntPtr ctx, IntPtr pName);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlSAX2InternalSubset(IntPtr ctx, IntPtr pName, IntPtr pExternalID, IntPtr pSystemID);



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
