namespace BeatSync.Repositories.IRepository;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task<Artist> GetByName(string name);
    Task<Artist> GetByUserName(string userName);
}
