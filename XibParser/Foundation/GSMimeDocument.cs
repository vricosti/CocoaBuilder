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

namespace Smartmobili.Cocoa
{
    public class GSMimeDocument : NSObject
    {
        new public static Class Class = new Class(typeof(GSMimeDocument));
        new public static GSMimeDocument alloc() { return new GSMimeDocument(); }

        


        private static void decodebase64(byte[] dst, byte[] src, int dstIndex)
        {
            dst[dstIndex] = Convert.ToByte((src[0] << 2) | ((src[1] & 0x30) >> 4));
            dst[dstIndex + 1] = Convert.ToByte(((src[1] & 0x0F) << 4) | ((src[2] & 0x3C) >> 2));
            dst[dstIndex + 2] = Convert.ToByte(((src[2] & 0x03) << 6) | (src[3] & 0x3F)); 
        }

        public static NSData decodeBase64(NSData source)
        {
            int length;
            int declen;
            byte[] src;
            byte[] end;
            byte[] result;
            int dstIndex = 0;
            byte[] buf = new byte[4];
            uint pos = 0;
            int pad = 0;

            if (source == null)
            {
                return null;
            }

            length = (int)source.length();
            if (length == 0)
            {
                return NSData.data();
            }

            declen = ((length + 3) * 3) / 4;
            src = source.Bytes;
            //end = &src[length];

            result = new byte[declen];

            for (int i = 0; (i < length) && (Convert.ToChar(src[i]) != '\0'); )
            {
                Char c = Convert.ToChar(src[i++]);

                if (Char.IsUpper(c))
                {
                    c -= 'A';
                }
                else if (Char.IsLower(c))
                {
                    c = Convert.ToChar(c - Convert.ToInt32('a') + 26);
                }
                else if (Char.IsDigit(c))
                {
                    c = Convert.ToChar(c - Convert.ToInt32('0') + 52);
                }
                else if (c == '/')
                {
                    c = Convert.ToChar(63);
                }
                else if (c == '_')
                {
                    c = Convert.ToChar(63);	/* RFC 4648 permits '_' in URLs and filenames */
                }
                else if (c == '+')
                {
                    c = Convert.ToChar(62);
                }
                else if (c == '-')
                {
                    c = Convert.ToChar(62);	/* RFC 4648 permits '-' in URLs and filenames */
                }
                else if (c == '=')
                {
                    c = Convert.ToChar(-1);
                    pad++;
                }
                else
                {
                    c = Convert.ToChar(-1);	/* Ignore ... non-standard but more tolerant. */
                    length--;	/* Don't count this as part of the length. */
                }

                if (c >= 0)
                {
                    buf[pos++] = Convert.ToByte(c);
                    if (pos == 4)
                    {
                        pos = 0;
                        decodebase64(result, buf, dstIndex);
                        dstIndex += 3;
                    }
                }
            }

            /* If number of bytes is not a multiple of four, treat it as if the missing
             * bytes were the '=' characters normally used for padding.
             * This is not allowed by the basic standards, but permitted in some
             * variants of 6ase64 encoding, so we should tolerate it.
             */
            if (length % 4 > 0)
            {
                pad += (4 - length % 4);
            }
            if (pos > 0)
            {
                uint i;
                byte[] tail = new byte[3];

                for (i = pos; i < 4; i++)
                {
                    buf[i] = Convert.ToByte('\0');
                }
                decodebase64(tail, buf, 0);
                if (pad > 3) 
                    pad = 3;

                for (int j = 0; j < (3 - pad); j++)
                    result[dstIndex + j] = tail[j];
                dstIndex += 3 - pad;
            }

            return (NSData)NSData.alloc().initWithBytes(result);
        }

    }
}
