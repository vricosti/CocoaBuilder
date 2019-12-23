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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Smartmobili.Cocoa
{
    public class IBArchive
    {
        public string Type { get; set; }

        public string Version { get; set; }

        public IBDocument Data { get; set; }

        public IBArchive()
        {
            this.Type = "com.apple.InterfaceBuilder3.Cocoa.XIB";
            this.Version = "7.10";
            this.Data = new IBDocument();
        }

        public static IBArchive Parse(XDocument xDoc)
        {
            IBArchive ibArchive = new IBArchive();

            foreach (XAttribute attr in xDoc.Root.Attributes())
            {
                if (attr.Name == "type")
                    ibArchive.Type = attr.Value;
                if (attr.Name == "version")
                    ibArchive.Version = attr.Value;
            }

            var elements = xDoc.XPathSelectElements(@"/archive/data/*");
            foreach (var element in elements)
            {
                var key = element.Attributes().Where(attr => attr.Name == "key").FirstOrDefault();
                if (key != null && element.Attribute("key").Value.StartsWith("IBDocument."))
                {
                    //IBDocument.parse(ibArchive.data, element);
                }
            }



            //if (xmlReader.Name == "archive")
            //{
            //    while (xmlReader.MoveToNextAttribute())
            //    {
            //        if (xmlReader.Name == "type")
            //            ibArchive.Type = xmlReader.Value;
            //        else if (xmlReader.Name == "version")
            //            ibArchive.Version = xmlReader.Value;
            //    }
            //}

            

            return ibArchive;
        }

    }
}
