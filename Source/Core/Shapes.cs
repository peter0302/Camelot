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
using System.ComponentModel;

namespace Camelot.Core
{
	public class Shape : FrameworkElement
    {
        public Shape () : base() 
        {            
        }            
        
        #region double StrokeThickness dependency property
       	public static DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(  "StrokeThickness", typeof(double), typeof(Shape), new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsRender,
                                                               (obj, args) => { ((Shape)obj).OnStrokeThicknessChanged(args); }));
       	public double StrokeThickness
       	{
            get
            {
                return (double)GetValue(StrokeThicknessProperty);
            }
            set
            {
                SetValue(StrokeThicknessProperty, value);
            }
        }
        private void OnStrokeThicknessChanged(DependencyPropertyChangedEventArgs args)
        {
            this.View.Invalidate();
        }
        #endregion
        
        #region Brush Fill dependency property
       	public static DependencyProperty FillProperty = DependencyProperty.Register(  "Fill", typeof(Brush), typeof(Shape), new FrameworkPropertyMetadata((Brush)null, FrameworkPropertyMetadataOptions.AffectsRender));
       	public Brush Fill
       	{
            get
            {
                return (Brush)GetValue(FillProperty);
            }
            set
            {
                SetValue(FillProperty, value);
            }
        }
        #endregion
        
        #region Brush Stroke dependency property
       	public static DependencyProperty StrokeProperty = DependencyProperty.Register(  "Stroke", typeof(Brush), typeof(Shape), new FrameworkPropertyMetadata((Brush)null, FrameworkPropertyMetadataOptions.AffectsRender,
                                                               (obj, args) => { ((Shape)obj).OnStrokeChanged(args); }));
       	public Brush Stroke
       	{
            get
            {
                return (Brush)GetValue(StrokeProperty);
            }
            set
            {
                SetValue(StrokeProperty, value);
            }
        }
        private void OnStrokeChanged(DependencyPropertyChangedEventArgs args)
        {
            this.View.Invalidate();
        }
        #endregion


    }


    [HasPlatformView]
    public sealed class Ellipse : Shape
    {
        public Ellipse() : base() { } 
  
    }

    [HasPlatformView]
    public sealed class Rectangle : Shape
    {
        public Rectangle() : base() { }
        
        #region double RadiusX dependency property
       	public static DependencyProperty RadiusXProperty = DependencyProperty.Register(  "RadiusX", typeof(double), typeof(Rectangle), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((Rectangle)obj).OnRadiusXChanged(args); }));
       	public double RadiusX
       	{
            get
            {
                return (double)GetValue(RadiusXProperty);
            }
            set
            {
                SetValue(RadiusXProperty, value);
            }
        }
        private void OnRadiusXChanged(DependencyPropertyChangedEventArgs args)
        {
            this._View.Invalidate();
        }
        #endregion
        
        #region double RadiusY dependency property
       	public static DependencyProperty RadiusYProperty = DependencyProperty.Register(  "RadiusY", typeof(double), typeof(Rectangle), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((Rectangle)obj).OnRadiusYChanged(args); }));
       	public double RadiusY
       	{
            get
            {
                return (double)GetValue(RadiusYProperty);
            }
            set
            {
                SetValue(RadiusYProperty, value);
            }
        }
        private void OnRadiusYChanged(DependencyPropertyChangedEventArgs args)
        {
            this._View.Invalidate();
        }
        #endregion


    }


    [HasPlatformView]
    public sealed class Path : Shape
	{
		public Path() : base() { }

	}


    [HasPlatformView]
    public sealed class Line : Shape
    {
		public Line() : base() { }


        
        #region double X1 dependency property
       	public static DependencyProperty X1Property = DependencyProperty.Register(  "X1", typeof(double), typeof(Line), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((Line)obj).OnX1Changed(args); }));
       	public double X1
       	{
            get
            {
                return (double)GetValue(X1Property);
            }
            set
            {
                SetValue(X1Property, value);
            }
        }
        private void OnX1Changed(DependencyPropertyChangedEventArgs args)
        {
            ResetFrame();
            this.View.Invalidate();
        }
        #endregion

        #region double Y1 dependency property
       	public static DependencyProperty Y1Property = DependencyProperty.Register(  "Y1", typeof(double), typeof(Line), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((Line)obj).OnY1Changed(args); }));
       	public double Y1
       	{
            get
            {
                return (double)GetValue(Y1Property);
            }
            set
            {
                SetValue(Y1Property, value);
            }
        }
        private void OnY1Changed(DependencyPropertyChangedEventArgs args)
        {
            ResetFrame();
            this.View.Invalidate();
        }
        #endregion
        
        #region double X2 dependency property
       	public static DependencyProperty X2Property = DependencyProperty.Register(  "X2", typeof(double), typeof(Line), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((Line)obj).OnX2Changed(args); }));
       	public double X2
       	{
            get
            {
                return (double)GetValue(X2Property);
            }
            set
            {
                SetValue(X2Property, value);
            }
        }
        private void OnX2Changed(DependencyPropertyChangedEventArgs args)
        {
            ResetFrame();
            this.View.Invalidate();
        }
        #endregion
        
        #region double Y2 dependency property
       	public static DependencyProperty Y2Property = DependencyProperty.Register(  "Y2", typeof(double), typeof(Line), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((Line)obj).OnY2Changed(args); }));
       	public double Y2
       	{
            get
            {
                return (double)GetValue(Y2Property);
            }
            set
            {
                SetValue(Y2Property, value);
            }
        }
        private void OnY2Changed(DependencyPropertyChangedEventArgs args)
        {
            ResetFrame();
            this.View.Invalidate();
        }
        #endregion



        private void ResetFrame()
        {
            this.View.Bounds = new Rect(this.View.Bounds.X, this.View.Bounds.Y, 
                Math.Min(X1,X2) + Math.Abs(X2-X1),
                Math.Min(Y1,Y2) + Math.Abs(Y2-Y1));
        }


    }






}

