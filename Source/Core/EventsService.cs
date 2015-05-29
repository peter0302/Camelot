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

namespace Camelot.Core
{
	public static class EventsService
	{
		public static IEventsService Service {
			get;
			private set;
		}
		public static void Register(IEventsService service)
		{
			if (Service == null)
				Service = service;
		}
	}


	public interface IEventsService
	{
		void MarkRoutedEventHandled(object platformArgs, bool handled);
		Point GetRelativeEventPoint(object platformEventArgs, object relativeToPlatformObject);
		VirtualKey GetKeyboardEvent (object platformKeyArgs);
	}
}

