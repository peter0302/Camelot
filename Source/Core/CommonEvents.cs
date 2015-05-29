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
using System.Collections;
using System.Collections.Specialized;

namespace Camelot.Core
{
	public enum ManipulationType
	{
		Starting,
		Delta,
		Completed
	}

	public enum InputType
	{
		Keyboard,
		Mouse,
		Stylus,
		Touch
	}
    
    public class DataTransferEventArgs : RoutedEventArgs
    {
        internal DataTransferEventArgs(RoutedEvent dataTransferEvent, object source, DependencyProperty property, DependencyObject targetObject) : 
                    base(dataTransferEvent, source)
        {
            this.Property = property;
            this.TargetObject = targetObject;
        }

        public DependencyProperty Property
        {
            get;
            private set;
        }

        public DependencyObject TargetObject
        {
            get;
            private set;
        }
    }


	public delegate void PointerEventHandler(object sender, PointerInputEventArgs e);
	public class PointerInputEventArgs : RoutedEventArgs
	{
		public PointerInputEventArgs(object platformArgs) : base(platformArgs) { }

		public PointerInputEventArgs(RoutedEvent routedEvent, object source, object platformArgs) : base(routedEvent, source) 
		{
			_PlatformArgs = platformArgs;
		}


		public Point GetPoint (IPlatformView relativeToPlatformObject)
		{
			//return EventsService.Service.GetRelativeEventPoint(this._PlatformArgs, relativeToPlatformObject);
            return relativeToPlatformObject.GetRelativePoint(this._PlatformArgs);
		}


		public InputType Type { get; set; }
		//public double X { get; set; }
		//public double Y { get; set; }
		public int MouseWheelDelta { get; set; }
		public bool MouseLeftButtonDown { get; set; }
		public bool MouseRightButtonDown { get; set; }
		public bool MouseCenterButtonDown { get; set; }
		public VirtualKeyModifiers KeyModifiers { get; set; }

		public override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
		{
			((PointerEventHandler)genericHandler)(this.Source, this);
		}
	}

	public delegate void ManipulationEventHandler (object sender, ManipulationEventArgs e);
	public class ManipulationEventArgs : RoutedEventArgs
	{
		public ManipulationEventArgs(object platformArgs) : base(platformArgs)
        { 
            ScaleDeltaX = 1;
            ScaleDeltaY = 1;
            CumulativeScaleX = 1;
            CumulativeScaleY = 1;
        }

		public ManipulationEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            ScaleDeltaX = 1;
            ScaleDeltaY = 1;
            CumulativeScaleX = 1;
            CumulativeScaleY = 1;
        }


		//public bool IsOriginTransformed { get; set; }

		public ManipulationType Type {get; set;}
		public Point Origin { get; set; }
		public Point ScreenOrigin { get; set; }
		public double DeltaX { get; set; }
		public double DeltaY { get; set; }
		public double CumulativeX { get; set; }
		public double CumulativeY { get; set; }
		public double ScaleDeltaX { get; set; }
		public double ScaleDeltaY { get; set; }
		public double CumulativeScaleX { get; set; }
		public double CumulativeScaleY { get; set; }
		public double DeltaRotation { get; set; }
		public double CumulativeRotation { get; set; }

