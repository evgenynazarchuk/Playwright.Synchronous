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

public static class TracingSynchronous
{
    /// <summary>
    /// <para>Start tracing.</para>
    /// <code>
    /// await using var browser = playwright.Chromium.LaunchAsync();<br/>
    /// await using var context = await browser.NewContextAsync();<br/>
    /// await context.Tracing.StartAsync(new TracingStartOptions<br/>
    /// {<br/>
    ///   Screenshots: true,<br/>
    ///   Snapshots: true<br/>
    /// });<br/>
    /// var page = context.NewPageAsync();<br/>
    /// await page.GotoAsync("https://playwright.dev");<br/>
    /// await context.Tracing.StopAsync(new TracingStopOptions<br/>
    /// {<br/>
    ///   Path: "trace.zip"<br/>
    /// });
    /// </code>
    /// </summary>
    /// <param name="options">Call options</param>
    public static void Start(ITracing tracing, TracingStartOptions? options = default)
    {
        tracing.StartAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Start a new trace chunk. If you'd like to record multiple traces on the same <see
    /// cref="IBrowserContext"/>, use <see cref="ITracing.StartAsync"/> once, and then create
    /// multiple trace chunks with <see cref="ITracing.StartChunkAsync"/> and <see cref="ITracing.StopChunkAsync"/>.
    /// </para>
    /// <code>
    /// await using var browser = playwright.Chromium.LaunchAsync();<br/>
    /// await using var context = await browser.NewContextAsync();<br/>
    /// await context.Tracing.StartAsync(new TracingStartOptions<br/>
    /// {<br/>
    ///   Screenshots: true,<br/>
    ///   Snapshots: true<br/>
    /// });<br/>
    /// var page = context.NewPageAsync();<br/>
    /// await page.GotoAsync("https://playwright.dev");<br/>
    /// <br/>
    /// await context.Tracing.StartChunkAsync();<br/>
    /// await page.ClickAsync("text=Get Started");<br/>
    /// // Everything between StartChunkAsync and StopChunkAsync will be recorded in the trace.<br/>
    /// await context.Tracing.StopChunkAsync(new TracingStopChunkOptions<br/>
    /// {<br/>
    ///   Path: "trace1.zip"<br/>
    /// });<br/>
    /// <br/>
    /// await context.Tracing.StartChunkAsync();<br/>
    /// await page.GotoAsync("http://example.com");<br/>
    /// // Save a second trace file with different actions.<br/>
    /// await context.Tracing.StopChunkAsync(new TracingStopChunkOptions<br/>
    /// {<br/>
    ///   Path: "trace2.zip"<br/>
    /// });
    /// </code>
    /// </summary>
    /// <param name="options">Call options</param>
    public static void StartChunk(ITracing tracing, TracingStartChunkOptions? options = default)
    {
        tracing.StartChunkAsync(options).GetAwaiter().GetResult();
    }

    /// <summary><para>Stop tracing.</para></summary>
    /// <param name="options">Call options</param>
    public static void Stop(ITracing tracing, TracingStopOptions? options = default)
    {
        tracing.StopAsync(options).GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Stop the trace chunk. See <see cref="ITracing.StartChunkAsync"/> for more details
    /// about multiple trace chunks.
    /// </para>
    /// </summary>
    /// <param name="options">Call options</param>
    public static void StopChunk(ITracing tracing, TracingStopChunkOptions? options = default)
    {
        tracing.StopChunkAsync(options).GetAwaiter().GetResult();
    }
}
