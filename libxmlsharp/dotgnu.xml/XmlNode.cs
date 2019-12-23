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
/*  TODO : make this abstract after stubbing out all the classes */
public class XmlNode 
{
    public IntPtr DataPtr { get { return dataPtr;  } }

	protected IntPtr dataPtr;
	protected String _name;
	protected XmlElementType _elemType=XmlElementType.XML_ELEMENT_UNDEF;
	protected String _rawXml=null;
	protected XmlNodeList _children=null;
	
	internal XmlNode() //ie to satisfy the inheritance
	{
	}

	internal XmlNode(IntPtr ptr)
	{
		// do not Cache here ! , as we should prevent
		// base() calls from interfering with the XmlNodeCache
		// Cache only at first reference
		dataPtr=ptr;
	}

	~XmlNode()
	{
		XmlNodeCache.Uncache(dataPtr);
	}

	public virtual XmlNode GetPreviousSibling()
	{
		IntPtr retval=Native._xmlNodeGetPrevSibling(dataPtr);
		// actually I should use the this.DeSerialize() , 
		// but what the heck, this is only FUN !
		if(retval==IntPtr.Zero)
		{
			return null;
		}
		return XmlNodeFactory.NewNode(retval);
	}

	public virtual XmlNode GetNextSibling()
	{
		IntPtr retval=Native._xmlNodeGetNextSibling(dataPtr);
		if(retval==IntPtr.Zero)
		{
			return null;
		}
		return XmlNodeFactory.NewNode(retval);
	}
	public virtual XmlNode GetFirstChild()
	{
		IntPtr retval=Native._xmlNodeGetFirstChild(dataPtr);
		if(retval==IntPtr.Zero)
		{
			return null;
		}
		return XmlNodeFactory.NewNode(retval);
	}

	public virtual XmlNode GetLastChild()
	{
		IntPtr retval=Native._xmlNodeGetLastChild(dataPtr);
		if(retval==IntPtr.Zero)
		{
			return null;
		}
		return XmlNodeFactory.NewNode(retval);
	}
	
	public XmlDoc GetDocument()
	{
		IntPtr retval=Native._xmlNodeGetDocument(dataPtr);	
		if(retval==IntPtr.Zero)
		{
			return null;
		}
		return (XmlDoc)(XmlNodeFactory.NewNode(retval));
	}


    public virtual String DumpXml(int format = 1)
	{
		if(_rawXml==null)
		{
            IntPtr ptr = Native._xmlNodeDump(dataPtr, 0, format);
            _rawXml = ptr.ToCsString();

		}
		return _rawXml;
	}

	public virtual String DumpXml(int level,int format)
	{
		if(_rawXml==null)
		{
            IntPtr ptr = Native._xmlNodeDump(dataPtr, level, format);
            _rawXml = ptr.ToCsString();
		}
		return _rawXml;
	}

	/*
	 * Ala Python , Note: base.ToString gives a nice type string ;)
	 */
	public String ShortString()
	{
		String data=this.DumpXml(0);
		if(data.Length < 20)
			return "<"+base.ToString()+"\""+ data+"\">";
		String retval=data.Substring(0,12);
		if(retval.Length == data.Length)
			{
				return retval;
			}
		for(int i=retval.Length;i<17;i++)
			retval=retval+".";
		retval=retval+data.Substring(data.Length-4,3);
		return "<"+base.ToString()+" \""+retval+"\" >";
	}

	public void RemoveCachedEntries()
	{
		_name=null;
		_rawXml=null;
		_children=null;		
	}
	public void Normalize()
	{
		foreach(XmlNode nd in this.Children)
		{
			nd.Normalize();
		}
		this.RemoveCachedEntries();
		Native._xmlNodeNormalize(dataPtr);
	}
	
	internal IntPtr DeSerialize()
	{
		return dataPtr;
	}

	public virtual String Name
	{
		get
		{
			if(_name==null)
			{
				_name=Native._xmlNodeGetName(dataPtr).ToCsString();
			}
			return _name;
		}
	}
	
	public virtual XmlNodeList Children
	{
		get
		{
			if(_children==null)
				_children=new 
					XmlNodeList(Native._xmlNodeGetFirstChild(dataPtr));
			return _children;
		}
	}
	public virtual String Content
	{
		get
		{
			return Native._xmlNodeGetContent(dataPtr).ToCsString();
		}
	}

	public virtual String FQName
	{
		get
		{
			return this.Name;
		}
	}
	
	public virtual XmlElementType ElementType
	{
		get
		{
			if(_elemType==XmlElementType.XML_ELEMENT_UNDEF)
			{
				_elemType=Native._xmlNodeGetElementType(dataPtr);
			}
			return _elemType;
		}
	}
}
}
