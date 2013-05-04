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

        NSCharacterSet _charactersToBeSkipped;
        uint _scanLocation;
        //unichar _decimal;
        bool _caseSensitive;
        bool _isUnicode;


        protected NSString _string;



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

//        #define	skipToNextField()	({\
//  while (_scanLocation < myLength() && _charactersToBeSkipped != nil \
//    && (*_skipImp)(_charactersToBeSkipped, memSel, myCharacter(_scanLocation)))\
//    _scanLocation++;\
//  (_scanLocation >= myLength()) ? NO : YES;\
//})

        private bool SkipToNextField()
        {
            while (_scanLocation < _string.Length && 
                 _charactersToBeSkipped != null &&
                 _charactersToBeSkipped.CharacterIsMember(_string[_scanLocation]))
                 _scanLocation++;

            return (_scanLocation >= _string.Length) ? false : true;
        }


        private bool _ScanInt(ref int value)
        {
            uint num = 0;
            uint limit = UInt32.MaxValue / 10;
            bool negative = false;
            bool overflow = false;
            bool got_digits = false;

            if (_scanLocation <_string.Length)
            {
                switch (_string[_scanLocation])
                {
                    case '+':
                        _scanLocation++;
                        break;
                    case '-':
                        negative = true;
                        _scanLocation++;
                        break;
                }
            }
            /* Process digits */
            while (_scanLocation < _string.Length)
            {
                char digit = _string[_scanLocation];

                if ((digit < '0') || (digit > '9'))
                    break;
                if (!overflow)
                {
                    if (num >= limit)
                        overflow = true;
                    else
                        num = (uint) (num * 10 + (digit - '0'));
                }
                _scanLocation++;
                got_digits = true;
            }
            if (!got_digits)
                return false;
            if (value != null)
            {
                if (overflow || (num > (negative ? UInt32.MinValue : UInt32.MaxValue)))
                    value = negative ? Int32.MinValue : Int32.MaxValue;
                else if (negative)
                    value = (int)-num;
                else
                    value = (int)num;
            }
            return true;

        }


        public virtual bool ScanInt(ref int value)
        {
            uint saveScanLocation = _scanLocation;

            if (SkipToNextField() && _ScanInt(ref value))
                return true;
            _scanLocation = saveScanLocation;
            return false;
        }

        public virtual bool ScanFloat(ref float value)
        {
            
            return false;
        }

        public virtual bool ScanUpToCharactersFromSet(NSCharacterSet stopSet, ref NSString stringValue)
        {
            uint saveScanLocation = _scanLocation;
            uint start;

            if (!SkipToNextField())
                return false;

            start = _scanLocation;
            while (_scanLocation < _string.Length)
            {
                if (stopSet.CharacterIsMember(_string[_scanLocation]))
                    break;
                _scanLocation++;
            }

            if (_scanLocation == start)
            {
                _scanLocation = saveScanLocation;
                return false;
            }

            NSRange range = new NSRange();
            range.Location = start;
            range.Length = _scanLocation - start;
            stringValue = _string.SubstringWithRange(range);

            return true;
        }
    }
}
