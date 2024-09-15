using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace Intensify.Core;

/// <summary>
/// <see langword="class"/> analyzer
/// </summary>
/// <typeparam name="T"></typeparam>
public static class ClassAnalyzer<T>
    where T : class
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    static ConcurrentDictionary<Type, object> fieldsAttributeMaps = new();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    static ConcurrentDictionary<Type, object> propertiesAttributeMaps = new();

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    static ClassAnalyzer()
    {
        var type = typeof(T);

        var pubProperties = type.GetProperties(Public | Instance);
        var pubStaticProperties = type.GetProperties(Public | Static);

        var pubFields = type.GetFields(Public | Instance);
        var pubStaticFields = type.GetFields(Public | Static);

        FieldInfos = new ReadOnlyCollection<FieldInfo>(pubFields);
        StaticFieldInfos = new ReadOnlyCollection<FieldInfo>(pubStaticFields);

        Properties = new ReadOnlyCollection<PropertyInfo>(pubProperties);
        StaticProperties = new ReadOnlyCollection<PropertyInfo>(pubStaticProperties);
    }

    /// <summary>
    /// all fieldInfos
    /// </summary>
    public static readonly IReadOnlyList<FieldInfo> FieldInfos;

    /// <summary>
    /// all static fieldInfos
    /// </summary>
    public static readonly IReadOnlyList<FieldInfo> StaticFieldInfos;

    /// <summary>
    /// all properties
    /// </summary>
    public static readonly IReadOnlyList<PropertyInfo> Properties;

    /// <summary>
    /// all static property infos
    /// </summary>
    public static readonly IReadOnlyList<PropertyInfo> StaticProperties;

    /// <summary>
    /// get <see langword="field"/> <typeparamref name="Attribute"/> <see cref="IDictionary{T, Attr}"/>
    /// </summary>
    /// <typeparam name="Attribute"></typeparam>
    /// <returns></returns>
    public static IDictionary<T, Attribute> GetFieldAttributes<Attribute>()
        where Attribute : System.Attribute
    {
        var type = typeof(T);

        if (fieldsAttributeMaps.TryGetValue(type, out var value) && value is ReadOnlyDictionary<T, Attribute> target)
        {
            return target;
        }

        lock (fieldsAttributeMaps)
        {
            if (fieldsAttributeMaps.TryGetValue(type, out var value2) && value2 is ReadOnlyDictionary<T, Attribute> target2)
            {
                return target2;
            }

            var dict = FieldInfos.ToDictionary(i => (T)i.GetValue(null)!, i => i.GetCustomAttribute<Attribute>())!;

            fieldsAttributeMaps[type] = target2 = new ReadOnlyDictionary<T, Attribute>(dict!);

            return target2;
        }
    }


    /// <summary>
    /// get <see langword="property"/> <typeparamref name="Attribute"/> <see cref="IDictionary{T, Attr}"/>
    /// </summary>
    /// <typeparam name="Attribute"></typeparam>
    /// <returns></returns>
    public static IDictionary<T, Attribute> GetPropertyAttributes<Attribute>()
        where Attribute : System.Attribute
    {
        var type = typeof(T);

        if (propertiesAttributeMaps.TryGetValue(type, out var value) && value is ReadOnlyDictionary<T, Attribute> target)
        {
            return target;
        }

        lock (propertiesAttributeMaps)
        {
            if (propertiesAttributeMaps.TryGetValue(type, out var value2) && value2 is ReadOnlyDictionary<T, Attribute> target2)
            {
                return target2;
            }

            var dict = Properties.ToDictionary(i => (T)i.GetValue(null)!, i => i.GetCustomAttribute<Attribute>())!;

            propertiesAttributeMaps[type] = target2 = new ReadOnlyDictionary<T, Attribute>(dict!);

            return target2;
        }
    }
}
