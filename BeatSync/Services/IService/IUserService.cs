
namespace BeatSync.Services.IService;

public interface IUserService : IGenericService<User>
{
    Task<User> GetByUsernameAsync(string name);
}
