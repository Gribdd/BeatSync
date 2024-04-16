namespace BeatSync.Repositories.IRepository;

public interface IAlbumRepository : IGenericRepository<Album>
{
    Task<Album> GetByName(string albumName);
}
