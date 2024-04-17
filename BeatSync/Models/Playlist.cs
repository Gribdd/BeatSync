namespace BeatSync.Models;

/// <summary>
/// Repository for each playlist created
/// </summary>
public partial class Playlist : BaseModel
{
    [ObservableProperty]
    private int _userId;

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private string? _imageFilePath;

    [ObservableProperty]
    private int? _songCount;

    public (bool, string) IsValid()
    {
        var validationMethods = new List<Func<(bool, string)>>
        {
            IsValidUserId,
            IsValidName,
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

    private (bool, string) IsValidUserId()
    {
        if (UserId <= 0)
            return (false, "Please select a user first");
        return (true, string.Empty);
    }

    private (bool, string) IsValidName()
    {
        if (string.IsNullOrEmpty(Name))
            return (false, "Name is required");
        return (true, string.Empty);
    }

    private (bool, string) IsValidImageFilePath()
    {
        if (string.IsNullOrEmpty(ImageFilePath))
            return (false, "Please upload an image first");
        return (true, string.Empty);
    }
}
