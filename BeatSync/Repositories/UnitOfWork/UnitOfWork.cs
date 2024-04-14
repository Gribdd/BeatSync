
namespace BeatSync.Repositories.UnitOfWork;

/// <summary>
/// responsible for handling all repositories,
/// good practice to have one unit of work for all repositories,
/// to ensure that all repositories are using the same connection (json)
/// </summary>
public class UnitOfWork : IUnitofWork
{
    public IGenericRepository<User> UserRepository { get; }
    public IGenericRepository<Publisher> PublisherRepository { get; }
    public IGenericRepository<Artist> ArtistRepository { get; }
    public ISongRepository SongRepository { get; }

    public UnitOfWork()
    {
        UserRepository = new GenericRepository<User>("Users.json");
        PublisherRepository = new GenericRepository<Publisher>("Publishers.json");
        ArtistRepository = new GenericRepository<Artist>("Artists.json");
        SongRepository = new SongRepository();
    }
    
    //needed so that we can accessed Generic Repositories
    //call this method to get the repository you want
    public IGenericRepository<T>? GetRepository<T>() where T : class
    {
        if (typeof(T) == typeof(User))
            return UserRepository as IGenericRepository<T>;
        if (typeof(T) == typeof(Publisher))
            return PublisherRepository as IGenericRepository<T>;
        if (typeof(T) == typeof(Artist))
            return ArtistRepository as IGenericRepository<T>;
        if (typeof(T) == typeof(Song))
            return SongRepository as IGenericRepository<T>;

        return null;
    }
}
