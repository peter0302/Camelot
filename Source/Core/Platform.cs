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
using System.Reflection;

using System.Threading.Tasks;

namespace Camelot.Core
{
    public interface IPlatformView
    {
        void WireUpGestureRecognizers();
        void RemoveGestureRecognizers();
        Rect Bounds { get; set; }
        FrameworkElement Element { get; set; }
        void AddChild(IPlatformView child);
        void RemoveFromParent();
        void Invalidate();
        Point GetRelativePoint(object pointerArgs);
    }
    

    [AttributeUsage(AttributeTargets.Class, Inherited=false)]
    public class HasPlatformView : Attribute
    {
        public HasPlatformView()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited=true)]
    public class PlatformViewAttribute : System.Attribute
    {
        public PlatformViewAttribute()
        {

        }
        
        public PlatformViewAttribute(Type platformType, Type frameworkType)
        {
            PlatformType = platformType;
            FrameworkType = frameworkType;
        }


        ConstructorInfo _PlatformConstructor;
        internal ConstructorInfo PlatformConstructor
        {
            get
            {
                return _PlatformConstructor;
            }
        }

        Type _PlatformType;
        internal Type PlatformType
        {
            get
            {
                return _PlatformType;
            }
            set
            {
                _PlatformType = value;
                _PlatformConstructor = _PlatformType.GetTypeInfo().DeclaredConstructors
                                .Where(c => c.GetParameters().Count() == 0)
                                .Select(c => c).FirstOrDefault();
            }
        }

        Type _FrameworkType;
        internal Type FrameworkType
        {
            get
            {
                return _FrameworkType;
            }
            private set
            {
                _FrameworkType = value;
            }
        }
    }


}
