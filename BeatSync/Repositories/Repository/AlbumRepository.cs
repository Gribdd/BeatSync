

namespace BeatSync.Repositories.Repository;

public class AlbumRepository : GenericRepository<Album>, IAlbumRepository
{
    public AlbumRepository() : base("Albums.json")
    {
    }

    public async Task<Album> GetByName(string albumName)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(a => a.Name == albumName)!;
    }

    public async Task<Album> GetByNameAndArtistId(string albumName, int artistId)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(a => a.Name == albumName && a.ArtistId == artistId)!;
    }
}
