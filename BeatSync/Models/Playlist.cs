namespace BeatSync.Models;

/// <summary>
/// Repository for each playlist created
/// </summary>
public partial class Playlist : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private int _userId;

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private string? _imageFilePath;

    [ObservableProperty]
    private bool _isDeleted;
}
