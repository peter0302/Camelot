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

    public class GeneralTransform : DependencyObject
    {
        protected GeneralTransform()
        {

        }

        public GeneralTransform Inverse
        {
            get
            {
                return this.InverseCore;
            }
        }

        protected virtual GeneralTransform InverseCore
        {
            get
            {
                return null;
            }
        }

        public Rect TransformBounds(Rect rect)
        {
            return TransformBoundsCore(rect);
        }

        protected virtual Rect TransformBoundsCore(Rect rect)
        {
            return rect;
        }

        public Point TransformPoint(Point point)
        {
            Point outPoint;
            if ( TryTransformCore (point, out outPoint))
            {
                return outPoint;
            }
            else
            {
                return point;
            }
        }
 
        public bool TryTransform(Point inPoint, out Point outPoint)
        {
            return TryTransformCore(inPoint, out outPoint);
        }
 
        protected virtual bool TryTransformCore(Point inPoint, out Point outPoint)
        {
            outPoint = inPoint;
            return true;
        }
    }
}
