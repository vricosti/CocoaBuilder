using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBMemberID : NSObject
    {
        new public static Class Class = new Class(typeof(IBMemberID));
        new public static IBMemberID alloc() { return new IBMemberID(); }

        protected NSString _memberIdentifier;
        protected int _retainCountMinusOne;
    }
}
