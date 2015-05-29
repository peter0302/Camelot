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
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using Camelot.Core.Internal;

namespace Camelot.Core
{
	public enum HorizontalAlignment
	{
		Center,
		Left,
		Right,
		Stretch
	}

	public enum VerticalAlignment
	{
		Center,
		Top,
		Bottom,
		Stretch
	}

    [HasPlatformView]
	public class FrameworkElement : UIElement, ISupportInitialize
	{        
		public FrameworkElement() : base()
		{
            CreateView();
		}
        
        public DependencyObject Parent
        {
            get;
            internal set;
        }



        ResourceDictionary _Resources = new ResourceDictionary();
        public ResourceDictionary Resources
        {
            get { return _Resources; }
            set { _Resources = value; }
        }

        protected internal bool HasLogicalChildren
        {
            get;
            set;
        }

        bool _IsInitialized;
        public bool IsInitialized
        {
            get 
            { 
                return _IsInitialized; 
            }
            private set
            {
                _IsInitialized = value;                    
            }
        }


        public event DependencyPropertyChangedEventHandler DataContextChanged;
        public event EventHandler Initialized;

        #region Loaded event
        public static readonly RoutedEvent LoadedEvent = EventManager.RegisterRoutedEvent("Loaded", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(FrameworkElement));
        public event RoutedEventHandler Loaded
        {
            add
            {
                AddHandler(LoadedEvent, value, false);
            }
            remove
            {
                RemoveHandler(LoadedEvent, value);
            }
        }
        #endregion



        
        #region FrameworkElement TemplatedParent dependency property
       	public static DependencyProperty TemplatedParentProperty = DependencyProperty.Register(  "TemplatedParent", typeof(FrameworkElement), typeof(FrameworkElement), new FrameworkPropertyMetadata((FrameworkElement)null, FrameworkPropertyMetadataOptions.Inherits,
                                                               (obj, args) => { ((FrameworkElement)obj).OnTemplatedParentChanged(args); }));
       	public FrameworkElement TemplatedParent
       	{
            get
            {
                return (FrameworkElement)GetValue(TemplatedParentProperty);
            }
            set
            {
                SetValue(TemplatedParentProperty, value);
            }
        }
        protected virtual void OnTemplatedParentChanged(DependencyPropertyChangedEventArgs args)
        {
            this.ReevaluateAllBindings(true);
            VisualTreeEnumerator tree = new VisualTreeEnumerator(this);
            foreach ( FrameworkElement element in tree )
            {
                element.ReevaluateAllBindings(true);
            }    
        }
        #endregion

