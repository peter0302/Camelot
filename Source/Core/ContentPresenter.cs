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
using System.Diagnostics;
using Camelot.Core.Internal;

namespace Camelot.Core
{
    public class ContentPresenter : Presenter
    {
        

        public ContentPresenter()
        {
            UpdateContentBindings("Content");
        }

        
        #region object Content dependency property
       	public static DependencyProperty ContentProperty = DependencyProperty.Register(  "Content", typeof(object), typeof(ContentPresenter), new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                               (obj, args) => { ((ContentPresenter)obj).OnContentChanged(args); }));
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
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region string ContentSource dependency property
       	public static DependencyProperty ContentSourceProperty = DependencyProperty.Register(  "ContentSource", typeof(string), typeof(ContentPresenter), new PropertyMetadata((string)"Content",
                                                               (obj, args) => { ((ContentPresenter)obj).OnContentSourceChanged(args); }));
       	public string ContentSource
       	{
            get
            {
                return (string)GetValue(ContentSourceProperty);
            }
            set
            {
                SetValue(ContentSourceProperty, value);                
            }
        }
        private void OnContentSourceChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null && (string)args.NewValue != "")
            {
                UpdateContentBindings((string)args.NewValue);
            }
        }
        #endregion
        
        #region DataTemplate ContentTemplate dependency property
       	public static DependencyProperty ContentTemplateProperty = DependencyProperty.Register(  "ContentTemplate", typeof(DataTemplate), typeof(ContentPresenter), new FrameworkPropertyMetadata((DataTemplate)null, FrameworkPropertyMetadataOptions.AffectsMeasure,
                                                               (obj, args) => { ((ContentPresenter)obj).OnContentTemplateChanged(args); }));
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
            if (args.NewValue != null)
            {
                this.VisualContent = (FrameworkElement)((DataTemplate)args.NewValue).Template.Play();                
                this.VisualContent.DataContext = this.Content;         
            }
        }
        #endregion

        #region string ContentStringFormat dependency property
       	public static DependencyProperty ContentStringFormatProperty = DependencyProperty.Register(  "ContentStringFormat", typeof(string), typeof(ContentPresenter), new PropertyMetadata((string)"",
                                                               (obj, args) => { ((ContentPresenter)obj).OnContentStringFormatChanged(args); }));
       	public string ContentStringFormat
       	{
            get
            {
                return (string)GetValue(ContentStringFormatProperty);
            }
            set
            {
                SetValue(ContentStringFormatProperty, value);
            }
        }
        private void OnContentStringFormatChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


        FrameworkElement _VisualContent;
        private FrameworkElement VisualContent
        {
            get { return _VisualContent; }
            set
            {
                if (this.VisualChildrenCount > 0)
                {
                    this.RemoveVisualChild(this.GetVisualChild(0));
                }
                _VisualContent = value;
                this.AddVisualChild(_VisualContent);
            }
        }


        private void UpdateContentBindings(string contentSource)
        {            
            BindingOperations.SetBinding(this, ContentProperty, new TemplateBinding((string)contentSource));
            BindingOperations.SetBinding(this, ContentTemplateProperty, new TemplateBinding((string)contentSource + "Template"));
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return this.VisualContent != null ? 1 : 0;
            }
        }

        protected override UIElement GetVisualChild(int index)
        {
            if (this.VisualContent == null || index > 0)
                throw new ArgumentOutOfRangeException();
            else
                return this.VisualContent;                
        }


    }



}