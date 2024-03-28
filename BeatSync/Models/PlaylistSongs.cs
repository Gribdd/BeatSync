namespace BeatSync.Models;

/// <summary>
/// Repository for all playlist and songs
/// </summary>
public partial class PlaylistSongs : ObservableObject
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private int _playlistId;

    [ObservableProperty]
    private int _songId;    
}
