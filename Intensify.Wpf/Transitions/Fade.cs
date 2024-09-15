using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;
using static Intensify.Wpf.Internal.RenderTransfromAnimationExtensions;

namespace Intensify.Wpf;

/// <summary>
/// a class of <see cref="Fade"/>
/// </summary>
/// <seealso cref="TransitionBase" />
public class Fade : TransitionBase
{
    /// <summary>
    ///  animation property.
    /// </summary>
    protected override DependencyProperty Property => FrameworkElement.OpacityProperty;

    /// <summary>
    ///  animation path.
    /// </summary>
    protected override string AnimationPath => "Opacity";

    /// <summary>
    /// Creates the animation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="propertyOwner">The property owner.</param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override AnimationTimeline AnimationBuild(FrameworkElement element, out object propertyOwner)
    {
        ApplyTransformGroup(element);

        propertyOwner = element;

        return base.AnimationBuild(element, out _);
    }

    /// <summary>
    /// to value
    /// </summary>
    public override double? To
    {
        get
        {
            return base.To switch
            {
                > 1 => 1,
                < 0 => 0,
                _ => base.To,
            };
        }
        set
        {
            base.To = value switch
            {
                > 1 => 1,
                < 0 => 0,
                _ => value,
            };
        }
    }

    /// <summary>
    /// from value
    /// </summary>
    public override double? From
    {
        get
        {
            return base.From switch
            {
                > 1 => 1,
                < 0 => 0,
                _ => base.From,
            };
        }
        set
        {
            base.From = value switch
            {
                > 1 => 1,
                < 0 => 0,
                _ => value,
            };
        }
    }
}
