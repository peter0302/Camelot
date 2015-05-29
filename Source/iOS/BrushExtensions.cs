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
using MonoTouch.CoreText;
using MonoTouch.CoreGraphics;
using Camelot.Core;

namespace Camelot.iOS
{
    public static class BrushExtensions
    {
        public enum ShapeType
        {
            Line,
            Ellipse,
            Rectangle
        }

        

        public static CGColor ToCGColor (this Color c)
        {
            return new CGColor((float)c.R / (float)255.0, (float)c.G / (float)255.0, (float)c.B / (float)255.0, (float)c.A / (float)255.0);
        }

        public static CGGradient ToCGGradient (this LinearGradientBrush lgb)
        {
            float[] locations = new float[lgb.GradientStops.Count];
            CGColor[] colors = new CGColor[lgb.GradientStops.Count];
            int i = 0;
            foreach (GradientStop stop in lgb.GradientStops)
            {
                locations[i] = (float)stop.Offset;
                colors[i] = stop.Color.ToPlatformColor().CGColor;
                i++;
            }
            return new CGGradient(CGColorSpace.CreateDeviceRGB(), colors, locations);                
        }

 

        public static UIColor ToUIColor(this Brush brush)
        {
            CGColor cgc = brush.ToColor();
            return new UIColor(cgc);        
        }


        public static CGColor ToColor (this Brush stroke)
        {
            if (stroke == null)
                return new CGColor(0, 0, 0, 1);
            else if (stroke is SolidColorBrush )
            {
                Color c = ((SolidColorBrush)stroke).Color;
                return c.ToCGColor();
            }
            else if ( stroke is LinearGradientBrush )
            {
                var c = (GradientStop)((LinearGradientBrush)stroke).GradientStops[0];
                return c.Color.ToCGColor();
            }
            else
            {
                return new CGColor(0, 0, 0, 1);
            }
        }




        private static void FillShape ( CGContext gc, Brush fillBrush, ShapeType type, Core.Point pt1, Core.Point pt2, Thickness thickness, double cornerRadius )
        {
            bool isGradient = false;
            LinearGradientBrush lgb = null;
            CGGradient gradient = null;
            CGColor fillColor = null;

            Rect shapeRect = new Rect(pt1, pt2);

            if ( fillBrush is LinearGradientBrush )
            {
                isGradient = true;
                lgb = (LinearGradientBrush)fillBrush;
                gradient = lgb.ToCGGradient();
                gc.SaveState();
            }                      
            else
            {                
                fillColor = ((SolidColorBrush)fillBrush).Color.ToCGColor();
                gc.SetFillColor(fillColor);
            }

            DrawShapePath(gc, type, new PointF((float)pt1.X, (float)pt1.Y), new PointF((float)pt2.X, (float)pt2.Y), thickness, (float)cornerRadius);

            if ( isGradient )
            {
                PointF startPoint = new PointF((float)(shapeRect.X + lgb.StartPoint.X * shapeRect.Width),
                                            (float)(shapeRect.Y + lgb.StartPoint.Y * shapeRect.Height));
                PointF endPoint = new PointF((float)(shapeRect.X + lgb.EndPoint.X * shapeRect.Width),
                                            (float)(shapeRect.Y + lgb.EndPoint.Y * shapeRect.Height));                    
                gc.Clip();
                gc.DrawLinearGradient(gradient, startPoint, endPoint, CGGradientDrawingOptions.DrawsAfterEndLocation);
                gc.RestoreState();
            }
            else
            {                
                gc.DrawPath( CGPathDrawingMode.Fill);
            }
        }


        private static void DrawShapePath (CGContext gc, ShapeType type, PointF pt1, PointF pt2, Thickness thickness, float cornerRadius)
        {
            switch (type)
            {
                case ShapeType.Rectangle:
                    if (cornerRadius == 0)
                    {
                        gc.AddRect(new RectangleF(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y));
                    }
                    else
                    {
                        var path = UIBezierPath.FromRoundedRect(new RectangleF(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y), cornerRadius);
                        gc.AddPath(path.CGPath);
                    }                    
                    break;
                case ShapeType.Line:
                    gc.SetLineWidth((float)thickness.Left);
                    gc.AddLines(new PointF[] { pt1, pt2 });
                    break;
                case ShapeType.Ellipse:
                    gc.SetLineWidth((float)thickness.Left);
                    gc.AddEllipseInRect(new RectangleF((float)pt1.X, (float)pt1.Y, (float)pt2.X, (float)pt2.Y));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Paints the specified shape with the brush using the specified CoreGraphics context.
        /// </summary>
        /// <param name="brush">The framework brush.</param>
        /// <param name="type">The type of shape..</param>
        /// <param name="gc">The provided CoreGraphics context.</param>
        /// <param name="pt1">The upper left corner of the shape, or the first point of the line.</param>
        /// <param name="pt2">The bottom right corner of the shape, or the second point of the line..</param>
        /// <param name="strokeThickness">The thickness of the pen used for the outline (Stroke)..</param>
        public static void PaintShape(Brush fill, Brush stroke, CGContext gc, ShapeType type, Camelot.Core.Point pt1, Camelot.Core.Point pt2, Thickness thickness, double cornerRadius)
        {
            if ( fill != null )          
                FillShape(gc, fill, type, pt1, pt2, thickness, cornerRadius);

            if (stroke == null) return;

            CGColor  strokeColor;
            if (stroke is SolidColorBrush)
                strokeColor = ((SolidColorBrush)stroke).Color.ToPlatformColor().CGColor;
            else
                return;

            gc.SetStrokeColor(strokeColor);
            gc.SetFillColor(strokeColor);

            if (cornerRadius == 0 && thickness == Thickness.None)
            {
                DrawShapePath(gc, type, new PointF((float)pt1.X, (float)pt1.Y), new PointF((float)pt2.X, (float)pt2.Y), thickness, (float)cornerRadius);
                gc.DrawPath(CGPathDrawingMode.Stroke);
            }
            else
            {
                DrawRoundedRectPath(gc, new PointF((float)pt1.X, (float)pt1.Y), new PointF((float)pt2.X, (float)pt2.Y), thickness, (float)cornerRadius);
            }

            
        }

        public static void DrawRoundedRectPath ( CGContext gc, PointF pt1, PointF pt2, Thickness thickness, float cornerRadius )
        {
            RectangleF outerRect = new RectangleF(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);
            var outerPath = UIBezierPath.FromRoundedRect(outerRect, cornerRadius);

            RectangleF innerRect = new RectangleF(
                outerRect.X + (float)thickness.Left, outerRect.Y + (float)thickness.Top,
                outerRect.Width - (float)thickness.TotalWidth, outerRect.Height - (float)thickness.TotalHeight
                );

            //path.AppendPath(UIBezierPath.FromRect(innerRect));
            //path.UsesEvenOddFillRule = true;
            //path.Fill();            
            
            gc.AddPath(outerPath.CGPath);
            gc.AddPath(UIBezierPath.FromRoundedRect(innerRect, cornerRadius / 2.0f).CGPath);
            gc.DrawPath(CGPathDrawingMode.EOFill);
        }
    }
}

