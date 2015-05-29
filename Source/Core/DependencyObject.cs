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
using System.Linq;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

using Camelot.Core.Internal;

namespace Camelot.Core
{
    public delegate void DependencyPropertyChangedEventHandler (object sender, DependencyPropertyChangedEventArgs e);

    public class DependencyObject 
    {
        Dictionary<DependencyProperty, object> _LocalValues = new Dictionary<DependencyProperty, object>();
        Dictionary<DependencyProperty, object> _CachedValues = new Dictionary<DependencyProperty, object>();
        Dictionary<DependencyProperty, object> _PropertySourceMappings = new Dictionary<DependencyProperty, object>();

        /// <summary>
        /// Camelot handles DependencyProperty change notifications differently than Microsoft WPF. Microsoft WPF
        /// DependencyObject's do not have an event that can be handled by outside objects, but rather routes
        /// change events to an internally maintained list of dependents. However, Camelot handles DependencyProperty
        /// changes with a public event. A binding target will add a handler to this event when a binding is
        /// created and a DependencyObject source is resolved. It must remove the handler when the source is changed
        /// for whatever reason, or the object is destroyed, otherwise memory leaks or other inefficiences will ensue!
        /// For that reason, this property should be kept internal to the Camelot library.
        /// </summary>
        internal event DependencyPropertyChangedEventHandler DependencyPropertyChanged;

        public DependencyObject() 
        {
        }

        public object GetValue ( DependencyProperty dp )
        {
            return GetValueInternal(dp);
        }

        public void SetValue ( DependencyProperty dp, object value )
        {
            SetValueInternal(dp, value);            
        }

        public void SetValue ( DependencyPropertyKey dp, object value )
        {
            SetValue(dp.DependencyProperty, value);
        }
            
        public object ReadLocalValue ( DependencyProperty dp )
        {
            if (dp == null)
                throw new ArgumentNullException();
            object value;
            if (!this._LocalValues.TryGetValue(dp, out value))
            {
                value = DependencyProperty.UnsetValue;
            }
            return value;
        }

        public void ClearValue ( DependencyProperty dp )
        {
            object oldLocalValue = this.ReadLocalValue(dp);
            if (oldLocalValue is BindingExpression)
                this.UnregisterBindingExpression(dp);

            _LocalValues.Remove(dp);
            _CachedValues.Remove(dp);
        }
            

        public virtual void InvalidateProperty ( DependencyProperty dp )
        {
            _CachedValues.Remove(dp);            
        }

        /// <summary>
        /// Sets the value of a dependency property without changing its value source.     
        /// </summary>
        /// <param name="dp">The identifier of the dependency property to set.</param>
        /// <param name="value">The new local value.</param>
        /// <remarks>
        /// This method is used by a component that programmatically sets the value of one of its own properties 
        /// without disabling an application's declared use of the property. The SetCurrentValue method changes 
        /// the effective value of the property, but existing triggers, data bindings, and styles will continue 
        /// to work.</remarks>
        public void SetCurrentValue ( DependencyProperty dp, object value )
        {
            //SetCurrentValueInternal(dp, value);
            _CachedValues[dp] = value;
        }

        /// <summary>
        /// Attaches a binding to this element, based on the provided source property name as a path qualification to the data source.
        /// </summary>
        /// <param name="dp">Identifies the destination property where the binding should be established.</param>
        /// <param name="path">The source property name or the path to the property used for the binding.</param>
        /// <returns>Records the conditions of the binding. This return value can be useful for error checking.</returns>
        /// <remarks>WPF puts this method in FrameworkElement; putting it in DependencyObject seems better and should not
        /// create compatiblity issues.</remarks>
        public BindingExpression SetBinding(DependencyProperty dp, string path)
        {
            Binding newBinding = new Binding(path);
            BindingExpression expr = (BindingExpression)newBinding.CreateBindingExpression(this, dp);
            this.SetValue(dp, expr);
            return expr;
        }

        /// <summary>
        /// Attaches a binding to this element, based on the provided binding object.
        /// </summary>
        /// <param name="dp">Identifies the property where the binding should be established.</param>
        /// <param name="binding">Represents the specifics of the data binding.</param>
        /// <returns>Records the conditions of the binding. This return value can be useful for error checking.</returns>
        /// <remarks>WPF puts this method in FrameworkElement; putting it in DependencyObject seems better and should not
        /// create compatiblity issues.</remarks>
        public BindingExpression SetBinding(DependencyProperty dp, BindingBase binding)
        {
            BindingExpression expr = (BindingExpression)binding.CreateBindingExpression(this, dp);
            this.SetValue(dp, expr);
            return expr;
        }

