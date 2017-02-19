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
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

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
        ///     The options to pass to youtube-dl
        /// </summary>
        public Options.Options Options { get; } = new Options.Options();

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
        public string YoutubeDlPath { get; set; } = "youtube-dl";

        /// <summary>
        ///     Convert class into parameters to pass to youtube-dl process, then create and run process.
        ///     Also handle output from process.
        /// </summary>
        /// <returns>
        ///     Process created.
        /// </returns>
        public Process Download()
        {
            string arguments = this.Options.OptionsToCliParameters() + " " + this.VideoUrl;

            this.processStartInfo = new ProcessStartInfo
            {
                FileName = this.YoutubeDlPath,
                Arguments = arguments,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            if (!File.Exists(this.processStartInfo.FileName))
            {
                throw new FileNotFoundException($"{this.processStartInfo.FileName} not found!");
            }

            this.process = new Process {StartInfo = this.processStartInfo, EnableRaisingEvents = true};

            this.stdOutputTokenSource = new CancellationTokenSource();
            this.stdErrorTokenSource = new CancellationTokenSource();

            this.process.Exited += (sender, args) => this.KillProcess();

            this.process.Start();

            this.RunCommand = this.processStartInfo.FileName + " " + this.processStartInfo.Arguments;

            // Note that synchronous calls are needed in order to process the output line by line.
            // Asynchronous output reading results in batches of output lines coming in all at once.
            // The following two threads convert synchronous output reads into asynchronous events.

            ThreadPool.QueueUserWorkItem(this.StandardOutput, this.stdOutputTokenSource.Token);
            ThreadPool.QueueUserWorkItem(this.StandardError, this.stdErrorTokenSource.Token);

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
            return this.Download();
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

            try
            {
                if (this.process != null)
                {
                    if (!this.process.HasExited)
                    {
                        this.process.Kill();
                    }

                    this.process.Dispose();
                }
            }
            catch (InvalidOperationException)
            {
            }
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

            while (this.process != null && !this.process.HasExited && !token.IsCancellationRequested)
            {
                string error;
                if (!string.IsNullOrEmpty(error = this.process.StandardError.ReadLine()))
                {
                    this.StandardErrorEvent?.Invoke(this, error);
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

            while (this.process != null && !this.process.HasExited && !token.IsCancellationRequested)
            {
                string output;
                if (!string.IsNullOrEmpty(output = this.process.StandardOutput.ReadLine()))
                {
                    this.StandardOutputEvent?.Invoke(this, output);
                }
            }
        }

        /// <summary>
        ///     Occurs when standard output is received.
        /// </summary>
        public event EventHandler<string> StandardOutputEvent;
    }
}