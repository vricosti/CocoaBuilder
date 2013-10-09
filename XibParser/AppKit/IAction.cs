using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public interface IAction
    {
        SEL Action
        {
            get;
            set;
        }

        id Target
        {
            get;
            set;
        }
    }
}
