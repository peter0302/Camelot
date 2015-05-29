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
    public class Thumb : Control
    {
        Point _DragStartPoint;
        Point _DragLastPoint;
        double _LastDeltaX;
        double _LastDeltaY;

        public static readonly RoutedEvent DragCompletedEvent = EventManager.RegisterRoutedEvent("DragCompleted", RoutingStrategy.Bubble, typeof(DragCompletedEventHandler), typeof(Thumb));
        public event DragCompletedEventHandler DragCompleted
        {
            add
            {
                this.AddHandler(DragCompletedEvent, value, false);
            }
            remove
            {
                this.RemoveHandler(DragCompletedEvent, value);
            }
        }

        public static readonly RoutedEvent DragDeltaEvent = EventManager.RegisterRoutedEvent("DragDelta", RoutingStrategy.Bubble, typeof(DragDeltaEventHandler), typeof(Thumb));
        public event DragDeltaEventHandler DragDelta
        {
            add
            {
                this.AddHandler(DragDeltaEvent, value, false);
            }
            remove
            {
                this.RemoveHandler(DragDeltaEvent, value);
            }
        }

        public static readonly RoutedEvent DragStartedEvent = EventManager.RegisterRoutedEvent("DragStarted", RoutingStrategy.Bubble, typeof(DragStartedEventHandler), typeof(Thumb));
        public event DragStartedEventHandler DragStarted
        {
            add
            {
                this.AddHandler(DragStartedEvent, value, false);
            }
            remove
            {
                this.RemoveHandler(DragStartedEvent, value);
            }
        }


        
        #region bool IsDragging dependency property
       	public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.Register(  "IsDragging", typeof(bool), typeof(Thumb), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((Thumb)obj).OnDraggingChanged(args); }));
        //
        // Summary:
        //     Responds to a change in the value of the System.Windows.Controls.Primitives.Thumb.IsDragging
        //     property.
        //
        // Parameters:
        //   e:
        //     The event data.
        protected virtual void OnDraggingChanged(DependencyPropertyChangedEventArgs e)
        {

        }
       	public bool IsDragging
       	{
            get
            {
                return (bool)GetValue(IsDraggingProperty);
            }
            protected set
            {
                SetValue(IsDraggingProperty, value);
            }
        }
        #endregion


        // Summary:
        //     Initializes a new instance of the System.Windows.Controls.Primitives.Thumb
        //     class.
        public Thumb()
        {
                       
        }



        // Summary:
        //     Cancels a drag operation for the System.Windows.Controls.Primitives.Thumb.
        public void CancelDrag()
        {
            this.IsDragging = false;
            RaiseEvent(new DragCompletedEventArgs(0, 0, true));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnStyleChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnStyleChanged(args);
        }

        protected internal override void OnPointerPressed(PointerInputEventArgs e)
        {
            //base.OnPointerPressed(e);
            this._DragStartPoint = this._DragLastPoint = e.GetPoint(this.VisualParent.View);
            Point offsetPt = e.GetPoint(this.View);                        
            this.IsDragging = true;            
            RaiseEvent(new DragStartedEventArgs(offsetPt.X, offsetPt.Y));
        }

        protected internal override void OnPointerMoved(PointerInputEventArgs e)
        {
            //base.OnPointerMoved(e);
            Point ptNewPoint = e.GetPoint(this.VisualParent.View);
            _LastDeltaX = ptNewPoint.X - _DragLastPoint.X;
            _LastDeltaY = ptNewPoint.Y - _DragLastPoint.Y;
            _DragLastPoint = ptNewPoint;
            RaiseEvent(new DragDeltaEventArgs(_LastDeltaX, _LastDeltaY));
        }

        protected internal override void OnPointerReleased(PointerInputEventArgs e)
        {
            //base.OnPointerReleased(e);
            Point ptNewPoint = e.GetPoint(this.VisualParent.View);
            this.IsDragging = false;
            RaiseEvent(new DragCompletedEventArgs(ptNewPoint.X - _DragStartPoint.X, ptNewPoint.Y - _DragStartPoint.Y, false));
        }

    }
}