﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBDocumentUndoManager : NSObject
    {
        new public static Class Class = new Class(typeof(IBDocumentUndoManager));
        new public static IBDocumentUndoManager alloc() { return new IBDocumentUndoManager(); }


    }
}
