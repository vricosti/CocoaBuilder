using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLFidelityNode : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLFidelityNode));
        new public static NSXMLFidelityNode alloc() { return new NSXMLFidelityNode(); }

        protected uint _fidelity;
        protected NSString _whitespace;
        protected NSMutableArray _names;
        protected NSMutableArray _ranges;


        public static NSString stringValueSubstitutingEntitiesForNode(NSXMLNode node, NSMutableArray ranges, NSMutableArray names, NSString objectValue)
        {
            NSMutableString result = (NSMutableString)NSMutableString.alloc().init();

            NSXMLDTD dtd = node.rootDocument().DTD();
            if (ranges != null && ranges.count() != 0)
            {
                uint offset = 0;
                for (uint i = 0; i < ranges.count(); i++)
                {
                    uint len = ranges.objectAtIndex(i).unsignedIntegerValue();
                    NSString name = (NSString)names.objectAtIndex(i);

                    if (name.characterAtIndex(0) == '#')
                    {
                        NSRange range = new NSRange(0, len);
                        result.appendString(objectValue.substringWithRange(range));
                        result.appendString("#");
                        //str.appendCharacters(,1);
                    }
                    else
                    {
                        NSString entityDecl = (NSString)dtd.entityDeclarationForName(name).objectValue();
                        if (entityDecl == null)
                            entityDecl = (NSString)NSXMLDTD.predefinedEntityDeclarationForName(name).objectValue();

                        NSRange range = new NSRange(0, len);
                        result.appendString(objectValue.substringWithRange(range));
                        result.appendString(entityDecl);
                    }

                    offset = name.length() + len + 2;
                }

                result.appendString(objectValue.substringFromIndex(offset));
            }
            

            return result;
        }


        public override id objectValue()
        {
            id result;
            
            if (_ranges.count() != 0)
            {
                result = NSXMLFidelityNode.stringValueSubstitutingEntitiesForNode(this, _ranges, _names, (NSString)_objectValue);
            }
            else
            {
                result = base.objectValue();
            }
            return result;
            // OK
        }


        public static void setObjectValuePreservingEntitiesForNode(id anObject, NSString str)
        {
            throw new NotImplementedException();
        }

    }
}
