using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IDEDocumentController : NSDocumentController
    {
        new public static Class Class = new Class(typeof(IDEEditorDocument));
        new public static IDEEditorDocument alloc() { return new IDEEditorDocument(); }



        public static IDEEditorDocument _newEditorDocumentWithClass(
            Class cls, NSURL forURL, NSURL withContentsOfURL, 
            NSString ofType, NSString extension, ref NSError outError)
        {
            return null;
        }
    }

}
