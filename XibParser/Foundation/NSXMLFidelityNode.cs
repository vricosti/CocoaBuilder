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
            NSString result = null;

            NSXMLDTD dtd = node.rootDocument().DTD();
            NSMutableString str = (NSMutableString)NSMutableString.alloc().init();
            if (ranges != null && ranges.count() != 0)
            {
                for (uint i = 0; i < ranges.count(); i++)
                {
                    uint len = ranges.objectAtIndex(i).unsignedIntegerValue();
                    NSString name = (NSString)names.objectAtIndex(i);

                    if (name.characterAtIndex(0) == '#')
                    {
                        NSRange range = new NSRange(0, len);
                        str.appendString(objectValue.substringWithRange(range));
                        str.appendString("#");
                        //str.appendCharacters(,1);
                    }
                    else
                    {
                        var entityDecl = dtd.entityDeclarationForName(name);
                    }
                }
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
