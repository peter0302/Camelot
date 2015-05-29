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
    public class Button : ButtonBase
    {
        
        public Button() : base()
        {

        }

        #region bool IsCancel dependency property
       	public static DependencyProperty IsCancelProperty = DependencyProperty.Register(  "IsCancel", typeof(bool), typeof(Button), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((Button)obj).OnIsCancelChanged(args); }));
       	public bool IsCancel
       	{
            get
            {
                return (bool)GetValue(IsCancelProperty);
            }
            set
            {
                SetValue(IsCancelProperty, value);
            }
        }
        private void OnIsCancelChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region bool IsDefault dependency property
       	public static DependencyProperty IsDefaultProperty = DependencyProperty.Register(  "IsDefault", typeof(bool), typeof(Button), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((Button)obj).OnIsDefaultChanged(args); }));
       	public bool IsDefault
       	{
            get
            {
                return (bool)GetValue(IsDefaultProperty);
            }
            set
            {
                SetValue(IsDefaultProperty, value);
            }
        }
        private void OnIsDefaultChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


    }
}