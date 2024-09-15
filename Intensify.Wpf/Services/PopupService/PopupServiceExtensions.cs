using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Intensify.Wpf;

/// <summary>
/// <see cref="IPopupService"/> extensions
/// </summary>
public static class PopupServiceExtensions
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    static IPopupVisualLocator? visualLocator;

    /// <summary>
    /// popup visual in main popup host
    /// </summary>
    /// <param name="popupService"></param>
    /// <param name="visual"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static async ValueTask<object> PopupAsync(this IPopupService popupService, Visual visual, PopupParameter? parameter = null)
    {
        var popupResult = await popupService.PopupAsync<object>(visual, parameter);
        return popupResult;
    }

    /// <summary>
    ///  popup visual in <paramref name="hostedName"/> popup host
    /// </summary>
    /// <param name="popupService"></param>
    /// <param name="hostedName"></param>
    /// <param name="visual"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static async ValueTask<object> PopupAsync(
        this IPopupService popupService,
        string hostedName,
        Visual visual,
        PopupParameter? parameter = null
    )
    {
        var popupResult = await popupService.PopupAsync<object>(hostedName, visual, parameter);
        return popupResult;
    }

    /// <summary>
    /// <para>popup visual in main popup host </para>
    /// <para>Use <see cref="IPopupVisualLocator"/> to locate the view</para>
    /// <para>Before using this method, please first set the <see cref="IPopupVisualLocator"/> using method <see cref="SetPopupVisualLocator(IPopupVisualLocator)"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="popupService"></param>
    /// <param name="visualToken"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static async ValueTask<T> PopupAsync<T>(this IPopupService popupService, string visualToken, PopupParameter? parameter = null)
    {
        _ = visualLocator ?? throw new InvalidOperationException("invalid visual locator");

        var visual = visualLocator.Locate(visualToken);

        return await popupService.PopupAsync<T>(visual, parameter);
    }

    /// <summary>
    /// <para>popup visual in <paramref name="hostedName"/> popup host </para>
    /// <para>Use <see cref="IPopupVisualLocator"/> to locate the view</para>
    /// <para>Before using this method, please first set the <see cref="IPopupVisualLocator"/> using method <see cref="SetPopupVisualLocator(IPopupVisualLocator)"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="popupService"></param>
    /// <param name="hostedName"></param>
    /// <param name="visualToken"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static async ValueTask<T> PopupAsync<T>(
        this IPopupService popupService,
        string hostedName,
        string visualToken,
        PopupParameter? parameter = null
    )
    {
        _ = visualLocator ?? throw new InvalidOperationException("invalid visual locator");

        var visual = visualLocator.Locate(visualToken);

        return await popupService.PopupAsync<T>(visual, parameter);
    }

    /// <summary>
    /// <para>popup visual in main popup host </para>
    /// <para>Use <see cref="IPopupVisualLocator"/> to locate the view</para>
    /// <para>Before using this method, please first set the <see cref="IPopupVisualLocator"/> using method <see cref="SetPopupVisualLocator(IPopupVisualLocator)"/></para>
    /// </summary>
    /// <param name="popupService"></param>
    /// <param name="visualToken"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static async ValueTask<object> PopupAsync(this IPopupService popupService, string visualToken, PopupParameter? parameter = null)
    {
        _ = visualLocator ?? throw new InvalidOperationException("invalid visual locator");

        var visual = visualLocator.Locate(visualToken);

        return await popupService.PopupAsync<object>(visual, parameter);
    }

    /// <summary>
    /// <para>popup visual in <paramref name="hostedName"/> popup host </para>
    /// <para>Use <see cref="IPopupVisualLocator"/> to locate the view</para>
    /// <para>Before using this method, please first set the <see cref="IPopupVisualLocator"/> using method <see cref="SetPopupVisualLocator(IPopupVisualLocator)"/></para>
    /// </summary>
    /// <param name="popupService"></param>
    /// <param name="hostedName"></param>
    /// <param name="visualToken"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static async ValueTask<object> PopupAsync(
        this IPopupService popupService,
        string hostedName,
        string visualToken,
        PopupParameter? parameter = null
    )
    {
        _ = visualLocator ?? throw new InvalidOperationException("invalid visual locator");

        var visual = visualLocator.Locate(visualToken);

        return await popupService.PopupAsync<object>(visual, parameter);
    }

    /// <summary>
    /// set popup visual locator when used
    /// <para><see cref="PopupAsync{T}(IPopupService, string, PopupParameter?)"/></para>
    /// <para>and</para>
    /// <para><see cref="PopupAsync{T}(IPopupService, string, string, PopupParameter?)"/></para>
    /// </summary>
    /// <param name="popupVisualLocator"></param>
    public static void SetPopupVisualLocator(IPopupVisualLocator popupVisualLocator)
    {
        visualLocator = popupVisualLocator ?? throw new ArgumentNullException(nameof(popupVisualLocator));
    }
}

/// <summary>
/// an <see langword="interface"/> of <see cref="IPopupVisualLocator"/>
/// </summary>
public interface IPopupVisualLocator
{
    /// <summary>
    /// locate visual
    /// </summary>
    /// <param name="visualToken"></param>
    /// <returns></returns>
    Visual Locate(string visualToken);
}
