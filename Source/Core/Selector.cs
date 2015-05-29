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

using Camelot.Core.Internal;

namespace Camelot.Core
{
    public abstract class Selector : ItemsControl
    {        
        private bool _IsSelectionChanging = false;

        ObservableCollection<object> _SelectedItems = new ObservableCollection<object>();

        public Selector() : base()
        {
        }


        #region bool IsSelected attached dependency property
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(Selector), new PropertyMetadata((bool)false, OnIsSelectedChanged));

        public static bool GetIsSelected(DependencyObject element)
        {
            return (bool)element.GetValue(IsSelectedProperty);
        }
        public static void SetIsSelected(DependencyObject element, bool value)
        {
            element.SetValue(IsSelectedProperty, value);
        }
        private static void OnIsSelectedChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
       
        #region int SelectedIndex dependency property
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(Selector), new PropertyMetadata((int)-1,
                                                               (obj, args) => { ((Selector)obj).OnSelectedIndexChanged(args); }));
       	public int SelectedIndex
       	{
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }
        protected virtual void OnSelectedIndexChanged(DependencyPropertyChangedEventArgs args)
        {
            if (_IsSelectionChanging) return;

            int i = (int)args.NewValue;
            if (i >= this.Items.Count)
                throw new ArgumentOutOfRangeException("SelectedIndex");

            object newSelection = this.Items[i];
            UpdateSelection(new object[] { newSelection });            
        }
        #endregion
        
        #region object SelectedItem dependency property
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(Selector), new PropertyMetadata((object)null,
                                                               (obj, args) => { ((Selector)obj).OnSelectedItemChanged(args); }));
        /// <summary>
        /// Gets or sets the first item in the current selection or returns null if the selection is empty.
        /// </summary>
        /// <remarks>
        /// If a Selector supports selecting a single item, the SelectedItem property returns the selected item. If a Selector supports multiple selections, 
        /// SelectedItem returns the item that the user selected first. Setting SelectedItem in a Selector that supports multiple selections clears existing 
        /// selected items and sets the selection to the item specified.</remarks>
       	public object SelectedItem
       	{
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }
        protected virtual void OnSelectedItemChanged(DependencyPropertyChangedEventArgs args)
        {
            if (_IsSelectionChanging) return;

            object newItem = args.NewValue;
            if ( this.Items.Contains(newItem) )
            {
                UpdateSelection(new object[] { newItem });
            }
            else
            {
                UpdateSelection(new object[] { null });
            }
        }
        #endregion
        
        #region string SelectedValuePath dependency property
        public static readonly DependencyProperty SelectedValuePathProperty = DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(Selector), new PropertyMetadata((string)"",
                                                               (obj, args) => { ((Selector)obj).OnSelectedValuePathChanged(args); }));
       	public string SelectedValuePath
       	{
            get
            {
                return (string)GetValue(SelectedValuePathProperty);
            }
            set
            {
                SetValue(SelectedValuePathProperty, value);
            }
        }
        private void OnSelectedValuePathChanged(DependencyPropertyChangedEventArgs args)
        {
            UpdateBySelectedValue(this.SelectedValue);
        }
        #endregion
        
        #region object SelectedValue dependency property
        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register("SelectedValue", typeof(object), typeof(Selector), new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                               (obj, args) => { ((Selector)obj).OnSelectedValueChanged(args); }));
        /// <summary>
        /// Gets or sets the value of the SelectedItem, obtained by using SelectedValuePath.
        /// </summary>
        /// <remarks>
        /// The SelectedValuePath property specifies the path to the property that is used to determine the value of the 
        /// SelectedValue property. Setting SelectedValue to a value X attempts to select an item whose value evaluates to X; 
        /// if no such item can be found, the selection is cleared.
        /// </remarks>
       	public object SelectedValue
       	{
            get
            {
                return (object)GetValue(SelectedValueProperty);
            }
            set
            {
                SetValue(SelectedValueProperty, value);
            }
        }
        protected virtual void OnSelectedValueChanged(DependencyPropertyChangedEventArgs args)
        {
            if (_IsSelectionChanging) return;

            if (this.SelectedValuePath == "")
                OnSelectedItemChanged(args);

            UpdateBySelectedValue(args.NewValue);
        }
        #endregion

        #region SelectionChanged routed event
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Direct, typeof(SelectionChangedEventHandler), typeof(Selector));
        public event SelectionChangedEventHandler SelectionChanged
        {
            add
            {
                AddHandler(SelectionChangedEvent, value, false);
            }
            remove
            {
                RemoveHandler(SelectionChangedEvent, value);
            }
        }
        #endregion


        protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            RaiseEvent(e);
        }

        protected void UpdateBySelectedValue(object newValue)
        {
            foreach (object item in this.Items)
            {
                var upk = UniversalPropertyKey.Create(item, this.SelectedValuePath);
                if (upk != null)
                {
                    if (AreEqual(upk.GetValue(), newValue))
                    {
                        // item is the new selection
                        UpdateSelection(new object[] { item });
                    }
                }
            }
            UpdateSelection(new object[] { null });
        }


        protected void ClearSelection()
        {
            foreach (object item in this.Items)
            {
                if (item is DependencyObject)
                    Selector.SetIsSelected((DependencyObject)item, false);
            }
            this.SelectedItem = null;
            this.SelectedIndex = -1;
            this.SelectedValue = null;
        }

        private void UpdateSelection (IList newSelection)
        {
            IList newList = newSelection as IList;
            if (newList == null) return;
            object[] newSelectionArray = new object[newList.Count];
            newList.CopyTo(newSelectionArray, 0);
            UpdateSelection(newSelectionArray);
        }

        private void UpdateSelection (object[] newSelection)
        {
            _IsSelectionChanging = true;

            List<object> newSelectionList = new List<object>(newSelection);

            if ( newSelection[0] == null || !this.Items.Contains(newSelection[0]) )  
            {
                ClearSelection();
            }
            else
            {
                foreach ( object item in this.Items )
                {
                    if (item is DependencyObject)
                    {
                        if (newSelectionList.Contains(item))
                        {
                            Selector.SetIsSelected((DependencyObject)item, true);
                        }
                        else
                        {
                            Selector.SetIsSelected((DependencyObject)item, false);
                        }
                    }
                }
                this.SelectedItem = newSelection[0];
                this.SelectedIndex = this.Items.IndexOf(newSelection[0]);
            }

            SelectionChangedEventArgs args = new SelectionChangedEventArgs(SelectionChangedEvent, new List<object>(this._SelectedItems),
                                                                                        newSelectionList);            
            OnSelectionChanged(args);

            _IsSelectionChanging = false;
        }


    }
}