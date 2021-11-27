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

public static class JSHandleSynchronous
{
    public static T Evaluate<T>(this IJSHandle jsHandle, string expression, object? arg = null)
    {
        return jsHandle.EvaluateAsync<T>(expression, arg).GetAwaiter().GetResult();
    }

    public static IJSHandle EvaluateHandle(this IJSHandle jsHandle, string expression, object? arg = null)
    {
        return jsHandle.EvaluateHandleAsync(expression, arg).GetAwaiter().GetResult();
    }

    public static Dictionary<string, IJSHandle> GetProperties(this IJSHandle jsHandle)
    {
        return jsHandle.GetPropertiesAsync().GetAwaiter().GetResult();
    }

    public static IJSHandle GetProperty(this IJSHandle jsHandle, string propertyName)
    {
        return jsHandle.GetPropertyAsync(propertyName).GetAwaiter().GetResult();
    }

    public static T JsonValue<T>(this IJSHandle jsHandle)
    {
        return jsHandle.JsonValueAsync<T>().GetAwaiter().GetResult();
    }

    public static JsonElement? Evaluate(this IJSHandle jsHandle, string expression, object? arg = null)
    {
        return jsHandle.EvaluateAsync(expression, arg).GetAwaiter().GetResult();
    }
}
