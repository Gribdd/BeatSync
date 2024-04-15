
namespace BeatSync.Repositories.Repository;

public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
{
    public PublisherRepository() : base("Publishers.json")
    {
    }

    public async Task<Publisher> GetByName(string name)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(p => p.FullName == name)!;
    }

    public async Task<Publisher> GetByUsername(string username)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(p => p.Username == username)!;
    }
}
