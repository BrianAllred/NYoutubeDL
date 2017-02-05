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
        ///     Audio Format Types
        /// </summary>
        public enum AudioFormatType
        {
            best,

            aac,

            vorbis,

            mp3,

            m4a,

            opus,

            wav
        }

        /// <summary>
        ///     Download rate units (B, K, M)
        /// </summary>
        public enum ByteUnit
        {
            B,

            K,

            M
        }

        /// <summary>
        ///     External downloader.
        /// </summary>
        public enum ExternalDownloader
        {
            aria2c,

            curl,

            wget
        }

        /// <summary>
        ///     Fixup policy, how to treat errors when downloading.
        /// </summary>
        public enum FixupPolicy
        {
            nothing,

            warn,

            detect_or_warn
        }

        /// <summary>
        ///     Video Format Types
        /// </summary>
        public enum VideoFormatType
        {
            mp4,

            flv,

            ogg,

            webm,

            mkv,

            avi
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
        ///     Abort downloading of further videos (in the
        ///     playlist or the command line) if an error
        ///     occurs
        /// </summary>
        public bool AbortOnError { get; set; }

        /// <summary>
        ///     Do not read configuration files. When given
        ///     in the global configuration file /etc
        ///     /youtube-dl.conf: Do not read the user
        ///     configuration in ~/.config/youtube-
        ///     dl/config(%APPDATA%/youtube-dl/config.txt
        ///     on Windows)
        /// </summary>
        public bool IgnoreConfig { get; set; }

        /// <summary>
        ///     Do not extract the videos of a playlist,
        ///     only list them.
        /// </summary>
        public bool FlatPlaylist { get; set; }

        /// <summary>
        ///     Use the specified HTTP/HTTPS proxy. Pass in
        ///     an empty string for direct
        ///     connection
        /// </summary>
        public string ProxyUrl { get; set; }

        /// <summary>
        ///     Time to wait before giving up, in seconds
        /// </summary>
        public int SocketTimeout { get; set; }

        /// <summary>
        ///     Client-side IP address to bind to
        /// </summary>
        public string SourceAddress { get; set; }

        /// <summary>
        ///     Make all connections via IPv4
        /// </summary>
        public bool Ipv4 { get; set; }

        /// <summary>
        ///     Make all connections via IPv6
        /// </summary>
        public bool Ipv6 { get; set; }

        /// <summary>
        ///     Use this proxy to verify the IP address for
        ///     some Chinese sites.The default proxy
        ///     specified by ProxyUrl (or none, if the
        ///     option is not present) is used for the
        ///     actual downloading.
        /// </summary>
        public string CnVerificationProxy { get; set; }

        // TODO: video selection options

        /// <summary>
        ///     Maximum download rate in bytes per second
        ///     (e.g. 50K or 4.2M)
        /// </summary>
        public double RateLimit { get; set; }

        /// <summary>
        ///     Download rate units (none, K, or M)
        /// </summary>
        public ByteUnit RateLimitUnit { get; set; }

        /// <summary>
        ///     Number of retries (default is 10), or
        ///     "infinite" (-1).
        /// </summary>
        public int Retries { get; set; }

        /// <summary>
        ///     Size of download buffer (default is 1024)
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        ///     Unit of download buffer
        /// </summary>
        public ByteUnit BufferSizeUnit { get; set; }

        /// <summary>
        ///     Do not automatically adjust the buffer size. By default, the buffer size is automatically resized from an initial
        ///     value of BufferSize
        /// </summary>
        public bool NoResizeBuffer { get; set; }

        /// <summary>
        ///     Download playlist videos in reverse order
        /// </summary>
        public bool PlaylistReverse { get; set; }

        /// <summary>
        ///     Use the specified external downloader. Currently supports aria2c, curl, wget
        /// </summary>
        public ExternalDownloader UseExternalDownloader { get; set; }

        /// <summary>
        ///     Give the arguments to the external downloader
        /// </summary>
        public string ExternalDownloaderArgs { get; set; }

        /// <summary>
        ///     Set file xattribute ytdl.filesize with expected filesize
        /// </summary>
        public bool XAttrSetFilesize { get; set; }

        /// <summary>
        ///     Use the native HLS downloader instead of ffmpeg
        /// </summary>
        public bool HlsPreferNative { get; set; }

        /// <summary>
        ///     File containing URLs to download
        /// </summary>
        public string BatchFile { get; set; }

        /// <summary>
        ///     Output filename template.
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        ///     Restrict filenames to only ASCII
        ///     characters, and avoid "&" and spaces in
        ///     filenames
        /// </summary>
        public bool RestrictFilenames { get; set; }

        /// <summary>
        ///     Do not overwrite files
        /// </summary>
        public bool NoOverwrites { get; set; }

        /// <summary>
        ///     Force resume of partially downloaded files.
        /// </summary>
        public bool Continue { get; set; }

        /// <summary>
        ///     Do not resume partially downloaded files.
        /// </summary>
        public bool NoContinue { get; set; }

        // TODO: Finish Filesystem Options

        /// <summary>
        ///     Login with this account ID
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     Account password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Two-factor auth code
        /// </summary>
        public string TwoFactor { get; set; }

        /// <summary>
        ///     Use .netrc authentication data
        /// </summary>
        public bool NetRc { get; set; }

        /// <summary>
        ///     Video password
        /// </summary>
        public string VideoPassword { get; set; }

        /// <summary>
        ///     Convert video files to audio-only files
        /// </summary>
        public bool ExtractAudio { get; set; }

        /// <summary>
        ///     Specify audio format
        /// </summary>
        public AudioFormatType AudioFormat { get; set; }

        /// <summary>
        ///     Specify audio quality between 0 and 9 inclusive
        ///     0 is better
        /// </summary>
        public int AudioQuality { get; set; }

        /// <summary>
        ///     Specifiy CBR audio quality
        /// </summary>
        public int CustomAudioQuality { get; set; }

        /// <summary>
        ///     Encode the video to another format
        /// </summary>
        public bool RecodeVideo { get; set; }

        /// <summary>
        ///     Format for video recoding
        /// </summary>
        public VideoFormatType RecodeVideoFormat { get; set; }

        /// <summary>
        ///     Keep the video file on disk after the post-processing
        /// </summary>
        public bool KeepVideo { get; set; }

        /// <summary>
        ///     Do not overwrite post-processed files
        /// </summary>
        public bool NoPostOverwrites { get; set; }

        /// <summary>
        ///     Embed subtitles in the video
        /// </summary>
        public bool EmbedSubs { get; set; }

        /// <summary>
        ///     Embed thumbnail in the audio as cover art
        /// </summary>
        public bool EmbedThumbnail { get; set; }

        /// <summary>
        ///     Write metadata to the video file
        /// </summary>
        public bool AddMetadata { get; set; }

        /// <summary>
        ///     Write metadata to the video file's xattrs
        /// </summary>
        public bool XAttrs { get; set; }

        /// <summary>
        ///     Automatically correct known faults of the file.
        /// </summary>
        public FixupPolicy Fixup { get; set; }

        /// <summary>
        ///     Execute a command on the file after downloading,
        ///     similar to find's -exec syntax.
        ///     Example: --exec 'adb push {} /sdcard/Music/ && rm {}'
        /// </summary>
        /// <value>The cmd.</value>
        public string Cmd { get; set; }

        /// <summary>
        ///     Whether to output verbosely
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        ///     Whether we're updating the binary
        /// </summary>
        public bool Update { get; set; }

        /// <summary>
        ///     The path to the youtube-dl binary
        /// </summary>
        public string YoutubeDlPath { get; set; } = "youtube-dl";

        /// <summary>
        ///     Occurs when standard output is received.
        /// </summary>
        public event EventHandler<string> StandardOutputEvent;

        /// <summary>
        ///     Occurs when standard error is received.
        /// </summary>
        public event EventHandler<string> StandardErrorEvent;

        /// <summary>
        ///     Convert class into parameters to pass to youtube-dl process, then create and run process.
        ///     Also handle output from process.
        /// </summary>
        /// <returns>
        ///     Process created.
        /// </returns>
        public Process Download()
        {
            string arguments = string.Empty;

            if (this.Update)
            {
                arguments = "-U";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this.Output))
                {
                    throw new ArgumentException("Output can not be empty.");
                }

                if (string.IsNullOrWhiteSpace(this.VideoUrl))
                {
                    throw new ArgumentException("VideoUrl can not be empty.");
                }

                arguments += $"-o \"{this.Output}\" ";

                if (this.IgnoreConfig)
                {
                    arguments += "--ignore-config ";
                }

                if (this.AbortOnError)
                {
                    arguments += "--abort-on-error ";
                }

                if (this.FlatPlaylist)
                {
                    arguments += "--flat-playlist ";
                }

                if (!string.IsNullOrWhiteSpace(this.ProxyUrl))
                {
                    arguments += $"--proxy {this.ProxyUrl} ";
                }

                if (this.SocketTimeout != 0)
                {
                    arguments += $"--socket-timeout {this.SocketTimeout} ";
                }

                if (!string.IsNullOrWhiteSpace(this.SourceAddress))
                {
                    arguments += $"--source-address {this.SourceAddress} ";
                }

                // if Ipv4 > then - 4 > else if Ipv6 > then - 6 > else empty
                arguments += this.Ipv4 ? "-4 " : (this.Ipv6 ? "-6 " : string.Empty);

                if (this.RateLimit != 0)
                {
                    arguments += $"-r {this.RateLimit}{this.RateLimitUnit} ";
                }

                if (this.Retries < 0)
                {
                    arguments += "-R infinite ";
                }
                else
                {
                    arguments += $"-R {this.Retries} ";
                }

                if (this.BufferSize > 0)
                {
                    arguments += $"--buffer-size {this.BufferSize}{this.BufferSizeUnit} ";
                }

                if (this.PlaylistReverse)
                {
                    arguments += "--playlist-reverse ";
                }

                if (!string.IsNullOrWhiteSpace(this.BatchFile))
                {
                    arguments += $"-a {this.BatchFile} ";
                }

                if (this.RestrictFilenames)
                {
                    arguments += "--restrict-filenames ";
                }

                if (this.NoOverwrites)
                {
                    arguments += "-w ";
                }

                if (this.Continue)
                {
                    arguments += "-c ";
                }
                else if (this.NoContinue)
                {
                    arguments += "--no-continue ";
                }

                if (this.RecodeVideo)
                {
                    arguments += $"--recode-video {this.RecodeVideoFormat} ";
                }

                if (!string.IsNullOrWhiteSpace(this.Username))
                {
                    arguments += $"-u {this.Username} -p {this.Password} ";

                    if (!string.IsNullOrWhiteSpace(this.TwoFactor))
                    {
                        arguments += $"-2 {this.TwoFactor} ";
                    }
                }

                if (this.NetRc)
                {
                    arguments += "-n ";
                }

                if (!string.IsNullOrWhiteSpace(this.VideoPassword))
                {
                    arguments += $"--video-password {this.VideoPassword} ";
                }

                if (this.ExtractAudio)
                {
                    arguments += "-x ";
                }

                arguments += $"--audio-format {this.AudioFormat} ";
                arguments += "--audio-quality ";
                if (this.AudioQuality == 10)
                {
                    arguments += $"{this.CustomAudioQuality}K ";
                }

                arguments += $"{this.AudioQuality} ";

                if (this.KeepVideo)
                {
                    arguments += "-k ";
                }

                if (this.NoPostOverwrites)
                {
                    arguments += "--no-post-overwrites ";
                }

                if (this.EmbedSubs)
                {
                    arguments += "--embed-subs ";
                }

                if (this.EmbedThumbnail)
                {
                    arguments += "--embed-thumbnail ";
                }

                if (this.AddMetadata)
                {
                    arguments += "--add-metadata ";
                }

                if (this.XAttrs)
                {
                    arguments += "--xattrs ";
                }

                arguments += $"--fixup {this.Fixup} ";

                if (!string.IsNullOrWhiteSpace(this.Cmd))
                {
                    arguments += $"--exec {this.Cmd} ";
                }

                if (this.Verbose)
                {
                    arguments += "--verbose ";
                }

                arguments += this.VideoUrl;
            }

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
        ///     Kills the process and associated threads.
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
    }
}