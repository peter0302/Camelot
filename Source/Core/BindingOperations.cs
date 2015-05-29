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
    public static class BindingOperations
    {
        /// <summary>
        /// Creates and associates a new instance of BindingExpressionBase with the specified binding target property.
        /// </summary>
        /// <param name="target">The binding target of the binding.</param>
        /// <param name="dp">The target property of the binding.</param>
        /// <param name="binding">The BindingBase object that describes the binding.</param>
        /// <returns>The instance of BindingExpressionBase created for and associated with the specified property. 
        /// The BindingExpressionBase class is the base class of BindingExpression, MultiBindingExpression, 
        /// and PriorityBindingExpression.</returns>
        public static BindingExpressionBase SetBinding(DependencyObject target, DependencyProperty dp, BindingBase binding)
        {
            if (target == null || dp == null || binding == null)
                throw new ArgumentNullException();

            target.UnregisterBindingExpression(dp, false);
            var expr = binding.CreateBindingExpression(target, dp);
            target.SetValue(dp, expr);
            return expr;
        }

    }

}