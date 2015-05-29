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

namespace Camelot.Core
{
    public enum TextAlignment
    {
        Left,
        Right,
        Center,
        Justify
    }

    public enum TextWrapping
    {
        WrapWithOverflow,
        Wrap,
        NoWrap
    }

    public interface ITextBlockView
    {
        /// <summary>
        /// Returns the dimensions, in pixels, of a single line of text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        Size GetTextDimensions(string text);

        void OnFontChanged();


    }


    [HasPlatformView]
    public class TextBlock : FrameworkElement
    {
        #region TextAlignment TextAlignment dependency property
       	public static DependencyProperty TextAlignmentProperty = DependencyProperty.Register(  "TextAlignment", typeof(TextAlignment), typeof(TextBlock), new FrameworkPropertyMetadata((TextAlignment)TextAlignment.Center, FrameworkPropertyMetadataOptions.AffectsRender,
                                                               (obj, args) => { ((TextBlock)obj).OnTextAlignmentChanged(args); }));
       	public TextAlignment TextAlignment
       	{
            get
            {
                return (TextAlignment)GetValue(TextAlignmentProperty);
            }
            set
            {
                SetValue(TextAlignmentProperty, value);
            }
        }
        private void OnTextAlignmentChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region string Text dependency property
       	public static DependencyProperty TextProperty = DependencyProperty.Register(  "Text", typeof(string), typeof(TextBlock), new FrameworkPropertyMetadata((string)"", FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                               (obj, args) => { ((TextBlock)obj).OnTextChanged(args); }));
       	public string Text
       	{
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        private void OnTextChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region Brush Background dependency property
       	public static DependencyProperty BackgroundProperty = DependencyProperty.Register(  "Background", typeof(Brush), typeof(TextBlock), new FrameworkPropertyMetadata((Brush)null, FrameworkPropertyMetadataOptions.AffectsRender,
                                                               (obj, args) => { ((TextBlock)obj).OnBackgroundChanged(args); }));
       	public Brush Background
       	{
            get
            {
                return (Brush)GetValue(BackgroundProperty);
            }
            set
            {
                SetValue(BackgroundProperty, value);
            }
        }
        private void OnBackgroundChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double FontSize dependency property
 //      	public static DependencyProperty FontSizeProperty = DependencyProperty.Register(  "FontSize", typeof(double), typeof(TextBlock), new FrameworkPropertyMetadata((double)12.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
          //                                                     (obj, args) => { ((TextBlock)obj).OnFontSizeChanged(args); }));
       	public double FontSize
       	{
            get
            {
                return (double)GetValue(Control.FontSizeProperty);
            }
            set
            {
                SetValue(Control.FontSizeProperty, value);
            }
        }
        private void OnFontSizeChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
      
        #region TextWrapping TextWrapping dependency property
       	public static DependencyProperty TextWrappingProperty = DependencyProperty.Register(  "TextWrapping", typeof(TextWrapping), typeof(TextBlock), new PropertyMetadata((TextWrapping)TextWrapping.Wrap,
                                                               (obj, args) => { ((TextBlock)obj).OnTextWrappingChanged(args); }));
       	public TextWrapping TextWrapping
       	{
            get
            {
                return (TextWrapping)GetValue(TextWrappingProperty);
            }
            set
            {
                SetValue(TextWrappingProperty, value);
            }
        }
        private void OnTextWrappingChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        
        #region Brush Foreground dependency property
//       	public static DependencyProperty ForegroundProperty = DependencyProperty.Register(  "Foreground", typeof(Brush), typeof(TextBlock), new PropertyMetadata((Brush)new SolidColorBrush(Colors.Black),
//                                                               (obj, args) => { ((TextBlock)obj).OnForegroundChanged(args); }));
       	public Brush Foreground
       	{
            get
            {
                return (Brush)GetValue(Control.ForegroundProperty);
            }
            set
            {
                SetValue(Control.ForegroundProperty, value);
            }
        }
        private void OnForegroundChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


        #region FontFamily FontFamily dependency property
       	//public static DependencyProperty FontFamilyProperty = DependencyProperty.Register(  "FontFamily", typeof(FontFamily), typeof(TextBlock), new PropertyMetadata((FontFamily)new FontFamily("Arial"),
                                                               //(obj, args) => { ((TextBlock)obj).OnFontFamilyChanged(args); }));
       	public FontFamily FontFamily
       	{
            get
            {
                return (FontFamily)GetValue(Control.FontFamilyProperty);
            }
            set
            {
                SetValue(Control.FontFamilyProperty, value);
            }
        }
        private void OnFontFamilyChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        /// <summary>
        /// Returns the desired size based on the current text and font, assuming an infinite width.
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            ITextBlockView view = (ITextBlockView)this.View;
            var sz = view.GetTextDimensions(this.Text);
            return sz;
            //return base.MeasureOverride(availableSize);
        }

    }
}