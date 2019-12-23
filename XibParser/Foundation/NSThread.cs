using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Smartmobili.Cocoa
{
    public class NSThread : NSObject
    {
        new public static Class Class = new Class(typeof(NSThread));
        new public static NSThread alloc() { return new NSThread(); }

        private Thread _thread;

        [ThreadStaticAttribute]
        static NSMutableDictionary _threadDictionary = (NSMutableDictionary)NSMutableDictionary.alloc().init();

        public static NSThread currentThread()
        {
            Thread curThread = Thread.CurrentThread;
            return new NSThread(curThread);
        }


        //- (NSMutableDictionary *)threadDictionary
        public virtual NSMutableDictionary threadDictionary()
        {
            return _threadDictionary;
        }

        public NSThread()
        {

        }

        private NSThread(Thread thread)
        {
            _thread = thread;
        }

    }
}
