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

        private static bool _singleAttributes_onceToken;
        private static NSSet _singleAttributes;

        private static bool _emptyHTMLNames_onceToken;
        private static NSSet _emptyHTMLNames;

        private static bool _transformers_onceToken;
        private static NSMutableDictionary _transformers;

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
            if (_singleAttributes_onceToken == false)
            {
                _singleAttributes = (NSSet)NSSet.alloc().initWithObjects(
                    (NSString)"checked", (NSString)"compact", (NSString)"declare", (NSString)"defer", 
                    (NSString)"disabled", (NSString)"ismap", (NSString)"multiple", (NSString)"nohref", 
                    (NSString)"noresize", (NSString)"noshade", (NSString)"nowrap", (NSString)"readonly", 
                    (NSString)"selected", null);

                _singleAttributes_onceToken = true;
            } 
        }

        public static void _initEmptyHTMLNames()
        {
            if (_emptyHTMLNames_onceToken == false)
            {
                _emptyHTMLNames = (NSSet)NSSet.alloc().initWithObjects(
                     (NSString)"area", (NSString)"base", (NSString)"basefont", (NSString)"br",  
                     (NSString)"col", (NSString)"frame", (NSString)"hr", (NSString)"img", 
                     (NSString)"input", (NSString)"isindex", (NSString)"link", (NSString)"meta", 
                     (NSString)"param", null);

                _emptyHTMLNames_onceToken = true;
            }
        }



        public static void initValueTransformers()
        {
            if (_transformers_onceToken == false)
            {
                _transformers = (NSMutableDictionary)NSMutableDictionary.alloc().init();
                _transformers.setObjectForKey(NSXMLNSNumberTransformerName.alloc().init(), (NSString)"NSNumber");
                _transformers.setObjectForKey(NSXMLNSURLTransformerName.alloc().init(), (NSString)"NSURL");
                _transformers.setObjectForKey(NSXMLNSDataTransformerName.alloc().init(), (NSString)"NSData");
                _transformers.setObjectForKey(NSXMLNSDateTransformerName.alloc().init(), (NSString)"NSDate");
                _transformers.setObjectForKey(NSXMLNSArrayTransformerName.alloc().init(), (NSString)"NSArray");
                
                
                _transformers_onceToken = true;
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
