using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    public class IBObjectRecord : NSObject
    {
        new public static Class Class = new Class(typeof(IBObjectRecord));
        new public static IBObjectRecord alloc() { return new IBObjectRecord(); }

        protected IBMemberID _objectID;
        protected NSString _explicitLabel;
        protected id _parent;
        protected id _object;
        protected NSMutableArray _children;
        protected IBGroup _group;
        protected IBObjectRecord _parentRecord;

        //@property(retain) IBObjectRecord *parentRecord; // @synthesize parentRecord;
        public virtual IBObjectRecord parentRecord() { return _parentRecord; }
        public virtual void setParentRecord(IBObjectRecord parentRecord) { _parentRecord = parentRecord; }

        //@property(nonatomic) __weak IBGroup *group; // @synthesize group;
        public virtual IBGroup group() { return _group; }
        public virtual void setGroup(IBGroup group) { _group = group; }

        //@property(copy) NSString *explicitLabel; // @synthesize explicitLabel;
        public virtual NSString explicitLabel() { return _explicitLabel; }

        //@property(readonly) id object; // @synthesize object;
        public virtual id getObject() { return _object; }

        //@property(retain) id parent; // @synthesize parent;
        public virtual id parent() { return _parent; }
        public virtual void setParent(id parent) { _parent = parent; }

        //@property(retain) IBMemberID *objectID; // @synthesize objectID;
        public virtual IBMemberID objectID() { return _objectID; }
        public virtual void setObjectID(IBMemberID objectID) { _objectID = objectID; }

        public override NSString description()
        {
            return NSString.stringWithFormat("<%@:%p Object=<%@:%p ibEffectiveLabel=%@>>",
                "IBObjectRecord", 0x0, getObject().description(), 0x0, explicitLabel());
        }

        public virtual NSArray children()
        {
            NSArray theChildren = null;

            if (_children == null)
            {
                theChildren = (NSArray)NSMutableArray.array().retain();
            }
            else
            {
                theChildren = (NSArray)_children.retain();
            }

            return theChildren;
        }


        public virtual id initWithObject(id anObject)
        {
            id self = this;

            if (base.init() == null)
                return null;

            _object = anObject;

            return self;
        }


        public override void encodeWithCoder(NSCoder aCoder)
        {
            if (aCoder.retain<NSCoder>().allowsKeyedCoding() == false)
            {
                throw new InvalidOperationException("-[IBObjectRecord encodeWithCoder:] : [coder allowsKeyedCoding]");
            }

            IBMemberID.IBEncodeMemberID(aCoder, this._objectID, "objectID", "id", 0x0);
            aCoder.encodeObjectForKey(_object, "object");
            if (_children != null)
            {
                aCoder.encodeObjectForKey(this._children, "children");
            }
            aCoder.encodeObjectForKey(this._parent, "parent");
            if (this._explicitLabel.length() != 0)
            {
                aCoder.encodeObjectForKey(this._explicitLabel, "objectName");
            }
        }

        public override id initWithCoder(NSCoder aDecoder)
        {
            id self = this;

            if (aDecoder.retain<NSCoder>().allowsKeyedCoding() == false)
            {
                throw new InvalidOperationException("-[IBObjectRecord initWithCoder:] : [coder allowsKeyedCoding]");
            }

            if (base.initWithCoder(aDecoder) == null)
                return null;

            _objectID = (IBMemberID)IBMemberID.IBDecodeMemberID(aDecoder, @"objectID", @"id").retain();
            _object = (id)aDecoder.decodeObjectForKey("object").retain();
            _children = (NSMutableArray)aDecoder.decodeObjectForKey("children").retain();
            _parent = (id)aDecoder.decodeObjectForKey("parent").retain();
            _explicitLabel = (NSString)aDecoder.decodeObjectForKey("objectName").retain();


            return self;
        }

        public virtual void insertChild(id anObject, uint anIndex)
        {
            if (_children == null)
            {
                _children = (NSMutableArray)NSMutableArray.alloc().init();
            }
            _children.insertObject(anObject, anIndex);
        }

        public virtual void addChild(id anObject)
        {
            _children.insertObject(anObject, _children.count());
        }

        public virtual void removeChild(id anObject)
        {
            _children.removeObject(anObject);
        }

        public virtual void moveChild(id anObject, uint anIndex)
        {
            uint index = _children.indexOfObject(anObject);
            _children.removeObjectAtIndex(index);
            _children.insertObject(anObject, anIndex);
        }

        public virtual void verifyNoDuplicateChildren()
        {
            throw new NotImplementedException("verifyNoDuplicateChildren");
        }


        public virtual bool isDescendantOfObjectRecord(id anObject)
        {
            return false;
        }

    }


//- (void).cxx_destruct;
//- (void)verifyNoDuplicateChildren;
//- (BOOL)isDescendantOfObjectRecord:(id)arg1;
//- (id)description;
//- (id)children;
//- (void)removeChild:(id)arg1;
//- (void)addChild:(id)arg1;
//- (void)insertChild:(id)arg1 atIndex:(long long)arg2;
//- (void)moveChild:(id)arg1 toIndex:(long long)arg2;
//- (void)encodeWithCoder:(id)arg1;
//- (id)initWithCoder:(id)arg1;
//- (id)initWithObject:(id)arg1;
}
