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
using System.Collections;
using System.Collections.Generic;
using System.Threading;

using Camelot.Core;

namespace Camelot.iOS
{
    public static class LayoutThread
    {
        public static void BeginLayoutThread(MonoTouch.UIKit.UIWindow mainWindow)
        {
            _MainWindow = mainWindow;
            LayoutUpdateManager.Begin(LayoutUpdater);
        }

        public static void TerminateLayoutThread()
        {
            LayoutUpdateManager.End();
        }

        static MonoTouch.UIKit.UIWindow _MainWindow;


        private static void LayoutUpdater(IList<LayoutUpdateManager.LayoutOperation> arrangeQueue, IList<LayoutUpdateManager.LayoutOperation> measureQueue,
                                               LayoutUpdateManager.CancellationToken cancellationToken)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            while (!cancellationToken.Cancel)
            {
                if (measureQueue.Count != 0)
                {
                    // process measure queue
                    lock (measureQueue)
                    {
                        //                        measureQueue.Sort();
                        var operation = measureQueue[0];
                        if (!operation.Element.IsMeasureValid)
                        {
                            _MainWindow.InvokeOnMainThread(() =>
                            {
                                operation.Element.Measure(new Size(_MainWindow.Frame.Width, _MainWindow.Frame.Height));
                            });
                        }
                        measureQueue.Remove(operation);
                    }
                }
                else if (arrangeQueue.Count != 0)
                {
                    // process arrange queue
                    lock (arrangeQueue)
                    {
                        var operation = arrangeQueue[0];
                        if (!operation.Element.IsArrangeValid)
                        {
                            _MainWindow.InvokeOnMainThread(() =>
                            {
                                Rect parentRect;
                                if (!double.IsNaN(operation.Element.LastKnownBounds.Width))
                                    operation.Element.Arrange(operation.Element.LastKnownBounds);
                                else
                                {
                                    parentRect = _MainWindow.Frame.ToFrameworkRect();
                                    operation.Element.Arrange(parentRect);
                                }
                            });
                        }
                        arrangeQueue.Remove(operation);
                    }
                }
            }
        }
    }
}