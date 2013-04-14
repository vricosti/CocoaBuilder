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
		Console.WriteLine("<"+node.FQName+" "+node+">");
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
		String file="ns.xml";
		if(args.Length==1)
			file=args[0];
		XmlDoc dc=XmlParser.Parse(file);
		dc.Normalize();
		printRecursive(dc);
	}
}
