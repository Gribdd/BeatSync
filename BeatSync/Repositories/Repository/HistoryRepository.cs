
namespace BeatSync.Repositories.Repository;

public class HistoryRepository : GenericRepository<History>, IHistoryRepository
{
    public HistoryRepository() : base("History.json")
    {
    }
}
