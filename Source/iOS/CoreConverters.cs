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
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;
using Camelot.Core;



namespace Camelot.iOS
{
    public static class CoreConverters
    {
        public static Rect ToFrameworkRect ( this RectangleF r )
        {
            return new Rect(r.X, r.Y, r.Width, r.Height);
        }

        public static RectangleF ToPlatformRect ( this Rect r)
        {
            return new RectangleF((float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
        }

        public static Camelot.Core.Point ToFrameworkPoint (this PointF pt)
        {
            return new Camelot.Core.Point(pt.X, pt.Y);
        }

        public static PointF ToPlatformPoint (this Camelot.Core.Point pt)
        {
            return new PointF((float)pt.X, (float)pt.Y);
        }

		public static CGAffineTransform ToPlatformTransform (this CompositeTransform t)
		{
			return new CGAffineTransform ((float)t.M11, (float)t.M12, (float)t.M21, (float)t.M22, (float)t.X0, (float)t.Y0);
		}

        public static UIColor ToPlatformColor (this Camelot.Core.Color color)
        {
            return new UIColor( (float)color.R / 255, (float)color.G / 255, (float)color.B / 255, (float)color.A / 255);
        }
    }
}

