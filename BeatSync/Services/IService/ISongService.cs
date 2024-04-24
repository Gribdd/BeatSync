namespace BeatSync.Services.IService;

public interface ISongService : IGenericService<Song>
{
    Task<Song> GetByNameAsync(string name);
    Task<Song> GetByNameAndArtistIdAsync(string name, int artistId);
}