        protected virtual void OnPropertyChanged ( DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.DefaultMetadata != null && e.Property.DefaultMetadata.PropertyChangedCallback != null)
            {
                e.Property.DefaultMetadata.PropertyChangedCallback(this, e);
            }
            if (  this.DependencyPropertyChanged != null &&  !e.NewValue.Equals(e.OldValue) ) 
            {
                this.DependencyPropertyChanged(this, e);
            }
        }

        internal void SetValueInternal (DependencyProperty dp, object value )
        {
            object oldLocalValue = ReadLocalValue(dp);
            object oldValue = this.GetCachedValue(dp); //EvaluateLocalValue(dp, oldLocalValue);
            if (oldValue == DependencyProperty.UnsetValue)
                oldValue = dp.DefaultMetadata.DefaultValue;
            object newValue = null;

            if ( oldLocalValue is BindingExpression  )
            {
                // Unregister old binding expression
                UnregisterBindingExpression( dp );
            }

            if ( value is Binding || value is BindingExpression ) // this DependencyObject is a binding target
            {
                // When a property is data bound, the local value is the BindingExpression
                BindingExpression expr = value is BindingExpression ? 
                                                    (BindingExpression)value :                    
                                                    (BindingExpression)((Binding)value).CreateBindingExpression(this, dp);
                var result = RegisterBindingExpression(dp, expr);
                if (expr.EffectiveBindingMode != BindingMode.OneWayToSource)    
                {                    
                    //newValue = this._CachedValues[dp] = result;
                    newValue = result;
                }
                else
                {
                    // for OneWayToSource binding, do nothing with the cached value, and don't show a
                    // genuine value change
                    newValue = oldValue;
                }
            }
            else if (value is StaticResource)
            {
                // Don't cache a value yet; this is a little different from WPF; later, the first time
                // the property is actually accessed, then we will find the resource and evaluate it
                // This makes XAML parsing much easier.
                newValue = this._LocalValues[dp] = value;
                this._CachedValues.Remove(dp); // make sure there is no old cached value
            }
            else if (oldLocalValue is BindingExpression &&
                (   ((BindingExpression)oldLocalValue).EffectiveBindingMode == BindingMode.TwoWay ||
                    ((BindingExpression)oldLocalValue).EffectiveBindingMode == BindingMode.OneWayToSource))
            {
                // Change the cached value only; don't change the local value or else we'd
                // destroy the binding. Also, make sure to update this first, since the 
                // source update operation will call our GetValue method which will prefer
                // to return the cached value. This would be a lot simpler if UpdateSource
                // took an argument, but for some reaosn, it does not.
                newValue = this._CachedValues[dp] = value;

                // Update the source property
                if ( ((BindingExpression)oldLocalValue).EffectiveUpdateSourceTrigger == UpdateSourceTrigger.PropertyChanged )
                    ((BindingExpression)oldLocalValue).UpdateSource();
            }            
            else // normal property change
            { 
                newValue = this._LocalValues[dp] = this._CachedValues[dp] = value;
            }

            if ( !AreEqual(oldValue, newValue) )
                this.OnPropertyChanged(new DependencyPropertyChangedEventArgs(dp, oldValue, newValue));
        }

        internal static bool AreEqual (object o1, object o2)
        {
            if (o1 == null && o2 == null)
                return true;
            else if (o1 != null && o2 == null)
                return false;
            else if (o2 != null && o1 == null)
                return false;
            else
                return o1.Equals(o2);                            
        }

