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
    public class StaticResource : MarkupExtension
    {
        public StaticResource() : base()
        {

        }

        public StaticResource (string resourceKey)
        {
            this.ResourceKey = resourceKey;
        }

        public string ResourceKey 
        {
            get;
            set;
        }

        public object FindResource (FrameworkElement start)
        {
            FrameworkElement current = start;
            while ( current != null )
            {
                if ( current.Resources.Contains(this.ResourceKey) )
                    return current.Resources[this.ResourceKey];
                current = (FrameworkElement)current.VisualParent;
            }

            if ( current == null )
            {
                // try to find resource in standard dictionary
                if (ResourceDictionary.StandardDictionary.Contains(this.ResourceKey))
                    return ResourceDictionary.StandardDictionary[this.ResourceKey];
            }

            return null;
            //throw new Exception ("Unable to locate resource" + this.ResourceKey);
        }

        public override object ProvideValue(System.IServiceProvider serviceProvider)
        {
            throw new System.NotImplementedException();
        }
    }
}