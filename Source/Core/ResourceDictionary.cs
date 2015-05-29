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
using System.Collections;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Camelot.Core
{
    public class ResourceDictionary : IDictionary, ICollection, IEnumerable
    {
        internal static readonly ResourceDictionary StandardDictionary = new ResourceDictionary();

        public ResourceDictionary()
        {

        }

        static ResourceDictionary()
        {
            XamlReader reader = (new XamlReaderLocal()).Reader;

            var asm = Assembly.GetAssembly(typeof(UIElement));
            var asmName = asm.GetName();

            var resourceStream = asm.GetManifestResourceStream(asmName.Name + ".Resources.Standard.xaml");
            if (resourceStream == null)
                throw new Exception("Could not locate standard resources");

            var strReader = new StreamReader(resourceStream);
            string xaml = strReader.ReadToEnd();
         
            ResourceDictionary.StandardDictionary = (ResourceDictionary)reader.Load(xaml, (object)null);            
        }



        Dictionary<object, object> _Resources = new Dictionary<object, object>();

        Collection<ResourceDictionary> _MergedDictionaries = new Collection<ResourceDictionary>();
        Collection<ResourceDictionary> MergedDictionaries
        {
            get
            {
                return _MergedDictionaries;
            }
        }

        public Uri Source
        {
            get;
            set;
        }
        

        public void Add(object key, object value)
        {
            _Resources.Add(key, value);
        }

        public void Clear()
        {
            _Resources.Clear();
        }

        public bool Contains(object key)
        {
            return _Resources.ContainsKey(key);
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return _Resources.GetEnumerator();
        }

        public void Remove(object key)
        {
            _Resources.Remove(key);
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public bool IsReadOnly { get { return false; } }

        public object this[object key] 
        { 
            get
            {
                return _Resources[key];
            }
            set
            {
                _Resources[key] = value;
            }
        }

        public ICollection Keys
        {
            get
            {
                return _Resources.Keys;
            }
        }

        public ICollection Values
        {
            get
            {
                return _Resources.Values;
            }
        }

        public void CopyTo(Array array, int index)
        {
            object[] theArray = new object[_Resources.Count];
            _Resources.Values.CopyTo(theArray, index);
            theArray.CopyTo(array, index);
        }

        public int Count
        {
            get
            {
                return _Resources.Count;
            }
        }

        public bool IsSynchronized 
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return _Resources;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Resources.GetEnumerator();
        }
    }
}