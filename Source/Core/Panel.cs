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

    [ContentProperty("Children"), HasPlatformView]
    public abstract class Panel : FrameworkElement
    {
        public Panel() : base()
        {

        }

        #region Brush Background dependency property
        public static DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Panel), new PropertyMetadata((Brush)null,
                                                               (obj, args) => { ((Panel)obj).OnBackgroundChanged(args); }));
        public Brush Background
        {
            get
            {
                return (Brush)GetValue(BackgroundProperty);
            }
            set
            {
                SetValue(BackgroundProperty, value);
            }
        }
        private void OnBackgroundChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region bool IsItemsHost dependency property
       	public static DependencyProperty IsItemsHostProperty = DependencyProperty.Register(  "IsItemsHost", typeof(bool), typeof(Panel), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((Panel)obj).OnIsItemsHostChanged(args); }));
       	public bool IsItemsHost
       	{
            get
            {
                return (bool)GetValue(IsItemsHostProperty);
            }
            set
            {
                SetValue(IsItemsHostProperty, value);
            }
        }
        private void OnIsItemsHostChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion

        
        #region int ZIndex attached property
       	public static DependencyProperty ZIndexProperty = DependencyProperty.RegisterAttached(  "ZIndex", typeof(int), typeof(Panel), new PropertyMetadata((int)0, OnZIndexChanged ));
        public static int GetZIndex (UIElement element)
        {
            if (element == null)
                throw new ArgumentException("element");
            return (int)element.GetValue(ZIndexProperty);
        }
        public static void SetZIndex (UIElement element, int index)
        {
            if (element == null)
                throw new ArgumentException("element");
            element.SetValue(ZIndexProperty, index);
        }
        private static void OnZIndexChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion



        UIElementCollection _Children;
        public UIElementCollection Children
        {
            get
            {
                if (_Children == null)
                    _Children = CreateUIElementCollection(this);
                return _Children;        
            }
        }

        protected virtual UIElementCollection CreateUIElementCollection ( FrameworkElement logicalParent )
        {
            return new UIElementCollection(this, logicalParent);
        }

        protected override int VisualChildrenCount
        {
            get
            {
                if (this._Children == null)
                    return 0;
                else
                    return this._Children.Count;
            }
        }

        protected override UIElement GetVisualChild(int index)
        {
            if (this._Children == null || this._Children.Count <= index)
                throw new ArgumentOutOfRangeException("index");
            return (UIElement)this._Children[index];
        }


        protected internal override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            // base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            // TODO: recalculate Z indices
        }

    }
}

