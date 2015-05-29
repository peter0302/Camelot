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
using System.Collections.ObjectModel;

namespace Camelot.Core
{
    
    public sealed class ItemContainerGenerator : IItemContainerGenerator
    {
        internal ItemContainerGenerator(IList<object> itemsInternal ) : base()
        {
            _Items = new ReadOnlyCollection<object>(itemsInternal);
        }

        private List<DependencyObject> _Containers = new List<DependencyObject>();

        
        private ReadOnlyCollection<object> _Items;
        /// <summary>
        /// Gets the collection of items that belong to this ItemContainerGenerator.
        /// </summary>
        public ReadOnlyCollection<object> Items
        {
            get
            {
                return _Items;
            }
        }

        /// <summary>
        /// The generation status of the ItemContainerGenerator.
        /// </summary>
        public GeneratorStatus Status
        {
            get;
            private set;
        }

        /// <summary>
        /// The ItemsChanged event is raised by a ItemContainerGenerator to inform layouts that the items collection has changed.
        /// </summary>
        public event ItemsChangedEventHandler ItemsChanged;

        /// <summary>
        /// The StatusChanged event is raised by a ItemContainerGenerator to inform controls that its status has changed.
        /// </summary>
        public event EventHandler StatusChanged;

        /// <summary>
        /// Returns the element corresponding to the item at the given index within the ItemCollection.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DependencyObject ContainerFromIndex (int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the UIElement corresponding to the given item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public DependencyObject ContainerFromItem ( object item )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the index to an item that corresponds to the specified, generated UIElement.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public int IndexFromContainer (DependencyObject container)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the index to an item that corresponds to the specified, generated UIElement, optionally recursively searching hierarchical items.
        /// </summary>
        /// <param name="container">The DependencyObject that corresponds to the item to the index to be returned.</param>
        /// <param name="returnLocalIndex">true to search the current level of hierarchical items; false to recursively search hierarchical items.</param>
        /// <returns>An Int32 index to an item that corresponds to the specified, generated UIElement or -1 if container is not found.</returns>
        public int IndexFromContainer (DependencyObject container, bool returnLocalIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the item that corresponds to the specified, generated UIElement.
        /// </summary>
        /// <param name="container">The DependencyObject that corresponds to the item to be returned.</param>
        /// <returns>A DependencyObject that is the item which corresponds to the specified, generated UIElement. If the UIElement has not been generated, UnsetValue is returned. </returns>
        public object ItemFromContainer (DependencyObject container)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the container element used to display the next item.
        /// </summary>
        /// <returns></returns>
        public DependencyObject GenerateNext()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the container element used to display the next item, and whether the container element has been newly generated (realized).
        /// </summary>
        /// <param name="isNewlyRealized">Is true if the returned DependencyObject is newly generated (realized); otherwise, false.</param>
        /// <returns></returns>
        public DependencyObject GenerateNext(out bool isNewlyRealized)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the GeneratorPosition object that maps to the item at the specified index.
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        public GeneratorPosition GeneratorPositionFromIndex(int itemIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the ItemContainerGenerator appropriate for use by the specified panel.
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public ItemContainerGenerator GetItemContainerGeneratorForPanel(Panel panel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the index that maps to the specified GeneratorPosition.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int IndexFromGeneratorPosition(GeneratorPosition position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prepares the specified element as the container for the corresponding item.
        /// </summary>
        /// <param name="container"></param>
        public void PrepareItemContainer(DependencyObject container)
        {

        }

        /// <summary>
        /// This member supports the Windows Presentation Foundation (WPF) infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="count"></param>
        public void Remove(GeneratorPosition position, int count)
        {

        }

        /// <summary>
        /// Removes all generated (realized) items.
        /// </summary>
        public void RemoveAll()
        {

        }

        /// <summary>
        /// Prepares the generator to generate items, starting at the specified GeneratorPosition, and in the specified GeneratorDirection.
        /// </summary>
        /// <param name="position">A GeneratorPosition that specifies the position of the item to start generating items at.</param>
        /// <param name="direction">A GeneratorDirection that specifies the direction which to generate items.</param>
        /// <returns>An IDisposable object that tracks the lifetime of the generation process.</returns>
        public IDisposable StartAt(GeneratorPosition position, GeneratorDirection direction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prepares the generator to generate items, starting at the specified GeneratorPosition, and in the specified GeneratorDirection, and controlling whether or not to start at a generated (realized) item.
        /// </summary>
        /// <param name="position">A GeneratorPosition that specifies the position of the item to start generating items at.</param>
        /// <param name="direction">A GeneratorDirection that specifies the direction which to generate items.</param>
        /// <param name="allowStartAtRealizedItem">A Boolean that specifies whether to start at a generated (realized) item.</param>
        /// <returns></returns>
        public IDisposable StartAt(GeneratorPosition position, GeneratorDirection direction, bool allowStartAtRealizedItem)
        {
            throw new NotImplementedException();
        }
    }


    public enum GeneratorStatus
    {
        NotStarted,
        ContainersGenerated,
        Error,
        GeneratingContainers,        
    }

    public struct GeneratorPosition
    {
        int Index { get; set; }
        int Offset { get; set; }
    }

    public enum GeneratorDirection
    {
        Backward,
        Forward
    }

    public interface IItemContainerGenerator
    {
        
        DependencyObject GenerateNext();
        
        DependencyObject GenerateNext(out bool isNewlyRealized);

        GeneratorPosition GeneratorPositionFromIndex(int itemIndex);

        ItemContainerGenerator GetItemContainerGeneratorForPanel(Panel panel);
        
        int IndexFromGeneratorPosition(GeneratorPosition position);

        void PrepareItemContainer(DependencyObject container);
        
        void Remove(GeneratorPosition position, int count);
        
        void RemoveAll();
        
        IDisposable StartAt(GeneratorPosition position, GeneratorDirection direction);
       
        IDisposable StartAt(GeneratorPosition position, GeneratorDirection direction, bool allowStartAtRealizedItem);
    }
}