using System.Runtime.CompilerServices;

namespace Intensify.Core;

/// <summary>
/// task  extensions
/// </summary>
/// 2024/1/29 14:01
public static class TaskExtensions
{
    /// <summary>
    /// Gets the awaiter.
    /// </summary>
    /// <param name="tasks">The tasks.</param>
    /// <returns></returns>
    /// 2023/12/12 13:52
    public static TaskAwaiter GetAwaiter(this TimeSpan tasks)
    {
        return Task.Delay(tasks).GetAwaiter();
    }

    /// <summary>
    /// Gets the awaiter.
    /// </summary>
    /// <param name="tasks">The tasks.</param>
    /// <returns></returns>
    /// 2023/11/27 8:02
    public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks)
    {
        _ = tasks ?? throw new ArgumentNullException(nameof(tasks));

        return Task.WhenAll(tasks).GetAwaiter();
    }

    /// <summary>
    /// Gets the awaiter.
    /// </summary>
    /// <param name="tasks">The tasks.</param>
    /// <returns></returns>
    /// 2023/11/27 8:02
    public static TaskAwaiter<T[]> GetAwaiter<T>(this IEnumerable<Task<T>> tasks)
    {
        _ = tasks ?? throw new ArgumentNullException(nameof(tasks));

        return Task.WhenAll(tasks).GetAwaiter();
    }
}
