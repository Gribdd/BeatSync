namespace BeatSync.Services.IService;

public interface ILikesService : IGenericService<Likes>
{
    Task<ObservableCollection<Likes>> GetLikesByUserIdAsync(int userId);
}
