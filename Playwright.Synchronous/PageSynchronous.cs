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
    /// <summary>
    /// <para>
    /// Returns the main resource response. In case of multiple redirects, the navigation
    /// will resolve with the response of the last redirect.
    /// </para>
    /// <para>The method will throw an error if:</para>
    /// <list type="bullet">
    /// <item><description>there's an SSL error (e.g. in case of self-signed certificates).</description></item>
    /// <item><description>target URL is invalid.</description></item>
    /// <item><description>the <paramref name="timeout"/> is exceeded during navigation.</description></item>
    /// <item><description>the remote server does not respond or is unreachable.</description></item>
    /// <item><description>the main resource failed to load.</description></item>
    /// </list>
    /// <para>
    /// The method will not throw an error when any valid HTTP status code is returned by
    /// the remote server, including 404 "Not Found" and 500 "Internal Server Error".  The
    /// status code for such responses can be retrieved by calling <see cref="IResponse.Status"/>.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.GotoAsync"/></para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The method either throws an error or returns a main resource response. The only
    /// exceptions are navigation to <c>about:blank</c> or navigation to the same URL with
    /// a different hash, which would succeed and return <c>null</c>.
    /// </para>
    /// <para>
    /// Headless mode doesn't support navigation to a PDF document. See the <a href="https://bugs.chromium.org/p/chromium/issues/detail?id=761295">upstream
    /// issue</a>.
    /// </para>
    /// </remarks>
    /// <param name="url">
    /// URL to navigate page to. The url should include scheme, e.g. <c>https://</c>. When
    /// a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Goto(this IPage page, string url, PageGotoOptions? options = null)
    {
        page.GotoAsync(url, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Returns the main resource response. In case of multiple redirects, the navigation
    /// will resolve with the response of the last redirect. If can not go back, returns
    /// <c>null</c>.
    /// </para>
    /// <para>Navigate to the previous page in history.</para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IPage GoBack(this IPage page, PageGoBackOptions? options = null)
    {
        page.GoBackAsync(options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Returns the main resource response. In case of multiple redirects, the navigation
    /// will resolve with the response of the last redirect. If can not go forward, returns
    /// <c>null</c>.
    /// </para>
    /// <para>Navigate to the next page in history.</para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IPage GoForward(this IPage page, PageGoForwardOptions? options = null)
    {
        page.GoForwardAsync(options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method reloads the current page, in the same way as if the user had triggered
    /// a browser refresh. Returns the main resource response. In case of multiple redirects,
    /// the navigation will resolve with the response of the last redirect.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IResponse? Reload(this IPage page, PageReloadOptions? options = null)
    {
        return page.ReloadAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// If <paramref name="runBeforeUnload"/> is <c>false</c>, does not run any unload handlers
    /// and waits for the page to be closed. If <paramref name="runBeforeUnload"/> is <c>true</c>
    /// the method will run unload handlers, but will **not** wait for the page to close.
    /// </para>
    /// <para>By default, <c>page.close()</c> **does not** run <c>beforeunload</c> handlers.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// if <paramref name="runBeforeUnload"/> is passed as true, a <c>beforeunload</c> dialog
    /// might be summoned and should be handled manually via <see cref="IPage.Dialog"/>
    /// event.
    /// </para>
    /// </remarks>
    /// <param name="options">Call options</param>
    public static void ClosePage(this IPage page, PageCloseOptions? options = null)
    {
        page.CloseAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// The method finds an element matching the specified selector within the page. If
    /// no elements match the selector, the return value resolves to <c>null</c>. To wait
    /// for an element on the page, use <see cref="ILocator.WaitForAsync"/>.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.QuerySelectorAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The use of <see cref="IElementHandle"/> is discouraged, use <see cref="ILocator"/>
    /// objects and web-first assertions instead.
    /// </para>
    /// </remarks>
    /// <param name="selector">
    /// A selector to query for. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IElementHandle? QuerySelector(this IPage page, string selector, PageQuerySelectorOptions? options = null)
    {
        return page.QuerySelectorAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// The method finds all elements matching the specified selector within the page. If
    /// no elements match the selector, the return value resolves to <c>[]</c>.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.QuerySelectorAllAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The use of <see cref="IElementHandle"/> is discouraged, use <see cref="ILocator"/>
    /// objects and web-first assertions instead.
    /// </para>
    /// </remarks>
    /// <param name="selector">
    /// A selector to query for. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    public static IReadOnlyList<IElementHandle> QuerySelectorAll(this IPage page, string selector)
    {
        return page.QuerySelectorAllAsync(selector).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// This method checks an element matching <paramref name="selector"/> by performing
    /// the following steps:
    /// </para>
    /// <list type="ordinal">
    /// <item><description>
    /// Find an element matching <paramref name="selector"/>. If there is none, wait until
    /// a matching element is attached to the DOM.
    /// </description></item>
    /// <item><description>
    /// Ensure that matched element is a checkbox or a radio input. If not, this method
    /// throws. If the element is already checked, this method returns immediately.
    /// </description></item>
    /// <item><description>
    /// Wait for <a href="./actionability.md">actionability</a> checks on the matched element,
    /// unless <paramref name="force"/> option is set. If the element is detached during
    /// the checks, the whole action is retried.
    /// </description></item>
    /// <item><description>Scroll the element into view if needed.</description></item>
    /// <item><description>Use <see cref="IPage.Mouse"/> to click in the center of the element.</description></item>
    /// <item><description>
    /// Wait for initiated navigations to either succeed or fail, unless <paramref name="noWaitAfter"/>
    /// option is set.
    /// </description></item>
    /// <item><description>Ensure that the element is now checked. If not, this method throws.</description></item>
    /// </list>
    /// <para>
    /// When all steps combined have not finished during the specified <paramref name="timeout"/>,
    /// this method throws a <see cref="TimeoutException"/>. Passing zero timeout disables
    /// this.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.CheckAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Check(this IPage page, string selector, PageCheckOptions? options = null)
    {
        page.CheckAsync(selector, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Sends a <c>keydown</c>, <c>keypress</c>/<c>input</c>, and <c>keyup</c> event for
    /// each character in the text. <c>page.type</c> can be used to send fine-grained keyboard
    /// events. To fill values in form fields, use <see cref="IPage.FillAsync"/>.
    /// </para>
    /// <para>To press a special key, like <c>Control</c> or <c>ArrowDown</c>, use <see cref="IKeyboard.PressAsync"/>.</para>
    /// <code>
    /// await page.TypeAsync("#mytextarea", "hello"); // types instantly<br/>
    /// await page.TypeAsync("#mytextarea", "world"); // types slower, like a user
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.TypeAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="text">A text to type into a focused element.</param>
    /// <param name="options">Call options</param>
    public static IPage Type(this IPage page, string selector, string value, PageTypeOptions? options = null)
    {
        page.TypeAsync(selector, value, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method unchecks an element matching <paramref name="selector"/> by performing
    /// the following steps:
    /// </para>
    /// <list type="ordinal">
    /// <item><description>
    /// Find an element matching <paramref name="selector"/>. If there is none, wait until
    /// a matching element is attached to the DOM.
    /// </description></item>
    /// <item><description>
    /// Ensure that matched element is a checkbox or a radio input. If not, this method
    /// throws. If the element is already unchecked, this method returns immediately.
    /// </description></item>
    /// <item><description>
    /// Wait for <a href="./actionability.md">actionability</a> checks on the matched element,
    /// unless <paramref name="force"/> option is set. If the element is detached during
    /// the checks, the whole action is retried.
    /// </description></item>
    /// <item><description>Scroll the element into view if needed.</description></item>
    /// <item><description>Use <see cref="IPage.Mouse"/> to click in the center of the element.</description></item>
    /// <item><description>
    /// Wait for initiated navigations to either succeed or fail, unless <paramref name="noWaitAfter"/>
    /// option is set.
    /// </description></item>
    /// <item><description>Ensure that the element is now unchecked. If not, this method throws.</description></item>
    /// </list>
    /// <para>
    /// When all steps combined have not finished during the specified <paramref name="timeout"/>,
    /// this method throws a <see cref="TimeoutException"/>. Passing zero timeout disables
    /// this.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.UncheckAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Uncheck(this IPage page, string selector, PageUncheckOptions? options = null)
    {
        page.UncheckAsync(selector, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method clicks an element matching <paramref name="selector"/> by performing
    /// the following steps:
    /// </para>
    /// <list type="ordinal">
    /// <item><description>
    /// Find an element matching <paramref name="selector"/>. If there is none, wait until
    /// a matching element is attached to the DOM.
    /// </description></item>
    /// <item><description>
    /// Wait for <a href="./actionability.md">actionability</a> checks on the matched element,
    /// unless <paramref name="force"/> option is set. If the element is detached during
    /// the checks, the whole action is retried.
    /// </description></item>
    /// <item><description>Scroll the element into view if needed.</description></item>
    /// <item><description>
    /// Use <see cref="IPage.Mouse"/> to click in the center of the element, or the specified
    /// <paramref name="position"/>.
    /// </description></item>
    /// <item><description>
    /// Wait for initiated navigations to either succeed or fail, unless <paramref name="noWaitAfter"/>
    /// option is set.
    /// </description></item>
    /// </list>
    /// <para>
    /// When all steps combined have not finished during the specified <paramref name="timeout"/>,
    /// this method throws a <see cref="TimeoutException"/>. Passing zero timeout disables
    /// this.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.ClickAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Click(this IPage page, string selector, PageClickOptions? options = null)
    {
        page.ClickAsync(selector, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method double clicks an element matching <paramref name="selector"/> by performing
    /// the following steps:
    /// </para>
    /// <list type="ordinal">
    /// <item><description>
    /// Find an element matching <paramref name="selector"/>. If there is none, wait until
    /// a matching element is attached to the DOM.
    /// </description></item>
    /// <item><description>
    /// Wait for <a href="./actionability.md">actionability</a> checks on the matched element,
    /// unless <paramref name="force"/> option is set. If the element is detached during
    /// the checks, the whole action is retried.
    /// </description></item>
    /// <item><description>Scroll the element into view if needed.</description></item>
    /// <item><description>
    /// Use <see cref="IPage.Mouse"/> to double click in the center of the element, or the
    /// specified <paramref name="position"/>.
    /// </description></item>
    /// <item><description>
    /// Wait for initiated navigations to either succeed or fail, unless <paramref name="noWaitAfter"/>
    /// option is set. Note that if the first click of the <c>dblclick()</c> triggers a
    /// navigation event, this method will throw.
    /// </description></item>
    /// </list>
    /// <para>
    /// When all steps combined have not finished during the specified <paramref name="timeout"/>,
    /// this method throws a <see cref="TimeoutException"/>. Passing zero timeout disables
    /// this.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.DblClickAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// <c>page.dblclick()</c> dispatches two <c>click</c> events and a single <c>dblclick</c>
    /// event.
    /// </para>
    /// </remarks>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage DblClick(this IPage page, string selector, PageDblClickOptions? options = null)
    {
        page.DblClickAsync(selector, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method waits for an element matching <paramref name="selector"/>, waits for
    /// <a href="./actionability.md">actionability</a> checks, focuses the element, fills
    /// it and triggers an <c>input</c> event after filling. Note that you can pass an empty
    /// string to clear the input field.
    /// </para>
    /// <para>
    /// If the target element is not an <c>&lt;input&gt;</c>, <c>&lt;textarea&gt;</c> or
    /// <c>[contenteditable]</c> element, this method throws an error. However, if the element
    /// is inside the <c>&lt;label&gt;</c> element that has an associated <a href="https://developer.mozilla.org/en-US/docs/Web/API/HTMLLabelElement/control">control</a>,
    /// the control will be filled instead.
    /// </para>
    /// <para>To send fine-grained keyboard events, use <see cref="IPage.TypeAsync"/>.</para>
    /// <para>Shortcut for main frame's <see cref="IFrame.FillAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="value">
    /// Value to fill for the <c>&lt;input&gt;</c>, <c>&lt;textarea&gt;</c> or <c>[contenteditable]</c>
    /// element.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Fill(this IPage page, string selector, string value, PageFillOptions? options = null)
    {
        page.FillAsync(selector, value, options);
        return page;
    }

    /// <summary>
    /// <para>
    /// This method fetches an element with <paramref name="selector"/> and focuses it.
    /// If there's no element matching <paramref name="selector"/>, the method waits until
    /// a matching element appears in the DOM.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.FocusAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Focus(this IPage page, string selector, PageFocusOptions? options = null)
    {
        page.FocusAsync(selector, options).GetAwaiter().GetResult();
        return page;
    }

    /// <param name="source">
    /// </param>
    /// <param name="target">
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage DragAndDrop(this IPage page, string source, string target, PageDragAndDropOptions? options = null)
    {
        page.DragAndDropAsync(source, target, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method hovers over an element matching <paramref name="selector"/> by performing
    /// the following steps:
    /// </para>
    /// <list type="ordinal">
    /// <item><description>
    /// Find an element matching <paramref name="selector"/>. If there is none, wait until
    /// a matching element is attached to the DOM.
    /// </description></item>
    /// <item><description>
    /// Wait for <a href="./actionability.md">actionability</a> checks on the matched element,
    /// unless <paramref name="force"/> option is set. If the element is detached during
    /// the checks, the whole action is retried.
    /// </description></item>
    /// <item><description>Scroll the element into view if needed.</description></item>
    /// <item><description>
    /// Use <see cref="IPage.Mouse"/> to hover over the center of the element, or the specified
    /// <paramref name="position"/>.
    /// </description></item>
    /// <item><description>
    /// Wait for initiated navigations to either succeed or fail, unless <c>noWaitAfter</c>
    /// option is set.
    /// </description></item>
    /// </list>
    /// <para>
    /// When all steps combined have not finished during the specified <paramref name="timeout"/>,
    /// this method throws a <see cref="TimeoutException"/>. Passing zero timeout disables
    /// this.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.HoverAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Hover(this IPage page, string selector, PageHoverOptions? options = null)
    {
        page.HoverAsync(selector, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>Focuses the element, and then uses <see cref="IKeyboard.DownAsync"/> and <see cref="IKeyboard.UpAsync"/>.</para>
    /// <para>
    /// <paramref name="key"/> can specify the intended <a href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key">keyboardEvent.key</a>
    /// value or a single character to generate the text for. A superset of the <paramref
    /// name="key"/> values can be found <a href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key/Key_Values">here</a>.
    /// Examples of the keys are:
    /// </para>
    /// <para>
    /// <c>F1</c> - <c>F12</c>, <c>Digit0</c>- <c>Digit9</c>, <c>KeyA</c>- <c>KeyZ</c>,
    /// <c>Backquote</c>, <c>Minus</c>, <c>Equal</c>, <c>Backslash</c>, <c>Backspace</c>,
    /// <c>Tab</c>, <c>Delete</c>, <c>Escape</c>, <c>ArrowDown</c>, <c>End</c>, <c>Enter</c>,
    /// <c>Home</c>, <c>Insert</c>, <c>PageDown</c>, <c>PageUp</c>, <c>ArrowRight</c>, <c>ArrowUp</c>,
    /// etc.
    /// </para>
    /// <para>
    /// Following modification shortcuts are also supported: <c>Shift</c>, <c>Control</c>,
    /// <c>Alt</c>, <c>Meta</c>, <c>ShiftLeft</c>.
    /// </para>
    /// <para>
    /// Holding down <c>Shift</c> will type the text that corresponds to the <paramref name="key"/>
    /// in the upper case.
    /// </para>
    /// <para>
    /// If <paramref name="key"/> is a single character, it is case-sensitive, so the values
    /// <c>a</c> and <c>A</c> will generate different respective texts.
    /// </para>
    /// <para>
    /// Shortcuts such as <c>key: "Control+o"</c> or <c>key: "Control+Shift+T"</c> are supported
    /// as well. When specified with the modifier, modifier is pressed and being held while
    /// the subsequent key is being pressed.
    /// </para>
    /// <code>
    /// var page = await browser.NewPageAsync();<br/>
    /// await page.GotoAsync("https://keycode.info");<br/>
    /// await page.PressAsync("body", "A");<br/>
    /// await page.ScreenshotAsync(new PageScreenshotOptions { Path = "A.png" });<br/>
    /// await page.PressAsync("body", "ArrowLeft");<br/>
    /// await page.ScreenshotAsync(new PageScreenshotOptions { Path = "ArrowLeft.png" });<br/>
    /// await page.PressAsync("body", "Shift+O");<br/>
    /// await page.ScreenshotAsync(new PageScreenshotOptions { Path = "O.png" });
    /// </code>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="key">
    /// Name of the key to press or a character to generate, such as <c>ArrowLeft</c> or
    /// <c>a</c>.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Press(this IPage page, string selector, string key, PagePressOptions? options = null)
    {
        page.PressAsync(selector, key, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method taps an element matching <paramref name="selector"/> by performing the
    /// following steps:
    /// </para>
    /// <list type="ordinal">
    /// <item><description>
    /// Find an element matching <paramref name="selector"/>. If there is none, wait until
    /// a matching element is attached to the DOM.
    /// </description></item>
    /// <item><description>
    /// Wait for <a href="./actionability.md">actionability</a> checks on the matched element,
    /// unless <paramref name="force"/> option is set. If the element is detached during
    /// the checks, the whole action is retried.
    /// </description></item>
    /// <item><description>Scroll the element into view if needed.</description></item>
    /// <item><description>
    /// Use <see cref="IPage.Touchscreen"/> to tap the center of the element, or the specified
    /// <paramref name="position"/>.
    /// </description></item>
    /// <item><description>
    /// Wait for initiated navigations to either succeed or fail, unless <paramref name="noWaitAfter"/>
    /// option is set.
    /// </description></item>
    /// </list>
    /// <para>
    /// When all steps combined have not finished during the specified <paramref name="timeout"/>,
    /// this method throws a <see cref="TimeoutException"/>. Passing zero timeout disables
    /// this.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.TapAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="IPage.TapAsync"/> requires that the <paramref name="hasTouch"/> option
    /// of the browser context be set to true.
    /// </para>
    /// </remarks>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage Tap(this IPage page, string selector, PageTapOptions? options = null)
    {
        page.TapAsync(selector, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method checks or unchecks an element matching <paramref name="selector"/> by
    /// performing the following steps:
    /// </para>
    /// <list type="ordinal">
    /// <item><description>
    /// Find an element matching <paramref name="selector"/>. If there is none, wait until
    /// a matching element is attached to the DOM.
    /// </description></item>
    /// <item><description>
    /// Ensure that matched element is a checkbox or a radio input. If not, this method
    /// throws.
    /// </description></item>
    /// <item><description>If the element already has the right checked state, this method returns immediately.</description></item>
    /// <item><description>
    /// Wait for <a href="./actionability.md">actionability</a> checks on the matched element,
    /// unless <paramref name="force"/> option is set. If the element is detached during
    /// the checks, the whole action is retried.
    /// </description></item>
    /// <item><description>Scroll the element into view if needed.</description></item>
    /// <item><description>Use <see cref="IPage.Mouse"/> to click in the center of the element.</description></item>
    /// <item><description>
    /// Wait for initiated navigations to either succeed or fail, unless <paramref name="noWaitAfter"/>
    /// option is set.
    /// </description></item>
    /// <item><description>Ensure that the element is now checked or unchecked. If not, this method throws.</description></item>
    /// </list>
    /// <para>
    /// When all steps combined have not finished during the specified <paramref name="timeout"/>,
    /// this method throws a <see cref="TimeoutException"/>. Passing zero timeout disables
    /// this.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.SetCheckedAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="checkedState">Whether to check or uncheck the checkbox.</param>
    /// <param name="options">Call options</param>
    public static IPage SetChecked(this IPage page, string selector, bool checkedState, PageSetCheckedOptions? options = null)
    {
        page.SetCheckedAsync(selector, checkedState, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method waits for an element matching <paramref name="selector"/>, waits for
    /// <a href="./actionability.md">actionability</a> checks, waits until all specified
    /// options are present in the <c>&lt;select&gt;</c> element and selects these options.
    /// </para>
    /// <para>
    /// If the target element is not a <c>&lt;select&gt;</c> element, this method throws
    /// an error. However, if the element is inside the <c>&lt;label&gt;</c> element that
    /// has an associated <a href="https://developer.mozilla.org/en-US/docs/Web/API/HTMLLabelElement/control">control</a>,
    /// the control will be used instead.
    /// </para>
    /// <para>Returns the array of option values that have been successfully selected.</para>
    /// <para>
    /// Triggers a <c>change</c> and <c>input</c> event once all the provided options have
    /// been selected.
    /// </para>
    /// <code>
    /// // single selection matching the value<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "blue" });<br/>
    /// // single selection matching both the value and the label<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { new SelectOptionValue() { Label = "blue" } });<br/>
    /// // multiple<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "red", "green", "blue" });
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.SelectOptionAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="values">
    /// Options to select. If the <c>&lt;select&gt;</c> has the <c>multiple</c> attribute,
    /// all matching options are selected, otherwise only the first option matching one
    /// of the passed options is selected. String values are equivalent to <c>{value:'string'}</c>.
    /// Option is considered matching if all specified properties match.
    /// </param>
    /// <param name="options">Call options</param>
    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, string values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// This method waits for an element matching <paramref name="selector"/>, waits for
    /// <a href="./actionability.md">actionability</a> checks, waits until all specified
    /// options are present in the <c>&lt;select&gt;</c> element and selects these options.
    /// </para>
    /// <para>
    /// If the target element is not a <c>&lt;select&gt;</c> element, this method throws
    /// an error. However, if the element is inside the <c>&lt;label&gt;</c> element that
    /// has an associated <a href="https://developer.mozilla.org/en-US/docs/Web/API/HTMLLabelElement/control">control</a>,
    /// the control will be used instead.
    /// </para>
    /// <para>Returns the array of option values that have been successfully selected.</para>
    /// <para>
    /// Triggers a <c>change</c> and <c>input</c> event once all the provided options have
    /// been selected.
    /// </para>
    /// <code>
    /// // single selection matching the value<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "blue" });<br/>
    /// // single selection matching both the value and the label<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { new SelectOptionValue() { Label = "blue" } });<br/>
    /// // multiple<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "red", "green", "blue" });
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.SelectOptionAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="values">
    /// Options to select. If the <c>&lt;select&gt;</c> has the <c>multiple</c> attribute,
    /// all matching options are selected, otherwise only the first option matching one
    /// of the passed options is selected. String values are equivalent to <c>{value:'string'}</c>.
    /// Option is considered matching if all specified properties match.
    /// </param>
    /// <param name="options">Call options</param>
    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, IElementHandle values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// This method waits for an element matching <paramref name="selector"/>, waits for
    /// <a href="./actionability.md">actionability</a> checks, waits until all specified
    /// options are present in the <c>&lt;select&gt;</c> element and selects these options.
    /// </para>
    /// <para>
    /// If the target element is not a <c>&lt;select&gt;</c> element, this method throws
    /// an error. However, if the element is inside the <c>&lt;label&gt;</c> element that
    /// has an associated <a href="https://developer.mozilla.org/en-US/docs/Web/API/HTMLLabelElement/control">control</a>,
    /// the control will be used instead.
    /// </para>
    /// <para>Returns the array of option values that have been successfully selected.</para>
    /// <para>
    /// Triggers a <c>change</c> and <c>input</c> event once all the provided options have
    /// been selected.
    /// </para>
    /// <code>
    /// // single selection matching the value<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "blue" });<br/>
    /// // single selection matching both the value and the label<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { new SelectOptionValue() { Label = "blue" } });<br/>
    /// // multiple<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "red", "green", "blue" });
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.SelectOptionAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="values">
    /// Options to select. If the <c>&lt;select&gt;</c> has the <c>multiple</c> attribute,
    /// all matching options are selected, otherwise only the first option matching one
    /// of the passed options is selected. String values are equivalent to <c>{value:'string'}</c>.
    /// Option is considered matching if all specified properties match.
    /// </param>
    /// <param name="options">Call options</param>
    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, IEnumerable<string> values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// This method waits for an element matching <paramref name="selector"/>, waits for
    /// <a href="./actionability.md">actionability</a> checks, waits until all specified
    /// options are present in the <c>&lt;select&gt;</c> element and selects these options.
    /// </para>
    /// <para>
    /// If the target element is not a <c>&lt;select&gt;</c> element, this method throws
    /// an error. However, if the element is inside the <c>&lt;label&gt;</c> element that
    /// has an associated <a href="https://developer.mozilla.org/en-US/docs/Web/API/HTMLLabelElement/control">control</a>,
    /// the control will be used instead.
    /// </para>
    /// <para>Returns the array of option values that have been successfully selected.</para>
    /// <para>
    /// Triggers a <c>change</c> and <c>input</c> event once all the provided options have
    /// been selected.
    /// </para>
    /// <code>
    /// // single selection matching the value<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "blue" });<br/>
    /// // single selection matching both the value and the label<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { new SelectOptionValue() { Label = "blue" } });<br/>
    /// // multiple<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "red", "green", "blue" });
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.SelectOptionAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="values">
    /// Options to select. If the <c>&lt;select&gt;</c> has the <c>multiple</c> attribute,
    /// all matching options are selected, otherwise only the first option matching one
    /// of the passed options is selected. String values are equivalent to <c>{value:'string'}</c>.
    /// Option is considered matching if all specified properties match.
    /// </param>
    /// <param name="options">Call options</param>
    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, SelectOptionValue values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// This method waits for an element matching <paramref name="selector"/>, waits for
    /// <a href="./actionability.md">actionability</a> checks, waits until all specified
    /// options are present in the <c>&lt;select&gt;</c> element and selects these options.
    /// </para>
    /// <para>
    /// If the target element is not a <c>&lt;select&gt;</c> element, this method throws
    /// an error. However, if the element is inside the <c>&lt;label&gt;</c> element that
    /// has an associated <a href="https://developer.mozilla.org/en-US/docs/Web/API/HTMLLabelElement/control">control</a>,
    /// the control will be used instead.
    /// </para>
    /// <para>Returns the array of option values that have been successfully selected.</para>
    /// <para>
    /// Triggers a <c>change</c> and <c>input</c> event once all the provided options have
    /// been selected.
    /// </para>
    /// <code>
    /// // single selection matching the value<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "blue" });<br/>
    /// // single selection matching both the value and the label<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { new SelectOptionValue() { Label = "blue" } });<br/>
    /// // multiple<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "red", "green", "blue" });
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.SelectOptionAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="values">
    /// Options to select. If the <c>&lt;select&gt;</c> has the <c>multiple</c> attribute,
    /// all matching options are selected, otherwise only the first option matching one
    /// of the passed options is selected. String values are equivalent to <c>{value:'string'}</c>.
    /// Option is considered matching if all specified properties match.
    /// </param>
    /// <param name="options">Call options</param>
    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, IEnumerable<IElementHandle> values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// This method waits for an element matching <paramref name="selector"/>, waits for
    /// <a href="./actionability.md">actionability</a> checks, waits until all specified
    /// options are present in the <c>&lt;select&gt;</c> element and selects these options.
    /// </para>
    /// <para>
    /// If the target element is not a <c>&lt;select&gt;</c> element, this method throws
    /// an error. However, if the element is inside the <c>&lt;label&gt;</c> element that
    /// has an associated <a href="https://developer.mozilla.org/en-US/docs/Web/API/HTMLLabelElement/control">control</a>,
    /// the control will be used instead.
    /// </para>
    /// <para>Returns the array of option values that have been successfully selected.</para>
    /// <para>
    /// Triggers a <c>change</c> and <c>input</c> event once all the provided options have
    /// been selected.
    /// </para>
    /// <code>
    /// // single selection matching the value<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "blue" });<br/>
    /// // single selection matching both the value and the label<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { new SelectOptionValue() { Label = "blue" } });<br/>
    /// // multiple<br/>
    /// await page.SelectOptionAsync("select#colors", new[] { "red", "green", "blue" });
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.SelectOptionAsync"/>.</para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="values">
    /// Options to select. If the <c>&lt;select&gt;</c> has the <c>multiple</c> attribute,
    /// all matching options are selected, otherwise only the first option matching one
    /// of the passed options is selected. String values are equivalent to <c>{value:'string'}</c>.
    /// Option is considered matching if all specified properties match.
    /// </param>
    /// <param name="options">Call options</param>
    public static IReadOnlyList<string> SelectOption(this IPage page, string selector, IEnumerable<SelectOptionValue> values, PageSelectOptionOptions? options = null)
    {
        return page.SelectOptionAsync(selector, values, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// This method expects <paramref name="selector"/> to point to an <a href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input">input
    /// element</a>.
    /// </para>
    /// <para>
    /// Sets the value of the file input to these file paths or files. If some of the <c>filePaths</c>
    /// are relative paths, then they are resolved relative to the the current working directory.
    /// For empty array, clears the selected files.
    /// </para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="files">
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage SetInputFiles(this IPage page, string selector, string files, PageSetInputFilesOptions? options = null)
    {
        page.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method expects <paramref name="selector"/> to point to an <a href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input">input
    /// element</a>.
    /// </para>
    /// <para>
    /// Sets the value of the file input to these file paths or files. If some of the <c>filePaths</c>
    /// are relative paths, then they are resolved relative to the the current working directory.
    /// For empty array, clears the selected files.
    /// </para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="files">
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage SetInputFiles(this IPage page, string selector, FilePayload files, PageSetInputFilesOptions? options = null)
    {
        page.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method expects <paramref name="selector"/> to point to an <a href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input">input
    /// element</a>.
    /// </para>
    /// <para>
    /// Sets the value of the file input to these file paths or files. If some of the <c>filePaths</c>
    /// are relative paths, then they are resolved relative to the the current working directory.
    /// For empty array, clears the selected files.
    /// </para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="files">
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage SetInputFiles(this IPage page, string selector, IEnumerable<string> files, PageSetInputFilesOptions? options = null)
    {
        page.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method expects <paramref name="selector"/> to point to an <a href="https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input">input
    /// element</a>.
    /// </para>
    /// <para>
    /// Sets the value of the file input to these file paths or files. If some of the <c>filePaths</c>
    /// are relative paths, then they are resolved relative to the the current working directory.
    /// For empty array, clears the selected files.
    /// </para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="files">
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage SetInputFiles(this IPage page, string selector, IEnumerable<FilePayload> files, PageSetInputFilesOptions? options = null)
    {
        page.SetInputFilesAsync(selector, files, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary><para>Returns <c>element.innerHTML</c>.</para></summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static string InnerHTML(this IPage page, string selector, PageInnerHTMLOptions? options = null)
    {
        return page.InnerHTMLAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary><para>Returns <c>element.innerText</c>.</para></summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static string InnerText(this IPage page, string selector, PageInnerTextOptions? options = null)
    {
        return page.InnerTextAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns <c>input.value</c> for the selected <c>&lt;input&gt;</c> or <c>&lt;textarea&gt;</c>
    /// or <c>&lt;select&gt;</c> element. Throws for non-input elements.
    /// </para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static string InputValue(this IPage page, string selector, PageInputValueOptions? options = null)
    {
        return page.InputValueAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary><para>Gets the full HTML contents of the page, including the doctype.</para></summary>
    public static string Content(this IPage page)
    {
        return page.ContentAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns <c>element.textContent</c>.</para></summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static string? TextContent(this IPage page, string selector, PageTextContentOptions? options = null)
    {
        return page.TextContentAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary><para>Returns the page's title. Shortcut for main frame's <see cref="IFrame.TitleAsync"/>.</para></summary>
    public static string Title(this IPage page)
    {
        return page.TitleAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns whether the element is checked. Throws if the element is not a checkbox
    /// or radio input.
    /// </para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static bool IsChecked(this IPage page, string selector, PageIsCheckedOptions? options = null)
    {
        return page.IsCheckedAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary><para>Returns whether the element is disabled, the opposite of <a href="./actionability.md#enabled">enabled</a>.</para></summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static bool IsDisabled(this IPage page, string selector, PageIsDisabledOptions? options = null)
    {
        return page.IsDisabledAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary><para>Returns whether the element is <a href="./actionability.md#editable">editable</a>.</para></summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static bool IsEditable(this IPage page, string selector, PageIsEditableOptions? options = null)
    {
        return page.IsEditableAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary><para>Returns whether the element is <a href="./actionability.md#enabled">enabled</a>.</para></summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static bool IsEnabled(this IPage page, string selector, PageIsEnabledOptions? options = null)
    {
        return page.IsEnabledAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns whether the element is hidden, the opposite of <a href="./actionability.md#visible">visible</a>.
    /// <paramref name="selector"/> that does not match any elements is considered hidden.
    /// </para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static bool IsHidden(this IPage page, string selector, PageIsHiddenOptions? options = null)
    {
        return page.IsHiddenAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns whether the element is <a href="./actionability.md#visible">visible</a>.
    /// <paramref name="selector"/> that does not match any elements is considered not visible.
    /// </para>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static bool IsVisible(this IPage page, string selector, PageIsVisibleOptions? options = null)
    {
        return page.IsVisibleAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary><para>Returns element attribute value.</para></summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="name">Attribute name to get the value for.</param>
    /// <param name="options">Call options</param>
    public static string? GetAttribute(this IPage page, string selector, string name, PageGetAttributeOptions? options = null)
    {
        string? result = null;

        try
        {
            result = page.GetAttributeAsync(selector, name, options).GetAwaiter().GetResult();

        }
        catch
        {
            return result;
        }

        return result;
    }

    /// <summary>
    /// <para>Returns the PDF buffer.</para>
    /// <para>
    /// <c>page.pdf()</c> generates a pdf of the page with <c>print</c> css media. To generate
    /// a pdf with <c>screen</c> media, call <see cref="IPage.EmulateMediaAsync"/> before
    /// calling <c>page.pdf()</c>:
    /// </para>
    /// <code>
    /// // Generates a PDF with 'screen' media type<br/>
    /// await page.EmulateMediaAsync(new PageEmulateMediaOptions { Media = Media.Screen });<br/>
    /// await page.PdfAsync(new PagePdfOptions { Path = "page.pdf" });
    /// </code>
    /// <para>
    /// The <paramref name="width"/>, <paramref name="height"/>, and <paramref name="margin"/>
    /// options accept values labeled with units. Unlabeled values are treated as pixels.
    /// </para>
    /// <para>A few examples:</para>
    /// <list type="bullet">
    /// <item><description><c>page.pdf({width: 100})</c> - prints with width set to 100 pixels</description></item>
    /// <item><description><c>page.pdf({width: '100px'})</c> - prints with width set to 100 pixels</description></item>
    /// <item><description><c>page.pdf({width: '10cm'})</c> - prints with width set to 10 centimeters.</description></item>
    /// </list>
    /// <para>All possible units are:</para>
    /// <list type="bullet">
    /// <item><description><c>px</c> - pixel</description></item>
    /// <item><description><c>in</c> - inch</description></item>
    /// <item><description><c>cm</c> - centimeter</description></item>
    /// <item><description><c>mm</c> - millimeter</description></item>
    /// </list>
    /// <para>The <paramref name="format"/> options are:</para>
    /// <list type="bullet">
    /// <item><description><c>Letter</c>: 8.5in x 11in</description></item>
    /// <item><description><c>Legal</c>: 8.5in x 14in</description></item>
    /// <item><description><c>Tabloid</c>: 11in x 17in</description></item>
    /// <item><description><c>Ledger</c>: 17in x 11in</description></item>
    /// <item><description><c>A0</c>: 33.1in x 46.8in</description></item>
    /// <item><description><c>A1</c>: 23.4in x 33.1in</description></item>
    /// <item><description><c>A2</c>: 16.54in x 23.4in</description></item>
    /// <item><description><c>A3</c>: 11.7in x 16.54in</description></item>
    /// <item><description><c>A4</c>: 8.27in x 11.7in</description></item>
    /// <item><description><c>A5</c>: 5.83in x 8.27in</description></item>
    /// <item><description><c>A6</c>: 4.13in x 5.83in</description></item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>Generating a pdf is currently only supported in Chromium headless.</para>
    /// <para>
    /// By default, <c>page.pdf()</c> generates a pdf with modified colors for printing.
    /// Use the <a href="https://developer.mozilla.org/en-US/docs/Web/CSS/-webkit-print-color-adjust"><c>-webkit-print-color-adjust</c></a>
    /// property to force rendering of exact colors.
    /// </para>
    /// <para>
    /// <paramref name="headerTemplate"/> and <paramref name="footerTemplate"/> markup have
    /// the following limitations: > 1. Script tags inside templates are not evaluated.
    /// > 2. Page styles are not visible inside templates.
    /// </para>
    /// </remarks>
    /// <param name="options">Call options</param>
    public static byte[] Pdf(this IPage page, PagePdfOptions? options = null)
    {
        return page.PdfAsync(options).GetAwaiter().GetResult();
    }

    /// <summary><para>Returns the buffer with the captured screenshot.</para></summary>
    /// <param name="options">Call options</param>
    public static byte[] Screenshot(this IPage page, PageScreenshotOptions? options = null)
    {
        return page.ScreenshotAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>Adds a script which would be evaluated in one of the following scenarios:</para>
    /// <list type="bullet">
    /// <item><description>Whenever the page is navigated.</description></item>
    /// <item><description>
    /// Whenever the child frame is attached or navigated. In this case, the script is evaluated
    /// in the context of the newly attached frame.
    /// </description></item>
    /// </list>
    /// <para>
    /// The script is evaluated after the document was created but before any of its scripts
    /// were run. This is useful to amend the JavaScript environment, e.g. to seed <c>Math.random</c>.
    /// </para>
    /// <para>An example of overriding <c>Math.random</c> before the page loads:</para>
    /// <code>await page.AddInitScriptAsync(new PageAddInitScriptOption { ScriptPath = "./preload.js" });</code>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The order of evaluation of multiple scripts installed via <see cref="IBrowserContext.AddInitScriptAsync"/>
    /// and <see cref="IPage.AddInitScriptAsync"/> is not defined.
    /// </para>
    /// </remarks>
    /// <param name="script">Script to be evaluated in all pages in the browser context.</param>
    /// <param name="scriptPath">Instead of specifying <paramref name="script"/>, gives the file name to load from.</param>
    public static IPage AddInitScript(this IPage page, string? script = null, string? scriptPath = null)
    {
        page.AddInitScriptAsync(script, scriptPath).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Adds a <c>&lt;script&gt;</c> tag into the page with the desired url or content.
    /// Returns the added tag when the script's onload fires or when the script content
    /// was injected into frame.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.AddScriptTagAsync"/>.</para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IPage AddScriptTag(this IPage page, PageAddScriptTagOptions? options = null)
    {
        page.AddScriptTagAsync(options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Adds a <c>&lt;link rel="stylesheet"&gt;</c> tag into the page with the desired url
    /// or a <c>&lt;style type="text/css"&gt;</c> tag with the content. Returns the added
    /// tag when the stylesheet's onload fires or when the CSS content was injected into
    /// frame.
    /// </para>
    /// <para>Shortcut for main frame's <see cref="IFrame.AddStyleTagAsync"/>.</para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IPage AddStyleTag(this IPage page, PageAddStyleTagOptions? options = null)
    {
        page.AddStyleTagAsync(options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary><para>Brings page to front (activates tab).</para></summary>
    public static IPage BringToFront(this IPage page)
    {
        page.BringToFrontAsync().GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// The snippet below dispatches the <c>click</c> event on the element. Regardless of
    /// the visibility state of the element, <c>click</c> is dispatched. This is equivalent
    /// to calling <a href="https://developer.mozilla.org/en-US/docs/Web/API/HTMLElement/click">element.click()</a>.
    /// </para>
    /// <code>await page.DispatchEventAsync("button#submit", "click");</code>
    /// <para>
    /// Under the hood, it creates an instance of an event based on the given <paramref
    /// name="type"/>, initializes it with <paramref name="eventInit"/> properties and dispatches
    /// it on the element. Events are <c>composed</c>, <c>cancelable</c> and bubble by default.
    /// </para>
    /// <para>
    /// Since <paramref name="eventInit"/> is event-specific, please refer to the events
    /// documentation for the lists of initial properties:
    /// </para>
    /// <list type="bullet">
    /// <item><description><a href="https://developer.mozilla.org/en-US/docs/Web/API/DragEvent/DragEvent">DragEvent</a></description></item>
    /// <item><description><a href="https://developer.mozilla.org/en-US/docs/Web/API/FocusEvent/FocusEvent">FocusEvent</a></description></item>
    /// <item><description><a href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/KeyboardEvent">KeyboardEvent</a></description></item>
    /// <item><description><a href="https://developer.mozilla.org/en-US/docs/Web/API/MouseEvent/MouseEvent">MouseEvent</a></description></item>
    /// <item><description><a href="https://developer.mozilla.org/en-US/docs/Web/API/PointerEvent/PointerEvent">PointerEvent</a></description></item>
    /// <item><description><a href="https://developer.mozilla.org/en-US/docs/Web/API/TouchEvent/TouchEvent">TouchEvent</a></description></item>
    /// <item><description><a href="https://developer.mozilla.org/en-US/docs/Web/API/Event/Event">Event</a></description></item>
    /// </list>
    /// <para>
    /// You can also specify <c>JSHandle</c> as the property value if you want live objects
    /// to be passed into the event:
    /// </para>
    /// <code>
    /// var dataTransfer = await page.EvaluateHandleAsync("() =&gt; new DataTransfer()");<br/>
    /// await page.DispatchEventAsync("#source", "dragstart", new { dataTransfer });
    /// </code>
    /// </summary>
    /// <param name="selector">
    /// A selector to search for an element. If there are multiple elements satisfying the
    /// selector, the first will be used. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="type">DOM event type: <c>"click"</c>, <c>"dragstart"</c>, etc.</param>
    /// <param name="eventInit">Optional event-specific initialization properties.</param>
    /// <param name="options">Call options</param>
    public static IPage DispatchEvent(this IPage page, string selector, string type, object? eventInit = null, PageDispatchEventOptions? options = null)
    {
        page.DispatchEventAsync(selector, type, eventInit, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// This method changes the <c>CSS media type</c> through the <c>media</c> argument,
    /// and/or the <c>'prefers-colors-scheme'</c> media feature, using the <c>colorScheme</c>
    /// argument.
    /// </para>
    /// <code>
    /// await page.EvaluateAsync("() =&gt; matchMedia('screen').matches");<br/>
    /// // → true<br/>
    /// await page.EvaluateAsync("() =&gt; matchMedia('print').matches");<br/>
    /// // → false<br/>
    /// <br/>
    /// await page.EmulateMediaAsync(new PageEmulateMediaOptions { Media = Media.Print });<br/>
    /// await page.EvaluateAsync("() =&gt; matchMedia('screen').matches");<br/>
    /// // → false<br/>
    /// await page.EvaluateAsync("() =&gt; matchMedia('print').matches");<br/>
    /// // → true<br/>
    /// <br/>
    /// await page.EmulateMediaAsync(new PageEmulateMediaOptions { Media = Media.Screen });<br/>
    /// await page.EvaluateAsync("() =&gt; matchMedia('screen').matches");<br/>
    /// // → true<br/>
    /// await page.EvaluateAsync("() =&gt; matchMedia('print').matches");<br/>
    /// // → false
    /// </code>
    /// <code>
    /// await page.EmulateMediaAsync(new PageEmulateMediaOptions { ColorScheme = ColorScheme.Dark });<br/>
    /// await page.EvaluateAsync("matchMedia('(prefers-color-scheme: dark)').matches");<br/>
    /// // → true<br/>
    /// await page.EvaluateAsync("matchMedia('(prefers-color-scheme: light)').matches");<br/>
    /// // → false<br/>
    /// await page.EvaluateAsync("matchMedia('(prefers-color-scheme: no-preference)').matches");<br/>
    /// // → false
    /// </code>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IPage EmulateMedia(this IPage page, PageEmulateMediaOptions? options = null)
    {
        page.EmulateMediaAsync(options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// The method finds an element matching the specified selector within the page and
    /// passes it as a first argument to <paramref name="expression"/>. If no elements match
    /// the selector, the method throws an error. Returns the value of <paramref name="expression"/>.
    /// </para>
    /// <para>
    /// If <paramref name="expression"/> returns a <see cref="Task"/>, then <see cref="IPage.EvalOnSelectorAsync"/>
    /// would wait for the promise to resolve and return its value.
    /// </para>
    /// <para>Examples:</para>
    /// <code>
    /// var searchValue = await page.EvalOnSelectorAsync&lt;string&gt;("#search", "el =&gt; el.value");<br/>
    /// var preloadHref = await page.EvalOnSelectorAsync&lt;string&gt;("link[rel=preload]", "el =&gt; el.href");<br/>
    /// var html = await page.EvalOnSelectorAsync(".main-container", "(e, suffix) =&gt; e.outerHTML + suffix", "hello");
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.EvalOnSelectorAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method does not wait for the element to pass actionability checks and therefore
    /// can lead to the flaky tests. Use <see cref="ILocator.EvaluateAsync"/>, other <see
    /// cref="ILocator"/> helper methods or web-first assertions instead.
    /// </para>
    /// </remarks>
    /// <param name="selector">
    /// A selector to query for. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    /// <param name="options">Call options</param>
    public static T EvalOnSelector<T>(this IPage page, string selector, string expression, object? arg = null, PageEvalOnSelectorOptions? options = null)
    {
        var result = page.EvalOnSelectorAsync<T>(selector, expression, arg, options).GetAwaiter().GetResult();
        return result;
    }

    /// <summary>
    /// <para>
    /// The method finds all elements matching the specified selector within the page and
    /// passes an array of matched elements as a first argument to <paramref name="expression"/>.
    /// Returns the result of <paramref name="expression"/> invocation.
    /// </para>
    /// <para>
    /// If <paramref name="expression"/> returns a <see cref="Task"/>, then <see cref="IPage.EvalOnSelectorAllAsync"/>
    /// would wait for the promise to resolve and return its value.
    /// </para>
    /// <para>Examples:</para>
    /// <code>var divsCount = await page.EvalOnSelectorAllAsync&lt;bool&gt;("div", "(divs, min) =&gt; divs.length &gt;= min", 10);</code>
    /// </summary>
    /// <remarks>
    /// <para>
    /// In most cases, <see cref="ILocator.EvaluateAllAsync"/>, other <see cref="ILocator"/>
    /// helper methods and web-first assertions do a better job.
    /// </para>
    /// </remarks>
    /// <param name="selector">
    /// A selector to query for. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    public static T EvalOnSelectorAll<T>(this IPage page, string selector, string expression, object? arg = null)
    {
        var result = page.EvalOnSelectorAllAsync<T>(selector, expression, arg).GetAwaiter().GetResult();
        return result;
    }

    /// <summary>
    /// <para>Returns the value of the <paramref name="expression"/> invocation.</para>
    /// <para>
    /// If the function passed to the <see cref="IPage.EvaluateAsync"/> returns a <see cref="Task"/>,
    /// then <see cref="IPage.EvaluateAsync"/> would wait for the promise to resolve and
    /// return its value.
    /// </para>
    /// <para>
    /// If the function passed to the <see cref="IPage.EvaluateAsync"/> returns a non-<see
    /// cref="Serializable"/> value, then <see cref="IPage.EvaluateAsync"/> resolves to
    /// <c>undefined</c>. Playwright also supports transferring some additional values that
    /// are not serializable by <c>JSON</c>: <c>-0</c>, <c>NaN</c>, <c>Infinity</c>, <c>-Infinity</c>.
    /// </para>
    /// <para>Passing argument to <paramref name="expression"/>:</para>
    /// <code>
    /// var result = await page.EvaluateAsync&lt;int&gt;("([x, y]) =&gt; Promise.resolve(x * y)", new[] { 7, 8 });<br/>
    /// Console.WriteLine(result);
    /// </code>
    /// <para>A string can also be passed in instead of a function:</para>
    /// <code>Console.WriteLine(await page.EvaluateAsync&lt;int&gt;("1 + 2")); // prints "3"</code>
    /// <para>
    /// <see cref="IElementHandle"/> instances can be passed as an argument to the <see
    /// cref="IPage.EvaluateAsync"/>:
    /// </para>
    /// <code>
    /// var bodyHandle = await page.EvaluateAsync("document.body");<br/>
    /// var html = await page.EvaluateAsync&lt;string&gt;("([body, suffix]) =&gt; body.innerHTML + suffix", new object [] { bodyHandle, "hello" });<br/>
    /// await bodyHandle.DisposeAsync();
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.EvaluateAsync"/>.</para>
    /// </summary>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    public static T Evaluate<T>(this IPage page, string expression, object? arg = null)
    {
        var result = page.EvaluateAsync<T>(expression, arg).GetAwaiter().GetResult();
        return result;
    }

    /// <summary>
    /// <para>Returns the value of the <paramref name="expression"/> invocation as a <see cref="IJSHandle"/>.</para>
    /// <para>
    /// The only difference between <see cref="IPage.EvaluateAsync"/> and <see cref="IPage.EvaluateHandleAsync"/>
    /// is that <see cref="IPage.EvaluateHandleAsync"/> returns <see cref="IJSHandle"/>.
    /// </para>
    /// <para>
    /// If the function passed to the <see cref="IPage.EvaluateHandleAsync"/> returns a
    /// <see cref="Task"/>, then <see cref="IPage.EvaluateHandleAsync"/> would wait for
    /// the promise to resolve and return its value.
    /// </para>
    /// <code>
    /// // Handle for the window object.<br/>
    /// var aWindowHandle = await page.EvaluateHandleAsync("() =&gt; Promise.resolve(window)");
    /// </code>
    /// <para>A string can also be passed in instead of a function:</para>
    /// <code>var docHandle = await page.EvalueHandleAsync("document"); // Handle for the `document`</code>
    /// <para><see cref="IJSHandle"/> instances can be passed as an argument to the <see cref="IPage.EvaluateHandleAsync"/>:</para>
    /// <code>
    /// var handle = await page.EvaluateHandleAsync("() =&gt; document.body");<br/>
    /// var resultHandle = await page.EvaluateHandleAsync("([body, suffix]) =&gt; body.innerHTML + suffix", new object[] { handle, "hello" });<br/>
    /// Console.WriteLine(await resultHandle.JsonValueAsync&lt;string&gt;());<br/>
    /// await resultHandle.DisposeAsync();
    /// </code>
    /// </summary>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    public static IPage EvaluateHandle(this IPage page, string expression, object? arg = null)
    {
        page.EvaluateHandleAsync(expression, arg).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// The method adds a function called <paramref name="name"/> on the <c>window</c> object
    /// of every frame in this page. When called, the function executes <paramref name="callback"/>
    /// and returns a <see cref="Task"/> which resolves to the return value of <paramref
    /// name="callback"/>. If the <paramref name="callback"/> returns a <see cref="Promise"/>,
    /// it will be awaited.
    /// </para>
    /// <para>
    /// The first argument of the <paramref name="callback"/> function contains information
    /// about the caller: <c>{ browserContext: BrowserContext, page: Page, frame: Frame
    /// }</c>.
    /// </para>
    /// <para>See <see cref="IBrowserContext.ExposeBindingAsync"/> for the context-wide version.</para>
    /// <para>An example of exposing page URL to all frames in a page:</para>
    /// <code>
    /// using Microsoft.Playwright;<br/>
    /// using System.Threading.Tasks;<br/>
    /// <br/>
    /// class PageExamples<br/>
    /// {<br/>
    ///     public static async Task Main()<br/>
    ///     {<br/>
    ///         using var playwright = await Playwright.CreateAsync();<br/>
    ///         await using var browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions<br/>
    ///         {<br/>
    ///             Headless: false<br/>
    ///         });<br/>
    ///         var page = await browser.NewPageAsync();<br/>
    /// <br/>
    ///         await page.ExposeBindingAsync("pageUrl", (source) =&gt; source.Page.Url);<br/>
    ///         await page.SetContentAsync("&lt;script&gt;\n" +<br/>
    ///         "  async function onClick() {\n" +<br/>
    ///         "    document.querySelector('div').textContent = await window.pageURL();\n" +<br/>
    ///         "  }\n" +<br/>
    ///         "&lt;/script&gt;\n" +<br/>
    ///         "&lt;button onclick=\"onClick()\"&gt;Click me&lt;/button&gt;\n" +<br/>
    ///         "&lt;div&gt;&lt;/div&gt;");<br/>
    /// <br/>
    ///         await page.ClickAsync("button");<br/>
    ///     }<br/>
    /// }
    /// </code>
    /// <para>An example of passing an element handle:</para>
    /// <code>
    /// var result = new TaskCompletionSource&lt;string&gt;();<br/>
    /// await page.ExposeBindingAsync("clicked", async (BindingSource _, IJSHandle t) =&gt;<br/>
    /// {<br/>
    ///     return result.TrySetResult(await t.AsElement().TextContentAsync());<br/>
    /// });<br/>
    /// <br/>
    /// await page.SetContentAsync("&lt;script&gt;\n" +<br/>
    ///   "  document.addEventListener('click', event =&gt; window.clicked(event.target));\n" +<br/>
    ///   "&lt;/script&gt;\n" +<br/>
    ///   "&lt;div&gt;Click me&lt;/div&gt;\n" +<br/>
    ///   "&lt;div&gt;Or click me&lt;/div&gt;\n");<br/>
    /// <br/>
    /// await page.ClickAsync("div");<br/>
    /// Console.WriteLine(await result.Task);
    /// </code>
    /// </summary>
    /// <remarks><para>Functions installed via <see cref="IPage.ExposeBindingAsync"/> survive navigations.</para></remarks>
    /// <param name="name">Name of the function on the window object.</param>
    /// <param name="callback">Callback function that will be called in the Playwright's context.</param>
    /// <param name="options">Call options</param>
    public static IPage ExposeBinding(this IPage page, string name, Action callback, PageExposeBindingOptions? options = null)
    {
        page.ExposeBindingAsync(name, callback, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// The method adds a function called <paramref name="name"/> on the <c>window</c> object
    /// of every frame in the page. When called, the function executes <paramref name="callback"/>
    /// and returns a <see cref="Task"/> which resolves to the return value of <paramref
    /// name="callback"/>.
    /// </para>
    /// <para>If the <paramref name="callback"/> returns a <see cref="Task"/>, it will be awaited.</para>
    /// <para>See <see cref="IBrowserContext.ExposeFunctionAsync"/> for context-wide exposed function.</para>
    /// <para>An example of adding a <c>sha256</c> function to the page:</para>
    /// <code>
    /// using Microsoft.Playwright;<br/>
    /// using System;<br/>
    /// using System.Security.Cryptography;<br/>
    /// using System.Threading.Tasks;<br/>
    /// <br/>
    /// class PageExamples<br/>
    /// {<br/>
    ///     public static async Task Main()<br/>
    ///     {<br/>
    ///         using var playwright = await Playwright.CreateAsync();<br/>
    ///         await using var browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions<br/>
    ///         {<br/>
    ///             Headless: false<br/>
    ///         });<br/>
    ///         var page = await browser.NewPageAsync();<br/>
    /// <br/>
    ///         await page.ExposeFunctionAsync("sha256", (string input) =&gt;<br/>
    ///         {<br/>
    ///             return Convert.ToBase64String(<br/>
    ///                 SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(input)));<br/>
    ///         });<br/>
    /// <br/>
    ///         await page.SetContentAsync("&lt;script&gt;\n" +<br/>
    ///         "  async function onClick() {\n" +<br/>
    ///         "    document.querySelector('div').textContent = await window.sha256('PLAYWRIGHT');\n" +<br/>
    ///         "  }\n" +<br/>
    ///         "&lt;/script&gt;\n" +<br/>
    ///         "&lt;button onclick=\"onClick()\"&gt;Click me&lt;/button&gt;\n" +<br/>
    ///         "&lt;div&gt;&lt;/div&gt;");<br/>
    /// <br/>
    ///         await page.ClickAsync("button");<br/>
    ///         Console.WriteLine(await page.TextContentAsync("div"));<br/>
    ///     }<br/>
    /// }
    /// </code>
    /// </summary>
    /// <remarks><para>Functions installed via <see cref="IPage.ExposeFunctionAsync"/> survive navigations.</para></remarks>
    /// <param name="name">Name of the function on the window object</param>
    /// <param name="callback">Callback function which will be called in Playwright's context.</param>
    public static IPage ExposeFunction(this IPage page, string name, Action callback)
    {
        page.ExposeFunctionAsync(name, callback).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Returns the opener for popup pages and <c>null</c> for others. If the opener has
    /// been closed already the returns <c>null</c>.
    /// </para>
    /// </summary>
    public static IPage? Opener(this IPage page)
    {
        return page.OpenerAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Pauses script execution. Playwright will stop executing the script and wait for
    /// the user to either press 'Resume' button in the page overlay or to call <c>playwright.resume()</c>
    /// in the DevTools console.
    /// </para>
    /// <para>
    /// User can inspect selectors or perform manual steps while paused. Resume will continue
    /// running the original script from the place it was paused.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method requires Playwright to be started in a headed mode, with a falsy <paramref
    /// name="headless"/> value in the <see cref="IBrowserType.LaunchAsync"/>.
    /// </para>
    /// </remarks>
    public static IPage Pause(this IPage page)
    {
        page.PauseAsync().GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>Routing provides the capability to modify network requests that are made by a page.</para>
    /// <para>
    /// Once routing is enabled, every request matching the url pattern will stall unless
    /// it's continued, fulfilled or aborted.
    /// </para>
    /// <para>An example of a naive handler that aborts all image requests:</para>
    /// <code>
    /// var page = await browser.NewPageAsync();<br/>
    /// await page.RouteAsync("**/*.{png,jpg,jpeg}", async r =&gt; await r.AbortAsync());<br/>
    /// await page.GotoAsync("https://www.microsoft.com");
    /// </code>
    /// <para>or the same snippet using a regex pattern instead:</para>
    /// <code>
    /// var page = await browser.NewPageAsync();<br/>
    /// await page.RouteAsync(new Regex("(\\.png$)|(\\.jpg$)"), async r =&gt; await r.AbortAsync());<br/>
    /// await page.GotoAsync("https://www.microsoft.com");
    /// </code>
    /// <para>
    /// It is possible to examine the request to decide the route action. For example, mocking
    /// all requests that contain some post data, and leaving all other requests as is:
    /// </para>
    /// <code>
    /// await page.RouteAsync("/api/**", async r =&gt;<br/>
    /// {<br/>
    ///   if (r.Request.PostData.Contains("my-string"))<br/>
    ///       await r.FulfillAsync(new RouteFulfillOptions { Body = "mocked-data" });<br/>
    ///   else<br/>
    ///       await r.ContinueAsync();<br/>
    /// });
    /// </code>
    /// <para>
    /// Page routes take precedence over browser context routes (set up with <see cref="IBrowserContext.RouteAsync"/>)
    /// when request matches both handlers.
    /// </para>
    /// <para>To remove a route with its handler you can use <see cref="IPage.UnrouteAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>The handler will only be called for the first url if the response is a redirect.</para>
    /// <para>
    /// <see cref="IPage.RouteAsync"/> will not intercept requests intercepted by Service
    /// Worker. See <a href="https://github.com/microsoft/playwright/issues/1090">this</a>
    /// issue. We recommend disabling Service Workers when using request interception. Via
    /// <c>await context.addInitScript(() =&gt; delete window.navigator.serviceWorker);</c>
    /// </para>
    /// <para>Enabling routing disables http cache.</para>
    /// </remarks>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while routing. When a <paramref name="baseURL"/> via the context options was provided
    /// and the passed URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="handler">handler function to route the request.</param>
    /// <param name="options">Call options</param>
    public static IPage Route(this IPage page, string url, Action<IRoute> handler, PageRouteOptions? options = null)
    {
        page.RouteAsync(url, handler, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>Routing provides the capability to modify network requests that are made by a page.</para>
    /// <para>
    /// Once routing is enabled, every request matching the url pattern will stall unless
    /// it's continued, fulfilled or aborted.
    /// </para>
    /// <para>An example of a naive handler that aborts all image requests:</para>
    /// <code>
    /// var page = await browser.NewPageAsync();<br/>
    /// await page.RouteAsync("**/*.{png,jpg,jpeg}", async r =&gt; await r.AbortAsync());<br/>
    /// await page.GotoAsync("https://www.microsoft.com");
    /// </code>
    /// <para>or the same snippet using a regex pattern instead:</para>
    /// <code>
    /// var page = await browser.NewPageAsync();<br/>
    /// await page.RouteAsync(new Regex("(\\.png$)|(\\.jpg$)"), async r =&gt; await r.AbortAsync());<br/>
    /// await page.GotoAsync("https://www.microsoft.com");
    /// </code>
    /// <para>
    /// It is possible to examine the request to decide the route action. For example, mocking
    /// all requests that contain some post data, and leaving all other requests as is:
    /// </para>
    /// <code>
    /// await page.RouteAsync("/api/**", async r =&gt;<br/>
    /// {<br/>
    ///   if (r.Request.PostData.Contains("my-string"))<br/>
    ///       await r.FulfillAsync(new RouteFulfillOptions { Body = "mocked-data" });<br/>
    ///   else<br/>
    ///       await r.ContinueAsync();<br/>
    /// });
    /// </code>
    /// <para>
    /// Page routes take precedence over browser context routes (set up with <see cref="IBrowserContext.RouteAsync"/>)
    /// when request matches both handlers.
    /// </para>
    /// <para>To remove a route with its handler you can use <see cref="IPage.UnrouteAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>The handler will only be called for the first url if the response is a redirect.</para>
    /// <para>
    /// <see cref="IPage.RouteAsync"/> will not intercept requests intercepted by Service
    /// Worker. See <a href="https://github.com/microsoft/playwright/issues/1090">this</a>
    /// issue. We recommend disabling Service Workers when using request interception. Via
    /// <c>await context.addInitScript(() =&gt; delete window.navigator.serviceWorker);</c>
    /// </para>
    /// <para>Enabling routing disables http cache.</para>
    /// </remarks>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while routing. When a <paramref name="baseURL"/> via the context options was provided
    /// and the passed URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="handler">handler function to route the request.</param>
    /// <param name="options">Call options</param>
    public static IPage Route(this IPage page, Regex url, Action<IRoute> handler, PageRouteOptions? options = null)
    {
        page.RouteAsync(url, handler, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>Routing provides the capability to modify network requests that are made by a page.</para>
    /// <para>
    /// Once routing is enabled, every request matching the url pattern will stall unless
    /// it's continued, fulfilled or aborted.
    /// </para>
    /// <para>An example of a naive handler that aborts all image requests:</para>
    /// <code>
    /// var page = await browser.NewPageAsync();<br/>
    /// await page.RouteAsync("**/*.{png,jpg,jpeg}", async r =&gt; await r.AbortAsync());<br/>
    /// await page.GotoAsync("https://www.microsoft.com");
    /// </code>
    /// <para>or the same snippet using a regex pattern instead:</para>
    /// <code>
    /// var page = await browser.NewPageAsync();<br/>
    /// await page.RouteAsync(new Regex("(\\.png$)|(\\.jpg$)"), async r =&gt; await r.AbortAsync());<br/>
    /// await page.GotoAsync("https://www.microsoft.com");
    /// </code>
    /// <para>
    /// It is possible to examine the request to decide the route action. For example, mocking
    /// all requests that contain some post data, and leaving all other requests as is:
    /// </para>
    /// <code>
    /// await page.RouteAsync("/api/**", async r =&gt;<br/>
    /// {<br/>
    ///   if (r.Request.PostData.Contains("my-string"))<br/>
    ///       await r.FulfillAsync(new RouteFulfillOptions { Body = "mocked-data" });<br/>
    ///   else<br/>
    ///       await r.ContinueAsync();<br/>
    /// });
    /// </code>
    /// <para>
    /// Page routes take precedence over browser context routes (set up with <see cref="IBrowserContext.RouteAsync"/>)
    /// when request matches both handlers.
    /// </para>
    /// <para>To remove a route with its handler you can use <see cref="IPage.UnrouteAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>The handler will only be called for the first url if the response is a redirect.</para>
    /// <para>
    /// <see cref="IPage.RouteAsync"/> will not intercept requests intercepted by Service
    /// Worker. See <a href="https://github.com/microsoft/playwright/issues/1090">this</a>
    /// issue. We recommend disabling Service Workers when using request interception. Via
    /// <c>await context.addInitScript(() =&gt; delete window.navigator.serviceWorker);</c>
    /// </para>
    /// <para>Enabling routing disables http cache.</para>
    /// </remarks>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while routing. When a <paramref name="baseURL"/> via the context options was provided
    /// and the passed URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="handler">handler function to route the request.</param>
    /// <param name="options">Call options</param>
    public static IPage Route(this IPage page, Func<string, bool> url, Action<IRoute> handler, PageRouteOptions? options = null)
    {
        page.RouteAsync(url, handler, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Removes a route created with <see cref="IPage.RouteAsync"/>. When <paramref name="handler"/>
    /// is not specified, removes all routes for the <paramref name="url"/>.
    /// </para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while routing.
    /// </param>
    /// <param name="handler">Optional handler function to route the request.</param>
    public static IPage Unroute(this IPage page, string url, Action<IRoute>? handler = null)
    {
        page.UnrouteAsync(url, handler).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Removes a route created with <see cref="IPage.RouteAsync"/>. When <paramref name="handler"/>
    /// is not specified, removes all routes for the <paramref name="url"/>.
    /// </para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while routing.
    /// </param>
    /// <param name="handler">Optional handler function to route the request.</param>
    public static IPage Unroute(this IPage page, Regex url, Action<IRoute>? handler = null)
    {
        page.UnrouteAsync(url, handler).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Removes a route created with <see cref="IPage.RouteAsync"/>. When <paramref name="handler"/>
    /// is not specified, removes all routes for the <paramref name="url"/>.
    /// </para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while routing.
    /// </param>
    /// <param name="handler">Optional handler function to route the request.</param>
    public static IPage Unroute(this IPage page, Func<string, bool> url, Action<IRoute>? handler = null)
    {
        page.UnrouteAsync(url, handler).GetAwaiter().GetResult();
        return page;
    }

    /// <param name="html">HTML markup to assign to the page.</param>
    /// <param name="options">Call options</param>
    public static IPage SetContent(this IPage page, string html, PageSetContentOptions? options = null)
    {
        page.SetContentAsync(html, options);
        return page;
    }

    /// <summary><para>The extra HTTP headers will be sent with every request the page initiates.</para></summary>
    /// <remarks>
    /// <para>
    /// <see cref="IPage.SetExtraHTTPHeadersAsync"/> does not guarantee the order of headers
    /// in the outgoing requests.
    /// </para>
    /// </remarks>
    /// <param name="headers">
    /// An object containing additional HTTP headers to be sent with every request. All
    /// header values must be strings.
    /// </param>
    public static IPage SetExtraHTTPHeaders(this IPage page, IEnumerable<KeyValuePair<string, string>> headers)
    {
        page.SetExtraHTTPHeadersAsync(headers).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// In the case of multiple pages in a single browser, each page can have its own viewport
    /// size. However, <see cref="IBrowser.NewContextAsync"/> allows to set viewport size
    /// (and more) for all pages in the context at once.
    /// </para>
    /// <para>
    /// <c>page.setViewportSize</c> will resize the page. A lot of websites don't expect
    /// phones to change size, so you should set the viewport size before navigating to
    /// the page. <see cref="IPage.SetViewportSizeAsync"/> will also reset <c>screen</c>
    /// size, use <see cref="IBrowser.NewContextAsync"/> with <c>screen</c> and <c>viewport</c>
    /// parameters if you need better control of these properties.
    /// </para>
    /// <code>
    /// var page = await browser.NewPageAsync();<br/>
    /// await page.SetViewportSizeAsync(640, 480);<br/>
    /// await page.GotoAsync("https://www.microsoft.com");
    /// </code>
    /// </summary>
    /// <param name="width">
    /// </param>
    /// <param name="height">
    /// </param>
    public static IPage SetViewportSize(this IPage page, int width, int height)
    {
        page.SetViewportSizeAsync(width, height).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a <see cref="IConsoleMessage"/> to be logged by in
    /// the page. If predicate is provided, it passes <see cref="IConsoleMessage"/> value
    /// into the <c>predicate</c> function and waits for <c>predicate(message)</c> to return
    /// a truthy value. Will throw an error if the page is closed before the <see cref="IPage.Console"/>
    /// event is fired.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IConsoleMessage WaitForConsoleMessage(this IPage page, PageWaitForConsoleMessageOptions? options = null)
    {
        return page.WaitForConsoleMessageAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a <see cref="IConsoleMessage"/> to be logged by in
    /// the page. If predicate is provided, it passes <see cref="IConsoleMessage"/> value
    /// into the <c>predicate</c> function and waits for <c>predicate(message)</c> to return
    /// a truthy value. Will throw an error if the page is closed before the <see cref="IPage.Console"/>
    /// event is fired.
    /// </para>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IConsoleMessage RunAndWaitForConsoleMessage(this IPage page, Func<Task> action, PageRunAndWaitForConsoleMessageOptions? options = null)
    {
        return page.RunAndWaitForConsoleMessageAsync(action, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IDownload"/>. If predicate is provided,
    /// it passes <see cref="IDownload"/> value into the <c>predicate</c> function and waits
    /// for <c>predicate(download)</c> to return a truthy value. Will throw an error if
    /// the page is closed before the download event is fired.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IDownload WaitForDownload(this IPage page, PageWaitForDownloadOptions? options = null)
    {
        return page.WaitForDownloadAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IDownload"/>. If predicate is provided,
    /// it passes <see cref="IDownload"/> value into the <c>predicate</c> function and waits
    /// for <c>predicate(download)</c> to return a truthy value. Will throw an error if
    /// the page is closed before the download event is fired.
    /// </para>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IDownload RunAndWaitForDownload(this IPage page, Func<Task> action, PageRunAndWaitForDownloadOptions? options = null)
    {
        return page.RunAndWaitForDownloadAsync(action, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IFileChooser"/> to be created. If
    /// predicate is provided, it passes <see cref="IFileChooser"/> value into the <c>predicate</c>
    /// function and waits for <c>predicate(fileChooser)</c> to return a truthy value. Will
    /// throw an error if the page is closed before the file chooser is opened.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IFileChooser WaitForFileChooser(this IPage page, PageWaitForFileChooserOptions? options = null)
    {
        return page.WaitForFileChooserAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IFileChooser"/> to be created. If
    /// predicate is provided, it passes <see cref="IFileChooser"/> value into the <c>predicate</c>
    /// function and waits for <c>predicate(fileChooser)</c> to return a truthy value. Will
    /// throw an error if the page is closed before the file chooser is opened.
    /// </para>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IFileChooser RunAndWaitForFileChooser(this IPage page, Func<Task> action, PageRunAndWaitForFileChooserOptions? options = null)
    {
        return page.RunAndWaitForFileChooserAsync(action, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns when the <paramref name="expression"/> returns a truthy value. It resolves
    /// to a JSHandle of the truthy value.
    /// </para>
    /// <para>
    /// The <see cref="IPage.WaitForFunctionAsync"/> can be used to observe viewport size
    /// change:
    /// </para>
    /// <code>
    /// using Microsoft.Playwright;<br/>
    /// using System.Threading.Tasks;<br/>
    /// <br/>
    /// class FrameExamples<br/>
    /// {<br/>
    ///   public static async Task WaitForFunction()<br/>
    ///   {<br/>
    ///     using var playwright = await Playwright.CreateAsync();<br/>
    ///     await using var browser = await playwright.Webkit.LaunchAsync();<br/>
    ///     var page = await browser.NewPageAsync();<br/>
    ///     await page.SetViewportSizeAsync(50, 50);<br/>
    ///     await page.MainFrame.WaitForFunctionAsync("window.innerWidth &lt; 100");<br/>
    ///   }<br/>
    /// }
    /// </code>
    /// <para>
    /// To pass an argument to the predicate of <see cref="IPage.WaitForFunctionAsync"/>
    /// function:
    /// </para>
    /// <code>
    /// var selector = ".foo";<br/>
    /// await page.WaitForFunctionAsync("selector =&gt; !!document.querySelector(selector)", selector);
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.WaitForFunctionAsync"/>.</para>
    /// </summary>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    /// <param name="options">Call options</param>
    public static IJSHandle WaitForFunction(this IPage page, string expression, object? arg = null, PageWaitForFunctionOptions? options = null)
    {
        return page.WaitForFunctionAsync(expression, arg, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>Returns when the required load state has been reached.</para>
    /// <para>
    /// This resolves when the page reaches a required load state, <c>load</c> by default.
    /// The navigation must have been committed when this method is called. If current document
    /// has already reached the required state, resolves immediately.
    /// </para>
    /// <code>
    /// await page.ClickAsync("button"); // Click triggers navigation.<br/>
    /// await page.WaitForLoadStateAsync(); // The promise resolves after 'load' event.
    /// </code>
    /// <code>
    /// var popup = await page.RunAndWaitForPopupAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button"); // click triggers the popup/<br/>
    /// });<br/>
    /// await popup.WaitForLoadStateAsync(LoadState.DOMContentLoaded);<br/>
    /// Console.WriteLine(await popup.TitleAsync()); // popup is ready to use.
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.WaitForLoadStateAsync"/>.</para>
    /// </summary>
    /// <param name="state">
    /// Optional load state to wait for, defaults to <c>load</c>. If the state has been
    /// already reached while loading current document, the method resolves immediately.
    /// Can be one of:
    /// <list type="bullet">
    /// <item><description><c>'load'</c> - wait for the <c>load</c> event to be fired.</description></item>
    /// <item><description><c>'domcontentloaded'</c> - wait for the <c>DOMContentLoaded</c> event to be fired.</description></item>
    /// <item><description>
    /// <c>'networkidle'</c> - wait until there are no network connections for at least
    /// <c>500</c> ms.
    /// </description></item>
    /// </list>
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage WaitForLoadState(this IPage page, LoadState? state = null, PageWaitForLoadStateOptions? options = null)
    {
        page.WaitForLoadStateAsync(state, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Waits for the main frame navigation and returns the main resource response. In case
    /// of multiple redirects, the navigation will resolve with the response of the last
    /// redirect. In case of navigation to a different anchor or navigation due to History
    /// API usage, the navigation will resolve with <c>null</c>.
    /// </para>
    /// <para>
    /// This resolves when the page navigates to a new URL or reloads. It is useful for
    /// when you run code which will indirectly cause the page to navigate. e.g. The click
    /// target has an <c>onclick</c> handler that triggers navigation from a <c>setTimeout</c>.
    /// Consider this example:
    /// </para>
    /// <code>
    /// await page.RunAndWaitForNavigationAsync(async () =&gt;<br/>
    /// {<br/>
    ///     // Clicking the link will indirectly cause a navigation.<br/>
    ///     await page.ClickAsync("a.delayed-navigation");<br/>
    /// });<br/>
    /// <br/>
    /// // The method continues after navigation has finished
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.RunAndWaitForNavigationAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Usage of the <a href="https://developer.mozilla.org/en-US/docs/Web/API/History_API">History
    /// API</a> to change the URL is considered a navigation.
    /// </para>
    /// </remarks>
    /// <param name="options">Call options</param>
    public static IResponse? WaitForNavigation(this IPage page, PageWaitForNavigationOptions? options = null)
    {
        return page.WaitForNavigationAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Waits for the main frame navigation and returns the main resource response. In case
    /// of multiple redirects, the navigation will resolve with the response of the last
    /// redirect. In case of navigation to a different anchor or navigation due to History
    /// API usage, the navigation will resolve with <c>null</c>.
    /// </para>
    /// <para>
    /// This resolves when the page navigates to a new URL or reloads. It is useful for
    /// when you run code which will indirectly cause the page to navigate. e.g. The click
    /// target has an <c>onclick</c> handler that triggers navigation from a <c>setTimeout</c>.
    /// Consider this example:
    /// </para>
    /// <code>
    /// await page.RunAndWaitForNavigationAsync(async () =&gt;<br/>
    /// {<br/>
    ///     // Clicking the link will indirectly cause a navigation.<br/>
    ///     await page.ClickAsync("a.delayed-navigation");<br/>
    /// });<br/>
    /// <br/>
    /// // The method continues after navigation has finished
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.RunAndWaitForNavigationAsync"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Usage of the <a href="https://developer.mozilla.org/en-US/docs/Web/API/History_API">History
    /// API</a> to change the URL is considered a navigation.
    /// </para>
    /// </remarks>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IResponse? RunAndWaitForNavigation(this IPage page, Func<Task> action, PageRunAndWaitForNavigationOptions? options = null)
    {
        return page.RunAndWaitForNavigationAsync(action, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a popup <see cref="IPage"/>. If predicate is provided,
    /// it passes <see cref="Popup"/> value into the <c>predicate</c> function and waits
    /// for <c>predicate(page)</c> to return a truthy value. Will throw an error if the
    /// page is closed before the popup event is fired.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IPage WaitForPopup(this IPage page, PageWaitForPopupOptions? options = null)
    {
        return page.WaitForPopupAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a popup <see cref="IPage"/>. If predicate is provided,
    /// it passes <see cref="Popup"/> value into the <c>predicate</c> function and waits
    /// for <c>predicate(page)</c> to return a truthy value. Will throw an error if the
    /// page is closed before the popup event is fired.
    /// </para>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IPage RunAndWaitForPopup(this IPage page, Func<Task> action, PageRunAndWaitForPopupOptions? options = null)
    {
        return page.RunAndWaitForPopupAsync(action, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Waits for the matching request and returns it. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next request with the specified url.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, request =&gt; request.Url == "https://example.com" &amp;&amp; request.Method == "GET");
    /// </code>
    /// </summary>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IRequest"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IRequest WaitForRequest(this IPage page, string urlOrPredicate, PageWaitForRequestOptions? options = null)
    {
        return page.WaitForRequestAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Waits for the matching request and returns it. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next request with the specified url.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, request =&gt; request.Url == "https://example.com" &amp;&amp; request.Method == "GET");
    /// </code>
    /// </summary>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IRequest"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IRequest WaitForRequest(this IPage page, Regex urlOrPredicate, PageWaitForRequestOptions? options = null)
    {
        return page.WaitForRequestAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Waits for the matching request and returns it. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next request with the specified url.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, request =&gt; request.Url == "https://example.com" &amp;&amp; request.Method == "GET");
    /// </code>
    /// </summary>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IRequest"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IRequest WaitForRequest(this IPage page, Func<IRequest, bool> urlOrPredicate, PageWaitForRequestOptions? options = null)
    {
        return page.WaitForRequestAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Waits for the matching request and returns it. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next request with the specified url.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, request =&gt; request.Url == "https://example.com" &amp;&amp; request.Method == "GET");
    /// </code>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IRequest"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IRequest RunAndWaitForRequest(this IPage page, Func<Task> action, string urlOrPredicate, PageRunAndWaitForRequestOptions? options = null)
    {
        return page.RunAndWaitForRequestAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Waits for the matching request and returns it. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next request with the specified url.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, request =&gt; request.Url == "https://example.com" &amp;&amp; request.Method == "GET");
    /// </code>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IRequest"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IRequest RunAndWaitForRequest(this IPage page, Func<Task> action, Regex urlOrPredicate, PageRunAndWaitForRequestOptions? options = null)
    {
        return page.RunAndWaitForRequestAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Waits for the matching request and returns it. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next request with the specified url.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForRequestAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, request =&gt; request.Url == "https://example.com" &amp;&amp; request.Method == "GET");
    /// </code>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IRequest"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IRequest RunAndWaitForRequest(this IPage page, Func<Task> action, Func<IRequest, bool> urlOrPredicate, PageRunAndWaitForRequestOptions? options = null)
    {
        return page.RunAndWaitForRequestAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a <see cref="IRequest"/> to finish loading. If predicate
    /// is provided, it passes <see cref="IRequest"/> value into the <c>predicate</c> function
    /// and waits for <c>predicate(request)</c> to return a truthy value. Will throw an
    /// error if the page is closed before the <see cref="IPage.RequestFinished"/> event
    /// is fired.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IRequest WaitForRequestFinished(this IPage page, PageWaitForRequestFinishedOptions? options = null)
    {
        return page.WaitForRequestFinishedAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a <see cref="IRequest"/> to finish loading. If predicate
    /// is provided, it passes <see cref="IRequest"/> value into the <c>predicate</c> function
    /// and waits for <c>predicate(request)</c> to return a truthy value. Will throw an
    /// error if the page is closed before the <see cref="IPage.RequestFinished"/> event
    /// is fired.
    /// </para>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IRequest RunAndWaitForRequestFinished(this IPage page, Func<Task> action, PageRunAndWaitForRequestFinishedOptions? options = null)
    {
        return page.RunAndWaitForRequestFinishedAsync(action, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns the matched response. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next response with the specified url.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button.triggers-response");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, response =&gt; response.Url == "https://example.com" &amp;&amp; response.Status == 200);
    /// </code>
    /// </summary>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IResponse"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IResponse WaitForResponse(this IPage page, string urlOrPredicate, PageWaitForResponseOptions? options = null)
    {
        return page.WaitForResponseAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns the matched response. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next response with the specified url.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button.triggers-response");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, response =&gt; response.Url == "https://example.com" &amp;&amp; response.Status == 200);
    /// </code>
    /// </summary>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IResponse"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IResponse WaitForResponse(this IPage page, Regex urlOrPredicate, PageWaitForResponseOptions? options = null)
    {
        return page.WaitForResponseAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns the matched response. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next response with the specified url.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button.triggers-response");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, response =&gt; response.Url == "https://example.com" &amp;&amp; response.Status == 200);
    /// </code>
    /// </summary>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IResponse"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IResponse WaitForResponse(this IPage page, Func<IResponse, bool> urlOrPredicate, PageWaitForResponseOptions? options = null)
    {
        return page.WaitForResponseAsync(urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns the matched response. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next response with the specified url.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button.triggers-response");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, response =&gt; response.Url == "https://example.com" &amp;&amp; response.Status == 200);
    /// </code>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IResponse"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IResponse RunAndWaitForResponse(this IPage page, Func<Task> action, string urlOrPredicate, PageRunAndWaitForResponseOptions? options = null)
    {
        return page.RunAndWaitForResponseAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns the matched response. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next response with the specified url.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button.triggers-response");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, response =&gt; response.Url == "https://example.com" &amp;&amp; response.Status == 200);
    /// </code>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IResponse"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IResponse RunAndWaitForResponse(this IPage page, Func<Task> action, Regex urlOrPredicate, PageRunAndWaitForResponseOptions? options = null)
    {
        return page.RunAndWaitForResponseAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns the matched response. See <a href="./events.md#waiting-for-event">waiting
    /// for event</a> for more details about events.
    /// </para>
    /// <code>
    /// // Waits for the next response with the specified url.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button.triggers-response");<br/>
    /// }, "http://example.com/resource");<br/>
    /// <br/>
    /// // Alternative way with a predicate.<br/>
    /// await page.RunAndWaitForResponseAsync(async () =&gt;<br/>
    /// {<br/>
    ///     await page.ClickAsync("button");<br/>
    /// }, response =&gt; response.Url == "https://example.com" &amp;&amp; response.Status == 200);
    /// </code>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="urlOrPredicate">
    /// Request URL string, regex or predicate receiving <see cref="IResponse"/> object.
    /// When a <paramref name="baseURL"/> via the context options was provided and the passed
    /// URL is a path, it gets merged via the <a href="https://developer.mozilla.org/en-US/docs/Web/API/URL/URL"><c>new
    /// URL()</c></a> constructor.
    /// </param>
    /// <param name="options">Call options</param>
    public static IResponse RunAndWaitForResponse(this IPage page, Func<Task> action, Func<IResponse, bool> urlOrPredicate, PageRunAndWaitForResponseOptions? options = null)
    {
        return page.RunAndWaitForResponseAsync(action, urlOrPredicate, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns when element specified by selector satisfies <paramref name="state"/> option.
    /// Returns <c>null</c> if waiting for <c>hidden</c> or <c>detached</c>.
    /// </para>
    /// <para>
    /// Wait for the <paramref name="selector"/> to satisfy <paramref name="state"/> option
    /// (either appear/disappear from dom, or become visible/hidden). If at the moment of
    /// calling the method <paramref name="selector"/> already satisfies the condition,
    /// the method will return immediately. If the selector doesn't satisfy the condition
    /// for the <paramref name="timeout"/> milliseconds, the function will throw.
    /// </para>
    /// <para>This method works across navigations:</para>
    /// <code>
    /// using Microsoft.Playwright;<br/>
    /// using System;<br/>
    /// using System.Threading.Tasks;<br/>
    /// <br/>
    /// class FrameExamples<br/>
    /// {<br/>
    ///   public static async Task Images()<br/>
    ///   {<br/>
    ///       using var playwright = await Playwright.CreateAsync();<br/>
    ///       await using var browser = await playwright.Chromium.LaunchAsync();<br/>
    ///       var page = await browser.NewPageAsync();<br/>
    /// <br/>
    ///       foreach (var currentUrl in new[] { "https://www.google.com", "https://bbc.com" })<br/>
    ///       {<br/>
    ///           await page.GotoAsync(currentUrl);<br/>
    ///           var element = await page.WaitForSelectorAsync("img");<br/>
    ///           Console.WriteLine($"Loaded image: {await element.GetAttributeAsync("src")}");<br/>
    ///       }<br/>
    /// <br/>
    ///       await browser.CloseAsync();<br/>
    ///   }<br/>
    /// }
    /// </code>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Playwright automatically waits for element to be ready before performing an action.
    /// Using <see cref="ILocator"/> objects and web-first assertions make the code wait-for-selector-free.
    /// </para>
    /// </remarks>
    /// <param name="selector">
    /// A selector to query for. See <a href="./selectors.md">working with selectors</a>
    /// for more details.
    /// </param>
    /// <param name="options">Call options</param>
    public static IElementHandle? WaitForSelector(this IPage page, string selector, PageWaitForSelectorOptions? options = null)
    {
        return page.WaitForSelectorAsync(selector, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>Waits for the given <paramref name="timeout"/> in milliseconds.</para>
    /// <para>
    /// Note that <c>page.waitForTimeout()</c> should only be used for debugging. Tests
    /// using the timer in production are going to be flaky. Use signals such as network
    /// events, selectors becoming visible and others instead.
    /// </para>
    /// <code>
    /// // Wait for 1 second<br/>
    /// await page.WaitForTimeoutAsync(1000);
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.WaitForTimeoutAsync"/>.</para>
    /// </summary>
    /// <param name="timeout">A timeout to wait for</param>
    public static IPage WaitForTimeout(this IPage page, float timeout)
    {
        page.WaitForTimeoutAsync(timeout).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>Waits for the main frame to navigate to the given URL.</para>
    /// <code>
    /// await page.ClickAsync("a.delayed-navigation"); // clicking the link will indirectly cause a navigation<br/>
    /// await page.WaitForURLAsync("**/target.html");
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.WaitForURLAsync"/>.</para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while waiting for the navigation. Note that if the parameter is a string without
    /// wilcard characters, the method will wait for navigation to URL that is exactly equal
    /// to the string.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage WaitForUrl(this IPage page, string url, PageWaitForURLOptions? options = default)
    {
        page.WaitForURLAsync(url, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>Waits for the main frame to navigate to the given URL.</para>
    /// <code>
    /// await page.ClickAsync("a.delayed-navigation"); // clicking the link will indirectly cause a navigation<br/>
    /// await page.WaitForURLAsync("**/target.html");
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.WaitForURLAsync"/>.</para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while waiting for the navigation. Note that if the parameter is a string without
    /// wilcard characters, the method will wait for navigation to URL that is exactly equal
    /// to the string.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage WaitForURL(this IPage page, Regex url, PageWaitForURLOptions? options = null)
    {
        page.WaitForURLAsync(url, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>Waits for the main frame to navigate to the given URL.</para>
    /// <code>
    /// await page.ClickAsync("a.delayed-navigation"); // clicking the link will indirectly cause a navigation<br/>
    /// await page.WaitForURLAsync("**/target.html");
    /// </code>
    /// <para>Shortcut for main frame's <see cref="IFrame.WaitForURLAsync"/>.</para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> to match
    /// while waiting for the navigation. Note that if the parameter is a string without
    /// wilcard characters, the method will wait for navigation to URL that is exactly equal
    /// to the string.
    /// </param>
    /// <param name="options">Call options</param>
    public static IPage WaitForURL(this IPage page, Func<string, bool> url, PageWaitForURLOptions? options = null)
    {
        page.WaitForURLAsync(url, options).GetAwaiter().GetResult();
        return page;
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IWebSocket"/>. If predicate is provided,
    /// it passes <see cref="IWebSocket"/> value into the <c>predicate</c> function and
    /// waits for <c>predicate(webSocket)</c> to return a truthy value. Will throw an error
    /// if the page is closed before the WebSocket event is fired.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IWebSocket WaitForWebSocket(this IPage page, PageWaitForWebSocketOptions? options = null)
    {
        return page.WaitForWebSocketAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IWebSocket"/>. If predicate is provided,
    /// it passes <see cref="IWebSocket"/> value into the <c>predicate</c> function and
    /// waits for <c>predicate(webSocket)</c> to return a truthy value. Will throw an error
    /// if the page is closed before the WebSocket event is fired.
    /// </para>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IWebSocket RunAndWaitForWebSocket(this IPage page, Func<Task> action, PageRunAndWaitForWebSocketOptions? options = null)
    {
        return page.RunAndWaitForWebSocketAsync(action, options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IWorker"/>. If predicate is provided,
    /// it passes <see cref="IWorker"/> value into the <c>predicate</c> function and waits
    /// for <c>predicate(worker)</c> to return a truthy value. Will throw an error if the
    /// page is closed before the worker event is fired.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IWorker WaitForWorker(this IPage page, PageWaitForWorkerOptions? options = null)
    {
        return page.WaitForWorkerAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IWorker"/>. If predicate is provided,
    /// it passes <see cref="IWorker"/> value into the <c>predicate</c> function and waits
    /// for <c>predicate(worker)</c> to return a truthy value. Will throw an error if the
    /// page is closed before the worker event is fired.
    /// </para>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IPage RunAndWaitForWorker(this IPage page, Func<Task> action, PageRunAndWaitForWorkerOptions? options = null)
    {
        page.RunAndWaitForWorkerAsync(action, options).GetAwaiter().GetResult();
        return page;
    }

    public static IPage Evaluate(this IPage page, string expression, object? arg = null)
    {
        page.EvaluateAsync(expression, arg).GetAwaiter().GetResult();
        return page;
    }

    public static IPage EvalOnSelector(this IPage page, string selector, string expression, object? arg = null)
    {
        page.EvalOnSelectorAsync(selector, expression, arg).GetAwaiter().GetResult();
        return page;
    }

    public static IPage EvalOnSelectorAll(this IPage page, string selector, string expression, object? arg = null)
    {
        page.EvalOnSelectorAllAsync(selector, expression, arg).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeBinding(this IPage page, string name, Action<BindingSource> callback)
    {
        page.ExposeBindingAsync(name, callback).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeBinding<T>(this IPage page, string name, Action<BindingSource, T> callback)
    {
        page.ExposeBindingAsync<T>(name, callback).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeBinding<TResult>(this IPage page, string name, Func<BindingSource, TResult> callback)
    {
        page.ExposeBindingAsync<TResult>(name, callback).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeBinding<TResult>(this IPage page, string name, Func<BindingSource, IJSHandle, TResult> callback)
    {
        page.ExposeBindingAsync<TResult>(name, callback).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeBinding<T, TResult>(this IPage page, string name, Func<BindingSource, T, TResult> callback)
    {
        page.ExposeBindingAsync<T, TResult>(name, callback).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeBinding<T1, T2, TResult>(this IPage page, string name, Func<BindingSource, T1, T2, TResult> callback)
    {
        page.ExposeBindingAsync<T1, T2, TResult>(name, callback).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeBinding<T1, T2, T3, TResult>(this IPage page, string name, Func<BindingSource, T1, T2, T3, TResult> callback)
    {
        page.ExposeBindingAsync<T1, T2, T3, TResult>(name, callback).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeBinding<T1, T2, T3, T4, TResult>(this IPage page, string name, Func<BindingSource, T1, T2, T3, T4, TResult> callback)
    {
        page.ExposeBindingAsync<T1, T2, T3, T4, TResult>(name, callback).GetAwaiter().GetResult();
        return page;
    }

    public static IPage ExposeFunction<T>(this IPage page, string name, Action<T> callback)
    {
        page.ExposeFunctionAsync<T>(name, callback);
        return page;
    }

    public static IPage ExposeFunction<TResult>(this IPage page, string name, Func<TResult> callback)
    {
        page.ExposeFunctionAsync<TResult>(name, callback);
        return page;
    }

    public static IPage ExposeFunction<T, TResult>(this IPage page, string name, Func<T, TResult> callback)
    {
        page.ExposeFunctionAsync<T, TResult>(name, callback);
        return page;
    }

    public static IPage ExposeFunction<T1, T2, TResult>(this IPage page, string name, Func<T1, T2, TResult> callback)
    {
        page.ExposeFunctionAsync<T1, T2, TResult>(name, callback);
        return page;
    }

    public static IPage ExposeFunction<T1, T2, T3, TResult>(this IPage page, string name, Func<T1, T2, T3, TResult> callback)
    {
        page.ExposeFunctionAsync<T1, T2, T3, TResult>(name, callback);
        return page;
    }

    public static IPage ExposeFunction<T1, T2, T3, T4, TResult>(this IPage page, string name, Func<T1, T2, T3, T4, TResult> callback)
    {
        page.ExposeFunctionAsync<T1, T2, T3, T4, TResult>(name, callback);
        return page;
    }
}
