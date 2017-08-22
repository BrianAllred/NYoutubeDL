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

namespace NYoutubeDL.Tests
{
    #region Using

    using System;
    using System.Text.RegularExpressions;
    using Helpers;
    using Models;
    using Options;
    using Xunit;

    #endregion

    public class Tests
    {
        [Fact]
        public void TestBoolOption()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string extractAudioOptionString = " -x ";

            ydlClient.Options.PostProcessingOptions.ExtractAudio = true;

            Assert.True(ydlClient.PrepareDownload().Contains(extractAudioOptionString));
        }

        [Fact]
        public void TestDateTimeOption()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string dateDateTimeOption = " --date 20170201 ";

            ydlClient.Options.VideoSelectionOptions.Date = new DateTime(2017, 02, 01);

            Assert.True(ydlClient.PrepareDownload().Contains(dateDateTimeOption));
        }

        [Fact]
        public void TestEnumOption()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string audioFormatEnumOption = " --audio-format mp3 ";

            ydlClient.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.mp3;

            Assert.True(ydlClient.PrepareDownload().Contains(audioFormatEnumOption));
        }

        [Fact]
        public void TestFileSizeRateOption1()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string bufferSizeFileSizeRateOption = " --buffer-size 5.5M ";

            ydlClient.Options.DownloadOptions.BufferSize = new FileSizeRate(5.5, Enums.ByteUnit.M);

            Assert.True(ydlClient.PrepareDownload().Contains(bufferSizeFileSizeRateOption));
        }

        [Fact]
        public void TestFileSizeRateOption2()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string bufferSizeFileSizeRateOption = " --buffer-size 5.5M ";

            ydlClient.Options.DownloadOptions.BufferSize = new FileSizeRate("5.5M");

            Assert.True(ydlClient.PrepareDownload().Contains(bufferSizeFileSizeRateOption));
        }

        [Fact]
        public void TestIntOption()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string socketTimeoutIntOption = " --socket-timeout 5 ";

            ydlClient.Options.NetworkOptions.SocketTimeout = 5;

            Assert.True(ydlClient.PrepareDownload().Contains(socketTimeoutIntOption));
        }

        [Fact]
        public void TestIntOptionNegativeIsInfinite()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string retriesIntOption = " -R infinite ";

            ydlClient.Options.DownloadOptions.Retries = -1;

            Assert.True(ydlClient.PrepareDownload().Contains(retriesIntOption));
        }

        [Fact]
        public void TestIsMultiDownload()
        {
            YoutubeDL ydlClient = new YoutubeDL();

            ydlClient.GetDownloadInfo(
                @"https://www.youtube.com/watch?v=dQw4w9WgXcQ https://www.youtube.com/playlist?list=PLrEnWoR732-BHrPp_Pm8_VleD68f9s14-");

            MultiDownloadInfo info = ydlClient.Info as MultiDownloadInfo;
            Assert.NotEqual(info, null);
        }

        [Fact]
        public void TestIsPlaylistDownload()
        {
            YoutubeDL ydlClient = new YoutubeDL();

            ydlClient.GetDownloadInfo(@"https://www.youtube.com/playlist?list=PLrEnWoR732-BHrPp_Pm8_VleD68f9s14-");

            PlaylistDownloadInfo info = ydlClient.Info as PlaylistDownloadInfo;
            Assert.NotEqual(info, null);
        }

        [Fact]
        public void TestIsVideoDownload()
        {
            YoutubeDL ydlClient = new YoutubeDL();

            ydlClient.GetDownloadInfo(@"https://www.youtube.com/watch?v=dQw4w9WgXcQ");

            VideoDownloadInfo info = ydlClient.Info as VideoDownloadInfo;
            Assert.NotEqual(info, null);
        }

        [Fact]
        public void TestStringOption()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string usernameStringOption = " -u testUser ";

            ydlClient.Options.AuthenticationOptions.Username = "testUser";

            Assert.True(ydlClient.PrepareDownload().Contains(usernameStringOption));
        }

        [Fact]
        public void TestOptionSerializer()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string optionsString =
                "{\"DownloadOptions\": {\"fragmentRetries\": -1,\"retries\": -1},\"PostProcessingOptions\": {\"audioFormat\": 0,\"audioQuality\": \"0\"},\"VideoFormatOptions\": {\"format\": 7}}";

            ydlClient.Options.DownloadOptions.FragmentRetries = -1;
            ydlClient.Options.DownloadOptions.Retries = -1;
            ydlClient.Options.VideoFormatOptions.Format = Enums.VideoFormat.best;
            ydlClient.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.best;
            ydlClient.Options.PostProcessingOptions.AudioQuality = "0";

            string options = ydlClient.Options.Serialize();

            Assert.Equal(Regex.Replace(options, @"\s+", ""), Regex.Replace(optionsString, @"\s+", ""));
        }

        [Fact]
        public void TestOptionDeserializer()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string optionsString =
                "{\"DownloadOptions\": {\"fragmentRetries\": -1,\"retries\": -1},\"PostProcessingOptions\": {\"audioFormat\": 0,\"audioQuality\": \"0\"},\"VideoFormatOptions\": {\"format\": 7}}";

            ydlClient.Options  = Options.Deserialize(optionsString);

            Assert.Equal(ydlClient.Options.DownloadOptions.FragmentRetries, -1);
            Assert.Equal(ydlClient.Options.DownloadOptions.Retries, -1);
            Assert.Equal(ydlClient.Options.VideoFormatOptions.Format, Enums.VideoFormat.best);
            Assert.Equal(ydlClient.Options.PostProcessingOptions.AudioFormat, Enums.AudioFormat.best);
            Assert.Equal(ydlClient.Options.PostProcessingOptions.AudioQuality, "0");
        }
    }
}