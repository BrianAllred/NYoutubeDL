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
    using Helpers;
    using Xunit;

    #endregion

    public class Tests
    {
        private readonly YoutubeDL ydlClient = new YoutubeDL();

        [Fact]
        public void TestBoolOption()
        {
            const string extractAudioOptionString = " -x ";

            this.ydlClient.Options.PostProcessingOptions.ExtractAudio = true;

            Assert.True(this.ydlClient.PrepareDownload().Contains(extractAudioOptionString));
        }

        [Fact]
        public void TestDateTimeOption()
        {
            const string dateDateTimeOption = " --date 20170201 ";

            this.ydlClient.Options.VideoSelectionOptions.Date = new DateTime(2017, 02, 01);

            Assert.True(this.ydlClient.PrepareDownload().Contains(dateDateTimeOption));
        }

        [Fact]
        public void TestEnumOption()
        {
            const string audioFormatEnumOption = " --audio-format mp3 ";

            this.ydlClient.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.mp3;

            Assert.True(this.ydlClient.PrepareDownload().Contains(audioFormatEnumOption));
        }

        [Fact]
        public void TestFileSizeRateOption1()
        {
            const string bufferSizeFileSizeRateOption = " --buffer-size 5.5M ";

            this.ydlClient.Options.DownloadOptions.BufferSize = new FileSizeRate(5.5, Enums.ByteUnit.M);

            Assert.True(this.ydlClient.PrepareDownload().Contains(bufferSizeFileSizeRateOption));
        }

        [Fact]
        public void TestFileSizeRateOption2()
        {
            const string bufferSizeFileSizeRateOption = " --buffer-size 5.5M ";

            this.ydlClient.Options.DownloadOptions.BufferSize = new FileSizeRate("5.5M");

            Assert.True(this.ydlClient.PrepareDownload().Contains(bufferSizeFileSizeRateOption));
        }

        [Fact]
        public void TestIntOption()
        {
            const string socketTimeoutIntOption = " --socket-timeout 5 ";

            this.ydlClient.Options.NetworkOptions.SocketTimeout = 5;

            Assert.True(this.ydlClient.PrepareDownload().Contains(socketTimeoutIntOption));
        }

        [Fact]
        public void TestIntOptionNegativeIsInfinite()
        {
            const string retriesIntOption = " -R infinite ";

            this.ydlClient.Options.DownloadOptions.Retries = -1;

            Assert.True(this.ydlClient.PrepareDownload().Contains(retriesIntOption));
        }

        [Fact]
        public void TestStringOption()
        {
            const string usernameStringOption = " -u testUser ";

            this.ydlClient.Options.AuthenticationOptions.Username = "testUser";

            Assert.True(this.ydlClient.PrepareDownload().Contains(usernameStringOption));
        }
    }
}