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
    public enum FrameworkPropertyMetadataOptions
    {
        None = 0,
        AffectsMeasure = 1,
        AffectsArrange = 2,
        AffectsParentMeasure = 4,
        AffectsParentArrange = 8,        
        AffectsRender = 0x10,
        Inherits = 0x20,
        OverridesInheritanceBehavior = 0x40,
        NotDataBindable = 0x80,
        BindsTwoWayByDefault = 0x100,        
        Journal = 0x400,        
        SubPropertiesDoNotAffectRender = 0x800
    }

    public class FrameworkPropertyMetadata : PropertyMetadata
    {
        FrameworkPropertyMetadataOptions _Flags;

        public FrameworkPropertyMetadata (object defaultValue, FrameworkPropertyMetadataOptions flags) : base (defaultValue)
        {
            this._Flags = flags;
        }

        public FrameworkPropertyMetadata (object defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback propertyChangedCallback) 
            : base (defaultValue, propertyChangedCallback)
        {
            this._Flags = flags;
        }

        public bool AffectsArrange
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.AffectsArrange) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.AffectsArrange;
            }
        }

        public bool AffectsMeasure
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.AffectsMeasure) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.AffectsMeasure;
            }
        }

        public bool AffectsParentMeasure
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.AffectsParentMeasure) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.AffectsParentMeasure;
            }
        }

        public bool AffectsParentArrange
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.AffectsParentArrange) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.AffectsParentArrange;
            }
        }

        public bool AffectsRender
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.AffectsRender) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.AffectsRender;
            }
        }

        public bool Inherits
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.Inherits) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.Inherits;
            }
        }

        public bool OverridesInheritanceBehavior
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.OverridesInheritanceBehavior) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.OverridesInheritanceBehavior;
            }
        }

        public bool NotDataBindable
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.NotDataBindable) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.NotDataBindable;
            }
        }

        public bool BindsTwoWayByDefault
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.BindsTwoWayByDefault) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.BindsTwoWayByDefault;
            }
        }

        public bool Journal
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.Journal) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.Journal;
            }
        }

        public bool SubPropertiesDoNotAffectRender
        {
            get
            {
                return (_Flags & FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender) > 0;
            }
            set
            {
                _Flags |= FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender;
            }
        }
    }

}