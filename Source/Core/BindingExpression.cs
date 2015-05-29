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
using System.ComponentModel;
using Camelot.Core.Internal;

namespace Camelot.Core
{
    /// <summary>
    /// Contains information about a single instance of a Binding. 
    /// </summary>
    /// <remarks>
    /// The Binding class is the high-level class for the declaration of a binding. The BindingExpression class is the underlying 
    /// object that maintains the connection between the binding source and the binding target. A Binding contains all the information 
    /// that can be shared across several BindingExpression objects. A BindingExpression is an instance expression that cannot be 
    /// shared and that contains all the instance information about the Binding.
    /// 
    /// For example, consider the following, where myDataObject is an instance of the MyData class, myBinding is the source Binding object, 
    /// and MyData class is a defined class that contains a string property named MyDataProperty. This example binds the text content of 
    /// mytext, which is an instance of TextBlock, to MyDataProperty.
    /// <code>
    ///   MyData myDataObject = new MyData(DateTime.Now);      
    ///   Binding myBinding = new Binding("MyDataProperty");
    ///   myBinding.Source = myDataObject;
    ///   myText.SetBinding(TextBlock.TextProperty, myBinding);
    /// </code>
    /// You can use the same myBinding object to create other bindings. For example, you might use the myBinding object to bind the text 
    /// content of a check box to MyDataProperty. In that scenario, there will be two instances of BindingExpression that share the myBinding 
    /// object. You can obtain a BindingExpression object by using the GetBindingExpression method or the GetBindingExpression method on a 
    /// data-bound object.
    /// </remarks>
    
    public class BindingExpression : BindingExpressionBase
    {
        bool _SuspendUpdateTarget = false;


        internal BindingExpression(Binding parent) : base(parent)
        {
            this.ParentBinding = parent;
        }


        /// <summary>
        /// Gets the binding source object that this BindingExpression uses.
        /// [UNCLEAR WHAT THIS ACTUALLY DOES IN WPF?]
        /// </summary>
        /// <value>
        /// The binding source object that this BindingExpression uses.
        /// </value>
        public object DataItem
        {
            get;
            internal set;
        }

        /// <summary>
        /// Returns the Binding object of the current BindingExpression. 
        /// </summary>
        /// <value>
        /// The Binding object of the current binding expression.
        /// </value>
        public Binding ParentBinding
        {
            get;
            internal set;
        }


        internal bool BindsToDataContext
        {
            get
            {
                return this.ParentBinding.Source == null && this.ParentBinding.RelativeSource == null;
            }
        }


        /// <summary>
        /// Gets the binding source object for this BindingExpression.
        /// </summary>
        /// <value>
        /// The binding source object for this BindingExpression.
        /// </value>
        public object ResolvedSource
        {
            get
            {
                if ( this.ParentBinding == null )
                    throw new Exception("Binding integrity has been compromised");

                if ( this.ParentBinding.Source != null )
                {
                    return this.ParentBinding.Source;
                }
                else if ( this.ParentBinding.RelativeSource != null )
                {
                    RelativeSource relSource = this.ParentBinding.RelativeSource;
                    if ( relSource.Mode == RelativeSourceMode.TemplatedParent && this.Target is FrameworkElement )
                    {
                        return ((FrameworkElement)this.Target).TemplatedParent;
                    }
                    else if ( relSource.Mode == RelativeSourceMode.Self )
                    {
                        return this.Target;
                    }
                    else if ( relSource.Mode == RelativeSourceMode.FindAncestor )
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else if ( this.Target is FrameworkElement  )
                {
                    return ((FrameworkElement)this.Target).DataContext;
                }
                else
                {
                    return null;
                }
            }            
        }

