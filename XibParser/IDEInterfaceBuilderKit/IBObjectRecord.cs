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

        IBMemberID _objectID;
        NSString _explicitLabel;
        id _parent;
        id _object;
        NSMutableArray _children;
        IBGroup _group;
        IBObjectRecord _parentRecord;
    }

//@property(retain) IBObjectRecord *parentRecord; // @synthesize parentRecord;
//@property(nonatomic) __weak IBGroup *group; // @synthesize group;
//@property(copy) NSString *explicitLabel; // @synthesize explicitLabel;
//@property(readonly) id object; // @synthesize object;
//@property(retain) id parent; // @synthesize parent;
//@property(retain) IBMemberID *objectID; // @synthesize objectID;
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
