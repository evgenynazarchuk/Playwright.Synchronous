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

public static class DownloadSynchronous
{
    /// <summary>
    /// <para>
    /// Cancels a download. Will not fail if the download is already finished or canceled.
    /// Upon successful cancellations, <c>download.failure()</c> would resolve to <c>'canceled'</c>.
    /// </para>
    /// </summary>
    public static void Cancel(this IDownload download)
    {
        download.CancelAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns readable stream for current download or <c>null</c> if download failed.</para></summary>
    public static Stream? CreateReadStream(this IDownload download)
    {
        return download.CreateReadStreamAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Deletes the downloaded file. Will wait for the download to finish if necessary.</para></summary>
    public static void Delete(this IDownload download)
    {
        download.DeleteAsync().GetAwaiter().GetResult();
    }

    /// <summary><para>Returns download error if any. Will wait for the download to finish if necessary.</para></summary>
    public static string? Failure(this IDownload download)
    {
        return download.FailureAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Returns path to the downloaded file in case of successful download. The method will
    /// wait for the download to finish if necessary. The method throws when connected remotely.
    /// </para>
    /// <para>
    /// Note that the download's file name is a random GUID, use <see cref="IDownload.SuggestedFilename"/>
    /// to get suggested file name.
    /// </para>
    /// </summary>
    public static string? Path(this IDownload download)
    {
        return download.PathAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// <para>
    /// Copy the download to a user-specified path. It is safe to call this method while
    /// the download is still in progress. Will wait for the download to finish if necessary.
    /// </para>
    /// </summary>
    /// <param name="path">Path where the download should be copied.</param>
    public static void SaveAs(this IDownload download, string path)
    {
        download.SaveAsAsync(path).GetAwaiter().GetResult();
    }
}
