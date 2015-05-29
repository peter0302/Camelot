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

namespace Camelot.Core
{
    public class ColumnDefinition : DependencyObject
    {

        #region GridLength Width dependency property
        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(GridLength), typeof(ColumnDefinition), new PropertyMetadata(GridLength.Default,
                                                               (obj, args) => { ((ColumnDefinition)obj).OnWidthChanged(args); }));
        public GridLength Width
        {
            get
            {
                return (GridLength)GetValue(WidthProperty);
            }
            set
            {
                SetValue(WidthProperty, value);
            }
        }
        private void OnWidthChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double MaxWidth dependency property
        public static readonly DependencyProperty MaxWidthProperty = DependencyProperty.Register("MaxWidth", typeof(double), typeof(ColumnDefinition), new PropertyMetadata((double)double.NaN,
                                                               (obj, args) => { ((ColumnDefinition)obj).OnMaxWidthChanged(args); }));
        public double MaxWidth
        {
            get
            {
                return (double)GetValue(MaxWidthProperty);
            }
            set
            {
                SetValue(MaxWidthProperty, value);
            }
        }
        private void OnMaxWidthChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double MinWidth dependency property
        public static readonly DependencyProperty MinWidthProperty = DependencyProperty.Register("MinWidth", typeof(double), typeof(ColumnDefinition), new PropertyMetadata((double)double.NaN,
                                                               (obj, args) => { ((ColumnDefinition)obj).OnMinWidthChanged(args); }));
        public double MinWidth
        {
            get
            {
                return (double)GetValue(MinWidthProperty);
            }
            set
            {
                SetValue(MinWidthProperty, value);
            }
        }
        private void OnMinWidthChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ActualWidth dependency property
        private static readonly DependencyPropertyKey ActualWidthPropertyKey = DependencyProperty.RegisterReadOnly("ActualWidth", typeof(double), typeof(ColumnDefinition), new PropertyMetadata((double)0.0,
                                                               (obj, args) => { ((ColumnDefinition)obj).OnActualWidthChanged(args); }));
        public double ActualWidth
        {
            get
            {
                return (double)GetValue(ActualWidthPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ActualWidthPropertyKey, value);
            }
        }
        private void OnActualWidthChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        


    }

    public class RowDefinition : DependencyObject
    {
        #region GridLength Height dependency property
        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(GridLength), typeof(RowDefinition), new PropertyMetadata(GridLength.Default,
                                                               (obj, args) => { ((RowDefinition)obj).OnHeightChanged(args); }));
        public GridLength Height
        {
            get
            {
                return (GridLength)GetValue(HeightProperty);
            }
            set
            {
                SetValue(HeightProperty, value);
            }
        }
        private void OnHeightChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double MaxHeight dependency property
        public static readonly DependencyProperty MaxHeightProperty = DependencyProperty.Register("MaxHeight", typeof(double), typeof(RowDefinition), new PropertyMetadata((double)double.NaN,
                                                               (obj, args) => { ((RowDefinition)obj).OnMaxHeightChanged(args); }));
        public double MaxHeight
        {
            get
            {
                return (double)GetValue(MaxHeightProperty);
            }
            set
            {
                SetValue(MaxHeightProperty, value);
            }
        }
        private void OnMaxHeightChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double MinHeight dependency property
        public static readonly DependencyProperty MinHeightProperty = DependencyProperty.Register("MinHeight", typeof(double), typeof(RowDefinition), new PropertyMetadata((double)double.NaN,
                                                               (obj, args) => { ((RowDefinition)obj).OnMinHeightChanged(args); }));
        public double MinHeight
        {
            get
            {
                return (double)GetValue(MinHeightProperty);
            }
            set
            {
                SetValue(MinHeightProperty, value);
            }
        }
        private void OnMinHeightChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double ActualHeight dependency property
        private static readonly DependencyPropertyKey ActualHeightPropertyKey = DependencyProperty.RegisterReadOnly("ActualHeight", typeof(double), typeof(RowDefinition), new PropertyMetadata((double)0.0,
                                                               (obj, args) => { ((RowDefinition)obj).OnActualHeightChanged(args); }));
        public double ActualHeight
        {
            get
            {
                return (double)GetValue(ActualHeightPropertyKey.DependencyProperty);
            }
            internal set
            {
                SetValue(ActualHeightPropertyKey, value);
            }
        }
        private void OnActualHeightChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

    }

    public sealed class ColumnDefinitionCollection : DependencyObjectCollection<ColumnDefinition, List<ColumnDefinition>>
    {
        internal Grid Parent { get; set; }

        protected override void OnCollectionChanged()
        {
            if ( Parent != null )
                Parent.InvalidateMeasure();
            base.OnCollectionChanged();
        }
    }

    public sealed class RowDefinitionCollection : DependencyObjectCollection<RowDefinition, List<RowDefinition>>
    {
        internal Grid Parent { get; set; }

        protected override void OnCollectionChanged()
        {
            if ( Parent != null )
                Parent.InvalidateMeasure();
            base.OnCollectionChanged();
        }
    }

    public enum GridUnitType
    {
        Auto,
        Pixel,
        Star
    }

    public struct GridLength
    {
        public static GridLength Default
        {
            get
            {
                return new GridLength(1, GridUnitType.Star);
            }
        }



        /// <summary>
        /// Initializes a new instance of GridLength as an absolute value in pixels.
        /// </summary>
        /// <param name="length"></param>
        public GridLength(double length)
            : this()
        {
            this.Value = length;
            if (double.IsNaN(length))
                this.GridUnitType = Core.GridUnitType.Auto;
            else
                this.GridUnitType = Core.GridUnitType.Pixel;
        }

        /// <summary>
        /// Initializes a new instance of GridLength and specifies what kind of value it holds.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="type"></param>
        public GridLength(double length, GridUnitType type)
            : this()
        {
            this.Value = length;
            this.GridUnitType = type;
        }

        /// <summary>
        /// Gets the associated GridUnitType for this instance of GridLength.
        /// </summary>
        public GridUnitType GridUnitType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value that indicates whether an instance of GridLength holds an absolute value.
        /// </summary>
        public bool IsAbsolute
        {
            get
            {
                return this.GridUnitType == Core.GridUnitType.Pixel;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether an instance of GridLength holds an automatic value.
        /// </summary>
        public bool IsAuto
        {
            get
            {
                return this.GridUnitType == Core.GridUnitType.Auto;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether an instance of GridLength holds a Star value.
        /// </summary>
        public bool IsStar
        {
            get
            {
                return this.GridUnitType == Core.GridUnitType.Star;
            }
        }

        /// <summary>
        /// Gets a Double that represents the value of this instance of GridLength.
        /// </summary>
        public double Value
        {
            get;
            private set;
        }

        public static GridLength FromString(string s)
        {
            if (s == "Auto")
                return new GridLength(double.NaN, GridUnitType.Auto);
            else if (s == "*")
                return new GridLength(1.0, GridUnitType.Star);
            else if (s.Contains("*"))
            {
                string number = s.LeftOf('*');
                double result = double.NaN;
                double.TryParse(number, out result);
                return new GridLength(result, GridUnitType.Star);
            }
            else
            {
                double result = double.NaN;
                double.TryParse(s, out result);
                return new GridLength(result);
            }
        }
    }
}