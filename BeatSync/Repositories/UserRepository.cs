using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSync.Repositories;

public class UserRepository : Repository<User>
{
    public UserRepository() : base("Users.json")
    {
        
    }
}
