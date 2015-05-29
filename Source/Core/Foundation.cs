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

	//[Register("Thickness", true)]
	public struct Thickness
	{
		//[Export("Left"), Browsable(true)]
		public double Left {get; set;}
		//[Export("Top"), Browsable(true)]
		public double Top  {get; set;}
		//[Export("Right"), Browsable(true)]
		public double Right { get; set; }
		//[Export("Bottom"), Browsable(true)]
		public double Bottom { get; set; }	

        public double TotalWidth { get { return Left + Right; } }

        public double TotalHeight { get { return Top + Bottom; } }

		public Thickness (double left, double top, double right, double bottom) : this()
		{ 
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

        public override string ToString()
        {
            return string.Format("[Thickness: Left={0}, Top={1}, Right={2}, Bottom={3}]", Left, Top, Right, Bottom);
        }

        public static Thickness None
        {
            get
            {
                return new Thickness(0, 0, 0, 0);
            }
        }

        public static Thickness FromString (string s)
        {
            string[] substrings = s.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            double[] values = new double[4];
            for (int i = 0; i < substrings.Length; i++)
            {
                values[i] = double.Parse(substrings[i]);
            }
            if ( substrings.Length == 1)
            {
                return new Thickness(values[0], values[0], values[0], values[0]);
            }
            else if ( substrings.Length == 2 )
            {
                return new Thickness(values[0], values[1], values[0], values[1]);
            }
            else if ( substrings.Length == 4 )
            {
                return new Thickness(values[0], values[1], values[2], values[3]);
            }
            else
            {
                throw new Exception("Could not parse thickness");
            }                            
        }

        public static bool operator == (Thickness t1, Thickness t2)
        {
            return (t1.Left == t2.Left &&
                t1.Right == t2.Right &&
                t1.Top == t2.Top &&
                t1.Bottom == t2.Bottom);
        }

        public static bool operator != (Thickness t1, Thickness t2)
        {
            return (t1.Left != t2.Left ||
                t1.Right != t2.Right ||
                t1.Top != t2.Top ||
                t1.Bottom != t2.Bottom);
        }
	}

	public struct Size
	{
		public double Width {get; set;}
		public double Height { get; set; }

		public Size (double width, double height) : this()
		{
			Width = width;
			Height = height;
		}

        public static Size operator - (Size a, Size b)
        {
            return new Size(a.Width - b.Width, a.Height - b.Height);
        }

        public override string ToString()
        {
            return string.Format("[Size: Width={0}, Height={1}]", Width, Height);
        }
	}

    public struct Point
    {	
        /// <summary>
        /// Returns a point that is as close to the original point as possible
        /// while still remaining within the boundaries of two other points.
        /// </summary>
        /// <param name="ptSource">The point being tested</param>
        /// <param name="pt1">The upper-left point of the bounding box</param>
        /// <param name="pt2">The lower-right point of the bounding box</param>
        /// <returns>A point as close as possible to the original while still within the bounding box</returns>
        public static Point EnsureBetween(Point ptSource, Point pt1, Point pt2)
        {
            Point ptTemp = new Point(ptSource.X, ptSource.Y);
            ptTemp.X = Math.Max(pt1.X, ptTemp.X);
            ptTemp.X = Math.Min(pt2.X, ptTemp.X);
            ptTemp.Y = Math.Max(pt1.Y, ptTemp.Y);
            ptTemp.Y = Math.Min(pt2.Y, ptTemp.Y);
            return ptTemp;
        }

        
        public Point(double x, double y)
        {
            _X = x;
            _Y = y;
        }


        public override string ToString()
        {
            return string.Format("[Point: X={0}, Y={1}]", X, Y);
        }

        #region double X property
        private double _X;
        public double X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
            }
        }
        #endregion


        #region double Y property
        private double _Y;
        public double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
            }
        }
        #endregion


        public static Point FromString(string s)
        {
            string strX = s.LeftOf(',');
            string strY = s.RightOf(',');

            double X = double.Parse(strX);
            double Y = double.Parse(strY);

            return new Point(X,Y);
        }

    }

    public struct Rect
    {

        double _X;
        double _Y;
        double _Width;
        double _Height;

        public static Rect Empty
        {
            get
            {
                return new Rect { X = 0, Y = 0, Width = 0, Height = 0 };
            }
        }


        /// <summary>
        /// Creates a new rectangle, guaranteed to have its (X,Y) coordinate be on the upper left boundary of the rectangle.
        /// </summary>
        /// <param name="x">The origin X coordinate.</param>
        /// <param name="y">The origin Y coordinate</param>
        /// <param name="width">The width of the rectange. If negative, the origin is adjusted accordingly.</param>
        /// <param name="height"></param>
        public Rect(double x, double y, double width, double height)
        {
            _X = width >= 0 ? x : x + width;
            _Y = height >= 0 ? y : y + height;
            _Width = Math.Abs(width);
            _Height = Math.Abs(height);
        }

        public Rect(Point pt1, Point pt2)
            : this(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y)
        {

        }

        public Rect (Point pt, Size size ) : this ( pt.X, pt.Y, size.Width, size.Height )
        {

        }




        public double X
        {
            get { return _X; }
            set
            {
                _X = value;
            }
        }


        public double Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
            }
        }



        public double Width
        {
            get { return _Width; }
            set
            {
                _Width = value;

            }
        }


        public double Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
            }
        }

        public double Left
        {
            get
            {
                return X;
            }
        }

        public double Top
        {
            get
            {
                return Y;
            }
        }

        public double Right
        {
            get
            {
                return X + Width;
            }
        }
        public double Bottom
        {
            get
            {
                return Y + Height;
            }
        }

        public Point TopLeft()
        {
            return new Point(Left, Top);
        }

        public Point TopRight()
        {
            return new Point(Right, Top);
        }

        public Point BottomLeft()
        {
            return new Point(Left, Bottom);
        }

        public Point BottomRight()
        {
            return new Point(Right, Bottom);
        }


        public bool IsEmpty
        {
            get
            {
                return (_X == 0) && (_Y == 0) && (_Width == 0) && (_Height == 0);
            }
        }

        public Rect Translate ( double x, double y )
        {
            return new Rect(this.X + x, this.Y + y, this.Width, this.Height);
        }


        public override string ToString()
        {
            return string.Format("[Rect: X={0}, Y={1}, Width={2}, Height={3}, Left={4}, Top={5}, Right={6}, Bottom={7}, IsEmpty={8}]", X, Y, Width, Height, Left, Top, Right, Bottom, IsEmpty);
        }


    }

}