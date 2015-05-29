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
using Camelot.Core.Internal;

namespace Camelot.Core
{

    public abstract class RangeBase : Control
    {
        #region double LargeChange dependency property
       	public static DependencyProperty LargeChangeProperty = DependencyProperty.Register(  "LargeChange", typeof(double), typeof(RangeBase), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((RangeBase)obj).OnLargeChangeChanged(args); }));
       	public double LargeChange
       	{
            get
            {
                return (double)GetValue(LargeChangeProperty);
            }
            set
            {
                SetValue(LargeChangeProperty, value);
            }
        }
        private void OnLargeChangeChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double Maximum dependency property
       	public static DependencyProperty MaximumProperty = DependencyProperty.Register(  "Maximum", typeof(double), typeof(RangeBase), new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits,
                                        (obj, args) => {
                                            ((RangeBase)obj).OnMaximumChanged((double)args.OldValue, (double)args.NewValue); 
                                        }));
       	public double Maximum
       	{
            get
            {
                return (double)GetValue(MaximumProperty);
            }
            set
            {
                SetValue(MaximumProperty, value);
            }
        }
        protected virtual void OnMaximumChanged(double oldValue, double newValue)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double Minimum dependency property
       	public static DependencyProperty MinimumProperty = DependencyProperty.Register(  "Minimum", typeof(double), typeof(RangeBase), new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.Inherits,
                                            (obj, args) => { 
                                                ((RangeBase)obj).OnMinimumChanged((double)args.OldValue, (double)args.NewValue);
                                            }));
       	public double Minimum
       	{
            get
            {
                return (double)GetValue(MinimumProperty);
            }
            set
            {
                SetValue(MinimumProperty, value);
            }
        }
        protected virtual void OnMinimumChanged(double oldValue, double newValue)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double SmallChange dependency property
       	public static DependencyProperty SmallChangeProperty = DependencyProperty.Register(  "SmallChange", typeof(double), typeof(RangeBase), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((RangeBase)obj).OnSmallChangeChanged(args); }));
       	public double SmallChange
       	{
            get
            {
                return (double)GetValue(SmallChangeProperty);
            }
            set
            {
                SetValue(SmallChangeProperty, value);
            }
        }
        private void OnSmallChangeChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double Value dependency property
       	public static DependencyProperty ValueProperty = DependencyProperty.Register(  "Value", typeof(double), typeof(RangeBase), new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits,
                                            (obj, args) => 
                                            {
                                                ((RangeBase)obj).OnValueChanged((double)args.OldValue, (double)args.NewValue);
                                            }));
       	public double Value
       	{
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
        protected virtual void OnValueChanged(double oldValue, double newValue)
        {

        }
        #endregion
    }
   
    public class ScrollBar : RangeBase
    {
        Thumb _Thumb;
        Track _Track;

        #region Orientation Orientation dependency property
       	public static DependencyProperty OrientationProperty = DependencyProperty.Register(  "Orientation", typeof(Orientation), typeof(ScrollBar), new FrameworkPropertyMetadata((Orientation)Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits ));
       	public Orientation Orientation
       	{
            get
            {
                return (Orientation)GetValue(OrientationProperty);
            }
            set
            {
                SetValue(OrientationProperty, value);
            }
        }
        #endregion

        #region double ViewportSize dependency property
       	public static DependencyProperty ViewportSizeProperty = DependencyProperty.Register(  "ViewportSize", typeof(double), typeof(ScrollBar), new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits,
                                                               (obj, args) => { ((ScrollBar)obj).OnViewportSizeChanged(args); }));
       	public double ViewportSize
       	{
            get
            {
                return (double)GetValue(ViewportSizeProperty);
            }
            set
            {
                SetValue(ViewportSizeProperty, value);
            }
        }
        private void OnViewportSizeChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        public static RoutedEvent ScrollEvent = EventManager.RegisterRoutedEvent("Scroll", RoutingStrategy.Direct, typeof(ScrollEventHandler), typeof(ScrollBar));
        public event ScrollEventHandler Scroll
        {
            add
            {
                this.AddHandler(ScrollEvent, value, false);
            }
            remove
            {
                this.RemoveHandler(ScrollEvent, value);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if ( _Thumb != null )
            {
                _Thumb.DragStarted -= _Thumb_DragStarted;
                _Thumb.DragDelta -= _Thumb_DragDelta;
                _Thumb.DragCompleted -= _Thumb_DragCompleted;
            }

            VisualTreeEnumerator tree = new VisualTreeEnumerator(this);
            foreach ( FrameworkElement fe in tree )
            {
                if ( fe is Thumb )
                {
                    _Thumb = (Thumb)fe;
                    _Thumb.DragStarted += _Thumb_DragStarted;
                    _Thumb.DragDelta += _Thumb_DragDelta;
                    _Thumb.DragCompleted += _Thumb_DragCompleted;
                }
                if (fe is Track)
                    _Track = (Track)fe;
            }            
        }

        void _Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
          //  throw new NotImplementedException();
        }

        void _Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
//            throw new NotImplementedException();
            if ( _Track == null ) return;

            double valueChange;
            if ( this.Orientation == Core.Orientation.Horizontal )
            {
                valueChange = e.HorizontalChange / _Track.ThumbPixelsPerValue;                
            }
            else
            {
                valueChange = e.VerticalChange / _Track.ThumbPixelsPerValue;
            }

            this.Value = Math.Max(  Math.Min(this.Maximum, this.Value + valueChange),
                                    this.Minimum);
        }

        void _Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
//            throw new NotImplementedException();
        }
    }
}