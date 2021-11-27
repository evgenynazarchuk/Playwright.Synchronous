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
    public static void Click(this IMouse mouse, float x, float y, MouseClickOptions? options = default)
    {
        mouse.ClickAsync(x, y, options).GetAwaiter().GetResult();
    }

    public static void DblClick(this IMouse mouse, float x, float y, MouseDblClickOptions? options = default)
    {
        mouse.DblClickAsync(x, y, options).GetAwaiter().GetResult();
    }

    public static void Down(this IMouse mouse, MouseDownOptions? options = default)
    {
        mouse.DownAsync(options).GetAwaiter().GetResult();
    }

    public static void Move(this IMouse mouse, float x, float y, MouseMoveOptions? options = default)
    {
        mouse.MoveAsync(x, y, options).GetAwaiter().GetResult();
    }

    public static void Up(this IMouse mouse, MouseUpOptions? options = default)
    {
        mouse.UpAsync(options).GetAwaiter().GetResult();
    }

    public static void Wheel(this IMouse mouse, float deltaX, float deltaY)
    {
        mouse.WheelAsync(deltaX, deltaY).GetAwaiter().GetResult();
    }
}
