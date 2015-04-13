using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class DVTFilePath : NSObject
    {
        new public static Class Class = new Class(typeof(DVTFilePath));
        new public static DVTFilePath alloc() { return new DVTFilePath(); }

        protected DVTFilePath _parentPath;
        //struct fastsimplearray *_childfsaPaths;
        protected DVTFileSystemVNode _vnode;
        protected Int64 _numAssociates;
        protected Int64 _numObservers;
        protected id _associates;
        protected NSString _pathString;
        protected NSURL _fileURL;
        protected bool _hasResolvedVnode;
        protected bool _cleanRemoveFromParent;
        protected bool _validationState;
        protected UInt16 _fsrepLength;
        protected int _childPathsLock;
        protected int _associatesLock;
        protected char _fsrep;

        //+ (id)filePathForFileURL:(id)arg1;
        public static DVTFilePath filePathForFileURL(id url)
        {
            DVTFilePath dvtFilePath = null;

            if (url != null)
            {
                if (url.isKindOfClass(NSURL.Class))
                {
                    if (((NSURL)url).isFileURL())
                    {
                        dvtFilePath = DVTFilePath.filePathForPathString(((NSURL)url).path());
                    }
                    else
                    {
                        System.Diagnostics.Trace.Assert(false, "[url isFileURL]");
                    }
                }
                else
                {
                    System.Diagnostics.Trace.Assert(false, "[(id)(url) isKindOfClass:[NSURL class]]");
                }
            }
            else
            {
                System.Diagnostics.Trace.Assert(false, "(url) != nil");
            }

            return dvtFilePath;
        }

        public static DVTFilePath filePathForPathString(id pathString)
        {
            DVTFilePath dvtFilePath = null;

            if (pathString != null)
            {
                if (pathString.isKindOfClass(NSString.Class))
                {
                     dvtFilePath = DVTFilePath._filePathForParent(null, (NSString)pathString);
                }
                else
                {
                    System.Diagnostics.Trace.Assert(false, "[(id)(str) isKindOfClass:[NSString class]]");
                }
            }
            else 
            {
                System.Diagnostics.Trace.Assert(false, "(str) != nil");
            }


            return dvtFilePath;
        }

        

        public static DVTFilePath _filePathForParent(id parent, NSString pathString)
        {
            DVTFilePath dvtFilePath = null;

            return dvtFilePath;
        }

//        function methImpl_static_DVTFilePath_filePathForPathString_ {
//    var_40 = rdi;
//    var_32 = rsi;
//    rax = [rdx retain];
//    if (rax != 0x0) {
//            rax = [*0x4c5710 class];
//            rax = [r12 isKindOfClass:rax];
//            if (rax == 0x0) {
//                    rax = [*0x4c5710 class];
//                    rax = NSStringFromClass(rax);
//                    rax = [rax retain];
//                    r14 = rax;
//                    rax = [r12 descriptionForAssertionMessage];
//                    rax = [rax retain];
//                    r15 = rax;
//                    __DVTAssertionFailureHandler(&var_40, &var_32, "+[DVTFilePath filePathForPathString:]", "/SourceCache/DVTFrameworks/DVTFrameworks-6760/DVTFoundation/FilePaths/DVTFilePath.m", 0x36e, @"[(id)(str) isKindOfClass:[NSString class]]");
//                    rbx = *objc_release;
//                    [r15 release];
//                    [r14 release];
//            }
//    }
//    else {
//            rax = [*0x4c5710 class];
//            rax = NSStringFromClass(rax);
//            rax = [rax retain];
//            r14 = rax;
//            __DVTAssertionFailureHandler(&var_40, &var_32, "+[DVTFilePath filePathForPathString:]", "/SourceCache/DVTFrameworks/DVTFrameworks-6760/DVTFoundation/FilePaths/DVTFilePath.m", 0x36e, @"(str) != nil");
//            [r14 release];
//    }
//    rax = [var_40 _filePathForParent:0x0 pathString:r12];
//    rax = [rax retain];
//    rbx = rax;
//    [r12 release];
//    rdi = rbx;
//    rax = [rdi autorelease];
//    return rax;
//}

    }
}
