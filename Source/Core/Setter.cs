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
using System.Reflection;
using System.ComponentModel;
using System.Collections.ObjectModel;

using Camelot.Core.Internal;

namespace Camelot.Core
{
    public class Setter : SetterBase
    {
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

        internal object RawValue
        {
            get
            {
                return _Value;
            }
        }


        public DependencyProperty Property { get; set; }
        public string TargetName { get; set; }

        private void NormalizeValueType()
        {
            if (Property.PropertyType.IsAssignableFrom(_Value.GetType()))
                return;
            _Value = UniversalPropertyKey.ConvertTo(_Value, Property.PropertyType);
        }

        private Dictionary<DependencyObject, object> _OldValues = new Dictionary<DependencyObject, object>();

        internal void SetValue(DependencyObject target)
        {
            if (this.Property == null)
                throw new Exception("Setter must have a Property set");

            if ( this.Property == Control.TemplateProperty )
            {
                int a = 1;
            }

            this._OldValues[target] = target.GetValue(this.Property);
            object value;
            if ( this._Value is StaticResource && target is FrameworkElement )
            {
                value = ((StaticResource)this._Value).FindResource((FrameworkElement)target);
            }
            else
            {
                value = this.Value;
            }
            if (value == null)
                throw new Exception("Setter must have a Value set");

            target.SetValue(this.Property, value);
        }

        internal void UnsetValue(DependencyObject target)
        {
            object oldValue;
            if ( this._OldValues.TryGetValue(target, out oldValue) )
            {
                target.SetValue(this.Property, this._OldValues[target]);
            }


        }
    }

    public sealed class SetterBaseCollection : Collection<SetterBase>
    {

    }

    public class SetterBase
    {

    }
}