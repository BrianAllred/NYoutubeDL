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
    ///     Object containing all of the option sections
    /// </summary>
    public class Options
    {
        public AdobePass AdobePassOptions { get; } = new AdobePass();
        public Authentication AuthenticationOptions { get; } = new Authentication();
        public Download DownloadOptions { get; } = new Download();
        public Filesystem FilesystemOptions { get; } = new Filesystem();
        public General GeneralOptions { get; } = new General();
        public Network NetworkOptions { get; } = new Network();
        public PostProcessing PostProcessingOptions { get; } = new PostProcessing();
        public Subtitle SubtitleOptions { get; } = new Subtitle();
        public ThumbnailImages ThumbnailImagesOptions { get; } = new ThumbnailImages();
        public VerbositySimulation VerbositySimulationOptions { get; } = new VerbositySimulation();
        public VideoFormat VideoFormatOptions { get; } = new VideoFormat();
        public VideoSelection VideoSelectionOptions { get; } = new VideoSelection();
        public Workarounds WorkaroundsOptions { get; } = new Workarounds();

        /// <summary>
        ///     Retrieves the options from each option section
        /// </summary>
        /// <returns>
        ///     The parameterized string of all the options
        /// </returns>
        public string ToCliParameters()
        {
            string parameters = this.AdobePassOptions.ToCliParameters() +
                                this.AuthenticationOptions.ToCliParameters() +
                                this.DownloadOptions.ToCliParameters() +
                                this.FilesystemOptions.ToCliParameters() +
                                this.GeneralOptions.ToCliParameters() +
                                this.NetworkOptions.ToCliParameters() +
                                this.PostProcessingOptions.ToCliParameters() +
                                this.SubtitleOptions.ToCliParameters() +
                                this.ThumbnailImagesOptions.ToCliParameters() +
                                this.VerbositySimulationOptions.ToCliParameters() +
                                this.VideoFormatOptions.ToCliParameters() +
                                this.VideoSelectionOptions.ToCliParameters() +
                                this.WorkaroundsOptions.ToCliParameters();

            // Remove extra spaces
            return parameters.RemoveExtraWhitespace();
        }
    }
}