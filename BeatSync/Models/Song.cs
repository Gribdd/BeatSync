using CommunityToolkit.Mvvm.ComponentModel;

namespace BeatSync.Models;

/// <summary>
/// if artist is deleted all songs associated with that artist will be deleted
/// </summary>
public partial class Song : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private int _artistID;

    [ObservableProperty]
    private string? _name;
    
    [ObservableProperty]
    private string? _genre;

    [ObservableProperty]
    private string? _filename;

    [ObservableProperty]
    private string? _imageFileName;

    [ObservableProperty]
    private bool _isDeleted;
}
