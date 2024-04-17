namespace BeatSync.Services.IService;

public interface IPlaylistService : IGenericService<Playlist>
{
    Task<ObservableCollection<Playlist>> GetPlaylistsByUserAsync(int userId);
    Task<Playlist> GetByNameAsync(string name, int userId);
}
