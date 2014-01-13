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

namespace Smartmobili.LibXml
{
#pragma warning disable 169

    //public enum xmlParserSeverities
    //{
    //    XML_PARSER_SEVERITY_VALIDITY_WARNING = 1,
    //    XML_PARSER_SEVERITY_VALIDITY_ERROR = 2,
    //    XML_PARSER_SEVERITY_WARNING = 3,
    //    XML_PARSER_SEVERITY_ERROR = 4,
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
        XML_DOCB_DOCUMENT_NODE = 21
        //#endif
    }

    public enum xmlAttributeType
    {
        XML_ATTRIBUTE_CDATA = 1,
        XML_ATTRIBUTE_ID,
        XML_ATTRIBUTE_IDREF,
        XML_ATTRIBUTE_IDREFS,
        XML_ATTRIBUTE_ENTITY,
        XML_ATTRIBUTE_ENTITIES,
        XML_ATTRIBUTE_NMTOKEN,
        XML_ATTRIBUTE_NMTOKENS,
        XML_ATTRIBUTE_ENUMERATION,
        XML_ATTRIBUTE_NOTATION
    }


    public enum xmlParserInputState
    {
        XML_PARSER_EOF = -1,	/* nothing is to be parsed */
        XML_PARSER_START = 0,	/* nothing has been parsed */
        XML_PARSER_MISC,		/* Misc* before int subset */
        XML_PARSER_PI,		/* Within a processing instruction */
        XML_PARSER_DTD,		/* within some DTD content */
        XML_PARSER_PROLOG,		/* Misc* after internal subset */
        XML_PARSER_COMMENT,		/* within a comment */
        XML_PARSER_START_TAG,	/* within a start tag */
        XML_PARSER_CONTENT,		/* within the content */
        XML_PARSER_CDATA_SECTION,	/* within a CDATA section */
        XML_PARSER_END_TAG,		/* within a closing tag */
        XML_PARSER_ENTITY_DECL,	/* within an entity declaration */
        XML_PARSER_ENTITY_VALUE,	/* within an entity value in a decl */
        XML_PARSER_ATTRIBUTE_VALUE,	/* within an attribute value */
        XML_PARSER_SYSTEM_LITERAL,	/* within a SYSTEM value */
        XML_PARSER_EPILOG, 		/* the Misc* after the last end tag */
        XML_PARSER_IGNORE,		/* within an IGNORED section */
        XML_PARSER_PUBLIC_LITERAL 	/* within a PUBLIC value */
    }



     [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct xmlDict
    {
         IntPtr dummy;
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
        void* _private;	            /* application data */
        xmlElementType type;        /* XML_ATTRIBUTE_NODE, must be second ! */
        xmlCharPtr name;            /* the name of the property */
        xmlNode* children;	        /* the value of the property */
        xmlNode* last;	            /* NULL */
        xmlNode* parent;	        /* child->parent link */
        xmlAttr* next;	            /* next sibling link  */
        xmlAttr* prev;	            /* previous sibling link  */
        xmlDoc* doc;	            /* the containing document */
        xmlNs* ns;                  /* pointer to the associated namespace */
        xmlAttributeType atype;     /* the attribute type if validating */
        void* psvi;	                /* for type/PSVI informations */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct xmlNode
    {
        public void* _private;
        public xmlElementType type;
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


    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct xmlDtd
    {
        void* _private;	            /* application data */
        xmlElementType type;        /* XML_DTD_NODE, must be second ! */
        xmlCharPtr name;	        /* Name of the DTD */
        xmlNode* children;	        /* the value of the property link */
        xmlNode* last;	            /* last child link */
        xmlDoc* parent;	            /* child->parent link */
        xmlNode* next;	            /* next sibling link  */
        xmlNode* prev;	            /* previous sibling link  */
        xmlDoc* doc;	            /* the containing document */

        /* End of common part */
        void* notations;            /* Hash table for notations if any */
        void* elements;             /* Hash table for elements if any */
        void* attributes;           /* Hash table for attributes if any */
        void* entities;             /* Hash table for entities if any */
        xmlCharPtr ExternalID;	    /* External identifier for PUBLIC DTD */
        xmlCharPtr SystemID;	    /* URI for a SYSTEM or PUBLIC DTD */
        void* pentities;            /* Hash table for param entities if any */
    }

    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct xmlDoc
    {
        void* _private;	/* application data */
        xmlElementType type;       /* XML_DOCUMENT_NODE, must be second ! */
        IntPtr name;	/* name/filename/URI of the document */
        xmlNode* children;	/* the document tree */
        xmlNode* last;	/* last child link */
        xmlNode* parent;	/* child->parent link */
        xmlNode* next;	/* next sibling link  */
        xmlNode* prev;	/* previous sibling link  */
        xmlDoc* doc;	/* autoreference to itself */

        /* End of common part */
        int compression;/* level of zlib compression */
        int standalone; /* standalone document (no external refs) 
				                1 if standalone="yes"
				                0 if standalone="no"
				                -1 if there is no XML declaration
				                -2 if there is an XML declaration, but no
					            standalone attribute was specified */
        xmlDtd* intSubset;	/* the document internal subset */
        xmlDtd* extSubset;	/* the document external subset */
        xmlNs* oldNs;	/* Global namespace, the old way */
        xmlCharPtr version;	/* the XML version string */
        xmlCharPtr encoding;   /* external initial encoding, if any */
        void* ids;        /* Hash table for ID attributes if any */
        void* refs;       /* Hash table for IDREFs attributes if any */
        xmlCharPtr URL;	/* The URI for that document */
        int charset;    /* encoding of the in-memory content
				            actually an xmlCharEncoding */
        void* dict;      /* dict used to allocate names or NULL */
        void* psvi;	/* for type/PSVI informations */
        int parseFlags;	/* set of xmlParserOption used to parse the
				            document */
        int properties;	/* set of xmlDocProperties for this document
				            set at the end of parsing */
    }

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

    public unsafe struct xmlParserInputBuffer
    {}

    public unsafe struct xmlParserInput
    {
        /* Input buffer */
        xmlParserInputBuffer* buf;      /* UTF-8 encoded buffer */

        IntPtr filename;             /* The file analyzed, if any */
        IntPtr directory;            /* the directory/base of the file */
        xmlCharPtr _base;              /* Base of the array to parse */
        xmlCharPtr cur;               /* Current char being parsed */
        xmlCharPtr end;               /* end of the array to parse */
        int length;                       /* length if known */
        int line;                         /* Current line */
        int col;                          /* Current column */
        /*
         * NOTE: consumed is only tested for equality in the parser code,
         *       so even if there is an overflow this should not give troubles
         *       for parsing very large instances.
         */
        uint consumed;           /* How many xmlChars already consumed */
        IntPtr free;    /* function to deallocate the base */
        xmlCharPtr encoding;          /* the encoding string for entity */
        xmlCharPtr version;           /* the version string for entity */
        int standalone;                   /* Was that entity marked standalone */
        int id;                           /* an unique identifier for the entity */
    }


    public unsafe struct xmlParserNodeInfo
    {
        xmlNode* node;
        /* Position & line # that text that created the node begins & ends on */
        uint begin_pos;
        uint begin_line;
        uint end_pos;
        uint end_line;
    }


    public unsafe struct xmlHashTable
    {}

    public unsafe struct xmlParserNodeInfoSeq
    {
        uint maximum;
        uint length;
        xmlParserNodeInfo* buffer;
    }

    public unsafe struct xmlValidCtxt
    {}

    public enum xmlErrorLevel
    {
        XML_ERR_NONE = 0,
        XML_ERR_WARNING = 1,	/* A simple warning */
        XML_ERR_ERROR = 2,		/* A recoverable error */
        XML_ERR_FATAL = 3		/* A fatal error */
    }

    public unsafe struct xmlError
    {
        int domain;	        /* What part of the library raised this error */
        int code;	        /* The error code, e.g. an xmlParserError */
        IntPtr message;     /* human-readable informative error message */
        xmlErrorLevel level;/* how consequent is the error */
        IntPtr file;	    /* the filename */
        int line;	        /* the line number if available */
        IntPtr str1;	    /* extra string information */
        IntPtr str2;	    /* extra string information */
        IntPtr str3;	    /* extra string information */
        int int1;	        /* extra number information */
        int int2;	        /* column number of the error or 0 if N/A (todo: rename this field when we would break ABI) */
        void* ctxt;         /* the parser context if available */
        void* node;         /* the node in the tree */
    }

    public enum xmlParserMode
    {
        XML_PARSE_UNKNOWN = 0,
        XML_PARSE_DOM = 1,
        XML_PARSE_SAX = 2,
        XML_PARSE_PUSH_DOM = 3,
        XML_PARSE_PUSH_SAX = 4,
        XML_PARSE_READER = 5
    }

    public unsafe struct xmlParserCtxt
    {
        xmlSAXHandler* sax;         /* The SAX handler */
        void* userData;             /* For SAX interface only, used by DOM build */
        xmlDoc* myDoc;              /* the document being built */
        int wellFormed;             /* is the document well formed */
        int replaceEntities;        /* shall we replace entities ? */
        xmlCharPtr version;         /* the XML version string */
        xmlCharPtr encoding;        /* the declared encoding, if any */
        int standalone;             /* standalone document */
        int html;                   /* an HTML(1)/Docbook(2) document
                                       * 3 is HTML after <head>
                                       * 10 is HTML after <body>
                                       */

        /* Input stream stack */
        xmlParserInput* input;          /* Current input stream */
        int inputNr;                    /* Number of current input streams */
        int inputMax;                   /* Max number of input streams */
        xmlParserInput** inputTab;      /* stack of inputs */

        /* Node analysis stack only used for DOM building */
        xmlNode* node;                  /* Current parsed Node */
        int nodeNr;                     /* Depth of the parsing stack */
        int nodeMax;                    /* Max depth of the parsing stack */
        xmlNode* nodeTab;               /* array of nodes */

        int record_info;                /* Whether node info should be kept */
        xmlParserNodeInfoSeq node_seq;  /* info about each node parsed */

        int errNo;                      /* error code */

        int hasExternalSubset;          /* reference and external subset */
        int hasPErefs;                  /* the internal subset has PE refs */
        int external;                   /* are we parsing an external entity */

        int valid;                      /* is the document valid */
        int validate;                   /* shall we try to validate ? */
        xmlValidCtxt vctxt;             /* The validity context */

        xmlParserInputState instate;    /* current type of input */
        int token;                      /* next char look-ahead */

        char* directory;                /* the data directory */

        /* Node name stack */
        xmlCharPtr name;                /* Current parsed Node */
        int nameNr;                     /* Depth of the parsing stack */
        int nameMax;                    /* Max depth of the parsing stack */
        xmlCharPtr* nameTab;            /* array of nodes */

        long nbChars;                   /* number of xmlChar processed */
        long checkIndex;                /* used by progressive parsing lookup */
        int keepBlanks;                 /* ugly but ... */
        int disableSAX;                 /* SAX callbacks are disabled */
        int inSubset;                   /* Parsing is in int 1/ext 2 subset */
        xmlCharPtr intSubName;          /* name of subset */
        xmlCharPtr extSubURI;           /* URI of external subset */
        xmlCharPtr extSubSystem;        /* SYSTEM ID of external subset */

        /* xml:space values */
        int* space;                     /* Should the parser preserve spaces */
        int spaceNr;                    /* Depth of the parsing stack */
        int spaceMax;                   /* Max depth of the parsing stack */
        int* spaceTab;                  /* array of space infos */

        int depth;                      /* to prevent entity substitution loops */
        xmlParserInput* entity;         /* used to check entities boundaries */
        int charset;                    /* encoding of the in-memory content
				                        actually an xmlCharEncoding */
        int nodelen;                    /* Those two fields are there to */
        int nodemem;                    /* Speed up large node parsing */
        int pedantic;                   /* signal pedantic warnings */
        void* _private;                 /* For user data, libxml won't touch it */

        int loadsubset;                 /* should the external subset be loaded */
        int linenumbers;                /* set line number in element content */
        void* catalogs;                 /* document's own catalog */
        int recovery;                   /* run in recovery mode */
        int progressive;                /* is this a progressive parsing */
        xmlDict* dict;                  /* dictionnary for the parser */
        xmlCharPtr* atts;               /* array for the attributes callbacks */
        int maxatts;                    /* the size of the array */
        int docdict;                    /* use strings from dict to build tree */

        /*
         * pre-interned strings
         */
        xmlCharPtr str_xml;
        xmlCharPtr str_xmlns;
        xmlCharPtr str_xml_ns;

        /*
         * Everything below is used only by the new SAX mode
         */
        int sax2;                       /* operating in the new SAX mode */
        int nsNr;                       /* the number of inherited namespaces */
        int nsMax;                      /* the size of the arrays */
        xmlCharPtr* nsTab;              /* the array of prefix/namespace name */
        int* attallocs;                 /* which attribute were allocated */
        void** pushTab;                 /* array of data for push */
        xmlHashTable* attsDefault;      /* defaulted attributes if any */
        xmlHashTable* attsSpecial;      /* non-CDATA attributes if any */
        int nsWellFormed;               /* is the document XML Nanespace okay */
        int options;                    /* Extra options */

        /*
         * Those fields are needed only for treaming parsing so far
         */
        int dictNames;                  /* Use dictionary names for the tree */
        int freeElemsNr;                /* number of freed element nodes */
        xmlNode* freeElems;             /* List of freed element nodes */
        int freeAttrsNr;                /* number of freed attributes nodes */
        xmlAttr* freeAttrs;             /* List of freed attributes nodes */

        /*
         * the complete error informations for the last error.
         */
        xmlError lastError;
        xmlParserMode parseMode;        /* the parser mode */
        uint nbentities;                /* number of entities references */
        uint sizeentities;              /* size of parsed entities */

        /* for use by HTML non-recursive parser */
        xmlParserNodeInfo* nodeInfo;    /* Current NodeInfo */
        int nodeInfoNr;                 /* Depth of the parsing stack */
        int nodeInfoMax;                /* Max depth of the parsing stack */
        xmlParserNodeInfo* nodeInfoTab; /* array of nodeInfos */
    }


    public enum xmlBufferAllocationScheme
    {
        XML_BUFFER_ALLOC_DOUBLEIT,	/* double each time one need to grow */
        XML_BUFFER_ALLOC_EXACT,	    /* grow only to the minimal size */
        XML_BUFFER_ALLOC_IMMUTABLE, /* immutable buffer */
        XML_BUFFER_ALLOC_IO		    /* special allocation scheme used for I/O */
    }

    public unsafe struct xmlBuffer
    {
        IntPtr content;		    /* The buffer content UTF8 */
        uint use;		        /* The buffer size used */
        uint size;		        /* The buffer size */
        xmlBufferAllocationScheme alloc; /* The realloc method */
        xmlCharPtr contentIO;		    /* in IO mode we may have a different base */
    }

    public unsafe struct xmlRelaxNG {}
    public unsafe struct xmlRelaxNGValidCtxt{}
    public unsafe struct xmlSchema { }
    public unsafe struct xmlSchemaValidCtxt { }
    public unsafe struct xmlSchemaSAXPlug { }
    public unsafe struct xmlXIncludeCtxt { }
    public unsafe struct xmlPattern { }
    

    public enum xmlTextReaderState
    {
        XML_TEXTREADER_NONE = -1,
        XML_TEXTREADER_START = 0,
        XML_TEXTREADER_ELEMENT = 1,
        XML_TEXTREADER_END = 2,
        XML_TEXTREADER_EMPTY = 3,
        XML_TEXTREADER_BACKTRACK = 4,
        XML_TEXTREADER_DONE = 5,
        XML_TEXTREADER_ERROR = 6
    }

    public enum xmlTextReaderValidate
    {
        XML_TEXTREADER_NOT_VALIDATE = 0,
        XML_TEXTREADER_VALIDATE_DTD = 1,
        XML_TEXTREADER_VALIDATE_RNG = 2,
        XML_TEXTREADER_VALIDATE_XSD = 4
    }


    unsafe struct xmlTextReader
    {
        int mode;	/* the parsing mode */
        xmlDoc* doc;    /* when walking an existing doc */
        xmlTextReaderValidate validate;/* is there any validation */
        int allocs;	/* what structure were deallocated */
        xmlTextReaderState state;
        xmlParserCtxt* ctxt;	/* the parser context */
        xmlSAXHandler* sax;	/* the parser SAX callbacks */
        xmlParserInputBuffer* input;	/* the input */
        IntPtr startElement;/* initial SAX callbacks */
        IntPtr /*endElementSAXFunc*/ endElement;  /* idem */
        IntPtr /*startElementNsSAX2Func*/ startElementNs;/* idem */
        IntPtr /*endElementNsSAX2Func*/ endElementNs;  /* idem */
        IntPtr /*charactersSAXFunc*/ characters;
        IntPtr /*cdataBlockSAXFunc*/ cdataBlock;
        uint _base;	/* base of the segment in the input */
        uint cur;	/* current position in the input */
        xmlNode* node;	/* current node */
        xmlNode* curnode;/* current attribute node */
        int depth;  /* depth of the current node */
        xmlNode* faketext;/* fake xmlNs chld */
        int preserve;/* preserve the resulting document */
        xmlBuffer* buffer; /* used to return const xmlChar * */
        xmlDict* dict;	/* the context dictionnary */

        /* entity stack when traversing entities content */
        xmlNode* ent;          /* Current Entity Ref Node */
        int entNr;        /* Depth of the entities stack */
        int entMax;       /* Max depth of the entities stack */
        xmlNode** entTab;       /* array of entities */

        /* error handling */
        IntPtr /*xmlTextReaderErrorFunc*/ errorFunc;    /* callback function */
        void* errorFuncArg; /* callback function user argument */

        //#ifdef LIBXML_SCHEMAS_ENABLED
        /* Handling of RelaxNG validation */
        xmlRelaxNG* rngSchemas;	/* The Relax NG schemas */
        xmlRelaxNGValidCtxt* rngValidCtxt;/* The Relax NG validation context */
        int rngValidErrors;/* The number of errors detected */
        xmlNode* rngFullNode;	/* the node if RNG not progressive */
        /* Handling of Schemas validation */
        xmlSchema* xsdSchemas;	/* The Schemas schemas */
        xmlSchemaValidCtxt* xsdValidCtxt;/* The Schemas validation context */
        int xsdPreserveCtxt; /* 1 if the context was provided by the user */
        int xsdValidErrors;/* The number of errors detected */
        xmlSchemaSAXPlug* xsdPlug;	/* the schemas plug in SAX pipeline */
        //#endif
        //#ifdef LIBXML_XINCLUDE_ENABLED
        /* Handling of XInclude processing */
        int xinclude;	/* is xinclude asked for */
        IntPtr xinclude_name;	/* the xinclude name from dict */
        xmlXIncludeCtxt* xincctxt;	/* the xinclude context */
        int in_xinclude;	/* counts for xinclude */
        //#endif
        //#ifdef LIBXML_PATTERN_ENABLED
        int patternNr;       /* number of preserve patterns */
        int patternMax;      /* max preserve patterns */
        xmlPattern** patternTab;      /* array of preserve patterns */
        //#endif
        int preserves;	/* level of preserves */
        int parserFlags;	/* the set of options set */
        /* Structured error handling */
        IntPtr /*xmlStructuredErrorFunc*/ sErrorFunc;  /* callback function */
    }



    public unsafe static class LibXmlWIP
    {
        public const UInt32 XML_SAX2_MAGIC = 0xDEEDBEAF;

        //xmlParserInputState
        public const Int32 XML_PARSER_CONTENT = 7;
        //xmlEntityType
        public const Int32 XML_INTERNAL_PREDEFINED_ENTITY = 6;



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
        public unsafe delegate xmlEntity* getEntitySAXFunc(IntPtr ctx, IntPtr name);
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
        int nb_namespaces, IntPtr namespaces, int nb_attributes, int nb_defaulted, IntPtr attributes);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void endElementNsSAX2Func(IntPtr ctx, IntPtr localname, IntPtr prefix, IntPtr URI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void xmlStructuredErrorFunc(IntPtr ctx, IntPtr error);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate xmlParserInput* xmlExternalEntityLoader(IntPtr URL, IntPtr ID, xmlParserCtxt* context);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void xmlTextReaderErrorFunc(IntPtr userData, IntPtr pMsg, int severity, IntPtr locator);
        

       


        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlInitParser();

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlCleanupParser();

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlSAXUserParseFile(xmlSAXHandler* sax, IntPtr user_data, [MarshalAs(UnmanagedType.LPArray)] byte[] filename);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlSAXUserParseMemory(xmlSAXHandler* sax, IntPtr user_data, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int size);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlStrlen(IntPtr str);


        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlParseChunk(xmlParserCtxt* ctxt, [MarshalAs(UnmanagedType.LPArray)] byte[] chunk, int size, int terminate);


        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlSetStructuredErrorFunc(IntPtr ctx, IntPtr /*xmlStructuredErrorFunc*/ handler);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlError* xmlCtxtGetLastError(IntPtr ctx);



        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlParserCtxt* xmlCreatePushParserCtxt(xmlSAXHandler* sax, IntPtr user_data,
            [MarshalAs(UnmanagedType.LPArray)] byte[] chunk, int size, [MarshalAs(UnmanagedType.LPArray)] byte[] filename);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlCtxtUseOptions(xmlParserCtxt* ctxt, int options);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlFreeEnumeration(IntPtr/*xmlEnumerationPtr*/ cur);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlEntity* xmlGetPredefinedEntity(IntPtr pName);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlExternalEntityLoader xmlGetExternalEntityLoader();

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlSetExternalEntityLoader(xmlExternalEntityLoader f);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlTextReader* xmlReaderForMemory(byte[] buffer, int size, IntPtr URL, IntPtr encoding, int options);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlTextReaderSetParserProp(xmlTextReader* reader, int prop, int value);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlTextReaderSetErrorHandler(xmlTextReader* reader, xmlTextReaderErrorFunc f, IntPtr pUserData);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int xmlTextReaderLocatorLineNumber(IntPtr locator);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlDoc* xmlTextReaderCurrentDoc(xmlTextReader* reader);




        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlNode* xmlAddChild(xmlNode* parent, xmlNode* cur);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlNode* xmlNewCharRef(xmlDoc* doc, IntPtr name);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern xmlNode* xmlNewReference(xmlDoc* doc, IntPtr name);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr xmlStrsub(IntPtr str, int start, int len);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlFree(IntPtr ptr);


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
        internal static extern xmlEntity* xmlSAX2GetEntity(IntPtr ctx, IntPtr pName);

        [DllImport("libxml2.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern void xmlSAX2InternalSubset(IntPtr ctx, IntPtr pName, IntPtr pExternalID, IntPtr pSystemID);



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


    }
#pragma warning restore 169
}
