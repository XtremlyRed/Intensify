﻿using System.Linq.Expressions;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Intensify.Wpf.Internal;

namespace Intensify.Wpf;

/// <summary>
///
/// </summary>
public static class Vector3DAnimationBuildExtension
{
    /// <summary>
    /// Builds the animation.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <param name="object">The object.</param>
    /// <param name="propertyExpression">The property expression.</param>
    /// <param name="toValue">To value.</param>
    /// <param name="duration">The duration.</param>
    /// <param name="completeCallback">The complete callback.</param>
    /// <returns></returns>
    public static Vector3DAnimation BuildAnimation<TObject>(
        this TObject @object,
        Expression<Func<TObject, Vector3D>> propertyExpression,
        Vector3D toValue,
        TimeSpan duration,
        Action? completeCallback = null
    )
        where TObject : DependencyObject
    {
        var property = propertyExpression.GetPropertyName();

        return BuildAnimation(@object, property, null, toValue, null, duration, null, completeCallback);
    }

    /// <summary>
    /// Builds the animation.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <param name="object">The object.</param>
    /// <param name="propertyExpression">The property expression.</param>
    /// <param name="fromValue">From value.</param>
    /// <param name="toValue">To value.</param>
    /// <param name="duration">The duration.</param>
    /// <param name="completeCallback">The complete callback.</param>
    /// <returns></returns>
    public static Vector3DAnimation BuildAnimation<TObject>(
        this TObject @object,
        Expression<Func<TObject, Vector3D>> propertyExpression,
        Vector3D fromValue,
        Vector3D toValue,
        TimeSpan duration,
        Action? completeCallback = null
    )
        where TObject : DependencyObject
    {
        var property = propertyExpression.GetPropertyName();

        return BuildAnimation(@object, property, fromValue, toValue, null, duration, null, completeCallback);
    }

    /// <summary>
    /// Builds the animation.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <param name="object">The object.</param>
    /// <param name="propertyExpression">The property expression.</param>
    /// <param name="toValue">To value.</param>
    /// <param name="beginTime">The begin time.</param>
    /// <param name="duration">The duration.</param>
    /// <param name="completeCallback">The complete callback.</param>
    /// <returns></returns>
    public static Vector3DAnimation BuildAnimation<TObject>(
        this TObject @object,
        Expression<Func<TObject, Vector3D>> propertyExpression,
        Vector3D toValue,
        TimeSpan beginTime,
        TimeSpan duration,
        Action? completeCallback = null
    )
        where TObject : DependencyObject
    {
        var property = propertyExpression.GetPropertyName();

        return BuildAnimation(@object, property, null, toValue, beginTime, duration, null, completeCallback);
    }

    /// <summary>
    /// Builds the animation.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <param name="object">The object.</param>
    /// <param name="propertyExpression">The property expression.</param>
    /// <param name="fromValue">From value.</param>
    /// <param name="toValue">To value.</param>
    /// <param name="beginTime">The begin time.</param>
    /// <param name="duration">The duration.</param>
    /// <param name="completeCallback">The complete callback.</param>
    /// <returns></returns>
    public static Vector3DAnimation BuildAnimation<TObject>(
        this TObject @object,
        Expression<Func<TObject, Vector3D>> propertyExpression,
        Vector3D? fromValue,
        Vector3D toValue,
        TimeSpan? beginTime,
        TimeSpan duration,
        Action? completeCallback = null
    )
        where TObject : DependencyObject
    {
        var property = propertyExpression.GetPropertyName();

        return BuildAnimation(@object, property, fromValue, toValue, beginTime, duration, null, completeCallback);
    }

    /// <summary>
    /// Builds the animation.
    /// </summary>
    /// <param name="object">The object.</param>
    /// <param name="animationProperty">The animation property.</param>
    /// <param name="fromValue">From value.</param>
    /// <param name="toValue">To value.</param>
    /// <param name="beginTime">The begin time.</param>
    /// <param name="duration">The duration.</param>
    /// <param name="easingFunction">The easing function.</param>
    /// <param name="completeCallback">The complete callback.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">
    /// object
    /// or
    /// animationProperty
    /// </exception>
    public static Vector3DAnimation BuildAnimation(
        this DependencyObject @object,
        string animationProperty,
        Vector3D? fromValue,
        Vector3D toValue,
        TimeSpan? beginTime,
        TimeSpan duration,
        IEasingFunction? easingFunction = null,
        Action? completeCallback = null
    )
    {
        _ = @object ?? throw new ArgumentNullException(nameof(@object));
        _ = string.IsNullOrWhiteSpace(animationProperty) ? throw new ArgumentNullException(nameof(animationProperty)) : 0;

        var animation = new Vector3DAnimation();

        if (fromValue.HasValue)
        {
            animation.From = fromValue.Value;
        }
        if (beginTime.HasValue)
        {
            animation.BeginTime = beginTime.Value;
        }
        if (easingFunction is not null)
        {
            animation.EasingFunction = easingFunction;
        }

        animation.Duration = duration;
        animation.To = toValue;

        if (completeCallback is not null)
        {
            animation.Completed += Animation_Completed;

            void Animation_Completed(object? sender, EventArgs e)
            {
                animation.Completed -= Animation_Completed;
                completeCallback();
            }
        }

        Storyboard.SetTarget(animation, @object);
        Storyboard.SetTargetProperty(animation, new PropertyPath(animationProperty));

        return animation;
    }
}
