using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static Intensify.Wpf.Internal.RenderTransfromAnimationExtensions;

namespace Intensify.Wpf;

/// <summary>
/// a class of <see cref="SlideX"/>
/// </summary>
/// <seealso cref="TransitionBase" />
public class SlideX : TransitionBase
{
    /// <summary>
    /// animation property.
    /// </summary>
    protected override DependencyProperty Property => TranslateTransform.XProperty;

    /// <summary>
    /// animation path.
    /// </summary>
    protected override string AnimationPath =>
        string.Format("(FrameworkElement.RenderTransform).(TransformGroup.Children)[{0}].(TranslateTransform.X)", TranslateIndex);

    /// <summary>
    /// Creates the animation.
    /// </summary>
    /// <param name="targetObject">The target object.</param>
    /// <param name="owner">The owner.</param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override AnimationTimeline AnimationBuild(FrameworkElement targetObject, out object owner)
    {
        ApplyTransformGroup(targetObject);

        owner = ((TransformGroup)targetObject.RenderTransform).Children[TranslateIndex];

        var animation = new DoubleAnimation()
        {
            Duration = this.Duration,
            BeginTime = this.BeginTime,
            EasingFunction = GetEasingFunction(targetObject),
        };

        animation.From = ((From!.HasValue ? From.Value : targetObject.ActualWidth));

        animation.To = (To!.HasValue ? To.Value : 0);

        return animation;
    }
}
