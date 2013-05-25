/*
* XibParser.
* Copyright (C) 2013 Smartmobili SARL
* 
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License as published by the Free Software Foundation; either
* version 2 of the License, or (at your option) any later version.
* 
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
* 
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, write to the
* Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
* Boston, MA  02110-1301, USA. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Smartmobili.Cocoa
{
    public struct NSRange
    {
        public uint Location { get; set; }
        public uint Length { get; set; }

        public NSRange(uint location, uint length)
            : this()
        {
            Location = location;
            Length = length;
        }
    }

    public struct NSSize
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public NSSize(double w, double h)
            : this() 
        {
            Width = w;
            Height = h;    
        }

        public static NSSize Create(XElement xElement)
        {
            return Create(xElement.Value);
        }

        public static NSSize Create(string nsSizeText)
        {
            NSSize nsSize = new NSSize();

            //[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?
            //[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?
            //var matches = Regex.Match(nsSizeText, @"{([-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?),\s*([-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?)}");
            //var matches = Regex.Match(nsSizeText, @"{([+-]?\d+[eE][-+]?\d+),\s*([+-]?\d+[eE][-+]?\d+)}");
            //if (matches.Groups.Count != 3)
            //    throw new Exception("Invalid xElement for NSSize");
            
            //var culture = CultureInfo.CreateSpecificCulture("en-US");
            //nsPoint.Width = Single.Parse(matches.Groups[1].Value, NumberStyles.AllowExponent, culture);
            //nsPoint.Height = Single.Parse(matches.Groups[2].Value, NumberStyles.AllowExponent, culture);



            var parts = nsSizeText.Split(',').ToList();
            if (parts.Count != 2)
                throw new Exception("Invalid format for NSSize");

            string w = parts[0].TrimStart('{').Trim();
            string h = parts[1].TrimEnd('}').Trim();

            var culture = CultureInfo.CreateSpecificCulture("en-US");
            nsSize.Width = Single.Parse(w, NumberStyles.AllowExponent, culture);
            nsSize.Height = Single.Parse(h, NumberStyles.AllowExponent, culture);

            return nsSize;
        }

        public static implicit operator NSRect(NSSize size)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSSize: implicit conversion to NSRect occurred");
#endif
            return new NSRect(new NSPoint(0, 0), size);
        }

        public static implicit operator NSPoint(NSSize size)
        {
#if TRACE_IMPLICIT_OPERATOR
            System.Console.WriteLine("NSSize: implicit conversion to NSPoint occurred");
#endif
            return new NSPoint(size.Width, size.Height);
        }
    }

    public struct NSPoint
    {
        public static readonly NSPoint Zero = new NSPoint(0, 0);

        public double X { get; set; }
        public double Y { get; set; }

        public NSPoint(double p1, double p2)
            : this()
        {
            X = p1;
            Y = p2;
        }

        public static NSPoint Create(XElement xElement)
        {
            return Create(xElement.Value);
        }

        public static NSPoint Create(string nsPointText)
        {
            NSPoint nsPoint = new NSPoint();

            var parts = nsPointText.Split(',').ToList();
            if (parts.Count != 2)
                throw new Exception("Invalid format for NSPoint");

            string x = parts[0].TrimStart('{').Trim();
            string y = parts[1].TrimEnd('}').Trim();

            var culture = CultureInfo.CreateSpecificCulture("en-US");
            nsPoint.X = Single.Parse(x, NumberStyles.AllowExponent, culture);
            nsPoint.Y = Single.Parse(y, NumberStyles.AllowExponent, culture);

            return nsPoint;
        }
    }

    
    public struct NSRect
    {
        public static readonly NSRect Zero = new NSRect(0, 0, 0, 0);

        public NSPoint Origin { get; set; }
        public NSSize Size { get; set; }

        public NSRect(double x, double y, double w, double h)
            : this()
        {
            Origin = new NSPoint(x, y);
            Size = new NSSize(w, h);
        }

        public NSRect(NSPoint origin, NSSize size) : this()
        {
            Origin = origin;
            Size = size;
        }

        public static NSRect Make(double x, double y, double w, double h)
        {
            return new NSRect(x, y, w, h);
        }

        public bool IsEmpty
        {
            get { return ((this.Size.Width > 0) && (this.Size.Height > 0)) ? false : true; }
        }

        public double MinX
        {
            get { return this.Origin.X; }
        }

        public double MinY
        {
            get { return this.Origin.Y; }
        }

        public double MaxX
        {
            get { return this.Origin.X + this.Size.Width; }
        }

        public double MaxY
        {
            get { return this.Origin.Y + this.Size.Height; }
        }

        //<string key="NSScreenRect">{{0, 0}, {2560, 1418}}</string>
        public static NSRect Create(XElement xElement)
        {
            return NSRect.Create(xElement.Value);
        }

        public static NSRect Create(string nsRectDesc)
        {
            NSRect nsRect = new NSRect();

            //var matches = Regex.Match(xElement.Value, @"{({\d+,\s*\d+}),\s*({\d+,\s*\d+})}");
            var matches = Regex.Match(nsRectDesc, @"{{([+-]?\d+),\s*([+-]?\d+)},\s*{([+-]?\d+),\s*([+-]?\d+)}}");
            if (matches.Groups.Count != 5)
                throw new Exception("Invalid xElement for NSRect");

            float x = Single.Parse(matches.Groups[1].Value);
            float y = Single.Parse(matches.Groups[2].Value);
            float width = Single.Parse(matches.Groups[3].Value);
            float height = Single.Parse(matches.Groups[4].Value);

            nsRect = new NSRect(x, y, width, height);

            return nsRect;
        }

        //Returns the smallest rectangle that completely encloses both aRect and bRect. 
        //If one of the rectangles has 0 (or negative) width or height, a copy of the other rectangle is returned; 
        //but if both have 0 (or negative) width or height, the returned rectangle has its origin at (0.0, 0.0) and has 0 width and height.
        public static NSRect Union(NSRect aRect, NSRect bRect)
        {
            NSRect rect = new NSRect();

            if (aRect.IsEmpty && bRect.IsEmpty)
                return NSRect.Make(0.0, 0.0, 0.0, 0.0);
            else if (aRect.IsEmpty)
                return bRect;
            else if (bRect.IsEmpty)
                return aRect;

            rect = NSRect.Make(Math.Min(aRect.MinX, bRect.MinX), Math.Min(aRect.MinY, bRect.MinY), 0.0, 0.0);

            rect = NSRect.Make(rect.MinX,
                               rect.MinY,
                               Math.Max(aRect.MaxX, bRect.MaxX) - rect.MinX,
                               Math.Max(aRect.MaxY, bRect.MaxY) - rect.MinY);
            return rect;
        }
    };

}
