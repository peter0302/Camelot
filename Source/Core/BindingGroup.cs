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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reflection;

namespace Camelot.Core
{
    public class BindingGroup : DependencyObject
    {
        Collection<BindingExpressionBase> _BindingExpressions = new Collection<BindingExpressionBase>();
        Collection<BindingExpressionBase> BindingExpressions
        {
            get
            {
                return _BindingExpressions;
            }
        }

        List<object> _Items = new List<object>();
        public IList Items
        {
            get
            {
                return _Items;
            }
        }

        public string Name { get; set; }

        public DependencyObject Owner { get; private set; }

        public object GetValue ( object item, string propertyName )
        {
            PropertyInfo pi = item.GetType().GetTypeInfo().GetDeclaredProperty(propertyName);
            if (pi == null)
                throw new Exception("Object " + item.GetType().Name + " has no public property " + propertyName);
            return pi.GetValue(item);            
        }

        public bool TryGetValue ( object item, string propertyName, out object value )
        {
            PropertyInfo pi = item.GetType().GetTypeInfo().GetDeclaredProperty(propertyName);
            if (pi == null)
            {
                value = null;
                return false;
            }
            try
            {
                value = pi.GetValue(item);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        public bool UpdateSources()
        {
            return false;
        }

        public bool ValidateWithoutUpdate()
        {
            return false;
        }
    

    }
}