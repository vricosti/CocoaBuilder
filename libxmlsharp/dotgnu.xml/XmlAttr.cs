/*
 * XmlAttr.cs - XmlAttr class XML_NODE_ATTRIBUTE
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
public class XmlAttr:XmlNode
{
    public XmlAttr(IntPtr dataPtr) : base(dataPtr) { }
	public String Value
	{
		get
		{
            String s = string.Empty;

            IntPtr ptr = Native._xmlNodeDump(Native._xmlNodeGetFirstChild(dataPtr), 0, 0);
            if (ptr != IntPtr.Zero)
            {
                s = ptr.ToCsString();
            }

            return s;
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
