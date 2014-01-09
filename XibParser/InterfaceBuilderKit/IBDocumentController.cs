using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBDocumentController : NSDocumentController
    {
        new public static Class Class = new Class(typeof(IBDocumentController));
        new public static IBDocumentController alloc() { return new IBDocumentController(); }

        protected NSDocument _activeDocument; //0x2C(x86) - ???(x64)

      

        public virtual NSDocument activeDocument()
        {
            return _activeDocument;
        }
    }
}
