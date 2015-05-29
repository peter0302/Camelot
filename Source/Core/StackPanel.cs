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
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    [HasPlatformView]
    public class StackPanel : Panel
    {
        public StackPanel () : base ()
        {
        }

        #region Orientation Orientation dependency property
       	public static DependencyProperty OrientationProperty = DependencyProperty.Register(  "Orientation", typeof(Orientation), typeof(StackPanel), new PropertyMetadata((Orientation)Orientation.Horizontal,
                                                               (obj, args) => { ((StackPanel)obj).OnOrientationChanged(args); }));
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
        private void OnOrientationChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


        protected override Size MeasureOverride(Size availableSize)
        {
            //return base.MeasureOverride(availableSize);
            bool horizontal = this.Orientation == Orientation.Horizontal;
            Size runningSize = new Size();
            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSize);
                if (horizontal)
                {
                    runningSize.Width += child.DesiredSize.Width;
                    runningSize.Height = Math.Max(runningSize.Height, child.DesiredSize.Height);
                }
                else
                {
                    runningSize.Width = Math.Max(runningSize.Width, child.DesiredSize.Width);
                    runningSize.Height += child.DesiredSize.Height;
                }
            }
            return runningSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            bool horizontal = this.Orientation == Orientation.Horizontal;
            Point runningLocation = new Point();
            foreach (UIElement child in this.Children)
            {
                if (horizontal)
                {
                    child.Arrange ( new Rect (runningLocation.X, runningLocation.Y, child.DesiredSize.Width, finalSize.Height) );
                    runningLocation.X += child.DesiredSize.Width;
                }
                else // vertical
                {
                    child.Arrange (new Rect(runningLocation.X, runningLocation.Y, finalSize.Width, child.DesiredSize.Height));
                    runningLocation.Y += child.DesiredSize.Height;
                }
            }
            return finalSize;
        }
    }
}