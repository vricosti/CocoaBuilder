using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLContext : NSObject
    {
        new public static Class Class = new Class(typeof(NSXMLContext));
        new public static NSXMLContext alloc() { return new NSXMLContext(); }

        private static bool onceToken;
        
        private static NSDictionary _defaultNamespacesForPrefix;

        private static NSDictionary _defaultNamespacesForURI;

        private static NSSet _singleAttributes;

        static NSXMLContext()
        {
            // Static constructor can simulate dispatch_once behavior
            _initDefaultNamespaces();
           
        }

        public static void _initDefaultNamespaces()
        {
            if (onceToken == false)
            {
                NSXMLNode nsXml = (NSXMLNode)NSXMLNode.alloc().initWithKind(NSXMLNodeKind.NSXMLNamespaceKind);
                nsXml.setName("xml");
                nsXml.setObjectValue(new NSString("http://www.w3.org/XML/1998/namespace"));

                NSXMLNode nsXs = (NSXMLNode)NSXMLNode.alloc().initWithKind(NSXMLNodeKind.NSXMLNamespaceKind);
                nsXs.setName("xs");
                nsXs.setObjectValue(new NSString("http://www.w3.org/2001/XMLSchema"));

                NSXMLNode nsXsi = (NSXMLNode)NSXMLNode.alloc().initWithKind(NSXMLNodeKind.NSXMLNamespaceKind);
                nsXsi.setName("xsi");
                nsXsi.setObjectValue(new NSString("http://www.w3.org/2001/XMLSchema-instance"));

                _defaultNamespacesForPrefix = (NSDictionary)NSDictionary.alloc().initWithObjectsAndKeys();
                _defaultNamespacesForURI = (NSDictionary)NSDictionary.alloc().initWithObjectsAndKeys();

                onceToken = true;

                throw new NotImplementedException("FIXME");
            }
        }



        public static NSString stringForObjectValue(id anObject)
        {
            return "";
        }

        public static NSXMLNode defaultNamespaceForPrefix(NSString prefix)
        {
            NSXMLContext._initDefaultNamespaces();
            return (NSXMLNode)_defaultNamespacesForPrefix.objectForKey(prefix);
        }



    }
}
