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
    public class AnimationClock
    {

    }

    public class AnimationTimeline
    {

    }

    public enum HandoffBehavior
    {
        Compose,
        SnapshotAndReplace
    }

    public abstract class Animatable : Freezable
    {
        // Summary:
        //     Initializes a new instance of the System.Windows.Media.Animation.Animatable
        //     class.
        protected Animatable()
        {
            throw new NotImplementedException();
        }

        // Summary:
        //     Gets a value that indicates whether one or more System.Windows.Media.Animation.AnimationClock
        //     objects is associated with any of this object's dependency properties.
        //
        // Returns:
        //     true if one or more System.Windows.Media.Animation.AnimationClock objects
        //     is associated with any of this object's dependency properties; otherwise,
        //     false.
        public bool HasAnimatedProperties { get; protected set;  }

        // Summary:
        //     Applies an System.Windows.Media.Animation.AnimationClock to the specified
        //     System.Windows.DependencyProperty. If the property is already animated, the
        //     System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace handoff
        //     behavior is used.
        //
        // Parameters:
        //   dp:
        //     The property to animate.
        //
        //   clock:
        //     The clock with which to animate the specified property. If clock is null,
        //     all animations will be removed from the specified property (but not stopped).
        public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Applies an System.Windows.Media.Animation.AnimationClock to the specified
        //     System.Windows.DependencyProperty. If the property is already animated, the
        //     specified System.Windows.Media.Animation.HandoffBehavior is used.
        //
        // Parameters:
        //   dp:
        //     The property to animate.
        //
        //   clock:
        //     The clock with which to animate the specified property. If handoffBehavior
        //     is System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace and
        //     clock is null, all animations will be removed from the specified property
        //     (but not stopped). If handoffBehavior is System.Windows.Media.Animation.HandoffBehavior.Compose
        //     and clock is null, this method has no effect.
        //
        //   handoffBehavior:
        //     A value that specifies how the new animation should interact with any current
        //     animations already affecting the property value.
        public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Applies an animation to the specified System.Windows.DependencyProperty.
        //     The animation is started when the next frame is rendered. If the specified
        //     property is already animated, the System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace
        //     handoff behavior is used.
        //
        // Parameters:
        //   dp:
        //     The property to animate.
        //
        //   animation:
        //     The animation used to animate the specified property.If the animation's System.Windows.Media.Animation.Timeline.BeginTime
        //     is null, any current animations will be removed and the current value of
        //     the property will be held.If animation is null, all animations will be removed
        //     from the property and the property value will revert back to its base value.
        public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Applies an animation to the specified System.Windows.DependencyProperty.
        //     The animation is started when the next frame is rendered. If the specified
        //     property is already animated, the specified System.Windows.Media.Animation.HandoffBehavior
        //     is used.
        //
        // Parameters:
        //   dp:
        //     The property to animate.
        //
        //   animation:
        //     The animation used to animate the specified property.If handoffBehavior is
        //     System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace and the
        //     animation's System.Windows.Media.Animation.Timeline.BeginTime is null, any
        //     current animations will be removed and the current value of the property
        //     will be held. If handoffBehavior is System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace
        //     and animation is a null reference, all animations will be removed from the
        //     property and the property value will revert back to its base value.If handoffBehavior
        //     is System.Windows.Media.Animation.HandoffBehavior.Compose, this method will
        //     have no effect if the animation or its System.Windows.Media.Animation.Timeline.BeginTime
        //     is null.
        //
        //   handoffBehavior:
        //     A value that specifies how the new animation should interact with any current
        //     animations already affecting the property value.
        public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Creates a modifiable clone of this System.Windows.Media.Animation.Animatable,
        //     making deep copies of this object's values. When copying this object's dependency
        //     properties, this method copies resource references and data bindings (but
        //     they might no longer resolve) but not animations or their current values.
        //
        // Returns:
        //     A modifiable clone of this instance. The returned clone is effectively a
        //     deep copy of the current object. The clone's System.Windows.Freezable.IsFrozen
        //     property is false.
        public Animatable Clone()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Makes this System.Windows.Media.Animation.Animatable object unmodifiable
        //     or determines whether it can be made unmodifiable.
        //
        // Parameters:
        //   isChecking:
        //     true if this method should simply determine whether this instance can be
        //     frozen. false if this instance should actually freeze itself when this method
        //     is called.
        //
        // Returns:
        //     If isChecking is true, this method returns true if this System.Windows.Media.Animation.Animatable
        //     can be made unmodifiable, or false if it cannot be made unmodifiable. If
        //     isChecking is false, this method returns true if the if this System.Windows.Media.Animation.Animatable
        //     is now unmodifiable, or false if it cannot be made unmodifiable, with the
        //     side effect of having begun to change the frozen status of this object.
        protected override bool FreezeCore(bool isChecking)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Returns the non-animated value of the specified System.Windows.DependencyProperty.
        //
        // Parameters:
        //   dp:
        //     Identifies the property whose base (non-animated) value should be retrieved.
        //
        // Returns:
        //     The value that would be returned if the specified property were not animated.
        public object GetAnimationBaseValue(DependencyProperty dp)
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Specifies whether a dependency object should be serialized.
        //
        // Parameters:
        //   target:
        //     Represents an object that participates in the dependency property system.
        //
        // Returns:
        //     true to serialize target; otherwise, false. The default is false.
        public static bool ShouldSerializeStoredWeakReference(DependencyObject target)
        {
            throw new NotImplementedException();
        }
    }
}