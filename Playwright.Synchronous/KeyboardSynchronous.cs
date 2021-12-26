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

public static class KeyboardSynchronous
{
    /// <summary>
    /// <para>Dispatches a <c>keydown</c> event.</para>
    /// <para>
    /// <paramref name="key"/> can specify the intended <a href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key">keyboardEvent.key</a>
    /// value or a single character to generate the text for. A superset of the <paramref
    /// name="key"/> values can be found <a href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key/Key_Values">here</a>.
    /// Examples of the keys are:
    /// </para>
    /// <para>
    /// <c>F1</c> - <c>F12</c>, <c>Digit0</c>- <c>Digit9</c>, <c>KeyA</c>- <c>KeyZ</c>,
    /// <c>Backquote</c>, <c>Minus</c>, <c>Equal</c>, <c>Backslash</c>, <c>Backspace</c>,
    /// <c>Tab</c>, <c>Delete</c>, <c>Escape</c>, <c>ArrowDown</c>, <c>End</c>, <c>Enter</c>,
    /// <c>Home</c>, <c>Insert</c>, <c>PageDown</c>, <c>PageUp</c>, <c>ArrowRight</c>, <c>ArrowUp</c>,
    /// etc.
    /// </para>
    /// <para>
    /// Following modification shortcuts are also supported: <c>Shift</c>, <c>Control</c>,
    /// <c>Alt</c>, <c>Meta</c>, <c>ShiftLeft</c>.
    /// </para>
    /// <para>
    /// Holding down <c>Shift</c> will type the text that corresponds to the <paramref name="key"/>
    /// in the upper case.
    /// </para>
    /// <para>
    /// If <paramref name="key"/> is a single character, it is case-sensitive, so the values
    /// <c>a</c> and <c>A</c> will generate different respective texts.
    /// </para>
    /// <para>
    /// If <paramref name="key"/> is a modifier key, <c>Shift</c>, <c>Meta</c>, <c>Control</c>,
    /// or <c>Alt</c>, subsequent key presses will be sent with that modifier active. To
    /// release the modifier key, use <see cref="IKeyboard.UpAsync"/>.
    /// </para>
    /// <para>
    /// After the key is pressed once, subsequent calls to <see cref="IKeyboard.DownAsync"/>
    /// will have <a href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/repeat">repeat</a>
    /// set to true. To release the key, use <see cref="IKeyboard.UpAsync"/>.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Modifier keys DO influence <c>keyboard.down</c>. Holding down <c>Shift</c> will
    /// type the text in upper case.
    /// </para>
    /// </remarks>
    /// <param name="key">
    /// Name of the key to press or a character to generate, such as <c>ArrowLeft</c> or
    /// <c>a</c>.
    /// </param>
    public static IKeyboard Down(this IKeyboard keyboard, string key)
    {
        keyboard.DownAsync(key).GetAwaiter().GetResult();
        return keyboard;
    }

    /// <summary>
    /// <para>
    /// Dispatches only <c>input</c> event, does not emit the <c>keydown</c>, <c>keyup</c>
    /// or <c>keypress</c> events.
    /// </para>
    /// <code>await page.Keyboard.PressAsync("嗨");</code>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Modifier keys DO NOT effect <c>keyboard.insertText</c>. Holding down <c>Shift</c>
    /// will not type the text in upper case.
    /// </para>
    /// </remarks>
    /// <param name="text">Sets input to the specified text value.</param>
    public static IKeyboard InsertText(this IKeyboard keyboard, string text)
    {
        keyboard.InsertTextAsync(text).GetAwaiter().GetResult();
        return keyboard;
    }

