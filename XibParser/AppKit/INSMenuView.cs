using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public interface NSMenuView
    {
        NSMenu Menu { set; }

        int HighlightedItemIndex { get; set; }

        void DetachSubmenu();

        void Update();

        void SizeToFit();

        float StateImageWidth { get; }

        float ImageAndTitleOffset { get; }

        float ImageAndTitleWidth { get; }

        float KeyEquivalentOffset { get; }

        float KeyEquivalentWidth { get; }

        NSPoint LocationForSubmenu(NSMenu aSubmenu);

        void PerformActionWithHighlightingForItemAtIndex(int anIndex);

        bool TrackWithEvent(NSEvent anEvent);
    }
}
