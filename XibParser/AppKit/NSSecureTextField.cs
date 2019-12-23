using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{

    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSSecureTextField.m
    public class NSSecureTextField : NSTextField
    {
        new public static Class Class = new Class(typeof(NSSecureTextField));
        new public static NSSecureTextField alloc() { return new NSSecureTextField(); }

        static NSSecureTextField() { initialize(); }
        new static void initialize()
        {
            
        }

        new public static Class CellClass
        {
            get { return NSSecureTextFieldCell.Class; }
            set 
            {
              NSException.raise(@"NSInvalidArgumentException", @"NSSecureTextField only uses NSSecureTextFieldCells.", null);
            }
        }



        public NSSecureTextField()
        {
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.initWithCoder(aDecoder) != null)
            {
                
            }

            return self;
        }
    }

   
}
