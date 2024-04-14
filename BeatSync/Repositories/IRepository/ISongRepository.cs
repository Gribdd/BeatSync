namespace BeatSync.Repositories.IRepository;

public interface ISongRepository : IGenericRepository<Song>
{
    Task<Song> GetSongByName(string name);
}
