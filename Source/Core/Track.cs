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
    public class Track : FrameworkElement
    {        
        #region double Maximum dependency property
 //      	public static DependencyProperty MaximumProperty = DependencyProperty.Register(  "Maximum", typeof(double), typeof(Track), new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsArrange,
 //                                                              (obj, args) => { ((Track)obj).OnMaximumChanged(args); }));
       	public double Maximum
       	{
            get
            {
                return (double)GetValue(ScrollBar.MaximumProperty);
            }
            set
            {
                SetValue(ScrollBar.MaximumProperty, value);
            }
        }
        private void OnMaximumChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double Minimum dependency property
 //      	public static DependencyProperty MinimumProperty = DependencyProperty.Register(  "Minimum", typeof(double), typeof(Track), new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsArrange,
  //                                                             (obj, args) => { ((Track)obj).OnMinimumChanged(args); }));
       	public double Minimum
       	{
            get
            {
                return (double)GetValue(ScrollBar.MinimumProperty);
            }
            set
            {
                SetValue(ScrollBar.MinimumProperty, value);
            }
        }
        private void OnMinimumChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region Orientation Orientation dependency property
 //      	public static DependencyProperty OrientationProperty = DependencyProperty.Register(  "Orientation", typeof(Orientation), typeof(Track), new FrameworkPropertyMetadata((Orientation)Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure,
 //                                                              (obj, args) => { ((Track)obj).OnOrientationChanged(args); }));
       	public Orientation Orientation
       	{
            get
            {
                return (Orientation)GetValue(ScrollBar.OrientationProperty);
            }
            set
            {
                SetValue(ScrollBar.OrientationProperty, value);
            }
        }
        private void OnOrientationChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
      
        #region double Value dependency property
 //      	public static DependencyProperty ValueProperty = DependencyProperty.Register(  "Value", typeof(double), typeof(Track), new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsArrange,
    //                                                           (obj, args) => { ((Track)obj).OnValueChanged(args); }));
       	public double Value
       	{
            get
            {
                return (double)GetValue(ScrollBar.ValueProperty);
            }
            set
            {
                SetValue(ScrollBar.ValueProperty, value);
            }
        }
        private void OnValueChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
     
        #region double ViewportSize dependency property
       	public double ViewportSize
       	{
            get
            {
                return (double)GetValue(ScrollBar.ViewportSizeProperty);
            }
            set
            {
                SetValue(ScrollBar.ViewportSizeProperty, value);
            }
        }
        private void OnViewportSizeChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        private Thumb _Thumb;
        public Thumb Thumb
        {
            get
            {
                return _Thumb;
            }
            set
            {
                if ( _Thumb != null )
                {
                    this.RemoveVisualChild (_Thumb);
                }
                _Thumb = value;
                if ( _Thumb != null )
                {
                    this.AddVisualChild(_Thumb);
                }
            }
        }

        /// <summary>
        /// Returns the number of pixels corresponding to 1 unit of value.
        /// </summary>
        internal double ThumbPixelsPerValue
        {
            get;
            private set;
        }


        protected override int VisualChildrenCount
        {
            get
            {
                if (this._Thumb != null)
                    return 1;
                else
                    return 0;
            }
        }

        protected override UIElement GetVisualChild(int index)
        {
            if (this.Thumb != null && index == 0)
                return this.Thumb;
            else
                throw new ArgumentException("index");
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this._Thumb != null)
            {
                double viewportFraction = this.ViewportSize / (this.ViewportSize + this.Maximum - this.Minimum);
                if (this.Orientation == Orientation.Horizontal)
                {
                    double valuePixels = (finalSize.Width - viewportFraction * finalSize.Width);
                    this.ThumbPixelsPerValue = valuePixels / (this.Maximum - this.Minimum);
                    double viewpoertOffset = this.ThumbPixelsPerValue * (this.Value - this.Minimum);                    
                    this.Thumb.Arrange(new Rect(viewpoertOffset, 0, viewportFraction * finalSize.Width, finalSize.Height));
                }
                else
                {
                    double valuePixels = (finalSize.Height - viewportFraction * finalSize.Height);
                    this.ThumbPixelsPerValue = valuePixels / (this.Maximum - this.Minimum);
                    double viewpoertOffset = this.ThumbPixelsPerValue * (this.Value - this.Minimum); 
                    //double viewpoertOffset = (this.Value - this.Minimum) / (finalSize.Height - viewportFraction * finalSize.Height);
                    this.Thumb.Arrange(new Rect(0, viewpoertOffset, finalSize.Width, viewportFraction * finalSize.Height));
                }
            }
            return finalSize;
        }


      

    }
}