		public override void InvokeEventHandler (Delegate genericHandler, object genericTarget)
		{
			((ManipulationEventHandler)genericHandler) (this.Source, this);
		}


	}

    public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);
    public class SelectionChangedEventArgs : RoutedEventArgs
    {
        public SelectionChangedEventArgs(RoutedEvent id, IList removedItems, IList addedItems) : base(id, null)
        {
            this.AddedItems = addedItems;
            this.RemovedItems = removedItems;
        }

        public IList AddedItems { get; private set; }
        public IList RemovedItems { get; private set; }
        public override void InvokeEventHandler(Delegate genericHandler, object genericTarget)    
        {
            ((SelectionChangedEventHandler)genericHandler)(this.Source, this);
        }
    }

    public delegate void ItemsChangedEventHandler( Object sender, ItemsChangedEventArgs e);
    public class ItemsChangedEventArgs : EventArgs
    {
        public NotifyCollectionChangedAction Action { get; internal set; }
        public int ItemCount { get; internal set; }
        public int ItemUICount { get; internal set; }
        //public GeneratorPosition OldPosition { get; internal set;}
        //public GeneratorPosition Position { get; internal set; }
    }


    public delegate void DragStartedEventHandler(object sender, DragStartedEventArgs e);
    // Summary:
    //     Provides information about the System.Windows.Controls.Primitives.Thumb.DragStarted
    //     event that occurs when a user drags a System.Windows.Controls.Primitives.Thumb
    //     control with the mouse..
    public class DragStartedEventArgs : RoutedEventArgs
    {
        // Summary:
        //     Initializes a new instance of the System.Windows.Controls.Primitives.DragStartedEventArgs
        //     class.
        //
        // Parameters:
        //   horizontalOffset:
        //     The horizontal offset of the mouse click with respect to the screen coordinates
        //     of the System.Windows.Controls.Primitives.Thumb.
        //
        //   verticalOffset:
        //     The vertical offset of the mouse click with respect to the screen coordinates
        //     of the System.Windows.Controls.Primitives.Thumb.
        public DragStartedEventArgs(double horizontalOffset, double verticalOffset) : base(Thumb.DragStartedEvent, null)
        {
            this.HorizontalOffset = horizontalOffset;
            this.VerticalOffset = verticalOffset;
        }

        // Summary:
        //     Gets the horizontal offset of the mouse click relative to the screen coordinates
        //     of the System.Windows.Controls.Primitives.Thumb.
        //
        // Returns:
        //     The horizontal offset of the mouse click with respect to the upper-left corner
        //     of the bounding box of the System.Windows.Controls.Primitives.Thumb. There
        //     is no default value.
        public double HorizontalOffset { get; private set; }

        //
        // Summary:
        //     Gets the vertical offset of the mouse click relative to the screen coordinates
        //     of the System.Windows.Controls.Primitives.Thumb.
        //
        // Returns:
        //     The horizontal offset of the mouse click with respect to the upper-left corner
        //     of the bounding box of the System.Windows.Controls.Primitives.Thumb. There
        //     is no default value.
        public double VerticalOffset { get; private set; }

        // Summary:
        //     Converts a method that handles the System.Windows.Controls.Primitives.Thumb.DragStarted
        //     event to the System.Windows.Controls.Primitives.DragStartedEventHandler type.
        //
        // Parameters:
        //   genericHandler:
        //     The event handler delegate.
        //
        //   genericTarget:
        //     The System.Windows.Controls.Primitives.Thumb that uses the handler.
        public override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((DragStartedEventHandler)genericHandler)(this.Source, this);
        }
    }

    public delegate void DragDeltaEventHandler(object sender, DragDeltaEventArgs e);
    // Summary:
    //     Provides information about the System.Windows.Controls.Primitives.Thumb.DragDelta
    //     event that occurs one or more times when a user drags a System.Windows.Controls.Primitives.Thumb
    //     control with the mouse.
    public class DragDeltaEventArgs : RoutedEventArgs
    {
        // Summary:
        //     Initializes a new instance of the System.Windows.Controls.Primitives.DragDeltaEventArgs
        //     class.
        //
        // Parameters:
        //   horizontalChange:
        //     The horizontal change in the System.Windows.Controls.Primitives.Thumb position
        //     since the last System.Windows.Controls.Primitives.Thumb.DragDelta event.
        //
        //   verticalChange:
        //     The vertical change in the System.Windows.Controls.Primitives.Thumb position
        //     since the last System.Windows.Controls.Primitives.Thumb.DragDelta event.
        public DragDeltaEventArgs(double horizontalChange, double verticalChange) : base(Thumb.DragDeltaEvent, null)
        {
            this.HorizontalChange = horizontalChange;
            this.VerticalChange = verticalChange;
        }

        // Summary:
        //     Gets the horizontal distance that the mouse has moved since the previous
        //     System.Windows.Controls.Primitives.Thumb.DragDelta event when the user drags
        //     the System.Windows.Controls.Primitives.Thumb control with the mouse.
        //
        // Returns:
        //     A horizontal change in position of the mouse during a drag operation. There
        //     is no default value.
        public double HorizontalChange { get; private set; }
        //
        // Summary:
        //     Gets the vertical distance that the mouse has moved since the previous System.Windows.Controls.Primitives.Thumb.DragDelta
        //     event when the user drags the System.Windows.Controls.Primitives.Thumb with
        //     the mouse.
        //
        // Returns:
        //     A vertical change in position of the mouse during a drag operation. There
        //     is no default value.
        public double VerticalChange { get; private set; }

        // Summary:
        //     Converts a method that handles the System.Windows.Controls.Primitives.Thumb.DragDelta
        //     event to the System.Windows.Controls.Primitives.DragDeltaEventHandler type.
        //
        // Parameters:
        //   genericHandler:
        //     The event handler delegate.
        //
        //   genericTarget:
        //     The System.Windows.Controls.Primitives.Thumb that uses the handler.
        public override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((DragDeltaEventHandler)genericHandler)(this.Source, this);
        }
    }

    public delegate void DragCompletedEventHandler(object sender, DragCompletedEventArgs e);
    public class DragCompletedEventArgs : RoutedEventArgs
    {
        // Summary:
        //     Initializes a new instance of the System.Windows.Controls.Primitives.DragCompletedEventArgs
        //     class.
        //
        // Parameters:
        //   horizontalChange:
        //     The horizontal change in position of the System.Windows.Controls.Primitives.Thumb
        //     control, resulting from the drag operation.
        //
        //   verticalChange:
        //     The vertical change in position of the System.Windows.Controls.Primitives.Thumb
        //     control, resulting from the drag operation.
        //
        //   canceled:
        //     A Boolean value that indicates whether the drag operation was canceled by
        //     a call to the System.Windows.Controls.Primitives.Thumb.CancelDrag() method.
        public DragCompletedEventArgs(double horizontalChange, double verticalChange, bool canceled)
            : base (Thumb.DragCompletedEvent, null)
        {
            this.HorizontalChange = horizontalChange;
            this.VerticalChange = verticalChange;
            this.Canceled = canceled;
        }

        // Summary:
        //     Gets whether the drag operation for a System.Windows.Controls.Primitives.Thumb
        //     was canceled by a call to the System.Windows.Controls.Primitives.Thumb.CancelDrag()
        //     method.
        //
        // Returns:
        //     true if a drag operation was canceled; otherwise, false.
        public bool Canceled { get; private set;  }

        //
        // Summary:
        //     Gets the horizontal change in position of the System.Windows.Controls.Primitives.Thumb
        //     after the user drags the control with the mouse.
        //
        // Returns:
        //     The horizontal difference between the point at which the user pressed the
        //     left mouse button and the point at which the user released the button during
        //     a drag operation of a System.Windows.Controls.Primitives.Thumb control. There
        //     is no default value.
        public double HorizontalChange { get; private set; }

        //
        // Summary:
        //     Gets the vertical change in position of the System.Windows.Controls.Primitives.Thumb
        //     after the user drags the control with the mouse.
        //
        // Returns:
        //     The vertical difference between the point at which the user pressed the left
        //     mouse button and the point at which the user released the button during a
        //     drag operation of a System.Windows.Controls.Primitives.Thumb control. There
        //     is no default value.
        public double VerticalChange { get; private set; }

        // Summary:
        //     Converts a method that handles the System.Windows.Controls.Primitives.Thumb.DragCompleted
        //     event to the System.Windows.Controls.Primitives.DragCompletedEventHandler
        //     type.
        //
        // Parameters:
        //   genericHandler:
        //     The event handler delegate.
        //
        //   genericTarget:
        //     The System.Windows.Controls.Primitives.Thumb that uses the handler.
        public override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((DragCompletedEventHandler)genericHandler)(this.Source, this);
        }
    }

    // Summary:
    //     Describes the behavior that caused a System.Windows.Controls.Primitives.ScrollBar.Scroll
    //     event for a System.Windows.Controls.Primitives.ScrollBar control.
    public enum ScrollEventType
    {
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb was dragged to a new position
        //     and is now no longer being dragged by the user.
        EndScroll = 0,
        //
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb moved to the System.Windows.Controls.Primitives.RangeBase.Minimum
        //     position of the System.Windows.Controls.Primitives.ScrollBar. For a vertical
        //     System.Windows.Controls.Primitives.ScrollBar, this movement occurs when the
        //     CTRL+HOME keys are pressed. This movement corresponds to a System.Windows.Controls.Primitives.ScrollBar.ScrollToTopCommand
        //     in a vertical System.Windows.Controls.Primitives.ScrollBar and a System.Windows.Controls.Primitives.ScrollBar.ScrollToLeftEndCommand
        //     in a horizontal System.Windows.Controls.Primitives.ScrollBar.
        First = 1,
        //
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb moved a specified distance,
        //     as determined by the value of System.Windows.Controls.Primitives.RangeBase.LargeChange,
        //     to the left for a horizontal System.Windows.Controls.Primitives.ScrollBar
        //     or upward for a vertical System.Windows.Controls.Primitives.ScrollBar. For
        //     a vertical System.Windows.Controls.Primitives.ScrollBar, this movement occurs
        //     when the page button that is above the System.Windows.Controls.Primitives.Thumb
        //     is pressed, or when the PAGE UP key is pressed, and corresponds to a System.Windows.Controls.Primitives.ScrollBar.PageUpCommand.
        //     For a horizontal System.Windows.Controls.Primitives.ScrollBar, this movement
        //     occurs when the page button to the left of the System.Windows.Controls.Primitives.Thumb
        //     is pressed, and corresponds to a System.Windows.Controls.Primitives.ScrollBar.PageLeftCommand.
        LargeDecrement = 2,
        //
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb moved a specified distance,
        //     as determined by the value of System.Windows.Controls.Primitives.RangeBase.LargeChange,
        //     to the right for a horizontal System.Windows.Controls.Primitives.ScrollBar
        //     or downward for a vertical System.Windows.Controls.Primitives.ScrollBar.
        //     For a vertical System.Windows.Controls.Primitives.ScrollBar, this movement
        //     occurs when the page button that is below the System.Windows.Controls.Primitives.Thumb
        //     is pressed, or when the PAGE DOWN key is pressed, and corresponds to a System.Windows.Controls.Primitives.ScrollBar.PageDownCommand.
        //     For a horizontal System.Windows.Controls.Primitives.ScrollBar, this movement
        //     occurs when the page button to the right of the System.Windows.Controls.Primitives.Thumb
        //     is pressed, and corresponds to a System.Windows.Controls.Primitives.ScrollBar.PageRightCommand.
        LargeIncrement = 3,
        //
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb moved to the System.Windows.Controls.Primitives.RangeBase.Maximum
        //     position of the System.Windows.Controls.Primitives.ScrollBar. For a vertical
        //     System.Windows.Controls.Primitives.ScrollBar, this movement occurs when the
        //     CTRL+END keys are pressed. This movement corresponds to a System.Windows.Controls.Primitives.ScrollBar.ScrollToEndCommand
        //     in a vertical System.Windows.Controls.Primitives.ScrollBar and a System.Windows.Controls.Primitives.ScrollBar.ScrollToRightEndCommand
        //     in a horizontal System.Windows.Controls.Primitives.ScrollBar.
        Last = 4,
        //
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb moved a small distance, as determined
        //     by the value of System.Windows.Controls.Primitives.RangeBase.SmallChange,
        //     to the left for a horizontal System.Windows.Controls.Primitives.ScrollBar
        //     or upward for a vertical System.Windows.Controls.Primitives.ScrollBar. For
        //     a vertical System.Windows.Controls.Primitives.ScrollBar, this movement occurs
        //     when the upper System.Windows.Controls.Primitives.RepeatButton is pressed
        //     or when the UP ARROW key is pressed, and corresponds to a System.Windows.Controls.Primitives.ScrollBar.LineUpCommand.
        //     For a horizontal System.Windows.Controls.Primitives.ScrollBar, this movement
        //     occurs when the left System.Windows.Controls.Primitives.RepeatButton is pressed,
        //     and corresponds to a System.Windows.Controls.Primitives.ScrollBar.LineLeftCommand.
        SmallDecrement = 5,
        //
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb moved a small distance, as determined
        //     by the value of System.Windows.Controls.Primitives.RangeBase.SmallChange,
        //     to the right for a horizontal System.Windows.Controls.Primitives.ScrollBar
        //     or downward for a vertical System.Windows.Controls.Primitives.ScrollBar.
        //     For a vertical System.Windows.Controls.Primitives.ScrollBar, this movement
        //     occurs when the lower System.Windows.Controls.Primitives.RepeatButton is
        //     pressed or when the DOWN ARROW key is pressed, and corresponds to a System.Windows.Controls.Primitives.ScrollBar.LineDownCommand.
        //     For a horizontal System.Windows.Controls.Primitives.ScrollBar, this movement
        //     occurs when the right System.Windows.Controls.Primitives.RepeatButton is
        //     pressed, and corresponds to a System.Windows.Controls.Primitives.ScrollBar.LineRightCommand.
        SmallIncrement = 6,
        //
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb moved to a new position because
        //     the user selected Scroll Here in the shortcut menu of the System.Windows.Controls.Primitives.ScrollBar.
        //     This movement corresponds to the System.Windows.Controls.Primitives.ScrollBar.ScrollHereCommand.
        //     To view the shortcut menu, right-click the mouse when the pointer is over
        //     the System.Windows.Controls.Primitives.ScrollBar.
        ThumbPosition = 7,
        //
        // Summary:
        //     The System.Windows.Controls.Primitives.Thumb was dragged and caused a System.Windows.UIElement.MouseMove
        //     event. A System.Windows.Controls.Primitives.ScrollBar.Scroll event of this
        //     System.Windows.Controls.Primitives.ScrollEventType may occur more than one
        //     time when the System.Windows.Controls.Primitives.Thumb is dragged in the
        //     System.Windows.Controls.Primitives.ScrollBar.
        ThumbTrack = 8,
    }

    public delegate void ScrollEventHandler(object sender, ScrollEventArgs e);
    // Summary:
    //     Provides data for a System.Windows.Controls.Primitives.ScrollBar.Scroll event
    //     that occurs when the System.Windows.Controls.Primitives.Thumb of a System.Windows.Controls.Primitives.ScrollBar
    //     moves.
    public class ScrollEventArgs : RoutedEventArgs
    {
        // Summary:
        //     Initializes an instance of the System.Windows.Controls.Primitives.ScrollEventArgs
        //     class by using the specified System.Windows.Controls.Primitives.ScrollEventType
        //     enumeration value and the new location of the System.Windows.Controls.Primitives.Thumb
        //     control in the System.Windows.Controls.Primitives.ScrollBar.
        //
        // Parameters:
        //   scrollEventType:
        //     A System.Windows.Controls.Primitives.ScrollEventType enumeration value that
        //     describes the type of System.Windows.Controls.Primitives.Thumb movement that
        //     caused the event.
        //
        //   newValue:
        //     The value that corresponds to the new location of the System.Windows.Controls.Primitives.Thumb
        //     in the System.Windows.Controls.Primitives.ScrollBar.
        public ScrollEventArgs(ScrollEventType scrollEventType, double newValue) : base()
        {
            this.NewValue = newValue;
            this.ScrollEventType = scrollEventType;
        }

        // Summary:
        //     Gets a value that represents the new location of the System.Windows.Controls.Primitives.Thumb
        //     in the System.Windows.Controls.Primitives.ScrollBar.
        //
        // Returns:
        //     The value that corresponds to the new position of the System.Windows.Controls.Primitives.Thumb
        //     in the System.Windows.Controls.Primitives.ScrollBar.
        public double NewValue { get; private set; }
        //
        // Summary:
        //     Gets the System.Windows.Controls.Primitives.ScrollEventType enumeration value
        //     that describes the change in the System.Windows.Controls.Primitives.Thumb
        //     position that caused this event.
        //
        // Returns:
        //     A System.Windows.Controls.Primitives.ScrollEventType enumeration value that
        //     describes the type of System.Windows.Controls.Primitives.Thumb movement that
        //     caused the System.Windows.Controls.Primitives.ScrollBar.Scroll event.
        public ScrollEventType ScrollEventType { get; private set; }

        // Summary:
        //     Performs the appropriate type casting to call the type-safe System.Windows.Controls.Primitives.ScrollEventHandler
        //     delegate for the System.Windows.Controls.Primitives.ScrollBar.Scroll event.
        //
        // Parameters:
        //   genericHandler:
        //     The event handler to call.
        //
        //   genericTarget:
        //     The current object along the event's route.
        public override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((ScrollEventHandler)genericHandler)(this.Source, this);
        }
    }

}

