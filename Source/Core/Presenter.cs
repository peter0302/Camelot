/***********************************************************************************************
 * � Copyright 2014-2015 Peter Moore. All rights reserved.
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
    public class Presenter : FrameworkElement
    {
        public static readonly DataTemplate StringPresenterTemplate = new DataTemplate { Template = new StringPresenterTemplate() };
    }


    internal class StringPresenterTemplate : TemplateContent
    {
        public override object Play()
        {
            TextBlock block = new TextBlock();
            Binding binding = new Binding();
            BindingOperations.SetBinding(block, TextBlock.TextProperty, binding);
            return block;
        }
    }


}