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
    public static void AddCookies(this IBrowserContext browserContext, IEnumerable<Cookie> cookies)
    {
        browserContext.AddCookiesAsync(cookies).GetAwaiter().GetResult();
    }

    public static void AddInitScript(this IBrowserContext browserContext, string? script = null, string? scriptPath = null)
    {
        browserContext.AddInitScriptAsync(script, scriptPath).GetAwaiter().GetResult();
    }

    public static void ClearCookies(this IBrowserContext browserContext)
    {
        browserContext.ClearCookiesAsync().GetAwaiter().GetResult();
    }

    public static void ClearPermissions(this IBrowserContext browserContext)
    {
        browserContext.ClearPermissionsAsync().GetAwaiter().GetResult();
    }

    public static void Close(this IBrowserContext browserContext)
    {
        browserContext.CloseAsync().GetAwaiter().GetResult();
    }

    public static IReadOnlyList<BrowserContextCookiesResult> Cookies(this IBrowserContext browserContext, IEnumerable<string>? urls = null)
    {
        return browserContext.CookiesAsync(urls).GetAwaiter().GetResult();
    }

    public static void ExposeBinding(this IBrowserContext browserContext, string name, Action callback, BrowserContextExposeBindingOptions? options = null)
    {
        browserContext.ExposeBindingAsync(name, callback, options).GetAwaiter().GetResult();
    }

    public static void ExposeBinding(this IBrowserContext browserContext, string name, Action<BindingSource> callback)
    {
        browserContext.ExposeBindingAsync(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T>(this IBrowserContext browserContext, string name, Action<BindingSource, T> callback)
    {
        browserContext.ExposeBindingAsync<T>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, TResult> callback)
    {
        browserContext.ExposeBindingAsync<TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, IJSHandle, TResult> callback)
    {
        browserContext.ExposeBindingAsync<TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T, TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, T, TResult> callback)
    {
        browserContext.ExposeBindingAsync<T, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T1, T2, TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, T1, T2, TResult> callback)
    {
        browserContext.ExposeBindingAsync<T1, T2, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T1, T2, T3, TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, T1, T2, T3, TResult> callback)
    {
        browserContext.ExposeBindingAsync<T1, T2, T3, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeBinding<T1, T2, T3, T4, TResult>(this IBrowserContext browserContext, string name, Func<BindingSource, T1, T2, T3, T4, TResult> callback)
    {
        browserContext.ExposeBindingAsync<T1, T2, T3, T4, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeFunction(this IBrowserContext browserContext, string name, Action callback)
    {
        browserContext.ExposeFunctionAsync(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeFunction<T>(this IBrowserContext browserContext, string name, Action<T> callback)
    {
        browserContext.ExposeFunctionAsync<T>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeFunction<TResult>(this IBrowserContext browserContext, string name, Func<TResult> callback)
    {
        browserContext.ExposeFunctionAsync<TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeFunction<T, TResult>(this IBrowserContext browserContext, string name, Func<T, TResult> callback)
    {
        browserContext.ExposeFunctionAsync<T, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeFunction<T1, T2, TResult>(this IBrowserContext browserContext, string name, Func<T1, T2, TResult> callback)
    {
        browserContext.ExposeFunctionAsync<T1, T2, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeFunction<T1, T2, T3, TResult>(this IBrowserContext browserContext, string name, Func<T1, T2, T3, TResult> callback)
    {
        browserContext.ExposeFunctionAsync<T1, T2, T3, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void ExposeFunction<T1, T2, T3, T4, TResult>(this IBrowserContext browserContext, string name, Func<T1, T2, T3, T4, TResult> callback)
    {
        browserContext.ExposeFunctionAsync<T1, T2, T3, T4, TResult>(name, callback).GetAwaiter().GetResult();
    }

    public static void GrantPermissions(this IBrowserContext browserContext, IEnumerable<string> permissions, BrowserContextGrantPermissionsOptions? options = null)
    {
        browserContext.GrantPermissionsAsync(permissions, options).GetAwaiter().GetResult();
    }

    public static IPage NewPage(this IBrowserContext browserContext)
    {
        return browserContext.NewPageAsync().GetAwaiter().GetResult();
    }

    public static void Route(this IBrowserContext browserContext, string url, Action<IRoute> handler, BrowserContextRouteOptions? options = null)
    {
        browserContext.RouteAsync(url, handler, options).GetAwaiter().GetResult();
    }

    public static void Route(this IBrowserContext browserContext, Regex url, Action<IRoute> handler, BrowserContextRouteOptions? options = null)
    {
        browserContext.RouteAsync(url, handler, options).GetAwaiter().GetResult();
    }

    public static void Route(this IBrowserContext browserContext, Func<string, bool> url, Action<IRoute> handler, BrowserContextRouteOptions? options = null)
    {
        browserContext.RouteAsync(url, handler, options).GetAwaiter().GetResult();
    }

    public static void SetExtraHTTPHeaders(this IBrowserContext browserContext, IEnumerable<KeyValuePair<string, string>> headers)
    {
        browserContext.SetExtraHTTPHeadersAsync(headers).GetAwaiter().GetResult();
    }

    public static void SetGeolocation(this IBrowserContext browserContext, Geolocation? geolocation)
    {
        browserContext.SetGeolocationAsync(geolocation).GetAwaiter().GetResult();
    }

    public static void SetOffline(this IBrowserContext browserContext, bool offline)
    {
        browserContext.SetOfflineAsync(offline).GetAwaiter().GetResult();
    }

    public static string StorageState(this IBrowserContext browserContext, BrowserContextStorageStateOptions? options = null)
    {
        return browserContext.StorageStateAsync(options).GetAwaiter().GetResult();
    }

    public static void Unroute(this IBrowserContext browserContext, string url, Action<IRoute>? handler = null)
    {
        browserContext.UnrouteAsync(url, handler).GetAwaiter().GetResult();
    }

    public static void Unroute(this IBrowserContext browserContext, Regex url, Action<IRoute>? handler = null)
    {
        browserContext.UnrouteAsync(url, handler).GetAwaiter().GetResult();
    }

    public static void Unroute(this IBrowserContext browserContext, Func<string, bool> url, Action<IRoute>? handler = null)
    {
        browserContext.UnrouteAsync(url, handler).GetAwaiter().GetResult();
    }

    public static IPage WaitForPage(this IBrowserContext browserContext, BrowserContextWaitForPageOptions? options = null)
    {
        return browserContext.WaitForPageAsync(options).GetAwaiter().GetResult();
    }

    public static IPage RunAndWaitForPage(this IBrowserContext browserContext, Func<Task> action, BrowserContextRunAndWaitForPageOptions? options = null)
    {
        return browserContext.RunAndWaitForPageAsync(action, options).GetAwaiter().GetResult();
    }
}
