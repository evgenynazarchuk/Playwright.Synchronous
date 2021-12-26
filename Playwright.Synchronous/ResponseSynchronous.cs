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

public static class ResponseSynchronous
{
    /// <summary><para>An object with all the response HTTP headers associated with this response.</para></summary>
    public static Dictionary<string, string> AllHeaders(this IResponse response)
    {
        return response.AllHeadersAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns the buffer with response body.</para></summary>
    public static byte[] Body(this IResponse response)
    {
        return response.BodyAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Waits for this response to finish, returns always <c>null</c>.</para></summary>
    public static string? Finished(this IResponse response)
    {
        return response.FinishedAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// An array with all the request HTTP headers associated with this response. Unlike
    /// <see cref="IResponse.AllHeadersAsync"/>, header names are NOT lower-cased. Headers
    /// with multiple entries, such as <c>Set-Cookie</c>, appear in the array multiple times.
    /// </para>
    /// </summary>
    public static IReadOnlyList<Header> HeadersArray(this IResponse response)
    {
        return response.HeadersArrayAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns the value of the header matching the name. The name is case insensitive.
    /// If multiple headers have the same name (except <c>set-cookie</c>), they are returned
    /// as a list separated by <c>, </c>. For <c>set-cookie</c>, the <c>\n</c> separator
    /// is used. If no headers are found, <c>null</c> is returned.
    /// </para>
    /// </summary>
    /// <param name="name">Name of the header.</param>
    public static string? HeaderValue(this IResponse response, string name)
    {
        return response.HeaderValueAsync(name).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns all values of the headers matching the name, for example <c>set-cookie</c>.
    /// The name is case insensitive.
    /// </para>
    /// </summary>
    /// <param name="name">Name of the header.</param>
    public static IReadOnlyList<string> HeaderValues(this IResponse response, string name)
    {
        return response.HeaderValuesAsync(name).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>Returns the JSON representation of response body.</para>
    /// <para>This method will throw if the response body is not parsable via <c>JSON.parse</c>.</para>
    /// </summary>
    public static JsonElement? Json(this IResponse response)
    {
        return response.JsonAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns SSL and other security information.</para></summary>
    public static ResponseSecurityDetailsResult? SecurityDetails(this IResponse response)
    {
        return response.SecurityDetailsAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns the IP address and port of the server.</para></summary>
    public static ResponseServerAddrResult? ServerAddr(this IResponse response)
    {
        return response.ServerAddrAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns the text representation of response body.</para></summary>
    public static string Text(this IResponse response)
    {
        return response.TextAsync().GetAwaiter().GetResult();
    }
}
