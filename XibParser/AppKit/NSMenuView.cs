using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSMenuView : NSView, INSMenuView
    {
        new public static Class Class = new Class(typeof(NSMenuView));

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
