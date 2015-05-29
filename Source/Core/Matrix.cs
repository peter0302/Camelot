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
    public struct Matrix
    {        
		#region double M11 property
        public double M11
        {
            get;
            set;
        }
		#endregion
        
		#region double M12 property
        public double M12
        {
            get;
            set;
        }
		#endregion
        
		#region double M21 property
        public double M21
        {
            get;
            set;
        }
		#endregion
        
		#region double M22 property
        public double M22
        {
            get;
            set;
        }
		#endregion
        
		#region double OffsetX property
        public double OffsetX
        {
            get;
            set;
        }
		#endregion
       
		#region double OffsetY property
        public double OffsetY
        {
            get;
            set;
        }
		#endregion

        #region Matrix Identity static property
        private static Matrix _Identity = new Matrix();
        public static Matrix Identity
        {
            get
            {
                return _Identity;
            }
        }
        #endregion

        #region bool IsIdentity property
        public bool IsIdentity
        {
            get
            {
                return this.Equals(Matrix._Identity);
            }
        }
        #endregion

        
		#region double Determinant property
		public double Determinant
		{
			get
			{
                return this.M11 * this.M22 - this.M12 * this.M21;
			}
		}
		#endregion


        public override bool Equals(object obj)
        {
            if (!(obj is Matrix))
                return false;
            Matrix m = (Matrix)obj;
            return (m.M11 == this.M11 && m.M12 == this.M12 && m.M21 == this.M21 && m.M22 == this.M22 && m.OffsetX == this.OffsetX && m.OffsetY == this.OffsetY);
        }

        public bool Equals (Matrix m)
        {
            return this.Equals((object)m);
        }

        public override int GetHashCode()
        {
            return (int)((this.M11 + this.M12 + this.M21 + this.M22 + this.OffsetX + this.OffsetY) * 100);
        }

    }
}