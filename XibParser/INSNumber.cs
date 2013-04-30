using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public interface INSNumber
    {
        double DoubleValue { get; }
        float FloatValue { get; }
        int IntValue { get; }
        int IntegerValue { get; }
        bool BoolValue { get; }
    }
}
