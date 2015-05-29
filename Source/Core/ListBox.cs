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

namespace Camelot.Core
{
    [StyleTypedPropertyAttribute(Property = "ItemContainerStyle", StyleTargetType = typeof(ListBoxItem))]
    public class ListBox : Selector
    {
        
        #region SelectionMode SelectionMode dependency property
       	public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(  "SelectionMode", typeof(SelectionMode), typeof(ListBox), new PropertyMetadata((SelectionMode)SelectionMode.Single,
                                                               (obj, args) => { ((ListBox)obj).OnSelectionModeChanged(args); }));
       	public SelectionMode SelectionMode
       	{
            get
            {
                return (SelectionMode)GetValue(SelectionModeProperty);
            }
            set
            {
                SetValue(SelectionModeProperty, value);
            }
        }
        private void OnSelectionModeChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        
        #region IList SelectedItems dependency property
       	public static DependencyProperty SelectedItemsProperty = DependencyProperty.Register(  "SelectedItems", typeof(IList), typeof(ListBox), new PropertyMetadata((IList)null,
                                                               (obj, args) => { ((ListBox)obj).OnSelectedItemsChanged(args); }));
       	public IList SelectedItems
       	{
            get
            {
                return (IList)GetValue(SelectedItemsProperty);
            }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }
        private void OnSelectedItemsChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        /// <summary>
        /// Selects all the items in a ListBox.
        /// </summary>
        public void SelectAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears all the selection in a ListBox.
        /// </summary>
        public void UnselectAll()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Sets a collection of selected items.
        /// </summary>
        /// <param name="selectedItems"></param>
        /// <returns>true if all items have been selected; otherwise, false.</returns>
        protected bool SetSelectedItems(IEnumerable selectedItems)
        {
            throw new NotImplementedException();
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
        }
    }

    public enum SelectionMode
    {
        Single,
        Multiple,
        Extended
    }


    public class ListBoxItem : ContentControl
    {
        
        #region bool IsSelected dependency property
       	public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(  "IsSelected", typeof(bool), typeof(ListBoxItem), new FrameworkPropertyMetadata((bool)false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                               (obj, args) => { ((ListBoxItem)obj).OnIsSelectedChanged(args); }));
       	public bool IsSelected
       	{
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }
        private void OnIsSelectedChanged(DependencyPropertyChangedEventArgs args)
        {
            if ( (bool)args.NewValue )
            {
                this.OnSelected(new RoutedEventArgs(SelectedEvent, this));
            }
            else
            {
                this.OnUnselected(new RoutedEventArgs(UnselectedEvent, this));
            }
        }
        #endregion


        protected internal override void OnPointerPressed(PointerInputEventArgs e)
        {
            this.IsSelected = !this.IsSelected;
        }

        protected internal override void OnPointerExited(PointerInputEventArgs e)
        {
            if ( e.Type == InputType.Touch )
            {
                this.IsSelected = false;
            }            
        }

        public virtual void OnSelected (RoutedEventArgs e)
        {
            RaiseEvent(e);
        }
                
        public virtual void OnUnselected (RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        #region Selected routed event
        public static readonly RoutedEvent SelectedEvent = EventManager.RegisterRoutedEvent("Selected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ListBoxItem));
        public event SelectionChangedEventHandler Selected
        {
            add
            {
                AddHandler(SelectedEvent, value, false);
            }
            remove
            {
                RemoveHandler(SelectedEvent, value);
            }
        }
        #endregion

        #region Unselected routed event
        public static readonly RoutedEvent UnselectedEvent = EventManager.RegisterRoutedEvent("Unelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ListBoxItem));
        public event SelectionChangedEventHandler Unselected
        {
            add
            {
                AddHandler(UnselectedEvent, value, false);
            }
            remove
            {
                RemoveHandler(UnselectedEvent, value);
            }
        }
        #endregion

    }
}