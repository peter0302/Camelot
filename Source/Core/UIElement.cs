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
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

using System.Reflection;


namespace Camelot.Core
{
    public enum Visibility
    {
        Visible = 0,
        Collapsed = 1
    }

	public enum ManipulationModes
	{
		None = 0,
		TranslateX = 1,
		TranslateY = 2,
		Rotate = 16,
		Scale = 32,
		All = 65535
	}

    
    public class UIElement : DependencyObject, INotifyPropertyChanged
    {
        protected IPlatformView _View;
        public IPlatformView View
        {
            get { return _View; }
        }

        public enum PlatformTypes
        {
            Unknown,
            Win32,
            WinRT,
            Android,
            iOS
        }


        static PlatformTypes _PlatformType;

        public static PlatformTypes PlatformType
        {
            get
            {
                return _PlatformType;
            }
        }

        static Dictionary<Type, ConstructorInfo> _ViewConstructors = new Dictionary<Type, ConstructorInfo>();


        static UIElement()
        {
#if __IOS__
            _PlatformType = PlatformTypes.iOS;
#elif __WIN32__
            _PlatformType = PlatformTypes.Win32;
#elif __WINRT__
            _PlatformType = PlatformTypes.WinRT;
#elif __ANDROID__
            _PlatformType = PlatformTypes.Android;
#endif
        }

        public UIElement()
        {
            this.LastKnownBounds = new Rect(double.NaN, double.NaN, double.NaN, double.NaN);                        
        }


        protected void CreateView ()
        {
            Type destType = this.GetType();
            while (destType != null )
            {
                if (destType.GetCustomAttribute(typeof(HasPlatformView)) != null)
                    break;
                destType = destType.BaseType;
            }
            if (destType == null)
                throw new Exception("HasPlatformView must be defined for some ancestor.");

            ConstructorInfo viewConstructor;
            if (!_ViewConstructors.TryGetValue(destType, out viewConstructor))
            {
                var asm = Assembly.GetExecutingAssembly();
                foreach (TypeInfo typeInfo in asm.DefinedTypes)
                {
                    PlatformViewAttribute attribute = (PlatformViewAttribute)typeInfo.GetCustomAttribute(typeof(PlatformViewAttribute));
                    if (attribute != null && (attribute.FrameworkType == destType ))
                    {
                        viewConstructor = attribute.PlatformConstructor;
                        _ViewConstructors.Add(destType, attribute.PlatformConstructor);
                        break;
                    }
                }
            }
                
            if (viewConstructor != null)
            {
                _View = (IPlatformView)viewConstructor.Invoke(new object[0]);
                _View.Element = (FrameworkElement)this;
            }
            else
            {
                //throw new Exception("Unable to locate a View object that matches this Framework object");
            }

        }


        
        #region ManipulationModes ManipulationMode dependency property
       	public static DependencyProperty ManipulationModeProperty = DependencyProperty.Register(  "ManipulationMode", typeof(ManipulationModes), typeof(UIElement), new PropertyMetadata((ManipulationModes)ManipulationModes.None,
                                                               (obj, args) => { ((UIElement)obj).OnManipulationModeChanged(args); }));
       	public ManipulationModes ManipulationMode
       	{
            get
            {
                return (ManipulationModes)GetValue(ManipulationModeProperty);
            }
            set
            {
                SetValue(ManipulationModeProperty, value);
            }
        }
        private void OnManipulationModeChanged(DependencyPropertyChangedEventArgs args)
        {
            if ((ManipulationModes)args.NewValue != ManipulationModes.None)
                this.View.WireUpGestureRecognizers();
            else
                this.View.RemoveGestureRecognizers();
        }
        #endregion

        #region double Opacity DependencyProperty
        public static readonly DependencyProperty OpacityProperty = DependencyProperty.Register("Opacity", typeof(double), typeof(UIElement), new PropertyMetadata((double)1, OnOpacityPropertyChanged));       
        public double Opacity
        {
            get
            {
                return (double)GetValue(OpacityProperty);
            }
            set
            {
                SetValue(OpacityProperty, value);
            }
        }
        private static void OnOpacityPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region Visibility Visibility DependencyProperty
        public static readonly DependencyProperty VisibilityProperty = DependencyProperty.Register("Visibility", typeof(Visibility), typeof(UIElement), new PropertyMetadata(Visibility.Visible, OnVisibilityPropertyChanged));
        public Visibility Visibility
        {
            get
            {
                return (Visibility)GetValue(VisibilityProperty);
            }
            set
            {
                SetValue(VisibilityProperty, value);
            }
        }
        private static void OnVisibilityPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((UIElement)o).InvalidateMeasure();
        }
        #endregion

