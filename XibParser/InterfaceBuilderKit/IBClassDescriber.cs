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
    public class IBClassDescriber : NSObject
    {
        public NSMutableArray ReferencedPartialClassDescriptions { get; set; }

        protected NSMutableDictionary _compositeDescriptions; //0x04(x86)
        protected NSMutableDictionary _classNamesToSubclasseNames; //0x08(x86)
        protected IBDocument _document; //0x0C(x86)
        
        private static NSMutableDictionary _systemClassDescriptions;
        
        public IBClassDescriber()
        {
            ReferencedPartialClassDescriptions = new NSMutableArray();
        }


        public static NSMutableArray systemClassDescriptionsForTargetRuntime(IBTargetRuntime targetRuntime)
        {
            NSMutableArray sysClassDescs = NSMutableArray.array();

            NSArray values = ((NSDictionary)_systemClassDescriptions.objectForKey(targetRuntime)).allValues();
            foreach(id value in values)
            {
                NSDictionary valDict = (NSDictionary)value;
                sysClassDescs.addObjectsFromArray(valDict.allValues());
            }

            return sysClassDescs;
        }



        public virtual void setDocument(IBDocument doc)
        {
            if ((doc != null) && (_document == null || (_document == doc)))
            {
                _document = doc;
                this.integrateSystemClassDescriptions();
                if (IBDocument.shouldUpdateSourceFileRelativePaths())
                {
                    //this.documentURLChangedFrom(null, _document.fileURL());
                }
            }
            else
            {
                NSException.raise("dqsdqsd", "");
            }
            
        }

        public virtual void integrateSystemClassDescriptions()
        {
            NSError error = null;
            var sysClassDesc = systemClassDescriptionsForTargetRuntime(_document.targetRuntime());
            this.integratePartialClassDescriptions(sysClassDesc, ref error);
        }

        public virtual void integratePartialClassDescriptions(NSMutableArray classDescs, ref NSError error)
        {
            //FIXME
        }


        public override id initWithCoder(NSCoder aDecoder)
        {
            base.initWithCoder(aDecoder);

            if (aDecoder.AllowsKeyedCoding)
            {
                ReferencedPartialClassDescriptions = (NSMutableArray)aDecoder.decodeObjectForKey("referencedPartialClassDescriptions");
            }

            return this;
        }
    }
}
