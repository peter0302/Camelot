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
using System.Collections.ObjectModel;

using Camelot.Core.Internal;

namespace Camelot.Core
{
    public enum BindingStatus
    {
        Active,
        AsyncRequestPending,
        Detached,
        Inactive,
        PathError,
        Unattached,
        UpdateSourceError,
        UpdateTargetError
    }

    public abstract class BindingExpressionBase// : Expression
    {
        internal BindingExpressionBase(BindingBase binding) 
        {
            this.ParentBindingBase = binding;
        }

        public BindingGroup BindingGroup
        {
            get; 
            internal set;
        }

        public virtual bool HasError
        {
            get;
            internal set;
        }

        public virtual bool HasValidationError
        {
            get;
            internal set;
        }

        public bool IsDirty
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the BindingBase object from which this BindingExpressionBase object is created.
        /// </summary>
        public BindingBase ParentBindingBase
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the status of the binding expression.
        /// </summary>
        public BindingStatus Status
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the element that is the binding target object of this binding expression.
        /// </summary>
        public DependencyObject Target
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the binding target property of this binding expression.
        /// </summary>
        public DependencyProperty TargetProperty
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the ValidationError that caused this instance of BindingExpressionBase to be invalid.
        /// </summary>
        public virtual ValidationError ValidationError
        {
            get;
            internal set;
        }

        List<ValidationError> _InternalValidationErrors = new List<ValidationError>();
        ReadOnlyCollection<ValidationError> _ValidationErrors;
        public virtual ReadOnlyCollection<ValidationError> ValidationErrors
        {
            get
            {
                return _ValidationErrors ?? (_ValidationErrors = new ReadOnlyCollection<ValidationError>(_InternalValidationErrors));
            }
        }

        /// <summary>
        /// Sends the current binding target value to the binding source in TwoWay or OneWayToSource bindings.
        /// </summary>
        public virtual void UpdateSource()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Forces a data transfer from the binding source to the binding target.
        /// </summary>
        public virtual void UpdateTarget()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Runs any ValidationRule objects on the associated Binding that have the ValidationStep property set to RawProposedValue or ConvertedProposedValue. This method does not update the source.
        /// </summary>
        /// <returns>true if the validation rules succeed; otherwise, false.</returns>
        /// <remarks>
        /// The ValidateWithoutUpdate method enables you to run validation rules on a binding without updating the source of the binding. 
        /// This is useful when you want to validate user input and update the source at different times in an application. For example, 
        /// suppose you have a form to update a data source that contains a submit button. You want to provide feedback to the user if an 
        /// invalid value is entered before the user attempts to submit the form. You can check the validity of a field by setting the 
        /// binding's UpdateSourceTrigger property to Explicit and calling ValidateWithoutUpdate when the TextBox loses focus.
        /// </remarks>
        public bool ValidateWithoutUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
