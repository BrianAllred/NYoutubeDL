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
    using System.Text.RegularExpressions;
    using Models;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    ///     Class holding data about the current download, which is parsed from youtube-dl's standard output
    /// </summary>
    public class DownloadInfo
    {
        private const string DownloadRateString = "iB/s";
        private const string DownloadSizeString = "iB";
        private const string EtaString = "ETA";
        private const string OfString = "of";
        private const string VideoString = "video";
        private VideoInfo currentVideo;
        private PlaylistInfo playlist;

        /// <summary>
        ///     The current download rate
        /// </summary>
        public string DownloadRate { get; set; }

        /// <summary>
        ///     The collection of error messages received
        /// </summary>
        public List<string> Error { get; } = new List<string>();

        /// <summary>
        ///     The current download's estimated time remaining
        /// </summary>
        public string Eta { get; set; }

        /// <summary>
        ///     Whether the current download is of a playlist
        /// </summary>
        public bool IsPlaylistDownload { get; private set; }

        /// <summary>
        ///     The title of the playlist currently downloading
        /// </summary>
        public string PlaylistTitle { get; private set; }

        /// <summary>
        ///     The title of the video currently downloading
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     The total number of videos (if downloading a playlist)
        /// </summary>
        public int TotalVideos { get; private set; } = 1;

        /// <summary>
        ///     The current index of the video (if in a playlist)
        /// </summary>
        public int VideoIndex { get; private set; } = 1;

        /// <summary>
        ///     The current download progresss
        /// </summary>
        public double VideoProgress { get; private set; }

        /// <summary>
        ///     The current download's total size
        /// </summary>
        public string VideoSize { get; private set; }

        /// <summary>
        ///     Fires when information in this object is updated
        /// </summary>
        public event Action UpdateEvent;

        internal void ParseError(object sender, string error)
        {
            this.Error.Add(error);
            this.UpdateEvent?.Invoke();
        }

        internal void ParseOutput(object sender, string output)
        {
            if (this.playlist == null)
            {
                try
                {
                    this.playlist = JsonConvert.DeserializeObject<PlaylistInfo>(output);
                    if (!string.IsNullOrEmpty(this.playlist._type) && this.playlist._type.Equals("playlist"))
                    {
                        this.IsPlaylistDownload = true;
                        this.PlaylistTitle = this.playlist.title;
                        this.TotalVideos = this.playlist.entries.Count;

                        this.UpdateEvent?.Invoke();
                        return;
                    }
                }
                catch (JsonSerializationException ex)
                {
                }
            }

            if (this.currentVideo == null)
            {
                try
                {
                    this.currentVideo = JsonConvert.DeserializeObject<VideoInfo>(output);
                    if (!string.IsNullOrEmpty(this.currentVideo.title))
                    {
                        this.Title = this.currentVideo.title;
                        this.UpdateEvent?.Invoke();
                        return;
                    }
                }
                catch (JsonSerializationException ex)
                {
                }
            }

            if (output.Contains(VideoString) && output.Contains(OfString))
            {
                Regex regex = new Regex(".*?(\\d+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match match = regex.Match(output);
                if (match.Success)
                {
                    this.VideoIndex = int.Parse(match.Groups[1].ToString());
                    this.currentVideo = this.playlist?.entries[this.VideoIndex - 1];
                    this.Title = this.currentVideo?.title;
                }
            }

            if (output.Contains("%"))
            {
                int progressIndex = output.LastIndexOf(' ', output.IndexOf('%')) + 1;
                string progressString = output.Substring(progressIndex, output.IndexOf('%') - progressIndex);
                this.VideoProgress = double.Parse(progressString);

                int sizeIndex = output.LastIndexOf(' ', output.IndexOf(DownloadSizeString)) + 1;
                string sizeString = output.Substring(sizeIndex, output.IndexOf(DownloadSizeString) - sizeIndex + 2);
                this.VideoSize = sizeString;
            }

            if (output.Contains(DownloadRateString))
            {
                int rateIndex = output.LastIndexOf(' ', output.LastIndexOf(DownloadRateString)) + 1;
                string rateString = output.Substring(rateIndex, output.LastIndexOf(DownloadRateString) - rateIndex + 4);
                this.DownloadRate = rateString;
            }

            if (output.Contains(EtaString))
            {
                this.Eta = output.Substring(output.LastIndexOf(' ') + 1);
            }

            this.UpdateEvent?.Invoke();
        }
    }
}