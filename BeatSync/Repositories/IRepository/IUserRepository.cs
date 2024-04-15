namespace BeatSync.Repositories.IRepository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByName(string name);
    Task<User> GetByUserName(string userName);
}
