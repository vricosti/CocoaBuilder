using dotgnu.xml;
using System;
public class DOMBuilder
{
	static int indent=0;
	private static void printIndent()
	{	
		for(int i=0;i<indent;i++)
			Console.Write("  ");
	}
	public static void printRecursive(XmlNode node)
	{
		printIndent();
		Console.Write("+--");
		Console.WriteLine(node.Name);	
		if(node.GetFirstChild()!=null)
		{
			indent++;
			printRecursive(node.GetFirstChild());
			indent--;
		}
		if(node.GetNextSibling()!=null)
		{
			printRecursive(node.GetNextSibling());
		}
	}
	public static void Main()
	{
		XmlDoc dc=new XmlDoc();
		XmlElement nd=new XmlElement("dotgnu");
		XmlElement nd2=new XmlElement("pnet");
		nd2.AddChild(new XmlCData("Hey I work here &"));
		nd2.AddChild(new XmlComment("so does rhys !"));
		nd.AddChild(nd2);
		XmlElement nd3=new XmlElement("phpgw");
		nd3.AddChild(new XmlPI("php","echo \"phpgroupware\";"));
		nd.AddChild(nd3);
		dc.AddChild(nd);
		//dc.Compression = 3;
		//dc.SaveToFile("/tmp/xml.gz",1);
		Console.WriteLine(dc.DumpXml());
		//printRecursive(dc);
	}
}
