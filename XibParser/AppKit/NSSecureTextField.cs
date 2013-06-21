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
        new public static NSSecureTextField Alloc() { return new NSSecureTextField(); }

        static NSSecureTextField() { Initialize(); }
        new static void Initialize()
        {
            
        }

        new public static Class CellClass
        {
            get { return NSSecureTextFieldCell.Class; }
            set 
            {
              NSException.Raise(@"NSInvalidArgumentException", @"NSSecureTextField only uses NSSecureTextFieldCells.", null);
            }
        }



        public NSSecureTextField()
        {
        }

        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.InitWithCoder(aDecoder) != null)
            {
                
            }

            return self;
        }
    }

   
}
