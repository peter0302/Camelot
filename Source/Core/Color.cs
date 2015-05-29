/***********************************************************************************************
 * © Copyright 2014-2015 Peter Moore. All rights reserved.
 *
 *  This file is part of Camelot.
 *  
 *  Camelot is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *  
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 ***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Camelot.Core
{
    public struct Color
    {
        public byte A { get; set; }
        public byte B { get; set; }
        public byte G { get; set; }
        public byte R { get; set; }

        public static Color FromArgb (byte a, byte r, byte g, byte b)
        {
            return new Color { A = a, B = b, G = g, R = r };
        }

        public static Color FromValue ( uint value )
        {
            return new Color
            {
                A = (byte)((value & 0xFF000000) >> 24),
                R = (byte)((value & 0x00FF0000) >> 16),
                G = (byte)((value & 0x0000FF00) >> 8),
                B = (byte)((value & 0x000000FF))
            };
        }

        public static object FromString (string s)
        {
            if (s[0] == '#')
            {
                string hexValue = s.RightOf('#');
                uint value = Convert.ToUInt32(hexValue, 16);
                return Color.FromValue(value);
            }
            else
            {
                PropertyInfo pi = typeof(Colors).GetRuntimeProperty(s);
                if (pi != null)
                {
                    Color c = (Color)pi.GetValue(null);
                    return c;
                }
                else
                {
                    return Colors.Black;
                }
            }
        }
    }
}
