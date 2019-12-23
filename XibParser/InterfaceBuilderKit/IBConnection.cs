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
    public abstract class IBConnection : NSObject
    {
        protected NSString _label;
        protected id _source;
        protected id _destination;

        public virtual NSString Label 
        {
            get { return _label; }
        }

        public virtual id Source 
        {
            get { return _source; } 
        }

        public virtual id Destination 
        {
            get { return _destination; } 
        }

        public virtual NSNibConnector NibConnector
        {
            get 
            {
                NSString tag = this.Label;
                NSRange colonRange = tag.rangeOfString(@":");
                uint location = colonRange.Location;
                NSNibConnector result = null;

                if (location == 0)
                  {
                      result = (NSNibOutletConnector)NSNibOutletConnector.alloc().init(); 
                  }
                else
                  {
                    result = (NSNibControlConnector)NSNibControlConnector.alloc().init(); 
                  }

                result.Destination = this.Destination;
                result.Source = this.Source;
                result.Label = this.Label;
                
                return result;
            }
            
        }



        public IBConnection()
        {

        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            base.initWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                if (aDecoder.containsValueForKey("label"))
                {
                    _label = (NSString)aDecoder.decodeObjectForKey("label");
                }
                if (aDecoder.containsValueForKey("source"))
                {
                    _source = (id)aDecoder.decodeObjectForKey("source");
                }
                if (aDecoder.containsValueForKey("destination"))
                {
                    _destination = (id)aDecoder.decodeObjectForKey("destination");
                }
            }

            return self;
        }
    }
}
