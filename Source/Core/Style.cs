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
    [ContentProperty("Setters")]
    public class Style
    {
        public Style()
        {

        }

        public Style BasedOn
        {
            get;
            set;
        }

        public ResourceDictionary Resources
        {
            get;
            set;
        }

        private SetterBaseCollection _Setters = new SetterBaseCollection();
        public SetterBaseCollection Setters
        {
            get
            {
                return _Setters;
            }
        }

        private TriggerCollection _Triggers = new TriggerCollection();
        public TriggerCollection Triggers
        {
            get
            {
                return _Triggers;
            }
        }


        public Type TargetType
        {
            get;
            set;
        }

        // TODO: Triggers
    }


    /// <summary>
    /// Represents an attribute that is applied to the class definition and determines the TargetTypes of the properties that are of type Style.
    /// </summary>
    /// <remarks>
    /// Control authors apply this attribute to the class definition to specify the TargetTypes of the properties that are of type Style. 
    /// For example, if you look at the declaration of the ListBox class, this attribute is used to specify that the TargetType of the 
    /// ItemContainerStyle property is ListBoxItem. Subclasses inherit this definition but can redefine the TargetType of the property 
    /// by using this attribute on its own class definition.
    /// </remarks>
    [AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class StyleTypedPropertyAttribute : Attribute
    {
        public string Property { get; set; }
        public Type StyleTargetType { get; set; }

    }
}