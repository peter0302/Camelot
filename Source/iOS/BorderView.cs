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

using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;

using Camelot.Core;

namespace Camelot.iOS
{
    [PlatformView(typeof(BorderView), typeof(Border))]
    public class BorderView : View
    {
        protected override void Paint (CGContext g, RectangleF rect)
        {
            base.Paint(g, rect);

            Border border = (Border)this.Element;
            

            // TODO: Add paint function
            BrushExtensions.PaintShape(border.Background, border.BorderBrush, g, BrushExtensions.ShapeType.Rectangle,
                new Core.Point(rect.X, rect.Y),
                new Core.Point(rect.Right, rect.Bottom), border.BorderThickness, border.CornerRadius
                );
        }
    }
}