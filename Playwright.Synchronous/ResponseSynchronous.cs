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
    public static Dictionary<string, string> AllHeaders(this IResponse response)
    {
        return response.AllHeadersAsync().GetAwaiter().GetResult();
    }

    public static byte[] Body(this IResponse response)
    {
        return response.BodyAsync().GetAwaiter().GetResult();
    }

    public static string? Finished(this IResponse response)
    {
        return response.FinishedAsync().GetAwaiter().GetResult();
    }

    public static IReadOnlyList<Header> HeadersArray(this IResponse response)
    {
        return response.HeadersArrayAsync().GetAwaiter().GetResult();
    }

    public static string? HeaderValue(this IResponse response, string name)
    {
        return response.HeaderValueAsync(name).GetAwaiter().GetResult();
    }

    public static IReadOnlyList<string> HeaderValues(this IResponse response, string name)
    {
        return response.HeaderValuesAsync(name).GetAwaiter().GetResult();
    }

    public static JsonElement? Json(this IResponse response)
    {
        return response.JsonAsync().GetAwaiter().GetResult();
    }

    public static ResponseSecurityDetailsResult? SecurityDetails(this IResponse response)
    {
        return response.SecurityDetailsAsync().GetAwaiter().GetResult();
    }

    public static ResponseServerAddrResult? ServerAddr(this IResponse response)
    {
        return response.ServerAddrAsync().GetAwaiter().GetResult();
    }

    public static string Text(this IResponse response)
    {
        return response.TextAsync().GetAwaiter().GetResult();
    }
}
