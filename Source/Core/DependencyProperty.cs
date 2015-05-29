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
 ***********************************************************************************************/using System;
using System.Collections.Generic;

namespace Camelot.Core
{
    public delegate void PropertyChangedCallback (DependencyObject d, DependencyPropertyChangedEventArgs e);

    public struct DependencyPropertyChangedEventArgs
    {
        public object NewValue { get; private set; }

        public object OldValue { get; private set; }

        public DependencyProperty Property { get; private set; }

        public DependencyPropertyChangedEventArgs ( DependencyProperty property, Object oldValue, Object newValue ) : this()
        { 
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
        }

    }

    public class PropertyMetadata
    {
        public object DefaultValue { get; set; }

        public PropertyChangedCallback PropertyChangedCallback {get; set;}

        public PropertyMetadata() { } 

        public PropertyMetadata ( object defaultValue )
        {
            DefaultValue = defaultValue;
        }

        public PropertyMetadata ( PropertyChangedCallback propertyChangedCallback )
        {
            PropertyChangedCallback = propertyChangedCallback;
        }

        public PropertyMetadata ( object defaultValue, PropertyChangedCallback propertyChangedCallback )
        {
            DefaultValue = defaultValue;
            PropertyChangedCallback = propertyChangedCallback;
        }

    }

    public sealed class DependencyPropertyKey
    {
        public DependencyProperty DependencyProperty { get; private set; }

        internal DependencyPropertyKey(DependencyProperty dp)
        {
            DependencyProperty = dp;
        }
    }

    public class DependencyProperty
    {
        private DependencyProperty()
        {
        }

        public int GlobalIndex { get; private set; }

        public PropertyMetadata DefaultMetadata { get; private set; }

        public string Name {get; private set;}

        public Type OwnerType {get; private set;}

        public Type PropertyType {get; private set;}

        public bool ReadOnly {get; private set; }

        public static DependencyProperty Register (string name, Type propertyType, Type ownerType)
        {
            return DependencyProperty.Register(name, propertyType, ownerType, null);
        }

        public static DependencyProperty Register (string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
        {
            DependencyProperty newProperty = new DependencyProperty { Name = name, PropertyType = propertyType, OwnerType = ownerType, DefaultMetadata = typeMetadata };
            Add(newProperty);
            return newProperty;
        }

        public static DependencyProperty RegisterAttached (string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
        {
            return DependencyProperty.Register(name, propertyType, ownerType, typeMetadata);
        }

        public static DependencyPropertyKey RegisterReadOnly (string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
        {
            DependencyProperty newProperty = new DependencyProperty { Name = name, PropertyType = propertyType, OwnerType = ownerType, DefaultMetadata = typeMetadata, ReadOnly= true };
            Add(newProperty);

            DependencyPropertyKey newKey = new DependencyPropertyKey(newProperty); 

            return newKey;
        }

        private static void Add (DependencyProperty property)
        {
            property.GlobalIndex = _MasterIndex++;
            _Properties.Add(property);
        }

        public static readonly object UnsetValue = new object();

        private static int _MasterIndex = 0;

        private static List<DependencyProperty> _Properties = new List<DependencyProperty>();

        public override bool Equals(object obj)
        {
            if ( obj is DependencyProperty )
            {
                return ((DependencyProperty)obj).GlobalIndex == this.GlobalIndex;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.GlobalIndex;
        }
    }
}

