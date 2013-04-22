using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
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

        public NSMenuView()
        {

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
