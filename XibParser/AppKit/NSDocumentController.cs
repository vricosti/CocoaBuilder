using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSDocumentController : NSObject
    {
        new public static Class Class = new Class(typeof(NSDocumentController));
        new public static NSDocumentController alloc() { return new NSDocumentController(); }


        private static volatile NSDocumentController _instance;
        private static object _syncRoot = new Object();

        

        protected NSDocumentController() { }

        public static NSDocumentController sharedDocumentController()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new NSDocumentController();
                }
            }
            return _instance;
        }

        


    }
}