        internal void UnregisterBindingExpression (DependencyProperty targetProperty, bool removeMapping = true)
        {
            object previousSource;
            if (!_PropertySourceMappings.TryGetValue(targetProperty, out previousSource))
                return;

            _PropertySourceMappings[targetProperty] = null;
            _CachedValues[targetProperty] = DependencyProperty.UnsetValue;
            if (previousSource is DependencyObject)
            {
                ((DependencyObject)previousSource).DependencyPropertyChanged -= OnSourceDependentPropertyChanged;
            }
            else if (previousSource is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)previousSource).PropertyChanged -= OnSourcePropertyChanged;
            }
            else
            {
                return;
            }
            if ( removeMapping )
                _PropertySourceMappings.Remove(targetProperty);
        }


        /// <summary>
        /// Resets all binding expressions that have no source; useful for FrameworkElements with data contexts.
        /// Optionally forces hard property changes. This is necessary because changing the DataContext won't
        /// trigger a property change notification.
        /// </summary>
        internal void ReevaluateAllBindings(bool forcePropertyChange)
        {
            var mappingList = _PropertySourceMappings.ToList();
            foreach ( var item in mappingList )
            {
                BindingExpression expr = ReadLocalValue(item.Key) as BindingExpression;
                if ( expr != null && expr.ParentBinding.Source == null ) // it's bound to DataContext or TemplatedParent
                {
                    UnregisterBindingExpression(item.Key, false);
                    if ( forcePropertyChange ) 
                    {
                        SetValueInternal(expr.TargetProperty, expr);
                    }
                    else
                    {
                        RegisterBindingExpression(item.Key, expr);                                           
                    }
                }

            }
        }

        /// <summary>
        /// Registers a RegisterBindingExpression with this DependencyObject so that its property change notification
        /// handlers can be added. Called when a DependencyProperty is first bound to another object, 
        /// and also needs to be called any time a property's binding source changes; for example, when 
        /// DataContext changes, this needs to be called for every DependencyProperty that is bound 
        /// without an explicit source. Don't forget to call UnregisterBindingSource first if the property
        /// was already bound!
        /// </summary>
        /// <param name="targetProperty"></param>
        /// <param name="source"></param>
        internal object RegisterBindingExpression(DependencyProperty targetProperty, BindingExpression expr)
        {
            object source = expr.ResolvedSource;
            this._LocalValues[targetProperty] = expr;

            // Remainder of this method requires an actual source
            if (source == null)
            {
                _PropertySourceMappings[targetProperty] = null;
                return null;
            }

            object newValue = EvaluateLocalValue(targetProperty, expr.SourceValue);
            this._CachedValues[targetProperty] = newValue;

            // Only in OneWay or TwoWay bindings do we need to rely on source notification; 
            BindingMode mode = expr.EffectiveBindingMode;
            if (mode == BindingMode.OneWay || mode == BindingMode.TwoWay)
            {
                if (source is DependencyObject)
                {
                    ((DependencyObject)source).DependencyPropertyChanged += OnSourceDependentPropertyChanged;
                }
                else if (source is INotifyPropertyChanged)
                {
                    ((INotifyPropertyChanged)source).PropertyChanged += OnSourcePropertyChanged;
                }
                _PropertySourceMappings[targetProperty] = source;
            }            
            
            return newValue;
        }        

        void OnSourcePropertyChangedInternal (object source)
        {
            // Find all dependency property(s) bound to this source
            var query = from entry in _PropertySourceMappings
                        where entry.Value == source
                        select entry.Key;
            foreach (DependencyProperty dp in query)
            {
                // verify the local value is still a binding expression with thi sosurce
                BindingExpression expr = ReadLocalValue(dp) as BindingExpression;
                if (expr == null)
                    throw new Exception("Binding integrity has been compromised.");
                expr.UpdateTarget();
            }
        }

        void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnSourcePropertyChangedInternal(sender);
        }

        void OnSourceDependentPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OnSourcePropertyChangedInternal(sender);
        }



        /// <summary>
        /// Returns the effective value for the DependencyProperty, checking first for
        /// a cached value, or, if it does not exist, obtaining it by evaluating the local value 
        /// (which may be a binding expression, StaticResource, inherited property, etc.)
        /// </summary>
        /// <param name="dp">The DependencyProperty to evaluate.</param>
        /// <returns>The effective value of the DependencyProperty</returns>
        private object GetValueInternal(DependencyProperty dp)
        {
            object result = GetCachedValue(dp);
            if (result != DependencyProperty.UnsetValue)
                return result;
            object evaledResult = EvaluateLocalValue(dp, ReadLocalValue(dp));
            if (evaledResult != DependencyProperty.UnsetValue)
            {
                SetCurrentValue(dp, evaledResult);
                return evaledResult;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the current, cached effective value, or DependencyProperty.Unset value, if none.
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        private object GetCachedValue(DependencyProperty dp)
        {
            if (dp == null)
                throw new ArgumentNullException();
            object value;
            if (!this._CachedValues.TryGetValue(dp, out value))
            {
                value = DependencyProperty.UnsetValue;
            }
            return value;
        }

        /// <summary>
        /// Evaluates a local value to create an effective value. 
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="value"></param>
        /// <returns>The effective value evaluated from the local value.</returns>
        /// <remarks>Overridable. Derived classes must call EvaluateLocalValue if they
        /// do not handle the evaluation operation. An example is FrameworkElement, which
        /// handles the logic for inherited properties. (Inherited properties make no
        /// sense for DependencyObject since DependencyObject's do not have parents.)
        /// </remarks>
        protected virtual object EvaluateLocalValue ( DependencyProperty dp, object value )
        {
            if (value is BindingExpression)
            {
                var expr = (BindingExpression)value;
                return EvaluateLocalValue(dp, expr.SourceValue);
            }
            else if ( value == DependencyProperty.UnsetValue )
            {
                return dp.DefaultMetadata.DefaultValue;
            }
            else if (!dp.PropertyType.IsAssignableFrom(value.GetType()))
            {                
                return UniversalPropertyKey.ConvertTo(value, dp.PropertyType);
            }
            else
            {
                return value;
            }
        }
            

    }
}

