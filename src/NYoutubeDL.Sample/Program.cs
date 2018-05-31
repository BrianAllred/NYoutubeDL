// Copyright 2018 Brian Allred
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

namespace NYoutubeDL.Sample
{
    #region Using

    using System;
    using Helpers;
    using Options;

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            YoutubeDL ydlClient = new YoutubeDL();

            ydlClient.Options.DownloadOptions.FragmentRetries = -1;
            ydlClient.Options.DownloadOptions.Retries = -1;
            ydlClient.Options.VideoFormatOptions.Format = Enums.VideoFormat.best;
            ydlClient.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.best;
            ydlClient.Options.PostProcessingOptions.AudioQuality = "0";

            string options = ydlClient.Options.Serialize();
            ydlClient.Options = Options.Deserialize(options);

            ydlClient.StandardErrorEvent += (sender, error) => Console.WriteLine(error);
            ydlClient.StandardOutputEvent += (sender, output) => Console.WriteLine(output);

            ydlClient.Download("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }
    }
}