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
using System.IO;

namespace Camelot.Core
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string RemoveWhitespace (this string src, char c)
        {
            int i, j;
            for (i = 0; i < src.Length; i++)
            {
                if (src[i] != c)
                    break;
            }
            if (i == src.Length) return "";
            for (j = src.Length - 1; j >= 0; j--)
            {
                if (src[j] != c)
                    break;
            }
            return src.Substring(i, j - i + 1);  
        }

        public static string LeftOf (this string src, char c)
        {
            int idx = src.IndexOf(c);
            if (idx == -1)
            {
                return src;
            }

            return src.Substring(0, idx);
        }

        public static string LeftOf (this string src, char c, int n)
        {
            int idx = -1;
            while (n != 0)
            {
                idx = src.IndexOf(c, idx + 1);
                if (idx == -1)
                {
                    return src;
                }
                --n;
            }
            return src.Substring(0, idx);
        }

        public static string RightOf (this string src, char c)
        {
            int idx = src.IndexOf(c);
            if (idx == -1)
            {
                return "";
            }

            return src.Substring(idx + 1);
        }

        public static string RightOf (this string src, char c, int n)
        {
            int idx = -1;
            while (n != 0)
            {
                idx = src.IndexOf(c, idx + 1);
                if (idx == -1)
                {
                    return "";
                }
                --n;
            }

            return src.Substring(idx + 1);
        }

        public static string Between (this string src, char start, char end)
        {
            string res = String.Empty;
            int idxStart = src.IndexOf(start);
            if (idxStart != -1)
            {
                ++idxStart;
                int idxEnd = src.IndexOf(end, idxStart);
                if (idxEnd != -1)
                {
                    res = src.Substring(idxStart, idxEnd - idxStart);
                }
            }
            return res;
        }
    }
}