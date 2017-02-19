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
    ///     Object containing PostProcessing parameters
    /// </summary>
    public class PostProcessing : OptionSection
    {
        [Option] private readonly BoolOption addMetadata = new BoolOption("--add-metadata");
        [Option] private readonly EnumOption<Enums.AudioFormat> audioFormat = new EnumOption<Enums.AudioFormat>("--audio-format");
        [Option] private readonly IntOption audioQuality = new IntOption("--audio-quality");
        [Option] private readonly StringOption command = new StringOption("--exec");

        [Option] private readonly EnumOption<Enums.SubtitleFormat> convertSubs =
            new EnumOption<Enums.SubtitleFormat>("--convert-subs");

        [Option] private readonly BoolOption embedSubs = new BoolOption("--embed-subs");
        [Option] private readonly BoolOption embedThumbnail = new BoolOption("--embed-thumbnail");
        [Option] private readonly BoolOption extractAudio = new BoolOption("-x");
        [Option] private readonly StringOption ffmpegLocation = new StringOption("--ffmpeg-location");
        [Option] private readonly EnumOption<Enums.FixupPolicy> fixupPolicy = new EnumOption<Enums.FixupPolicy>("--fixup");
        [Option] private readonly BoolOption keepVideo = new BoolOption("-k");
        [Option] private readonly StringOption metadataFromTitle = new StringOption("--metadata-from-title");
        [Option] private readonly BoolOption noPostOverwrites = new BoolOption("--no-post-overwrites");
        [Option] private readonly StringOption postProcessorArgs = new StringOption("--postprocessor-args");
        [Option] private readonly BoolOption preferAvconv = new BoolOption("--prefer-avconv");
        [Option] private readonly BoolOption preferFfmpeg = new BoolOption("--prefer-ffmpeg");
        [Option] private readonly EnumOption<Enums.VideoFormat> recodeFormat = new EnumOption<Enums.VideoFormat>("--recode-video");
        [Option] private readonly BoolOption xattrs = new BoolOption("--xattrs");

        /// <summary>
        ///     --add-metadata
        /// </summary>
        public bool AddMetadata
        {
            get { return this.addMetadata.Value ?? false; }
            set { this.addMetadata.Value = value; }
        }

        /// <summary>
        ///     --audio-format
        /// </summary>
        public Enums.AudioFormat AudioFormat
        {
            get
            {
                return this.audioFormat.Value == null
                    ? Enums.AudioFormat.best
                    : (Enums.AudioFormat) this.audioFormat.Value;
            }
            set { this.audioFormat.Value = (int) value; }
        }

        /// <summary>
        ///     --audio-quality
        /// </summary>
        public int AudioQuality
        {
            get { return this.audioQuality.Value ?? 5; }
            set { this.audioFormat.Value = value; }
        }

        /// <summary>
        ///     --exec
        /// </summary>
        public string Command
        {
            get { return this.command.Value; }
            set { this.command.Value = value; }
        }

        /// <summary>
        ///     --convert-subs
        /// </summary>
        public Enums.SubtitleFormat ConvertSubs
        {
            get
            {
                return this.convertSubs.Value == null
                    ? Enums.SubtitleFormat.undefined
                    : (Enums.SubtitleFormat) this.convertSubs.Value;
            }
            set { this.convertSubs.Value = (int) value; }
        }

        /// <summary>
        ///     --embed-subs
        /// </summary>
        public bool EmbedSubs
        {
            get { return this.embedSubs.Value ?? false; }
            set { this.embedSubs.Value = value; }
        }

        /// <summary>
        ///     --embed-thumbnail
        /// </summary>
        public bool EmbedThumbnail
        {
            get { return this.embedThumbnail.Value ?? false; }
            set { this.embedThumbnail.Value = value; }
        }

        /// <summary>
        ///     -x
        /// </summary>
        public bool ExtractAudio
        {
            get { return this.extractAudio.Value ?? false; }
            set { this.extractAudio.Value = value; }
        }

        /// <summary>
        ///     --ffmpeg-location
        /// </summary>
        public string FfmpegLocation
        {
            get { return this.ffmpegLocation.Value; }
            set { this.ffmpegLocation.Value = value; }
        }

        /// <summary>
        ///     --fixup
        /// </summary>
        public Enums.FixupPolicy FixupPolicy
        {
            get
            {
                return this.fixupPolicy.Value == null
                    ? Enums.FixupPolicy.detect_or_warn
                    : (Enums.FixupPolicy) this.fixupPolicy.Value;
            }
            set { this.fixupPolicy.Value = (int) value; }
        }

        /// <summary>
        ///     -k
        /// </summary>
        public bool KeepVideo
        {
            get { return this.keepVideo.Value ?? false; }
            set { this.keepVideo.Value = value; }
        }

        /// <summary>
        ///     --metadata-from-title
        /// </summary>
        public string MetadataFromTitle
        {
            get { return this.metadataFromTitle.Value; }
            set { this.metadataFromTitle.Value = value; }
        }

        /// <summary>
        ///     --no-post-overwrites
        /// </summary>
        public bool NoPostOverwrites
        {
            get { return this.noPostOverwrites.Value ?? false; }
            set { this.noPostOverwrites.Value = value; }
        }

        /// <summary>
        ///     --postprocessor-args
        /// </summary>
        public string PostProcessorArgs
        {
            get { return this.postProcessorArgs.Value; }
            set { this.postProcessorArgs.Value = value; }
        }

        /// <summary>
        ///     --prefer-avconv
        /// </summary>
        public bool PreferAvconv
        {
            get { return this.preferAvconv.Value ?? false; }
            set { this.preferAvconv.Value = value; }
        }

        /// <summary>
        ///     --prefer-ffmpeg
        /// </summary>
        public bool PreferFfmpeg
        {
            get { return this.preferFfmpeg.Value ?? false; }
            set { this.preferFfmpeg.Value = value; }
        }

        /// <summary>
        ///     --recode-video
        /// </summary>
        public Enums.VideoFormat RecodeFormat
        {
            get
            {
                return this.recodeFormat.Value == null
                    ? Enums.VideoFormat.undefined
                    : (Enums.VideoFormat) this.recodeFormat.Value;
            }
            set { this.recodeFormat.Value = (int) value; }
        }

        /// <summary>
        ///     [Experimental]
        ///     --xattrs
        /// </summary>
        public bool Xattrs
        {
            get { return this.xattrs.Value ?? false; }
            set { this.xattrs.Value = value; }
        }
    }
}