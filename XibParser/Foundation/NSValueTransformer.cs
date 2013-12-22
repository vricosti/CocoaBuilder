using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSValueTransformer : NSObject
    {
        new public static Class Class = new Class(typeof(NSValueTransformer));
        new public static NSValueTransformer alloc() { return new NSValueTransformer(); }

        private static object __transformerRegistryLock = new Object();

        private static NSMutableDictionary __transformerRegistry;

        private static NSArray __transformerNames;
        public static NSMutableDictionary _transformerRegistry()
        {
            if (__transformerRegistry == null)
            {
                __transformerRegistry = (NSMutableDictionary)NSMutableDictionary.alloc().initWithCapacity(8);
                __transformerRegistry.setObjectForKey(_NSNegateBooleanTransformer.alloc().init(), (NSString)"NSNegateBoolean");
                __transformerRegistry.setObjectForKey(_NSIsNilTransformer.alloc().init(), (NSString)"NSIsNil");
                __transformerRegistry.setObjectForKey(_NSIsNotNilTransformer.alloc().init(), (NSString)"NSIsNotNil");
                __transformerRegistry.setObjectForKey(_NSUnarchiveFromDataTransformer.alloc().init(), (NSString)"NSUnarchiveFromData");
                __transformerRegistry.setObjectForKey(_NSKeyedUnarchiveFromDataTransformer.alloc().init(), (NSString)"NSKeyedUnarchiveFromData");
            }

            return __transformerRegistry;
        }

        public static void setValueTransformerForName(NSValueTransformer transformer, NSString name)
        {
            if (name != null)
            {
                lock (__transformerRegistryLock)
                {
                    NSMutableDictionary registry = _transformerRegistry();
                    if (transformer != null)
                        registry.setObjectForKey(transformer, name);
                    else
                        registry.removeObjectForKey(name);

                    __transformerNames = null;
                }
            }
            else
            {
                NSException.raise("NSInvalidArgumentException", "Name cannot be nil");
            }
        }

        public static Class transformedValueClass()
        {
            return null;
        }
        
        public static NSValueTransformer valueTransformerForName(NSString name)
        {
            NSValueTransformer transformer = null;

            lock (__transformerRegistryLock)
            {
                if (name != null)
                {
                    transformer = (NSValueTransformer)_transformerRegistry().objectForKey(name);
                }
            }

            if (transformer != null)
            {
                transformer = (NSValueTransformer)transformer.retain().autorelease();
            }
            else
            {
                Class cls = Class.NSClassFromString(name);
                if (cls != null)
                {
                    transformer = cls.alloc().init() as NSValueTransformer;
                    if (transformer != null)
                    {
                        Objc.MsgSend(transformer, "setValueTransformerForName", transformer, name);
                    }
                }
            }

            return transformer;
        }

        public static NSArray valueTransformerForNames()
        {
            lock (__transformerRegistryLock)
            {
                if (__transformerNames == null)
                {
                    __transformerNames = (NSArray)NSValueTransformer._transformerRegistry().allKeys();
                }
            }

            return __transformerNames;
        }


        public static bool allowsReverseTransformation()
        {
            return true;
        }


        public virtual id transformedValue(id value)
        {
            return value;
        }

        public virtual id reverseTransformedValue(id value)
        {
            if (allowsReverseTransformation() == false)
            {
                NSException.raise("NSInternalInconsistencyException", "Transformer does not support reverse transformations");
            }

            return this.transformedValue(value);
        }

    }
}
