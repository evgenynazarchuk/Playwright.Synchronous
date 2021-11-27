/*
 * MIT License
 *
 * Copyright (c) Evgeny Nazarchuk.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace Playwright.Synchronous;

public static class PageSynchronous
{
    public static void AddInitScript(this IPage page, string? script = null, string? scriptPath = null)
    {
        page.AddInitScriptAsync(script, scriptPath).GetAwaiter().GetResult();
    }

    public static void AddScriptTag(this IPage page, PageAddScriptTagOptions? options = null)
    {
        page.AddScriptTagAsync(options).GetAwaiter().GetResult();
    }

    public static void AddStyleTag(this IPage page, PageAddStyleTagOptions? options = null)
    {
        page.AddStyleTagAsync(options).GetAwaiter().GetResult();
    }

    public static void BringToFront(this IPage page)
    {
        page.BringToFrontAsync().GetAwaiter().GetResult();
    }

    public static void Check(this IPage page, string selector, PageCheckOptions? options = null)
    {
        page.CheckAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void Click(this IPage page, string selector, PageClickOptions? options = null)
    {
        page.ClickAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void ClosePage(this IPage page, PageCloseOptions? options = null)
    {
        page.CloseAsync(options).GetAwaiter().GetResult();
    }

    public static string Content(this IPage page)
    {
        return page.ContentAsync().GetAwaiter().GetResult();
    }

    public static void DblClick(this IPage page, string selector, PageDblClickOptions? options = null)
    {
        page.DblClickAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void DispatchEvent(this IPage page, string selector, string type, object? eventInit = null, PageDispatchEventOptions? options = null)
    {
        page.DispatchEventAsync(selector, type, eventInit, options).GetAwaiter().GetResult();
    }

    public static void DragAndDrop(this IPage page, string source, string target, PageDragAndDropOptions? options = null)
    {
        page.DragAndDropAsync(source, target, options).GetAwaiter().GetResult();
    }

    public static void EmulateMedia(this IPage page, PageEmulateMediaOptions? options = null)
    {
        page.EmulateMediaAsync(options).GetAwaiter().GetResult();
    }

    public static void EvalOnSelector<T>(this IPage page, string selector, string expression, object? arg = null, PageEvalOnSelectorOptions? options = null)
    {
        page.EvalOnSelectorAsync<T>(selector, expression, arg, options).GetAwaiter().GetResult();
    }

    public static void EvalOnSelectorAll<T>(this IPage page, string selector, string expression, object? arg = null)
    {
        page.EvalOnSelectorAllAsync<T>(selector, expression, arg).GetAwaiter().GetResult();
    }

    public static void Evaluate<T>(this IPage page, string expression, object? arg = null)
    {
        page.EvaluateAsync<T>(expression, arg);
    }

    public static void EvaluateHandle(this IPage page, string expression, object? arg = null)
    {
        page.EvaluateHandleAsync(expression, arg).GetAwaiter().GetResult();
    }

    public static void ExposeBinding(this IPage page, string name, Action callback, PageExposeBindingOptions? options = null)
    {
        page.ExposeBindingAsync(name, callback, options).GetAwaiter().GetResult();
    }

    public static void ExposeFunction(this IPage page, string name, Action callback)
    {
        page.ExposeFunctionAsync(name, callback).GetAwaiter().GetResult();
    }

    public static void Fill(this IPage page, string selector, string value, PageFillOptions? options = null)
    {
        page.FillAsync(selector, value, options);
    }

    public static void Focus(this IPage page, string selector, PageFocusOptions? options = null)
    {
        page.FocusAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string? GetAttribute(this IPage page, string selector, string name, PageGetAttributeOptions? options = null)
    {
        return page.GetAttributeAsync(selector, name, options).GetAwaiter().GetResult();
    }

    public static void GoBack(this IPage page, PageGoBackOptions? options = null)
    {
        page.GoBackAsync(options).GetAwaiter().GetResult();
    }

    public static void GoForward(this IPage page, PageGoForwardOptions? options = null)
    {
        page.GoForwardAsync(options).GetAwaiter().GetResult();
    }

    public static void Goto(this IPage page, string url, PageGotoOptions? options = null)
    {
        page.GotoAsync(url, options).GetAwaiter().GetResult();
    }

    public static void Hover(this IPage page, string selector, PageHoverOptions? options = null)
    {
        page.HoverAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string InnerHTML(this IPage page, string selector, PageInnerHTMLOptions? options = null)
    {
        return page.InnerHTMLAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string InnerText(this IPage page, string selector, PageInnerTextOptions? options = null)
    {
        return page.InnerTextAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string InputValue(this IPage page, string selector, PageInputValueOptions? options = null)
    {
        return page.InputValueAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsChecked(this IPage page, string selector, PageIsCheckedOptions? options = null)
    {
        return page.IsCheckedAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsDisabled(this IPage page, string selector, PageIsDisabledOptions? options = null)
    {
        return page.IsDisabledAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsEditable(this IPage page, string selector, PageIsEditableOptions? options = null)
    {
        return page.IsEditableAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsEnabled(this IPage page, string selector, PageIsEnabledOptions? options = null)
    {
        return page.IsEnabledAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsHidden(this IPage page, string selector, PageIsHiddenOptions? options = null)
    {
        return page.IsHiddenAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsVisible(this IPage page, string selector, PageIsVisibleOptions? options = null)
    {
        return page.IsVisibleAsync(selector, options).GetAwaiter().GetResult();
    }

    public static IPage? Opener(this IPage page)
    {
        return page.OpenerAsync().GetAwaiter().GetResult();
    }

    public static void Pause(this IPage page)
    {
        page.PauseAsync().GetAwaiter().GetResult();
    }

    public static byte[] Pdf(this IPage page, PagePdfOptions? options = null)
    {
        return page.PdfAsync(options).GetAwaiter().GetResult();
    }

    public static void Press(this IPage page, string selector, string key, PagePressOptions? options = null)
    {
        page.PressAsync(selector, key, options).GetAwaiter().GetResult();
    }

    public static IElementHandle? QuerySelector(this IPage page, string selector, PageQuerySelectorOptions? options = null)
    {
        return page.QuerySelectorAsync(selector, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<IElementHandle> QuerySelectorAll(this IPage page, string selector)
    {
        return page.QuerySelectorAllAsync(selector).GetAwaiter().GetResult();
    }

    public static IResponse? ReloadAsync(this IPage page, PageReloadOptions? options = null)
    {
        return page.ReloadAsync(options).GetAwaiter().GetResult();
    }

    public static void Route(this IPage page, string url, Action<IRoute> handler, PageRouteOptions? options = null)
    {
        page.RouteAsync(url, handler, options).GetAwaiter().GetResult();
    }

    public static void Route(this IPage page, Regex url, Action<IRoute> handler, PageRouteOptions? options = null)
    {
        page.RouteAsync(url, handler, options).GetAwaiter().GetResult();
    }

    public static void Route(this IPage page, Func<string, bool> url, Action<IRoute> handler, PageRouteOptions? options = null)
    {
        page.RouteAsync(url, handler, options).GetAwaiter().GetResult();
    }

    public static byte[] ScreenshotAsync(this IPage page, PageScreenshotOptions? options = null)
    {
        return page.ScreenshotAsync(options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, string values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, IElementHandle values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, IEnumerable<string> values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, SelectOptionValue values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, IEnumerable<IElementHandle> values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, IEnumerable<SelectOptionValue> values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static void SetChecked(this IPage page, string selector, bool checkedState, PageSetCheckedOptions? options = null)
    {
        page.SetCheckedAsync(selector, checkedState, options).GetAwaiter().GetResult();
    }

    public static void SetContent(this IPage page, string html, PageSetContentOptions? options = null)
    {
        page.SetContentAsync(html, options);
    }

    public static void SetExtraHTTPHeaders(this IPage page, IEnumerable<KeyValuePair<string, string>> headers)
    {
        page.SetExtraHTTPHeadersAsync(headers).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IPage page, string selector, string files, PageSetInputFilesOptions? options = null)
    {
        page.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IPage page, string selector, FilePayload files, PageSetInputFilesOptions? options = null)
    {
        page.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IPage page, string selector, IEnumerable<string> files, PageSetInputFilesOptions? options = null)
    {
        page.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IPage page, string selector, IEnumerable<FilePayload> files, PageSetInputFilesOptions? options = null)
    {
        page.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
    }

    public static void SetViewportSize(this IPage page, int width, int height)
    {
        page.SetViewportSizeAsync(width, height).GetAwaiter().GetResult();
    }

    public static void Tap(this IPage page, string selector, PageTapOptions? options = null)
    {
        page.TapAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string? TextContent(this IPage page, string selector, PageTextContentOptions? options = null)
    {
        return page.TextContentAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string Title(this IPage page)
    {
        return page.TitleAsync().GetAwaiter().GetResult();
    }

    public static void Type(this IPage page, string selector, string value, PageTypeOptions? options = null)
    {
        page.TypeAsync(selector, value, options).GetAwaiter().GetResult();
    }

    public static void Uncheck(this IPage page, string selector, PageUncheckOptions? options = null)
    {
        page.UncheckAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void Unroute(this IPage page, string url, Action<IRoute>? handler = null)
    {
        page.UnrouteAsync(url, handler).GetAwaiter().GetResult();
    }

    public static void Unroute(this IPage page, Regex url, Action<IRoute>? handler = null)
    {
        page.UnrouteAsync(url, handler).GetAwaiter().GetResult();
    }

    public static void Unroute(this IPage page, Func<string, bool> url, Action<IRoute>? handler = null)
    {
        page.UnrouteAsync(url, handler).GetAwaiter().GetResult();
    }

    public static IConsoleMessage WaitForConsoleMessage(this IPage page, PageWaitForConsoleMessageOptions? options = null)
    {
        return page.WaitForConsoleMessageAsync(options).GetAwaiter().GetResult();
    }

    public static IConsoleMessage RunAndWaitForConsoleMessage(this IPage page, Func<Task> action, PageRunAndWaitForConsoleMessageOptions? options = null)
    {
        return page.RunAndWaitForConsoleMessageAsync(action, options).GetAwaiter().GetResult();
    }

    public static IDownload WaitForDownload(this IPage page, PageWaitForDownloadOptions? options = null)
    {
        return page.WaitForDownloadAsync(options).GetAwaiter().GetResult();
    }

    public static IDownload RunAndWaitForDownload(this IPage page, Func<Task> action, PageRunAndWaitForDownloadOptions? options = null)
    {
        return page.RunAndWaitForDownloadAsync(action, options).GetAwaiter().GetResult();
    }

    public static IFileChooser WaitForFileChooser(this IPage page, PageWaitForFileChooserOptions? options = null)
    {
        return page.WaitForFileChooserAsync(options).GetAwaiter().GetResult();
    }

    public static IFileChooser RunAndWaitForFileChooser(this IPage page, Func<Task> action, PageRunAndWaitForFileChooserOptions? options = null)
    {
        return page.RunAndWaitForFileChooserAsync(action, options).GetAwaiter().GetResult();
    }

    public static IJSHandle WaitForFunction(this IPage page, string expression, object? arg = null, PageWaitForFunctionOptions? options = null)
    {
        return page.WaitForFunctionAsync(expression, arg, options).GetAwaiter().GetResult();
    }

    public static void WaitForLoadState(this IPage page, LoadState? state = null, PageWaitForLoadStateOptions? options = null)
    {
        page.WaitForLoadStateAsync(state, options).GetAwaiter().GetResult();
    }

    public static IResponse? WaitForNavigation(this IPage page, PageWaitForNavigationOptions? options = null)
    {
        return page.WaitForNavigationAsync(options).GetAwaiter().GetResult();
    }

    public static IResponse? RunAndWaitForNavigation(this IPage page, Func<Task> action, PageRunAndWaitForNavigationOptions? options = null)
    {
        return page.RunAndWaitForNavigationAsync(action, options).GetAwaiter().GetResult();
    }

    public static IPage WaitForPopup(this IPage page, PageWaitForPopupOptions? options = null)
    {
        return page.WaitForPopupAsync(options).GetAwaiter().GetResult();
    }

    public static IPage RunAndWaitForPopup(this IPage page, Func<Task> action, PageRunAndWaitForPopupOptions? options = null)
    {
        return page.RunAndWaitForPopupAsync(action, options).GetAwaiter().GetResult();
    }

    public static IRequest WaitForRequest(this IPage page, string urlOrPredicate, PageWaitForRequestOptions? options = null)
    {
        return page.WaitForRequestAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IRequest WaitForRequest(this IPage page, Regex urlOrPredicate, PageWaitForRequestOptions? options = null)
    {
        return page.WaitForRequestAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IRequest WaitForRequest(this IPage page, Func<IRequest, bool> urlOrPredicate, PageWaitForRequestOptions? options = null)
    {
        return page.WaitForRequestAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IRequest RunAndWaitForRequest(this IPage page, Func<Task> action, string urlOrPredicate, PageRunAndWaitForRequestOptions? options = null)
    {
        return page.RunAndWaitForRequestAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IRequest RunAndWaitForRequest(this IPage page, Func<Task> action, Regex urlOrPredicate, PageRunAndWaitForRequestOptions? options = null)
    {
        return page.RunAndWaitForRequestAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IRequest RunAndWaitForRequest(this IPage page, Func<Task> action, Func<IRequest, bool> urlOrPredicate, PageRunAndWaitForRequestOptions? options = null)
    {
        return page.RunAndWaitForRequestAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IRequest WaitForRequestFinished(this IPage page, PageWaitForRequestFinishedOptions? options = null)
    {
        return page.WaitForRequestFinishedAsync(options).GetAwaiter().GetResult();
    }

    public static IRequest RunAndWaitForRequestFinished(this IPage page, Func<Task> action, PageRunAndWaitForRequestFinishedOptions? options = null)
    {
        return page.RunAndWaitForRequestFinishedAsync(action, options).GetAwaiter().GetResult();
    }

    public static IResponse WaitForResponse(this IPage page, string urlOrPredicate, PageWaitForResponseOptions? options = null)
    {
        return page.WaitForResponseAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IResponse WaitForResponse(this IPage page, Regex urlOrPredicate, PageWaitForResponseOptions? options = null)
    {
        return page.WaitForResponseAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IResponse WaitForResponse(this IPage page, Func<IResponse, bool> urlOrPredicate, PageWaitForResponseOptions? options = null)
    {
        return page.WaitForResponseAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IResponse RunAndWaitForResponse(this IPage page, Func<Task> action, string urlOrPredicate, PageRunAndWaitForResponseOptions? options = null)
    {
        return page.RunAndWaitForResponseAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IResponse RunAndWaitForResponse(this IPage page, Func<Task> action, Regex urlOrPredicate, PageRunAndWaitForResponseOptions? options = null)
    {
        return page.RunAndWaitForResponseAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IResponse RunAndWaitForResponse(this IPage page, Func<Task> action, Func<IResponse, bool> urlOrPredicate, PageRunAndWaitForResponseOptions? options = null)
    {
        return page.RunAndWaitForResponseAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    public static IElementHandle? WaitForSelector(this IPage page, string selector, PageWaitForSelectorOptions? options = null)
    {
        return page.WaitForSelectorAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void WaitForTimeout(this IPage page, float timeout)
    {
        page.WaitForTimeoutAsync(timeout).GetAwaiter().GetResult();
    }

    public static void WaitForURL(this IPage page, Regex url, PageWaitForURLOptions? options = null)
    {
        page.WaitForURLAsync(url, options).GetAwaiter().GetResult();
    }

    public static void WaitForURL(this IPage page, Func<string, bool> url, PageWaitForURLOptions? options = null)
    {
        page.WaitForURLAsync(url, options).GetAwaiter().GetResult();
    }

    public static IWebSocket WaitForWebSocket(this IPage page, PageWaitForWebSocketOptions? options = null)
    {
        return page.WaitForWebSocketAsync(options).GetAwaiter().GetResult();
    }

    public static IWebSocket RunAndWaitForWebSocket(this IPage page, Func<Task> action, PageRunAndWaitForWebSocketOptions? options = null)
    {
        return page.RunAndWaitForWebSocketAsync(action, options).GetAwaiter().GetResult();
    }

    public static IWorker WaitForWorker(this IPage page, PageWaitForWorkerOptions? options = null)
    {
        return page.WaitForWorkerAsync(options).GetAwaiter().GetResult();
    }

    public static void RunAndWaitForWorker(this IPage page, Func<Task> action, PageRunAndWaitForWorkerOptions? options = null)
    {
        page.RunAndWaitForWorkerAsync(action, options).GetAwaiter().GetResult();
    }

    public static void Evaluate(this IPage page, string expression, object? arg = null)
    {
        page.EvaluateAsync(expression, arg).GetAwaiter().GetResult();
    }

    public static void EvalOnSelector(this IPage page, string selector, string expression, object? arg = null)
    {
        page.EvalOnSelectorAsync(selector, expression, arg).GetAwaiter().GetResult();
    }

    public static void EvalOnSelectorAll(this IPage page, string selector, string expression, object? arg = null)
    {
        page.EvalOnSelectorAllAsync(selector, expression, arg).GetAwaiter().GetResult();
    }

    public static void ExposeBinding(this IPage page, string name, Action<BindingSource> callback)
    {
        page.ExposeBindingAsync(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T>(this IPage page, string name, Action<BindingSource, T> callback)
    {
        page.ExposeBindingAsync<T>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<TResult>(this IPage page, string name, Func<BindingSource, TResult> callback)
    {
        page.ExposeBindingAsync<TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<TResult>(this IPage page, string name, Func<BindingSource, IJSHandle, TResult> callback)
    {
        page.ExposeBindingAsync<TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T, TResult>(this IPage page, string name, Func<BindingSource, T, TResult> callback)
    {
        page.ExposeBindingAsync<T, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T1, T2, TResult>(this IPage page, string name, Func<BindingSource, T1, T2, TResult> callback)
    {
        page.ExposeBindingAsync<T1, T2, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T1, T2, T3, TResult>(this IPage page, string name, Func<BindingSource, T1, T2, T3, TResult> callback)
    {
        page.ExposeBindingAsync<T1, T2, T3, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T1, T2, T3, T4, TResult>(this IPage page, string name, Func<BindingSource, T1, T2, T3, T4, TResult> callback)
    {
        page.ExposeBindingAsync<T1, T2, T3, T4, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeFunction<T>(this IPage page, string name, Action<T> callback)
    {
        page.ExposeFunctionAsync<T>(name, callback);
    }

    public static void ExposeFunction<TResult>(this IPage page, string name, Func<TResult> callback)
    {
        page.ExposeFunctionAsync<TResult>(name, callback);
    }

    public static void ExposeFunction<T, TResult>(this IPage page, string name, Func<T, TResult> callback)
    {
        page.ExposeFunctionAsync<T, TResult>(name, callback);
    }

    public static void ExposeFunction<T1, T2, TResult>(this IPage page, string name, Func<T1, T2, TResult> callback)
    {
        page.ExposeFunctionAsync<T1, T2, TResult>(name, callback);
    }

    public static void ExposeFunction<T1, T2, T3, TResult>(this IPage page, string name, Func<T1, T2, T3, TResult> callback)
    {
        page.ExposeFunctionAsync<T1, T2, T3, TResult>(name, callback);
    }

    public static void ExposeFunction<T1, T2, T3, T4, TResult>(this IPage page, string name, Func<T1, T2, T3, T4, TResult> callback)
    {
        page.ExposeFunctionAsync<T1, T2, T3, T4, TResult>(name, callback);
    }

    public static void Reload(this IPage page, PageReloadOptions? options = null)
    {
        page.ReloadAsync(options).GetAwaiter().GetResult();
    }

    public static byte[] Screenshot(this IPage page, PageScreenshotOptions? options = null)
    {
        return page.ScreenshotAsync(options).GetAwaiter().GetResult();
    }
}
