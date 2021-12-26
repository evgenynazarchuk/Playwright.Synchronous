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

public static class WorkerSynchronous
{
    /// <summary>
    /// <para>Returns the return value of <paramref name="expression"/>.</para>
    /// <para>
    /// If the function passed to the <see cref="IWorker.EvaluateAsync"/> returns a <see
    /// cref="Task"/>, then <see cref="IWorker.EvaluateAsync"/> would wait for the promise
    /// to resolve and return its value.
    /// </para>
    /// <para>
    /// If the function passed to the <see cref="IWorker.EvaluateAsync"/> returns a non-<see
    /// cref="Serializable"/> value, then <see cref="IWorker.EvaluateAsync"/> returns <c>undefined</c>.
    /// Playwright also supports transferring some additional values that are not serializable
    /// by <c>JSON</c>: <c>-0</c>, <c>NaN</c>, <c>Infinity</c>, <c>-Infinity</c>.
    /// </para>
    /// </summary>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    public static T Evaluate<T>(this IWorker worker, string expression, object? arg = null)
    {
        return worker.EvaluateAsync<T>(expression, arg).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>Returns the return value of <paramref name="expression"/> as a <see cref="IJSHandle"/>.</para>
    /// <para>
    /// The only difference between <see cref="IWorker.EvaluateAsync"/> and <see cref="IWorker.EvaluateHandleAsync"/>
    /// is that <see cref="IWorker.EvaluateHandleAsync"/> returns <see cref="IJSHandle"/>.
    /// </para>
    /// <para>
    /// If the function passed to the <see cref="IWorker.EvaluateHandleAsync"/> returns
    /// a <see cref="Task"/>, then <see cref="IWorker.EvaluateHandleAsync"/> would wait
    /// for the promise to resolve and return its value.
    /// </para>
    /// </summary>
    /// <param name="expression">
    /// JavaScript expression to be evaluated in the browser context. If it looks like a
    /// function declaration, it is interpreted as a function. Otherwise, evaluated as an
    /// expression.
    /// </param>
    /// <param name="arg">Optional argument to pass to <paramref name="expression"/>.</param>
    public static IJSHandle EvaluateHandle(this IWorker worker, string expression, object? arg = null)
    {
        return worker.EvaluateHandleAsync(expression, arg).GetAwaiter().GetResult();
    }
}
