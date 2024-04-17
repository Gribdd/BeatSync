namespace BeatSync.Repositories.Repository;

public class PlaylistSongRepository : GenericRepository<PlaylistSongs>, IPlaylistSongRepository
{
    public PlaylistSongRepository() : base("PlaylistSongs.json")
    {
    }

    public async Task<ObservableCollection<PlaylistSongs>> GetPlaylistSongsByPlaylistId(int playlistId)
    {
        var activePlaylistSongs = await GetActive();
        if (activePlaylistSongs.Count == 0)
        {
            return new ObservableCollection<PlaylistSongs>();
        }
        return new ObservableCollection<PlaylistSongs>(activePlaylistSongs.Where(ps => ps.PlaylistId == playlistId));
    }
}
