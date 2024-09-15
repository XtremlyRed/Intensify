using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static Intensify.Wpf.Internal.RenderTransfromAnimationExtensions;

namespace Intensify.Wpf;

/// <summary>
///  a class of <see cref="ScaleX"/>
/// </summary>
/// <seealso cref="TransitionBase" />
public class ScaleX : TransitionBase
{
    /// <summary>
    /// animation property.
    /// </summary>
    protected override DependencyProperty Property => ScaleTransform.ScaleXProperty;

    /// <summary>
    /// animation path.
    /// </summary>
    protected override string AnimationPath =>
        string.Format("(FrameworkElement.RenderTransform).(TransformGroup.Children)[{0}].(ScaleTransform.ScaleX)", ScaleIndex);

    /// <summary>
    /// Creates the animation.
    /// </summary>
    /// <param name="targetObject">The target object.</param>
    /// <param name="owner">The owner.</param>
    /// <returns></returns>
    protected override AnimationTimeline AnimationBuild(FrameworkElement targetObject, out object owner)
    {
        ApplyTransformGroup(targetObject);

        owner = ((TransformGroup)targetObject.RenderTransform).Children[ScaleIndex];

        var animation = new DoubleAnimation()
        {
            Duration = this.Duration,
            BeginTime = this.BeginTime,
            EasingFunction = GetEasingFunction(targetObject),
        };

        if (this.From.HasValue)
        {
            animation.From = From.Value;
        }
        if (this.To.HasValue)
        {
            animation.To = To.Value;
        }

        return animation;
    }
}
