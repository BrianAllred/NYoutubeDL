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

    using System;
    using Helpers;

    #endregion

    /// <summary>
    ///     Object containing Video Selection parameters
    /// </summary>
    public class VideoSelection : OptionSection
    {
        [Option] private readonly IntOption ageLimit = new IntOption("--age-limit");
        [Option] private readonly DateTimeOption date = new DateTimeOption("--date");
        [Option] private readonly DateTimeOption dateAfter = new DateTimeOption("--dateafter");
        [Option] private readonly DateTimeOption dateBefore = new DateTimeOption("--datebefore");
        [Option] private readonly StringOption downloadArchive = new StringOption("--download-archive");
        [Option] private readonly BoolOption includeAds = new BoolOption("--include-ads");
        [Option] private readonly StringOption matchFilter = new StringOption("--match-filter");
        [Option] private readonly StringOption matchTitle = new StringOption("--match-title");
        [Option] private readonly IntOption maxDownloads = new IntOption("--max-downloads");
        [Option] private readonly FileSizeRateOption maxFileSize = new FileSizeRateOption("--max-filesize");
        [Option] private readonly IntOption maxViews = new IntOption("--max-views");
        [Option] private readonly FileSizeRateOption minFileSize = new FileSizeRateOption("--min-filesize");
        [Option] private readonly IntOption minViews = new IntOption("--min-views");
        [Option] private readonly BoolOption noPlaylist = new BoolOption("--no-playlist");
        [Option] private readonly IntOption playlistEnd = new IntOption("--playlist-end");
        [Option] private readonly StringOption playlistItems = new StringOption("--playlist-items");
        [Option] private readonly IntOption playlistStart = new IntOption("--playlist-start");
        [Option] private readonly StringOption rejectTitle = new StringOption("--reject-title");
        [Option] private readonly BoolOption yesPlaylist = new BoolOption("--yes-playlist");

        /// <summary>
        ///     --age-limit
        /// </summary>
        public int AgeLimit
        {
            get { return this.ageLimit.Value ?? -1; }
            set { this.ageLimit.Value = value; }
        }

        /// <summary>
        ///     --date
        /// </summary>
        public DateTime Date
        {
            get { return this.date.Value ?? new DateTime(); }
            set { this.date.Value = value; }
        }

        /// <summary>
        ///     --dateafter
        /// </summary>
        public DateTime DateAfter
        {
            get { return this.dateAfter.Value ?? new DateTime(); }
            set { this.dateAfter.Value = value; }
        }

        /// <summary>
        ///     --datebefore
        /// </summary>
        public DateTime DateBefore
        {
            get { return this.dateBefore.Value ?? new DateTime(); }
            set { this.dateBefore.Value = value; }
        }

        /// <summary>
        ///     --download-archive
        /// </summary>
        public string DownloadArchive
        {
            get { return this.downloadArchive.Value; }
            set { this.downloadArchive.Value = value; }
        }

        /// <summary>
        ///     [Experimental]
        ///     --include-ads
        /// </summary>
        public bool IncludeAds
        {
            get { return this.includeAds.Value ?? false; }
            set { this.includeAds.Value = value; }
        }

        /// <summary>
        ///     --match-filter
        /// </summary>
        public string MatchFilter
        {
            get { return this.matchFilter.Value; }
            set { this.matchFilter.Value = value; }
        }

        /// <summary>
        ///     --match-title
        /// </summary>
        public string MatchTitle
        {
            get { return this.matchTitle.Value; }
            set { this.matchTitle.Value = value; }
        }

        /// <summary>
        ///     --max-downloads
        /// </summary>
        public int MaxDownloads
        {
            get { return this.maxDownloads.Value ?? -1; }
            set { this.maxDownloads.Value = value; }
        }

        /// <summary>
        ///     --max-filesize
        /// </summary>
        public FileSizeRate MaxFileSize
        {
            get { return this.maxFileSize.Value; }
            set { this.maxFileSize.Value = value; }
        }

        /// <summary>
        ///     --max-views
        /// </summary>
        public int MaxViews
        {
            get { return this.maxViews.Value ?? -1; }
            set { this.maxViews.Value = value; }
        }

        /// <summary>
        ///     --min-filesize
        /// </summary>
        public FileSizeRate MinFileSize
        {
            get { return this.minFileSize.Value; }
            set { this.minFileSize.Value = value; }
        }

        /// <summary>
        ///     --min-views
        /// </summary>
        public int MinViews
        {
            get { return this.minViews.Value ?? -1; }
            set { this.minViews.Value = value; }
        }

        /// <summary>
        ///     --no-playlist
        /// </summary>
        public bool NoPlaylist
        {
            get { return this.noPlaylist.Value ?? false; }
            set { this.noPlaylist.Value = value; }
        }

        /// <summary>
        ///     --playlist-end
        /// </summary>
        public int PlaylistEnd
        {
            get { return this.playlistEnd.Value ?? -1; }
            set { this.playlistEnd.Value = value; }
        }

        /// <summary>
        ///     --playlist-items
        /// </summary>
        public string PlaylistItems
        {
            get { return this.playlistItems.Value; }
            set { this.playlistItems.Value = value; }
        }

        /// <summary>
        ///     --playlist-start
        /// </summary>
        public int PlaylistStart
        {
            get { return this.playlistStart.Value ?? 1; }
            set { this.playlistStart.Value = value; }
        }

        /// <summary>
        ///     --reject-title
        /// </summary>
        public string RejectTitle
        {
            get { return this.rejectTitle.Value; }
            set { this.rejectTitle.Value = value; }
        }

        /// <summary>
        ///     --yes-playlist
        /// </summary>
        public bool YesPlaylist
        {
            get { return this.yesPlaylist.Value ?? false; }
            set { this.yesPlaylist.Value = value; }
        }
    }
}