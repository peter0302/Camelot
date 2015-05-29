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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camelot.Core
{
    public enum RoutingStrategy
    {
        Bubble,
        Direct,
        Tunnel
    }

    public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);
	    
    public class RoutedEventArgs : EventArgs
    {
        public RoutedEventArgs()
        {

        }


        /// <summary>
        /// Use this constructor for platforms that already have routed event handling (e.g., Windows)
        /// </summary>
        /// <param name="platformArgs">The platform-specific event args (e.g., TappedRoutedEventArgs)</param>
        public RoutedEventArgs(object platformArgs)
        {
            _PlatformArgs = platformArgs;
        }


        /// <summary>
        /// Use this constructor for platforms that use our custom routed event handling system (e.g., iOS)
        /// </summary>
        /// <param name="routedEvent">The RoutedEvent for which to create arguments.</param>
        public RoutedEventArgs(RoutedEvent routedEvent, object source)
        {
            this._RoutedEvent = routedEvent;
            this._Source = source;
            this._OriginalSource = source;
        }

        protected object _PlatformArgs;

        bool _Handled;
        public bool Handled
        {
            get
            {
                return _Handled;
            }
            set
            {
                _Handled = value;
                if (_PlatformArgs != null)
					EventsService.Service.MarkRoutedEventHandled(_PlatformArgs, value);
            }
        }

        object _OriginalSource;
        public object OriginalSource
        {
            get
            {
                return _OriginalSource;
            }
            internal set
            {
                _OriginalSource = value;
            }
        }

        RoutedEvent _RoutedEvent;
        public RoutedEvent RoutedEvent
        {
            get
            {
                return _RoutedEvent;
            }
            set
            {
                _RoutedEvent = value;
            }
        }


        object _Source;
        public object Source
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;
                //OnSetSource(_Source);
            }
        }


        public virtual void InvokeEventHandler(Delegate genericHandler, object genericTarget)
        {
            ((RoutedEventHandler)genericHandler)(this._Source, this);
        }

        protected virtual void OnSetSource(object source)
        {

        }
    }


    public static class EventManager
    {
        public static RoutedEvent RegisterRoutedEvent (string name, RoutingStrategy routingStrategy, Type handlerType, Type ownerType)
        {
            return RoutedEvent.Create(name, handlerType, ownerType, routingStrategy);
        }
    }



    public class RoutedEvent
    {        
        Type _HandlerType;
        public Type HandlerType { get { return _HandlerType; } }

        string _Name;
        public string Name { get { return _Name; } }

        Type _OwnerType;
        public Type OwnerType { get { return _OwnerType; } }

        RoutingStrategy _RoutingStrategy;
        public RoutingStrategy RoutingStrategy { get { return _RoutingStrategy; } }

        public RoutedEvent AddOwner(Type ownerType)
        {
            return new RoutedEvent { _Name = _Name, _HandlerType = _HandlerType, _OwnerType = ownerType, _RoutingStrategy = RoutingStrategy };
        }
			
        static internal RoutedEvent Create(string name, Type handlerType, Type ownerType, RoutingStrategy routingStrategy)
        {
            return new RoutedEvent { _Name = name, _HandlerType = handlerType, _OwnerType = ownerType, _RoutingStrategy = routingStrategy }; 
        }			        
    }
}
