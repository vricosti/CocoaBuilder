using dotgnu.xml;
using System;

public class struct_dumper
{
	public static void Main(String[] args)
	{
		String file="struct.xml";
		if(args.Length==1)
			file=args[0];
		XmlDoc dc=XmlParser.Parse(file);
		foreach(XmlNode st in dc.Children)
		{
			if(st.Name=="struct")
			{
				Console.WriteLine("struct {0}",((XmlElement)st).GetAttr("name"));	
				Console.WriteLine("{");	
				foreach(XmlNode el in st.Children)
				{
					if(el.Name=="elem")
					{
						Console.WriteLine("{0} {1};",
						((XmlElement)el).GetAttr("type"),
						((XmlElement)el).GetAttr("name"));
					}
				}
				Console.WriteLine("}");
			}
		}
	}
}
