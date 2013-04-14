/*
 * XmlParser.cs - XmlParser class
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
public class XmlParser
{
	public static XmlDoc Parse(String filename)
	{
		IntPtr ptr = Native._xmlParseFile(filename);
		if(ptr==IntPtr.Zero)
		{
			throw new Exception("Could not parse !");
		}
		XmlDoc doc=(XmlDoc)XmlNodeFactory.NewNode(ptr);
		return doc;
	}
	public static XmlDoc ParseString(String data)
	{
		IntPtr ptr = Native._xmlParseMemory(data,data.Length);
		if(ptr==IntPtr.Zero)
		{
			throw new Exception("Could not parse !");
		}
		XmlDoc doc=(XmlDoc)XmlNodeFactory.NewNode(ptr);
		return doc;
	}
}
}
