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
    public class NSNibConnector : NSObject, NSCoding
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

        public static NSNibConnector Alloc()
        {
            return new NSNibConnector();
        }

        public NSNibConnector()
        {

        }


        public virtual void EstablishConnection()
        {

        }

        public virtual void ReplaceObject(id anObject, id anotherObject)
        {

        }


        public override id InitWithCoder(NSObjectDecoder aDecoder)
        {
            //base.InitWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.ContainsValueForKey(@"NSDestination"))
                {
                    _dst = aDecoder.DecodeObjectForKey(@"NSDestination");
                }
                if (aDecoder.ContainsValueForKey(@"NSSource"))
                {
                    _src = aDecoder.DecodeObjectForKey(@"NSSource");
                }
                if (aDecoder.ContainsValueForKey(@"NSLabel"))
                {
                    _tag = (NSString)aDecoder.DecodeObjectForKey(@"NSLabel");
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

        public static NSNibControlConnector Alloc()
        {
            return new NSNibControlConnector();
        }

        public override void EstablishConnection()
        {
            SEL sel = SEL.SelectorFromString(_tag);
            ((IAction)_src).Target = _dst;
            ((IAction)_src).Action = sel;
        }
    }

    public class NSNibOutletConnector : NSNibConnector
    {
        new public static Class Class = new Class(typeof(NSNibOutletConnector));

        public static NSNibOutletConnector Alloc()
        {
            return new NSNibOutletConnector();
        }

        public override void EstablishConnection()
        {
            if (_src != null)
            {
                NSString selName;
                SEL sel; 	 
          
                selName = NSString.StringWithFormat("set%@%@:", 
                    _tag.SubstringToIndex(1).UppercaseString(),
                    _tag.SubstringFromIndex(1));
                
                sel = SEL.SelectorFromString(selName); 	 
          
                if (sel != null && _src.RespondsToSelector(sel)) 	 
                {
                    _src.PerformSelector(sel, _dst); 	 
                } 	 
                else 	 
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
