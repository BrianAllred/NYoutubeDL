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

namespace NYoutubeDL.Options
{
    #region Using

    using Helpers;

    #endregion

    /// <summary>
    ///     Object containing Download parameters
    /// </summary>
    public class Download : OptionSection
    {
        [Option] private readonly BoolOption abortOnUnvailableFragment = new BoolOption("--abort-on-unavailable-fragment");
        [Option] private readonly FileSizeRateOption bufferSize = new FileSizeRateOption("--buffer-size");

        [Option] private readonly EnumOption<Enums.ExternalDownloader> externalDownloader =
            new EnumOption<Enums.ExternalDownloader>("--external-downloader");

        [Option] private readonly StringOption externalDownloaderArgs = new StringOption("--external-downloader-args");
        [Option] private readonly IntOption fragmentRetries = new IntOption("--fragment-retries", true);
        [Option] private readonly BoolOption hlsPreferFfmpeg = new BoolOption("--hls-prefer-ffmpeg");
        [Option] private readonly BoolOption hlsPreferNative = new BoolOption("--hls-prefer-native");
        [Option] private readonly BoolOption hlsUseMpegts = new BoolOption("--hls-use-mpegts");
        [Option] private readonly FileSizeRateOption limitRate = new FileSizeRateOption("-r");
        [Option] private readonly BoolOption noResizeBuffer = new BoolOption("--no-resize-buffer");
        [Option] private readonly BoolOption playlistRandom = new BoolOption("--playlist-random");
        [Option] private readonly BoolOption playlistReverse = new BoolOption("--playlist-reverse");
        [Option] private readonly IntOption retries = new IntOption("-R", true);
        [Option] private readonly BoolOption skipUnavailableFragments = new BoolOption("--skip-unavailable-fragments");
        [Option] private readonly BoolOption xattrSetFilesize = new BoolOption("--xattr-set-filesize");

        /// <summary>
        ///     --abort-on-unavailable-fragment
        /// </summary>
        public bool AbortOnUnavailableFragment
        {
            get { return this.abortOnUnvailableFragment.Value ?? false; }
            set { this.abortOnUnvailableFragment.Value = value; }
        }

        /// <summary>
        ///     --buffer-size
        /// </summary>
        public FileSizeRate BufferSize
        {
            get { return this.bufferSize.Value; }
            set { this.bufferSize.Value = value; }
        }

        /// <summary>
        ///     --external-downloader
        /// </summary>
        public Enums.ExternalDownloader ExternalDownloader
        {
            get
            {
                return this.externalDownloader.Value == null
                    ? Enums.ExternalDownloader.undefined
                    : (Enums.ExternalDownloader) this.externalDownloader.Value;
            }
            set { this.externalDownloader.Value = (int) value; }
        }

        /// <summary>
        ///     --external-downloader-args
        /// </summary>
        public string ExternalDownloaderArgs
        {
            get { return this.externalDownloaderArgs.Value; }
            set { this.externalDownloaderArgs.Value = value; }
        }

        /// <summary>
        ///     --fragment-retries
        /// </summary>
        public int FragmentRetries
        {
            get { return this.fragmentRetries.Value ?? 10; }
            set { this.fragmentRetries.Value = value; }
        }

        /// <summary>
        ///     --hls-prefer-ffmpeg
        /// </summary>
        public bool HlsPreferFfmpeg
        {
            get { return this.hlsPreferFfmpeg.Value ?? false; }
            set { this.hlsPreferFfmpeg.Value = value; }
        }

        /// <summary>
        ///     --hls-prefer-native
        /// </summary>
        public bool HlsPreferNative
        {
            get { return this.hlsPreferNative.Value ?? false; }
            set { this.hlsPreferNative.Value = value; }
        }

        /// <summary>
        ///     --hls-use-mpegts
        /// </summary>
        public bool HlsUseMpegts
        {
            get { return this.hlsUseMpegts.Value ?? false; }
            set { this.hlsUseMpegts.Value = value; }
        }

        /// <summary>
        ///     -r
        /// </summary>
        public FileSizeRate LimitRate
        {
            get { return this.limitRate.Value; }
            set { this.limitRate.Value = value; }
        }

        /// <summary>
        ///     --no-resize-buffer
        /// </summary>
        public bool NoResizeBuffer
        {
            get { return this.noResizeBuffer.Value ?? false; }
            set { this.noResizeBuffer.Value = value; }
        }

        /// <summary>
        ///     --playlist-random
        /// </summary>
        public bool PlaylistRandom
        {
            get { return this.playlistRandom.Value ?? false; }
            set { this.playlistRandom.Value = value; }
        }

        /// <summary>
        ///     --playlist-reverse
        /// </summary>
        public bool PlaylistReverse
        {
            get { return this.playlistReverse.Value ?? false; }
            set { this.playlistReverse.Value = value; }
        }

        /// <summary>
        ///     -R
        /// </summary>
        public int Retries
        {
            get { return this.retries.Value ?? 10; }
            set { this.retries.Value = value; }
        }

        /// <summary>
        ///     --skip-unavailable-fragments
        /// </summary>
        public bool SkipUnavailableFragments
        {
            get { return this.skipUnavailableFragments.Value ?? false; }
            set { this.skipUnavailableFragments.Value = value; }
        }

        /// <summary>
        ///     --xattr-set-filesize
        /// </summary>
        public bool XattrSetFilesize
        {
            get { return this.xattrSetFilesize.Value ?? false; }
            set { this.xattrSetFilesize.Value = value; }
        }
    }
}