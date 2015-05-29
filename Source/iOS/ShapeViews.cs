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
using System.Threading.Tasks;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.GLKit;

using Camelot.Core;

namespace Camelot.iOS
{
    [PlatformView(typeof(LineView), typeof(Line))]
    public sealed class LineView : View
    {
        protected override void Paint (CGContext g, RectangleF rect)
        {
            base.Paint(g, rect);
            Line line = (Line)this.Element;

            BrushExtensions.PaintShape(null, line.Stroke, g, BrushExtensions.ShapeType.Line, 
                new Camelot.Core.Point(line.X1, line.Y1),
                new Camelot.Core.Point(line.X2, line.Y2),
                new Thickness(line.StrokeThickness,0,0,0), 0);
        }


    }


    [PlatformView(typeof(RectangleView),typeof(Camelot.Core.Rectangle))]
    public sealed class RectangleView : View
    {
        protected override void Paint(CGContext g, RectangleF rect)
        {
            base.Paint (g, rect);

            Camelot.Core.Rectangle rectangle = (Camelot.Core.Rectangle)this.Element;
                BrushExtensions.PaintShape(rectangle.Fill, rectangle.Stroke, g, BrushExtensions.ShapeType.Rectangle,
                    new Camelot.Core.Point(rect.Left + rectangle.StrokeThickness / 2, rect.Top + rectangle.StrokeThickness / 2),
                    new Camelot.Core.Point(rect.Right - rectangle.StrokeThickness, rect.Bottom - rectangle.StrokeThickness),
                    new Thickness(rectangle.StrokeThickness,0,0,0),0);
        }
    }

    [PlatformView(typeof(EllipseView),typeof(Ellipse))]
    public sealed class EllipseView : View
    {
        protected override void Paint(CGContext g, RectangleF rect)
        {
            base.Paint (g, rect);

            Rect cl = this.Element.Clip;

            Ellipse ellipse = (Ellipse)this.Element;            
            BrushExtensions.PaintShape(ellipse.Fill, ellipse.Stroke, g, BrushExtensions.ShapeType.Ellipse,
                new Camelot.Core.Point(rect.Left + ellipse.StrokeThickness / 2, rect.Top + ellipse.StrokeThickness / 2),
                new Camelot.Core.Point(rect.Right - ellipse.StrokeThickness, rect.Bottom - ellipse.StrokeThickness), 
                new Thickness(ellipse.StrokeThickness,0,0,0), 0);
            
        }
    }
}

