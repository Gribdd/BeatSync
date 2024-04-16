namespace BeatSync.Models;

public partial class Album : BaseModel
{
    [ObservableProperty]
    private int _artistId;

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private string? _artistName;
    
    [ObservableProperty]
    private string? _imageFilePath;

    [ObservableProperty]
    private ObservableCollection<Song>? _songs;

    public (bool, string) IsValid()
    {
        var validationMethods = new List<Func<(bool, string)>>
        {
            IsValidArtistId,
            IsValidName,
            IsValidArtistName,
            IsValidImageFilePath,
        };

        foreach (var method in validationMethods)
        {
            var (isValid, message) = method();
            if (!isValid)
                return (false, message);
        }
        return (true, string.Empty);
    }

    private (bool, string) IsValidArtistId()
    {
        if (ArtistId <= 0)
            return (false, "Please select an artist first");
        return (true, string.Empty);
    }

    private (bool, string) IsValidName()
    {
        if (string.IsNullOrEmpty(Name))
            return (false, "Name is required");
        return (true, string.Empty);
    }

    private (bool, string) IsValidArtistName()
    {
        if (string.IsNullOrEmpty(ArtistName))
            return (false, "Please select an artist first");
        return (true, string.Empty);
    }

    private (bool, string) IsValidImageFilePath()
    {
        if (string.IsNullOrEmpty(ImageFilePath))
            return (false, "Please upload an image");
        return (true, string.Empty);
    }
}
