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

public static class AccessibilitySynchronous
{
    /// <summary>
    /// <para>
    /// Captures the current state of the accessibility tree. The returned object represents
    /// the root accessible node of the page.
    /// </para>
    /// <para>An example of dumping the entire accessibility tree:</para>
    /// <code>
    /// var accessibilitySnapshot = await page.Accessibility.SnapshotAsync();<br/>
    /// Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(accessibilitySnapshot));
    /// </code>
    /// <para>An example of logging the focused node's name:</para>
    /// <code>
    /// var accessibilitySnapshot = await page.Accessibility.SnapshotAsync();<br/>
    /// Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(accessibilitySnapshot));
    /// </code>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Chromium accessibility tree contains nodes that go unused on most platforms
    /// and by most screen readers. Playwright will discard them as well for an easier to
    /// process tree, unless <paramref name="interestingOnly"/> is set to <c>false</c>.
    /// </para>
    /// </remarks>
    /// <param name="options">Call options</param>
    public static JsonElement? Snapshot(this IAccessibility accessibility, AccessibilitySnapshotOptions? options = default)
    {
        return accessibility.SnapshotAsync(options).GetAwaiter().GetResult();
    }
}
