using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using static Intensify.Wpf.PopupService;

namespace Intensify.Wpf;

/// <summary>
/// popup context
/// </summary>
public abstract class PopupContext : IEquatable<PopupContext>, IEquatable<object>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    static int configIndex = int.MinValue;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int contextIndex;

    /// <summary>
    ///
    /// </summary>
    protected PopupContext()
    {
        contextIndex = Interlocked.Increment(ref configIndex);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal Dictionary<string, ButtonResult> buttonResult = new Dictionary<string, ButtonResult>();

    /// <summary>
    /// display buttons
    /// </summary>
    public string[] Buttons => buttonResult.Keys.ToArray();

    /// <summary>
    /// primary button index
    /// </summary>
    public int PrimaryIndex { get; set; } = 0;

    /// <summary>
    /// popup title
    /// </summary>
    public string? Title { get; internal set; }

    /// <summary>
    /// popup content
    /// </summary>
    public string? Content { get; internal set; }

    /// <summary>
    /// <see langword="add"/>  button and  <see cref="ButtonResult"/>
    /// </summary>
    /// <param name="buttonContent"></param>
    /// <param name="result"></param>
    /// <exception cref="ArgumentException"></exception>
    public void AddButton(string buttonContent, ButtonResult result)
    {
        _ = string.IsNullOrEmpty(buttonContent) ? throw new ArgumentException("button content error") : 0;

        buttonResult[buttonContent] = result;
    }

    /// <summary>
    /// button click command
    /// </summary>
    public virtual Command<string> ClickCommand =>
        new Command<string>(
            (btnContent) =>
            {
                var eventArgs = new PubEventArgs(btnContent!, this);
                eventService.Publish(eventArgs);
            }
        );

    internal static PopupContext GetDefault(int buttonCount = 3)
    {
        var defaultConfig = new InnerPopupConfig();
        if (buttonCount > 0)
            defaultConfig.AddButton("Yes", ButtonResult.Yes);
        if (buttonCount > 1)
            defaultConfig.AddButton("No", ButtonResult.No);
        if (buttonCount > 2)
            defaultConfig.AddButton("Cancel", ButtonResult.Cancel);

        return defaultConfig;
    }

    private class InnerPopupConfig : PopupContext { }

    /// <summary>
    /// <see langword="equals"/>
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is PopupContext context && contextIndex == context.contextIndex;
    }

    /// <summary>
    /// get haso code
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return contextIndex;
    }

    /// <summary>
    /// <see langword="equals"/>
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(PopupContext? other)
    {
        return other?.contextIndex == contextIndex;
    }

    /// <summary>
    /// <see langword="equals"/>  operator
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(PopupContext? left, PopupContext? right)
    {
        return left is not null && right is not null && left.contextIndex == right.contextIndex;
    }

    /// <summary>
    /// not <see langword="equals"/>  operator
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(PopupContext? left, PopupContext? right)
    {
        return left is null || right is null || left.contextIndex != right.contextIndex;
    }

    /// <summary>
    /// a <see langword="class"/> of <see cref="Command{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Command<T> : ICommand
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        Action<string> callback;

        /// <summary>
        ///
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        ///
        /// </summary>
        /// <param name="callback"></param>
        public Command(Action<string> callback)
        {
            this.callback = callback;
        }

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute(parameter is string str ? str : parameter?.ToString()!);
        }

        /// <summary>
        /// can execution
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual bool CanExecute(string? parameter)
        {
            return true;
        }

        void ICommand.Execute(object? parameter)
        {
            try
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                Execute(parameter is string str ? str : parameter?.ToString()!);
            }
            finally
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// execution
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void Execute(string? parameter)
        {
            callback?.Invoke(parameter!);
        }
    }
}
