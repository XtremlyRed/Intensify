using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using static Intensify.Wpf.Internal.RenderTransfromAnimationExtensions;

namespace Intensify.Wpf;

/// <summary>
/// a class of <see cref="TransitionBase"/>
/// </summary>
public abstract class TransitionBase : AnimationGeneric<double?>, IPropertyAnimation
{
    /// <summary>
    ///  animation property.
    /// </summary>
    protected abstract DependencyProperty Property { get; }

    /// <summary>
    ///  animation path.
    /// </summary>
    protected abstract string AnimationPath { get; }
    DependencyProperty IPropertyAnimation.Property => Property;

    /// <summary>
    /// Creates the animation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="propertyOwner">The property owner.</param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override AnimationTimeline AnimationBuild(FrameworkElement element, out object propertyOwner)
    {
        var animation = new DoubleAnimation()
        {
            Duration = this.Duration,
            BeginTime = this.BeginTime,
            EasingFunction = GetEasingFunction(element),
        };

        if (this.From.HasValue)
        {
            animation.From = this.From.Value;
        }
        if (this.To.HasValue)
        {
            animation.To = this.To.Value;
        }

        propertyOwner = element;

        return animation;
    }
}
