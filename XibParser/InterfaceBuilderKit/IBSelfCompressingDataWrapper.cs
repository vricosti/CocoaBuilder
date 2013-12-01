using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class IBSelfCompressingDataWrapper : NSObject
    {
        new public static Class Class = new Class(typeof(IBSelfCompressingDataWrapper));
        new public static IBSelfCompressingDataWrapper Alloc() { return new IBSelfCompressingDataWrapper(); }

        protected NSData _originalData;
        protected NSData _compressedData;

        //IBSelfCompressingDataWrapper_initWithData_shouldCompress_
        public virtual id InitWithData(NSData data, bool shouldCompress)
        {
            id self = this;

            _originalData = data;

            return self;
        }


//        function meth_IBSelfCompressingDataWrapper_initWithData_shouldCompress_ {
//    var_48 = arg_0;
//    var_52 = *0x24c484;
//    eax = [[&var_48 super] init];
//    if (eax != 0x0) {
//            [esi setOriginalData:arg_8];
//            if (arg_C != 0x0) {
//                    eax = dispatch_get_global_queue(0xfffffffffffffffe, 0x0);
//                    var_24 = *imp___nl_symbol_ptr___NSConcreteStackBlock;
//                    var_28 = 0xffffffffc2000000;
//                    var_32 = 0x0;
//                    var_36 = ___60-[IBSelfCompressingDataWrapper initWithData:shouldCompress:]_block_invoke;
//                    var_40 = ___block_descriptor_tmp_23c540;
//                    var_44 = esi;
//                    dispatch_async(eax, &var_24);
//            }
//    }
//    eax = esi;
//    return eax;
//}



            //__IBSelfCompressingDataWrapper_initWithData_shouldCompress__ __text 00000000001E3790 000000B2 R . . . B . .
//        __IBSelfCompressingDataWrapper_dealloc_            __text 00000000001E3901 00000063 R . . . B . .
//__IBSelfCompressingDataWrapper_data_               __text 00000000001E3964 00000048 R . . . B . .
//__IBSelfCompressingDataWrapper_originalData_       __text 00000000001E39AC 0000002D R . . . B . .
//__IBSelfCompressingDataWrapper_setOriginalData__   __text 00000000001E39D9 0000003C R . . . B . .
//__IBSelfCompressingDataWrapper_compressedData_     __text 00000000001E3A15 0000002D R . . . B . .
//__IBSelfCompressingDataWrapper_setCompressedData__ __text 00000000001E3A42 0000003C R . . . B . .



    }
}
