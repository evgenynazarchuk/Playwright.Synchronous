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

namespace Playwright.Synchronous;

public static class ElementHandleSynchronous
{
    public static ElementHandleBoundingBoxResult? BoundingBox(this IElementHandle element)
    {
        return element.BoundingBoxAsync().GetAwaiter().GetResult();
    }

    public static void Check(this IElementHandle element, ElementHandleCheckOptions? options = null)
    {
        element.CheckAsync(options).GetAwaiter().GetResult();
    }

    public static void Click(this IElementHandle element, ElementHandleClickOptions? options = null)
    {
        element.ClickAsync(options).GetAwaiter().GetResult();
    }

    public static IFrame? ContentFrame(this IElementHandle element)
    {
        return element.ContentFrameAsync().GetAwaiter().GetResult();
    }

    public static void DblClick(this IElementHandle element, ElementHandleDblClickOptions? options = null)
    {
        element.DblClickAsync(options).GetAwaiter().GetResult();
    }

    public static void DispatchEvent(this IElementHandle element, string type, object? eventInit = null)
    {
        element.DispatchEventAsync(type, eventInit).GetAwaiter().GetResult();
    }

    public static T EvalOnSelector<T>(this IElementHandle element, string selector, string expression, object? arg = null)
    {
        return element.EvalOnSelectorAsync<T>(selector, expression, arg).GetAwaiter().GetResult();
    }

    public static T EvalOnSelectorAll<T>(this IElementHandle element, string selector, string expression, object? arg = null)
    {
        return element.EvalOnSelectorAllAsync<T>(selector, expression, arg).GetAwaiter().GetResult();
    }

    public static void Fill(this IElementHandle element, string value, ElementHandleFillOptions? options = null)
    {
        element.FillAsync(value, options).GetAwaiter().GetResult();
    }

    public static void Focus(this IElementHandle element)
    {
        element.FocusAsync().GetAwaiter().GetResult();
    }

    public static string? GetAttribute(this IElementHandle element, string name)
    {
        return element.GetAttributeAsync(name).GetAwaiter().GetResult();
    }

    public static void Hover(this IElementHandle element, ElementHandleHoverOptions? options = null)
    {
        element.HoverAsync(options).GetAwaiter().GetResult();
    }

    public static string InnerHTML(this IElementHandle element)
    {
        return element.InnerHTMLAsync().GetAwaiter().GetResult();
    }

    public static string InnerText(this IElementHandle element)
    {
        return element.InnerTextAsync().GetAwaiter().GetResult();
    }

    public static string InputValue(this IElementHandle element, ElementHandleInputValueOptions? options = null)
    {
        return element.InputValueAsync(options).GetAwaiter().GetResult();
    }

    public static bool IsChecked(this IElementHandle element)
    {
        return element.IsCheckedAsync().GetAwaiter().GetResult();
    }

    public static bool IsDisabled(this IElementHandle element)
    {
        return element.IsDisabledAsync().GetAwaiter().GetResult();
    }

    public static bool IsEditable(this IElementHandle element)
    {
        return element.IsEditableAsync().GetAwaiter().GetResult();
    }

    public static bool IsEnabled(this IElementHandle element)
    {
        return element.IsEnabledAsync().GetAwaiter().GetResult();
    }

    public static bool IsHidden(this IElementHandle element)
    {
        return element.IsHiddenAsync().GetAwaiter().GetResult();
    }

    public static bool IsVisible(this IElementHandle element)
    {
        return element.IsVisibleAsync().GetAwaiter().GetResult();
    }

    public static IFrame? OwnerFrame(this IElementHandle element)
    {
        return element.OwnerFrameAsync().GetAwaiter().GetResult();
    }

    public static void Press(this IElementHandle element, string key, ElementHandlePressOptions? options = null)
    {
        element.PressAsync(key, options).GetAwaiter().GetResult();
    }

    public static IElementHandle? QuerySelector(this IElementHandle element, string selector)
    {
        return element.QuerySelectorAsync(selector).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<IElementHandle> QuerySelectorAll(this IElementHandle element, string selector)
    {
        return element.QuerySelectorAllAsync(selector).GetAwaiter().GetResult();
    }

    public static byte[] Screenshot(this IElementHandle element, ElementHandleScreenshotOptions? options = null)
    {
        return element.ScreenshotAsync(options).GetAwaiter().GetResult();
    }

    public static void ScrollIntoViewIfNeeded(this IElementHandle element, ElementHandleScrollIntoViewIfNeededOptions? options = null)
    {
        element.ScrollIntoViewIfNeededAsync(options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IElementHandle element, string values, ElementHandleSelectOptionOptions? options = null)
    {
        return element.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IElementHandle element, IElementHandle values, ElementHandleSelectOptionOptions? options = null)
    {
        return element.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IElementHandle element, IEnumerable<string> values, ElementHandleSelectOptionOptions? options = null)
    {
        return element.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IElementHandle element, SelectOptionValue values, ElementHandleSelectOptionOptions? options = null)
    {
        return element.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IElementHandle element, IEnumerable<IElementHandle> values, ElementHandleSelectOptionOptions? options = null)
    {
        return element.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this IElementHandle element, IEnumerable<SelectOptionValue> values, ElementHandleSelectOptionOptions? options = null)
    {
        return element.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static void SelectText(this IElementHandle element, ElementHandleSelectTextOptions? options = null)
    {
        element.SelectTextAsync(options).GetAwaiter().GetResult();
    }

    public static void SetChecked(this IElementHandle element, bool checkedState, ElementHandleSetCheckedOptions? options = null)
    {
        element.SetCheckedAsync(checkedState, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IElementHandle element, string files, ElementHandleSetInputFilesOptions? options = null)
    {
        element.SetInputFilesAsync(files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IElementHandle element, IEnumerable<string> files, ElementHandleSetInputFilesOptions? options = null)
    {
        element.SetInputFilesAsync(files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IElementHandle element, FilePayload files, ElementHandleSetInputFilesOptions? options = null)
    {
        element.SetInputFilesAsync(files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this IElementHandle element, IEnumerable<FilePayload> files, ElementHandleSetInputFilesOptions? options = null)
    {
        element.SetInputFilesAsync(files, options).GetAwaiter().GetResult();
    }

    public static void Tap(this IElementHandle element, ElementHandleTapOptions? options = null)
    {
        element.TapAsync(options).GetAwaiter().GetResult();
    }

    public static string? TextContent(this IElementHandle element)
    {
        return element.TextContentAsync().GetAwaiter().GetResult();
    }

    public static void Type(this IElementHandle element, string text, ElementHandleTypeOptions? options = null)
    {
        element.TypeAsync(text, options).GetAwaiter().GetResult();
    }

    public static void Uncheck(this IElementHandle element, ElementHandleUncheckOptions? options = null)
    {
        element.UncheckAsync(options).GetAwaiter().GetResult();
    }

    public static void WaitForElementState(this IElementHandle element, ElementState state, ElementHandleWaitForElementStateOptions? options = null)
    {
        element.WaitForElementStateAsync(state, options).GetAwaiter().GetResult();
    }

    public static IElementHandle? WaitForSelector(this IElementHandle element, string selector, ElementHandleWaitForSelectorOptions? options = null)
    {
        return element.WaitForSelectorAsync(selector, options).GetAwaiter().GetResult();
    }

    public static JsonElement? EvalOnSelector(this IElementHandle element, string selector, string expression, object? arg = null)
    {
        return element.EvalOnSelectorAsync(selector, expression, arg).GetAwaiter().GetResult();
    }
}
