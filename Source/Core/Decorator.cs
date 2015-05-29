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
    [ContentProperty("Child")]
	public class Decorator : FrameworkElement
	{
		public Decorator()
		{


		}

        
        #region UIElement Child dependency property
       	public static DependencyProperty ChildProperty = DependencyProperty.Register(  "Child", typeof(UIElement), typeof(Decorator), new FrameworkPropertyMetadata((UIElement)null, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                               (obj, args) => { ((Decorator)obj).OnChildChanged(args); }));
       	public UIElement Child
       	{
            get
            {
                return (UIElement)GetValue(ChildProperty);
            }
            set
            {
                SetValue(ChildProperty, value);
            }
        }
        private void OnChildChanged(DependencyPropertyChangedEventArgs args)
        {
            this.AddVisualChild((UIElement)args.NewValue);
        }
        #endregion

        protected override int VisualChildrenCount
        {
            get
            {
                if (this.Child == null) return 0;
                else return 1;
            }
        }

        protected override UIElement GetVisualChild(int index)
        {
            if (this.Child == null || index > 0)
                throw new ArgumentOutOfRangeException();
            return this.Child;
        }
	}
}

