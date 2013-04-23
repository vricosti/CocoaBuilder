using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSMenuPanel : NSPanel
    {
        new public static Class Class = new Class(typeof(NSMenuPanel));

        protected NSMenu _the_menu;

        [ObjcPropAttribute("menu", GetName=null)]
        public override NSMenu Menu
        {
            set { _the_menu = value; }
        }


    }
}
