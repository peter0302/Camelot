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
    public class Grid : Panel
    {        
        #region int Row attached property
       	public static DependencyProperty RowProperty = DependencyProperty.RegisterAttached (  "Row", typeof(int), typeof(Grid), new PropertyMetadata((int)0, OnRowChanged));
        public static int GetRow (UIElement element)
        {
            return (int)element.GetValue(RowProperty);
        }
        public static void SetRow(UIElement element, int value)
        {
            element.SetValue(RowProperty, value);
        }
        private static void OnRowChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region int Column attached property
        public static DependencyProperty ColumnProperty = DependencyProperty.RegisterAttached("Column", typeof(int), typeof(Grid), new PropertyMetadata((int)0, OnColumnChanged));
        public static int GetColumn(UIElement element)
        {
            return (int)element.GetValue(ColumnProperty);
        }
        public static void SetColumn(UIElement element, int value)
        {
            element.SetValue(ColumnProperty, value);
        }
        private static void OnColumnChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region int RowSpan attached property
        public static DependencyProperty RowSpanProperty = DependencyProperty.RegisterAttached("RowSpan", typeof(int), typeof(Grid), new PropertyMetadata((int)1, OnRowSpanChanged));
        public static int GetRowSpan(UIElement element)
        {
            return (int)element.GetValue(RowSpanProperty);
        }
        public static void SetRowSpan(UIElement element, int value)
        {
            element.SetValue(RowSpanProperty, value);
        }
        private static void OnRowSpanChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region int ColumnSpan attached property
        public static DependencyProperty ColumnSpanProperty = DependencyProperty.RegisterAttached("ColumnSpan", typeof(int), typeof(Grid), new PropertyMetadata((int)1, OnColumnSpanChanged));
        public static int GetColumnSpan(UIElement element)
        {
            return (int)element.GetValue(ColumnSpanProperty);
        }
        public static void SetColumnSpan(UIElement element, int value)
        {
            element.SetValue(ColumnSpanProperty, value);
        }
        private static void OnColumnSpanChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        ColumnDefinitionCollection _ColumnDefinitions = new ColumnDefinitionCollection();
        public ColumnDefinitionCollection ColumnDefinitions
        {
            get
            {
                return _ColumnDefinitions;
            }
        }

        RowDefinitionCollection _RowDefinitions = new RowDefinitionCollection();
        public RowDefinitionCollection RowDefinitions
        {
            get
            {
                return _RowDefinitions;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {            
            Size sizeAvailableForStars = finalSize - this.DesiredSize;
            sizeAvailableForStars.Width = Math.Max(sizeAvailableForStars.Width, 0);
            sizeAvailableForStars.Height = Math.Max(sizeAvailableForStars.Height, 0);
            ComputeSizesFromStarValues(sizeAvailableForStars);

            foreach ( FrameworkElement child in this.Children )
            {
                int row = Math.Min(this.RowDefinitions.Count - 1, Grid.GetRow(child));
                int column = Math.Min(this.ColumnDefinitions.Count - 1, Grid.GetColumn(child));
                int rowSpan = Grid.GetRowSpan(child);
                int columnSpan = Grid.GetColumnSpan(child);

                Point pt = GetCellOrigin(column, row);
                Size size = GetArrangeSize(column,row,columnSpan,rowSpan);
                child.Arrange(child.Clip = new Rect(pt, size));
            }

            return ComputeTotalUsedSize();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            EnsureMinimumRowsAndColumns();
           
            Size fixedSize = SetAllFixedSizeDimensions();
            SetAllAutoSizeDimensions(availableSize - fixedSize);

            return ComputeTotalUsedSize();         
        }

        private Size GetArrangeSize (int columnIndex, int rowIndex, int columnSpan, int rowSpan)
        {
            Size result = new Size();
            int endColumn = Math.Min(this.ColumnDefinitions.Count, columnIndex + columnSpan);
            int endRow = Math.Min(this.RowDefinitions.Count, rowIndex + rowSpan);
            for (int i = columnIndex; i < endColumn; i++)
            {
                result.Width += ((ColumnDefinition)this.ColumnDefinitions[i]).ActualWidth;
            }
            for (int j = rowIndex; j < endRow; j++)
            {
                result.Height += ((RowDefinition)this.RowDefinitions[j]).ActualHeight;
            }
            return result;
        }

        private Point GetCellOrigin (int columnIndex, int rowIndex)
        {
            Point pt = new Point();
            for (int i = 0; i < columnIndex; i++ )
            {
                pt.X += ((ColumnDefinition)this.ColumnDefinitions[i]).ActualWidth;
            }
            for (int j = 0; j < rowIndex; j++)
            {
                pt.Y += ((RowDefinition)this.RowDefinitions[j]).ActualHeight;
            }
            return pt;
        }

        private void ComputeSizesFromStarValues(Size totalStarSize)
        {
            Size totalStarValues = ComputeTotalStarValues();
            foreach (ColumnDefinition column in this.ColumnDefinitions)
            {
                if ( column.Width.IsStar )
                {
                    column.ActualWidth = totalStarSize.Width * column.Width.Value / totalStarValues.Width;
                }
            }
            foreach (RowDefinition row in this.RowDefinitions)
            {
                if ( row.Height.IsStar )
                {
                    row.ActualHeight = totalStarSize.Height * row.Height.Value / totalStarValues.Height;
                }
            }            
        }

        private Size ComputeTotalStarValues()
        {
            Size result = new Size();
            foreach (ColumnDefinition column in this.ColumnDefinitions)
            {
                if (column.Width.IsStar)
                    result.Width += column.Width.Value;
            }
            foreach (RowDefinition row in this.RowDefinitions)
            {
                if (row.Height.IsStar)
                    result.Height += row.Height.Value;
            }
            return result;
        }

        private Size ComputeTotalUsedSize ()
        {
            Size result = new Size();
            foreach (ColumnDefinition column in this.ColumnDefinitions)
            {
                result.Width += column.ActualWidth;
            }
            foreach (RowDefinition row in this.RowDefinitions)
            {
                result.Height += row.ActualHeight;
            }
            return result;
        }

        /// <summary>
        /// Cycles through all the children of the grid, measures them, and if they are
        /// in cells that have auto widths or heights, adjusts the width or 
        /// height of the row or column 
        /// </summary>
        /// <returns></returns>
        private void SetAllAutoSizeDimensions(Size availableSize)
        {
            foreach ( UIElement child in this.Children )
            {
                child.Measure(availableSize);
                Size desiredSize = child.DesiredSize;

                int columnIndex = Math.Min(this.ColumnDefinitions.Count - 1, Grid.GetColumn(child));
                int rowIndex = Math.Min(this.RowDefinitions.Count - 1, Grid.GetRow(child));

                var column = (ColumnDefinition)this.ColumnDefinitions[columnIndex];
                var row = (RowDefinition)this.RowDefinitions[rowIndex];
                if ( column.Width.IsAuto && Grid.GetColumnSpan(child) == 1)
                {
                    column.ActualWidth = Math.Max(column.ActualWidth, desiredSize.Width);
                }
                if ( row.Height.IsAuto && Grid.GetRowSpan(child) == 1)
                {
                    row.ActualHeight = Math.Max(row.ActualHeight, desiredSize.Height);
                }
            }
        }

        /// <summary>
        /// Cycles through all rows and columns in the grid and computes the total size used
        /// up by fixed size cells
        /// </summary>
        /// <returns></returns>
        private Size SetAllFixedSizeDimensions ()
        {
            Size result = new Size();
            foreach ( ColumnDefinition column in this.ColumnDefinitions )
            {
                if (column.Width.IsAbsolute)
                {
                    result.Width += (column.ActualWidth = column.Width.Value);
                }
                else
                {
                    column.ActualWidth = 0;
                }
            }
            foreach ( RowDefinition row in this.RowDefinitions )
            {
                if (row.Height.IsAbsolute)
                {
                    result.Height += (row.ActualHeight = row.Height.Value);
                }
                else
                {
                    row.ActualHeight = 0;
                }
            }
            return result;
        }

        private Size CalculateCellSize (int column, int row)
        {
            Size result = new Size();
            GridLength setWidth = ((ColumnDefinition)this.ColumnDefinitions[column]).Width;
            GridLength setHeight = ((RowDefinition)this.RowDefinitions[row]).Height;

            return result;
        }


        private Size[,] _SizeMatrix = null;
        private void CreateSizeMatrix ()
        {
            _SizeMatrix = new Size[this.ColumnDefinitions.Count, this.RowDefinitions.Count];
/*            for (int i = 0; i < this.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < this.RowDefinitions.Count; j++)
                {
                    _SizeMatrix[i, j] = new Size(double.NaN, double.NaN);
                }
            }        */
        }

        private void EnsureMinimumRowsAndColumns()
        {
            if ( this.RowDefinitions.Count == 0 )
                this.RowDefinitions.Add(new RowDefinition());
            if ( this.ColumnDefinitions.Count == 0 )
                this.ColumnDefinitions.Add(new ColumnDefinition ());
        }


    }




}