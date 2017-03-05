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
        [Option] internal readonly BoolOption callHome = new BoolOption("-C");
        [Option] internal readonly BoolOption consoleTitle = new BoolOption("--console-title");
        [Option] internal readonly BoolOption dumpJson = new BoolOption("-j");
        [Option] internal readonly BoolOption dumpPages = new BoolOption("--dump-pages");
        [Option] internal readonly BoolOption dumpSingleJson = new BoolOption("-J");
        [Option] internal readonly BoolOption getDescription = new BoolOption("--get-description");
        [Option] internal readonly BoolOption getDuration = new BoolOption("--get-duration");
        [Option] internal readonly BoolOption getFilename = new BoolOption("--get-filename");
        [Option] internal readonly BoolOption getFormat = new BoolOption("--get-format");
        [Option] internal readonly BoolOption getId = new BoolOption("--get-id");
        [Option] internal readonly BoolOption getThumbnail = new BoolOption("--get-thumbnail");
        [Option] internal readonly BoolOption getTitle = new BoolOption("-e");
        [Option] internal readonly BoolOption getUrl = new BoolOption("-g");
        [Option] internal readonly BoolOption newline = new BoolOption("--newline");
        [Option] internal readonly BoolOption noCallHome = new BoolOption("--no-call-home");
        [Option] internal readonly BoolOption noProgress = new BoolOption("--no-progress");
        [Option] internal readonly BoolOption noWarnings = new BoolOption("--no-warnings");
        [Option] internal readonly BoolOption printJson = new BoolOption("--print-jobs");
        [Option] internal readonly BoolOption printTraffic = new BoolOption("--print-traffic");
        [Option] internal readonly BoolOption quiet = new BoolOption("-q");
        [Option] internal readonly BoolOption simulate = new BoolOption("-s");
        [Option] internal readonly BoolOption skipDownload = new BoolOption("--skip-download");
        [Option] internal readonly BoolOption verbose = new BoolOption("-v");
        [Option] internal readonly BoolOption writePages = new BoolOption("--write-pages");

        /// <summary>
        ///     -C
        /// </summary>
        public bool CallHome
        {
            get { return this.callHome.Value ?? false; }
            set { this.SetField(ref this.callHome.Value, value); }
        }

        /// <summary>
        ///     --console-title
        /// </summary>
        public bool ConsoleTitle
        {
            get { return this.consoleTitle.Value ?? false; }
            set { this.SetField(ref this.consoleTitle.Value, value); }
        }

        /// <summary>
        ///     -j
        /// </summary>
        public bool DumpJson
        {
            get { return this.dumpJson.Value ?? false; }
            set { this.SetField(ref this.dumpJson.Value, value); }
        }

        /// <summary>
        ///     --dump-pages
        /// </summary>
        public bool DumpPages
        {
            get { return this.dumpPages.Value ?? false; }
            set { this.SetField(ref this.dumpPages.Value, value); }
        }

        /// <summary>
        ///     -J
        /// </summary>
        public bool DumpSingleJson
        {
            get { return this.dumpSingleJson.Value ?? false; }
            set { this.SetField(ref this.dumpSingleJson.Value, value); }
        }

        /// <summary>
        ///     --get-description
        /// </summary>
        public bool GetDescription
        {
            get { return this.getDescription.Value ?? false; }
            set { this.SetField(ref this.getDescription.Value, value); }
        }

        /// <summary>
        ///     --get-duration
        /// </summary>
        public bool GetDuration
        {
            get { return this.getDuration.Value ?? false; }
            set { this.SetField(ref this.getDuration.Value, value); }
        }

        /// <summary>
        ///     --get-filename
        /// </summary>
        public bool GetFilename
        {
            get { return this.getFilename.Value ?? false; }
            set { this.SetField(ref this.getFilename.Value, value); }
        }

        /// <summary>
        ///     --get-format
        /// </summary>
        public bool GetFormat
        {
            get { return this.getFormat.Value ?? false; }
            set { this.SetField(ref this.getFormat.Value, value); }
        }

        /// <summary>
        ///     --get-id
        /// </summary>
        public bool GetId
        {
            get { return this.getId.Value ?? false; }
            set { this.SetField(ref this.getId.Value, value); }
        }

        /// <summary>
        ///     --get-thumbnail
        /// </summary>
        public bool GetThumbnail
        {
            get { return this.getThumbnail.Value ?? false; }
            set { this.SetField(ref this.getThumbnail.Value, value); }
        }

        /// <summary>
        ///     -e
        /// </summary>
        public bool GetTitle
        {
            get { return this.getTitle.Value ?? false; }
            set { this.SetField(ref this.getTitle.Value, value); }
        }

        /// <summary>
        ///     -g
        /// </summary>
        public bool GetUrl
        {
            get { return this.getUrl.Value ?? false; }
            set { this.SetField(ref this.getUrl.Value, value); }
        }

        /// <summary>
        ///     --newline
        /// </summary>
        public bool Newline
        {
            get { return this.newline.Value ?? false; }
            set { this.SetField(ref this.newline.Value, value); }
        }

        /// <summary>
        ///     --no-call-home
        /// </summary>
        public bool NoCallHome
        {
            get { return this.noCallHome.Value ?? false; }
            set { this.SetField(ref this.noCallHome.Value, value); }
        }

        /// <summary>
        ///     --no-progress
        /// </summary>
        public bool NoProgress
        {
            get { return this.noProgress.Value ?? false; }
            set { this.SetField(ref this.noProgress.Value, value); }
        }

        /// <summary>
        ///     --no-warnings
        /// </summary>
        public bool NoWarnings
        {
            get { return this.noWarnings.Value ?? false; }
            set { this.SetField(ref this.noWarnings.Value, value); }
        }

        /// <summary>
        ///     --print-json
        /// </summary>
        public bool PrintJson
        {
            get { return this.printJson.Value ?? false; }
            set { this.SetField(ref this.printJson.Value, value); }
        }

        /// <summary>
        ///     --print-traffic
        /// </summary>
        public bool PrintTraffic
        {
            get { return this.printTraffic.Value ?? false; }
            set { this.SetField(ref this.printTraffic.Value, value); }
        }

        /// <summary>
        ///     -q
        /// </summary>
        public bool Quiet
        {
            get { return this.quiet.Value ?? false; }
            set { this.SetField(ref this.quiet.Value, value); }
        }

        /// <summary>
        ///     -s
        /// </summary>
        public bool Simulate
        {
            get { return this.simulate.Value ?? false; }
            set { this.SetField(ref this.simulate.Value, value); }
        }

        /// <summary>
        ///     --skip-download
        /// </summary>
        public bool SkipDownload
        {
            get { return this.skipDownload.Value ?? false; }
            set { this.SetField(ref this.skipDownload.Value, value); }
        }

        /// <summary>
        ///     -v
        /// </summary>
        public bool Verbose
        {
            get { return this.verbose.Value ?? false; }
            set { this.SetField(ref this.verbose.Value, value); }
        }

        /// <summary>
        ///     --write-pages
        /// </summary>
        public bool WritePages
        {
            get { return this.writePages.Value ?? false; }
            set { this.SetField(ref this.writePages.Value, value); }
        }
    }
}