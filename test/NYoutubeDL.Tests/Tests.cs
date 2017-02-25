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
    using System.Threading;
    using Helpers;
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
        public void TestIsPlaylist()
        {
            bool wait = true;
            YoutubeDL ydlClient = new YoutubeDL();
            ydlClient.Info.UpdateEvent += delegate
            {
                Assert.True(ydlClient.Info.IsPlaylistDownload);
                wait = false;
            };

            ydlClient.GetDownloadInfo(@"https://www.youtube.com/playlist?list=PLrEnWoR732-BHrPp_Pm8_VleD68f9s14-");

            int count = 0;
            while (wait && count++ < 30)
            {
                Thread.Sleep(1000);
            }
        }

        [Fact]
        public void TestIsNotPlaylist()
        {
            bool wait = true;
            YoutubeDL ydlClient = new YoutubeDL();
            ydlClient.Info.UpdateEvent += delegate
            {
                Assert.False(ydlClient.Info.IsPlaylistDownload);
                wait = false;
            };

            ydlClient.GetDownloadInfo(@"https://www.youtube.com/watch?v=dQw4w9WgXcQ");

            int count = 0;
            while (wait && count++ < 30)
            {
                Thread.Sleep(1000);
            }
        }

        [Fact]
        public void TestStringOption()
        {
            YoutubeDL ydlClient = new YoutubeDL();
            const string usernameStringOption = " -u testUser ";

            ydlClient.Options.AuthenticationOptions.Username = "testUser";

            Assert.True(ydlClient.PrepareDownload().Contains(usernameStringOption));
        }
    }
}