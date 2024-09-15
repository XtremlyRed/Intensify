using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Intensify.Core;

/// <summary>
/// a <see langword="class"/> of <see cref="TypeConverterExtensions"/>
/// </summary>
public static class TypeConverterExtensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type, TypeConverter>> typeConvertMaps = new();

    /// <summary>
    /// Converts to.
    /// </summary>
    /// <typeparam name="To">The type of the o.</typeparam>
    /// <param name="from">From.</param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException">
    /// null values cannot be converted
    /// or
    /// type conversion unsuccessful
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// type converter not registered
    /// or
    /// type converter from {fromType} to {toType} not registered
    /// </exception>
    public static To ConvertTo<To>(this object? from)
    {
        if (from is To to)
        {
            return to;
        }

        if (from is IConvertible convertible)
        {
            var targetType = typeof(To);

            if (
                EnumAnalyzer<TypeCode>.Names.Contains(targetType.Name)
                && convertible.ToType(typeof(To), CultureInfo.CurrentCulture) is To convertValue
            )
            {
                return convertValue;
            }
        }

        Type fromType = from?.GetType() ?? throw new InvalidCastException("null values cannot be converted");

        if (typeConvertMaps.TryGetValue(fromType, out ConcurrentDictionary<Type, TypeConverter>? targetTypeConverterMaps) == false)
        {
            typeConvertMaps[fromType] = targetTypeConverterMaps = new ConcurrentDictionary<Type, TypeConverter>();
        }

        Type toType = typeof(To);

        if (targetTypeConverterMaps.TryGetValue(toType, out TypeConverter? typeConverter) == false)
        {
            typeConverter = TypeDescriptor.GetConverter(toType);

            if (typeConverter is null)
            {
                throw new InvalidOperationException("type converter not registered");
            }

            targetTypeConverterMaps[toType] = typeConverter;
        }

        if (typeConverter.CanConvertFrom(fromType) == false)
        {
            throw new InvalidOperationException($"type converter from {fromType} to {toType} not registered");
        }

        object? destination = typeConverter.ConvertFrom(from);

        if (destination is To toValue)
        {
            return toValue;
        }

        throw new InvalidCastException("type conversion unsuccessful");
    }
}