        /// <summary>
        /// Gets the name of the binding source property for this BindingExpression. 
        /// </summary>
        /// <value>
        /// The name of the binding source property for this BindingExpression.
        /// </value>
        public string ResolvedSourcePropertyName
        {
            get
            {
                PropertyPath path = ParentBinding.Path;
                if (path == null)
                {
                    return "";
                }
                else if ( this.ResolvedSource != null )
                {
                    return path.GetPropertyKey(this.ResolvedSource).PropertyName;
                }   
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Sends the current binding target value to the binding source property in TwoWay or OneWayToSource bindings.
        /// </summary>
        /// <remarks>
        /// This method does nothing when the Mode of the binding is not TwoWay or OneWayToSource.
        /// If the UpdateSourceTrigger value of your binding is set to Explicit, you must call the UpdateSource method or 
        /// the changes will not propagate back to the source.
        /// </remarks>
        public override void UpdateSource()
        {
            object source = this.ResolvedSource;

            // If Path is null, the target property is not bound to any source property, but
            // just the sourc eobject, such as {Binding Source={StaticResource MyResource}}. This
            // is pretty rare though.
            if (source == null || this.ParentBinding == null || this.ParentBinding.Path == null) 
                return;

            UniversalPropertyKey key = this.ParentBinding.Path.GetPropertyKey(source);
            object newValue = this.Target.GetValue(this.TargetProperty);

            // this ensures the target does not update in response to a source update 
            // notification, otherwise infinite loops could result
            _SuspendUpdateTarget = true;    

            if ( source is DependencyObject )
            {
                // This will (and should?) destroy any binding on the source property
                ((DependencyObject)source).SetValue(key.DependencyPropertyInfo, newValue);
            }
            else
            {
                key.SetValue(newValue);
            }
            _SuspendUpdateTarget = false;            
        }

        /// <summary>
        /// Forces a data transfer from the binding source property to the binding target property.
        /// </summary>
        /// <remarks>
        /// This method enables you to force a data transfer from the source property to the target property. If your source 
        /// object implements a proper property-changed notification mechanism such as INotifyPropertyChanged, target updates
        /// happen automatically. However, you have the option to use this method to update the target property explicitly in 
        /// cases where your source object does not provide the proper property-changed notifications. You can also use this 
        /// method if your application needs to update the target properties periodically.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// The binding has been detached from its target.
        /// </exception>                
        public override void UpdateTarget()
        {
            if (_SuspendUpdateTarget) return;

            if (this.Target == null || this.TargetProperty == null)
                throw new Exception("The binding has been detached from its target.");

            this.Target.InvalidateProperty(this.TargetProperty);
        }


        internal object SourceValue
        {
            get
            {
                // TODO: Add value conversion capabilities

                object resolvedSource = this.ResolvedSource;

                PropertyPath path = ParentBinding.Path;
                if ( path == null )
                {
                    // don't need a path; assume the binding is direct to source
                    // as in {Binding Source={StaticResource MyResource}}
                    return resolvedSource;
                }
                else if ( resolvedSource == null )
                {
                    return null;
                }
                else
                {
                    return path.GetPropertyKey(this.ResolvedSource).GetValue();
                }                
            }
        }


        internal BindingMode EffectiveBindingMode
        {
            get
            {
                BindingMode parentMode = ParentBinding.Mode;
                if (parentMode == BindingMode.Default)
                {
                    FrameworkPropertyMetadata fpm = this.TargetProperty.DefaultMetadata as FrameworkPropertyMetadata;
                    if ( fpm != null && fpm.BindsTwoWayByDefault  )
                    {
                        return BindingMode.TwoWay;                        
                    }
                    return BindingMode.OneWay;
                }
                else
                {
                    return parentMode;
                }
            }
        }

        internal UpdateSourceTrigger EffectiveUpdateSourceTrigger
        {
            get
            {
                UpdateSourceTrigger parentTrigger = ParentBinding.UpdateSourceTrigger;
                if ( parentTrigger == UpdateSourceTrigger.Default )
                {
                    // TODO: Allow different default source triggers in property metadata
                    return UpdateSourceTrigger.PropertyChanged;
                }
                else
                {
                    return parentTrigger;
                }
            }
        }


    }
}