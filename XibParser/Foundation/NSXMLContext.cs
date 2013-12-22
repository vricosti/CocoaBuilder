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

        private static bool _ns_onceToken;
        
        private static NSDictionary _defaultNamespacesForPrefix;

        private static NSDictionary _defaultNamespacesForURI;

        private static bool _ns_singleAttributes;
        private static NSSet _singleAttributes;

        static NSXMLContext()
        {
            // Static constructor can simulate dispatch_once behavior
            _initDefaultNamespaces();
           
        }

        public static void _initDefaultNamespaces()
        {
            if (_ns_onceToken == false)
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

                _ns_onceToken = true;

                throw new NotImplementedException("FIXME");
            }
        }

        public static void _initSingleAttributes()
        {
            if (_ns_singleAttributes == false)
            {
                _singleAttributes = (NSSet)NSSet.alloc().initWithObjects(
                    (NSString)"checked", (NSString)"compact", (NSString)"declare", (NSString)"defer", 
                    (NSString)"disabled", (NSString)"ismap", (NSString)"multiple", (NSString)"nohref", 
                    (NSString)"noresize", (NSString)"noshade", (NSString)"nowrap", (NSString)"readonly", 
                    (NSString)"selected", null);

                _ns_singleAttributes = true;
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
