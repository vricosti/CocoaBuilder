/*
 * XmlNode.cs - XmlNode base class
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
public class XmlElement:XmlNode
{	
	public XmlElement(String name)
	{
		// pass a null namespace along
		dataPtr=Native._xmlNewNode(IntPtr.Zero,name);
	}

	public XmlElement(IntPtr dataPtr):base(dataPtr)
	{
		XmlNodeCache.Cache(dataPtr,this);
	}

	public XmlNode AddChild(XmlNode child)
	{
		if(child.ElementType == XmlElementType.XML_DOCUMENT_NODE)
		{
			throw new Exception("Document nodes cannot be added anywhere !");
		}
		// clear the cache as this /might/ be freed !
		// I don't take no chances. (wild wild west)
		if(child.ElementType == XmlElementType.XML_TEXT_NODE)
			XmlNodeCache.Uncache(child.DeSerialize()); 
		IntPtr retval=Native._xmlAddChild(dataPtr,child.DeSerialize());
		if(retval == IntPtr.Zero)
		{
			throw new Exception("New node Could not be linked in !");
		}
		return XmlNodeFactory.NewNode(retval);
	}

	public override String FQName
	{
		get
		{
			if(this.Namespace!=null)
			{
				return this.Namespace.Prefix+":"+this.Name;
			}
			return this.Name;
		}
	}

	public String GetAttr(String name)
	{
        return Native._xmlElementGetAttrValue(dataPtr, name).ToCsString();
	}
	
	public void SetAttr(String name,String value)
	{
		Native._xmlElementSetAttrValue(dataPtr,name,value);
	}
	public XmlNodeList Attributes
	{
		get
		{
			IntPtr ptr=Native._xmlElementGetFirstAttr(dataPtr);
			return new XmlNodeList(ptr);
		}
	}

	public XmlNs Namespace
	{
		get
		{
			IntPtr retval=Native._xmlNodeGetNs(dataPtr);
			if(retval==IntPtr.Zero)
			{
				return null;
			}
			return new XmlNs(retval);
		}
	}
}
}
