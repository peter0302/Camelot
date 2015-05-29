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
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Collections.Generic;


namespace Camelot.Core
{
    public static class LayoutUpdateManager
    {
        public class CancellationToken
        {
            public bool Cancel { get; internal set; }
        }


        public delegate void LayoutThread(IList<LayoutOperation> arrangeQueue, IList<LayoutOperation> measureQueue, CancellationToken cancellationToken);

        public enum OperationType
        {
            Measure,
            Arrange
        }

        public struct LayoutOperation : IComparable<LayoutOperation>
        {
            public UIElement Element;

            public override bool Equals(object obj)
            {
                if (obj is LayoutOperation)
                {
                    return ((LayoutOperation)obj).Element == this.Element;
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                return Element.GetHashCode();
            }

            public int CompareTo (LayoutOperation other)
            {
                if (Element.LayoutGeneration < other.Element.LayoutGeneration)
                    return -1;
                else if (Element.LayoutGeneration == other.Element.LayoutGeneration)
                    return 0;
                else
                    return 1;
            }
        }

        private static CancellationToken _CancellationToken = new CancellationToken();


        static List<LayoutOperation> _ArrangeQueue = new List<LayoutOperation>();
        static List<LayoutOperation> _MeasureQueue = new List<LayoutOperation>();

        public static void Add ( OperationType type, UIElement element )
        {
            LayoutOperation newOperation = new LayoutOperation {  Element = element };
            if (type == OperationType.Arrange)
            {
                if (!_ArrangeQueue.Contains(newOperation))
                {
                    _ArrangeQueue.Add(newOperation);
                    _ArrangeQueue.Sort();
                }
            }
            else
            {
                if (!_MeasureQueue.Contains(newOperation))
                {
                    _MeasureQueue.Add(newOperation);
                    _ArrangeQueue.Sort();
                }
            }
            return;
        }

        internal static void Begin (LayoutThread mainThread)
        {
            _CancellationToken = new CancellationToken();
            Task.Run(() =>
                {
                    mainThread(_ArrangeQueue, _MeasureQueue, _CancellationToken);
                });
        }

        /*


        }*/

        internal static void End()
        {
            _CancellationToken.Cancel = true;
        }

    }
}

