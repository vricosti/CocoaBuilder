using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSMenuView.m
    public class NSMenuView : NSView, INSMenuView
    {
        new public static Class Class = new Class(typeof(NSMenuView));

        protected NSMutableArray _itemCells;
        protected bool _horizontal;
        protected char[] _pad1 = new char[3];
        protected NSFont _font;
        protected int _highlightedItemIndex;
        protected float _horizontalEdgePad;
        protected float _stateImageOffset;
        protected float _stateImageWidth;
        protected float _imageAndTitleOffset;
        protected float _imageAndTitleWidth;
        protected float _keyEqOffset;
        protected float _keyEqWidth;
        protected bool _needsSizing;
        protected char[] _pad2 = new char[3];
        protected NSSize _cellSize;

        private id _items_link;
        private int _leftBorderOffset;
        private id _titleView;

        /*
        Private and not named '_menu' to avoid confusion and further problems
        with NSResponder's menu.
        */
        private NSMenu _attachedMenu;


        public static NSMenuView Alloc()
        {
            return new NSMenuView();
        }

        public NSMenuView()
        {

        }


        public id InitWithFrame(NSRect aFrame)
        {
            id self = this;

            //self = [super initWithFrame: aFrame];
            //if (!self)
            //  return nil;

            //[self setFont: [NSFont menuFontOfSize: 0.0]];

            _highlightedItemIndex = -1;
            _horizontalEdgePad = 4.0f;

            /* Set the necessary offset for the menuView. That is, how many pixels 
             * do we need for our left side border line.
             */
            _leftBorderOffset = 1;

            // Create an array to store our menu item cells.
            _itemCells = (NSMutableArray)NSMutableArray.Alloc().Init();

            return self;
        }


         private bool _RootIsHorizontal(ref bool isAppMenu)
        {
            return false;
            //NSMenu m = _attachedMenu;

            ///* Determine root menu of this menu hierarchy */
            //while (m.SuperMenu != null)
            //  {
            //    m = m.SuperMenu;
            //  }

            //if (isAppMenu != false)
            //  {
            //    if (m == [NSApp mainMenu])
            //      {
            //        isAppMenu = true;
            //      }
            //    else
            //      {
            //        isAppMenu = false;
            //      }
            //  }
            //return [[m menuRepresentation] isHorizontal];
        }


        public new NSMenu Menu
        {
            set { throw new NotImplementedException(); }
        }

        public int HighlightedItemIndex
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void DetachSubmenu()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void SizeToFit()
        {
            throw new NotImplementedException();
        }

        public float StateImageWidth
        {
            get { throw new NotImplementedException(); }
        }

        public float ImageAndTitleOffset
        {
            get { throw new NotImplementedException(); }
        }

        public float ImageAndTitleWidth
        {
            get { throw new NotImplementedException(); }
        }

        public float KeyEquivalentOffset
        {
            get { throw new NotImplementedException(); }
        }

        public float KeyEquivalentWidth
        {
            get { throw new NotImplementedException(); }
        }

        public NSPoint LocationForSubmenu(NSMenu aSubmenu)
        {
            throw new NotImplementedException();
        }

        public void PerformActionWithHighlightingForItemAtIndex(int anIndex)
        {
            throw new NotImplementedException();
        }

        public bool TrackWithEvent(NSEvent anEvent)
        {
            throw new NotImplementedException();
        }
    }



}
