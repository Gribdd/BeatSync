

namespace BeatSync.Services.Service;
public class LikesService : GenericService<Likes>, ILikesService
{
    private readonly IUnitofWork _unitofWork;
    public LikesService(IUnitofWork unitofWork) : base(unitofWork)
    {
        _unitofWork = unitofWork;
    }

    public async Task<ObservableCollection<Likes>> GetLikesByUserIdAsync(int userId)
    {
        return await _unitofWork.LikesRepository.GetLikesByUserId(userId);
    }
}
