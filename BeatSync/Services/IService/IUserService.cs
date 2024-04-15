
namespace BeatSync.Services.IService;

public interface IUserService : IGenericService<User>
{
    Task<User> GetByNameAsync(string name);
}
