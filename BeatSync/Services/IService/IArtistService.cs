namespace BeatSync.Services.IService;

public interface IArtistService : IGenericService<Artist>
{
    Task<Artist> GetByUsernameAsync(string username);
}
