using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBCocoaDocument : IBDocument
    {
        new public static Class Class = new Class(typeof(IBCocoaDocument));
        new public static IBCocoaDocument alloc() { return new IBCocoaDocument(); }

        protected Int64 _nextIDFromIB2Import;
        protected NSDictionary _idsFromIB2Import;
        protected id _classDescriberObserverToken;
        protected IBBindingManager _bindingManager;

        public override id initForURL(NSURL forUrl, NSURL url, NSString type, ref NSError error)
        {
            id self = base.initForURL(forUrl, url, type, ref error);
            if (self != null)
            {
                
            }

            return self;
        }


    }
}
