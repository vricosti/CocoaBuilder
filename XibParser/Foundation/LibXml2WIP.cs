using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using xmlCharPtr = System.IntPtr;

//using xmlParserInputPtr = System.IntPtr;
//using xmlParserCtxtPtr = System.IntPtr;
//using xmlSAXHandlerPtr = System.IntPtr;
//using NodePtr = Node*;
//using DocPtr = System.IntPtr;
//using NsPtr = System.IntPtr;
//using AttrPtr = System.IntPtr;
//using DtdPtr = System.IntPtr;
//using xmlEntityPtr = System.IntPtr;
//using xmlExternalEntityLoaderPtr = System.IntPtr;
//using xmlTextReaderPtr = System.IntPtr;
//using xmlTextReaderLocatorPtr = System.IntPtr;
//using xmlTextReaderErrorFunc = System.IntPtr;
//using xmlNodePtr = System.IntPtr;
//using xmlDocPtr = System.IntPtr;
//using xmlErrorPtr = System.IntPtr;

namespace Smartmobili.Cocoa
{
#pragma warning disable 169

    //public enum xmlParserSeverities
    //{
    //    XML_PARSER_SEVERITY_VALIDITY_WARNING = 1,
    //    XML_PARSER_SEVERITY_VALIDITY_ERROR = 2,
    //    XML_PARSER_SEVERITY_WARNING = 3,
    //    XML_PARSER_SEVERITY_ERROR = 4,
    //}
    //public enum ElementType 
    //{
    //    ELEMENT_NODE,
    //    ATTRIBUTE_NODE,
    //    TEXT_NODE,
    //    CDATA_SECTION_NODE,
    //    ENTITY_REF_NODE,
    //    ENTITY_NODE,
    //    PI_NODE,
    //    COMMENT_NODE,
    //    DOCUMENT_NODE,
    //    DOCUMENT_TYPE_NODE,
    //    DOCUMENT_FRAG_NODE,
    //    NOTATION_NODE,
    //    HTML_DOCUMENT_NODE,
    //    DTD_NODE,
    //    ELEMENT_DECL,
    //    ATTRIBUTE_DECL,
    //    ENTITY_DECL,
    //    NAMESPACE_DECL,
    //    XINCLUDE_START,
    //    XINCLUDE_END,
    //    DOCB_DOCUMENT_NODE,
    //}

