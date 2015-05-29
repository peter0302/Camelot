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
    public class UIElementCollection : IList, ICollection, IEnumerable
    {
        List<UIElement> _InternalList = new List<UIElement>();
        FrameworkElement _LogicalParent;
        UIElement _VisualParent;

        public UIElementCollection(UIElement visualParent, FrameworkElement logicalParent)
        {
            _LogicalParent = logicalParent;
            _VisualParent = visualParent;
        }

        protected void ClearLogicalParent ( UIElement element )
        {
            if (element is FrameworkElement)
                (element as FrameworkElement).Parent = null;
        }

        protected void SetLogicalParent ( UIElement element )
        {
            if (element is FrameworkElement)
                (element as FrameworkElement).Parent = _LogicalParent;
        }

        private void InternalAdd ( UIElement element )
        {
            if (this._LogicalParent != null)
                SetLogicalParent(element);
            if (this._VisualParent != null)
                _VisualParent.AddVisualChildInternal (element);
            _VisualParent.InvalidateMeasure();
        }

        private void InternalRemove ( UIElement element, bool immediateInvalidate )
        {
            if (this._LogicalParent != null)
                ClearLogicalParent(element);
            if (this._VisualParent != null)
                _VisualParent.RemoveVisualChildInternal (element);
            if ( immediateInvalidate )
                _VisualParent.InvalidateMeasure();
        }

        #region ICollection implementation


        public int Count
        {
            get
            {
                return _InternalList.Count;
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
                return this;
            }
        }


        public void CopyTo(Array array, int index)
        {
            _InternalList.ToArray().CopyTo(array, index);
        }


        #endregion

        #region IEnumerable implementation

        public IEnumerator GetEnumerator()
        {
            return _InternalList.GetEnumerator();
        }

        #endregion

        #region IList implementation


        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public object this[int index] 
        {
            get { return _InternalList[index]; }
            set 
            {
                UIElement oldElement = _InternalList[index];
                _InternalList[index] = (UIElement)value;
                InternalRemove(oldElement, true);
            }
        }
        


        public int Add(object value)
        {
            _InternalList.Add((UIElement)value);
            InternalAdd((UIElement)value);
            return _InternalList.Count - 1;
        }

        public void Clear()
        {            
            UIElement[] elements = new UIElement[_InternalList.Count];
            _InternalList.CopyTo(elements);
            _InternalList.Clear();
            foreach ( UIElement e in elements )
            {
                InternalRemove(e, false);
            }
            _VisualParent.InvalidateMeasure();
        }

        public bool Contains(object value)
        {
            return _InternalList.Contains((UIElement)value);
        }

        public int IndexOf(object value)
        {
            return _InternalList.IndexOf((UIElement)value);
        }

        public void Insert(int index, object value)
        {
            _InternalList.Insert(index, (UIElement)value);
            InternalAdd((UIElement)value);
        }

        public void Remove(object value)
        {
            _InternalList.Remove((UIElement)value);
            InternalRemove((UIElement)value, true);
        } 

        public void RemoveAt(int index)
        {
            UIElement oldElement = _InternalList[index];
            _InternalList.RemoveAt(index);
            InternalRemove(oldElement, true);            
        }

        #endregion
    }
}