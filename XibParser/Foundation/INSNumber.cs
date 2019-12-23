using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public interface INSNumber
    {
        double doubleValue();
        float floatValue();
        int intValue();
        int integerValue();
        bool boolValue();

        uint unsignedIntegerValue();
    }
}
