// Copyright 2017 Brian Allred
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

namespace NYoutubeDL
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using Helpers;
    using Models;

    #endregion

    // ReSharper disable InconsistentNaming
    // due to following youtube-dl
    // naming conventions

    /// <summary>
    ///     C# interface for youtube-dl
    /// </summary>
    public class YoutubeDL
    {
        /// <summary>
        ///     Whether this is an information gathering process
        /// </summary>
        private bool isInfoProcess;

        /// <summary>
        ///     The youtube-dl process
        /// </summary>
        private Process process;

        /// <summary>
        ///     The process's information
        /// </summary>
        private ProcessStartInfo processStartInfo;

        /// <summary>
        ///     Cancellation token used to stop the thread processing youtube-dl's standard error output.
        /// </summary>
        private CancellationTokenSource stdErrorTokenSource;

        /// <summary>
        ///     Cancellation token used to stop the thread processing youtube-dl's standard console output.
        /// </summary>
        private CancellationTokenSource stdOutputTokenSource;

        /// <summary>
        ///     Creates a new YoutubeDL client
        /// </summary>
        public YoutubeDL()
        {
        }

        /// <summary>
        ///     Creates a new YoutubeDL client
        /// </summary>
        /// <param name="path">
        ///     Path of youtube-dl binary
        /// </param>
        public YoutubeDL(string path)
        {
            this.YoutubeDlPath = path;
        }

        /// <summary>
        ///     Information about the download
        /// </summary>
        public DownloadInfo Info { get; private set; }

        /// <summary>
        ///     The options to pass to youtube-dl
        /// </summary>
        public Options.Options Options { get; set; } = new Options.Options();

        /// <summary>
        ///     Returns whether the download process is actively running
        /// </summary>
        public bool ProcessRunning
        {
            get
            {
                try
                {
                    return !this.process.HasExited;
                }
                catch (Exception)
                {
                }

                return false;
            }
        }

        /// <summary>
        ///     Gets the complete command that was run by Download().
        /// </summary>
        /// <value>The run command.</value>
        public string RunCommand { get; private set; }

        /// <summary>
        ///     URL of video to download
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        ///     The path to the youtube-dl binary
        /// </summary>
        public string YoutubeDlPath { get; set; } = new FileInfo("youtube-dl").GetFullPath();

        /// <summary>
        ///     Whether this youtubedl client should retrieve all info
        ///     NOTE: For large playlists / many videos, this will be excruciatingly SLOW!
        /// </summary>
        public bool RetrieveAllInfo { get; set; }

        /// <summary>
        ///     Convert class into parameters to pass to youtube-dl process, then create and run process.
        ///     Also handle output from process.
        /// </summary>
        /// <param name="prepareDownload">
        ///     Whether we need to prepare the download.
        /// </param>
        /// <returns>
        ///     Process created.
        /// </returns>
        public Process Download(bool prepareDownload = false)
        {
            if (prepareDownload)
            {
                this.PrepareDownload();
            }

            this.process = new Process { StartInfo = this.processStartInfo, EnableRaisingEvents = true };

            this.stdOutputTokenSource = new CancellationTokenSource();
            this.stdErrorTokenSource = new CancellationTokenSource();

            this.process.Exited += (sender, args) => this.KillProcess();

            // Note that synchronous calls are needed in order to process the output line by line.
            // Asynchronous output reading results in batches of output lines coming in all at once.
            // The following two threads convert synchronous output reads into asynchronous events.

            ThreadPool.QueueUserWorkItem(this.StandardOutput, this.stdOutputTokenSource.Token);
            ThreadPool.QueueUserWorkItem(this.StandardError, this.stdErrorTokenSource.Token);

            if (!this.isInfoProcess && this.Info != null)
            {
                this.StandardOutputEvent += (sender, output) => this.Info.ParseOutput(sender, output.Trim());
                this.StandardErrorEvent += (sender, output) => this.Info.ParseError(sender, output.Trim());
            }

            this.process.Start();

            return this.process;
        }

        /// <summary>
        ///     Convert class into parameters to pass to youtube-dl process, then create and run process.
        ///     Also handle output from process.
        /// </summary>
        /// <param name="videoUrl">URL of video to download</param>
        /// <returns>
        ///     Process created.
        /// </returns>
        public Process Download(string videoUrl)
        {
            this.VideoUrl = videoUrl;
            return this.Download(true);
        }

        /// <summary>
        ///     Get information about the video/playlist before downloading
        /// </summary>
        /// <returns>
        ///     Object representing the information of the video/playlist
        /// </returns>
        public DownloadInfo GetDownloadInfo()
        {
            if (string.IsNullOrEmpty(this.VideoUrl))
            {
                return null;
            }

            List<DownloadInfo> infos = new List<DownloadInfo>();

            YoutubeDL infoYdl = new YoutubeDL(this.YoutubeDlPath) { VideoUrl = this.VideoUrl, isInfoProcess = true };
            infoYdl.Options.VerbositySimulationOptions.DumpSingleJson = true;
            infoYdl.Options.VerbositySimulationOptions.Simulate = true;
            infoYdl.Options.GeneralOptions.FlatPlaylist = !this.RetrieveAllInfo;
            infoYdl.Options.GeneralOptions.IgnoreErrors = true;

            // Use provided authentication in case the video is restricted
            infoYdl.Options.AuthenticationOptions.Username = this.Options.AuthenticationOptions.Username;
            infoYdl.Options.AuthenticationOptions.Password = this.Options.AuthenticationOptions.Password;
            infoYdl.Options.AuthenticationOptions.NetRc = this.Options.AuthenticationOptions.NetRc;
            infoYdl.Options.AuthenticationOptions.VideoPassword = this.Options.AuthenticationOptions.VideoPassword;
            infoYdl.Options.AuthenticationOptions.TwoFactor = this.Options.AuthenticationOptions.TwoFactor;

            infoYdl.StandardOutputEvent += (sender, output) => { infos.Add(DownloadInfo.CreateDownloadInfo(output)); };

            infoYdl.Download(true).WaitForExit();

            while (infoYdl.ProcessRunning || infos.Count == 0)
            {
                Thread.Sleep(1);
            }

            this.Info = infos.Count > 1 ? new MultiDownloadInfo(infos) : infos[0];

            return this.Info;
        }

        /// <summary>
        ///     Get information about the video/playlist before downloading
        /// </summary>
        /// <param name="url">
        ///     Video/playlist to retrieve information about
        /// </param>
        /// <returns>
        ///     Object representing the information of the video/playlist
        /// </returns>
        public DownloadInfo GetDownloadInfo(string url)
        {
            this.VideoUrl = url;
            return this.GetDownloadInfo();
        }

        /// <summary>
        ///     Kills the process and associated threads.
        ///     We catch these exceptions in case the objects have already been disposed of.
        /// </summary>
        public void KillProcess()
        {
            try
            {
                this.stdOutputTokenSource.Cancel();
                this.stdOutputTokenSource.Dispose();
            }
            catch (ObjectDisposedException)
            {
            }

            try
            {
                this.stdErrorTokenSource.Cancel();
                this.stdErrorTokenSource.Dispose();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        ///     Prepares the arguments to pass into downloader
        /// </summary>
        /// <returns>
        ///     The string of arguments built from the options
        /// </returns>
        public string PrepareDownload()
        {
            string arguments = this.Options.ToCliParameters() + " " + this.VideoUrl;

            this.processStartInfo = new ProcessStartInfo
            {
                FileName = this.YoutubeDlPath,
                Arguments = arguments,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            if (string.IsNullOrEmpty(this.processStartInfo.FileName))
            {
                throw new FileNotFoundException("youtube-dl not found on path!");
            }

            if (!File.Exists(this.processStartInfo.FileName))
            {
                throw new FileNotFoundException($"{this.processStartInfo.FileName} not found!");
            }

            if (!this.isInfoProcess)
            {
                this.Info = this.GetDownloadInfo() ?? new DownloadInfo();
            }

            this.RunCommand = this.processStartInfo.FileName + " " + this.processStartInfo.Arguments;

            return this.RunCommand;
        }

        /// <summary>
        ///     Fires the error event when error output is received from process.
        /// </summary>
        /// <param name="tokenObj">
        ///     Cancellation token
        /// </param>
        private void StandardError(object tokenObj)
        {
            CancellationToken token = (CancellationToken) tokenObj;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (this.process != null && !this.process.HasExited)
                    {
                        string error;
                        if (!string.IsNullOrEmpty(error = this.process.StandardError.ReadLine()))
                        {
                            this.StandardErrorEvent?.Invoke(this, error);
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        /// <summary>
        ///     Occurs when standard error is received.
        /// </summary>
        public event EventHandler<string> StandardErrorEvent;

        /// <summary>
        ///     Fires the output event when output is received from process.
        /// </summary>
        /// <param name="tokenObj">
        ///     Cancellation token
        /// </param>
        private void StandardOutput(object tokenObj)
        {
            CancellationToken token = (CancellationToken) tokenObj;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    if (this.process != null && !this.process.HasExited)
                    {
                        string output;
                        if (!string.IsNullOrEmpty(output = this.process.StandardOutput.ReadLine()))
                        {
                            this.StandardOutputEvent?.Invoke(this, output);
                        }
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        /// <summary>
        ///     Occurs when standard output is received.
        /// </summary>
        public event EventHandler<string> StandardOutputEvent;
    }
}