        #region double ActualWidth readonly dependency property
        private static readonly DependencyPropertyKey ActualWidthPropertyKey = DependencyProperty.RegisterReadOnly("ActualWidth", typeof(double), typeof(FrameworkElement), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((FrameworkElement)obj).OnActualWidthChanged(args); }));
        public double ActualWidth
        {
            get
            {
                return (double)GetValue(ActualWidthPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ActualWidthPropertyKey.DependencyProperty, value);
            }
        }
        private void OnActualWidthChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ActualHeight readonly dependency property
        private static readonly DependencyPropertyKey ActualHeightPropertyKey = DependencyProperty.RegisterReadOnly("ActualHeight", typeof(double), typeof(FrameworkElement), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((FrameworkElement)obj).OnActualHeightChanged(args); }));
        public double ActualHeight
        {
            get
            {
                return (double)GetValue(ActualHeightPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ActualHeightPropertyKey.DependencyProperty, value);
            }
        }
        private void OnActualHeightChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region string Name dependency property
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(FrameworkElement), new PropertyMetadata((string)"",
                                                               (obj, args) => { ((FrameworkElement)obj).OnNameChanged(args); }));
       	public string Name
       	{
            get
            {
                return (string)GetValue(NameProperty);
            }
            set
            {
                SetValue(NameProperty, value);
            }
        }
        private void OnNameChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region object DataContext dependency property
        public static readonly DependencyProperty DataContextProperty = DependencyProperty.Register("DataContext", typeof(object), typeof(FrameworkElement), new FrameworkPropertyMetadata( (object)null, FrameworkPropertyMetadataOptions.Inherits,
                                                               (obj, args) => { ((FrameworkElement)obj).OnDataContextChanged(args); }));
       	public object DataContext
       	{
            get
            {
                return (object)GetValue(DataContextProperty);
            }
            set
            {
                SetValue(DataContextProperty, value);
            }
        }
        private void OnDataContextChanged( DependencyPropertyChangedEventArgs args)
        {
            this.ReevaluateAllBindings(true);
            VisualTreeEnumerator tree = new VisualTreeEnumerator(this);
            foreach ( FrameworkElement element in tree )
            {
                element.ReevaluateAllBindings(true);
                if (element.DataContextChanged != null)
                    element.DataContextChanged(this, args);
            }                   
        }
        #endregion
        
        #region double Height dependency property
        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(double), typeof(FrameworkElement), new FrameworkPropertyMetadata((double)double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                               (obj, args) => { ((FrameworkElement)obj).OnHeightChanged(args); }));
       	public double Height
       	{
            get
            {
                return (double)GetValue(HeightProperty);
            }
            set
            {
                SetValue(HeightProperty, value);
            }
        }
        private void OnHeightChanged(DependencyPropertyChangedEventArgs args)
        {
        }
        #endregion
        
        #region double Width dependency property
        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(FrameworkElement), new FrameworkPropertyMetadata((double)double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                               (obj, args) => { ((FrameworkElement)obj).OnWidthChanged(args); }));
       	public double Width
       	{
            get
            {
                return (double)GetValue(WidthProperty);
            }
            set
            {
                SetValue(WidthProperty, value);
            }
        }
        private void OnWidthChanged(DependencyPropertyChangedEventArgs args)
        {
        }
        #endregion
        
        #region HorizontalAlignment HorizontalAlignment dependency property
        public static readonly DependencyProperty HorizontalAlignmentProperty = DependencyProperty.Register("HorizontalAlignment", typeof(HorizontalAlignment), typeof(FrameworkElement), new FrameworkPropertyMetadata((HorizontalAlignment)HorizontalAlignment.Stretch, FrameworkPropertyMetadataOptions.AffectsArrange,
                                                               (obj, args) => { ((FrameworkElement)obj).OnHorizontalAlignmentChanged(args); }));
       	public HorizontalAlignment HorizontalAlignment
       	{
            get
            {
                return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            }
            set
            {
                SetValue(HorizontalAlignmentProperty, value);
            }
        }
        private void OnHorizontalAlignmentChanged(DependencyPropertyChangedEventArgs args)
        {
        }
        #endregion
        
        #region VerticalAlignment VerticalAlignment dependency property
        public static readonly DependencyProperty VerticalAlignmentProperty = DependencyProperty.Register("VerticalAlignment", typeof(VerticalAlignment), typeof(FrameworkElement), new FrameworkPropertyMetadata((VerticalAlignment)VerticalAlignment.Stretch, FrameworkPropertyMetadataOptions.AffectsArrange,
                                                               (obj, args) => { ((FrameworkElement)obj).OnVerticalAlignmentChanged(args); }));
       	public VerticalAlignment VerticalAlignment
       	{
            get
            {
                return (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            }
            set
            {
                SetValue(VerticalAlignmentProperty, value);
            }
        }
        private void OnVerticalAlignmentChanged(DependencyPropertyChangedEventArgs args)
        {
            
        }
        #endregion
        
        #region Thickness Margin dependency property
        public static readonly DependencyProperty MarginProperty = DependencyProperty.Register("Margin", typeof(Thickness), typeof(FrameworkElement), new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                               (obj, args) => { ((FrameworkElement)obj).OnMarginChanged(args); }));
        public Thickness Margin
       	{
            get
            {
                return (Thickness)GetValue(MarginProperty);
            }
            set
            {
                SetValue(MarginProperty, value);
            }
        }
        private void OnMarginChanged(DependencyPropertyChangedEventArgs args)
        {
            InvalidateMeasure();
        }
        #endregion
        
        #region Style Style dependency property
       	public static DependencyProperty StyleProperty = DependencyProperty.Register(  "Style", typeof(Style), typeof(FrameworkElement), new PropertyMetadata((Style)null,
                                                               (obj, args) => { ((FrameworkElement)obj).OnStyleChanged(args); }));
       	public Style Style
       	{
            get
            {
                return (Style)GetValue(StyleProperty);
            }
            set
            {
                SetValue(StyleProperty, value);
            }
        }
        protected virtual void OnStyleChanged(DependencyPropertyChangedEventArgs args)
        {
            if (  args.NewValue != null )
            {
                this.ApplyStyle();
            }
        }
        #endregion

        #region Rect Clip dependency property
        public static DependencyProperty ClipProperty = DependencyProperty.Register("Clip", typeof(Rect), typeof(FrameworkElement), new FrameworkPropertyMetadata((Rect)Rect.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        public Rect Clip
        {
            get
            {
                return (Rect)GetValue(ClipProperty);
            }
            set
            {
                SetValue(ClipProperty, value);
            }
        }
        private void OnClipChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


        public void BeginInit ()
        {
            this.IsInitialized = false;
        }

        public void EndInit()
        {
            this.IsInitialized = true;
            OnInitialized(new EventArgs());
        }

        public object FindResource(object key)
        {
            FrameworkElement current = this;
            while (current != null)
            {
                if (current.Resources.Contains(key))
                    return current.Resources[key];
                current = (FrameworkElement)current.VisualParent;
            }

            if (current == null)
            {
                // try to find resource in standard dictionary
                if (ResourceDictionary.StandardDictionary.Contains(key))
                    return ResourceDictionary.StandardDictionary[key];
            }

            return null;
        }

        /// <summary>
        /// This method has no default implementation.
        /// </summary>
        public virtual void OnApplyTemplate()
        {
            
        }

        internal void CompleteLoad()
        {
            this.RaiseEvent(new RoutedEventArgs(LoadedEvent, this));
            VisualTreeEnumerator vte = new VisualTreeEnumerator(this);
            foreach (FrameworkElement elem in vte )
            {
                elem.RaiseEvent(new RoutedEventArgs(LoadedEvent, elem));
            }
        }

        protected virtual void OnInitialized ( EventArgs e )
        {
            if (Initialized != null)
                Initialized(this, e);
        }

        public object FindName (string name)
        {
            for (int i = 0; i < this.VisualChildrenCount; i++)
            {
                UIElement child = this.GetVisualChild(i);
                if (child is FrameworkElement)
                {
                    if ( ((FrameworkElement)child).Name == name)
                        return child;
                    else 
                    {
                        var result = ((FrameworkElement)child).FindName(name);
                        if (result != null) return result;
                    }
                }
            }
            return null;                    
        }


        internal void ApplyStyle()
        {
            if (this.Style == null) return;
            ApplyStyleSetters();
            ApplyStyleTriggers();
        }

        private void ApplyStyleTriggers()
        {
            foreach (Trigger trigger in this.Style.Triggers)
            {
                DependencyObject source;
                if ( trigger.SourceName == null || trigger.SourceName == "" )
                {
                    // trigger source is object
                    source = this;
                }
                else
                {
                    throw new NotImplementedException();
                }
                source.DependencyPropertyChanged += trigger.OnSourcePropertyChanged;
                trigger.OnSourcePropertyChanged(source, new DependencyPropertyChangedEventArgs(
                    trigger.Property,
                    source.GetValue(trigger.Property),
                    source.GetValue(trigger.Property) ));
            }
        }


        private void ApplyStyleSetters()
        {
            foreach (Setter setter in this.Style.Setters)
            {
                // Style controls unless property has been explicitly set
                if (this.ReadLocalValue(setter.Property) == DependencyProperty.UnsetValue)
                {
                    setter.SetValue(this);
                }
            }
        }

        internal void ApplyAllStyles()
        {
            VisualTreeEnumerator tree = new VisualTreeEnumerator(this);
            foreach ( FrameworkElement fe in tree )
            {
                if ( fe.Style != null )
                {
                    fe.ApplyStyle();
                }
                else
                {
                    Style style = (Style)this.FindResource(fe.GetType());
                    if (style != null)
                    {
                        fe.Style = style;
                    }
                }
                if (fe is Control && ((Control)fe).Template != null)
                {
                    ((Control)fe).ApplyTemplate();
                }
            }
        }

        protected internal void AddLogicalChild (object child)
        {
            if (child is FrameworkElement)
            {
                ((FrameworkElement)child).Parent = this;
                this.HasLogicalChildren = true;
            }
        }

        protected internal virtual IEnumerator LogicalChildren
        {
            get
            {
                return null;
            }
        }

        protected internal void RemoveLogicalChild (object child)
        {
            if (child is FrameworkElement && ((FrameworkElement)child).Parent == this )
            {
                ((FrameworkElement)child).Parent = null;
            }
            IEnumerator logicalChildren = this.LogicalChildren;
            if ( logicalChildren == null )
            {
                this.HasLogicalChildren = false;
            }
            else
            {
                this.HasLogicalChildren = logicalChildren.MoveNext();
            }
        }

		protected virtual Size MeasureOverride(Camelot.Core.Size availableSize)
		{
            if (this.VisualChildrenCount != 0)
            {
                UIElement child = GetVisualChild(0);
                child.Measure(availableSize);
                return child.DesiredSize;
            }
            else
            {
                return new Size(0, 0);
            }
		}

		protected virtual Size ArrangeOverride (Size finalSize)
		{
			if (this.VisualChildrenCount != 0)
			{
				GetVisualChild(0).Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
			}
			return finalSize;
		}

		protected override sealed Size MeasureCore(Size availableSize)
		{            
			base.MeasureCore (availableSize);

            if (this.Visibility == Visibility.Collapsed)
                return new Size(0, 0);

            Size measureResult = new Size();

            if ( !double.IsNaN(this.Width) )
            {
                measureResult.Width = this.Width;
                availableSize.Width = this.Width;
            }
            if ( !double.IsNaN(this.Height) )
            {
                measureResult.Height = this.Height;
                availableSize.Height = this.Height;
            }

			var childMeasure = MeasureOverride (availableSize);
            if ( double.IsNaN(this.Width) ) // this element's width is determined by its children
                measureResult.Width = childMeasure.Width;
            if (double.IsNaN(this.Height)) // this element's height is determined by its children
                measureResult.Height = childMeasure.Height;

			measureResult.Width += this.Margin.Left + this.Margin.Right;
			measureResult.Height += this.Margin.Top + this.Margin.Bottom;

			return measureResult;
		}

		protected override sealed void ArrangeCore (Rect finalRect)
		{
			base.ArrangeCore (finalRect);           

            if (this.Visibility == Visibility.Collapsed)
            {
                this.View.Bounds = new Rect(0, 0, 0, 0);
            }


            Size renderSize = this.GetRenderSize(finalRect);


			Rect finalFrame = new Rect ();
            finalFrame.Width = renderSize.Width;
            finalFrame.Height = renderSize.Height;

			// arrange self within parent
			switch (this.HorizontalAlignment) 
			{
                case HorizontalAlignment.Left:
                    finalFrame.X = (float)finalRect.X;
				    break;
    			case HorizontalAlignment.Right:
                    finalFrame.X = (float)(finalRect.Right - renderSize.Width - this.Margin.Right);
    				break;
    			case HorizontalAlignment.Center:
                    finalFrame.X = finalRect.Left + (float)(finalRect.Width / 2 - renderSize.Width / 2);
    				break;
                case HorizontalAlignment.Stretch:
                    if (double.IsNaN(this.Width))
                    {
                        finalFrame.X = (float)finalRect.X;
                    }
                    else
                    {
                        finalFrame.X = Math.Max(0, finalRect.Left + (float)(finalRect.Width / 2 - renderSize.Width / 2));
                    }
    				break;
			}

			switch (this.VerticalAlignment) 
			{
    			case VerticalAlignment.Top:
                    finalFrame.Y = (float)finalRect.Y;
    				break;
    			case VerticalAlignment.Bottom:
                    finalFrame.Y = (float)(finalRect.Bottom - renderSize.Height - this.Margin.Bottom);
    				break;
    			case VerticalAlignment.Center:
                    finalFrame.Y = finalRect.Top + (float)(finalRect.Height / 2 - renderSize.Height / 2);
    				break;
                case VerticalAlignment.Stretch:
                    if (double.IsNaN(this.Height))
                    {
                        finalFrame.Y = (float)finalRect.Y;
                    }
                    else
                    {
                        finalFrame.Y = Math.Max(0, finalRect.Top + (float)(finalRect.Height / 2 - renderSize.Height / 2));
                    }
    				break;
			}

            finalFrame.X += this.Margin.Left;
            finalFrame.Y += this.Margin.Top;

            this.View.Bounds = finalFrame;
            this.View.Invalidate();
            this.ActualWidth = finalFrame.Width;
            this.ActualHeight = finalFrame.Height;

			// Arrange children within self
			ArrangeOverride (new Camelot.Core.Size (finalFrame.Width, finalFrame.Height));
		}

        private Size GetRenderSize(Rect finalRect)
        {
            Size trueDesiredSize = new Size();
            if (this.HorizontalAlignment == HorizontalAlignment.Stretch && double.IsNaN(this.Width))
            {
                trueDesiredSize.Width = finalRect.Width - this.Margin.TotalWidth;
            }
            else
            {
                trueDesiredSize.Width = this.DesiredSize.Width - this.Margin.TotalWidth;                
            }
            if (this.VerticalAlignment == VerticalAlignment.Stretch && double.IsNaN(this.Height))
            {
                trueDesiredSize.Height = finalRect.Height - this.Margin.TotalHeight;
            }
            else
            {
                trueDesiredSize.Height = this.DesiredSize.Height - this.Margin.TotalHeight;
            }
            return trueDesiredSize;
        }


        private object GetInheritedLocalValue(DependencyProperty dp)
        {
            FrameworkElement parent = this.VisualParent as FrameworkElement;
            while (parent != null)
            {
                var result = parent.ReadLocalValue(dp);
                if (result != DependencyProperty.UnsetValue)
                {
                    return result;
                }
                else
                {
                    parent = parent.VisualParent as FrameworkElement;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        private void InvalidateChildrenInheritedProperty (DependencyProperty dp)
        {
            VisualTreeEnumerator tree = new VisualTreeEnumerator(this);
            foreach ( UIElement element in tree )
            {
                element.InvalidateProperty(dp);
            }            
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            ProcessInvalidatedProperty(e.Property);
            base.OnPropertyChanged(e);       
        }


        public override void InvalidateProperty(DependencyProperty dp)
        {
            ProcessInvalidatedProperty(dp);
            base.InvalidateProperty(dp);
        }

        private void ProcessInvalidatedProperty ( DependencyProperty dp )
        {
            if (! (dp.DefaultMetadata is FrameworkPropertyMetadata) )
            {
                return;
            }
            FrameworkPropertyMetadata metadata = (FrameworkPropertyMetadata)dp.DefaultMetadata;

            if (metadata.Inherits)
            {
                InvalidateChildrenInheritedProperty(dp);
            }
            if (metadata.AffectsMeasure)
            {
                this.InvalidateMeasure();
            }
            if ( metadata.AffectsArrange)
            {
                this.InvalidateArrange();
            }
            if (metadata.AffectsRender)
            {
                this.View.Invalidate();
            }
        }

        protected internal override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
        }

        protected override sealed object EvaluateLocalValue(DependencyProperty dp, object value)
        {
            if (value is StaticResource)
            {
                // Since this is a static resource, the first time the result is retrieved, try to
                // evaluate it, and save as the new local value. A dynamic resource would be 
                // retrieved each time.
                var evaledResult = ((StaticResource)value).FindResource((FrameworkElement)this);
                if (evaledResult == null)
                    return DependencyProperty.UnsetValue;
                else
                    return evaledResult;
            }
            else if ( value == DependencyProperty.UnsetValue )
            {
                if (dp.DefaultMetadata is FrameworkPropertyMetadata && ((FrameworkPropertyMetadata)dp.DefaultMetadata).Inherits)
                {
                    var result = GetInheritedLocalValue(dp);
                    if (result != DependencyProperty.UnsetValue)
                        return base.EvaluateLocalValue(dp, result);
                    else
                        return dp.DefaultMetadata.DefaultValue;
                }
                else
                {
                    return base.EvaluateLocalValue(dp, value);
                }
            }
            else
            {
                return base.EvaluateLocalValue(dp, value);
            }
        }

	}
}

