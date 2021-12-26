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
    /// <summary>
    /// <para>Returns the return value of <paramref name="expression"/>.</para>
    /// <para>This method passes this handle as the first argument to <paramref name="expression"/>.</para>
    /// <para>
    /// If <paramref name="expression"/> returns a <see cref="Task"/>, then <c>handle.evaluate</c>
    /// would wait for the promise to resolve and return its value.
    /// </para>
    /// <para>Examples:</para>
    /// <code>
    /// var tweetHandle = await page.QuerySelectorAsync(".tweet .retweets");<br/>
    /// Assert.AreEqual("10 retweets", await tweetHandle.EvaluateAsync("node =&gt; node.innerText"));
    /// </code>
    /// </summary>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    public static T Evaluate<T>(this IJSHandle jsHandle, string expression, object? arg = null)
    {
        return jsHandle.EvaluateAsync<T>(expression, arg).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>Returns the return value of <paramref name="expression"/> as a <see cref="IJSHandle"/>.</para>
    /// <para>This method passes this handle as the first argument to <paramref name="expression"/>.</para>
    /// <para>
    /// The only difference between <c>jsHandle.evaluate</c> and <c>jsHandle.evaluateHandle</c>
    /// is that <c>jsHandle.evaluateHandle</c> returns <see cref="IJSHandle"/>.
    /// </para>
    /// <para>
    /// If the function passed to the <c>jsHandle.evaluateHandle</c> returns a <see cref="Task"/>,
    /// then <c>jsHandle.evaluateHandle</c> would wait for the promise to resolve and return
    /// its value.
    /// </para>
    /// <para>See <see cref="IPage.EvaluateHandleAsync"/> for more details.</para>
    /// </summary>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    public static IJSHandle EvaluateHandle(this IJSHandle jsHandle, string expression, object? arg = null)
    {
        return jsHandle.EvaluateHandleAsync(expression, arg).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// The method returns a map with **own property names** as keys and JSHandle instances
    /// for the property values.
    /// </para>
    /// <code>
    /// var handle = await page.EvaluateHandleAsync("() =&gt; ({window, document}");<br/>
    /// var properties = await handle.GetPropertiesAsync();<br/>
    /// var windowHandle = properties["window"];<br/>
    /// var documentHandle = properties["document"];<br/>
    /// await handle.DisposeAsync();
    /// </code>
    /// </summary>
    public static Dictionary<string, IJSHandle> GetProperties(this IJSHandle jsHandle)
    {
        return jsHandle.GetPropertiesAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Fetches a single property from the referenced object.</para></summary>
    /// <param name="propertyName">property to get</param>
    public static IJSHandle GetProperty(this IJSHandle jsHandle, string propertyName)
    {
        return jsHandle.GetPropertyAsync(propertyName).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns a JSON representation of the object. If the object has a <c>toJSON</c> function,
    /// it **will not be called**.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The method will return an empty JSON object if the referenced object is not stringifiable.
    /// It will throw an error if the object has circular references.
    /// </para>
    /// </remarks>
    public static T JsonValue<T>(this IJSHandle jsHandle)
    {
        return jsHandle.JsonValueAsync<T>().GetAwaiter().GetResult();
    }

    /// <inheritdoc cref="EvaluateAsync{T}(string, object)" />
    public static JsonElement? Evaluate(this IJSHandle jsHandle, string expression, object? arg = null)
    {
        return jsHandle.EvaluateAsync(expression, arg).GetAwaiter().GetResult();
    }
}
