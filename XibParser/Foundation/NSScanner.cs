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
        new public static NSScanner alloc() { return new NSScanner(); }

        public const int LONG_MAX = Int16.MaxValue;
        public const int LONG_MIN = Int16.MinValue;
        public const Int64 DBL_MAX = Int64.MaxValue;

        static NSCharacterSet defaultSkipSet = null;

        NSCharacterSet _charactersToBeSkipped;
        uint _scanLocation;
        Char _decimal;
        bool _caseSensitive;
        bool _isUnicode;


        protected NSString _string;


        static NSScanner() { initialize(); }
        public static void initialize()
        {
            defaultSkipSet = NSCharacterSet.WhitespaceAndNewlineCharacterSet;
        }

        public static id scannerWithString(NSString aString)
        {
            return alloc().initWithString(aString);
        }

        public virtual id initWithString(NSString aString)
        {
            id self = this;

            _string = aString;

            CharactersToBeSkipped = defaultSkipSet;
            _decimal = '.';

            return self;
        }

        public virtual bool isAtEnd()
        {
            uint save__scanLocation;
            bool ret;

            if (_scanLocation >= myLength())
                return true;
            save__scanLocation = _scanLocation;
            ret = !skipToNextField();
            _scanLocation = save__scanLocation;
            return ret;
        }

        
        private uint myLength()
        {
            return _string.Length;
        }

        private Char MyCharacter(uint location)
        {
            return _string[_scanLocation];
        }


        private bool skipToNextField()
        {
            while (_scanLocation < _string.Length && 
                 _charactersToBeSkipped != null &&
                 _charactersToBeSkipped.characterIsMember(MyCharacter(_scanLocation)))
                 _scanLocation++;

            return (_scanLocation >= _string.Length) ? false : true;
        }


        private bool _scanInt(ref int value)
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
            //if (value != null)
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

            if (skipToNextField() && _scanInt(ref value))
                return true;
            _scanLocation = saveScanLocation;
            return false;
        }

        public virtual bool scanFloat(ref float value)
        {
            
            return false;
        }

        public virtual bool scanDouble(ref double value)
        {
            char c = (Char)0;
            double num = 0.0;
            long exponent = 0;
            bool negative = false;
            bool got_dot = false;
            bool got_digit = false;
            uint saveScanLocation = _scanLocation;

            /* Skip whitespace */
            if (!skipToNextField())
            {
                _scanLocation = saveScanLocation;
                return false;
            }

            /* Check for sign */
            if (_scanLocation < myLength())
            {
                switch (MyCharacter(_scanLocation))
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


            /* Process number */
            while (_scanLocation < myLength())
            {
                c = MyCharacter(_scanLocation);
                if ((c >= '0') && (c <= '9'))
                {
                    /* Ensure that the number being accumulated will not overflow. */
                    if (num >= (DBL_MAX / 10.000000001))
                    {
                        ++exponent;
                    }
                    else
                    {
                        num = (num * 10.0) + (c - '0');
                        got_digit = true;
                    }
                    /* Keep track of the number of digits after the decimal point.
                   If we just divided by 10 here, we would lose precision. */
                    if (got_dot)
                        --exponent;
                }
                else if (!got_dot && (c == _decimal))
                {
                    /* Note that we have found the decimal point. */
                    got_dot = true;
                }
                else
                {
                    /* Any other character terminates the number. */
                    break;
                }
                _scanLocation++;
            }
            if (!got_digit)
            {
                _scanLocation = saveScanLocation;
                return false;
            }

            /* Check for trailing exponent */
            if ((_scanLocation < myLength()) && ((c == 'e') || (c == 'E')))
            {
                uint expScanLocation = _scanLocation;
                int expval = 0;


                _scanLocation++;
                if (_scanInt(ref expval))
                {
                    /* Check for exponent overflow */
                    if (num != 0)
                    {
                        if ((exponent > 0) && (expval > (LONG_MAX - exponent)))
                            exponent = LONG_MAX;
                        else if ((exponent < 0) && (expval < (LONG_MIN - exponent)))
                            exponent = LONG_MIN;
                        else
                            exponent += expval;
                    }
                }
                else
                {
                    /* Numbers like 1.23eFOO are accepted (as 1.23). */
                    _scanLocation = expScanLocation;
                }
            }

            //if (value != 0) // ref cannot be null
            {
                if ((num != 0) && (exponent != 0))
                    num *= Math.Pow(10.0, (double)exponent);
                if (negative)
                    value = -num;
                else
                    value = num;
            }
            return true;
        }




        public virtual bool scanUpToCharactersFromSet(NSCharacterSet stopSet, ref NSString stringValue)
        {
            uint saveScanLocation = _scanLocation;
            uint start;

            if (!skipToNextField())
                return false;

            start = _scanLocation;
            while (_scanLocation < _string.Length)
            {
                if (stopSet.characterIsMember(_string[_scanLocation]))
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
            stringValue = _string.substringWithRange(range);

            return true;
        }

        public virtual NSString String
        {
            get { return _string; }
        }


        public virtual uint ScanLocation
        {
            get
            {
                return _scanLocation;
            }
            set
            {
                if (_scanLocation <= myLength())
                    _scanLocation = value;
                else
                    NSException.raise("NSRangeException", @"Attempt to set scan location beyond end of string");
            }
        }

        public virtual NSCharacterSet CharactersToBeSkipped
        {
            get 
            {
                return _charactersToBeSkipped;
            }
            set
            {
                _charactersToBeSkipped = value;
                //_skipImp = (BOOL (*)(NSCharacterSet*, SEL, unichar))
                //[_charactersToBeSkipped methodForSelector: memSel];
            }
        }
    }
}
