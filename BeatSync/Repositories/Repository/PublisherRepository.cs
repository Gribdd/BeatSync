
namespace BeatSync.Repositories.Repository;

public class PublisherRepository : GenericRepository<Publisher>
{
    public PublisherRepository() : base("Publishers.json")
    {
    }
}
