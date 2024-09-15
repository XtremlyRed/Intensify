using System.ComponentModel;

namespace Intensify.Wpf;

/// <summary>
/// a class of <see cref="EnumDescriptionConverter"/>
/// </summary>
public class EnumDescriptionConverter : EnumConverter<DescriptionAttribute>
{
    /// <summary>
    /// display
    /// </summary>
    protected override Func<DescriptionAttribute?, string?> DisplaySelector => i => i?.Description;
}
