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
using System.ComponentModel;

namespace Camelot.Core
{
	public class CompositeTransform : Transform
	{
		private const double _RadianFactor = 0.01745329251994;
		private double _SinTheta = 0;
		private double _CosTheta = 1;

		public CompositeTransform ()
		{
		}

        
		public CompositeTransform Transform
		{
			get 
			{
				return this;
			}
		}

        // NEW to Camelot; not in WPF
        #region bool PreservePosition dependency property
        public static DependencyProperty PreservePositionProperty = DependencyProperty.Register("PreservePosition", typeof(bool), typeof(CompositeTransform), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((CompositeTransform)obj).OnPreservePositionChanged(args); }));
        /// <summary>
        /// Gets or sets a value indicating whether to preserve a scaled or       
        /// rotated object's transformed position when changing its centerpoint.
        /// </summary>
        /// <remarks>
        /// By default (false), CompositeTransform mimics the behavior of the Windows CompositeTransform object, insofar as changes to CenterX 
        /// and CenterY will have an immediate impact on the position of a scaled or rotated object, as the object is now being scaled or rotated 
        /// about the new centerpoint. If this property is true, CompositeTransform will adjust the TranslateX and TranslateY properties automatically 
        /// whenever CenterX and CenterY are changed in order to keep the object in the same real position despite changing its centerpoint. This is 
        /// useful in multitouch applications where the centerpoint about which an object is scaled or rotated might change frequently depending on 
        /// the location of a pinch gesture. Compensating for the changed centerpoint eliminates the need to "collapse" the transform at the end of 
        /// each multitouch operation. 
        /// 
        /// Note: changing this value will have no effect on the transformation matrix. The change will only be apparent
        /// when subsequent changes are made to the CenterX or CenterY properties.
        /// Also, any changes to CenterX or CenterY while this property is true will have the effect of destroying any data bindings
        /// involving TranslateX and TranslateY.
        /// </remarks>
        public bool PreservePosition
       	{
            get
            {
                return (bool)GetValue(PreservePositionProperty);
            }
            set
            {
                SetValue(PreservePositionProperty, value);
            }
        }
        private void OnPreservePositionChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        #region double TranslateX dependency property
       	public static DependencyProperty TranslateXProperty = DependencyProperty.Register(  "TranslateX", typeof(double), typeof(CompositeTransform), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((CompositeTransform)obj).OnTranslateXChanged(args); }));
       	public double TranslateX
       	{
            get
            {
                return (double)GetValue(TranslateXProperty);
            }
            set
            {
                SetValue(TranslateXProperty, value);
            }
        }
        private void OnTranslateXChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double TranslateY dependency property
       	public static DependencyProperty TranslateYProperty = DependencyProperty.Register(  "TranslateY", typeof(double), typeof(CompositeTransform), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((CompositeTransform)obj).OnTranslateYChanged(args); }));
       	public double TranslateY
       	{
            get
            {
                return (double)GetValue(TranslateYProperty);
            }
            set
            {
                SetValue(TranslateYProperty, value);
            }
        }
        private void OnTranslateYChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double ScaleX dependency property
       	public static DependencyProperty ScaleXProperty = DependencyProperty.Register(  "ScaleX", typeof(double), typeof(CompositeTransform), new PropertyMetadata((double)1,
                                                               (obj, args) => { ((CompositeTransform)obj).OnScaleXChanged(args); }));
       	public double ScaleX
       	{
            get
            {
                return (double)GetValue(ScaleXProperty);
            }
            set
            {
                SetValue(ScaleXProperty, value);
            }
        }
        private void OnScaleXChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double ScaleY dependency property
       	public static DependencyProperty ScaleYProperty = DependencyProperty.Register(  "ScaleY", typeof(double), typeof(CompositeTransform), new PropertyMetadata((double)1,
                                                               (obj, args) => { ((CompositeTransform)obj).OnScaleYChanged(args); }));
       	public double ScaleY
       	{
            get
            {
                return (double)GetValue(ScaleYProperty);
            }
            set
            {
                SetValue(ScaleYProperty, value);
            }
        }
        private void OnScaleYChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region double Rotation dependency property
       	public static DependencyProperty RotationProperty = DependencyProperty.Register(  "Rotation", typeof(double), typeof(CompositeTransform), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((CompositeTransform)obj).OnRotationChanged(args); }));
       	public double Rotation
       	{
            get
            {
                return (double)GetValue(RotationProperty);
            }
            set
            {
                SetValue(RotationProperty, value);
            }
        }
        private void OnRotationChanged(DependencyPropertyChangedEventArgs args)
        {
            _SinTheta = Math.Sin((double)args.NewValue * _RadianFactor);
            _CosTheta = Math.Cos((double)args.NewValue * _RadianFactor);
        }
        #endregion
        
        #region double CenterX dependency property
       	public static DependencyProperty CenterXProperty = DependencyProperty.Register(  "CenterX", typeof(double), typeof(CompositeTransform), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((CompositeTransform)obj).OnCenterXChanged(args); }));
       	public double CenterX
       	{
            get
            {
                return (double)GetValue(CenterXProperty);
            }
            set
            {
                SetValue(CenterXProperty, value);
            }
        }
        private void OnCenterXChanged(DependencyPropertyChangedEventArgs args)
        {
            if ( this.PreservePosition )
                this.TranslateX += ((double)args.OldValue - (double)args.NewValue) * (1 - this.ScaleX * _CosTheta);
        }
        #endregion

        #region double CenterY dependency property
       	public static DependencyProperty CenterYProperty = DependencyProperty.Register(  "CenterY", typeof(double), typeof(CompositeTransform), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((CompositeTransform)obj).OnCenterYChanged(args); }));
       	public double CenterY
       	{
            get
            {
                return (double)GetValue(CenterYProperty);
            }
            set
            {
                SetValue(CenterYProperty, value);
            }
        }
        private void OnCenterYChanged(DependencyPropertyChangedEventArgs args)
        {
            if (this.PreservePosition)
                this.TranslateY += ((double)args.OldValue - (double)args.NewValue) * (1 - this.ScaleY * _CosTheta);
        }
        #endregion

        public override Matrix Value
        {
            get
            {
                return new Matrix
                {
                    M11 = this.M11,
                    M12 = this.M12,
                    M21 = this.M21,
                    M22 = this.M22,
                    OffsetX = this.X0,
                    OffsetY = this.Y0
                };
            }
        }

		public double M11
		{
			get 
			{
				return this.ScaleX * _CosTheta;
			}
		}

		public double M12
		{
			get
			{
				return this.ScaleX * _SinTheta;
			}
		}

		public double M21
		{
			get 
			{
				return -this.ScaleY * _SinTheta;
			}
		}

		public double M22
		{
			get
			{
				return this.ScaleY * _CosTheta;
			}
		}

		public double X0
		{
			get
			{
				return this.TranslateX + this.CenterX - this.CenterX * this.M11 - this.CenterY * this.M12;
			}
		}

		public double Y0
		{
			get 
			{
				return this.TranslateY + this.CenterY - this.CenterX * this.M21 - this.CenterY * this.M22;
			}
		}


        protected override bool TryTransformCore(Point inPoint, out Point outPoint)
        {
            outPoint = new Point(inPoint.X * M11 + inPoint.Y * M12 + X0,
                                inPoint.X * M21 + inPoint.Y * M22 + Y0 );
            return true;
        }


        protected override Rect TransformBoundsCore(Rect rect)
        {
            Point pt1, pt2;

            TryTransformCore(new Point(rect.X, rect.Y), out pt1);
            TryTransformCore(new Point(rect.Right, rect.Bottom), out pt2);

            return new Rect(pt1, pt2);
        }
	}
}

