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
    public class ScrollViewer : ContentControl
    {        
        #region bool CanContentScroll dependency property
       	public static DependencyProperty CanContentScrollProperty = DependencyProperty.Register(  "CanContentScroll", typeof(bool), typeof(ScrollViewer), new PropertyMetadata((bool)true,
                                                               (obj, args) => { ((ScrollViewer)obj).OnCanContentScrollChanged(args); }));
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
      
        #region double ContentHorizontalOffset dependency property
       	private static readonly DependencyPropertyKey ContentHorizontalOffsetPropertyKey = DependencyProperty.RegisterReadOnly(  "ContentHorizontalOffset", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnContentHorizontalOffsetChanged(args); }));
       	public double ContentHorizontalOffset
       	{
            get
            {
                return (double)GetValue(ContentHorizontalOffsetPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ContentHorizontalOffsetPropertyKey.DependencyProperty, value);
            }
        }
        private void OnContentHorizontalOffsetChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ContentVerticalOffset dependency property
        private static readonly DependencyPropertyKey ContentVerticalOffsetPropertyKey = DependencyProperty.RegisterReadOnly("ContentVerticalOffset", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnContentVerticalOffsetChanged(args); }));
        public double ContentVerticalOffset
        {
            get
            {
                return (double)GetValue(ContentVerticalOffsetPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ContentVerticalOffsetPropertyKey.DependencyProperty, value);
            }
        }
        private void OnContentVerticalOffsetChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ExtentWidth dependency property
        private static readonly DependencyPropertyKey ExtentWidthPropertyKey = DependencyProperty.RegisterReadOnly("ExtentWidth", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnExtentWidthChanged(args); }));
        public double ExtentWidth
        {
            get
            {
                return (double)GetValue(ExtentWidthPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ExtentWidthPropertyKey.DependencyProperty, value);
            }
        }
        private void OnExtentWidthChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ExtentHeight dependency property
        private static readonly DependencyPropertyKey ExtentHeightPropertyKey = DependencyProperty.RegisterReadOnly("ExtentHeight", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnExtentHeightChanged(args); }));
        public double ExtentHeight
        {
            get
            {
                return (double)GetValue(ExtentHeightPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ExtentHeightPropertyKey.DependencyProperty, value);
            }
        }
        private void OnExtentHeightChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double HorizontalOffset dependency property
        private static readonly DependencyPropertyKey HorizontalOffsetPropertyKey = DependencyProperty.RegisterReadOnly("HorizontalOffset", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnHorizontalOffsetChanged(args); }));
        public double HorizontalOffset
        {
            get
            {
                return (double)GetValue(HorizontalOffsetPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(HorizontalOffsetPropertyKey.DependencyProperty, value);
            }
        }
        private void OnHorizontalOffsetChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double VerticalOffset dependency property
        private static readonly DependencyPropertyKey VerticalOffsetPropertyKey = DependencyProperty.RegisterReadOnly("VerticalOffset", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnVerticalOffsetChanged(args); }));
        public double VerticalOffset
        {
            get
            {
                return (double)GetValue(VerticalOffsetPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(VerticalOffsetPropertyKey.DependencyProperty, value);
            }
        }
        private void OnVerticalOffsetChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region Visibility HorizontalScrollBarVisibility dependency property
       	public static DependencyProperty HorizontalScrollBarVisibilityProperty = DependencyProperty.Register(  "HorizontalScrollBarVisibility", typeof(Visibility), typeof(ScrollViewer), new PropertyMetadata((Visibility)Visibility.Visible,
                                                               (obj, args) => { ((ScrollViewer)obj).OnHorizontalScrollBarVisibilityChanged(args); }));
       	public Visibility HorizontalScrollBarVisibility
       	{
            get
            {
                return (Visibility)GetValue(HorizontalScrollBarVisibilityProperty);
            }
            set
            {
                SetValue(HorizontalScrollBarVisibilityProperty, value);
            }
        }
        private void OnHorizontalScrollBarVisibilityChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
      
        #region Visibility VerticalScrollBarVisibility dependency property
       	public static DependencyProperty VerticalScrollBarVisibilityProperty = DependencyProperty.Register(  "VerticalScrollBarVisibility", typeof(Visibility), typeof(ScrollViewer), new PropertyMetadata((Visibility)Visibility.Visible,
                                                               (obj, args) => { ((ScrollViewer)obj).OnVerticalScrollBarVisibilityChanged(args); }));
       	public Visibility VerticalScrollBarVisibility
       	{
            get
            {
                return (Visibility)GetValue(VerticalScrollBarVisibilityProperty);
            }
            set
            {
                SetValue(VerticalScrollBarVisibilityProperty, value);
            }
        }
        private void OnVerticalScrollBarVisibilityChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region Visibility ComputedHorizontalScrollBarVisibility dependency property
       	public static DependencyProperty ComputedHorizontalScrollBarVisibilityProperty = DependencyProperty.Register(  "ComputedHorizontalScrollBarVisibility", typeof(Visibility), typeof(ScrollViewer), new PropertyMetadata((Visibility)Visibility.Collapsed,
                                                               (obj, args) => { ((ScrollViewer)obj).OnComputedHorizontalScrollBarVisibilityChanged(args); }));
       	public Visibility ComputedHorizontalScrollBarVisibility
       	{
            get
            {
                return (Visibility)GetValue(ComputedHorizontalScrollBarVisibilityProperty);
            }
            private set
            {
                SetValue(ComputedHorizontalScrollBarVisibilityProperty, value);
            }
        }
        private void OnComputedHorizontalScrollBarVisibilityChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region Visibility ComputedVerticalScrollBarVisibility dependency property
       	public static DependencyProperty ComputedVerticalScrollBarVisibilityProperty = DependencyProperty.Register(  "ComputedVerticalScrollBarVisibility", typeof(Visibility), typeof(ScrollViewer), new PropertyMetadata((Visibility)Visibility.Collapsed,
                                                               (obj, args) => { ((ScrollViewer)obj).OnComputedVerticalScrollBarVisibilityChanged(args); }));
       	public Visibility ComputedVerticalScrollBarVisibility
       	{
            get
            {
                return (Visibility)GetValue(ComputedVerticalScrollBarVisibilityProperty);
            }
            set
            {
                SetValue(ComputedVerticalScrollBarVisibilityProperty, value);
            }
        }
        private void OnComputedVerticalScrollBarVisibilityChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ScrollableHeight dependency property
        private static readonly DependencyPropertyKey ScrollableHeightPropertyKey = DependencyProperty.RegisterReadOnly("ScrollableHeight", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnScrollableHeightChanged(args); }));
        public double ScrollableHeight
        {
            get
            {
                return (double)GetValue(ScrollableHeightPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ScrollableHeightPropertyKey.DependencyProperty, value);
            }
        }
        private void OnScrollableHeightChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ScrollableWidth dependency property
        private static readonly DependencyPropertyKey ScrollableWidthPropertyKey = DependencyProperty.RegisterReadOnly("ScrollableWidth", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnScrollableWidthChanged(args); }));
        public double ScrollableWidth
        {
            get
            {
                return (double)GetValue(ScrollableWidthPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ScrollableWidthPropertyKey.DependencyProperty, value);
            }
        }
        private void OnScrollableWidthChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ViewportWidth dependency property
        private static readonly DependencyPropertyKey ViewportWidthPropertyKey = DependencyProperty.RegisterReadOnly("ViewportWidth", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnViewportWidthChanged(args); }));
        public double ViewportWidth
        {
            get
            {
                return (double)GetValue(ViewportWidthPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ViewportWidthPropertyKey.DependencyProperty, value);
            }
        }
        private void OnViewportWidthChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ViewportHeight dependency property
        private static readonly DependencyPropertyKey ViewportHeightPropertyKey = DependencyProperty.RegisterReadOnly("ViewportHeight", typeof(double), typeof(ScrollViewer), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((ScrollViewer)obj).OnViewportHeightChanged(args); }));
        public double ViewportHeight
        {
            get
            {
                return (double)GetValue(ViewportHeightPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ViewportHeightPropertyKey.DependencyProperty, value);
            }
        }
        private void OnViewportHeightChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
    }
}