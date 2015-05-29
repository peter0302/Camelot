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
    [ContentProperty("Content")]
	public class ContentControl : Control
	{

		public ContentControl()
		{
		
		}

        
        #region object Content dependency property
       	public static DependencyProperty ContentProperty = DependencyProperty.Register(  "Content", typeof(object), typeof(ContentControl), new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                               (obj, args) => { ((ContentControl)obj).OnContentChanged(args); }));
       	public object Content
       	{
            get
            {
                return (object)GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }
        private void OnContentChanged(DependencyPropertyChangedEventArgs args)
        {
            if ( args.NewValue is string )
            {
                this.ContentTemplate = ContentPresenter.StringPresenterTemplate;
            }
        }
        #endregion
        
        #region DataTemplate ContentTemplate dependency property
       	public static DependencyProperty ContentTemplateProperty = DependencyProperty.Register(  "ContentTemplate", typeof(DataTemplate), typeof(ContentControl), new PropertyMetadata((DataTemplate)null,
                                                               (obj, args) => { ((ContentControl)obj).OnContentTemplateChanged(args); }));
       	public DataTemplate ContentTemplate
       	{
            get
            {
                return (DataTemplate)GetValue(ContentTemplateProperty);
            }
            set
            {
                SetValue(ContentTemplateProperty, value);
            }
        }
        private void OnContentTemplateChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region DataTemplateSelector ContentTemplateSelector dependency property
       	public static DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register(  "ContentTemplateSelector", typeof(DataTemplateSelector), typeof(ContentControl), new PropertyMetadata((DataTemplateSelector)null,
                                                               (obj, args) => { ((ContentControl)obj).OnContentTemplateSelectorChanged(args); }));
       	public DataTemplateSelector ContentTemplateSelector
       	{
            get
            {
                return (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty);
            }
            set
            {
                SetValue(ContentTemplateSelectorProperty, value);
            }
        }
        private void OnContentTemplateSelectorChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion



		protected virtual void AddChild (object value)
		{
			this.Content = value;
		}

	}


}

