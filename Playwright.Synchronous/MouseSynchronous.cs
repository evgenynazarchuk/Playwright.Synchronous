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

public static class MouseSynchronous
{
    /// <summary>
    /// <para>
    /// Shortcut for <see cref="IMouse.MoveAsync"/>, <see cref="IMouse.DownAsync"/>, <see
    /// cref="IMouse.UpAsync"/>.
    /// </para>
    /// </summary>
    /// <param name="x">
    /// </param>
    /// <param name="y">
    /// </param>
    /// <param name="options">Call options</param>
    public static IMouse Click(this IMouse mouse, float x, float y, MouseClickOptions? options = default)
    {
        mouse.ClickAsync(x, y, options).GetAwaiter().GetResult();
        return mouse;
    }

    /// <summary>
    /// <para>
    /// Shortcut for <see cref="IMouse.MoveAsync"/>, <see cref="IMouse.DownAsync"/>, <see
    /// cref="IMouse.UpAsync"/>, <see cref="IMouse.DownAsync"/> and <see cref="IMouse.UpAsync"/>.
    /// </para>
    /// </summary>
    /// <param name="x">
    /// </param>
    /// <param name="y">
    /// </param>
    /// <param name="options">Call options</param>
    public static IMouse DblClick(this IMouse mouse, float x, float y, MouseDblClickOptions? options = default)
    {
        mouse.DblClickAsync(x, y, options).GetAwaiter().GetResult();
        return mouse;
    }

    /// <summary><para>Dispatches a <c>mousedown</c> event.</para></summary>
    /// <param name="options">Call options</param>
    public static IMouse Down(this IMouse mouse, MouseDownOptions? options = default)
    {
        mouse.DownAsync(options).GetAwaiter().GetResult();
        return mouse;
    }

    /// <summary><para>Dispatches a <c>mousemove</c> event.</para></summary>
    /// <param name="x">
    /// </param>
    /// <param name="y">
    /// </param>
    /// <param name="options">Call options</param>
    public static IMouse Move(this IMouse mouse, float x, float y, MouseMoveOptions? options = default)
    {
        mouse.MoveAsync(x, y, options).GetAwaiter().GetResult();
        return mouse;
    }

    /// <summary><para>Dispatches a <c>mouseup</c> event.</para></summary>
    /// <param name="options">Call options</param>
    public static IMouse Up(this IMouse mouse, MouseUpOptions? options = default)
    {
        mouse.UpAsync(options).GetAwaiter().GetResult();
        return mouse;
    }

    /// <summary><para>Dispatches a <c>wheel</c> event.</para></summary>
    /// <remarks>
    /// <para>
    /// Wheel events may cause scrolling if they are not handled, and this method does not
    /// wait for the scrolling to finish before returning.
    /// </para>
    /// </remarks>
    /// <param name="deltaX">Pixels to scroll horizontally.</param>
    /// <param name="deltaY">Pixels to scroll vertically.</param>
    public static IMouse Wheel(this IMouse mouse, float deltaX, float deltaY)
    {
        mouse.WheelAsync(deltaX, deltaY).GetAwaiter().GetResult();
        return mouse;
    }
}
