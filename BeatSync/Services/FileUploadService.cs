namespace BeatSync.Services;

public class FileUploadService
{
    public async Task<(FileResult?,string?)> UploadImage(string? name, string? directory)
    {
        string imageFilePath = string.Empty;
        var fileResult =  await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick an image for the song",
            FileTypes = FilePickerFileType.Images
        });

        if(fileResult == null)
        {
            return (fileResult, imageFilePath);
        }

        string dir = Path.Combine(FileSystem.Current.AppDataDirectory, directory!);
        CreateDirectoryIfMissing(dir);

        imageFilePath = Path.Combine(dir, $"{Guid.NewGuid()}.jpg");
        await Shell.Current.DisplayAlert("Upload picture", "Picture successfully uploaded ", "OK");
        
        return (fileResult,imageFilePath);
    }

    public async Task<(FileResult?, string?)> UploadSong(string? name)
    {
        string songFilePath = string.Empty;
        var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // UTType values
                    { DevicePlatform.Android, new[] { "audio/mpeg" } }, // MIME type
                    { DevicePlatform.WinUI, new[] { ".mp3" } }, // file extension
                    { DevicePlatform.Tizen, new[] { "*/*" } },
                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // UTType values
                });

        var fileResult = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please pick a song",
            FileTypes = customFileType
        });

        if(fileResult == null)
        {
            return (fileResult, songFilePath);
        }

        string dir = Path.Combine(FileSystem.Current.AppDataDirectory, "SongsPlayer");
        CreateDirectoryIfMissing(dir);

        songFilePath = Path.Combine(dir, $"{Guid.NewGuid()}.mp3");
        await Shell.Current.DisplayAlert("Upload song", "Song successfully uploaded ", "OK");
        return (fileResult, songFilePath);
    }

    private void CreateDirectoryIfMissing(string dir)
    {
        bool folderExists = Directory.Exists(dir);
        if (!folderExists)
        {
            Directory.CreateDirectory(dir);
        }
    }
}
