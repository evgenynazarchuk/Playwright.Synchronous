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

public static class BrowserContextSynchronous
{
    /// <summary>
    /// <para>
    /// Adds cookies into this browser context. All pages within this context will have
    /// these cookies installed. Cookies can be obtained via <see cref="IBrowserContext.CookiesAsync"/>.
    /// </para>
    /// <code>await context.AddCookiesAsync(new[] { cookie1, cookie2 });</code>
    /// </summary>
    /// <param name="cookies">
    /// </param>
    public static IBrowserContext AddCookies(this IBrowserContext browserContext, IEnumerable<Cookie> cookies)
    {
        browserContext.AddCookiesAsync(cookies).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>Adds a script which would be evaluated in one of the following scenarios:</para>
    /// <list type="bullet">
    /// <item><description>Whenever a page is created in the browser context or is navigated.</description></item>
    /// <item><description>
    /// Whenever a child frame is attached or navigated in any page in the browser context.
    /// In this case, the script is evaluated in the context of the newly attached frame.
    /// </description></item>
    /// </list>
    /// <para>
    /// The script is evaluated after the document was created but before any of its scripts
    /// were run. This is useful to amend the JavaScript environment, e.g. to seed <c>Math.random</c>.
    /// </para>
    /// <para>An example of overriding <c>Math.random</c> before the page loads:</para>
    /// <code>await context.AddInitScriptAsync(new BrowserContextAddInitScriptOptions { ScriptPath = "preload.js" });</code>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The order of evaluation of multiple scripts installed via <see cref="IBrowserContext.AddInitScriptAsync"/>
    /// and <see cref="IPage.AddInitScriptAsync"/> is not defined.
    /// </para>
    /// </remarks>
    /// <param name="script">Script to be evaluated in all pages in the browser context.</param>
    /// <param name="scriptPath">Instead of specifying <paramref name="script"/>, gives the file name to load from.</param>
    public static IBrowserContext AddInitScript(this IBrowserContext browserContext, string? script = null, string? scriptPath = null)
    {
        browserContext.AddInitScriptAsync(script, scriptPath).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary><para>Clears context cookies.</para></summary>
    public static IBrowserContext ClearCookies(this IBrowserContext browserContext)
    {
        browserContext.ClearCookiesAsync().GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>Clears all permission overrides for the browser context.</para>
    /// <code>
    /// var context = await browser.NewContextAsync();<br/>
    /// await context.GrantPermissionsAsync(new[] { "clipboard-read" });<br/>
    /// // Alternatively, you can use the helper class ContextPermissions<br/>
    /// //  to specify the permissions...<br/>
    /// // do stuff ...<br/>
    /// await context.ClearPermissionsAsync();
    /// </code>
    /// </summary>
    public static IBrowserContext ClearPermissions(this IBrowserContext browserContext)
    {
        browserContext.ClearPermissionsAsync().GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Closes the browser context. All the pages that belong to the browser context will
    /// be closed.
    /// </para>
    /// </summary>
    /// <remarks><para>The default browser context cannot be closed.</para></remarks>
    public static IBrowserContext Close(this IBrowserContext browserContext)
    {
        browserContext.CloseAsync().GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// If no URLs are specified, this method returns all cookies. If URLs are specified,
    /// only cookies that affect those URLs are returned.
    /// </para>
    /// </summary>
    /// <param name="urls">Optional list of URLs.</param>
    public static IReadOnlyList<BrowserContextCookiesResult> Cookies(this IBrowserContext browserContext, IEnumerable<string>? urls = null)
    {
        return browserContext.CookiesAsync(urls).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// The method adds a function called <paramref name="name"/> on the <c>window</c> object
    /// of every frame in every page in the context. When called, the function executes
    /// <paramref name="callback"/> and returns a <see cref="Task"/> which resolves to the
    /// return value of <paramref name="callback"/>. If the <paramref name="callback"/>
    /// returns a <see cref="Promise"/>, it will be awaited.
    /// </para>
    /// <para>
    /// The first argument of the <paramref name="callback"/> function contains information
    /// about the caller: <c>{ browserContext: BrowserContext, page: Page, frame: Frame
    /// }</c>.
    /// </para>
    /// <para>See <see cref="IPage.ExposeBindingAsync"/> for page-only version.</para>
    /// <para>An example of exposing page URL to all frames in all pages in the context:</para>
    /// <code>
    /// using Microsoft.Playwright;<br/>
    /// using System.Threading.Tasks;<br/>
    /// <br/>
    /// class Program<br/>
    /// {<br/>
    ///     public static async Task Main()<br/>
    ///     {<br/>
    ///         using var playwright = await Playwright.CreateAsync();<br/>
    ///         var browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });<br/>
    ///         var context = await browser.NewContextAsync();<br/>
    /// <br/>
    ///         await context.ExposeBindingAsync("pageURL", source =&gt; source.Page.Url);<br/>
    ///         var page = await context.NewPageAsync();<br/>
    ///         await page.SetContentAsync("&lt;script&gt;\n" +<br/>
    ///         "  async function onClick() {\n" +<br/>
    ///         "    document.querySelector('div').textContent = await window.pageURL();\n" +<br/>
    ///         "  }\n" +<br/>
    ///         "&lt;/script&gt;\n" +<br/>
    ///         "&lt;button onclick=\"onClick()\"&gt;Click me&lt;/button&gt;\n" +<br/>
    ///         "&lt;div&gt;&lt;/div&gt;");<br/>
    ///         await page.ClickAsync("button");<br/>
    ///     }<br/>
    /// }
    /// </code>
    /// <para>An example of passing an element handle:</para>
    /// <code>
    /// var result = new TaskCompletionSource&lt;string&gt;();<br/>
    /// var page = await Context.NewPageAsync();<br/>
    /// await Context.ExposeBindingAsync("clicked", async (BindingSource _, IJSHandle t) =&gt;<br/>
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
    /// // Note: it makes sense to await the result here, because otherwise, the context<br/>
    /// //  gets closed and the binding function will throw an exception.<br/>
    /// Assert.AreEqual("Click me", await result.Task);
    /// </code>
    /// </summary>
    /// <param name="name">Name of the function on the window object.</param>
    /// <param name="callback">Callback function that will be called in the Playwright's context.</param>
    /// <param name="options">Call options</param>
    public static IBrowserContext ExposeBinding(this IBrowserContext browserContext, string name, Action callback, BrowserContextExposeBindingOptions? options = null)
    {
        browserContext.ExposeBindingAsync(name, callback, options).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeBindingAsync(string, Action, BrowserContextExposeBindingOptions)"/>
    public static IBrowserContext ExposeBinding(this IBrowserContext browserContext, string name, Action<BindingSource> callback)
    {
        browserContext.ExposeBindingAsync(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeBindingAsync(string, Action, BrowserContextExposeBindingOptions)"/>
    public static IBrowserContext ExposeBinding<T>(this IBrowserContext browserContext, string name, Action<BindingSource, T> callback)
    {
        browserContext.ExposeBindingAsync<T>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeBindingAsync(string, Action, BrowserContextExposeBindingOptions)"/>
    public static IBrowserContext ExposeBinding<TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, TResult> callback)
    {
        browserContext.ExposeBindingAsync<TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeBindingAsync(string, Action, BrowserContextExposeBindingOptions)"/>
    public static IBrowserContext ExposeBinding<TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, IJSHandle, TResult> callback)
    {
        browserContext.ExposeBindingAsync<TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeBindingAsync(string, Action, BrowserContextExposeBindingOptions)"/>
    public static IBrowserContext ExposeBinding<T, TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, T, TResult> callback)
    {
        browserContext.ExposeBindingAsync<T, TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeBindingAsync(string, Action, BrowserContextExposeBindingOptions)"/>
    public static IBrowserContext ExposeBinding<T1, T2, TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, T1, T2, TResult> callback)
    {
        browserContext.ExposeBindingAsync<T1, T2, TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeBindingAsync(string, Action, BrowserContextExposeBindingOptions)"/>
    public static IBrowserContext ExposeBinding<T1, T2, T3, TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, T1, T2, T3, TResult> callback)
    {
        browserContext.ExposeBindingAsync<T1, T2, T3, TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeBindingAsync(string, Action, BrowserContextExposeBindingOptions)"/>
    public static IBrowserContext ExposeBinding<T1, T2, T3, T4, TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, T1, T2, T3, T4, TResult> callback)
    {
        browserContext.ExposeBindingAsync<T1, T2, T3, T4, TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// The method adds a function called <paramref name="name"/> on the <c>window</c> object
    /// of every frame in every page in the context. When called, the function executes
    /// <paramref name="callback"/> and returns a <see cref="Task"/> which resolves to the
    /// return value of <paramref name="callback"/>.
    /// </para>
    /// <para>If the <paramref name="callback"/> returns a <see cref="Task"/>, it will be awaited.</para>
    /// <para>See <see cref="IPage.ExposeFunctionAsync"/> for page-only version.</para>
    /// <para>An example of adding a <c>sha256</c> function to all pages in the context:</para>
    /// <code>
    /// using Microsoft.Playwright;<br/>
    /// using System;<br/>
    /// using System.Security.Cryptography;<br/>
    /// using System.Threading.Tasks;<br/>
    /// <br/>
    /// class BrowserContextExamples<br/>
    /// {<br/>
    ///     public static async Task Main()<br/>
    ///     {<br/>
    ///         using var playwright = await Playwright.CreateAsync();<br/>
    ///         var browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });<br/>
    ///         var context = await browser.NewContextAsync();<br/>
    /// <br/>
    ///         await context.ExposeFunctionAsync("sha256", (string input) =&gt;<br/>
    ///         {<br/>
    ///             return Convert.ToBase64String(<br/>
    ///                 SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(input)));<br/>
    ///         });<br/>
    /// <br/>
    ///         var page = await context.NewPageAsync();<br/>
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
    /// <param name="name">Name of the function on the window object.</param>
    /// <param name="callback">Callback function that will be called in the Playwright's context.</param>
    public static IBrowserContext ExposeFunction(this IBrowserContext browserContext, string name, Action callback)
    {
        browserContext.ExposeFunctionAsync(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeFunctionAsync(string, Action)"/>
    public static IBrowserContext ExposeFunction<T>(this IBrowserContext browserContext, string name, Action<T> callback)
    {
        browserContext.ExposeFunctionAsync<T>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeFunctionAsync(string, Action)"/>
    public static IBrowserContext ExposeFunction<TResult>(this IBrowserContext browserContext, string name, Func<TResult> callback)
    {
        browserContext.ExposeFunctionAsync<TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeFunctionAsync(string, Action)"/>
    public static IBrowserContext ExposeFunction<T, TResult>(this IBrowserContext browserContext, string name, Func<T, TResult> callback)
    {
        browserContext.ExposeFunctionAsync<T, TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeFunctionAsync(string, Action)"/>
    public static IBrowserContext ExposeFunction<T1, T2, TResult>(this IBrowserContext browserContext, string name, Func<T1, T2, TResult> callback)
    {
        browserContext.ExposeFunctionAsync<T1, T2, TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeFunctionAsync(string, Action)"/>
    public static IBrowserContext ExposeFunction<T1, T2, T3, TResult>(this IBrowserContext browserContext, string name, Func<T1, T2, T3, TResult> callback)
    {
        browserContext.ExposeFunctionAsync<T1, T2, T3, TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <inheritdoc cref="ExposeFunctionAsync(string, Action)"/>
    public static IBrowserContext ExposeFunction<T1, T2, T3, T4, TResult>(this IBrowserContext browserContext, string name, Func<T1, T2, T3, T4, TResult> callback)
    {
        browserContext.ExposeFunctionAsync<T1, T2, T3, T4, TResult>(name, callback).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Grants specified permissions to the browser context. Only grants corresponding permissions
    /// to the given origin if specified.
    /// </para>
    /// </summary>
    /// <param name="permissions">
    /// A permission or an array of permissions to grant. Permissions can be one of the
    /// following values:
    /// <list type="bullet">
    /// <item><description><c>'geolocation'</c></description></item>
    /// <item><description><c>'midi'</c></description></item>
    /// <item><description><c>'midi-sysex'</c> (system-exclusive midi)</description></item>
    /// <item><description><c>'notifications'</c></description></item>
    /// <item><description><c>'push'</c></description></item>
    /// <item><description><c>'camera'</c></description></item>
    /// <item><description><c>'microphone'</c></description></item>
    /// <item><description><c>'background-sync'</c></description></item>
    /// <item><description><c>'ambient-light-sensor'</c></description></item>
    /// <item><description><c>'accelerometer'</c></description></item>
    /// <item><description><c>'gyroscope'</c></description></item>
    /// <item><description><c>'magnetometer'</c></description></item>
    /// <item><description><c>'accessibility-events'</c></description></item>
    /// <item><description><c>'clipboard-read'</c></description></item>
    /// <item><description><c>'clipboard-write'</c></description></item>
    /// <item><description><c>'payment-handler'</c></description></item>
    /// </list>
    /// </param>
    /// <param name="options">Call options</param>
    public static IBrowserContext GrantPermissions(this IBrowserContext browserContext, IEnumerable<string> permissions, BrowserContextGrantPermissionsOptions? options = null)
    {
        browserContext.GrantPermissionsAsync(permissions, options).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary><para>Creates a new page in the browser context.</para></summary>
    public static IPage NewPage(this IBrowserContext browserContext)
    {
        return browserContext.NewPageAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Routing provides the capability to modify network requests that are made by any
    /// page in the browser context. Once route is enabled, every request matching the url
    /// pattern will stall unless it's continued, fulfilled or aborted.
    /// </para>
    /// <para>An example of a naive handler that aborts all image requests:</para>
    /// <code>
    /// var context = await browser.NewContextAsync();<br/>
    /// var page = await context.NewPageAsync();<br/>
    /// await context.RouteAsync("**/*.{png,jpg,jpeg}", r =&gt; r.AbortAsync());<br/>
    /// await page.GotoAsync("https://theverge.com");<br/>
    /// await browser.CloseAsync();
    /// </code>
    /// <para>or the same snippet using a regex pattern instead:</para>
    /// <code>
    /// var context = await browser.NewContextAsync();<br/>
    /// var page = await context.NewPageAsync();<br/>
    /// await context.RouteAsync(new Regex("(\\.png$)|(\\.jpg$)"), r =&gt; r.AbortAsync());<br/>
    /// await page.GotoAsync("https://theverge.com");<br/>
    /// await browser.CloseAsync();
    /// </code>
    /// <para>
    /// It is possible to examine the request to decide the route action. For example, mocking
    /// all requests that contain some post data, and leaving all other requests as is:
    /// </para>
    /// <code>
    /// await page.RouteAsync("/api/**", async r =&gt;<br/>
    /// {<br/>
    ///     if (r.Request.PostData.Contains("my-string"))<br/>
    ///         await r.FulfillAsync(body: "mocked-data");<br/>
    ///     else<br/>
    ///         await r.ContinueAsync();<br/>
    /// });
    /// </code>
    /// <para>
    /// Page routes (set up with <see cref="IPage.RouteAsync"/>) take precedence over browser
    /// context routes when request matches both handlers.
    /// </para>
    /// <para>To remove a route with its handler you can use <see cref="IBrowserContext.UnrouteAsync"/>.</para>
    /// </summary>
    /// <remarks>
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
    public static IBrowserContext Route(this IBrowserContext browserContext, string url, Action<IRoute> handler, BrowserContextRouteOptions? options = null)
    {
        browserContext.RouteAsync(url, handler, options).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Routing provides the capability to modify network requests that are made by any
    /// page in the browser context. Once route is enabled, every request matching the url
    /// pattern will stall unless it's continued, fulfilled or aborted.
    /// </para>
    /// <para>An example of a naive handler that aborts all image requests:</para>
    /// <code>
    /// var context = await browser.NewContextAsync();<br/>
    /// var page = await context.NewPageAsync();<br/>
    /// await context.RouteAsync("**/*.{png,jpg,jpeg}", r =&gt; r.AbortAsync());<br/>
    /// await page.GotoAsync("https://theverge.com");<br/>
    /// await browser.CloseAsync();
    /// </code>
    /// <para>or the same snippet using a regex pattern instead:</para>
    /// <code>
    /// var context = await browser.NewContextAsync();<br/>
    /// var page = await context.NewPageAsync();<br/>
    /// await context.RouteAsync(new Regex("(\\.png$)|(\\.jpg$)"), r =&gt; r.AbortAsync());<br/>
    /// await page.GotoAsync("https://theverge.com");<br/>
    /// await browser.CloseAsync();
    /// </code>
    /// <para>
    /// It is possible to examine the request to decide the route action. For example, mocking
    /// all requests that contain some post data, and leaving all other requests as is:
    /// </para>
    /// <code>
    /// await page.RouteAsync("/api/**", async r =&gt;<br/>
    /// {<br/>
    ///     if (r.Request.PostData.Contains("my-string"))<br/>
    ///         await r.FulfillAsync(body: "mocked-data");<br/>
    ///     else<br/>
    ///         await r.ContinueAsync();<br/>
    /// });
    /// </code>
    /// <para>
    /// Page routes (set up with <see cref="IPage.RouteAsync"/>) take precedence over browser
    /// context routes when request matches both handlers.
    /// </para>
    /// <para>To remove a route with its handler you can use <see cref="IBrowserContext.UnrouteAsync"/>.</para>
    /// </summary>
    /// <remarks>
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
    public static IBrowserContext Route(this IBrowserContext browserContext, Regex url, Action<IRoute> handler, BrowserContextRouteOptions? options = null)
    {
        browserContext.RouteAsync(url, handler, options).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Routing provides the capability to modify network requests that are made by any
    /// page in the browser context. Once route is enabled, every request matching the url
    /// pattern will stall unless it's continued, fulfilled or aborted.
    /// </para>
    /// <para>An example of a naive handler that aborts all image requests:</para>
    /// <code>
    /// var context = await browser.NewContextAsync();<br/>
    /// var page = await context.NewPageAsync();<br/>
    /// await context.RouteAsync("**/*.{png,jpg,jpeg}", r =&gt; r.AbortAsync());<br/>
    /// await page.GotoAsync("https://theverge.com");<br/>
    /// await browser.CloseAsync();
    /// </code>
    /// <para>or the same snippet using a regex pattern instead:</para>
    /// <code>
    /// var context = await browser.NewContextAsync();<br/>
    /// var page = await context.NewPageAsync();<br/>
    /// await context.RouteAsync(new Regex("(\\.png$)|(\\.jpg$)"), r =&gt; r.AbortAsync());<br/>
    /// await page.GotoAsync("https://theverge.com");<br/>
    /// await browser.CloseAsync();
    /// </code>
    /// <para>
    /// It is possible to examine the request to decide the route action. For example, mocking
    /// all requests that contain some post data, and leaving all other requests as is:
    /// </para>
    /// <code>
    /// await page.RouteAsync("/api/**", async r =&gt;<br/>
    /// {<br/>
    ///     if (r.Request.PostData.Contains("my-string"))<br/>
    ///         await r.FulfillAsync(body: "mocked-data");<br/>
    ///     else<br/>
    ///         await r.ContinueAsync();<br/>
    /// });
    /// </code>
    /// <para>
    /// Page routes (set up with <see cref="IPage.RouteAsync"/>) take precedence over browser
    /// context routes when request matches both handlers.
    /// </para>
    /// <para>To remove a route with its handler you can use <see cref="IBrowserContext.UnrouteAsync"/>.</para>
    /// </summary>
    /// <remarks>
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
    public static IBrowserContext Route(this IBrowserContext browserContext, Func<string, bool> url, Action<IRoute> handler, BrowserContextRouteOptions? options = null)
    {
        browserContext.RouteAsync(url, handler, options).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// The extra HTTP headers will be sent with every request initiated by any page in
    /// the context. These headers are merged with page-specific extra HTTP headers set
    /// with <see cref="IPage.SetExtraHTTPHeadersAsync"/>. If page overrides a particular
    /// header, page-specific header value will be used instead of the browser context header
    /// value.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="IBrowserContext.SetExtraHTTPHeadersAsync"/> does not guarantee the order
    /// of headers in the outgoing requests.
    /// </para>
    /// </remarks>
    /// <param name="headers">
    /// An object containing additional HTTP headers to be sent with every request. All
    /// header values must be strings.
    /// </param>
    public static IBrowserContext SetExtraHTTPHeaders(this IBrowserContext browserContext, IEnumerable<KeyValuePair<string, string>> headers)
    {
        browserContext.SetExtraHTTPHeadersAsync(headers).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Sets the context's geolocation. Passing <c>null</c> or <c>undefined</c> emulates
    /// position unavailable.
    /// </para>
    /// <code>
    /// await context.SetGeolocationAsync(new Geolocation()<br/>
    /// {<br/>
    ///     Latitude = 59.95f,<br/>
    ///     Longitude = 30.31667f<br/>
    /// });
    /// </code>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Consider using <see cref="IBrowserContext.GrantPermissionsAsync"/> to grant permissions
    /// for the browser context pages to read its geolocation.
    /// </para>
    /// </remarks>
    /// <param name="geolocation">
    /// </param>
    public static IBrowserContext SetGeolocation(this IBrowserContext browserContext, Geolocation? geolocation)
    {
        browserContext.SetGeolocationAsync(geolocation).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <param name="offline">Whether to emulate network being offline for the browser context.</param>
    public static IBrowserContext SetOffline(this IBrowserContext browserContext, bool offline)
    {
        browserContext.SetOfflineAsync(offline).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Returns storage state for this browser context, contains current cookies and local
    /// storage snapshot.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static string StorageState(this IBrowserContext browserContext, BrowserContextStorageStateOptions? options = null)
    {
        return browserContext.StorageStateAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Removes a route created with <see cref="IBrowserContext.RouteAsync"/>. When <paramref
    /// name="handler"/> is not specified, removes all routes for the <paramref name="url"/>.
    /// </para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> used to register
    /// a routing with <see cref="IBrowserContext.RouteAsync"/>.
    /// </param>
    /// <param name="handler">Optional handler function used to register a routing with <see cref="IBrowserContext.RouteAsync"/>.</param>
    public static IBrowserContext Unroute(this IBrowserContext browserContext, string url, Action<IRoute>? handler = null)
    {
        browserContext.UnrouteAsync(url, handler).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Removes a route created with <see cref="IBrowserContext.RouteAsync"/>. When <paramref
    /// name="handler"/> is not specified, removes all routes for the <paramref name="url"/>.
    /// </para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> used to register
    /// a routing with <see cref="IBrowserContext.RouteAsync"/>.
    /// </param>
    /// <param name="handler">Optional handler function used to register a routing with <see cref="IBrowserContext.RouteAsync"/>.</param>
    public static IBrowserContext Unroute(this IBrowserContext browserContext, Regex url, Action<IRoute>? handler = null)
    {
        browserContext.UnrouteAsync(url, handler).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Removes a route created with <see cref="IBrowserContext.RouteAsync"/>. When <paramref
    /// name="handler"/> is not specified, removes all routes for the <paramref name="url"/>.
    /// </para>
    /// </summary>
    /// <param name="url">
    /// A glob pattern, regex pattern or predicate receiving <see cref="URL"/> used to register
    /// a routing with <see cref="IBrowserContext.RouteAsync"/>.
    /// </param>
    /// <param name="handler">Optional handler function used to register a routing with <see cref="IBrowserContext.RouteAsync"/>.</param>
    public static IBrowserContext Unroute(this IBrowserContext browserContext, Func<string, bool> url, Action<IRoute>? handler = null)
    {
        browserContext.UnrouteAsync(url, handler).GetAwaiter().GetResult();
        return browserContext;
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IPage"/> to be created in the context.
    /// If predicate is provided, it passes <see cref="IPage"/> value into the <c>predicate</c>
    /// function and waits for <c>predicate(event)</c> to return a truthy value. Will throw
    /// an error if the context closes before new <see cref="IPage"/> is created.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static IPage WaitForPage(this IBrowserContext browserContext, BrowserContextWaitForPageOptions? options = null)
    {
        return browserContext.WaitForPageAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Performs action and waits for a new <see cref="IPage"/> to be created in the context.
    /// If predicate is provided, it passes <see cref="IPage"/> value into the <c>predicate</c>
    /// function and waits for <c>predicate(event)</c> to return a truthy value. Will throw
    /// an error if the context closes before new <see cref="IPage"/> is created.
    /// </para>
    /// </summary>
    /// <param name="action">Action that triggers the event.</param>
    /// <param name="options">Call options</param>
    public static IPage RunAndWaitForPage(this IBrowserContext browserContext, Func<Task> action, BrowserContextRunAndWaitForPageOptions? options = null)
    {
        return browserContext.RunAndWaitForPageAsync(action, options).GetAwaiter().GetResult();
    }
}
