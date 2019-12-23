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
    //http://developer.apple.com/library/mac/#documentation/cocoa/reference/ApplicationKit/Classes/NSNibConnector_Class/Reference/Reference.html#//apple_ref/occ/cl/NSNibConnector
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSBundleAdditions.m
    public class NSNibConnector : NSObject, NSCoding2
    {
        new public static Class Class = new Class(typeof(NSNibConnector));

        protected id _src;
        protected id _dst;
        protected NSString _tag;


        public virtual id Source
        {
            get { return _src; }
            set { _src = value; }
        }

        public virtual id Destination
        {
            get { return _dst; }
            set { _dst = value; }
        }

        public virtual NSString Label
        {
            get { return _tag; }
            set { _tag = value; }
        }

        new public static NSNibConnector alloc()
        {
            return new NSNibConnector();
        }

        public NSNibConnector()
        {

        }


        public virtual void establishConnection()
        {

        }

        public virtual void ReplaceObject(id anObject, id anotherObject)
        {

        }

        public override void encodeWithCoder(NSCoder aCoder)
        {
            if (aCoder.AllowsKeyedCoding)
            {
                if (_src != null)
                {
                    aCoder.encodeObjectForKey(_src, @"NSSource");
                }
                if (_dst != null)
                {
                    aCoder.encodeObjectForKey(_dst, @"NSDestination");
                }
                if (_tag != null)
                {
                    aCoder.encodeObjectForKey(_tag, @"NSLabel");
                }
            }
            else
            {
                aCoder.encodeObject(_src);
                aCoder.encodeObject(_dst);
                aCoder.encodeObject(_tag);
            }
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            //base.initWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.containsValueForKey(@"NSDestination"))
                {
                    _dst = aDecoder.decodeObjectForKey(@"NSDestination");
                }
                if (aDecoder.containsValueForKey(@"NSSource"))
                {
                    _src = aDecoder.decodeObjectForKey(@"NSSource");
                }
                if (aDecoder.containsValueForKey(@"NSLabel"))
                {
                    _tag = (NSString)aDecoder.decodeObjectForKey(@"NSLabel");
                }
            }
            else
            {

            }

            return this;
        }
    }

    public class NSNibControlConnector : NSNibConnector
    {
        new public static Class Class = new Class(typeof(NSNibControlConnector));

        new public static NSNibControlConnector alloc()
        {
            return new NSNibControlConnector();
        }

        public override void establishConnection()
        {
            SEL sel = SEL.SelectorFromString(_tag);
            ((IAction)_src).Target = _dst;
            ((IAction)_src).Action = sel;
        }
    }

    public class NSNibOutletConnector : NSNibConnector
    {
        new public static Class Class = new Class(typeof(NSNibOutletConnector));

        new public static NSNibOutletConnector alloc()
        {
            return new NSNibOutletConnector();
        }

        public override void establishConnection()
        {
            if (_src != null)
            {
                NSString selName;
                SEL sel; 	 
          
                selName = NSString.stringWithFormat("set%@%@:", 
                    _tag.substringToIndex(1).uppercaseString(),
                    _tag.substringFromIndex(1));
                
                sel = SEL.SelectorFromString(selName); 	 
          
                if (sel != null && _src.respondsToSelector(sel)) 	 
                {
                    _src.performSelector(sel, _dst); 	 
                } 	 
                else 	 
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
