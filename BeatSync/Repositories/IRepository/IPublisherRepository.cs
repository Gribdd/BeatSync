namespace BeatSync.Repositories.IRepository;

public interface IPublisherRepository : IGenericRepository<Publisher>
{
    Task<Publisher> GetByName(string name);
    Task<Publisher> GetByUsername(string username);
}
