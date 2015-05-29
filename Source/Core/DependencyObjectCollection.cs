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

namespace Camelot.Core
{
    public class DependencyObjectCollection<MT,CT> : DependencyObject, IList, ICollection, IList<MT>, ICollection<MT>, IEnumerable<MT>, IEnumerable
                                                     where CT : IList, IEnumerable, ICollection, IList<MT>, IEnumerable<MT>, ICollection<MT>, new()                                                                                                          
    {
        private CT _InternalList = new CT();


        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)_InternalList.GetEnumerator();
        }

        IEnumerator<MT> IEnumerable<MT>.GetEnumerator()
        {
            return _InternalList.GetEnumerator();
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
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
                return _InternalList;
            }
        }

        public int Count
        {
            get
            {
                return ((IList)_InternalList).Count;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public void Add(MT stop)
        {
            _InternalList.Add(stop);
            OnCollectionChanged();
        }

        public void Add(object item)
        {
            _InternalList.Add((MT)item);
            OnCollectionChanged();
        }

        int IList.Add(object item)
        {
            int result = ((IList)_InternalList).Add(item);
            OnCollectionChanged();
            return result;
        }

        public void Remove(MT item)
        {
            _InternalList.Remove(item);
            OnCollectionChanged();
        }

        public void Remove(object item)
        {
            _InternalList.Remove((MT)item);
            OnCollectionChanged();
        }

        bool ICollection<MT>.Remove(MT item)
        {
            bool result = ((ICollection<MT>)_InternalList).Remove(item);
            OnCollectionChanged();
            return result;
        }

        public void RemoveAt(int index)
        {
            ((IList)_InternalList).RemoveAt(index);
            OnCollectionChanged();
        }

        public int IndexOf(MT item)
        {
            return _InternalList.IndexOf(item);
        }

        public int IndexOf (object item)
        {
            return _InternalList.IndexOf(item);
        }


        public void CopyTo(MT[] array, int arrayIndex)
        {
            _InternalList.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            _InternalList.CopyTo(array, index);
        }

        public void Insert(int index, MT item)
        {
            _InternalList.Insert(index, item);
            OnCollectionChanged();
        }

        public void Insert(int index, object item)
        {
            _InternalList.Insert(index, (MT)item);
            OnCollectionChanged();
        }

        public bool Contains(MT item)
        {
            return _InternalList.Contains(item);
        }

        public bool Contains (object item)
        {
            return _InternalList.Contains(item);
        }

        public void Clear()
        {
            ((IList)_InternalList).Clear();
            OnCollectionChanged();
        }

        public object this[int index]
        {
            get
            {
                return ((IList)_InternalList)[index];
            }
            set
            {
                ((IList)_InternalList)[index] = (MT)value;
                OnCollectionChanged();
            }
        }

        MT IList<MT>.this[int index]
        {
            get
            {
                return ((IList<MT>)_InternalList)[index];
            }
            set
            {
                ((IList<MT>)_InternalList)[index] = value;
                OnCollectionChanged();
            }
        }

        protected virtual void OnCollectionChanged()
        {

        }
    }

}