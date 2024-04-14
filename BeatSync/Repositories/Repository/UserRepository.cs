
namespace BeatSync.Repositories.Repository;

public class UserRepository : GenericRepository<User>
{
    public UserRepository() : base("Users.json")
    {
        
    }
}
