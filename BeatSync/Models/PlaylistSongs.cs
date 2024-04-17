namespace BeatSync.Models;

/// <summary>
/// Repository for all playlist and songs
/// </summary>
public partial class PlaylistSongs : BaseModel
{
    [ObservableProperty]
    private int _playlistId;

    [ObservableProperty]
    private int _songId;
}
