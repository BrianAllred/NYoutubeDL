# NYoutubeDL
**CircleCI:** [![CircleCI](https://circleci.com/gh/BrianAllred/NYoutubeDL.svg?style=svg)](https://circleci.com/gh/BrianAllred/NYoutubeDL)

A simple youtube-dl library for C#.

See the [main page](https://rg3.github.io/youtube-dl/) for youtube-dl for more information.

### Getting the package
* In the Nuget package manager console, run

		PM> Install-Package NYoutubeDL

* For DotNet Core apps, edit your project.json dependencies

		"NYoutubeDL": "0.0.1"

### Using the code
See the [documentation](https://github.com/rg3/youtube-dl/blob/master/README.md#readme) for youtube-dl first to understand what it does and how it does it.

1. Create a new YoutubeDL client:

		var youtubeDl = YoutubeDL.Instance();

2. Set some options:

		// Required
		youtubeDl.Output = "/path/to/downloads/video.mp4";

		// Optional, required if binary is not in $PATH
		youtubeDl.YoutubeDlPath = "/path/to/youtube-dl";

3. Subscribe to the console output (optional, but recommended):

		youtubeDl.StandardOutputEvent += (sender, output) => Console.WriteLine(output);
		youtubeDl.StandardErrorEvent += (sender, errorOutput) => Console.WriteLine(errorOutput);

4. Start the download:

		 // Just let it run
		youtubeDl.Download();
		
		// Or, start the download and monitor it using a process object
		Process ydlDownloadProcess = youtubeDl.Download();

