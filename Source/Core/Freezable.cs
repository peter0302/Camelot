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
    public abstract class Freezable : DependencyObject
    {
        // Summary:
        //     Initializes a new instance of a System.Windows.Freezable derived class.
        protected Freezable()
        {

        }

        // Summary:
        //     Gets a value that indicates whether the object can be made unmodifiable.
        //
        // Returns:
        //     true if the current object can be made unmodifiable or is already unmodifiable;
        //     otherwise, false.
        public bool CanFreeze { get; protected set;  }

        //
        // Summary:
        //     Gets a value that indicates whether the object is currently modifiable.
        //
        // Returns:
        //     true if the object is frozen and cannot be modified; false if the object
        //     can be modified.
        public bool IsFrozen { get; protected set; }

        // Summary:
        //     Occurs when the System.Windows.Freezable or an object it contains is modified.
        public event EventHandler Changed;

        // Summary:
        //     Creates a modifiable clone of the System.Windows.Freezable, making deep copies
        //     of the object's values. When copying the object's dependency properties,
        //     this method copies expressions (which might no longer resolve) but not animations
        //     or their current values.
        //
        // Returns:
        //     A modifiable clone of the current object. The cloned object's System.Windows.Freezable.IsFrozen
        //     property is false even if the source's System.Windows.Freezable.IsFrozen
        //     property is true.
        public Freezable Clone()
        {
            throw new NotImplementedException();            
        }


        //
        // Summary:
        //     Makes the instance a clone (deep copy) of the specified System.Windows.Freezable
        //     using base (non-animated) property values.
        //
        // Parameters:
        //   sourceFreezable:
        //     The object to clone.
        protected virtual void CloneCore(Freezable sourceFreezable)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Creates a modifiable clone (deep copy) of the System.Windows.Freezable using
        //     its current values.
        //
        // Returns:
        //     A modifiable clone of the current object. The cloned object's System.Windows.Freezable.IsFrozen
        //     property is false even if the source's System.Windows.Freezable.IsFrozen
        //     property is true.
        public Freezable CloneCurrentValue()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Makes the instance a modifiable clone (deep copy) of the specified System.Windows.Freezable
        //     using current property values.
        //
        // Parameters:
        //   sourceFreezable:
        //     The System.Windows.Freezable to be cloned.
        protected virtual void CloneCurrentValueCore(Freezable sourceFreezable)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Windows.Freezable class.
        //
        // Returns:
        //     The new instance.
        protected Freezable CreateInstance()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     When implemented in a derived class, creates a new instance of the System.Windows.Freezable
        //     derived class.
        //
        // Returns:
        //     The new instance.
        protected abstract Freezable CreateInstanceCore();

        //
        // Summary:
        //     Makes the current object unmodifiable and sets its System.Windows.Freezable.IsFrozen
        //     property to true.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Windows.Freezable cannot be made unmodifiable.
        public void Freeze()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     If the isChecking parameter is true, this method indicates whether the specified
        //     System.Windows.Freezable can be made unmodifiable. If the isChecking parameter
        //     is false, this method attempts to make the specified System.Windows.Freezable
        //     unmodifiable and indicates whether the operation succeeded.
        //
        // Parameters:
        //   freezable:
        //     The object to check or make unmodifiable. If isChecking is true, the object
        //     is checked to determine whether it can be made unmodifiable. If isChecking
        //     is false, the object is made unmodifiable, if possible.
        //
        //   isChecking:
        //     true to return an indication of whether the object can be frozen (without
        //     actually freezing it); false to actually freeze the object.
        //
        // Returns:
        //     If isChecking is true, this method returns true if the specified System.Windows.Freezable
        //     can be made unmodifiable, or false if it cannot be made unmodifiable. If
        //     isChecking is false, this method returns true if the specified System.Windows.Freezable
        //     is now unmodifiable, or false if it cannot be made unmodifiable.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     When isChecking is false, the attempt to make freezable unmodifiable was
        //     unsuccessful; the object is now in an unknown state (it might be partially
        //     frozen).
        protected internal static bool Freeze(Freezable freezable, bool isChecking)
        {
            throw new NotImplementedException();
        }




        //
        // Summary:
        //     Makes the System.Windows.Freezable object unmodifiable or tests whether it
        //     can be made unmodifiable.
        //
        // Parameters:
        //   isChecking:
        //     true to return an indication of whether the object can be frozen (without
        //     actually freezing it); false to actually freeze the object.
        //
        // Returns:
        //     If isChecking is true, this method returns true if the System.Windows.Freezable
        //     can be made unmodifiable, or false if it cannot be made unmodifiable. If
        //     isChecking is false, this method returns true if the if the specified System.Windows.Freezable
        //     is now unmodifiable, or false if it cannot be made unmodifiable.
        protected virtual bool FreezeCore(bool isChecking)
        {
            throw new NotImplementedException();
        }



        //
        // Summary:
        //     Creates a frozen copy of the System.Windows.Freezable, using base (non-animated)
        //     property values. Because the copy is frozen, any frozen sub-objects are copied
        //     by reference.
        //
        // Returns:
        //     A frozen copy of the System.Windows.Freezable. The copy's System.Windows.Freezable.IsFrozen
        //     property is set to true.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Windows.Freezable cannot be frozen because it contains expressions
        //     or animated properties.
        public Freezable GetAsFrozen()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Makes the instance a frozen clone of the specified System.Windows.Freezable
        //     using base (non-animated) property values.
        //
        // Parameters:
        //   sourceFreezable:
        //     The instance to copy.
        protected virtual void GetAsFrozenCore(Freezable sourceFreezable)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Creates a frozen copy of the System.Windows.Freezable using current property
        //     values. Because the copy is frozen, any frozen sub-objects are copied by
        //     reference.
        //
        // Returns:
        //     A frozen copy of the System.Windows.Freezable. The copy's System.Windows.Freezable.IsFrozen
        //     property is set to true.
        public Freezable GetCurrentValueAsFrozen()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Makes the current instance a frozen clone of the specified System.Windows.Freezable.
        //     If the object has animated dependency properties, their current animated
        //     values are copied.
        //
        // Parameters:
        //   sourceFreezable:
        //     The System.Windows.Freezable to copy and freeze.
        protected virtual void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Called when the current System.Windows.Freezable object is modified.
        protected virtual void OnChanged()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Ensures that appropriate context pointers are established for a System.Windows.DependencyObjectType
        //     data member that has just been set.
        //
        // Parameters:
        //   oldValue:
        //     The previous value of the data member.
        //
        //   newValue:
        //     The current value of the data member.
        protected void OnFreezablePropertyChanged(DependencyObject oldValue, DependencyObject newValue)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     This member supports the Windows Presentation Foundation (WPF) infrastructure
        //     and is not intended to be used directly from your code.
        //
        // Parameters:
        //   oldValue:
        //     The previous value of the data member.
        //
        //   newValue:
        //     The current value of the data member.
        //
        //   property:
        //     The property that changed.
        protected void OnFreezablePropertyChanged(DependencyObject oldValue, DependencyObject newValue, DependencyProperty property)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Overrides the System.Windows.DependencyObject implementation of System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)
        //     to also invoke any System.Windows.Freezable.Changed handlers in response
        //     to a changing dependency property of type System.Windows.Freezable.
        //
        // Parameters:
        //   e:
        //     Event data that contains information about which property changed, and its
        //     old and new values.
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Ensures that the System.Windows.Freezable is being accessed from a valid
        //     thread. Inheritors of System.Windows.Freezable must call this method at the
        //     beginning of any API that reads data members that are not dependency properties.
        protected void ReadPreamble()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Raises the System.Windows.Freezable.Changed event for the System.Windows.Freezable
        //     and invokes its System.Windows.Freezable.OnChanged() method. Classes that
        //     derive from System.Windows.Freezable should call this method at the end of
        //     any API that modifies class members that are not stored as dependency properties.
        protected void WritePostscript()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Verifies that the System.Windows.Freezable is not frozen and that it is being
        //     accessed from a valid threading context. System.Windows.Freezable inheritors
        //     should call this method at the beginning of any API that writes to data members
        //     that are not dependency properties.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Windows.Freezable instance is frozen and cannot have its members
        //     written to.
        protected void WritePreamble()
        {
            throw new NotImplementedException();
        }

    }
}