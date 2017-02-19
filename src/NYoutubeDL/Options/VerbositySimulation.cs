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
    /// Object containing Verbosity and Simulation parameters
    /// </summary>
    public class VerbositySimulation : OptionSection
    {
        [Option] private readonly BoolOption callHome = new BoolOption("-C");
        [Option] private readonly BoolOption consoleTitle = new BoolOption("--console-title");
        [Option] private readonly BoolOption dumpJson = new BoolOption("-j");
        [Option] private readonly BoolOption dumpPages = new BoolOption("--dump-pages");
        [Option] private readonly BoolOption dumpSingleJson = new BoolOption("-J");
        [Option] private readonly BoolOption getDescription = new BoolOption("--get-description");
        [Option] private readonly BoolOption getDuration = new BoolOption("--get-duration");
        [Option] private readonly BoolOption getFilename = new BoolOption("--get-filename");
        [Option] private readonly BoolOption getFormat = new BoolOption("--get-format");
        [Option] private readonly BoolOption getId = new BoolOption("--get-id");
        [Option] private readonly BoolOption getThumbnail = new BoolOption("--get-thumbnail");
        [Option] private readonly BoolOption getTitle = new BoolOption("-e");
        [Option] private readonly BoolOption getUrl = new BoolOption("-g");
        [Option] private readonly BoolOption newline = new BoolOption("--newline");
        [Option] private readonly BoolOption noCallHome = new BoolOption("--no-call-home");
        [Option] private readonly BoolOption noProgress = new BoolOption("--no-progress");
        [Option] private readonly BoolOption noWarnings = new BoolOption("--no-warnings");
        [Option] private readonly BoolOption printJson = new BoolOption("--print-jobs");
        [Option] private readonly BoolOption printTraffic = new BoolOption("--print-traffic");
        [Option] private readonly BoolOption quiet = new BoolOption("-q");
        [Option] private readonly BoolOption simulate = new BoolOption("-s");
        [Option] private readonly BoolOption skipDownload = new BoolOption("--skip-download");
        [Option] private readonly BoolOption verbose = new BoolOption("-v");
        [Option] private readonly BoolOption writePages = new BoolOption("--write-pages");

        /// <summary>
        ///     -C
        /// </summary>
        public bool CallHome
        {
            get { return this.callHome.Value ?? false; }
            set { this.callHome.Value = value; }
        }

        /// <summary>
        ///     --console-title
        /// </summary>
        public bool ConsoleTitle
        {
            get { return this.consoleTitle.Value ?? false; }
            set { this.consoleTitle.Value = value; }
        }

        /// <summary>
        ///     -j
        /// </summary>
        public bool DumpJson
        {
            get { return this.dumpJson.Value ?? false; }
            set { this.dumpJson.Value = value; }
        }

        /// <summary>
        ///     --dump-pages
        /// </summary>
        public bool DumpPages
        {
            get { return this.dumpPages.Value ?? false; }
            set { this.dumpPages.Value = value; }
        }

        /// <summary>
        ///     -J
        /// </summary>
        public bool DumpSingleJson
        {
            get { return this.dumpSingleJson.Value ?? false; }
            set { this.dumpSingleJson.Value = value; }
        }

        /// <summary>
        ///     --get-description
        /// </summary>
        public bool GetDescription
        {
            get { return this.getDescription.Value ?? false; }
            set { this.getDescription.Value = value; }
        }

        /// <summary>
        ///     --get-duration
        /// </summary>
        public bool GetDuration
        {
            get { return this.getDuration.Value ?? false; }
            set { this.getDuration.Value = value; }
        }

        /// <summary>
        ///     --get-filename
        /// </summary>
        public bool GetFilename
        {
            get { return this.getFilename.Value ?? false; }
            set { this.getFilename.Value = value; }
        }

        /// <summary>
        ///     --get-format
        /// </summary>
        public bool GetFormat
        {
            get { return this.getFormat.Value ?? false; }
            set { this.getFormat.Value = value; }
        }

        /// <summary>
        ///     --get-id
        /// </summary>
        public bool GetId
        {
            get { return this.getId.Value ?? false; }
            set { this.getId.Value = value; }
        }

        /// <summary>
        ///     --get-thumbnail
        /// </summary>
        public bool GetThumbnail
        {
            get { return this.getThumbnail.Value ?? false; }
            set { this.getThumbnail.Value = value; }
        }

        /// <summary>
        ///     -e
        /// </summary>
        public bool GetTitle
        {
            get { return this.getTitle.Value ?? false; }
            set { this.getTitle.Value = value; }
        }

        /// <summary>
        ///     -g
        /// </summary>
        public bool GetUrl
        {
            get { return this.getUrl.Value ?? false; }
            set { this.getUrl.Value = value; }
        }

        /// <summary>
        ///     --newline
        /// </summary>
        public bool Newline
        {
            get { return this.newline.Value ?? false; }
            set { this.newline.Value = value; }
        }

        /// <summary>
        ///     --no-call-home
        /// </summary>
        public bool NoCallHome
        {
            get { return this.noCallHome.Value ?? false; }
            set { this.noCallHome.Value = value; }
        }

        /// <summary>
        ///     --no-progress
        /// </summary>
        public bool NoProgress
        {
            get { return this.noProgress.Value ?? false; }
            set { this.noProgress.Value = value; }
        }

        /// <summary>
        ///     --no-warnings
        /// </summary>
        public bool NoWarnings
        {
            get { return this.noWarnings.Value ?? false; }
            set { this.noWarnings.Value = value; }
        }

        /// <summary>
        ///     --print-json
        /// </summary>
        public bool PrintJson
        {
            get { return this.printJson.Value ?? false; }
            set { this.printJson.Value = value; }
        }

        /// <summary>
        ///     --print-traffic
        /// </summary>
        public bool PrintTraffic
        {
            get { return this.printTraffic.Value ?? false; }
            set { this.printTraffic.Value = value; }
        }

        /// <summary>
        ///     -q
        /// </summary>
        public bool Quiet
        {
            get { return this.quiet.Value ?? false; }
            set { this.quiet.Value = value; }
        }

        /// <summary>
        ///     -s
        /// </summary>
        public bool Simulate
        {
            get { return this.simulate.Value ?? false; }
            set { this.simulate.Value = value; }
        }

        /// <summary>
        ///     --skip-download
        /// </summary>
        public bool SkipDownload
        {
            get { return this.skipDownload.Value ?? false; }
            set { this.skipDownload.Value = value; }
        }

        /// <summary>
        ///     -v
        /// </summary>
        public bool Verbose
        {
            get { return this.verbose.Value ?? false; }
            set { this.verbose.Value = value; }
        }

        /// <summary>
        ///     --write-pages
        /// </summary>
        public bool WritePages
        {
            get { return this.writePages.Value ?? false; }
            set { this.writePages.Value = value; }
        }
    }
}