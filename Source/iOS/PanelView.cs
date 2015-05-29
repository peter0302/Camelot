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
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;

using Camelot.Core;

namespace Camelot.iOS
{
    [PlatformView(typeof(PanelView),typeof(Panel))]
    public class PanelView : View
    {
        public PanelView() : base () {   }

        protected override void Paint(CGContext g, RectangleF rect)
        {
            Panel panel = (Panel)this.Element;

            base.Paint(g, rect);                        

            BrushExtensions.PaintShape(panel.Background, null, g, BrushExtensions.ShapeType.Rectangle, 
                new Camelot.Core.Point(rect.Left, rect.Top), new Camelot.Core.Point(rect.Right, rect.Bottom), Thickness.None, 0);            
        }
    }

    [PlatformView(typeof(CanvasView),typeof(Canvas))]
    public class CanvasView : PanelView
    {
        public CanvasView() : base() { } 

    }

    [PlatformView(typeof(StackPanelView), typeof(StackPanel))]
    public class StackPanelView : PanelView
    {
        public StackPanelView() : base() { } 
    }
      


}

