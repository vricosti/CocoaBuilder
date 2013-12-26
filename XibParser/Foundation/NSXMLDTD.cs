using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSXMLDTD : NSXMLNode
    {
        new public static Class Class = new Class(typeof(NSXMLDTD));
        new public static NSXMLDTD alloc() { return new NSXMLDTD(); }

        protected NSString _name;
        protected NSString _publicID;
        protected NSString _systemID;
        protected NSXMLChildren _children;
        protected bool _childrenHaveMutated;
        protected NSMutableDictionary _entities;
        protected NSMutableDictionary _elements;
        protected NSMutableDictionary _notations;
        protected NSMutableDictionary _attributes;
        protected NSString _original;
        protected bool _modified;


//        function methImpl_NSXMLDTDNode_setSystemID_ {
//    if (rdi._systemID == rdx) goto loc_1bc3cf;
//    goto loc_1bc377;

//loc_1bc3cf:
//    return rax;

//loc_1bc377:
//    (*objc_msg_release)();
//    rax = [r14 copy];
//    rax = objc_assign_ivar(rax, rbx, *_OBJC_IVAR_$_NSXMLDTDNode._systemID);
//    if (rbx._parent == 0x0) goto loc_1bc3cf;
//    rax = [rdi _setModified:0x1];
//}

        protected virtual void _setModified(bool modified)
        {
            _modified = modified;
        }

        public virtual void setSystemID(NSString systemID)
        {
            if (_systemID != systemID)
            { 
                _systemID = systemID;
                if(_parent != null)
                {
                    this._setModified(true);
                }
            }
        }

        public virtual NSString systemID()
        {
            return _systemID;
        }

        public virtual void setPublicID(NSString publicID)
        {
            _publicID = publicID;
        }

        public virtual NSString publicID()
        {
            return _publicID;
        }


    }
}
