using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBAsyncXMLDecoderWrapper : NSObject
    {
        new public static Class Class = new Class(typeof(IBAsyncXMLDecoderWrapper));
        new public static IBAsyncXMLDecoderWrapper alloc() { return new IBAsyncXMLDecoderWrapper(); }
    }
}
