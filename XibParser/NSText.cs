using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSText_Class/Reference/Reference.html
    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/AppKit/NSText.h

    public enum NSTextAlignment 
    {
        NSLeftTextAlignment = 0,
        NSRightTextAlignment,
        NSCenterTextAlignment,
        NSJustifiedTextAlignment,
        NSNaturalTextAlignment
    }

    public enum NSWritingDirection
    {
        NSWritingDirectionNatural = -1,
        NSWritingDirectionLeftToRight = 0,
        NSWritingDirectionRightToLeft
    }

    public enum NSTextMovement
    {
        NSIllegalTextMovement	= 0,
        NSReturnTextMovement	= 0x10,
        NSTabTextMovement	= 0x11,
        NSBacktabTextMovement	= 0x12,
        NSLeftTextMovement	= 0x13,
        NSRightTextMovement	= 0x14,
        NSUpTextMovement	= 0x15,
        NSDownTextMovement	= 0x16
    }

    public enum NSTextCharacter
    {
        NSParagraphSeparatorCharacter = 0x2029,
        NSLineSeparatorCharacter = 0x2028,
        NSTabCharacter = 0x0009,
        NSFormFeedCharacter = 0x000c,
        NSNewlineCharacter = 0x000a,
        NSCarriageReturnCharacter = 0x000d,
        NSEnterCharacter = 0x0003,
        NSBackspaceCharacter = 0x0008,
        NSBackTabCharacter = 0x0019,
        NSDeleteCharacter = 0x007f,
    }



    public class NSText : NSView
    {
        public NSText()
        {

        }
    }
}
