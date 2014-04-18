using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBLocalDescriptionsClassProvider : IBClassDescriptionBasedClassProvider
    {
        new public static Class Class = new Class(typeof(IBLocalDescriptionsClassProvider));
        new public static IBLocalDescriptionsClassProvider alloc() { return new IBLocalDescriptionsClassProvider(); }

        protected bool _integratedPartials;
    }

//    - (void)integrateDocumentDecodedPartialClassDescriptions:(id)arg1;
//- (void)threadSafeSetDataSourceProvidedDataOnce;
}
