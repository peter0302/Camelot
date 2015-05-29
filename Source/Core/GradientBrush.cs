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

using System.Collections;
using System.Collections.Generic;


namespace Camelot.Core
{
    [ContentProperty("GradientStops")]
    public abstract class GradientBrush : Brush
    {
        public GradientBrush() : base()
        {
            this.GradientStops = new GradientStopCollection();
        }

        public GradientBrush( GradientStopCollection collection ) : base()
        {
            this.GradientStops = collection;
        }

        #region GradientStopCollection GradientStops dependency property
       	public static DependencyProperty GradientStopsProperty = DependencyProperty.Register(  "GradientStops", typeof(GradientStopCollection), typeof(GradientBrush), new PropertyMetadata((GradientStopCollection)null,
                                                               (obj, args) => { ((GradientBrush)obj).OnGradientStopsChanged(args); }));
       	public GradientStopCollection GradientStops
       	{
            get
            {
                return (GradientStopCollection)GetValue(GradientStopsProperty);
            }
            set
            {
                SetValue(GradientStopsProperty, value);
            }
        }
        private void OnGradientStopsChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

    }
}