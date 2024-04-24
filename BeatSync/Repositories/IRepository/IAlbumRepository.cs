namespace BeatSync.Repositories.IRepository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    Task<Album> GetByNameAndArtistId(string albumName, int artistId);
    Task<Album> GetByName(string albumName);
}
