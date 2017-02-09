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
    ///     Object containing Subtitle parameters
    /// </summary>
    public class Subtitle : OptionSection
    {
        [Option] private readonly BoolOption allSubs = new BoolOption("--all-subs");
        [Option] private readonly BoolOption listsubs = new BoolOption("--list-subs");

        [Option] private readonly EnumOption<Enums.SubtitleFormat> subFormat =
            new EnumOption<Enums.SubtitleFormat>("--sub-format");

        [Option] private readonly StringOption subFormatAdvanced = new StringOption("--sub-format");
        [Option] private readonly StringOption subLang = new StringOption("--sub-lang");
        [Option] private readonly BoolOption writeAutoSub = new BoolOption("--write-auto-sub");
        [Option] private readonly BoolOption writeSub = new BoolOption("--write-sub");

        /// <summary>
        ///     --all-subs
        /// </summary>
        public bool AllSubs
        {
            get { return this.allSubs.Value ?? false; }
            set { this.allSubs.Value = value; }
        }

        /// <summary>
        ///     --list-subs
        /// </summary>
        public bool ListSubs
        {
            get { return this.listsubs.Value ?? false; }
            set { this.listsubs.Value = value; }
        }

        /// <summary>
        ///     This is a simple version of --sub-format. For more advanced format usage, use the SubFormatAdvanced
        ///     property.
        ///     NOTE: SubFormatAdvanced takes precedence over SubFormat.
        /// </summary>
        public Enums.SubtitleFormat SubFormat
        {
            get
            {
                return this.subFormat.Value == null
                    ? Enums.SubtitleFormat.undefined
                    : (Enums.SubtitleFormat) this.subFormat.Value;
            }
            set { this.subFormat.Value = (int) value; }
        }

        /// <summary>
        ///     This accepts a string matching the advanced --sub-format according to the youtube-dl documentation below.
        ///     NOTE: SubFormatAdvanced takess precedence over SubFormat.
        ///     <see cref="https://github.com/rg3/youtube-dl/blob/master/README.md#subtitle-options" />
        /// </summary>
        public string SubFormatAdvanced
        {
            get { return this.subFormatAdvanced.Value; }
            set { this.subFormatAdvanced.Value = value; }
        }

        /// <summary>
        ///     --sub-lang
        /// </summary>
        public string SubLang
        {
            get { return this.subLang.Value; }
            set { this.subLang.Value = value; }
        }

        /// <summary>
        ///     --write-auto-sub
        /// </summary>
        public bool WriteAutoSub
        {
            get { return this.writeAutoSub.Value ?? false; }
            set { this.writeAutoSub.Value = value; }
        }

        /// <summary>
        ///     --write-sub
        /// </summary>
        public bool WriteSub
        {
            get { return this.writeSub.Value ?? false; }
            set { this.writeSub.Value = value; }
        }

        public override string OptionsToCliParameters()
        {
            // Set subFormat to undefined if subFormatAdvanced has a valid value,
            // then return the parameters.
            if (!string.IsNullOrWhiteSpace(this.subFormatAdvanced.Value))
            {
                this.subFormat.Value = (int) Enums.SubtitleFormat.undefined;
            }

            return base.OptionsToCliParameters();
        }
    }
}