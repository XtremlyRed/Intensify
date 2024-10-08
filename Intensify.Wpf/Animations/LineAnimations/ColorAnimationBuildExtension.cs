﻿using System.Linq.Expressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Intensify.Wpf.Internal;

namespace Intensify.Wpf;

/// <summary>
///
/// </summary>
public static class ColorAnimationBuildExtension
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
    public static ColorAnimation BuildAnimation<TObject>(
        this TObject @object,
        Expression<Func<TObject, Color>> propertyExpression,
        Color toValue,
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
    public static ColorAnimation BuildAnimation<TObject>(
        this TObject @object,
        Expression<Func<TObject, Color>> propertyExpression,
        Color fromValue,
        Color toValue,
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
    public static ColorAnimation BuildAnimation<TObject>(
        this TObject @object,
        Expression<Func<TObject, Color>> propertyExpression,
        Color toValue,
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
    public static ColorAnimation BuildAnimation<TObject>(
        this TObject @object,
        Expression<Func<TObject, Color>> propertyExpression,
        Color? fromValue,
        Color toValue,
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
    public static ColorAnimation BuildAnimation(
        this DependencyObject @object,
        string animationProperty,
        Color? fromValue,
        Color toValue,
        TimeSpan? beginTime,
        TimeSpan duration,
        IEasingFunction? easingFunction = null,
        Action? completeCallback = null
    )
    {
        _ = @object ?? throw new ArgumentNullException(nameof(@object));
        _ = string.IsNullOrWhiteSpace(animationProperty) ? throw new ArgumentNullException(nameof(animationProperty)) : 0;

        var animation = new ColorAnimation();

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
