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
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/Foundation/Classes/NSScanner_Class/Reference/Reference.html
    //https://github.com/gnustep/gnustep-base/blob/master/Source/NSScanner.m
    public class NSScanner : NSObject
    {
        new public static Class Class = new Class(typeof(NSScanner));
        new public static NSScanner Alloc() { return new NSScanner(); }

        uint _scanLocation;
        //unichar _decimal;
        bool _caseSensitive;
        bool _isUnicode;


        protected string _string;



        public static id ScannerWithString(NSString aString)
        {
            return Alloc().InitWithString(aString);
        }

        public virtual id InitWithString(NSString aString)
        {
            id self = this;

            _string = aString;

            return self;
        }

        public virtual bool ScanInt(ref int value)
        {
            return false;
        }


    }
}
