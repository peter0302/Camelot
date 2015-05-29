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
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace Camelot.Core
{
	public enum ClickMode
	{
		Press,
		Release
	}
    
	public abstract class ButtonBase : ContentControl
	{
		public ButtonBase()
		{
			Initialize ();            
		}


		private void Initialize()
		{
			this.PointerPressed += (object sender, PointerInputEventArgs e) => 
			{
				this.IsPressed = true;
			};
			this.PointerExited += (object sender, PointerInputEventArgs e) => 
			{
                if ( sender == this )
				    this.IsPressed = false;
			};
			this.PointerEntered += (object sender, PointerInputEventArgs e) => 
			{
                if ( sender == this )
				    this.IsPressed = true;
			};
            this.Tapped += (object sender, PointerInputEventArgs e) =>
            {
                RaiseEvent(new RoutedEventArgs(ClickEvent, this));
                this.IsPressed = false;
            };
			this.PointerReleased += (object sender, PointerInputEventArgs e) => 
			{
				this.IsPressed = false;
				RaiseEvent ( new RoutedEventArgs(ClickEvent, this) );
			};
		}


		public static RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent ("Click", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(ButtonBase));
		public event RoutedEventHandler Click
		{
			add
			{
				AddHandler(ClickEvent, value, false);
			}
			remove 
			{
				RemoveHandler (ClickEvent, value);
			}
		}


        
        #region ClickMode ClickMode dependency property
       	public static DependencyProperty ClickModeProperty = DependencyProperty.Register(  "ClickMode", typeof(ClickMode), typeof(ButtonBase), new PropertyMetadata((ClickMode)ClickMode.Press,
                                                               (obj, args) => { ((ButtonBase)obj).OnClickModeChanged(args); }));
       	public ClickMode ClickMode
       	{
            get
            {
                return (ClickMode)GetValue(ClickModeProperty);
            }
            set
            {
                SetValue(ClickModeProperty, value);
            }
        }
        private void OnClickModeChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region ICommand Command dependency property
       	public static DependencyProperty CommandProperty = DependencyProperty.Register(  "Command", typeof(ICommand), typeof(ButtonBase), new PropertyMetadata((ICommand)null,
                                                               (obj, args) => { ((ButtonBase)obj).OnCommandChanged(args); }));
       	public ICommand Command
       	{
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }
        private void OnCommandChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region bool IsPressed dependency property
       	public static DependencyProperty IsPressedProperty = DependencyProperty.Register(  "IsPressed", typeof(bool), typeof(ButtonBase), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((ButtonBase)obj).OnIsPressedChanged(args); }));
       	public bool IsPressed
       	{
            get
            {
                return (bool)GetValue(IsPressedProperty);
            }
            set
            {
                SetValue(IsPressedProperty, value);
            }
        }
        protected virtual void OnIsPressedChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


		protected virtual void OnClick ()
		{
            if (this.Command != null)
            {
                this.Command.Execute(null);
            }
            else
            {
                RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
                RaiseEvent(args);
            }
		}






	}
}

