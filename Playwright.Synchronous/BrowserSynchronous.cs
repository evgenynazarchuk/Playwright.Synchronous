﻿/*
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

public static class BrowserSynchronous
{
    public static IBrowser Close(this IBrowser browser)
    {
        browser.CloseAsync().GetAwaiter().GetResult();
        return browser;
    }

    public static void Dispose(this IBrowser browser)
    {
        browser.DisposeAsync().GetAwaiter().GetResult();
    }

    public static IBrowserContext NewContext(this IBrowser browser, BrowserNewContextOptions? options = null)
    {
        return browser.NewContextAsync(options).GetAwaiter().GetResult();
    }

    public static IPage NewPage(this IBrowser browser, BrowserNewPageOptions? options = null)
    {
        return browser.NewPageAsync(options).GetAwaiter().GetResult();
    }
}
