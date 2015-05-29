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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.GLKit;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;

using Camelot.Core;


namespace Camelot.iOS
{
    [PlatformView(typeof(View), typeof(FrameworkElement))]
    public class View : UIView, IPlatformView
    {
        public View () : base()
        {
            //WireUpGestureRecognizers();
            WireUpLongPressGestureRecognizer();
            this.Layer.AnchorPoint = new PointF(0, 0);
            this.BackgroundColor = UIColor.Clear;
        }



        FrameworkElement _Element = null;
        public FrameworkElement Element
        {
            get 
            {
                return _Element;
            }
            set
            {
                _Element = value;
                OnFrameworkElementSet(value);
            }
        }

        protected virtual void OnFrameworkElementSet (UIElement newElement)
        {

        }

        public void Invalidate()
        {
            this.SetNeedsDisplay();
        }

        public virtual new Rect Bounds
        {
            get
            {
                return this.Frame.ToFrameworkRect();
            }
            set
            {
                this.Frame = value.ToPlatformRect();
            }
        }

        public void SetBounds(Rect bounds)
        {
            this.Frame = bounds.ToPlatformRect();
        }


        public void AddChild(IPlatformView child)
        {
            this.AddSubview(child as UIView);
        }

        public void RemoveFromParent()
        {
            this.RemoveFromSuperview();
            //this.RemoveFromParent();
        }

        public Camelot.Core.Point GetRelativePoint(object pointerArgs)
        {
            UITouch touch = pointerArgs as UITouch;
            if (touch == null)
                throw new ArgumentException("pointerArgs");

            return touch.LocationInView(this).ToFrameworkPoint();
        }

        public override sealed void Draw(RectangleF rect)
        {
            base.Draw(rect);

            using ( CGContext g = UIGraphics.GetCurrentContext() )
            {
                bool shouldClip = !((FrameworkElement)Element).Clip.IsEmpty;
                if (shouldClip)
                {
                    g.SaveState();

                    Rect clipRect = ((FrameworkElement)Element).Clip;
                    Rect clipRectViewCoords = clipRect.Translate(-this.Frame.Left, -this.Frame.Top);
                    g.AddRect(clipRectViewCoords.ToPlatformRect());
                    g.Clip();
                }
                Paint(g, rect);
                if (shouldClip) 
                    g.RestoreState();
            }
        }

        protected virtual void Paint ( CGContext g, RectangleF rect )
        {

        }

        bool _PossibleTap = false;
        bool _PointerIsInView = false;
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            
            _PointerIsInView = true;
            _PossibleTap = true;
            UITouch touch = touches.AnyObject as UITouch;

            var args = new PointerInputEventArgs(UIElement.PointerPressedEvent, this.Element, touch);
            Element.OnPointerPressed(args);
            // Only raise event once, even though internally keep track
            if (touch.View == this)
            {
                Element.RaiseEvent(args);
            }   
             
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            UITouch touch = touches.AnyObject as UITouch;

            bool isTouchInView = IsTouchInView(touch);

            if (isTouchInView && !_PointerIsInView)
            {
                PointerInputEventArgs args = new PointerInputEventArgs(UIElement.PointerEnteredEvent, this.Element, touch);
                Element.OnPointerEntered(args);
                if ( touch.View == this)
                {
                    Element.RaiseEvent(args);
                }
                _PointerIsInView = true;
            }
            else if (!isTouchInView && _PointerIsInView)
            {
                PointerInputEventArgs args = new PointerInputEventArgs(UIElement.PointerExitedEvent, this.Element, touch); 
                Element.OnPointerExited(args);
                if ( touch.View == this)
                {
                    Element.RaiseEvent(args);
                }
                _PointerIsInView = false;
                _PossibleTap = false;
            }

            // Only raise event once
            PointerInputEventArgs moveArgs = new PointerInputEventArgs(UIElement.PointerMovedEvent, this.Element, touch); 
            Element.OnPointerMoved(moveArgs);
            if (touch.View == this)
                Element.RaiseEvent(moveArgs);
        }

        /*
        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            UITouch touch = touches.AnyObject as UITouch;
            if (touch.View != this)
                return;            
        }*/


        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            //base.TouchesEnded(touches, evt);            

