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

    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Models;

    #endregion

    /// <summary>
    ///     Service containing logic for preparing a youtube-dl command
    /// </summary>
    internal static class PreparationService
    {
        /// <summary>
        ///     Asynchronously prepare a youtube-dl command
        /// </summary>
        /// <param name="ydl">
        ///     The client.
        /// </param>
        /// <returns>
        ///     The youtube-dl command that will be executed
        /// </returns>
        internal static async Task<string> PrepareDownloadAsync(this YoutubeDL ydl)
        {
            if (ydl.Info == null)
            {
                ydl.Info = await InfoService.GetDownloadInfoAsync(ydl) ?? new DownloadInfo();
            }

            SetupPrepare(ydl);

            return ydl.RunCommand;
        }

        /// <summary>
        ///     synchronously prepare a youtube-dl command
        /// </summary>
        /// <param name="ydl">
        ///     The client.
        /// </param>
        /// <returns>
        ///     The youtube-dl command that will be executed
        /// </returns>
        internal static string PrepareDownload(this YoutubeDL ydl)
        {
            if (ydl.Info == null)
            {
                ydl.Info = InfoService.GetDownloadInfo(ydl) ?? new DownloadInfo();
            }

            SetupPrepare(ydl);

            return ydl.RunCommand;
        }

        /// <summary>
        ///     Setup a youtube-dl command using the given options
        /// </summary>
        /// <param name="ydl">
        ///     Client with configured options
        /// </param>
        internal static void SetupPrepare(YoutubeDL ydl)
        {
            string arguments = ydl.Options.ToCliParameters() + " " + ydl.VideoUrl;

            ydl.processStartInfo = new ProcessStartInfo
            {
                FileName = ydl.YoutubeDlPath,
                Arguments = arguments,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            if (string.IsNullOrWhiteSpace(ydl.processStartInfo.FileName))
            {
                throw new FileNotFoundException("youtube-dl not found on path!");
            }

            if (!File.Exists(ydl.processStartInfo.FileName))
            {
                throw new FileNotFoundException($"{ydl.processStartInfo.FileName} not found!");
            }

            ydl.RunCommand = ydl.processStartInfo.FileName + " " + ydl.processStartInfo.Arguments;
        }
    }
}