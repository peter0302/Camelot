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
using System.Collections.ObjectModel;

using Camelot.Core.Internal;

namespace Camelot.Core
{
    [ContentProperty("Setters")]
    public class Trigger : TriggerBase
    {
        SetterBaseCollection _Setters = new SetterBaseCollection();
        public SetterBaseCollection Setters
        {
            get
            {
                return _Setters;
            }
        }

        public DependencyProperty Property
        {
            get;
            set;
        }

        public string SourceName
        {
            get;
            set;
        }

        object _Value;
        public object Value
        {
            get
            {
                if (_Value == null) return null;
                NormalizeValueType();
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private void NormalizeValueType()
        {
            if (this.Property.PropertyType.IsAssignableFrom(_Value.GetType()))
                return;
            _Value = UniversalPropertyKey.ConvertTo(_Value, Property.PropertyType);
        }


        internal void OnSourcePropertyChanged (object sender, DependencyPropertyChangedEventArgs e)
        {
            if ( e.Property == this.Property )
            {                
                if ( e.NewValue.Equals(this.Value) )
                {
                    // activate trigger!
                    foreach (Setter setter in this.Setters)
                        setter.SetValue((DependencyObject)sender);
                }
                else
                {
                    // deactivate trigger!
                    foreach (Setter setter in this.Setters)
                        setter.UnsetValue((DependencyObject)sender);
                }
            }
        }
    }


    public class TriggerBase : DependencyObject
    {
        TriggerActionCollection _EnterActions = new TriggerActionCollection();
        public TriggerActionCollection EnterActions
        {
            get
            {
                return _EnterActions;
            }
        }

        TriggerActionCollection _ExitActions = new TriggerActionCollection();
        public TriggerActionCollection ExitActions
        {
            get
            {
                return _ExitActions;
            }
        }
    }

    public class TriggerAction : DependencyObject
    {

    }

    public class TriggerActionCollection : DependencyObjectCollection<TriggerAction, List<TriggerAction>>
    {

    }

    public sealed class TriggerCollection : Collection<Trigger>
    {

    }
}