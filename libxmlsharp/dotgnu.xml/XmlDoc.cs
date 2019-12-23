/*
 * XmlDoc.cs - XmlDoc class
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
public class XmlDoc : XmlNode
{
	internal XmlDoc(IntPtr dataPtr):base(dataPtr)
	{
		XmlNodeCache.Cache(dataPtr,this);
	}
	public XmlDoc()
	{
		dataPtr=Native._xmlNewDoc("1.0");
		XmlNodeCache.Cache(dataPtr,this);
	}
	public XmlDoc(String xmlVersion)
	{
		dataPtr=Native._xmlNewDoc(xmlVersion);
	}

	public XmlNode AddChild(XmlNode child)
	{
		// clear the cache as this /might/ be freed !
		// I ain't taking no chances. (wild wild west)
		if(this.GetFirstChild()!=null)
		{
			throw new Exception("Cannot add more that one child to a XmlDoc");
		}
		if(child.ElementType==XmlElementType.XML_TEXT_NODE)
			XmlNodeCache.Uncache(child.DeSerialize()); 
		IntPtr retval=Native._xmlAddChild(dataPtr,child.DeSerialize());
		if(retval == IntPtr.Zero)
		{
			throw new Exception("New node Could not be linked in !");
		}
		return XmlNodeFactory.NewNode(retval);
	}
	
	public void SaveToFile(String filename,int format)
	{
		Native._xmlDocSaveToFile(dataPtr,filename,format);
	}

	public void SaveToFile(String filename)
	{
		this.SaveToFile(filename,0);
	}

	public override String DumpXml( int format = 1)
	{
        string s = string.Empty;

        IntPtr ptr = Native._xmlDocDump(dataPtr, format);
        if (ptr != IntPtr.Zero)
        {
            s = ptr.ToCsString();
        }

		return s;
	}

	public int Compression
	{
		get
		{
			return Native._xmlDocGetCompression(dataPtr);
		}
		set
		{
			Native._xmlDocSetCompression(dataPtr,value);
		}
	}

	public override String Name
	{
		get
		{
			return "#DOCUMENT";
		}
	}
}
}
