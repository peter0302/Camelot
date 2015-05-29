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
    public class LinearGradientBrush : GradientBrush
    {
        static LinearGradientBrush()
        {

        }

        public LinearGradientBrush()
        {
        }

        public LinearGradientBrush(GradientStopCollection gradientStopCollection)
            : base(gradientStopCollection)
        {
        }

        public LinearGradientBrush(GradientStopCollection gradientStopCollection, double angle)
            : base(gradientStopCollection)
        {
            this.EndPoint = AngleToPoint(angle);
        }

        public LinearGradientBrush(Color startColor, Color endColor, double angle)
        {
            this.EndPoint = AngleToPoint(angle);
            this.GradientStops.Add(new GradientStop(startColor, 0));
            this.GradientStops.Add(new GradientStop(endColor, 1));
        }


        #region Point StartPoint dependency property
       	public static DependencyProperty StartPointProperty = DependencyProperty.Register(  "StartPoint", typeof(Point), typeof(LinearGradientBrush), new PropertyMetadata((Point)new Point(0,0),
                                                               (obj, args) => { ((LinearGradientBrush)obj).OnStartPointChanged(args); }));
       	public Point StartPoint
       	{
            get
            {
                return (Point)GetValue(StartPointProperty);
            }
            set
            {
                SetValue(StartPointProperty, value);
            }
        }
        private void OnStartPointChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region Point EndPoint dependency property
       	public static DependencyProperty EndPointProperty = DependencyProperty.Register(  "EndPoint", typeof(Point), typeof(LinearGradientBrush), new PropertyMetadata((Point)new Point(1,1),
                                                               (obj, args) => { ((LinearGradientBrush)obj).OnEndPointChanged(args); }));
       	public Point EndPoint
       	{
            get
            {
                return (Point)GetValue(EndPointProperty);
            }
            set
            {
                SetValue(EndPointProperty, value);
            }
        }
        private void OnEndPointChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        private static Point AngleToPoint (double angle)
        {
            angle = angle * 3.141592653589 / 180;
            return new Point(Math.Cos(angle), Math.Sin(angle));
        }

    }
}