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
using System.Reflection;
using System.IO;


namespace Camelot.Core
{
    [ContentProperty("Content"), HasPlatformView]
    public class UserControl : Control
	{
		public UserControl () : base() 
		{
		}

        protected XamlReader _XamlReader;

        protected virtual void InitializeComponent()
        {
            _XamlReader = (new XamlReaderLocal()).Reader;

            XamlResourceLocationAttribute attr = (XamlResourceLocationAttribute)this.GetType().GetTypeInfo().GetCustomAttribute(typeof(XamlResourceLocationAttribute));
            if (attr == null)
                throw new Exception("No XAML file defined for class " + this.GetType().Name);

                _XamlReader.Load(this, attr.Location);

            /*
            catch (Exception ex)
            {
                throw new Exception("XAML Parse Error: " + ex.Message);
            }*/
        }

        
        #region UIElement Content dependency property
       	public static DependencyProperty ContentProperty = DependencyProperty.Register(  "Content", typeof(UIElement), typeof(UserControl), new PropertyMetadata((UIElement)null,
                                                               (obj, args) => { ((UserControl)obj).OnContentChanged(args); }));
       	public UIElement Content
       	{
            get
            {
                return (UIElement)GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }
        private void OnContentChanged(DependencyPropertyChangedEventArgs args)
        {
            if ( args.OldValue != null )
                this.RemoveVisualChild(args.OldValue as UIElement);
            this.AddVisualChild((UIElement)args.NewValue);
        }
        #endregion

        protected override UIElement GetVisualChild(int index)
        {
            if (this.Content == null || index != 0)
                throw new ArgumentOutOfRangeException();
            return this.Content;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                if (this.Content != null)
                    return 1;
                else
                    return 0;
            }
        }
	}
}

