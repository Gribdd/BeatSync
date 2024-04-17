namespace BeatSync.Repositories.IRepository;

public interface IPlaylistRepository : IGenericRepository<Playlist>
{
    Task<ObservableCollection<Playlist>> GetPlaylistsByUser(int userId);
    Task<Playlist> GetByName(string name, int userId);
}