    /// <summary>
    /// <para>
    /// <paramref name="key"/> can specify the intended <a href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key">keyboardEvent.key</a>
    /// value or a single character to generate the text for. A superset of the <paramref
    /// name="key"/> values can be found <a href="https://developer.mozilla.org/en-US/docs/Web/API/KeyboardEvent/key/Key_Values">here</a>.
    /// Examples of the keys are:
    /// </para>
    /// <para>
    /// <c>F1</c> - <c>F12</c>, <c>Digit0</c>- <c>Digit9</c>, <c>KeyA</c>- <c>KeyZ</c>,
    /// <c>Backquote</c>, <c>Minus</c>, <c>Equal</c>, <c>Backslash</c>, <c>Backspace</c>,
    /// <c>Tab</c>, <c>Delete</c>, <c>Escape</c>, <c>ArrowDown</c>, <c>End</c>, <c>Enter</c>,
    /// <c>Home</c>, <c>Insert</c>, <c>PageDown</c>, <c>PageUp</c>, <c>ArrowRight</c>, <c>ArrowUp</c>,
    /// etc.
    /// </para>
    /// <para>
    /// Following modification shortcuts are also supported: <c>Shift</c>, <c>Control</c>,
    /// <c>Alt</c>, <c>Meta</c>, <c>ShiftLeft</c>.
    /// </para>
    /// <para>
    /// Holding down <c>Shift</c> will type the text that corresponds to the <paramref name="key"/>
    /// in the upper case.
    /// </para>
    /// <para>
    /// If <paramref name="key"/> is a single character, it is case-sensitive, so the values
    /// <c>a</c> and <c>A</c> will generate different respective texts.
    /// </para>
    /// <para>
    /// Shortcuts such as <c>key: "Control+o"</c> or <c>key: "Control+Shift+T"</c> are supported
    /// as well. When specified with the modifier, modifier is pressed and being held while
    /// the subsequent key is being pressed.
    /// </para>
    /// <code>
    /// await page.GotoAsync("https://keycode.info");<br/>
    /// await page.Keyboard.PressAsync("A");<br/>
    /// await page.ScreenshotAsync(new PageScreenshotOptions { Path = "A.png" });<br/>
    /// await page.Keyboard.PressAsync("ArrowLeft");<br/>
    /// await page.ScreenshotAsync(new PageScreenshotOptions { Path = "ArrowLeft.png" });<br/>
    /// await page.Keyboard.PressAsync("Shift+O");<br/>
    /// await page.ScreenshotAsync(new PageScreenshotOptions { Path = "O.png" });<br/>
    /// await browser.CloseAsync();
    /// </code>
    /// <para>Shortcut for <see cref="IKeyboard.DownAsync"/> and <see cref="IKeyboard.UpAsync"/>.</para>
    /// </summary>
    /// <param name="key">
    /// Name of the key to press or a character to generate, such as <c>ArrowLeft</c> or
    /// <c>a</c>.
    /// </param>
    /// <param name="options">Call options</param>
    public static IKeyboard Press(this IKeyboard keyboard, string key, KeyboardPressOptions? options = default)
    {
        keyboard.PressAsync(key, options).GetAwaiter().GetResult();
        return keyboard;
    }

    /// <summary>
    /// <para>
    /// Sends a <c>keydown</c>, <c>keypress</c>/<c>input</c>, and <c>keyup</c> event for
    /// each character in the text.
    /// </para>
    /// <para>To press a special key, like <c>Control</c> or <c>ArrowDown</c>, use <see cref="IKeyboard.PressAsync"/>.</para>
    /// <code>
    /// await page.Keyboard.TypeAsync("Hello"); // types instantly<br/>
    /// await page.Keyboard.TypeAsync("World", new KeyboardTypeOptions { Delay = 100 }); // types slower, like a user
    /// </code>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Modifier keys DO NOT effect <c>keyboard.type</c>. Holding down <c>Shift</c> will
    /// not type the text in upper case.
    /// </para>
    /// <para>
    /// For characters that are not on a US keyboard, only an <c>input</c> event will be
    /// sent.
    /// </para>
    /// </remarks>
    /// <param name="text">A text to type into a focused element.</param>
    /// <param name="options">Call options</param>
    public static IKeyboard Type(this IKeyboard keyboard, string text, KeyboardTypeOptions? options = default)
    {
        keyboard.TypeAsync(text, options).GetAwaiter().GetResult();
        return keyboard;
    }

    /// <summary><para>Dispatches a <c>keyup</c> event.</para></summary>
    /// <param name="key">
    /// Name of the key to press or a character to generate, such as <c>ArrowLeft</c> or
    /// <c>a</c>.
    /// </param>
    public static IKeyboard Up(this IKeyboard keyboard, string key)
    {
        keyboard.UpAsync(key).GetAwaiter().GetResult();
        return keyboard;
    }
}
