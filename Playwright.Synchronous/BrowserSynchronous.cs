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

namespace Playwright.Synchronous;

public static class BrowserSynchronous
{
    /// <summary>
    /// <para>
    /// In case this browser is obtained using <see cref="IBrowserType.LaunchAsync"/>, closes
    /// the browser and all of its pages (if any were opened).
    /// </para>
    /// <para>
    /// In case this browser is connected to, clears all created contexts belonging to this
    /// browser and disconnects from the browser server.
    /// </para>
    /// <para>
    /// The <see cref="IBrowser"/> object itself is considered to be disposed and cannot
    /// be used anymore.
    /// </para>
    /// </summary>
    public static IBrowser Close(this IBrowser browser)
    {
        browser.CloseAsync().GetAwaiter().GetResult();
        return browser;
    }

    /// <summary>
    /// <para>Creates a new browser context. It won't share cookies/cache with other browser contexts.</para>
    /// <code>
    /// using var playwright = await Playwright.CreateAsync();<br/>
    /// var browser = await playwright.Firefox.LaunchAsync();<br/>
    /// // Create a new incognito browser context.<br/>
    /// var context = await browser.NewContextAsync();<br/>
    /// // Create a new page in a pristine context.<br/>
    /// var page = await context.NewPageAsync(); ;<br/>
    /// await page.GotoAsync("https://www.bing.com");
    /// </code>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IBrowserContext NewContext(this IBrowser browser, BrowserNewContextOptions? options = null)
    {
        return browser.NewContextAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Creates a new page in a new browser context. Closing this page will close the context
    /// as well.
    /// </para>
    /// <para>
    /// This is a convenience API that should only be used for the single-page scenarios
    /// and short snippets. Production code and testing frameworks should explicitly create
    /// <see cref="IBrowser.NewContextAsync"/> followed by the <see cref="IBrowserContext.NewPageAsync"/>
    /// to control their exact life times.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IPage NewPage(this IBrowser browser, BrowserNewPageOptions? options = null)
    {
        return browser.NewPageAsync(options).GetAwaiter().GetResult();
    }

    public static void Dispose(this IBrowser browser)
    {
        browser.DisposeAsync().GetAwaiter().GetResult();
    }
}
