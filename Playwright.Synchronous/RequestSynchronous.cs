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

public static class RequestSynchronous
{
    /// <summary>
    /// <para>
    /// An object with all the request HTTP headers associated with this request. The header
    /// names are lower-cased.
    /// </para>
    /// </summary>
    public static Dictionary<string, string> AllHeaders(this IRequest request)
    {
        return request.AllHeadersAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// An array with all the request HTTP headers associated with this request. Unlike
    /// <see cref="IRequest.AllHeadersAsync"/>, header names are NOT lower-cased. Headers
    /// with multiple entries, such as <c>Set-Cookie</c>, appear in the array multiple times.
    /// </para>
    /// </summary>
    public static IReadOnlyList<Header> HeadersArray(this IRequest request)
    {
        return request.HeadersArrayAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns the value of the header matching the name. The name is case insensitive.</para></summary>
    /// <param name="name">Name of the header.</param>
    public static string? HeaderValueAsync(this IRequest request, string name)
    {
        return request.HeaderValueAsync(name).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns the matching <see cref="IResponse"/> object, or <c>null</c> if the response
    /// was not received due to error.
    /// </para>
    /// </summary>
    public static IResponse? Response(this IRequest request)
    {
        return request.ResponseAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns resource size information for given request.</para></summary>
    public static RequestSizesResult Sizes(this IRequest request)
    {
        return request.SizesAsync().GetAwaiter().GetResult();
    }
}
