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

#if __IOS__
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
#endif

namespace Camelot.Core
{
    public class TranslateTransform : Transform
    {
        public TranslateTransform() : base()
        {            
        }

        public TranslateTransform(double x, double y) : base()
        {
            this.X = x;
            this.Y = y;
        }
        
#if __IOS__        
        #region CGAffineTransform CGAffineTransform dependency property
       	public static DependencyProperty CGAffineTransformProperty = DependencyProperty.Register(  "CGAffineTransform", typeof(CGAffineTransform), typeof(TranslateTransform), new PropertyMetadata((CGAffineTransform)new CGAffineTransform(1,0,0,1,0,0),
                                                               (obj, args) => { ((TranslateTransform)obj).OnCGAffineTransformChanged(args); }));
       	public CGAffineTransform CGAffineTransform
       	{
            get
            {
                return (CGAffineTransform)GetValue(CGAffineTransformProperty);
            }
            set
            {
                SetValue(CGAffineTransformProperty, value);
            }
        }
        private void OnCGAffineTransformChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
#endif

        
        #region double X dependency property
       	public static readonly DependencyProperty XProperty = DependencyProperty.Register(  "X", typeof(double), typeof(TranslateTransform), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((TranslateTransform)obj).OnXChanged(args); }));
       	public double X
       	{
            get
            {
                return (double)GetValue(XProperty);
            }
            set
            {
                SetValue(XProperty, value);
            }
        }
        private void OnXChanged(DependencyPropertyChangedEventArgs args)
        {
            _Value.OffsetX = (double)args.NewValue;
        }
        #endregion
        
        #region double Y dependency property
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(TranslateTransform), new PropertyMetadata((double)0,
                                                               (obj, args) => { ((TranslateTransform)obj).OnYChanged(args); }));
       	public double Y
       	{
            get
            {
                return (double)GetValue(YProperty);
            }
            set
            {
                SetValue(YProperty, value);
            }
        }
        private void OnYChanged(DependencyPropertyChangedEventArgs args)
        {
            _Value.OffsetY = (double)args.NewValue;
        }
        #endregion

        #region Matrix Value property
        Matrix _Value = new Matrix();
        public override Matrix Value
        {
            get 
            {
                return _Value;
            }
        }
        #endregion

       

        protected override GeneralTransform InverseCore
        {
            get
            {
                return new TranslateTransform(-this.X, -this.Y);
            }
        }

    }
}