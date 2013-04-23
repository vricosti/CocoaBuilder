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
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public enum NSInterfaceStyle
    {
        NSNoInterfaceStyle = 0,
        NSNextStepInterfaceStyle = 1,
        NSMacintoshInterfaceStyle = 2,
        NSWindows95InterfaceStyle = 3,

        /*
         * GNUstep specific. Blame: Michael Hanni.
         */
        GSWindowMakerInterfaceStyle = 4
    } 

    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSResponder.m
    public class NSResponder : NSObject
    {
        new public static Class Class = new Class(typeof(NSResponder));

        protected NSInterfaceStyle _interface_style;
        protected NSResponder _next_responder;
        protected NSMenu _menu;


        
        public virtual NSResponder NextResponder 
        {
            get { return _next_responder; }
            set { _next_responder = value; }  
        }

        public virtual NSMenu Menu 
        {
            get { return _menu; }
            set { _menu = value; } 
        }

        public NSResponder()
        {

        }


        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {
            id self = this;

            id menu = null;

            base.InitWithCoder(aDecoder);
            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.ContainsValueForKey(@"NSInterfaceStyle"))
                {
                    _interface_style = (NSInterfaceStyle)aDecoder.DecodeIntForKey(@"NSInterfaceStyle");
                }
                if (aDecoder.ContainsValueForKey(@"NSMenu"))
                {
                    menu = (NSMenu)aDecoder.DecodeObjectForKey(@"NSMenu");
                }
                if (aDecoder.ContainsValueForKey(@"NSNextResponder"))
                {
                    NextResponder = (NSResponder)aDecoder.DecodeObjectForKey(@"NSNextResponder");
                }
            }
            else
            {

            }

            Menu = (NSMenu)menu;

            return self;
        }
    }
}
