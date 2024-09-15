using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static Intensify.Wpf.Internal.RenderTransfromAnimationExtensions;

namespace Intensify.Wpf;

/// <summary>
/// a class of <see cref="Rotate"/>
/// </summary>
/// <seealso cref="TransitionBase" />
public class Rotate : TransitionBase
{
    /// <summary>
    ///  animation property.
    /// </summary>
    protected override DependencyProperty Property => RotateTransform.AngleProperty;

    /// <summary>
    ///  animation path.
    /// </summary>
    protected override string AnimationPath =>
        string.Format("(FrameworkElement.RenderTransform).(TransformGroup.Children)[{0}].(RotateTransform.Angle)", RotateIndex);

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

        element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

        propertyOwner = ((TransformGroup)element.RenderTransform).Children[RotateIndex];

        return base.AnimationBuild(element, out _);
    }
}
