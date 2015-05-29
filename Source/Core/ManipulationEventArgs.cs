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

namespace Camelot.Core
{
    public delegate void ManipulationStartedEventHandler(object sender, ManipulationStartedRoutedEventArgs e);
    public class ManipulationStartedRoutedEventArgs : InputEventArgs
    {
        // Summary:
        //     Initializes a new instance of the ManipulationStartedRoutedEventArgs class.
        public ManipulationStartedRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            this.Cumulative = new ManipulationDelta
            {
                Scale = 1,
                Expansion = 0,
                Rotation = 0,
                Translation = new Point(0, 0)
            };
            this.Container = source as UIElement;
        }

        // Summary:
        //     Gets the UIElement that is considered the container of the manipulation.
        //
        // Returns:
        //     The UIElement that is considered the container of the manipulation.
        public UIElement Container
        {
            get;
            internal set;
        }
 
        //
        // Summary:
        //     Gets the overall changes since the beginning of the manipulation.
        //
        // Returns:
        //     The overall changes since the beginning of the manipulation.
        public ManipulationDelta Cumulative
        {
            get;
            internal set;
        }

        
        //
        // Summary:
        //     Gets the PointerDeviceType for the pointer device involved in the manipulation.
        //
        // Returns:
        //     A value of the enumeration.      
        public PointerDeviceType PointerDeviceType
        {
            get;
            internal set;
        }

        //
        // Summary:
        //     Gets the point from which the manipulation originated.
        //
        // Returns:
        //     The point from which the manipulation originated.
        public Point Position
        {
            get;
            internal set;
        }

        // Summary:
        //     Completes the manipulation without inertia.
        public void Complete()
        {

        }

        public override void InvokeEventHandler(System.Delegate genericHandler, object genericTarget)
        {
            ((ManipulationStartedEventHandler)genericHandler)(genericTarget , this);
        }
    }

    public delegate void ManipulationDeltaEventHandler (object sender, ManipulationDeltaRoutedEventArgs e);
    public sealed class ManipulationDeltaRoutedEventArgs : InputEventArgs
    {
        public ManipulationDeltaRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {

        }

        public UIElement Container 
        { 
            get; 
            internal set; 
        }

        public ManipulationDelta Cumulative
        { 
            get; 
            internal set; 
        }

        public ManipulationDelta Delta 
        { 
            get; 
            internal set; 
        }


        public bool IsInertial
        {
            get;
            internal set;
        }

        public PointerDeviceType PointerDeviceType
        {
            get;
            internal set;
        }

        public Point Position
        {
            get;
            internal set;
        }

        public ManipulationVelocities Velocities 
        { 
            get; 
            internal set; 
        }


        public void Complete()
        {

        }
    }

    public delegate void ManipulationCompletedEventHandler (object sender, ManipulationCompletedRoutedEventArgs e);
    public sealed class ManipulationCompletedRoutedEventArgs : InputEventArgs
    {
        public ManipulationCompletedRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {

        }

        public UIElement Container
        {
            get;
            internal set;
        }

        public ManipulationDelta Cumulative
        {
            get;
            internal set;
        }


        public bool IsInertial
        {
            get;
            internal set;
        }


        public PointerDeviceType PointerDeviceType
        {
            get;
            internal set;
        }


        public Point Position 
        { 
            get; 
            internal set; 
        }

        public ManipulationVelocities Velocities 
        { 
            get; 
            internal set; 
        }
    }
}