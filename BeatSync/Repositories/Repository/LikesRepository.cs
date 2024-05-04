
namespace BeatSync.Repositories.Repository;

public class LikesRepository : GenericRepository<Likes>, ILikesRepository
{
    public LikesRepository() : base("Likes.json")
    {
    }

    public async Task<ObservableCollection<Likes>> GetLikesByUserId(int userId)
    {
        _entities = await GetAll();
        return new ObservableCollection<Likes>(_entities.Where(x => x.UserID == userId));
    }
}
