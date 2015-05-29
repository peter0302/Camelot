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
    public enum RelativeSourceMode
    {
        FindAncestor,
        PreviousData,
        Self,
        TemplatedParent
    }

    public class RelativeSource : MarkupExtension
    {
        public RelativeSource()
        {

        }

        public RelativeSource(RelativeSourceMode mode)
        {
            this.Mode = mode;
        }

        public RelativeSource(RelativeSourceMode mode, Type ancestorType, int ancestorLevel )
        {
            this.AncestorLevel = ancestorLevel;
            this.Mode = mode;
            this.AncestorType = ancestorType;
        }

        public int AncestorLevel { get; set; }
        public Type AncestorType { get; set; }
        public RelativeSourceMode Mode { get; set; }

        public static RelativeSource Self
        {
            get
            {
                return new RelativeSource(RelativeSourceMode.Self);
            }
        }

        public static RelativeSource TemplatedParent
        {
            get
            {
                return new RelativeSource(RelativeSourceMode.TemplatedParent);
            }
        }

        public static RelativeSource PreviousData
        {
            get
            {
                return new RelativeSource(RelativeSourceMode.PreviousData);
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}