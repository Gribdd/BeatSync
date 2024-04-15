
namespace BeatSync.Repositories.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository() : base("Users.json")
    {
    }

    public async Task<User> GetByName(string name)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(u => u.FullName == name)!;
    }

    public async Task<User> GetByUserName(string userName)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(u => u.Username == userName)!;
    }
}
