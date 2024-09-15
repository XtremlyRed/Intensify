using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using static System.Reflection.BindingFlags;

namespace Intensify.Wpf;

/// <summary>
/// a class of <see cref="Interaction"/>
/// </summary>
public static class Interaction
{
    /// <summary>
    /// Gets the animations.
    /// </summary>
    /// <param name="element">The object.</param>
    /// <returns></returns>
    public static AnimationCollection GetAnimations(FrameworkElement element)
    {
        if (element.GetValue(AnimationsProperty) is not AnimationCollection animations)
        {
            animations = new AnimationCollection();

            animations.Attach(element!);

            element.SetValue(AnimationsProperty, animations);
        }

        return animations;
    }

    /// <summary>
    /// Sets the animations.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="animations">The animations.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void SetAnimations(FrameworkElement element, AnimationCollection animations)
    {
        if (animations is not null)
        {
            animations.Attach(element!);

            for (int i = 0, length = animations.Count; i < length; i++)
            {
                var animation = animations[i];

                if (animations[i] is null)
                {
                    continue;
                }
                Internal.Extensions.Register(element!, animations[i]);
            }
        }

        element.SetValue(AnimationsProperty, animations);
    }

    /// <summary>
    /// The animations property
    /// </summary>
    public static readonly DependencyProperty AnimationsProperty = DependencyProperty.RegisterAttached(
        "ShadowAnimations",
        typeof(AnimationCollection),
        typeof(Interaction),
        new FrameworkPropertyMetadata(null)
    );

    /// <summary>
    /// get the transitions
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static TransitionCollection GetTransitions(FrameworkElement element)
    {
        if (element.GetValue(TransitionsProperty) is not TransitionCollection transitions)
        {
            transitions = new TransitionCollection();

            transitions.Attach(element!);

            element.SetValue(TransitionsProperty, transitions);
        }

        return transitions;
    }

    /// <summary>
    /// set the transitions
    /// </summary>
    /// <param name="element"></param>
    /// <param name="transitions"></param>

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void SetTransitions(FrameworkElement element, TransitionCollection transitions)
    {
        if (transitions is not null)
        {
            transitions.Attach(element!);

            for (int i = 0, length = transitions.Count; i < length; i++)
            {
                var animation = transitions[i];

                if (transitions[i] is null)
                {
                    continue;
                }

                Internal.Extensions.Register(element!, transitions[i]);
            }
        }

        element.SetValue(TransitionsProperty, transitions);
    }

    /// <summary>
    /// the transitions property
    /// </summary>
    public static readonly DependencyProperty TransitionsProperty = DependencyProperty.RegisterAttached(
        "ShadowTransitions",
        typeof(TransitionCollection),
        typeof(Interaction),
        new FrameworkPropertyMetadata(null)
    );
}
