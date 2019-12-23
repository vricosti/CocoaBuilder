/*
* XibParser.
* Copyright (C) 2013 Smartmobili SARL
* 
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License as published by the Free Software Foundation; either
* version 2 of the License, or (at your option) any later version.
* 
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
* 
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, write to the
* Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
* Boston, MA  02110-1301, USA. 
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSProcessInfo : NSObject
    {
        new public static Class Class = new Class(typeof(NSProcessInfo));

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
