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
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Camelot.Core.Internal;

namespace Camelot.Core
{
    public class PropertyPath
    {
        public PropertyPath ( object parameter )
        {
            if (parameter is string)
                this.Path = (string)parameter;
        }

        internal string[] _Branches = new string[0];

        string _Path;
        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                SetPath(value);
            }
        }

        Collection<object> _PathParameters = new Collection<object>();
        public Collection<object> PathParameters
        {
            get
            {
                return _PathParameters;
            }
        }

 
        internal UniversalPropertyKey GetPropertyKey (object source)
        {
            int branchIndex = 0;

            object theObject = source;
            UniversalPropertyKey key = null;

            while ( branchIndex < _Branches.Length )
            {
                key = UniversalPropertyKey.Create(theObject, _Branches[branchIndex]);
                if ( branchIndex < _Branches.Length - 1)
                    theObject = key.GetValue();
                branchIndex++;               
            }

            return key;                        
        }


        internal void SetPath(string path)
        {
            _Path = path;
            _Branches = this.Path.Split('.');
        }

    }
}