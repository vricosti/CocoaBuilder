using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public enum NSComparisonResult : int
    {
        NSOrderedAscending = -1,
        NSOrderedSame,
        NSOrderedDescending
    }

    //https://github.com/gnustep/gnustep-gui/blob/master/Headers/Additions/GNUstepGUI/GSModelLoaderFactory.h
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/GSModelLoaderFactory.m
    public class GSModelLoader : NSObject
    {
        new public static Class Class = new Class(typeof(GSModelLoader));

        public static NSString Type
        {
            get { return null; }
        }

        public static float Priority
        {
            get { return 0; }
        }

        public virtual bool LoadModelData(NSData data, NSDictionary context)
        {
            throw new NotImplementedException();
            //[NSException raise: NSInternalInconsistencyException format: @"Abstract model loader."];
            return false;
        }

        public virtual bool LoadModelFile(NSString fileName, NSDictionary context)
        {
            NSData data = DataForFile(fileName);

            if (data != null)
            {
                bool loaded = LoadModelData(data, context);

                if (!loaded)
                    System.Diagnostics.Debug.WriteLine(string.Format("Could not load Nib file: %@", fileName));
                return loaded;
            }
            else
            {
                return false;
            }
        }

        public virtual NSData DataForFile(NSString fileName)
        {
            return NSData.DataWithContentsOfFile(fileName);
        }







    }
}
