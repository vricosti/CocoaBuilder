/*
 * XmlNodeFactory.cs - XmlNodeFactory class -- [TODO]
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


namespace dotgnu.xml
{
public abstract class XmlNodeFactory
{
	public static XmlNode NewNode(IntPtr ptr)
	{
		XmlNode retval;
		/*
		 * TODO: find a way to debug ....
		 * 
		 *     Also fix pnet's switch bug so that this works !
		 *     try #define INCLUDE_TODO and running this 
		 */
		if(ptr==IntPtr.Zero)
		{
			return null; //should I throw an exception here ?
		}
		if(XmlNodeCache.IsCached(ptr))
		{
			return XmlNodeCache.GetCached(ptr);
		}
		/*
		 * I wish I could write macros for the following code :(
		 * identical code .... yuck !
		 */
		switch(Native._xmlNodeGetElementType(ptr))
		{
			case XmlElementType.XML_ELEMENT_UNDEF:
				throw new Exception("Impossible !");
				break;

			case XmlElementType.XML_ELEMENT_NODE:
				retval=new XmlElement(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			case XmlElementType.XML_ATTRIBUTE_NODE:
				retval=new XmlAttr(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			case XmlElementType.XML_TEXT_NODE:
				retval=new XmlText(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			case XmlElementType.XML_CDATA_SECTION_NODE:
				retval=new XmlCData(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;
			/*TODO*/
			case XmlElementType.XML_ENTITY_REF_NODE:
				goto default;
			
			/*TODO*/
			case XmlElementType.XML_ENTITY_NODE:
				goto default;

			case XmlElementType.XML_PI_NODE:
				retval=new XmlPI(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;


			case XmlElementType.XML_COMMENT_NODE:
				retval=new XmlComment(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;


			case XmlElementType.XML_DOCUMENT_NODE:
				retval=new XmlDoc(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;
			/*TODO*/
			case XmlElementType.XML_DOCUMENT_TYPE_NODE:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;
			
			/*TODO*/
			case XmlElementType.XML_DOCUMENT_FRAG_NODE:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			/*TODO*/
			case XmlElementType.XML_NOTATION_NODE:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;
			
			/*TODO*/
			case XmlElementType.XML_HTML_DOCUMENT_NODE:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;
			
			/*TODO*/
			case XmlElementType.XML_DTD_NODE:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			/*TODO*/
			case XmlElementType.XML_ELEMENT_DECL:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			/*TODO*/
			case XmlElementType.XML_ATTRIBUTE_DECL:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			/*TODO*/
			case XmlElementType.XML_ENTITY_DECL:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			/*TODO*/
			case XmlElementType.XML_NAMESPACE_DECL:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			/*TODO*/
			case XmlElementType.XML_XINCLUDE_START:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;

			/*TODO*/
			case XmlElementType.XML_XINCLUDE_END:
				retval=new XmlNode(ptr);
				XmlNodeCache.Cache(ptr,retval);
				return retval;
				break;
			default:
				throw new Exception("Unexpected error !");
		}
		//we never get here !
		return null;
	}
}
}