            UITouch touch = touches.AnyObject as UITouch;
            //if (touch.View != this) return;

            if (IsTouchInView(touch))
            {
                if (_PossibleTap)
                {
                    PointerInputEventArgs args = new PointerInputEventArgs(UIElement.TappedEvent, this.Element, touch);
                    Element.OnTapped(args);
                    if (touch.View == this)
                        Element.RaiseEvent(args);
                }
                PointerInputEventArgs releaseArgs = new PointerInputEventArgs(UIElement.PointerReleasedEvent, this.Element, touch); 
                Element.OnPointerReleased(releaseArgs);
                if ( touch.View == this )
                {
                    Element.RaiseEvent(releaseArgs);
                }
            }

            _PossibleTap = false;
            _PointerIsInView = false;
        }

        protected UIPanGestureRecognizer _PanGesture = null;
        protected UIPinchGestureRecognizer _PinchGesture = null;
        protected UITapGestureRecognizer _TapGesture = null;
        protected UITapGestureRecognizer _DoubleTapGesture = null;
        protected UILongPressGestureRecognizer _LongPressGesture = null;

        private void Initialize()
        {
            //this._Transform = new MultitouchTransform();
            this.Layer.AnchorPoint = new PointF(0, 0);
            this.Frame = new RectangleF(0, 0, 0, 0);
        }


        private enum ManipulationState
        {
            NotManipulating,
            PossibleStart,
            Manipulating
        }


        public void WireUpGestureRecognizers()
        {
            WireUpPanGestureRecognizer();
            WireUpPinchGestureRecognizer();
            
            //WireUpDoubleTapGestureRecognizer();
            //WireUpTapGestureRecognizer();
        }
        
        public void RemoveGestureRecognizers()
        {
            if (_PanGesture != null)
                this.RemoveGestureRecognizer(_PanGesture);
            if (_PinchGesture != null)
                this.RemoveGestureRecognizer(_PinchGesture);
            
        }
    

        double _ManipulationPreviousScale = 1;
        PointF _ManipulationCumulative;
        int _ManipulationPreviousTouches = 0;

        protected void WireUpPinchGestureRecognizer()
        {
            _PinchGesture = new UIPinchGestureRecognizer(() =>
            {
                if ((this.Element.ManipulationMode & ManipulationModes.Scale) == 0)
                    return;

                PointF manipulationCurrentView = _PinchGesture.LocationInView(this);
                PointF manipulationCurrentGlobal = _PinchGesture.LocationInView(null);
                if (_PinchGesture.State == UIGestureRecognizerState.Began)
                {
                    _ManipulationCumulative = new PointF(0, 0);
                    _ManipulationPreviousTouches = _PinchGesture.NumberOfTouches;
                    _ManipulationOriginView = manipulationCurrentView;
                    _ManipulationOriginGlobal = manipulationCurrentGlobal;
                    _ManipulationLastGlobal = _ManipulationOriginGlobal;
                    _ManipulationPreviousScale = 1;
                    ManipulationStartedRoutedEventArgs startArgs = new ManipulationStartedRoutedEventArgs(UIElement.ManipulationStartedEvent, this);
                    startArgs.Position = _ManipulationOriginView.ToFrameworkPoint();
                    startArgs.PointerDeviceType = PointerDeviceType.Touch;
                    Element.RaiseEvent(startArgs);
                }

                PointF manipulationDelta = (_ManipulationPreviousTouches == _PinchGesture.NumberOfTouches) ?
                                                        new PointF(manipulationCurrentGlobal.X - _ManipulationLastGlobal.X,
                                                                    manipulationCurrentGlobal.Y - _ManipulationLastGlobal.Y) :
                                                        new PointF(0, 0);
                _ManipulationPreviousTouches = _PinchGesture.NumberOfTouches;

                _ManipulationCumulative.X += manipulationDelta.X;
                _ManipulationCumulative.Y += manipulationDelta.Y;
                _ManipulationLastGlobal = manipulationCurrentGlobal;

                double manipulationDeltaScale = _PinchGesture.Scale / _ManipulationPreviousScale;
                _ManipulationPreviousScale = _PinchGesture.Scale;

                ManipulationEventArgs args;
                if (_PinchGesture.State == UIGestureRecognizerState.Ended)
                {
                    args = new ManipulationEventArgs(UIElement.ManipulationCompletedEvent, this);
                    args.Type = ManipulationType.Completed;
                }
                else
                {
                    args = new ManipulationEventArgs(UIElement.ManipulationDeltaEvent, this);
                    args.Type = ManipulationType.Delta;
                }
                args.ScreenOrigin = manipulationCurrentGlobal.ToFrameworkPoint();
                args.Origin = manipulationCurrentView.ToFrameworkPoint();
                args.CumulativeScaleX = args.CumulativeScaleY = _PinchGesture.Scale;
                args.CumulativeX = _ManipulationCumulative.X;
                args.CumulativeY = _ManipulationCumulative.Y;
                args.DeltaX = manipulationDelta.X;
                args.DeltaY = manipulationDelta.Y;
                args.ScaleDeltaX = args.ScaleDeltaY = manipulationDeltaScale;
                Element.RaiseEvent(args);

            });
            this.AddGestureRecognizer(_PinchGesture);
        }





        PointF _ManipulationOriginView;
        PointF _ManipulationOriginGlobal;
        PointF _ManipulationLastGlobal;

        protected void WireUpPanGestureRecognizer()
        {
            _PanGesture = new UIPanGestureRecognizer();
            _PanGesture.AddTarget(() =>
            {
                if (((int)this.Element.ManipulationMode & (int)(ManipulationModes.TranslateX)) == 0 &&
                    ((int)this.Element.ManipulationMode & (int)(ManipulationModes.TranslateY)) == 0)
                    return;

                // If it's just began, cache the location of the image
                if (_PanGesture.State == UIGestureRecognizerState.Began)
                {
                    _ManipulationOriginView = _PanGesture.LocationInView(this);
                    _ManipulationOriginGlobal = _PanGesture.LocationInView(null);
                    _ManipulationLastGlobal = _ManipulationOriginGlobal;

                    ManipulationStartedRoutedEventArgs startArgs = new ManipulationStartedRoutedEventArgs(UIElement.ManipulationStartedEvent, this.Element);
                    startArgs.Position = _ManipulationOriginView.ToFrameworkPoint();
                    startArgs.PointerDeviceType = PointerDeviceType.Touch;
                    Element.OnManipulationStarted(startArgs);
                }
                else
                {
                    PointF manipulationCumulative = _PanGesture.TranslationInView(null);
                    PointF manipulationCurrentView = _PanGesture.LocationInView(this);
                    PointF manipulationCurrentGlobal = _PanGesture.LocationInView(null);
                    PointF manipulationDelta = new PointF(manipulationCurrentGlobal.X - _ManipulationLastGlobal.X,
                                                          manipulationCurrentGlobal.Y - _ManipulationLastGlobal.Y);
                    _ManipulationLastGlobal = manipulationCurrentGlobal;

                    //ManipulationEventArgs args;
                    if (_PanGesture.State == UIGestureRecognizerState.Ended)
                    {
                        ManipulationCompletedRoutedEventArgs args = new ManipulationCompletedRoutedEventArgs(UIElement.ManipulationCompletedEvent, this.Element);
                        args.Position = manipulationCurrentView.ToFrameworkPoint();
                        args.Cumulative = new ManipulationDelta { Translation = manipulationCumulative.ToFrameworkPoint() };
                        Element.OnManipulationCompleted(args);
                        //args.Cumulative.Translation = manipulationCumulative.ToFrameworkPoint();
                        //args.Origin = manipulationCurrentView.ToFrameworkPoint();
                        //args.ScreenOrigin = manipulationCurrentGlobal.ToFrameworkPoint();
                        //args.CumulativeX = manipulationCumulative.X;
                        //args.CumulativeY = manipulationCumulative.Y;
                    
                        //args = new ManipulationEventArgs(UIElement.ManipulationCompletedEvent, this.Element);
                      //  args.Type = ManipulationType.Completed;
                    }
                    else
                    {
                        ManipulationDeltaRoutedEventArgs args = new ManipulationDeltaRoutedEventArgs(UIElement.ManipulationDeltaEvent, this.Element);
                        args.Delta = new ManipulationDelta { Translation = manipulationDelta.ToFrameworkPoint() };
                        args.Cumulative = new ManipulationDelta { Translation = manipulationCumulative.ToFrameworkPoint() };
                        args.Position = manipulationCurrentView.ToFrameworkPoint();
                        Element.OnManipulationDelta(args);
                        //args = new ManipulationEventArgs(UIElement.ManipulationDeltaEvent, this.Element);
                        //args.Type = ManipulationType.Delta;
                        //args.DeltaX = manipulationDelta.X;
                        //args.DeltaY = manipulationDelta.Y;
                    }
                    //args.Origin = manipulationCurrentView.ToFrameworkPoint();
                    //args.ScreenOrigin = manipulationCurrentGlobal.ToFrameworkPoint();
                    //args.CumulativeX = manipulationCumulative.X;
                    //args.CumulativeY = manipulationCumulative.Y;
                    //Element.RaiseEvent(args);
                }
            });
            //_PanGesture.RequireGestureRecognizerToFail (_PinchGesture);
            this.AddGestureRecognizer(_PanGesture);
        }



        protected void WireUpLongPressGestureRecognizer()
        {
            _LongPressGesture = new UILongPressGestureRecognizer(() =>
            {
                PointerInputEventArgs args = new PointerInputEventArgs(UIElement.HoldingEvent, this.Element, _LongPressGesture);
                args.Type = InputType.Touch;
                Element.OnHolding(args);
            });
            if ( _PanGesture != null )
                _LongPressGesture.RequireGestureRecognizerToFail(_PanGesture);
            _LongPressGesture.MinimumPressDuration = 1;
            _LongPressGesture.NumberOfTouchesRequired = 1;
            _LongPressGesture.CancelsTouchesInView = false;
            this.AddGestureRecognizer(_LongPressGesture);
        }


        /*
        protected void WireUpDoubleTapGestureRecognizer()
        {
            _DoubleTapGesture = new UITapGestureRecognizer(() =>
            {
                PointerInputEventArgs args = new PointerInputEventArgs(UIElement.DoubleTappedEvent, this, _DoubleTapGesture);
                args.Type = InputType.Touch;
                UIElement.RaiseEvent(args);
            });

            _DoubleTapGesture.CancelsTouchesInView = false;
            _DoubleTapGesture.DelaysTouchesEnded = false;

            if ( _LongPressGesture != null )
                _DoubleTapGesture.RequireGestureRecognizerToFail(_LongPressGesture);
            _DoubleTapGesture.NumberOfTouchesRequired = 1;
            _DoubleTapGesture.NumberOfTapsRequired = 2;
            this.AddGestureRecognizer(_DoubleTapGesture);
        }



        protected void WireUpTapGestureRecognizer()
        {
            _TapGesture = new UITapGestureRecognizer(() =>
            {
                PointerInputEventArgs args = new PointerInputEventArgs(UIElement.TappedEvent, this, _TapGesture);
                args.Type = InputType.Touch;
                UIElement.RaiseEvent(args);
            });
            if ( _DoubleTapGesture != null )
                _TapGesture.RequireGestureRecognizerToFail(_DoubleTapGesture);
            _TapGesture.NumberOfTouchesRequired = 1;
            _TapGesture.NumberOfTapsRequired = 1;
            this.AddGestureRecognizer(_TapGesture);
        }*/

 

        private bool IsTouchInView(UITouch touch)
        {
            PointF pt = touch.LocationInView(this);
            if (pt.X < 0 || pt.Y < 0)
                return false;  // this one's easy
            else if (pt.X > this.Frame.Width || pt.Y > this.Frame.Height)
                return false;
            else
                return true;
        }


    }



    /*

    public class ImageView : View
    {

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            _ImageView.Frame = new RectangleF(0, 0, this.Frame.Width, this.Frame.Height);
            this.AddSubview(_ImageView);
        }

        string _URISource;
        [Export("URISource"), Browsable(true)]
        public string URISource
        {
            get
            {
                return _URISource;
            }
            set
            {
                _URISource = value;
                _ImageView.Image = UIImage.FromBundle(_URISource);
                OnPropertyChanged("URISource");
            }
        }
    }



    public class Line : View
    {

        public override void Draw(Rect rect)
        {

        }
    }



*/
}
