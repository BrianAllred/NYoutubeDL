// Copyright 2018 Brian Allred
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

namespace NYoutubeDL.Services
{
    #region Using

    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Helpers;

    #endregion

    /// <summary>
    ///     Service containing download functionality
    /// </summary>
    internal static class DownloadService
    {
        /// <summary>
        ///     Asynchronously download a video/playlist
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        internal static async Task DownloadAsync(this YoutubeDL ydl)
        {
            if (ydl.processStartInfo == null)
            {
                await PreparationService.PrepareDownloadAsync(ydl);

                if (ydl.processStartInfo == null)
                {
                    throw new NullReferenceException();
                }
            }

            SetupDownload(ydl);

            await ydl.process.WaitForExitAsync();
        }

        /// <summary>
        ///     Synchronously download a video/playlist
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        internal static void Download(this YoutubeDL ydl)
        {
            if (ydl.processStartInfo == null)
            {
                PreparationService.PrepareDownload(ydl);

                if (ydl.processStartInfo == null)
                {
                    throw new NullReferenceException();
                }
            }

            SetupDownload(ydl);

            ydl.process.WaitForExit();
        }

        /// <summary>
        ///     Asynchronously download a video/playlist
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="url">
        ///     The video / playlist URL to download
        /// </param>
        internal static async Task DownloadAsync(this YoutubeDL ydl, string url)
        {
            ydl.VideoUrl = url;
            await DownloadAsync(ydl);
        }

        /// <summary>
        ///     Synchronously download a video/playlist
        /// </summary>
        /// <param name="ydl">
        ///     The client
        /// </param>
        /// <param name="url">
        ///     The video / playlist URL to download
        /// </param>
        internal static void Download(this YoutubeDL ydl, string url)
        {
            ydl.VideoUrl = url;
            Download(ydl);
        }

        private static void SetupDownload(YoutubeDL ydl)
        {
            ydl.process = new Process { StartInfo = ydl.processStartInfo, EnableRaisingEvents = true };

            ydl.stdOutputTokenSource = new CancellationTokenSource();
            ydl.stdErrorTokenSource = new CancellationTokenSource();

            ydl.process.Exited += (sender, args) => ydl.KillProcess();

            // Note that synchronous calls are needed in order to process the output line by line.
            // Asynchronous output reading results in batches of output lines coming in all at once.
            // The following two threads convert synchronous output reads into asynchronous events.

            ThreadPool.QueueUserWorkItem(ydl.StandardOutput, ydl.stdOutputTokenSource.Token);
            ThreadPool.QueueUserWorkItem(ydl.StandardError, ydl.stdErrorTokenSource.Token);

            if (ydl.Info != null)
            {
                ydl.StandardOutputEvent += (sender, output) => ydl.Info.ParseOutput(sender, output.Trim());
                ydl.StandardErrorEvent += (sender, output) => ydl.Info.ParseError(sender, output.Trim());
            }

            ydl.process.Start();
        }
    }
}