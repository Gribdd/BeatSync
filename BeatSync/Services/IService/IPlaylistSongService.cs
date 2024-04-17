namespace BeatSync.Services.IService;

public interface IPlaylistSongService
{
    Task<ObservableCollection<PlaylistSongs>> GetPlaylistSongsByPlaylistIdAsync(int playlistId);
}
