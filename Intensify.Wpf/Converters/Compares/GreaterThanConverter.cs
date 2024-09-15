using System.Windows;

namespace Intensify.Wpf;

/// <summary>
/// a class of <see cref="GreaterThanConverter"/>
/// </summary>
/// <seealso cref="CompareConverter" />
public class GreaterThanConverter : CompareConverter
{
    /// <summary>
    /// create a new instance of <see cref="GreaterThanConverter"/>
    /// </summary>
    public GreaterThanConverter()
        : base(CompareMode.GreaterThan) { }
}
