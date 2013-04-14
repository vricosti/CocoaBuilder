/*
 * libxml_wrapper.c - Wrapper for libxml2 functions
 *
 * Copyright (C) 2013 Vincent R.
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

#ifndef _LIBXML_WRAPPER_H_
#define _LIBXML_WRAPPER_H_

#ifndef __BEGIN_DECLS
#if defined(__cplusplus)
#define	__BEGIN_DECLS	extern "C" {
#define	__END_DECLS	};
#else
#define	__BEGIN_DECLS
#define	__END_DECLS
#endif
#endif

#ifdef _MSC_VER
#define LIBXML_WRAPPER __declspec(dllexport)
#else
#define LIBXML_WRAPPER __attribute__ ((visibility ("default")))
#endif

#include <libxml/parser.h>
#include <libxml/xmlerror.h>

__BEGIN_DECLS

LIBXML_WRAPPER xmlDocPtr _xmlParseFile(const char* name);

LIBXML_WRAPPER xmlDocPtr _xmlParseMemory(const char* data,int len);

LIBXML_WRAPPER int _xmlNodeCache(xmlNodePtr node,void* nodeObj);

LIBXML_WRAPPER int _xmlNodeUncache(xmlNodePtr node);

LIBXML_WRAPPER int _xmlNodeIsCached(xmlNodePtr node);

LIBXML_WRAPPER void* _xmlNodeGetCached(xmlNodePtr node);

LIBXML_WRAPPER int _XmlNodeIsFreed(xmlNodePtr node);

LIBXML_WRAPPER xmlElementType _xmlNodeGetElementType(xmlNodePtr node);

LIBXML_WRAPPER char* _xmlNodeGetName(xmlNodePtr node);

LIBXML_WRAPPER xmlNodePtr _xmlNodeGetFirstChild(xmlNodePtr node);

LIBXML_WRAPPER xmlNodePtr _xmlNodeGetLastChild(xmlNodePtr node);

LIBXML_WRAPPER xmlNodePtr _xmlNodeGetNextSibling(xmlNodePtr node);

LIBXML_WRAPPER xmlNodePtr _xmlNodeGetPrevSibling(xmlNodePtr node);

LIBXML_WRAPPER xmlDocPtr _xmlNodeGetDocument(xmlNodePtr node);

LIBXML_WRAPPER xmlAttrPtr _xmlElementGetFirstAttr(xmlNodePtr node);

LIBXML_WRAPPER char* _xmlNodeDump(xmlNodePtr node,int level,int format);

LIBXML_WRAPPER char* _xmlDocDump(xmlDocPtr doc,int format);

LIBXML_WRAPPER char* _xmlNodeGetContent(xmlNodePtr node);

LIBXML_WRAPPER xmlNsPtr _xmlNodeGetNs(xmlNodePtr node);

LIBXML_WRAPPER char* _xmlNsGetPrefix(xmlNsPtr ns);

LIBXML_WRAPPER char* _xmlNsGetHref(xmlNsPtr ns);

LIBXML_WRAPPER xmlAttrPtr _xmlElementGetAttr(xmlNodePtr node,const char *name);

LIBXML_WRAPPER xmlAttrPtr _xmlElementSetAttr(xmlNodePtr node,const char *name, const char *value);

LIBXML_WRAPPER char* _xmlElementGetAttrValue(xmlNodePtr node,const char *name);

LIBXML_WRAPPER int _xmlElementSetAttrValue(xmlNodePtr node,const char *name, const char *value);

LIBXML_WRAPPER xmlDocPtr _xmlNewDoc(const char *version);

LIBXML_WRAPPER xmlNodePtr _xmlNewNode(xmlNsPtr ns,const char *name);

LIBXML_WRAPPER xmlNodePtr _xmlNewText(const char *content);

LIBXML_WRAPPER xmlNodePtr _xmlNewComment(const char *content);

LIBXML_WRAPPER xmlNodePtr _xmlNewCDataBlock(const char *content,const int len);

LIBXML_WRAPPER xmlNodePtr _xmlNewPI(const char *name,const char *content);

LIBXML_WRAPPER xmlNodePtr _xmlAddChild(xmlNodePtr parent, xmlNodePtr child);

LIBXML_WRAPPER void _xmlNodeNormalize(xmlNodePtr node);

LIBXML_WRAPPER int _xmlDocGetCompression(xmlDocPtr ptr);

LIBXML_WRAPPER int _xmlParserGetCompression(void);

LIBXML_WRAPPER void _xmlDocSetCompression(xmlDocPtr ptr,int gzip);

LIBXML_WRAPPER void _xmlParserSetCompression(int gzip);

LIBXML_WRAPPER void _xmlDocSaveToFile(xmlDocPtr ptr,char* filename,int format);

LIBXML_WRAPPER xmlDocPtr _xmlSAXUserParseFile(const char* name);

__END_DECLS



#endif //_LIBXML_WRAPPER_H_