        #region bool IsMeasureValid property
        bool _IsMeasureValid;
		public bool IsMeasureValid
		{
			get
			{
				return _IsMeasureValid;
			}
		}
        #endregion

        #region bool IsArrangeValid property
        bool _IsArrangeValid;
		public bool IsArrangeValid
		{
			get 
			{
				return _IsArrangeValid;
			}
		}
        #endregion

        #region Size DesiredSize property
        Size _DesiredSize;
		public Size DesiredSize
		{
			get 
			{
				return _DesiredSize;
			}
		}
        #endregion

        #region UIElement VisualParent property
        UIElement _VisualParent;
		public UIElement VisualParent
		{
			get
			{
				return _VisualParent;
			}
		}
        #endregion

        #region CompositeTransform RenderTransform dependency property
        public static DependencyProperty RenderTransformProperty = DependencyProperty.Register(  "RenderTransform", typeof(CompositeTransform), typeof(UIElement), new PropertyMetadata((CompositeTransform)null,
                                                               (obj, args) => { ((UIElement)obj).OnRenderTransformChanged(args); }));
       	public CompositeTransform RenderTransform
       	{
            get
            {
                return (CompositeTransform)GetValue(RenderTransformProperty);
            }
            set
            {
                SetValue(RenderTransformProperty, value);
            }
        }
        private void OnRenderTransformChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion




        #region Routed Event Registrations

