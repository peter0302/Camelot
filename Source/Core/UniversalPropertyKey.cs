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
using System.Reflection;
using System.ComponentModel;
using Camelot.Core;

namespace Camelot.Core.Internal
{
    /// <summary>
    /// Used for obtaining property values from objects at runtime, with universal support
    /// for DependencyProperty's and normal CLR properties. Used by the XAML parser and 
    /// data binding systems in particular.
    /// </summary>
    internal class UniversalPropertyKey
    {
        private UniversalPropertyKey(object source) : this(source.GetType())
        {
            this.Source = source;
        }

        private UniversalPropertyKey(Type sourceType)
        {
            this.SourceType = sourceType;
        }

        private UniversalPropertyKey(Type sourceType, DependencyProperty propertyInfo)
            : this(sourceType)
        {
            this.DependencyPropertyInfo = propertyInfo;
            this.IsDependencyProperty = true;
        }

        private UniversalPropertyKey(Type sourceType, PropertyInfo propertyInfo)
            : this(sourceType)
        {
            this.PropertyInfo = propertyInfo;
            this.IsDependencyProperty = false;
        }

        public UniversalPropertyKey(object source, PropertyInfo propertyInfo)
            : this(source)
        {
            this.PropertyInfo = propertyInfo;
            this.IsDependencyProperty = false;
        }

        public UniversalPropertyKey(DependencyObject source, DependencyProperty propertyInfo)
            : this(source)
        {
            this.DependencyPropertyInfo = propertyInfo;
            this.IsDependencyProperty = true;
        }

        public string PropertyName
        {
            get
            {
                if ( this.IsDependencyProperty )
                {
                    return this.DependencyPropertyInfo.Name;
                }
                else
                {
                    return this.PropertyInfo.Name;
                }
            }
        }

        public Type PropertyType
        {
            get
            {
                if (this.IsDependencyProperty)
                    return this.DependencyPropertyInfo.PropertyType;
                else
                    return this.PropertyInfo.PropertyType;
            }
        }

        public Type SourceType { get; private set; }
        public object Source { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public DependencyProperty DependencyPropertyInfo { get; set; }
        public bool IsDependencyProperty { get; private set; }

        public static UniversalPropertyKey Create(Type sourceType, string propertyName)
        {
            if ( typeof(DependencyObject).IsAssignableFrom (sourceType ) )                
            {
                // see if source object has a DependencyProperty with the given name
                FieldInfo field = sourceType.GetField(propertyName + "Property", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                if (field != null)
                {
                    return new UniversalPropertyKey(sourceType, (DependencyProperty)field.GetValue(null));
                }
            }            
            // try as a normal property
            PropertyInfo pi = sourceType.GetProperty(propertyName);
            if ( pi != null )
            {
                return new UniversalPropertyKey(sourceType, pi);
            }
            // Property does not exist
            return null;
        }

        public static UniversalPropertyKey Create(object source, string propertyName)
        {
            UniversalPropertyKey key = UniversalPropertyKey.Create(source.GetType(), propertyName);
            if ( key != null )
                key.Source = source;
            return key;
        }


        public object GetValue (object source)
        {
            if (this.IsDependencyProperty)
            {
                return ((DependencyObject)source).GetValue(this.DependencyPropertyInfo);
            }
            else
            {
                return PropertyInfo.GetValue(source);
            }
        }

        public object GetValue()
        {
            if (this.Source == null)
                throw new NullReferenceException("Source object not set.");
            return GetValue(this.Source);
        }

        public void SetValue(object value)
        {
            if (this.Source == null)
                throw new NullReferenceException("Source object not set.");
            SetValue(this.Source, value);
        }

        public void SetValue(object source, object value)
        {
            if (this.IsDependencyProperty)
            {
                ((DependencyObject)source).SetValue(this.DependencyPropertyInfo, value);
            }
            else
            {
                PropertyInfo.SetValue(source, value);
            }
        }

        public static object ConvertTo (object value, Type destType)
        {
            if (destType.IsAssignableFrom(value.GetType()) )
                return value;

            if ( value.GetType() == typeof(string) )
            {
                // See if property implements FromString static method
                MethodInfo mi = destType.GetMethod("FromString");
                if (mi != null)
                {
                    return mi.Invoke(null, new object[] { value });
                }

                // See if this is the XXX="Auto" case
                if (destType == typeof(double) && ((string)value) == "Auto")
                {
                    return double.NaN;
                }
            }

            // See if there's a type converter
            TypeConverter tc = TypeDescriptor.GetConverter(destType);
            if (tc.CanConvertFrom(value.GetType()))
            {
                return tc.ConvertFrom(value);
            }

            return null;
        }
    }
}