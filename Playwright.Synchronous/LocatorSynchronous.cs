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

public static class LocatorSynchronous
{
    public static IReadOnlyList<string> AllInnerTexts(this ILocator locator)
    {
        return locator.AllInnerTextsAsync().GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> AllTextContents(this ILocator locator)
    {
        return locator.AllTextContentsAsync().GetAwaiter().GetResult();
    }

    public static LocatorBoundingBoxResult? BoundingBox(this ILocator locator, LocatorBoundingBoxOptions? options = null)
    {
        return locator.BoundingBoxAsync(options).GetAwaiter().GetResult();
    }

    public static void Check(this ILocator locator, LocatorCheckOptions? options = null)
    {
        locator.CheckAsync(options).GetAwaiter().GetResult();
    }

    public static void Click(this ILocator locator, LocatorClickOptions? options = null)
    {
        locator.ClickAsync(options).GetAwaiter().GetResult();
    }

    public static int Count(this ILocator locator)
    {
        return locator.CountAsync().GetAwaiter().GetResult();
    }

    public static void DblClick(this ILocator locator, LocatorDblClickOptions? options = null)
    {
        locator.DblClickAsync(options).GetAwaiter().GetResult();
    }

    public static void DispatchEvent(this ILocator locator, string type, object? eventInit = null, LocatorDispatchEventOptions? options = null)
    {
        locator.DispatchEventAsync(type, eventInit, options).GetAwaiter().GetResult();
    }

    public static IElementHandle ElementHandle(this ILocator locator, LocatorElementHandleOptions? options = null)
    {
        return locator.ElementHandleAsync(options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<IElementHandle> ElementHandles(this ILocator locator)
    {
        return locator.ElementHandlesAsync().GetAwaiter().GetResult();
    }

    public static T Evaluate<T>(this ILocator locator, string expression, object? arg = null, LocatorEvaluateOptions? options = null)
    {
        return locator.EvaluateAsync<T>(expression, arg, options).GetAwaiter().GetResult();
    }

    public static T EvaluateAll<T>(this ILocator locator, string expression, object? arg = null)
    {
        return locator.EvaluateAllAsync<T>(expression, arg).GetAwaiter().GetResult();
    }

    public static IJSHandle EvaluateHandle(this ILocator locator, string expression, object? arg = null, LocatorEvaluateHandleOptions? options = null)
    {
        return locator.EvaluateHandleAsync(expression, arg, options).GetAwaiter().GetResult();
    }

    public static void Fill(this ILocator locator, string value, LocatorFillOptions? options = null)
    {
        locator.FillAsync(value, options).GetAwaiter().GetResult();
    }

    public static void Focus(this ILocator locator, LocatorFocusOptions? options = null)
    {
        locator.FocusAsync(options).GetAwaiter().GetResult();
    }

    public static string? GetAttribute(this ILocator locator, string name, LocatorGetAttributeOptions? options = null)
    {
        return locator.GetAttributeAsync(name, options).GetAwaiter().GetResult();
    }

    public static void Hover(this ILocator locator, LocatorHoverOptions? options = null)
    {
        locator.HoverAsync(options).GetAwaiter().GetResult();
    }

    public static string InnerHTML(this ILocator locator, LocatorInnerHTMLOptions? options = null)
    {
        return locator.InnerHTMLAsync(options).GetAwaiter().GetResult();
    }

    public static string InnerText(this ILocator locator, LocatorInnerTextOptions? options = null)
    {
        return locator.InnerTextAsync(options).GetAwaiter().GetResult();
    }

    public static string InputValue(this ILocator locator, LocatorInputValueOptions? options = null)
    {
        return locator.InputValueAsync(options).GetAwaiter().GetResult();
    }

    public static bool IsChecked(this ILocator locator, LocatorIsCheckedOptions? options = null)
    {
        return locator.IsCheckedAsync(options).GetAwaiter().GetResult();
    }

    public static bool IsDisabled(this ILocator locator, LocatorIsDisabledOptions? options = null)
    {
        return locator.IsDisabledAsync(options).GetAwaiter().GetResult();
    }

    public static bool IsEditable(this ILocator locator, LocatorIsEditableOptions? options = null)
    {
        return locator.IsEditableAsync(options).GetAwaiter().GetResult();
    }

    public static bool IsEnabled(this ILocator locator, LocatorIsEnabledOptions? options = null)
    {
        return locator.IsEnabledAsync(options).GetAwaiter().GetResult();
    }

    public static bool IsHidden(this ILocator locator, LocatorIsHiddenOptions? options = null)
    {
        return locator.IsHiddenAsync(options).GetAwaiter().GetResult();
    }

    public static bool IsVisible(this ILocator locator, LocatorIsVisibleOptions? options = null)
    {
        return locator.IsVisibleAsync(options).GetAwaiter().GetResult();
    }

    public static void Press(this ILocator locator, string key, LocatorPressOptions? options = null)
    {
        locator.PressAsync(key, options).GetAwaiter().GetResult();
    }

    public static byte[] Screenshot(this ILocator locator, LocatorScreenshotOptions? options = null)
    {
        return locator.ScreenshotAsync(options).GetAwaiter().GetResult();
    }

    public static void ScrollIntoViewIfNeeded(this ILocator locator, LocatorScrollIntoViewIfNeededOptions? options = null)
    {
        locator.ScrollIntoViewIfNeededAsync(options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this ILocator locator, string values, LocatorSelectOptionOptions? options = null)
    {
        return locator.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this ILocator locator, IElementHandle values, LocatorSelectOptionOptions? options = null)
    {
        return locator.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this ILocator locator, IEnumerable<string> values, LocatorSelectOptionOptions? options = null)
    {
        return locator.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this ILocator locator, SelectOptionValue values, LocatorSelectOptionOptions? options = null)
    {
        return locator.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this ILocator locator, IEnumerable<IElementHandle> values, LocatorSelectOptionOptions? options = null)
    {
        return locator.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> SelectOption(this ILocator locator, IEnumerable<SelectOptionValue> values, LocatorSelectOptionOptions? options = null)
    {
        return locator.SelectOptionAsync(values, options).GetAwaiter().GetResult();
    }

    public static void SelectText(this ILocator locator, LocatorSelectTextOptions? options = null)
    {
        locator.SelectTextAsync(options).GetAwaiter().GetResult();
    }

    public static void SetChecked(this ILocator locator, bool checkedState, LocatorSetCheckedOptions? options = null)
    {
        locator.SetCheckedAsync(checkedState, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this ILocator locator, string files, LocatorSetInputFilesOptions? options = null)
    {
        locator.SetInputFilesAsync(files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this ILocator locator, IEnumerable<string> files, LocatorSetInputFilesOptions? options = null)
    {
        locator.SetInputFilesAsync(files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this ILocator locator, FilePayload files, LocatorSetInputFilesOptions? options = null)
    {
        locator.SetInputFilesAsync(files, options).GetAwaiter().GetResult();
    }

    public static void SetInputFiles(this ILocator locator, IEnumerable<FilePayload> files, LocatorSetInputFilesOptions? options = null)
    {
        locator.SetInputFilesAsync(files, options).GetAwaiter().GetResult();
    }

    public static void Tap(this ILocator locator, LocatorTapOptions? options = null)
    {
        locator.TapAsync(options).GetAwaiter().GetResult();
    }

    public static string? TextContent(this ILocator locator, LocatorTextContentOptions? options = null)
    {
        return locator.TextContentAsync(options).GetAwaiter().GetResult();
    }

    public static void Type(this ILocator locator, string text, LocatorTypeOptions? options = null)
    {
        locator.TypeAsync(text, options).GetAwaiter().GetResult();
    }

    public static void Uncheck(this ILocator locator, LocatorUncheckOptions? options = null)
    {
        locator.UncheckAsync(options).GetAwaiter().GetResult();
    }

    public static void WaitFor(this ILocator locator, LocatorWaitForOptions? options = null)
    {
        locator.WaitForAsync(options).GetAwaiter().GetResult();
    }

    public static JsonElement? Evaluate(this ILocator locator, string expression, object? arg = null, LocatorEvaluateOptions? options = null)
    {
        return locator.EvaluateAsync(expression, arg, options).GetAwaiter().GetResult();
    }
}
