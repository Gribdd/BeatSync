namespace BeatSync.Repositories.IRepository;

public interface ISongRepository : IGenericRepository<Song>
{
    Task<Song> GetSongByName(string name);
    Task<Song> GetSongByNameAndArtistId(string name, int artistId);
}
