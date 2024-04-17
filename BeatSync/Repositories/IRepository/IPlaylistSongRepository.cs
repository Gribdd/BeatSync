namespace BeatSync.Repositories.IRepository;

public interface IPlaylistSongRepository : IGenericRepository<PlaylistSongs>
{
    Task<ObservableCollection<PlaylistSongs>> GetPlaylistSongsByPlaylistId(int playlistId);
}
