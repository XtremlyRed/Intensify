using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using static System.Reflection.BindingFlags;

namespace Intensify.Core;

/// <summary>
/// <see cref="Enum"/> analyzer
/// </summary>
/// <typeparam name="T"></typeparam>
public static class EnumAnalyzer<T>
    where T : struct, Enum
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    static ConcurrentDictionary<Type, object> attributeMaps = new();

    /// <summary>
    ///
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    static EnumAnalyzer()
    {
        if (typeof(T).IsEnum == false)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        var values = Enum.GetValues(typeof(T)).OfType<T>().ToArray();

        Values = new ReadOnlyCollection<T>(values);

        var fieldInfos = typeof(T).GetFields(Public | Static).Where(i => i.IsStatic).ToArray();

        FieldInfos = new ReadOnlyCollection<FieldInfo>(fieldInfos);

        Names = new ReadOnlyCollection<string>(fieldInfos.Select(i => i.Name).ToArray());
    }

    /// <summary>
    /// all values
    /// </summary>
    public static readonly IReadOnlyList<T> Values;

    /// <summary>
    /// all fieldInfos
    /// </summary>
    public static readonly IReadOnlyList<FieldInfo> FieldInfos;

    /// <summary>
    /// all enum name
    /// </summary>
    public static readonly IReadOnlyList<string> Names;

    /// <summary>
    /// get target <see cref="Attribute"/> maps
    /// </summary>
    /// <typeparam name="Attribute"></typeparam>
    /// <returns></returns>
    public static IDictionary<T, Attribute> GetAttribute<Attribute>()
        where Attribute : System.Attribute
    {
        var type = typeof(T);

        if (attributeMaps.TryGetValue(type, out var value) && value is ReadOnlyDictionary<T, Attribute> target)
        {
            return target;
        }

        lock (attributeMaps)
        {
            if (attributeMaps.TryGetValue(type, out var value2) && value2 is ReadOnlyDictionary<T, Attribute> target2)
            {
                return target2;
            }

            var dict = FieldInfos.ToDictionary(i => (T)i.GetValue(null)!, i => i.GetCustomAttribute<Attribute>())!;

            attributeMaps[type] = target2 = new ReadOnlyDictionary<T, Attribute>(dict!);

            return target2;
        }
    }
}
