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
 ***********************************************************************************************/using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camelot.Core
{
    public class SolidColorBrush : Brush
    {
        
        #region Color Color dependency property
       	public static DependencyProperty ColorProperty = DependencyProperty.Register(  "Color", typeof(Color), typeof(SolidColorBrush), new PropertyMetadata((Color)Colors.Transparent,
                                                               (obj, args) => { ((SolidColorBrush)obj).OnColorChanged(args); }));
       	public Color Color
       	{
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }
        private void OnColorChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        public SolidColorBrush () : base() { }

        public SolidColorBrush ( Color color ) : base ()
        {
            this.Color = color;
        }


    }
}
