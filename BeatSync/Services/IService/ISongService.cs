namespace BeatSync.Services.IService;

public interface ISongService : IGenericService<Song>
{
    Task<Song> GetByNameAsync(string name);
}
