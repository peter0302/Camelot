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

namespace Camelot.Core
{    
    [HasPlatformView]
    public class Canvas : Panel
    {
        #region double Left attached property
        public static DependencyProperty LeftProperty = DependencyProperty.RegisterAttached("Left", typeof(double), typeof(Panel), new PropertyMetadata((double)0, OnPositionChanged));
        public static double GetLeft(UIElement element)
        {
            if (element == null)
                throw new ArgumentException("element");
            return (double)element.GetValue(LeftProperty);
        }
        public static void SetLeft(UIElement element, double value)
        {
            if (element == null)
                throw new ArgumentException("element");
            element.SetValue(LeftProperty, value);
        }
        #endregion

        #region double Top attached property
        public static DependencyProperty TopProperty = DependencyProperty.RegisterAttached("Top", typeof(double), typeof(Panel), new PropertyMetadata((double)0, OnPositionChanged));
        public static double GetTop(UIElement element)
        {
            if (element == null)
                throw new ArgumentException("element");
            return (double)element.GetValue(TopProperty);
        }
        public static void SetTop(UIElement element, double value)
        {
            if (element == null)
                throw new ArgumentException("element");
            element.SetValue(TopProperty, value);
        }        
        #endregion

        #region double Right attached property
        public static DependencyProperty RightProperty = DependencyProperty.RegisterAttached("Right", typeof(double), typeof(Panel), new PropertyMetadata((double)0, OnPositionChanged));
        public static double GetRight(UIElement element)
        {
            if (element == null)
                throw new ArgumentException("element");
            return (double)element.GetValue(RightProperty);
        }
        public static void SetRight(UIElement element, double value)
        {
            if (element == null)
                throw new ArgumentException("element");
            element.SetValue(RightProperty, value);
        }
        #endregion

        #region double Bottom attached property
        public static DependencyProperty BottomProperty = DependencyProperty.RegisterAttached("Bottom", typeof(double), typeof(Panel), new PropertyMetadata((double)0, OnPositionChanged));
        public static double GetBottom(UIElement element)
        {
            if (element == null)
                throw new ArgumentException("element");
            return (double)element.GetValue(BottomProperty);
        }
        public static void SetBottom(UIElement element, double value)
        {
            if (element == null)
                throw new ArgumentException("element");
            element.SetValue(BottomProperty, value);
        }        
        #endregion

        public Canvas () : base()
        {
        }

        private static void OnPositionChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if ( o is FrameworkElement && ((FrameworkElement)o).Parent is Canvas )
            {
                Canvas parent = (Canvas)((FrameworkElement)o).Parent;
                parent.InvalidateArrange();                
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement element in this.Children)
            {
                if (element != null)
                {
                    double x = 0.0;
                    double y = 0.0;
                    double left = GetLeft(element);
                    if ( !double.IsNaN(left) )
                    {
                        x = left;
                    }
                    else
                    {
                        double right = GetRight(element);
                        if (!double.IsNaN(right))
                        {
                            x = (finalSize.Width - element.DesiredSize.Width) - right;
                        }
                    }
                    double top = GetTop(element);
                    if (!double.IsNaN(top))
                    {
                        y = top;
                    }
                    else
                    {
                        double bottom = GetBottom(element);
                        if (!double.IsNaN(bottom))
                        {
                            y = (finalSize.Height - element.DesiredSize.Height) - bottom;
                        }
                    }
                    element.Arrange(new Rect(new Point(x, y), element.DesiredSize));
                }
            }
            return finalSize;
        }



    }


}
