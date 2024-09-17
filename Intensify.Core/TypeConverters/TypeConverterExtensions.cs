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
    /// convert <see cref="object"/> to <typeparamref name="To"/>
    /// </summary>
    /// <typeparam name="To"></typeparam>
    /// <param name="from"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
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

        Type fromType = from?.GetType() ?? throw new InvalidCastException("null value cannot be converted");

        if (typeConvertMaps.TryGetValue(fromType, out ConcurrentDictionary<Type, TypeConverter>? fromTypeConverterStorages) == false)
        {
            typeConvertMaps[fromType] = fromTypeConverterStorages = new ConcurrentDictionary<Type, TypeConverter>();
        }

        Type toType = typeof(To);

        if (fromTypeConverterStorages.TryGetValue(toType, out TypeConverter? toTypeConverter) == false)
        {
            toTypeConverter = TypeDescriptor.GetConverter(toType);

            if (toTypeConverter is null)
            {
                throw new InvalidOperationException("type converter not registered");
            }

            fromTypeConverterStorages[toType] = toTypeConverter;
        }

        if (toTypeConverter.CanConvertFrom(fromType) == false)
        {
            throw new InvalidOperationException($"type converter from {fromType} to {toType} not registered");
        }

        object? destination = toTypeConverter.ConvertFrom(from);

        if (destination is To toValue)
        {
            return toValue;
        }

        throw new InvalidCastException("type conversion unsuccessful");
    }

    /// <summary>
    /// appen custom converter
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    /// <param name="converter"></param>
    public static void AppendConverter<TFrom, TTo>(Func<TFrom, TTo> converter)
    {
        var fromType = typeof(TFrom);
        var toType = typeof(TTo);

        if (typeConvertMaps.TryGetValue(fromType, out ConcurrentDictionary<Type, TypeConverter>? targetTypeConverterMaps) == false)
        {
            typeConvertMaps[fromType] = targetTypeConverterMaps = new ConcurrentDictionary<Type, TypeConverter>();
        }

        targetTypeConverterMaps[toType] = new CustomTypeConverter<TFrom, TTo>(converter);
    }

    private class CustomTypeConverter<TFrom, TTo> : TypeConverter
    {
        Func<TFrom, TTo> converter;
        static Type FromType = typeof(TFrom);
        static Type ToType = typeof(TTo);

        public CustomTypeConverter(Func<TFrom, TTo> converter)
        {
            this.converter = converter;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            if (sourceType == FromType)
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is TFrom from)
            {
                return converter(from);
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
