

namespace BeatSync.Repositories.Repository;

public class PlaylistRepository : GenericRepository<Playlist>, IPlaylistRepository
{
    public PlaylistRepository() : base("Playlists.json")
    {
    }

    public async Task<Playlist> GetByName(string name, int userId)
    {
        var userPlaylists = await GetPlaylistsByUser(userId);
        return userPlaylists.FirstOrDefault(p => p.Name == name)!;
    }

    public async Task<ObservableCollection<Playlist>> GetPlaylistsByUser(int userId)
    {
        var activePlaylists = await GetActive();
        if(activePlaylists.Count == 0)
        {
            new ObservableCollection<Playlist>();
        }
        return new ObservableCollection<Playlist>(activePlaylists.Where(p => p.UserId == userId));
    }
}
