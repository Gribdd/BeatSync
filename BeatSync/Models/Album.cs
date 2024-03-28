namespace BeatSync.Models;

public partial class Album : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private int _artistId;

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private string? _artistName;
    
    [ObservableProperty]
    private string? _imageFilePath;

    [ObservableProperty]
    private bool _isDeleted;

    [ObservableProperty]
    private ObservableCollection<Song>? _songs;
}
