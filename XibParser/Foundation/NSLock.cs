﻿/*
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
using System.Threading;

namespace Smartmobili.Cocoa
{
    public class NSLock : NSObject
    {
        new public static Class Class = new Class(typeof(NSLock));
        new public static NSLock alloc() { return new NSLock(); }

        SpinLock _spinLock = new SpinLock();
        bool _gotLock = false;


        //readonly object locker = new object();

        public virtual void Lock()
        {
            //try
            //{
                _spinLock.Enter(ref _gotLock);
            //}
            
            //Monitor.Enter(locker);
        }

        public virtual void Unlock()
        {
            if (_gotLock) 
                _spinLock.Exit();

            //Monitor.Exit(locker);
        }
    }
}
