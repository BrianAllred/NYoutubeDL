# NYoutubeDL
**CircleCI:** [![CircleCI](https://circleci.com/gh/BrianAllred/NYoutubeDL.svg?style=svg)](https://circleci.com/gh/BrianAllred/NYoutubeDL)

A simple youtube-dl library for C#.

See the [main page](https://rg3.github.io/youtube-dl/) for youtube-dl for more information.

### Getting the package
* Search for `NYoutubeDL` in you project's Nuget Manager and click install.

* In the Nuget package manager console, run

        PM> Install-Package NYoutubeDL

* Manually [download](https://www.nuget.org/packages/NYoutubeDL/) nupkg from NuGet Gallery.

### Using the code
See the [documentation](https://github.com/rg3/youtube-dl/blob/master/README.md#readme) for youtube-dl first to understand what it does and how it does it.

1. Create a new YoutubeDL client:

        var youtubeDl = new YoutubeDL();

2. Options are grouped according to the youtube-dl documentation:

        youtubeDl.Options.FileSystem.Output = "/path/to/downloads/video.mp4";
        youtubeDl.Options.PostProcessing.ExtractAudio = true;
        youtubeDl.VideoUrl = "http://www.somevideosite.com/videoUrl";

        // Or update the binary
        youtubeDl.Options.General.Update = true;

        // Optional, required if binary is not in $PATH
        youtubeDl.YoutubeDlPath = "/path/to/youtube-dl";

3. Options can also be saved and loaded. Only changed options will be saved.

        File.WriteAllText("options.config", youtubeDl.Options.Serialize());
        youtubeDl.Options = Options.Deserialize(File.ReadAllText("options.config"));

4. Subscribe to the console output (optional, but recommended):

        youtubeDl.StandardOutputEvent += (sender, output) => Console.WriteLine(output);
        youtubeDl.StandardErrorEvent += (sender, errorOutput) => Console.WriteLine(errorOutput);
        
5. Subscribe to download information updates. Hard subscription is optional, the DownloadInfo class implements INotifyPropertyChanged.

        youtubeDl.Info.PropertyChanged += delegate { <your code here> };

6. Start the download:
        
        // Prepare the download (in case you need to validate the command before starting the download)
        string commandToRun = await youtubeDl.PrepareDownload();
        // Alternatively
        string commandToRun = youtubeDl.PrepareDownload().Result;

         // Just let it run
        youtubeDl.Download();

        // Wait for it
        await youtubeDl.Download();
        // Or
        youtubeDl.Download().Wait();

        // Or provide video url
        youtubeDl.Download("http://videosite.com/videoUrl");

