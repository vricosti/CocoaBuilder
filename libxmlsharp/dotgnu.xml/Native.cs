/*
 * Native.cs - PInvoke calls for libxml_wrapper
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

using System;
using System.Runtime.InteropServices;

namespace dotgnu.xml
{
internal class Native
{
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlParseFile(String file); 
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlParseMemory(String file,int size);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern XmlElementType _xmlNodeGetElementType(IntPtr node);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr _xmlNodeGetName(IntPtr node);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNodeGetNs(IntPtr node);
	
	/* Navigation */
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNodeGetPrevSibling(IntPtr node);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNodeGetNextSibling(IntPtr node);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNodeGetFirstChild(IntPtr node);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNodeGetLastChild(IntPtr node);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNodeGetDocument(IntPtr node);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlElementGetFirstAttr(IntPtr node);
	
	/* String Dumpers */

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr _xmlNodeDump(IntPtr node, int level, int format);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr _xmlDocDump(IntPtr doc, int format);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr _xmlNodeGetContent(IntPtr node);
	
	/* Namespaces */

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr _xmlNsGetHref(IntPtr ns);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr _xmlNsGetPrefix(IntPtr ns);
	
	/* Attributes */

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr _xmlElementGetAttrValue(IntPtr node,
	String name);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlElementGetAttr(IntPtr node,
	String name);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int _xmlElementSetAttrValue(IntPtr node,
	String name,String value);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlElementSetAttr(IntPtr node,
	String name,String value);
	
	/* Cache nodes to prevent double freeing */
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool _xmlNodeCache(IntPtr node, IntPtr nodeObj);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern bool _xmlNodeUncache(IntPtr node);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern bool _xmlNodeIsCached(IntPtr node);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr _xmlNodeGetCached(IntPtr node);

	/* object construction */

    [DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNewDoc(String version);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNewNode(IntPtr ns,String name);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNewText(String content);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNewCDataBlock(String content,int len);
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNewComment(String content);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlNewPI(String name,String content);

	/* Adding to the tree */

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr _xmlAddChild(IntPtr parent,IntPtr child);
 	/* adjusting the tree */
 
 	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
 	internal static extern void _xmlNodeNormalize(IntPtr node);

	/* compression support ie my favourite */

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int _xmlDocGetCompression(IntPtr doc);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int _xmlParserGetCompression();

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void _xmlDocSetCompression(IntPtr doc,int level);

	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void _xmlParserSetCompression(int gzip);

	/* file output */
	
	[DllImport("libxml_wrapper.dll", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void _xmlDocSaveToFile(IntPtr doc,String filename, int format);
	
	
}
}
