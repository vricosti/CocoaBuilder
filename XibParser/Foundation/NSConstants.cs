using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Smartmobili.Cocoa
{
    public static class NS
    {
        //externs
        public static NSString NibTopLevelObjects { get { return @"NSTopLevelObjects"; } }
        public static NSString NibOwner { get { return @"NSOwner"; } }

        public const uint NotFound = (int)Int32.MaxValue;

        public static void Log(NSString format, params object[] args)
        {
            NSLog.Log(format, args);
        }


        public static NSString StringFromClass(Class cls)
        {
            if (cls == null)
                return (NSString)"";

            return (NSString)cls.InnerType.ToString();
        }

        public static NSString Encode<T>()
        {
            return "";
        }

        public static bool IsBitSet(uint mask, uint flags)
        {
            return Convert.ToBoolean((mask & flags) != 0);
        }

        public static NSRect ZeroRect
        {
            get { return NSRect.Zero; }
        }

        public static double MinX(NSRect aRect)
        {
            return aRect.MinX;
        }

        public static double MinY(NSRect aRect)
        {
            return aRect.MinY;
        }

		public static double Width(NSRect aRect)
		{
			return aRect.Size.Width;
		}

		public static double Height(NSRect aRect)
		{
			return aRect.Size.Height;
		}

		public static NSRect OffsetRect(NSRect aRect, double dx, double dy)
		{
			return NSRect.OffsetRect (aRect, dx, dy);
		}

		private static bool almostEqual(double A, double B)
		{
			return (A == B);
		}

		public static bool EqualRects(NSRect aRect, NSRect bRect)
		{
			return (almostEqual(NS.MinX(aRect), NS.MinX(bRect))
			        && almostEqual(NS.MinY(aRect), NS.MinY(bRect))
			        && almostEqual(NS.Width(aRect), NS.Width(bRect))
			        && almostEqual(NS.Height(aRect), NS.Height(bRect))) ? true : false;
		}

		public static bool EqualSizes(NSSize aSize, NSSize bSize)
		{
			return (almostEqual(aSize.Width, bSize.Width)
			        && almostEqual(aSize.Height, bSize.Height)) ? true : false;
		}

		public static bool EqualPoints(NSPoint aPoint, NSPoint bPoint)
		{
			return (aPoint.X == bPoint.X) && (aPoint.Y == bPoint.Y) ? true : false;
		}


		public static NSPoint MakePoint(double x, double y)
		{
			NSPoint point = new NSPoint (x, y);
			return point;
		}

		public static NSSize MakeSize(double w, double h)
		{
			NSSize size = new NSSize (w, h);
			return size;
		}

		public static NSRect MakeRect(double x, double y, double w, double h)
		{
			NSRect rect = new NSRect(x,y,w,h);
			return rect;
		}
    }
    
    public static class GS
    {
        public static NSArray ObjCAllSubclassesOfClass(Class cls)
        {
            NSMutableArray result = null;
            if (cls == null)
            {
                return null;
            }
            else
            {
                result = (NSMutableArray)NSMutableArray.Array();
                //Assembly a = Assembly.GetExecutingAssembly();
                //foreach (Type t in a.GetTypes())
                //{
                //    if (t.IsSubclassOf(cls.InnerType))
                //    {
                //        //System.Diagnostics.Debug.WriteLine("dqsd");
                //        result.AddObject(new Class(t));
                //    }
                //}

                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (a.FullName.StartsWith("Smartmobili"))
                    {
                        foreach (Type t in a.GetTypes())
                        {
                            if (t.IsSubclassOf(cls.InnerType))
                            {
                                //System.Diagnostics.Debug.WriteLine("dqsd");
                                result.AddObject(new Class(t));
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