        #region MouseLeftButtonDown event
        public static RoutedEvent MouseLeftButtonDownEvent = EventManager.RegisterRoutedEvent("MouseLeftButtonDown", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
        public event PointerEventHandler MouseLeftButtonDown
        {
            add
            {
                AddHandler(MouseLeftButtonDownEvent, value, false);
            }
            remove
            {
                RemoveHandler(MouseLeftButtonDownEvent, value);
            }
        }
        #endregion

        #region MouseLeftButtonUp event
        public static RoutedEvent MouseLeftButtonUpEvent = EventManager.RegisterRoutedEvent("MouseLeftButtonUp", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
        public event PointerEventHandler MouseLeftButtonUp
        {
            add
            {
                AddHandler(MouseLeftButtonUpEvent, value, false);
            }
            remove
            {
                RemoveHandler(MouseLeftButtonUpEvent, value);
            }
        }
        #endregion

        #region MouseLeave event
        public static RoutedEvent MouseLeaveEvent = EventManager.RegisterRoutedEvent("MouseLeave", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
        public event PointerEventHandler MouseLeave
        {
            add
            {
                AddHandler(MouseLeaveEvent, value, false);
            }
            remove
            {
                RemoveHandler(MouseLeaveEvent, value);
            }
        }
        #endregion

        #region PointerPressed event
        public static RoutedEvent PointerPressedEvent = EventManager.RegisterRoutedEvent ("PointerPressed", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
		public event PointerEventHandler PointerPressed
		{
			add
			{
				AddHandler(PointerPressedEvent, value, false);
			}
			remove 
			{
				RemoveHandler (PointerPressedEvent, value);
			}
		}
        #endregion

        #region PointerMoved event
        public static RoutedEvent PointerMovedEvent = EventManager.RegisterRoutedEvent("PointerMoved", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
		public event PointerEventHandler PointerMoved
		{
			add
			{
				AddHandler(PointerMovedEvent, value, false);
			}
			remove 
			{
				RemoveHandler (PointerMovedEvent, value);
			}
		}
        #endregion

        #region PointerEntered event
        public static RoutedEvent PointerEnteredEvent = EventManager.RegisterRoutedEvent("PointerEntered", RoutingStrategy.Direct, typeof(PointerEventHandler), typeof(UIElement));
		public event PointerEventHandler PointerEntered
		{
			add
			{
				AddHandler(PointerEnteredEvent, value, false);
			}
			remove 
			{
				RemoveHandler (PointerEnteredEvent, value);
			}
		}
        #endregion

        #region PointerExited event
        public static RoutedEvent PointerExitedEvent = EventManager.RegisterRoutedEvent("PointerExited", RoutingStrategy.Direct, typeof(PointerEventHandler), typeof(UIElement));
		public event PointerEventHandler PointerExited
		{
			add
			{
				AddHandler(PointerExitedEvent, value, false);
			}
			remove 
			{
				RemoveHandler (PointerExitedEvent, value);
			}
		}
        #endregion

        #region PointerReleased event
        public static RoutedEvent PointerReleasedEvent = EventManager.RegisterRoutedEvent ("PointerReleased", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
		public event PointerEventHandler PointerReleased
		{
			add
			{
				AddHandler(PointerReleasedEvent, value, false);
			}
			remove 
			{
				RemoveHandler (PointerReleasedEvent, value);
			}
		}
        #endregion

        #region TappedEvent event
        public static RoutedEvent TappedEvent = EventManager.RegisterRoutedEvent("Tapped", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
        public event PointerEventHandler Tapped
        {
            add
            {
                AddHandler(TappedEvent,value,false);
            }
            remove
            {
                RemoveHandler(TappedEvent, value);
            }
        }
        #endregion

        #region DoubleTapped event
        public static RoutedEvent DoubleTappedEvent = EventManager.RegisterRoutedEvent("DoubleTapped", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
        public event PointerEventHandler DoubleTapped
        {
            add
            {
                AddHandler(DoubleTappedEvent,value,false);
            }
            remove
            {
                RemoveHandler(DoubleTappedEvent, value);
            }
        }
        #endregion

        #region Holding event
        public static RoutedEvent HoldingEvent = EventManager.RegisterRoutedEvent("Holding", RoutingStrategy.Bubble, typeof(PointerEventHandler), typeof(UIElement));
		public event PointerEventHandler Holding
		{
			add
			{
				AddHandler(HoldingEvent, value, false);
			}
			remove
			{
				RemoveHandler (HoldingEvent, value);
			}
		}
        #endregion

        #region ManipulationStarted event
        public static RoutedEvent ManipulationStartedEvent = EventManager.RegisterRoutedEvent("ManipulationStarted", RoutingStrategy.Bubble, typeof(ManipulationStartedEventHandler), typeof(UIElement));
        public event ManipulationStartedEventHandler ManipulationStarted
        {
            add
            {
                AddHandler(ManipulationStartedEvent, value, false);
            }
            remove
            {
                RemoveHandler(ManipulationStartedEvent, value);
            }
        }
        #endregion

        #region ManipulationDelta event
        public static RoutedEvent ManipulationDeltaEvent = EventManager.RegisterRoutedEvent("ManipulationDelta", RoutingStrategy.Bubble, typeof(ManipulationEventHandler), typeof(UIElement));
		public event ManipulationEventHandler ManipulationDelta
		{
			add
			{
				AddHandler(ManipulationDeltaEvent, value, false);				
			}
			remove 
			{
				RemoveHandler (ManipulationDeltaEvent, value);
			}
		}
        #endregion

        #region ManipulationCompleted event
        public static RoutedEvent ManipulationCompletedEvent = EventManager.RegisterRoutedEvent("ManipulationCompleted", RoutingStrategy.Bubble, typeof(ManipulationEventHandler), typeof(UIElement));
		public event ManipulationEventHandler ManipulationCompleted
		{
			add
			{
				AddHandler(ManipulationCompletedEvent, value, false);
			}
			remove 
			{
				RemoveHandler (ManipulationCompletedEvent, value);
			}
		}
        #endregion


        #endregion


        public void AddHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
        {
            _RoutedEventHandlers.Add(routedEvent, new RoutedEventHandlerInfo { Handler = handler, HandledEventsToo = handledEventsToo });
        }

        public void RemoveHandler(RoutedEvent routedEvent, Delegate handler)
        {
            _RoutedEventHandlers.Remove(routedEvent);
        }

        public void RaiseEvent (RoutedEventArgs e)
        {
            RoutedEventHandlerInfo eventInfo = null;
            if (_RoutedEventHandlers.TryGetValue(e.RoutedEvent, out eventInfo))
            {
                e.InvokeEventHandler(eventInfo.Handler, this);
            }

            if (eventInfo == null || e.Handled == false || eventInfo.HandledEventsToo)
            {
                if (e.RoutedEvent.RoutingStrategy == RoutingStrategy.Bubble)
                {
                    if (this.VisualParent != null)
                        this.VisualParent.RaiseEvent(e);
                }
                else if (e.RoutedEvent.RoutingStrategy == RoutingStrategy.Tunnel)
                {
                    for (int i = 0; i < this.VisualChildrenCount; i++)
                    {
                        GetVisualChild(i).RaiseEvent(e);
                    }
                }
                // do nothing if RoutingStrategy.Direct
            }
        }



		public void InvalidateArrange()
		{
			_IsArrangeValid = false;

            LayoutUpdateManager.Add(LayoutUpdateManager.OperationType.Arrange, this);
		}

		public void InvalidateMeasure()
		{
			_IsMeasureValid = false;
            if (this.VisualParent != null)
            {
                this.VisualParent.InvalidateMeasure();
            }
            else
            {
                LayoutUpdateManager.Add(LayoutUpdateManager.OperationType.Measure, this);
            }
			InvalidateArrange();
		}

        Rect _LastKnownBounds;
        public Rect LastKnownBounds
        {
            get
            {
                if (LayoutGeneration == 0)
                    return new Rect(double.NaN, double.NaN, double.NaN, double.NaN);
                else
                    return _LastKnownBounds;
            }
            protected set
            {
                _LastKnownBounds = value;
            }
        }

		public void Arrange (Rect finalRect)
		{
            this.LastKnownBounds = finalRect;
			ArrangeCore (finalRect);
			_IsArrangeValid = true;
		}

		public void Measure (Size availableSize)
		{
			_DesiredSize = MeasureCore (availableSize);
			_IsMeasureValid = true;
            InvalidateArrange();
		}

		protected virtual Size MeasureCore (Camelot.Core.Size availableSize)
		{
			return new Size (0, 0);
		}

		protected virtual void ArrangeCore (Rect finalRect)
		{

		}


        private int _LayoutGeneration = 0;
        public int LayoutGeneration 
        {
            get
            {
                return _LayoutGeneration;
            }
        }

        internal int VirtualChildrenCountInternal
        {
            get
            {
                return this.VisualChildrenCount;
            }
        }

		protected virtual int VisualChildrenCount
		{
			get 
			{
				return 0;
			}
		}

        protected internal virtual void OnVisualParentChanged ( DependencyObject oldParent )
        {

        }

        protected internal virtual void OnVisualChildrenChanged ( DependencyObject visualAdded, DependencyObject visualRemoved )
        {

        }

        protected virtual UIElement GetVisualChild(int index)
		{
			throw new ArgumentOutOfRangeException ();
		}
			
        internal UIElement GetVisualChildInternal (int index)
        {
            return GetVisualChild(index);
        }

		protected void AddVisualChild (UIElement child)
		{
            UIElement oldParent = child._VisualParent;

			child._VisualParent = this;
            child.AddLayoutGenerations(this._LayoutGeneration + 1);

            if ( this.View != null )
                this.View.AddChild(child.View);

            child.InvalidateMeasure();
			InvalidateMeasure ();

            OnVisualChildrenChanged(child, null);
            child.OnVisualParentChanged(oldParent);
		}

        internal void AddVisualChildInternal (UIElement child)
        {
            AddVisualChild(child);
        }

        protected void RemoveVisualChild(UIElement child)
		{
			child._VisualParent = null;
            child.AddLayoutGenerations(-(this._LayoutGeneration+1) );

            if ( this.View != null )
                child.View.RemoveFromParent();

            child.InvalidateMeasure();
			InvalidateMeasure ();

            OnVisualChildrenChanged(null, child);
            child.OnVisualParentChanged(this);
		}

        internal void RemoveVisualChildInternal (UIElement child)
        {
            RemoveVisualChild(child);
        }

        private void AddLayoutGenerations (int generations)
        {
            this._LayoutGeneration += generations;
            for (int i = 0; i < this.VisualChildrenCount; i++)
            {
                UIElement child = GetVisualChild(i);
                child.AddLayoutGenerations(generations);
            }
        }

        private Dictionary<RoutedEvent, RoutedEventHandlerInfo> _RoutedEventHandlers = new Dictionary<RoutedEvent, RoutedEventHandlerInfo>();

        private class RoutedEventHandlerInfo
        {
            public Delegate Handler {get; set;}
            public bool HandledEventsToo {get; set;}
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        protected internal virtual void OnPointerPressed ( PointerInputEventArgs e )
        {
            //this.RaiseEvent(e);
        }

        protected internal virtual void OnPointerEntered ( PointerInputEventArgs e )
        {
            //this.RaiseEvent(e);
        }

        protected internal virtual void OnPointerExited (PointerInputEventArgs e)
        {
            //this.RaiseEvent(e);
        }

        protected internal virtual void OnPointerMoved (PointerInputEventArgs e)
        {
            //this.RaiseEvent(e);
        }

        protected internal virtual void OnManipulationStarted (ManipulationStartedRoutedEventArgs e)
        {
            this.RaiseEvent(e);
        }

        protected internal virtual void OnManipulationDelta (ManipulationDeltaRoutedEventArgs e)
        {
            this.RaiseEvent(e);
        }

        protected internal virtual void OnManipulationCompleted (ManipulationCompletedRoutedEventArgs e)
        {
            this.RaiseEvent(e);
        }

        protected internal virtual void OnPointerReleased (PointerInputEventArgs e)
        {
            //this.RaiseEvent(e);
        }

        protected internal virtual void OnTapped(PointerInputEventArgs e)
        {
            //this.RaiseEvent(e);
        }

        protected internal virtual void OnDoubleTapped(PointerInputEventArgs e)
        {
            this.RaiseEvent(e);
        }

        protected internal virtual void OnHolding(PointerInputEventArgs e)
        {
            this.RaiseEvent(e);
        }
    }
}

