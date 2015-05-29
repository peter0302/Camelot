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
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using Camelot.Core.Internal;


namespace Camelot.Core
{
    [ContentProperty("Items")]
    [StyleTypedPropertyAttribute(Property = "ItemContainerStyle", StyleTargetType = typeof(FrameworkElement))]
    public class ItemsControl : Control
    {        
        public ItemsControl() : base()
        {
            //_ItemContainerGenerator = new ItemContainerGenerator(_Items);
        }


        #region int AlternationCount dependency property
       	public static DependencyProperty AlternationCountProperty = DependencyProperty.Register(  "AlternationCount", typeof(int), typeof(ItemsControl), new PropertyMetadata((int)0,
                                                               (obj, args) => { ((ItemsControl)obj).OnAlternationCountChanged(args); }));
       	public int AlternationCount
       	{
            get
            {
                return (int)GetValue(AlternationCountProperty);
            }
            set
            {
                SetValue(AlternationCountProperty, value);
            }
        }
        private void OnAlternationCountChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region int AlternationIndex dependency property
       	public static DependencyProperty AlternationIndexProperty = DependencyProperty.Register(  "AlternationIndex", typeof(int), typeof(ItemsControl), new PropertyMetadata((int)0,
                                                               (obj, args) => { ((ItemsControl)obj).OnAlternationIndexChanged(args); }));
       	public int AlternationIndex
       	{
            get
            {
                return (int)GetValue(AlternationIndexProperty);
            }
            set
            {
                SetValue(AlternationIndexProperty, value);
            }
        }
        private void OnAlternationIndexChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region string DisplayMemberPath dependency property
       	public static DependencyProperty DisplayMemberPathProperty = DependencyProperty.Register(  "DisplayMemberPath", typeof(string), typeof(ItemsControl), new PropertyMetadata((string)"",
                                                               (obj, args) => { ((ItemsControl)obj).OnDisplayMemberPathChanged(args); }));
       	public string DisplayMemberPath
       	{
            get
            {
                return (string)GetValue(DisplayMemberPathProperty);
            }
            set
            {
                SetValue(DisplayMemberPathProperty, value);
            }
        }
        private void OnDisplayMemberPathChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
              
