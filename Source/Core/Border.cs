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
using System.ComponentModel;

using Camelot.Core;

namespace Camelot.Core
{
    [HasPlatformView]
	public class Border : Decorator
	{
		public Border()
		{


		}


        
        #region Brush Background dependency property
       	public static DependencyProperty BackgroundProperty = DependencyProperty.Register(  "Background", typeof(Brush), typeof(Border), new PropertyMetadata((Brush)null,
                                                               (obj, args) => { ((Border)obj).OnBackgroundChanged(args); }));
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
        
        #region Brush BorderBrush dependency property
       	public static DependencyProperty BorderBrushProperty = DependencyProperty.Register(  "BorderBrush", typeof(Brush), typeof(Border), new PropertyMetadata((Brush)null,
                                                               (obj, args) => { ((Border)obj).OnBorderBrushChanged(args); }));
       	public Brush BorderBrush
       	{
            get
            {
                return (Brush)GetValue(BorderBrushProperty);
            }
            set
            {
                SetValue(BorderBrushProperty, value);
            }
        }
        private void OnBorderBrushChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region Thickness BorderThickness dependency property
        public static DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(Border), new PropertyMetadata((Thickness)new Thickness(0,0,0,0),
                                                               (obj, args) => { ((Border)obj).OnBorderThicknessChanged(args); }));
        public Thickness BorderThickness
       	{
            get
            {
                return (Thickness)GetValue(BorderThicknessProperty);
            }
            set
            {
                SetValue(BorderThicknessProperty, value);
            }
        }
        private void OnBorderThicknessChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double CornerRadius dependency property
       	public static DependencyProperty CornerRadiusProperty = DependencyProperty.Register(  "CornerRadius", typeof(double), typeof(Border), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((Border)obj).OnCornerRadiusChanged(args); }));
       	public double CornerRadius
       	{
            get
            {
                return (double)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }
        private void OnCornerRadiusChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region Thickness Padding dependency property
       	public static DependencyProperty PaddingProperty = DependencyProperty.Register(  "Padding", typeof(Thickness), typeof(Border), new PropertyMetadata((Thickness)new Thickness(0,0,0,0),
                                                               (obj, args) => { ((Border)obj).OnPaddingChanged(args); }));
       	public Thickness Padding
       	{
            get
            {
                return (Thickness)GetValue(PaddingProperty);
            }
            set
            {
                SetValue(PaddingProperty, value);
            }
        }
        private void OnPaddingChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.Child == null)
            {
                return base.MeasureOverride(    new Size(availableSize.Width - this.BorderThickness.TotalWidth, 
                                                    availableSize.Height - this.BorderThickness.TotalHeight));
            }

            this.Child.Measure(availableSize);
            var childSize = this.Child.DesiredSize;
            childSize.Width += this.BorderThickness.TotalWidth;
            childSize.Height += this.BorderThickness.TotalHeight;
            return childSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Child != null && this.Child is FrameworkElement)
            {
                Rect clientRect = new Rect(this.BorderThickness.Left, this.BorderThickness.Top, finalSize.Width - this.BorderThickness.TotalWidth, finalSize.Height - this.BorderThickness.TotalHeight);
                this.Child.Arrange(clientRect);
                ((FrameworkElement)this.Child).Clip = new Rect(0,0,clientRect.Width, clientRect.Height);
                return finalSize;
            }
            else
            {
                return base.ArrangeOverride(finalSize);
            }
        }
	}
}

