/*
 * XmlNodeEnumerator.cs 
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
using System.Collections;

namespace dotgnu.xml
{
public class XmlNodeEnumerator : IEnumerator
{
	private IntPtr startPtr;
	private IntPtr indexPtr;
	private bool first_move=true;
	internal XmlNodeEnumerator(XmlNodeList list)
	{
		startPtr=list.DeSerialize();
		indexPtr=startPtr;
	}

	public bool MoveNext()
	{
		if(indexPtr==IntPtr.Zero)
		{
			return false; // and prevent a SEGFAULT
		}
		if(first_move)
		{
			first_move=false;
			return true;
		}

		indexPtr=Native._xmlNodeGetNextSibling(indexPtr);	
		
		if(indexPtr==IntPtr.Zero)
		{
			return false; // no elements left
		}
		/* else */
		return true;
	}
	public void Reset()
	{
		indexPtr=startPtr;
		first_move=true;
	}
	Object IEnumerator.Current
	{
		get
		{
			if(indexPtr!=IntPtr.Zero)
			{
				return XmlNodeFactory.NewNode(indexPtr);
			}
			else
			{
				throw new InvalidOperationException(
				"Invalid/Bad Enumerator Position");
			}
		}
	}
	public XmlNode Current
	{
		get
		{
			if(indexPtr!=IntPtr.Zero)
			{
				return XmlNodeFactory.NewNode(indexPtr);
			}
			else
			{
				throw new InvalidOperationException(
				"Invalid/Bad Enumerator Position");
			}
		}
	}
}
}
