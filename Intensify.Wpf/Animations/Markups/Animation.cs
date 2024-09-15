using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;
using static System.Reflection.BindingFlags;

namespace Intensify.Wpf;

/// <summary>
/// a class of <see cref="AnimationBase"/>
/// </summary>
/// <seealso cref="System.Windows.Freezable" />
public abstract partial class AnimationBase : Freezable
{
    /// <summary>
    /// Creates the instance core.
    /// </summary>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override Freezable? CreateInstanceCore()
    {
        return Activator.CreateInstance(this.GetType()) as Freezable;
    }

    /// <summary>
    ///  event mode.
    /// </summary>
    public virtual EventMode EventMode
    {
        get { return (EventMode)GetValue(EventModeProperty); }
        set { SetValue(EventModeProperty, value); }
    }

    /// <summary>
    ///  event mode.
    /// </summary>
    public static readonly DependencyProperty EventModeProperty = DependencyProperty.Register(
        "EventMode",
        typeof(EventMode),
        typeof(AnimationBase),
        new PropertyMetadata(EventMode.Loaded)
    );

    /// <summary>
    /// the play.
    /// </summary>
    public bool? Play
    {
        get { return (bool?)GetValue(PlayProperty); }
        set { SetValue(PlayProperty, value); }
    }

    /// <summary>
    ///   play property
    /// </summary>
    public static readonly DependencyProperty PlayProperty = DependencyProperty.Register(
        "Play",
        typeof(bool?),
        typeof(AnimationBase),
        new FrameworkPropertyMetadata(
            null,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            (s, e) =>
            {
                if (e.NewValue is not true || s is null)
                {
                    return;
                }

                if (s is AnimationBase animation)
                {
                    animation.AnimationInvoke();
                }
                else if (s is AnimationCollection animations)
                {
                    animations.AnimationInvoke();
                }
            },
            null,
            true,
            UpdateSourceTrigger.PropertyChanged
        )
    );

    /// <summary>
    /// Animations the invoke.
    /// </summary>
    internal void AnimationInvoke()
    {
        this.SetCurrentValue(PlayProperty, false);

        /* 项目“Intensify.Wpf (netcoreapp3.1)”的未合并的更改
        在此之前:
                Intensify.Wpf.Intarnal.Extensions.GetAnimationInfo(this).Invoke();
            }
        在此之后:
                Extensions.GetAnimationInfo(this).Invoke();
            }
        */
        Internal.Extensions.GetAnimationInfo(this).Invoke();
    }
}

/// <summary>
/// a class of <see cref="AnimationGeneric{T}"/>
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="System.Windows.Freezable" />
public abstract class AnimationGeneric<T> : AnimationBase
{
    /// <summary>
    /// to value
    /// </summary>
    public virtual T? To
    {
        get { return (T?)GetValue(ToProperty); }
        set { SetValue(ToProperty, value); }
    }

    /// <summary>
    /// to value
    /// </summary>
    public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
        "To",
        typeof(T?),
        typeof(AnimationGeneric<T>),
        new PropertyMetadata(null)
    );

    /// <summary>
    ///  from value.
    /// </summary>
    public virtual T? From
    {
        get { return (T?)GetValue(FromProperty); }
        set { SetValue(FromProperty, value); }
    }

    /// <summary>
    ///  from value.
    /// </summary>
    public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
        "From",
        typeof(T?),
        typeof(AnimationGeneric<T>),
        new PropertyMetadata(null)
    );
}
