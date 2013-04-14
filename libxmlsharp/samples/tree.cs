using System;
using dotgnu.xml;
public class test
{
	static int indent=0;
	static int nodes=0;
	private static void printIndent()
	{	
		for(int i=0;i<indent;i++)
			Console.Write("  ");
	}
	public static void printRecursive(XmlNode node)
	{
		printIndent();
		Console.Write("+--");
		Console.Write("<"+node.Name+" "+node+" >\n");
		nodes++;
		if(node.ElementType==XmlElementType.XML_ELEMENT_NODE)
		{
			XmlElement el=(XmlElement)node;
			indent+=4;
			foreach(XmlAttr ar in el.Attributes)
			{
				printIndent();
				Console.Write("+--");
				Console.WriteLine(ar.Name+" => "+ar.Value);
			}
			indent-=4;
		}
		if(node.GetFirstChild()!=null)
		{
			indent+=2;
			printRecursive(node.GetFirstChild());
			indent-=2;
		}
		if(node.GetNextSibling()!=null)
		{
			printRecursive(node.GetNextSibling());
		}
	}
	public static void Main(String[] args)
	{
		String file="tree.xml";
		if(args.Length==1)
			file=args[0];
		XmlDoc dc=XmlParser.Parse(file);
		Console.WriteLine("**********Before Normalization***********");
		printRecursive(dc);
		Console.WriteLine(" == {0} Nodes",nodes);
		nodes=0;
		
		Console.WriteLine("***********After Normalization***********");
		dc.Normalize();
		printRecursive(dc);
		Console.WriteLine(" == {0} Nodes",nodes);
	}
}
