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
using Camelot.Core;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreText;
using MonoTouch.CoreGraphics;

namespace Camelot.iOS
{
    [PlatformView(typeof(TextBlockView), typeof(TextBlock))]
    public class TextBlockView : View, ITextBlockView
    {
        UIFont _Font;

        protected override void Paint(MonoTouch.CoreGraphics.CGContext g, System.Drawing.RectangleF rect)
        {            
            if (_Font == null) OnFontChanged();
            TextBlock b = (TextBlock)this.Element;

            BrushExtensions.PaintShape(b.Background, null, g, BrushExtensions.ShapeType.Rectangle, new Core.Point(rect.X, rect.Y),
                                                new Core.Point(rect.Right, rect.Bottom), 
                                                Thickness.None, 0);

            NSString drawText = new NSString(b.Text);
            g.SetTextDrawingMode(CGTextDrawingMode.Fill);
            //gc.SetFillColorWithColor(background.ToColor());

            CGColor textColor = b.Foreground.ToColor();
            g.SetFillColorWithColor(textColor);


            UILineBreakMode lbMode = UILineBreakMode.WordWrap;
            switch ( b.TextWrapping )
            {
                case TextWrapping.NoWrap:
                    lbMode = UILineBreakMode.Clip;
                    break;
                case TextWrapping.Wrap:
                    lbMode = UILineBreakMode.WordWrap;
                    break;
                default:
                    throw new NotSupportedException();            
            }

            UITextAlignment textAlign = UITextAlignment.Left;
            switch ( b.TextAlignment )
            {
                case TextAlignment.Center:
                    textAlign = UITextAlignment.Center;
                    break;
                case TextAlignment.Justify:
                    textAlign = UITextAlignment.Justified;
                    break;
                case TextAlignment.Left:
                    textAlign = UITextAlignment.Left;
                    break;
                case TextAlignment.Right:
                    textAlign = UITextAlignment.Right;
                    break;
            }

            drawText.DrawString(rect, _Font, lbMode, textAlign);

        }

        public void OnFontChanged()
        {
            TextBlock b = (TextBlock)this.Element;
            _Font = UIFont.FromName(b.FontFamily.Source, (float)b.FontSize);
        }

        public Size GetTextDimensions(string sourceText)
        {
            if (_Font == null) OnFontChanged();
            TextBlock b = (TextBlock)this.Element;

            NSString text = new NSString(b.Text);
            var sz = text.StringSize(_Font);
            return new Size(sz.Width, sz.Height);            
        }


    }
}