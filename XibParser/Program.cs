using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class TestNXMLParser : NSObject
    {
        public void Run()
        {
            NSString str =
                @"<test:Plan xmlns:test='http://test.org/schema'>" +
                "<test:Case name='test1' emptyAttribute='' test:ns_id='auio'>" +
                "</test:Case>" +
                "</test:Plan>";

            NSData data = str.dataUsingEncoding(NSStringEncoding.NSUTF8StringEncoding);
            NSXMLParserWIP parser = (NSXMLParserWIP)NSXMLParserWIP.alloc().initWithData(data);
            parser.setDelegate(this);
            parser.parse();

        }

        public virtual void parserDidStartDocument(NSXMLParserWIP parser)
        {
            NS.Log("parserDidStartDocument");
        }

        public virtual void parserDidEndDocument(NSXMLParserWIP parser)
        {
            NS.Log("Document finished");
        }

        public virtual void parserDidStartElement(NSXMLParserWIP parser, NSString elementName, NSString namespaceURI, NSString qualifiedName, NSDictionary attributeDict)
        {
            NS.Log("didStartElement : elementName=%@, namespaceURI=%@, qualifiedName=%@, attributes=%@",
                elementName, namespaceURI, qualifiedName, attributeDict);
        }


        public virtual void parserDidStartMappingPrefix(NSString prefix, NSString namespaceURI)
        {
            NS.Log("didStartMappingPrefix : prefix=%@, namespaceURI=%@", prefix, namespaceURI);
        }

        //didStartMappingPrefix : prefix=test, namespaceURI=http://test.org/schema
        public virtual void parserDidEndElement(NSXMLParserWIP parser, NSString elementName, NSString namespaceURI, NSString qualifiedName)
        {
            NS.Log("didEndElement : elementName=%@, namespaceURI=%@, qualifiedName=%@",
                elementName, namespaceURI, qualifiedName);

           // System.Diagnostics.Debug.WriteLine("parserDidEndElement");
        }

        public virtual void parserFoundCharacters(NSXMLParserWIP parser, NSString foundCharacters)
        {
            System.Diagnostics.Debug.WriteLine("parserFoundCharacters");
        }
    }


    class Program
    {
        

        static void Main(string[] args)
        { 
            TestNXMLParser testXMLParser = new TestNXMLParser();
            testXMLParser.Run();

            //NSXMLNodeOptions xmlNodeOPtions = (NSXMLNodeOptions)0x800004;

#if TEST_COLOR
            GSNamedColor nColor1 = (GSNamedColor)GSNamedColor.alloc().initWithCatalogName("list1", "color1");
            GSNamedColor nColor2 = (GSNamedColor)GSNamedColor.alloc().initWithCatalogName("list2", "color2");
            GSNamedColor nColor11 = (GSNamedColor)GSNamedColor.alloc().initWithCatalogName("list1", "color1");

            //Why does it work without having to override GetHashCode and Equals ???
            System.Diagnostics.Debug.Assert(nColor1.Equals(nColor11));
#endif



//#if TEST

#if TEST_PATH
            NSString strPath = @"";
            NSString strPathRes = null;

            strPath = "/tmp/scratch.tiff";
            strPathRes = strPath.LastPathComponent();
            System.Diagnostics.Debug.Assert(strPathRes.IsEqualToString(@"scratch.tiff"));
            
            strPath = "/tmp/scratch";
            strPathRes = strPath.LastPathComponent();
            System.Diagnostics.Debug.Assert(strPathRes.IsEqualToString(@"scratch"));

            strPath = "/tmp/";
            strPathRes = strPath.LastPathComponent();
            System.Diagnostics.Debug.Assert(strPathRes.IsEqualToString(@"tmp"));
            
            strPath = "scratch///";
            strPathRes = strPath.LastPathComponent();
            System.Diagnostics.Debug.Assert(strPathRes.IsEqualToString(@"scratch"));

            strPath = "/";
            strPathRes = strPath.LastPathComponent();
            System.Diagnostics.Debug.Assert(strPathRes.IsEqualToString(@"/"));
#endif

            //string xibPath = @"C:/Developer/cygwin/home/Vincent/projects/CocoaBuilder/Tests/Button/ButtonTextAlign/ButtonTextAlign/en.lproj/ButtonTextAlign.xib";
            //string xibPath = @"/Users/v.richomme/Developer/CocoaBuilder/Tests/Button/ButtonTextAlign/ButtonTextAlign/en.lproj/ButtonTextAlign.xib";

            //var tmp = new GSModelLoaderFactory();

            //string fullPath = System.Reflection.Assembly.GetAssembly(typeof(Smartmobili.Cocoa.Program)).Location;
			string progPath = System.Reflection.Assembly.GetAssembly (typeof(Smartmobili.Cocoa.Program)).Location;
			string progDir = Path.GetDirectoryName(progPath);
			string xibPath = progDir + "/../../../Tests/Button/ButtonTextAlign/ButtonTextAlign/en.lproj/ButtonTextAlign.xib";

            //NSData data = NSData.alloc().initWithContentsOfFile(xibPath);
            //if (data != null)
            IBDocument ibDoc = (IBDocument)IBDocument.alloc().init();
            if (ibDoc.ReadFromURL((NSURL)NSURL.fileURLWithPath(xibPath), ""))
            {
               // var u = GSXibKeyedUnarchiver.alloc().initForReadingWithData(data);
                //id container = u.decodeObjectForKey(@"IBDocument.Objects");
                //if (container == null || container.isKindOfClass(IBObjectContainer.Class) == false)
                if (false)
                {
                    //result = NO;
                }
                else
                {
                    //NSArray rootObjects = (NSArray)u.decodeObjectForKey(@"IBDocument.RootObjects");
                    var rootObjects = ibDoc.RootObjects;

                    NSWindowTemplate nsWindow = (NSWindowTemplate)rootObjects.Where(o =>
                    (o != null) && (o.isKindOfClass(NSWindowTemplate.Class))).FirstOrDefault();
                    if (nsWindow != null)
                    {
                        if (nsWindow.View != null)
                        {
                            NSView view = (NSView)nsWindow.View;
                            // We want to see differences between buttons starting from the upper button to the lower one
                            // Note tha origin in cocoa is the left bottom side
                            var btnArray = view.SubViews.Where(o => (o != null) && (o.GetType() == typeof(NSButton))).OrderByDescending(x => ((NSButton)x).Frame.Origin.Y).ToArray();
                            if (btnArray != null && btnArray.Length > 0)
                            {
                                KellermanSoftware.CompareNetObjects.CompareObjects compareObjects = new KellermanSoftware.CompareNetObjects.CompareObjects();
                                if (!compareObjects.Compare(btnArray[0], btnArray[1]))
                                {
                                    System.Diagnostics.Trace.WriteLine(compareObjects.DifferencesString);
                                    Console.WriteLine(compareObjects.DifferencesString);
                                }
                                if (!compareObjects.Compare(btnArray[0], btnArray[2]))
                                {
                                    System.Diagnostics.Trace.WriteLine(compareObjects.DifferencesString);
                                    Console.WriteLine(compareObjects.DifferencesString);
                                }
                                if (!compareObjects.Compare(btnArray[0], btnArray[3]))
                                {
                                    System.Diagnostics.Trace.WriteLine(compareObjects.DifferencesString);
                                    Console.WriteLine(compareObjects.DifferencesString);
                                }
                                if (!compareObjects.Compare(btnArray[0], btnArray[4]))
                                {
                                    System.Diagnostics.Trace.WriteLine(compareObjects.DifferencesString);
                                    Console.WriteLine(compareObjects.DifferencesString);
                                }
                            }
                        }
                    }
                }
            }
            
//#endif
        }
    }
}
