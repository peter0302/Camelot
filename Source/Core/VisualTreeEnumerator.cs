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

namespace Camelot.Core.Internal
{
    /// <summary>
    /// Provides an enumerator for all the visual children of the parent UIElement, even
    /// traversing the child tree branches without the caller having to worry about
    /// recursion, etc.
    /// </summary>
    internal class VisualTreeEnumerator : IEnumerator
    {
        UIElement _parent;
        UIElement _currentParent;
        int _currentChild = -1;

        List<KeyValuePair<UIElement, int>> _History = new List<KeyValuePair<UIElement, int>>();

        public VisualTreeEnumerator(UIElement parent)
        {
            _parent = parent;
            _currentParent = parent;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public object Current
        {
            get
            {
                if (_currentChild == -1)
                    return _currentParent;
                else
                    return _currentParent.GetVisualChildInternal(_currentChild);
            }
        }

        public bool MoveNext()
        {
            _currentChild++;
            if ( _currentChild >= _currentParent.VirtualChildrenCountInternal )
            {
                if (_History.Count == 0)
                    return false;
                var last = _History[_History.Count - 1];
                _History.RemoveAt(_History.Count - 1);
                _currentParent = last.Key;
                _currentChild = last.Value;
                return MoveNext();
            }
            else
            {
                UIElement child = _currentParent.GetVisualChildInternal(_currentChild);
                _History.Add(new KeyValuePair<UIElement, int>(_currentParent, _currentChild));
                _currentParent = child;
                _currentChild = -1;
                return true;
            }
        }

        public void Reset()
        {
            _currentParent = _parent;
            _currentChild = -1;
        }

    }
}