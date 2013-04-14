/*
 * libxml_wrapper.c - Wrapper for libxml2 functions
 *
 * Copyright (C) 2002 Gopal.V
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

/*
 * Predominantly the DOM aspects of the libs 
 */

#include <string.h>

#include "libxml_wrapper.h"


#define NODESTATUS_FREED 0x01

//just in case I decide to keep more data in a Node
// epilog: well I did ;-)
typedef struct 
{
	unsigned int refCount; // references
	void* nodeObj; // the ILObject * object 
	unsigned int status; // signals bitfield
}nodeInfo;

#define NODEINFO_OBJECT(x) (((nodeInfo*)(x->_private))->nodeObj)
#define NODEINFO_REFCOUNT(x) (((nodeInfo*)(x->_private))->refCount)
#define NODEINFO_STATUS(x) (((nodeInfo*)(x->_private))->status)
#define NODEINFO_IS_FREE(x) ((((nodeInfo*)(x->_private))->status) & \
								NODESTATUS_FREED)
/*
 * Just _<name> all used funcs
 */
xmlDocPtr _xmlParseFile(const char* name)
{
	return xmlParseFile(name);
}

xmlDocPtr _xmlParseMemory(const char* data,int len)
{
	return xmlParseMemory(data,len);
}

int _xmlNodeCache(xmlNodePtr node,void* nodeObj)
{
	if(!node)return 0;
	if((node->_private))
	{
		return 0;// already cached/error ?
	}
	if(!(node->_private))
	{
		node->_private=(nodeInfo*)calloc(1,sizeof(nodeInfo));
		//fprintf(stdout,"0x%x\n", node->_private);
	}
	NODEINFO_OBJECT(node)=nodeObj;
	NODEINFO_REFCOUNT(node)=1;
	return 1;
}

int _xmlNodeUncache(xmlNodePtr node)
{
	if(!node)return 0;
	if(node->_private==NULL)return 0;
	if(NODEINFO_REFCOUNT(node)<1)
	{
		fprintf(stderr,"**ERROR in refCount\n");
		return 0;
	}
	if(NODEINFO_REFCOUNT(node)>1)
	{
		NODEINFO_REFCOUNT(node)=NODEINFO_REFCOUNT(node)-1;
	}
#ifdef DEBUG
	fprintf(stderr,"freeing %s\n",node->name);
#endif
	if(NODEINFO_IS_FREE(node))
	{
		free(node->_private);
		node->_private=NULL;
		//xmlFreeNode(node);
	}
	else
	{
		free(node->_private);
		node->_private=NULL;
	}
	return 1;
}

int _xmlNodeIsCached(xmlNodePtr node)
{
	if(!node)return 0;
	//printf("<%08x>\n",node->_private);
	return (node->_private==NULL ? 0 : 1);	
}

void* _xmlNodeGetCached(xmlNodePtr node)
{
	if(!node)return NULL;
	NODEINFO_REFCOUNT(node)=NODEINFO_REFCOUNT(node)+1;
	return NODEINFO_OBJECT(node);	
}

int _XmlNodeIsFreed(xmlNodePtr node)
{
	if(!node)return 0;
	return NODEINFO_IS_FREE(node);
}

/* 
 * all the prototypes from the libxml2 is used directly
 * we need some more funcs to access each member of XmlDoc
 */
xmlElementType _xmlNodeGetElementType(xmlNodePtr node)
{
	return node->type;
}

char* _xmlNodeGetName(xmlNodePtr node)
{
	return (char*)(node->name);
}

xmlNodePtr _xmlNodeGetFirstChild(xmlNodePtr node)
{
	return node->children;
}

xmlNodePtr _xmlNodeGetLastChild(xmlNodePtr node)
{
	return node->last;
}

xmlNodePtr _xmlNodeGetNextSibling(xmlNodePtr node)
{
	return node->next;
}

xmlNodePtr _xmlNodeGetPrevSibling(xmlNodePtr node)
{
	return node->prev;
}

xmlDocPtr _xmlNodeGetDocument(xmlNodePtr node)
{
	return node->doc;
}

xmlAttrPtr _xmlElementGetFirstAttr(xmlNodePtr node)
{
	return node->properties;
}

char* _xmlNodeDump(xmlNodePtr node,int level,int format)
{
	xmlBufferPtr buf;
	const xmlChar *content;

	//xmlSetBufferAllocationScheme(XML_BUFFER_ALLOC_DOUBLEIT);
	//buf=xmlBufferCreateSize(60);
	buf = xmlBufferCreate();
	xmlBufferEmpty(buf);
	xmlNodeDump(buf,node->doc,node,level,format);
	content = xmlBufferContent(buf);
	return (char*)content;
}
/* Because xmlDoc needs another dumper */
char* _xmlDocDump(xmlDocPtr doc,int format)
{
	char *str=NULL;
	int size;
	xmlDocDumpFormatMemory(doc,(xmlChar**)(&str),&size,format);
	return str;
}

