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
    public enum BindingMode
    {
        Default,
        OneTime,
        OneWay,
        OneWayToSource,
        TwoWay
    }

    public enum UpdateSourceTrigger
    {
        Default,
        Explicit,
        LostFocus,
        PropertyChanged
    }

    public class Binding : BindingBase
    {
        public Binding ()
        {
            
        }

        public Binding (string path)
        {
            this.Path = new PropertyPath(path);
        }

        public static readonly object DoNothing;
        public const string IndexerName = "Item[]";

        public static readonly RoutedEvent SourceUpdatedEvent = EventManager.RegisterRoutedEvent("SourceUpdated", RoutingStrategy.Bubble, typeof(EventHandler<DataTransferEventArgs>), typeof(RelativeSource));
        public static readonly RoutedEvent TargetUpdatedEvent = EventManager.RegisterRoutedEvent("TargetUpdated", RoutingStrategy.Bubble, typeof(EventHandler<DataTransferEventArgs>), typeof(RelativeSource));

        internal override BindingExpressionBase CreateBindingExpressionOverride(DependencyObject targetObject, DependencyProperty targetProperty, BindingExpressionBase owner)
        {
            BindingExpression expression = new BindingExpression(this);
            expression.Target = targetObject;
            expression.TargetProperty = targetProperty;
            return expression;
        }


        public IValueConverter Converter { get; set; }

        public string ElementName { get; set; }

        private object _Source;
        public object Source
        { 
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
            }
        }

        BindingMode _Mode = BindingMode.Default;
        public BindingMode Mode
        {
            get
            {
                return _Mode;
            }
            set
            {
                _Mode = value;
            }
        }

        public PropertyPath Path { get; set; }
        public RelativeSource RelativeSource { get; set; }
        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }
        public object FallbackValue { get; set; }
        
        public static void AddSourceUpdatedHandler (DependencyObject element, EventHandler<DataTransferEventArgs> handler)
        {
            UIElement e = element as UIElement;
            if ( e != null )
            {
                e.AddHandler(SourceUpdatedEvent, handler, false);
            }
        }

        public static void RemoveSourceUpdatedHandler(DependencyObject element, EventHandler<DataTransferEventArgs> handler)
        {
            UIElement e = element as UIElement;
            if (e != null)
            {
                e.RemoveHandler(SourceUpdatedEvent, handler);
            }
        }

        public static void AddTargetUpdatedHandler (DependencyObject element, EventHandler<DataTransferEventArgs> handler)
        {
            UIElement e = element as UIElement;
            if (e != null)
            {
                e.AddHandler(TargetUpdatedEvent, handler, false);
            }
        }

        public static void RemoveTargetUpdatedHandler (DependencyObject element, EventHandler<DataTransferEventArgs> handler)
        {
            UIElement e = element as UIElement;
            if (e != null)
            {
                e.RemoveHandler(TargetUpdatedEvent, handler);
            }
        }        
    }
}