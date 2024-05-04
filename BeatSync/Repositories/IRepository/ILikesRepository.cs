namespace BeatSync.Repositories.IRepository;

public interface ILikesRepository : IGenericRepository<Likes>
{
    Task<ObservableCollection<Likes>> GetLikesByUserId(int userId);
}
