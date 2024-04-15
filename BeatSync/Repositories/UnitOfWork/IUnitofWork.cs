namespace BeatSync.Repositories.UnitOfWork;

public interface IUnitofWork
{
    IUserRepository UserRepository { get; }
    IGenericRepository<Publisher> PublisherRepository { get; }
    IGenericRepository<Artist> ArtistRepository { get; }
    ISongRepository SongRepository { get; }
    IGenericRepository<T>? GetRepository<T>() where T : class;
}
