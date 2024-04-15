
namespace BeatSync.Repositories.Repository;

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    public ArtistRepository() : base("Artists.json")
    {
    }

    public async Task<Artist> GetByName(string name)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(a => a.FullName == name)!;
    }

    public async Task<Artist> GetByUserName(string userName)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(a => a.Username == userName)!;
    }
}
