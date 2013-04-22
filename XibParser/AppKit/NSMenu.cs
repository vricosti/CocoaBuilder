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
    //class NSMenuView
    public class NSMenu : NSObject
    {
        new public static Class Class = new Class(typeof(NSMenu));

        protected NSString _title;
        protected NSMutableArray _items;
        //protected NSView<NSMenuView> _view;
        protected NSMenu _superMenu;
        protected NSMenu _attachedMenu;
        protected NSMutableArray _notifications;
        protected id _delegate; 

        
        public string Title { get; set; }

        public NSMutableArray MenuItems { get; set; }

        public string Name { get; set; }

        public NSMenu()
        {
            MenuItems = new NSMutableArray();
        }

        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {
            base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                Title = (NSString)aDecoder.DecodeObjectForKey("NSTitle");

                MenuItems = (NSMutableArray)aDecoder.DecodeObjectForKey("NSMenuItems");

                Name = (NSString)aDecoder.DecodeObjectForKey("NSName");
            }

            return this;
        }



    }
}