    public enum xmlElementType
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
        XML_XINCLUDE_END = 20,
//#ifdef LIBXML_DOCB_ENABLED
        XML_DOCB_DOCUMENT_NODE=	21
//#endif
    }

    public enum xmlAttributeType 
    {
        XML_ATTRIBUTE_CDATA = 1,
        XML_ATTRIBUTE_ID,
        XML_ATTRIBUTE_IDREF	,
        XML_ATTRIBUTE_IDREFS,
        XML_ATTRIBUTE_ENTITY,
        XML_ATTRIBUTE_ENTITIES,
        XML_ATTRIBUTE_NMTOKEN,
        XML_ATTRIBUTE_NMTOKENS,
        XML_ATTRIBUTE_ENUMERATION,
        XML_ATTRIBUTE_NOTATION
    }


    /* entities.h */
    public enum xmlEntityType
    {
        XML_INTERNAL_GENERAL_ENTITY = 1,
        XML_EXTERNAL_GENERAL_PARSED_ENTITY = 2,
        XML_EXTERNAL_GENERAL_UNPARSED_ENTITY = 3,
        XML_INTERNAL_PARAMETER_ENTITY = 4,
        XML_EXTERNAL_PARAMETER_ENTITY = 5,
        XML_INTERNAL_PREDEFINED_ENTITY = 6
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct xmlEntity
    {
        void* _private;	            /* application data */
        xmlElementType type;        /* XML_ENTITY_DECL, must be second ! */
        xmlCharPtr name;	        /* Entity name */
        xmlNode* children;	        /* First child link */
        xmlNode* last;	            /* Last child link */
        xmlDtd* parent;	            /* -> DTD */
        xmlNode* next;	            /* next sibling link  */
        xmlNode* prev;	            /* previous sibling link  */
        xmlDoc* doc;                /* the containing document */

        xmlCharPtr orig;	        /* content without ref substitution */
        xmlCharPtr content;     	/* content or ndata if unparsed */
        int length;	                /* the content length */
        xmlEntityType etype;	    /* The entity type */
        xmlCharPtr ExternalID;	    /* External identifier for PUBLIC */
        xmlCharPtr SystemID;	    /* URI for a SYSTEM or PUBLIC Entity */

        xmlEntity* nexte;	        /* unused */
        xmlCharPtr URI;	            /* the full URI as computed */
        int owner;	                /* does the entity own the childrens */
        int _checked;	            /* was the entity content checked */
                                    /* this is also used to count entites
                                    * references done from that entity */
    }


    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct xmlNs
    {
        xmlNs* next;	        /* next Ns link for this node  */
        xmlElementType type;	/* global or local */
        xmlCharPtr href;	    /* URL for the namespace */
        xmlCharPtr prefix;	    /* prefix for the namespace */
        void* _private;         /* application data */
        xmlDoc* context;		/* normally an xmlDoc */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
   public unsafe struct xmlAttr
   {
       void             *_private;	/* application data */
       xmlElementType   type;       /* XML_ATTRIBUTE_NODE, must be second ! */
       xmlCharPtr       name;       /* the name of the property */
       xmlNode*         children;	/* the value of the property */
       xmlNode*         last;	    /* NULL */
       xmlNode*         parent;	    /* child->parent link */
       xmlAttr*         next;	    /* next sibling link  */
       xmlAttr*         prev;	    /* previous sibling link  */
       xmlDoc*          doc;	    /* the containing document */
       xmlNs*           ns;         /* pointer to the associated namespace */
       xmlAttributeType atype;      /* the attribute type if validating */
       void*            psvi;	    /* for type/PSVI informations */
   }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct xmlNode
    {
        public void* _private;
        public ElementType type;
        public xmlCharPtr name;
        public xmlNode* children;
        public xmlNode* last;
        public xmlNode* parent;
        public xmlNode* next;
        public xmlNode* prev;
        public xmlDoc* doc;
        
        /* End of common part */
        public xmlNs* ns;
        public xmlCharPtr content;
        public xmlAttr* properties;
        public xmlNs* nsDef;
        public void* psvi;
        public uint line;
        public uint extra;
    }

    

    public unsafe struct xmlDtd
    {

    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct xmlDoc
    {
        void*           _private;	/* application data */
        xmlElementType  type;       /* XML_DOCUMENT_NODE, must be second ! */
        IntPtr          name;	/* name/filename/URI of the document */
        xmlNode*        children;	/* the document tree */
        xmlNode*        last;	/* last child link */
        xmlNode*        parent;	/* child->parent link */
        xmlNode*        next;	/* next sibling link  */
        xmlNode*        prev;	/* previous sibling link  */
        xmlDoc*         doc;	/* autoreference to itself */

        /* End of common part */
        int             compression;/* level of zlib compression */
        int             standalone; /* standalone document (no external refs) 
				                1 if standalone="yes"
				                0 if standalone="no"
				                -1 if there is no XML declaration
				                -2 if there is an XML declaration, but no
					            standalone attribute was specified */
        xmlDtd*         intSubset;	/* the document internal subset */
        xmlDtd*         extSubset;	/* the document external subset */
        xmlNs*          oldNs;	/* Global namespace, the old way */
        xmlCharPtr      version;	/* the XML version string */
        xmlCharPtr      encoding;   /* external initial encoding, if any */
        void*           ids;        /* Hash table for ID attributes if any */
        void*           refs;       /* Hash table for IDREFs attributes if any */
        xmlCharPtr      URL;	/* The URI for that document */
        int             charset;    /* encoding of the in-memory content
				            actually an xmlCharEncoding */
        void*           dict;      /* dict used to allocate names or NULL */
        void*           psvi;	/* for type/PSVI informations */
        int             parseFlags;	/* set of xmlParserOption used to parse the
				            document */
        int             properties;	/* set of xmlDocProperties for this document
				            set at the end of parsing */
    }

    


    

   


    //public static class LibXmlWIP
    //{
    //    public const UInt32 XML_SAX2_MAGIC = 0xDEEDBEAF;

    //    //xmlParserInputState
    //    public const Int32 XML_PARSER_CONTENT = 7;
    //    //xmlEntityType
    //    public const Int32 XML_INTERNAL_PREDEFINED_ENTITY = 6;



    //    public enum XmlParserOption : uint
    //    {
    //        XML_PARSE_RECOVER = 1, //: recover on errors
    //        XML_PARSE_NOENT = 2, //: substitute entities
    //        XML_PARSE_DTDLOAD = 4, //: load the external subset
    //        XML_PARSE_DTDATTR = 8, //: default DTD attributes
    //        XML_PARSE_DTDVALID = 16, //: validate with the DTD
    //        XML_PARSE_NOERROR = 32, //: suppress error reports
    //        XML_PARSE_NOWARNING = 64, //: suppress warning reports
    //        XML_PARSE_PEDANTIC = 128, //: pedantic error reporting
    //        XML_PARSE_NOBLANKS = 256, //: remove blank nodes
    //        XML_PARSE_SAX1 = 512, //: use the SAX1 interface internally
    //        XML_PARSE_XINCLUDE = 1024, //: Implement XInclude substitition
    //        XML_PARSE_NONET = 2048, //: Forbid network access
    //        XML_PARSE_NODICT = 4096, //: Do not reuse the context dictionnary
    //        XML_PARSE_NSCLEAN = 8192, //: remove redundant namespaces declarations
    //        XML_PARSE_NOCDATA = 16384, //: merge CDATA as text nodes
    //        XML_PARSE_NOXINCNODE = 32768, //: do not generate XINCLUDE START/END nodes
    //        XML_PARSE_COMPACT = 65536, //: compact small text nodes; no modification of the tree allowed afterwards (will possibly crash if you try to modify the tree)
    //        XML_PARSE_OLD10 = 131072, //: parse using XML-1.0 before update 5
    //        XML_PARSE_NOBASEFIX = 262144, //: do not fixup XINCLUDE xml:base uris
    //        XML_PARSE_HUGE = 524288, //: relax any hardcoded limit from the parser
    //        XML_PARSE_OLDSAX = 1048576, //: parse using SAX2 interface before 2.7.0
    //        XML_PARSE_IGNORE_ENC = 2097152, //: ignore internal document encoding hint
    //        XML_PARSE_BIG_LINES = 4194304, //: Store big lines numbers in text PSVI field
    //    }

    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void internalSubsetSAXFunc(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate int isStandaloneSAXFunc(IntPtr ctx);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate int hasInternalSubsetSAXFunc(IntPtr ctx);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate int hasExternalSubsetSAXFunc(IntPtr ctx);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate IntPtr resolveEntitySAXFunc(IntPtr ctx, IntPtr publicId, IntPtr systemId);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate xmlEntityPtr getEntitySAXFunc(IntPtr ctx, IntPtr name);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void entityDeclSAXFunc(IntPtr ctx, IntPtr name, int type, IntPtr publicId, IntPtr systemId, IntPtr content);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void notationDeclSAXFunc(IntPtr ctx, IntPtr name, IntPtr publicId, IntPtr systemId);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void attributeDeclSAXFunc(IntPtr ctx, IntPtr elem, IntPtr fullname, int type, int def, IntPtr defaultValue, IntPtr tree);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void elementDeclSAXFunc(IntPtr ctx, IntPtr name, int type, IntPtr content);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void unparsedEntityDeclSAXFunc(IntPtr ctx, IntPtr name, IntPtr publicId, IntPtr systemId, IntPtr notationName);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void setDocumentLocatorSAXFunc(IntPtr ctx, IntPtr loc);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void startDocumentSAXFunc(IntPtr ctx);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void endDocumentSAXFunc(IntPtr ctx);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void startElementSAXFunc(IntPtr ctx, IntPtr name, string[] atts);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void endElementSAXFunc(IntPtr ctx, IntPtr name);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void referenceSAXFunc(IntPtr ctx, IntPtr name);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void charactersSAXFunc(IntPtr ctx, IntPtr ch, int len);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void ignorableWhitespaceSAXFunc(IntPtr ctx, IntPtr ch, int len);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void processingInstructionSAXFunc(IntPtr ctx, IntPtr target, IntPtr data);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void commentSAXFunc(IntPtr ctx, IntPtr value);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void warningSAXFunc(IntPtr ctx, IntPtr msg, params IntPtr[] prms);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void errorSAXFunc(IntPtr ctx, IntPtr msg, params IntPtr[] prms);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void fatalErrorSAXFunc(IntPtr ctx, IntPtr msg, params IntPtr[] prms);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate IntPtr getParameterEntitySAXFunc(IntPtr ctx, IntPtr name);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void cdataBlockSAXFunc(IntPtr ctx, IntPtr value, int len);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void externalSubsetSAXFunc(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void startElementNsSAX2Func(IntPtr ctx, IntPtr localname, IntPtr prefix, IntPtr URI,
    //    int nb_namespaces, IntPtr namespaces, int nb_attributes, int nb_defaulted, IntPtr attributes);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void endElementNsSAX2Func(IntPtr ctx, IntPtr localname, IntPtr prefix, IntPtr URI);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void xmlStructuredErrorFunc(IntPtr ctx, IntPtr error);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate xmlParserInputPtr xmlExternalEntityLoader(IntPtr URL, IntPtr ID, xmlParserCtxtPtr context);
    //    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //    public unsafe delegate void xmlTextReaderErrorFunc(IntPtr userData, IntPtr pMsg, int severity, xmlTextReaderLocatorPtr locator);


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

       

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlInitParser();
        
    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlCleanupParser();

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern int xmlSAXUserParseFile(xmlSAXHandlerPtr sax, IntPtr user_data, [MarshalAs(UnmanagedType.LPArray)] byte[] filename);
        
    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern int xmlSAXUserParseMemory(xmlSAXHandlerPtr sax, IntPtr user_data, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int size);
        
    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern int xmlStrlen( IntPtr str);


    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern int xmlParseChunk(xmlParserCtxtPtr ctxt, [MarshalAs(UnmanagedType.LPArray)] byte[] chunk, int size, int terminate);


    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlSetStructuredErrorFunc(IntPtr ctx, IntPtr /*xmlStructuredErrorFunc*/ handler);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlErrorPtr xmlCtxtGetLastError(IntPtr ctx);



    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlParserCtxtPtr xmlCreatePushParserCtxt(xmlSAXHandlerPtr sax, IntPtr user_data,
    //        [MarshalAs(UnmanagedType.LPArray)] byte[] chunk, int size, [MarshalAs(UnmanagedType.LPArray)] byte[] filename);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern int xmlCtxtUseOptions(xmlParserCtxtPtr ctxt, int options);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlFreeEnumeration(IntPtr/*xmlEnumerationPtr*/ cur);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlEntityPtr xmlGetPredefinedEntity(IntPtr pName);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlExternalEntityLoaderPtr xmlGetExternalEntityLoader();

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlSetExternalEntityLoader(xmlExternalEntityLoaderPtr f);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlTextReaderPtr xmlReaderForMemory(byte[] buffer, int size, IntPtr URL, IntPtr encoding, int options);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern int xmlTextReaderSetParserProp(xmlTextReaderPtr reader, int prop, int value);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlTextReaderSetErrorHandler(xmlTextReaderPtr reader, xmlTextReaderErrorFunc f, IntPtr pUserData);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern int xmlTextReaderLocatorLineNumber(xmlTextReaderLocatorPtr locator);
        
    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlDocPtr xmlTextReaderCurrentDoc(xmlTextReaderPtr reader);
        



    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlNodePtr xmlAddChild(xmlNodePtr parent, xmlNodePtr cur);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlNodePtr xmlNewCharRef(xmlDocPtr doc, IntPtr name);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlNodePtr xmlNewReference(xmlDocPtr doc, IntPtr name);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern IntPtr xmlStrsub(IntPtr str, int start, int len);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlFree(IntPtr ptr);


    //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //    //SAX2 - http://www.xmlsoft.org/html/libxml-SAX2.html
    //    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlSAX2ExternalSubset(IntPtr ctx, IntPtr name, IntPtr ExternalID, IntPtr SystemID);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlSAX2UnparsedEntityDecl(IntPtr ctx, IntPtr name, IntPtr pPublicId, IntPtr pSystemId, IntPtr notationName);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlSAX2EntityDecl(IntPtr ctx, IntPtr pName, int type, IntPtr pPublicId, IntPtr pSystemId, IntPtr pContent);
        
    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern xmlEntityPtr xmlSAX2GetEntity(IntPtr ctx, IntPtr pName);

    //    [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    //    internal static extern void xmlSAX2InternalSubset(IntPtr ctx, IntPtr pName, IntPtr pExternalID, IntPtr pSystemID);



    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlParseFile(String file);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlParseMemory(String file, int size);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern XmlElementType _xmlNodeGetElementType(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern String _xmlNodeGetName(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNodeGetNs(IntPtr node);

    //    ///* Navigation */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNodeGetPrevSibling(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNodeGetNextSibling(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNodeGetFirstChild(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNodeGetLastChild(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNodeGetDocument(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlElementGetFirstAttr(IntPtr node);

    //    ///* String Dumpers */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern String _xmlNodeDump(IntPtr node, int level, int format);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern String _xmlDocDump(IntPtr doc, int format);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern String _xmlNodeGetContent(IntPtr node);

    //    ///* Namespaces */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern String _xmlNsGetHref(IntPtr ns);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern String _xmlNsGetPrefix(IntPtr ns);

    //    ///* Attributes */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern String _xmlElementGetAttrValue(IntPtr node, String name);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlElementGetAttr(IntPtr node, String name);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern int _xmlElementSetAttrValue(IntPtr node, String name, String value);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlElementSetAttr(IntPtr node, String name, String value);

    //    ///* Cache nodes to prevent double freeing */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern bool _xmlNodeCache(IntPtr node, Object nodeObj);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern bool _xmlNodeUncache(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern bool _xmlNodeIsCached(IntPtr node);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern Object _xmlNodeGetCached(IntPtr node);

    //    ///* object construction */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNewDoc(String version);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNewNode(IntPtr ns, String name);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNewText(String content);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNewCDataBlock(String content, int len);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNewComment(String content);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlNewPI(String name, String content);

    //    ///* Adding to the tree */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern IntPtr _xmlAddChild(IntPtr parent, IntPtr child);
    //    ///* adjusting the tree */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern void _xmlNodeNormalize(IntPtr node);

    //    ///* compression support ie my favourite */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern int _xmlDocGetCompression(IntPtr doc);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern int _xmlParserGetCompression();

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern void _xmlDocSetCompression(IntPtr doc, int level);

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern void _xmlParserSetCompression(int gzip);

    //    ///* file output */

    //    //[DllImport("libxml_wrapper", CallingConvention = CallingConvention.Cdecl)]
    //    //internal static extern void _xmlDocSaveToFile(IntPtr doc, String filename, int format);


    //}
#pragma warning restore 169
}