char* _xmlNodeGetContent(xmlNodePtr node)
{
	return (char*)xmlNodeGetContent(node);
}

/* Namespace Support */
xmlNsPtr _xmlNodeGetNs(xmlNodePtr node)
{
	return node->ns;
}

char* _xmlNsGetPrefix(xmlNsPtr ns)
{
	if(!ns)return NULL;
	//printf("<%s>\n",ns->prefix);
	return (char*)BAD_CAST(ns->prefix);
}
char* _xmlNsGetHref(xmlNsPtr ns)
{
	if(!ns)return NULL;
	return (char*)BAD_CAST(ns->href);
}

/* Attribute Support */
xmlAttrPtr _xmlElementGetAttr(xmlNodePtr node,const char *name)
{
	return xmlHasProp(node, (const xmlChar*)name);
}

xmlAttrPtr _xmlElementSetAttr(xmlNodePtr node,const char *name,
				const char *value)
{
	return xmlSetProp(node,(const xmlChar*)name,(const xmlChar*)value);
}

char* _xmlElementGetAttrValue(xmlNodePtr node,const char *name)
{
	return (char*)(xmlGetProp(node,(const xmlChar*)name));
}

int _xmlElementSetAttrValue(xmlNodePtr node,const char *name,
				const char *value)
{
	return xmlSetProp(node,(const xmlChar*)name,(const xmlChar*)value)==NULL ? 0 : 1 ;
}

/*
 * Now the Object construction methods ... ie the more dangerous
 * part of this work ;)
 */
xmlDocPtr _xmlNewDoc(const char *version)
{
	return xmlNewDoc((const xmlChar*)version);
}

/*
 * We've named the XmlElement node to replace the _xmlNode
 * so this is actually XmlElement() ;
 * 
 * But will use coz, all the Node operations are defined for an
 * Element (child,sibling....)
 */
xmlNodePtr _xmlNewNode(xmlNsPtr ns,const char *name)
{
	return xmlNewNode(ns,(const xmlChar*)name);
}

xmlNodePtr _xmlNewText(const char *content)
{
	return xmlNewText(BAD_CAST(content));
}

xmlNodePtr _xmlNewComment(const char *content)
{
	return xmlNewComment(BAD_CAST(content));
}

xmlNodePtr _xmlNewCDataBlock(const char *content,const int len)
{
	return xmlNewCDataBlock(NULL,BAD_CAST(content),len);
}

xmlNodePtr _xmlNewPI(const char *name,const char *content)
{
	return xmlNewPI(BAD_CAST(name),BAD_CAST(content));
}


/* this is Insubordination , I say ! */

xmlNodePtr _xmlAddChild(xmlNodePtr parent, xmlNodePtr child)
{
	return xmlAddChild(parent,child);
}

/* normalize a node -- ie my way !*/
void _xmlNodeNormalize(xmlNodePtr node)
{
	xmlNodePtr ptr=node->children;
	xmlNodePtr tmp;
	while(ptr!=NULL)
	{
		if(xmlIsBlankNode(ptr))
		{
			tmp=ptr->prev;
			xmlUnlinkNode(ptr);
			if(_xmlNodeIsCached(ptr))
			{
				NODEINFO_STATUS(ptr)=NODEINFO_STATUS(ptr) | NODESTATUS_FREED;
				// mark for GC ;-)
				// I have to emulate the mark and sweep technique
				// to match the ilrun GC -- bah !
			}
			else
			{
				xmlFreeNode(ptr);
			}
			if(!tmp)//if it is the first node
				ptr=node->children;
			else
				ptr=tmp->next;
		}
		else if(xmlNodeIsText(ptr) && ptr->next && xmlNodeIsText(ptr->next))
		{
			tmp=ptr->next;
			ptr=xmlTextMerge(ptr,ptr->next);
			xmlUnlinkNode(tmp);
			ptr=ptr->next;
		}
		else
		{
			ptr=ptr->next;
		}
	}
}


/* compression support */
int _xmlDocGetCompression(xmlDocPtr ptr)
{
	return xmlGetDocCompressMode(ptr);
}

int _xmlParserGetCompression(void)
{
	return xmlGetCompressMode();
}

void _xmlDocSetCompression(xmlDocPtr ptr,int gzip)
{
	xmlSetDocCompressMode(ptr,gzip);
}
void _xmlParserSetCompression(int gzip)
{
	xmlSetCompressMode(gzip);
}

/* file output support */
void _xmlDocSaveToFile(xmlDocPtr ptr,char* filename,int format)
{
	xmlSaveFormatFile(filename,ptr,format);
}
