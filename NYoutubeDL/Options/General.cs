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
    ///     Object containing General parameters
    /// </summary>
    public class General : OptionSection
    {
        [Option] private readonly BoolOption abortOnError = new BoolOption("--abort-on-error");
        [Option] private readonly StringOption configLocation = new StringOption("--config-location");
        [Option] private readonly StringOption defaultSearch = new StringOption("--default-search");
        [Option] private readonly BoolOption dumpUserAgent = new BoolOption("--dump-user-agent");
        [Option] private readonly BoolOption extractorDescriptions = new BoolOption("--extractor-descriptions");
        [Option] private readonly BoolOption flatPlaylist = new BoolOption("--flat-playlist");
        [Option] private readonly BoolOption forceGenericExtractor = new BoolOption("--force-generic-extractor");
        [Option] private readonly BoolOption ignoreConfig = new BoolOption("--ignore-config");
        [Option] private readonly BoolOption ignoreErrors = new BoolOption("-i");
        [Option] private readonly BoolOption listExtractors = new BoolOption("--list-extractors");
        [Option] private readonly BoolOption markWatched = new BoolOption("--mark-watched");
        [Option] private readonly BoolOption noColor = new BoolOption("--no-color");
        [Option] private readonly BoolOption noMarkWatched = new BoolOption("--no-mark-watched");
        [Option] private readonly BoolOption update = new BoolOption("-U");

        /// <summary>
        ///     --abort-on-error
        /// </summary>
        public bool AbortOnError
        {
            get { return this.abortOnError.Value ?? false; }
            set { this.abortOnError.Value = value; }
        }

        /// <summary>
        ///     --config-location
        /// </summary>
        public string ConfigLocation
        {
            get { return this.configLocation.Value; }
            set { this.configLocation.Value = value; }
        }

        /// <summary>
        ///     --default-search
        /// </summary>
        public string DefaultSearch
        {
            get { return this.defaultSearch.Value; }
            set { this.defaultSearch.Value = value; }
        }

        /// <summary>
        ///     --dump-user-agent
        /// </summary>
        public bool DumpUserAgent
        {
            get { return this.dumpUserAgent.Value ?? false; }
            set { this.dumpUserAgent.Value = value; }
        }

        /// <summary>
        ///     --extractor-descriptions
        /// </summary>
        public bool ExtractorDescriptions
        {
            get { return this.extractorDescriptions.Value ?? false; }
            set { this.extractorDescriptions.Value = value; }
        }

        /// <summary>
        ///     --flat-playlist
        /// </summary>
        public bool FlatPlaylist
        {
            get { return this.flatPlaylist.Value ?? false; }
            set { this.flatPlaylist.Value = value; }
        }

        /// <summary>
        ///     --force-generic-extractor
        /// </summary>
        public bool ForceGenericExtractor
        {
            get { return this.forceGenericExtractor.Value ?? false; }
            set { this.forceGenericExtractor.Value = value; }
        }

        /// <summary>
        ///     --ignore-config
        /// </summary>
        public bool IgnoreConfig
        {
            get { return this.ignoreConfig.Value ?? false; }
            set { this.ignoreConfig.Value = value; }
        }

        /// <summary>
        ///     -i
        /// </summary>
        public bool IgnoreErrors
        {
            get { return this.ignoreErrors.Value ?? false; }
            set { this.ignoreErrors.Value = value; }
        }

        /// <summary>
        ///     --list-extractors
        /// </summary>
        public bool ListExtractors
        {
            get { return this.listExtractors.Value ?? false; }
            set { this.listExtractors.Value = value; }
        }

        /// <summary>
        ///     --mark-watched
        /// </summary>
        public bool MarkWatched
        {
            get { return this.markWatched.Value ?? false; }
            set { this.markWatched.Value = value; }
        }

        /// <summary>
        ///     --no-color
        /// </summary>
        public bool NoColor
        {
            get { return this.noColor.Value ?? false; }
            set { this.noColor.Value = value; }
        }

        /// <summary>
        ///     --no-mark-watched
        /// </summary>
        public bool NoMarkWatched
        {
            get { return this.noMarkWatched.Value ?? false; }
            set { this.noMarkWatched.Value = value; }
        }

        /// <summary>
        ///     -U
        /// </summary>
        public bool Update
        {
            get { return this.update.Value ?? false; }
            set { this.update.Value = value; }
        }
    }
}