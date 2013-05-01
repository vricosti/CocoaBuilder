using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSMenuItemCell.m
    public class NSMenuItemCell : NSButtonCell
    {
        new public static Class Class = new Class(typeof(NSMenuItemCell));

        NSMenuItem _menuItem;
        NSMenuView _menuView;

        /* If we belong to a popupbutton, we display image on the extreme
           right */
        bool _mcell_belongs_to_popupbutton;

        // Cache
        bool _needs_sizing;
        bool _needs_display;
        char[] _pad = new char[1];

        float _imageWidth;
        float _titleWidth;
        float _keyEquivalentWidth;
        float _stateImageWidth;
        float _menuItemHeight;

        NSImage _imageToDisplay;
        NSString _titleToDisplay;
        NSSize _imageSize;


        [ObjcPropAttribute("menuItem")]
        public virtual NSMenuItem MenuItem
        {
            get { return _menuItem; }
            set 
            {
                _menuItem = value;
                this.Enabled = _menuItem.Enabled;
            }

        }
         


        public NSMenuItemCell()
        {
            Init();
        }

        public override id Init()
        {
            id self = this;

            if (base.Init() == null)
                return null;

            this.ButtonType = NSButtonType.NSMomentaryLightButton;
            this.Alignment = NSTextAlignment.NSLeftTextAlignment;
            //this.Font = NSFont.MenuFontOfSize(0);
            //this.NeedsSizing = true;
            
            return self;
        }
       

        [ObjcMethodAttribute("InitWithCoder")]
        public override id InitWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (base.InitWithCoder(aDecoder) == null)
                return null;

            if (aDecoder.AllowsKeyedCoding)
            {
                //this.MenuItem = aDecoder.DecodeObjectForKey(@"NSMenuItem");
            }

            _needs_sizing = true;

            return self;
        }



//        - (id) initWithCoder: (NSCoder*)aDecoder
//{
//  self = [super initWithCoder: aDecoder];
//  if (nil == self)
//    return nil;

//  if ([aDecoder allowsKeyedCoding])
//    {
//      [self setMenuItem: [aDecoder decodeObjectForKey: @"NSMenuItem"]];
//    }
//  else
//    {
//      ASSIGN (_menuItem, [aDecoder decodeObject]);

//      if ([aDecoder versionForClassName: @"NSMenuItemCell"] < 2)
//        {
//          /* In version 1, we used to encode the _menuView here.  */
//          [aDecoder decodeObject];
//        }
//    }
//  _needs_sizing = YES;

//  return self;
//}
    }
}
