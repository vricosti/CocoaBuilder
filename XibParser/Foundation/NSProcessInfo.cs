using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSProcessInfo
    {
        protected NSString _procName;


        public static NSProcessInfo ProcessInfo
        {
            get 
            {
                NSProcessInfo procInfo = new NSProcessInfo();

                Process currentProcess = Process.GetCurrentProcess();
                procInfo._procName = currentProcess.ProcessName;

                return procInfo;
            }
        }

        public NSString ProcessName
        {
            get { return _procName; }
            set { }
        }



    }
}