        #region bool HasItems dependency property
       	public static DependencyProperty HasItemsProperty = DependencyProperty.Register(  "HasItems", typeof(bool), typeof(ItemsControl), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((ItemsControl)obj).OnHasItemsChanged(args); }));
       	public bool HasItems
       	{
            get
            {
                return (bool)GetValue(HasItemsProperty);
            }
            set
            {
                SetValue(HasItemsProperty, value);
            }
        }
        private void OnHasItemsChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region Style ItemContainerStyle dependency property
       	public static DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register(  "ItemContainerStyle", typeof(Style), typeof(ItemsControl), new PropertyMetadata((Style)null,
                                                               (obj, args) => { ((ItemsControl)obj).OnItemContainerStyleChanged(args); }));
       	public Style ItemContainerStyle
       	{
            get
            {
                return (Style)GetValue(ItemContainerStyleProperty);
            }
            set
            {
                SetValue(ItemContainerStyleProperty, value);
            }
        }
        private void OnItemContainerStyleChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region ItemsPanelTemplate ItemsPanel dependency property
       	public static DependencyProperty ItemsPanelProperty = DependencyProperty.Register(  "ItemsPanel", typeof(ItemsPanelTemplate), typeof(ItemsControl), new PropertyMetadata((ItemsPanelTemplate)null,
                                                               (obj, args) => { ((ItemsControl)obj).OnItemsPanelChanged(args); }));
       	public ItemsPanelTemplate ItemsPanel
       	{
            get
            {
                return (ItemsPanelTemplate)GetValue(ItemsPanelProperty);
            }
            set
            {
                SetValue(ItemsPanelProperty, value);
            }
        }
        private void OnItemsPanelChanged(DependencyPropertyChangedEventArgs args)
        {
            if (_ItemsPresenter != null)
                _ItemsPresenter.ItemsPanelTemplate = (ItemsPanelTemplate)args.NewValue;
        }
        #endregion
        
        #region IEnumerable ItemsSource dependency property
       	public static DependencyProperty ItemsSourceProperty = DependencyProperty.Register(  "ItemsSource", typeof(IEnumerable), typeof(ItemsControl), new PropertyMetadata((IEnumerable)null,
                                                               (obj, args) => { ((ItemsControl)obj).OnItemsSourceChanged(args); }));
       	public IEnumerable ItemsSource
       	{
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        protected virtual void OnItemsSourceChanged(DependencyPropertyChangedEventArgs args)
        {
            INotifyCollectionChanged oldCollection = args.OldValue as INotifyCollectionChanged;
            if ( oldCollection != null )
            {
                oldCollection.CollectionChanged -= OnSourceCollectionChanged;
            }

            INotifyCollectionChanged collection = args.NewValue as INotifyCollectionChanged;
            if ( collection != null )
            {
                collection.CollectionChanged += OnSourceCollectionChanged;
            }

            _Items.Clear();
            foreach (object item in (IEnumerable)args.NewValue)
            {
                AddChild(item);
            }
        }

        void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ( e.Action == NotifyCollectionChangedAction.Add )
            {
                foreach (object item in e.NewItems)
                    AddChild(item);
            }
            else if ( e.Action == NotifyCollectionChangedAction.Remove )
            {
                foreach (object item in e.OldItems)
                    RemoveChild(item);
            }
            OnItemsChanged(e);
        }
        #endregion
        
        #region DataTemplate ItemTemplate dependency property
       	public static DependencyProperty ItemTemplateProperty = DependencyProperty.Register(  "ItemTemplate", typeof(DataTemplate), typeof(ItemsControl), new PropertyMetadata((DataTemplate)null,
                                                               (obj, args) => { ((ItemsControl)obj).OnItemTemplateChanged(args); }));
       	public DataTemplate ItemTemplate
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
            if (_ItemsPresenter != null)
                _ItemsPresenter.ItemTemplate = (DataTemplate)args.NewValue;
        }
        #endregion


        //ItemContainerGenerator _ItemContainerGenerator;
        /// <summary>
        /// Gets the ItemContainerGenerator that is associated with the control.
        /// </summary>
        /// <remarks>
        /// An ItemContainerGenerator is responsible for generating the user interface (UI) for its host, such as an ItemsControl. 
        /// It maintains the association between the items in the data view of the control and the corresponding UIElement objects. 
        /// Every ItemsControl has an associated item container that contains a data item in the item collection. You can use the 
        /// ItemContainerGenerator property to access the item container that is associated with your ItemsControl. For example, 
        /// if you have a data-bound TreeView control and you want to get a TreeViewItem based on its index or its associated data item, 
        /// you can use the ItemContainerGenerator.ContainerFromIndex or the ItemContainerGenerator.ContainerFromItem method. Alternatively, 
        /// you can use the ItemContainerGenerator.IndexFromContainer or the ItemContainerGenerator.ItemFromContainer method to get the 
        /// index or data item that is associated with a given generated container element.
        /// The IItemContainerGenerator interface is also used in advanced scenarios. Typically, advanced applications that have their 
        /// own implementation of a virtualizing panel call members of the interface.
        /// </remarks>
        /*public ItemContainerGenerator ItemContainerGenerator
        {
            get
            {
                return _ItemContainerGenerator;
            }
        }*/



        ObservableCollection<object> _Items = new ObservableCollection<object>();
        public ObservableCollection<object> Items
        {
            get
            {
                return _Items;
            }
        }

        protected virtual void AddChild (object value)
        {
            _Items.Add(value);
            if (_ItemsPresenter != null)
                _ItemsPresenter.AddItem(value);
        }

        protected virtual void RemoveChild (object value)
        {
            _Items.Remove(value);
            if (_ItemsPresenter != null)
                _ItemsPresenter.RemoveItem(value);
        }

        ItemsPresenter _ItemsPresenter = null;
        internal ItemsPresenter ItemsPresenter
        {
            get { return _ItemsPresenter; }
            set 
            {
                // won't be called until an ItemsPresenter has been created with this ItemsControl as the templated parent
                // at which point we should be fully initialized
                _ItemsPresenter = value;
                _ItemsPresenter.ItemsPanelTemplate = this.ItemsPanel;
                _ItemsPresenter.ItemTemplate = this.ItemTemplate;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // find ItemsPresenter
            VisualTreeEnumerator enumerator = new VisualTreeEnumerator(this);
            foreach ( UIElement item in enumerator )
            {
                if (item is ItemsPresenter)
                {
                    this.ItemsPresenter = (ItemsPresenter)item;
                    foreach (object value in this.Items)
                        this.ItemsPresenter.AddItem(value);
                    return;
                }                    
            }
        }

        protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {

        }


    }

}