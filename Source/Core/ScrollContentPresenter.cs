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
    public class ScrollContentPresenter : ContentPresenter
    {        
        #region bool CanContentScroll dependency property
        // Summary:
        //     Identifies the System.Windows.Controls.ScrollContentPresenter.CanContentScroll dependency
        //     property.
        //
        // Returns:
        //     The identifier for the System.Windows.Controls.ScrollContentPresenter.CanContentScroll dependency
        //     property.
       	public static DependencyProperty CanContentScrollProperty = DependencyProperty.Register(  "CanContentScroll", typeof(bool), typeof(ScrollContentPresenter), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((ScrollContentPresenter)obj).OnCanContentScrollChanged(args); }));

        /// <summary>
        /// Indicates whether the content, if it supports System.Windows.Controls.Primitives.IScrollInfo, should be allowed to control scrolling.
        /// </summary>
        /// <returns> true if the content is allowed to scroll; otherwise, false. A false value indicates that the 
        /// System.Windows.Controls.ScrollContentPresenter acts as 
        /// the scrolling client. This property has no default value.
        /// </returns>
       	public bool CanContentScroll
       	{
            get
            {
                return (bool)GetValue(CanContentScrollProperty);
            }
            set
            {
                SetValue(CanContentScrollProperty, value);
            }
        }
        private void OnCanContentScrollChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion




        // Summary:
        //     Initializes a new instance of the System.Windows.Controls.ScrollContentPresenter
        //     class.
        public ScrollContentPresenter()
        {

        }

        /*
        // Summary:
        //     Gets the System.Windows.Documents.AdornerLayer on which adorners are rendered.
        //
        // Returns:
        //     An System.Windows.Documents.AdornerLayer.
        public AdornerLayer AdornerLayer { get; }
         */

        //
        // Summary:
        //     Gets or sets a value that indicates whether scrolling on the horizontal axis
        //     is possible.
        //
        // Returns:
        //     true if scrolling is possible; otherwise, false. This property has no default
        //     value.
        public bool CanHorizontallyScroll { get; set; }

        //
        // Summary:
        //     Gets or sets a value that indicates whether scrolling on the vertical axis
        //     is possible.
        //
        // Returns:
        //     true if scrolling is possible; otherwise, false. This property has no default
        //     value.
        public bool CanVerticallyScroll { get; set; }

        //
        // Summary:
        //     Gets the vertical size of the extent.
        //
        // Returns:
        //     The vertical size of the extent. This property has no default value.
        public double ExtentHeight { get; private set;  }

        //
        // Summary:
        //     Gets the horizontal size of the extent.
        //
        // Returns:
        //     The horizontal size of the extent. This property has no default value.
        public double ExtentWidth { get; private set;  }
        //
        // Summary:
        //     Gets the horizontal offset of the scrolled content.
        //
        // Returns:
        //     The horizontal offset. This property has no default value.
        public double HorizontalOffset { get; private set; }

        //
        // Summary:
        //     Gets or sets a System.Windows.Controls.ScrollViewer element that controls
        //     scrolling behavior.
        //
        // Returns:
        //     The System.Windows.Controls.ScrollViewer element that controls scrolling
        //     behavior. This property has no default value.
        public ScrollViewer ScrollOwner { get; private set; }

        //
        // Summary:
        //     Gets the vertical offset of the scrolled content.
        //
        // Returns:
        //     The vertical offset of the scrolled content. Valid values are between zero
        //     and the System.Windows.Controls.ScrollContentPresenter.ExtentHeight minus
        //     the System.Windows.Controls.ScrollContentPresenter.ViewportHeight. This property
        //     has no default value.
        public double VerticalOffset { get; private set; }

        //
        // Summary:
        //     Gets the vertical size of the viewport for this content.
        //
        // Returns:
        //     The vertical size of the viewport for this content. This property has no
        //     default value.
        public double ViewportHeight { get; private set; }

        //
        // Summary:
        //     Gets the horizontal size of the viewport for this content.
        //
        // Returns:
        //     The horizontal size of the viewport for this content. This property has no
        //     default value.
        public double ViewportWidth { get; private set; }

        protected override int VisualChildrenCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override UIElement GetVisualChild(int index)
        {
            throw new NotImplementedException();
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            throw new NotImplementedException();
        }

        /*
        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            throw new NotImplementedException();
        }*/
        

        //
        // Summary:
        //     Scrolls the System.Windows.Controls.ScrollContentPresenter content downward
        //     by one line.
        public void LineDown()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls the System.Windows.Controls.ScrollContentPresenter content to the
        //     left by a predetermined amount.
        public void LineLeft()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls the System.Windows.Controls.ScrollContentPresenter content to the
        //     right by a predetermined amount.
        public void LineRight()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls the System.Windows.Controls.ScrollContentPresenter content upward
        //     by one line.
        public void LineUp()
        { 
            throw new NotImplementedException(); 
        }

        //
        // Summary:
        //     Forces content to scroll until the coordinate space of a System.Windows.Media.Visual
        //     object is visible.
        //
        // Parameters:
        //   visual:
        //     The System.Windows.Media.Visual that becomes visible.
        //
        //   rectangle:
        //     The bounding rectangle that identifies the coordinate space to make visible.
        //
        // Returns:
        //     A System.Windows.Rect that represents the visible region.
        public Rect MakeVisible(UIElement visual, Rect rectangle)
        {
            throw new NotImplementedException();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls down within content after a user clicks the wheel button on a mouse.
        public void MouseWheelDown()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls left within content after a user clicks the wheel button on a mouse.
        public void MouseWheelLeft()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls right within content after a user clicks the wheel button on a mouse.
        public void MouseWheelRight()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls up within content after a user clicks the wheel button on a mouse.
        public void MouseWheelUp()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Invoked when an internal process or application calls System.Windows.FrameworkElement.ApplyTemplate(),
        //     which is used to build the visual tree of the current template.
        public override void OnApplyTemplate()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls down within content by one page.
        public void PageDown()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls left within content by one page.
        public void PageLeft()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls right within content by one page.
        public void PageRight()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Scrolls up within content by one page.
        public void PageUp()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Sets the amount of horizontal offset.
        //
        // Parameters:
        //   offset:
        //     The degree to which content is horizontally offset from the containing viewport.
        public void SetHorizontalOffset(double offset)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Sets the amount of vertical offset.
        //
        // Parameters:
        //   offset:
        //     The degree to which content is vertically offset from the containing viewport.
        public void SetVerticalOffset(double offset)
        {
            throw new NotImplementedException();
        }

    }
}