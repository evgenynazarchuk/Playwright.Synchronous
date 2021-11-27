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
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Playwright.Synchronous;

public static class FrameSynchronous
{
    public static IElementHandle AddScriptTag(this IFrame frame, FrameAddScriptTagOptions? options = null)
    {
        return frame.AddScriptTagAsync(options).GetAwaiter().GetResult();
    }

    public static IElementHandle AddStyleTag(this IFrame frame, FrameAddStyleTagOptions? options = null)
    {
        return frame.AddStyleTagAsync(options).GetAwaiter().GetResult();
    }

    public static void Check(this IFrame frame, string selector, FrameCheckOptions? options = null)
    {
        frame.CheckAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void Click(this IFrame frame, string selector, FrameClickOptions? options = null)
    {
        frame.ClickAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string Content(this IFrame frame)
    {
        return frame.ContentAsync().GetAwaiter().GetResult();
    }

    public static void DblClick(this IFrame frame, string selector, FrameDblClickOptions? options = null)
    {
        frame.DblClickAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void DispatchEvent(this IFrame frame, string selector, string type, object? eventInit = null, FrameDispatchEventOptions? options = null)
    {
        frame.DispatchEventAsync(selector, type, eventInit, options).GetAwaiter().GetResult();
    }

    public static void DragAndDrop(this IFrame frame, string source, string target, FrameDragAndDropOptions? options = null)
    {
        frame.DragAndDropAsync(source, target, options).GetAwaiter().GetResult();
    }

    public static T EvalOnSelector<T>(this IFrame frame, string selector, string expression, object? arg = null, FrameEvalOnSelectorOptions? options = null)
    {
        return frame.EvalOnSelectorAsync<T>(selector, expression, arg).GetAwaiter().GetResult();
    }

    public static T EvalOnSelectorAll<T>(this IFrame frame, string selector, string expression, object? arg = null)
    {
        return frame.EvalOnSelectorAllAsync<T>(selector, expression).GetAwaiter().GetResult();
    }

    public static T Evaluate<T>(this IFrame frame, string expression, object? arg = null)
    {
        return frame.EvaluateAsync<T>(expression, arg).GetAwaiter().GetResult();
    }

    public static IJSHandle EvaluateHandle(this IFrame frame, string expression, object? arg = null)
    {
        return frame.EvaluateHandleAsync(expression, arg).GetAwaiter().GetResult();
    }

    public static void Fill(this IFrame frame, string selector, string value, FrameFillOptions? options = null)
    {
        frame.FillAsync(selector, value, options).GetAwaiter().GetResult();
    }

    public static void Focus(this IFrame frame, string selector, FrameFocusOptions? options = null)
    {
        frame.FocusAsync(selector, options).GetAwaiter().GetResult();
    }

    public static IElementHandle FrameElement(this IFrame frame)
    {
        return frame.FrameElementAsync().GetAwaiter().GetResult();
    }

    public static string? GetAttribute(this IFrame frame, string selector, string name, FrameGetAttributeOptions? options = null)
    {
        return frame.GetAttributeAsync(selector, name, options).GetAwaiter().GetResult();
    }

    public static IResponse? Goto(this IFrame frame, string url, FrameGotoOptions? options = null)
    {
        return frame.GotoAsync(url, options).GetAwaiter().GetResult();
    }

    public static void Hover(this IFrame frame, string selector, FrameHoverOptions? options = null)
    {
        frame.HoverAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string InnerHTML(this IFrame frame, string selector, FrameInnerHTMLOptions? options = null)
    {
        return frame.InnerHTMLAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string InnerText(this IFrame frame, string selector, FrameInnerTextOptions? options = null)
    {
        return frame.InnerTextAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string InputValue(this IFrame frame, string selector, FrameInputValueOptions? options = null)
    {
        return frame.InputValueAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsChecked(this IFrame frame, string selector, FrameIsCheckedOptions? options = null)
    {
        return frame.IsCheckedAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsDisabled(this IFrame frame, string selector, FrameIsDisabledOptions? options = null)
    {
        return frame.IsDisabledAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsEditable(this IFrame frame, string selector, FrameIsEditableOptions? options = null)
    {
        return frame.IsEditableAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsEnabled(this IFrame frame, string selector, FrameIsEnabledOptions? options = null)
    {
        return frame.IsEnabledAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsHidden(this IFrame frame, string selector, FrameIsHiddenOptions? options = null)
    {
        return frame.IsHiddenAsync(selector, options).GetAwaiter().GetResult();
    }

    public static bool IsVisible(this IFrame frame, string selector, FrameIsVisibleOptions? options = null)
    {
        return frame.IsVisibleAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void Press(this IFrame frame, string selector, string key, FramePressOptions? options = null)
    {
        frame.PressAsync(selector, key, options).GetAwaiter().GetResult();
    }

    public static IElementHandle? QuerySelector(this IFrame frame, string selector, FrameQuerySelectorOptions? options = null)
    {
        return frame.QuerySelectorAsync(selector, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<IElementHandle> QuerySelectorAll(this IFrame frame, string selector)
    {
        return frame.QuerySelectorAllAsync(selector).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IFrame frame, string selector, string values, FrameSelectOptionOptions? options = null)
    {
        return frame.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IFrame frame, string selector, IElementHandle values, FrameSelectOptionOptions? options = null)
    {
        return frame.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IFrame frame, string selector, IEnumerable<string> values, FrameSelectOptionOptions? options = null)
    {
        return frame.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IFrame frame, string selector, SelectOptionValue values, FrameSelectOptionOptions? options = null)
    {
        return frame.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IFrame frame, string selector, IEnumerable<IElementHandle> values, FrameSelectOptionOptions? options = null)
    {
        return frame.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IFrame frame, string selector, IEnumerable<SelectOptionValue> values, FrameSelectOptionOptions? options = null)
    {
        return frame.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    public static void SetChecked(this IFrame frame, string selector, bool checkedState, FrameSetCheckedOptions? options = null)
    {
        frame.SetCheckedAsync(selector, checkedState, options).GetAwaiter().GetResult();
    }

    public static void SetContent(this IFrame frame, string html, FrameSetContentOptions? options = null)
    {
        frame.SetContentAsync(html, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IFrame frame, string selector, string files, FrameSetInputFilesOptions? options = null)
    {
        frame.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IFrame frame, string selector, IEnumerable<string> files, FrameSetInputFilesOptions? options = null)
    {
        frame.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IFrame frame, string selector, FilePayload files, FrameSetInputFilesOptions? options = null)
    {
        frame.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IFrame frame, string selector, IEnumerable<FilePayload> files, FrameSetInputFilesOptions? options = null)
    {
        frame.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
    }

    public static void Tap(this IFrame frame, string selector, FrameTapOptions? options = null)
    {
        frame.TapAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string? TextContent(this IFrame frame, string selector, FrameTextContentOptions? options = null)
    {
        return frame.TextContentAsync(selector, options).GetAwaiter().GetResult();
    }

    public static string Title(this IFrame frame)
    {
        return frame.TitleAsync().GetAwaiter().GetResult();
    }

    public static void Type(this IFrame frame, string selector, string text, FrameTypeOptions? options = null)
    {
        frame.TypeAsync(selector, text, options).GetAwaiter().GetResult();
    }

    public static void Uncheck(this IFrame frame, string selector, FrameUncheckOptions? options = null)
    {
        frame.UncheckAsync(selector, options).GetAwaiter().GetResult();
    }

    public static IJSHandle WaitForFunction(this IFrame frame, string expression, object? arg = null, FrameWaitForFunctionOptions? options = null)
    {
        return frame.WaitForFunctionAsync(expression, arg, options).GetAwaiter().GetResult();
    }

    public static void WaitForLoadState(this IFrame frame, LoadState? state = null, FrameWaitForLoadStateOptions? options = null)
    {
        frame.WaitForLoadStateAsync(state, options).GetAwaiter().GetResult();
    }

    public static IResponse? WaitForNavigation(this IFrame frame, FrameWaitForNavigationOptions? options = null)
    {
        return frame.WaitForNavigationAsync(options).GetAwaiter().GetResult();
    }

    public static IResponse? RunAndWaitForNavigation(this IFrame frame, Func<Task> action, FrameRunAndWaitForNavigationOptions? options = null)
    {
        return frame.RunAndWaitForNavigationAsync(action, options).GetAwaiter().GetResult();
    }

    public static IElementHandle? WaitForSelector(this IFrame frame, string selector, FrameWaitForSelectorOptions? options = null)
    {
        return frame.WaitForSelectorAsync(selector, options).GetAwaiter().GetResult();
    }

    public static void WaitForTimeout(this IFrame frame, float timeout)
    {
        frame.WaitForTimeoutAsync(timeout).GetAwaiter().GetResult();
    }

    public static void WaitForURL(this IFrame frame, string url, FrameWaitForURLOptions? options = null)
    {
        frame.WaitForURLAsync(url, options).GetAwaiter().GetResult();
    }

    public static void WaitForURL(this IFrame frame, Regex url, FrameWaitForURLOptions? options = null)
    {
        frame.WaitForURLAsync(url, options).GetAwaiter().GetResult();
    }

    public static void WaitForURL(this IFrame frame, Func<string, bool> url, FrameWaitForURLOptions? options = null)
    {
        frame.WaitForURLAsync(url, options).GetAwaiter().GetResult();
    }

    public static JsonElement? Evaluate(this IFrame frame, string expression, object? arg = null)
    {
        return frame.EvaluateAsync(expression, arg).GetAwaiter().GetResult();
    }

    public static JsonElement? EvalOnSelector(this IFrame frame, string selector, string expression, object? arg = null)
    {
        return frame.EvalOnSelectorAsync(selector, expression, arg).GetAwaiter().GetResult();
    }

    public static JsonElement? EvalOnSelectorAll(this IFrame frame, string selector, string expression, object? arg = null)
    {
        return frame.EvalOnSelectorAllAsync(selector, expression, arg).GetAwaiter().GetResult();
    }
}