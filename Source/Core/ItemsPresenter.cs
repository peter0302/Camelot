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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Camelot.Core
{
    /// <summary>
    /// Used within the template of an item control to specify the place in the control’s visual tree where the ItemsPanel defined by the ItemsControl is to be added.
    /// </summary>
    public class ItemsPresenter : Presenter
    {        
        public ItemsPresenter()
        {            
  //          BindingOperations.SetBinding(this, ItemsPanelTemplateProperty, new TemplateBinding("ItemsPanel") );
            //BindingOperations.SetBinding(this, ItemTemplateProperty, new TemplateBinding("ItemTemplate"));    
        }
        
        #region ItemsPanelTemplate ItemsPanelTemplate dependency property
       	internal static readonly DependencyProperty ItemsPanelTemplateProperty = DependencyProperty.Register(  "ItemsPanelTemplate", typeof(ItemsPanelTemplate), typeof(ItemsPresenter), new PropertyMetadata((ItemsPanelTemplate)null,
                                                               (obj, args) => { ((ItemsPresenter)obj).OnItemsPanelTemplateChanged(args); }));
       	internal ItemsPanelTemplate ItemsPanelTemplate
       	{
            get
            {
                return (ItemsPanelTemplate)GetValue(ItemsPanelTemplateProperty);
            }
            set
            {
                SetValue(ItemsPanelTemplateProperty, value);
            }
        }
        private void OnItemsPanelTemplateChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
            {
                var visualContent = (FrameworkElement)((ItemsPanelTemplate)args.NewValue).Template.Play();
                if ( !(visualContent is Panel ) )
                {
                    throw new Exception("VisualTree of ItemsPanelTemplate must contain a panel.");
                }
                this.VisualPanel = (Panel)visualContent;
            }
        }
        #endregion
        
        #region DataTemplate ItemTemplate dependency property
       	internal static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(  "ItemTemplate", typeof(DataTemplate), typeof(ItemsPresenter), new PropertyMetadata((DataTemplate)null,
                                                               (obj, args) => { ((ItemsPresenter)obj).OnItemTemplateChanged(args); }));
       	internal DataTemplate ItemTemplate
       	{
            get
            {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }
        private void OnItemTemplateChanged(DependencyPropertyChangedEventArgs args)
        {
            if (this.VisualPanel == null)   // not yet active
                return;
            object[] values = new object[ _HostedItems.Count];
            _HostedItems.Keys.CopyTo(values,0);
            this.VisualPanel.Children.Clear();
            foreach (object value in values)
            {
                this.AddItem(value, (DataTemplate)args.NewValue);
            }
        }
        #endregion



        Dictionary<object, FrameworkElement> _HostedItems = new Dictionary<object, FrameworkElement>();

        internal void AddItem (object value)
        {
            if (this.VisualPanel == null)
                throw new Exception("No ItemsPanelTemplate specified!");

            AddItem(value, this.ItemTemplate);
        }

        internal void AddItem ( object value, DataTemplate template )
        {           
            FrameworkElement container = null;

            Type containerType = this.ItemContainerType;

            if (this.ItemContainerType == null || !typeof(FrameworkElement).IsAssignableFrom(this.ItemContainerType))
            {
                throw new Exception("Item container type must be derived from FrameworkElement.");
            }            

            if ( this.ItemContainerType.IsAssignableFrom( value.GetType() ))
            {
                // the provided value is already of the correct container type, so just add it
                container = (ContentControl)value;
                container.DataContext = container;
            }
            else
            {
                container = (FrameworkElement)Activator.CreateInstance(containerType);                
                if ( template != null )
                {
                    // if there is a template, play the template and set value
                    // as the DataContext of the root element, and the root element
                    // to be the Content of the container
                    FrameworkElement root = (FrameworkElement)template.Template.Play();
                    root.DataContext = value;
                    ((ContentControl)container).Content = root;                    
                }
                else
                {
                    // if there is no template, attempt to set value to be the
                    // Content of the new container
                    container.DataContext = ((ContentControl)container).Content = value;                    
                }
            }

            this.VisualPanel.Children.Add(container);
        }

        internal void RemoveItem (object value)
        {
            if (this.VisualPanel == null)
                throw new Exception("No ItemsPanelTemplate specified!");

            FrameworkElement fe;
            if ( _HostedItems.TryGetValue(value, out fe) )
            {
                this.VisualPanel.Children.Remove(fe);
                _HostedItems.Remove(value);
            }
        }

        internal Type ItemContainerType
        {
            get
            {
                if ( this.ItemsControlParent != null )
                {
                    StyleTypedPropertyAttribute attr = (StyleTypedPropertyAttribute)this.ItemsControlParent.GetType().GetCustomAttribute(typeof(StyleTypedPropertyAttribute));
                    return attr.StyleTargetType;
                }
                else
                {
                    return null;
                }
            }
            
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if ( this.VisualPanel != null )
            {
                this.VisualPanel.Measure(availableSize);
                return this.VisualPanel.DesiredSize;
            }
            else
            { 	
                return base.MeasureOverride(availableSize);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.VisualPanel != null)
            {
                this.VisualPanel.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
                return finalSize;
            }
            else
            {
                return base.ArrangeOverride(finalSize);
            }
        }

        private ItemsControl ItemsControlParent
        {
            get
            {
                return this.TemplatedParent as ItemsControl;
            }
        }

        Panel _VisualPanel;
        private Panel VisualPanel
        {
            get { return _VisualPanel; }
            set
            {
                if (this.VisualChildrenCount > 0)
                {
                    this.RemoveVisualChild(this.GetVisualChild(0));
                }
                _VisualPanel = value;
                this.AddVisualChild(_VisualPanel);
            }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return this.VisualPanel != null ? 1 : 0;
            }
        }

        protected override UIElement GetVisualChild(int index)
        {
            if (this.VisualPanel == null || index > 0)
                throw new ArgumentOutOfRangeException();
            else
                return this.VisualPanel;
        }
    }
}