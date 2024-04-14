namespace BeatSync.Models;

/// <summary>
/// if artist is deleted all songs associated with that artist will be deleted
/// </summary>
public partial class Song : BaseModel
{
    [ObservableProperty]
    private int _artistID;

    [ObservableProperty]
    private int? _albumId;

    [ObservableProperty]
    private string? _artistName;

    [ObservableProperty]
    private string? _name;
    
    [ObservableProperty]
    private string? _genre;

    [ObservableProperty]
    private string? _filePath;

    [ObservableProperty]
    private string? _imageFilePath;

    public (bool, string) IsValid()
    {
        var validationMethods = new List<Func<(bool, string)>>
        {
            IsValidArtistID,
            IsValidAlbumID,
            IsValidArtistName,
            IsValidName,
            IsValidGenre,
            IsValidFilePath,
            IsValidImageFilePath
        };

        foreach (var method in validationMethods)
        {
            var (isValid, message) = method();
            if (!isValid)
                return (false, message);
        }

        return (true, string.Empty);
    }

    private (bool, string) IsValidArtistID()
    {
        if (ArtistID < 0)
            return (false, "Invalid Artist");
        return (true, string.Empty);
    }

    private (bool, string) IsValidAlbumID()
    {
        if (AlbumId < 0)
            return (false, "Invalid Album");
        return (true, string.Empty);
    }

    private (bool, string) IsValidArtistName()
    {
        if (string.IsNullOrEmpty(ArtistName))
            return (false, "Artist Name is required");
        return (true, string.Empty);
    }

    private (bool, string) IsValidName()
    {
        if (string.IsNullOrEmpty(Name))
            return (false, "Name is required");
        return (true, string.Empty);
    }

    private (bool, string) IsValidGenre()
    {
        if (string.IsNullOrEmpty(Genre))
            return (false, "Genre is required");
        return (true, string.Empty);
    }

    private (bool, string) IsValidFilePath()
    {
        if (string.IsNullOrEmpty(FilePath))
            return (false, "Please upload a song");
        return (true, string.Empty);
    }

    private (bool, string) IsValidImageFilePath()
    {
        if (string.IsNullOrEmpty(ImageFilePath))
            return (false, "Please upload an image");
        return (true, string.Empty);
    }
}
