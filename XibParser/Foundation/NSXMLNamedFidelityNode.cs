using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLNamedFidelityNode : NSXMLNamedNode
    {
        new public static Class Class = new Class(typeof(NSXMLNamedFidelityNode));
        new public static NSXMLNamedFidelityNode alloc() { return new NSXMLNamedFidelityNode(); }


        protected NSString _whitespace;

        protected NSArray _ranges;

        protected NSArray _names;

        protected uint _fidelity;


        public virtual NSString _XMLStringWithOptionAppendingToString(uint options, NSString str)
        {
            if (name() == null)
                return null;

            
            return null;
        }


        public virtual uint fidelity()
        {
            return _fidelity;
        }

        public virtual void setFidelity(uint fidelity)
        {
            _fidelity = fidelity;
        }


        //        function methImpl_NSXMLNamedFidelityNode__XMLStringWithOptions_appendingToString_ {
        //    r14 = rcx;
        //    r15 = rdx;
        //    rbx = rdi;
        //    rax = [rdi name];
        //    if (rax == 0x0) goto loc_1d0320;
        //    goto loc_1cff7e;

        //loc_1d0320:
        //    return rax;

        //loc_1cff7e:
        //    rdx = 0x27;
        //    rcx = 0x22;
        //    if ((rbx._fidelity & 0x8) >> 0x3 != 0x0) {
        //            rcx = 0x27;
        //    }
        //    var_110 = rcx;
        //    var_106 = 0x3d;
        //    var_108 = rcx;
        //    rax = [rbx kind];
        //    if (rax == 0x3) goto loc_1d002d;
        //    goto loc_1cffc0;

        //loc_1d002d:
        //    [r14 appendString:r12];
        //    r15 = r15 & 0x200;
        //    if (r15 == 0x0) goto loc_1d0068;
        //    goto loc_1d0049;

        //loc_1d0068:
        //    [r14 appendCharacters:&var_106 length:0x2];
        //    rax = rbx._ranges;
        //    if (r15 != 0x0) {
        //            if (rax != 0x0) {
        //                    var_72 = rbx;
        //                    var_80 = *0x37e278;
        //                    rax = [[&var_72 super] objectValue];
        //                    r12 = *objc_msgSend;
        //                    rax = [NSMutableString stringWithString:rax];
        //                    rbx = rax;
        //                    r15 = objc_msg_length;
        //                    rax = [rbx length];
        //                    var_56 = 0x0;
        //                    var_64 = rax;
        //                    [rbx replaceOccurrencesOfString:@"&amp;{" withString:@"&{" options:0x2 range:r9];
        //                    rax = [rbx length];
        //                    var_40 = 0x0;
        //                    var_48 = rax;
        //                    [rbx replaceOccurrencesOfString:@"&lt;" withString:@"<" options:0x2 range:r9];
        //                    [r14 appendString:rbx];
        //            }
        //            else {
        //                    r12 = *objc_msgSend;
        //                    rax = [rbx stringValue];
        //                    [NSXMLNode _escapeHTMLAttributeCharacters:rax withQuote:var_110 & 0xffff appendingToString:r14];
        //            }
        //    }
        //    else {
        //            if (rax != 0x0) {
        //                    var_24 = rbx;
        //                    var_32 = *0x37e278;
        //                    rax = [[&var_24 super] objectValue];
        //                    [r14 appendString:rax];
        //            }
        //            else {
        //                    var_18 = 0x26003c;
        //                    var_22 = var_110;
        //                    r12 = *objc_msgSend;
        //                    rax = [rbx stringValue];
        //                    [NSXMLNode _escapeCharacters:&var_18 countOfCharacters:0x3 inString:rax appendingToString:r14];
        //            }
        //    }
        //    rax = [r14 appendCharacters:&var_110 length:0x1];
        //    goto loc_1d0320;

        //loc_1d0049:
        //    rax = [NSXMLContext isSingleAttribute:r12];
        //    if (rax != 0x0) goto loc_1d0320;
        //    goto loc_1d0068;

        //loc_1cffc0:
        //    if (rax == 0x4) {
        //            if ((rbx._whitespace != 0x0) && ((r15 & 0x20000) == 0x0)) {
        //                    [r14 appendString:rdx];
        //            }
        //            rax = [r12 length];
        //            if (rax != 0x0) {
        //                    rcx = r12;
        //                    [r14 appendFormat:@"xmlns:%@"];
        //            }
        //            else {
        //                    [r14 appendString:@"xmlns"];
        //            }
        //            r12 = *objc_msgSend;
        //            [r14 appendCharacters:&var_106 length:0x2];
        //            rax = [rbx stringValue];
        //            rax = [NSXMLNode _escapeCharacters:&var_110 countOfCharacters:0x1 inString:rax appendingToString:r14];
        //    }
        //    else {
        //            if (rax == 0x5) {
        //                    if ((rbx._whitespace != 0x0) && ((r15 & 0x20000) == 0x0)) {
        //                            [r14 appendString:rdx];
        //                    }
        //                    var_88 = rbx;
        //                    var_96 = *0x37e278;
        //                    rax = [[&var_88 super] _XMLStringWithOptions:r15 appendingToString:r14];
        //            }
        //    }
        //    goto loc_1d0320;
        //}

    }
}
