namespace BeatSync.Services.IService;

public interface IPublisherService : IGenericService<Publisher>
{
    Task<Publisher> GetByUsernameAsync(string username);
    Task<Publisher> GetCurrentUser();
}
