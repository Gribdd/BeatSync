
using BeatSync.Repositories.IRepository;

namespace BeatSync.Repositories.Repository;

public class SongRepository : GenericRepository<Song>, ISongRepository
{
    public SongRepository() : base("Songs.json")
    {
    }

    public async Task<Song> GetSongByName(string name)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(s => s.Name == name)!;
    }

    public async Task<Song> GetSongByNameAndArtistId(string name, int artistId)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(s => s.Name == name && s.ArtistID == artistId)!;
    }

    public async Task<Song> GetSongByArtistId(int artistId)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(s => s.ArtistID == artistId)!;
    }
}
