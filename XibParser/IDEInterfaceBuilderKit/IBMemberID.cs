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


        //void IBEncodeMemberID(NSCoder *__strong, IBMemberID *__strong, NSString *__strong, NSString *__strong, IBMemberIDEncodingStrategy)
        //"/SourceCache/IDEInterfaceBuilder/IDEInterfaceBuilder-4514/Framework/Document/IBMemberID.m"
        public static void IBEncodeMemberID(NSCoder aCoder, IBMemberID aMemberID, NSString aString, NSString aString2, int aIBMemberStrategy)
        {

        }

        //"IBMemberID *IBDecodeMemberID(NSCoder *__strong, NSString *__strong, NSString *__strong)"
        public static IBMemberID IBDecodeMemberID(NSCoder aDecoder, NSString aString, NSString aString2)
        {
            return null;
        }
    }
}
