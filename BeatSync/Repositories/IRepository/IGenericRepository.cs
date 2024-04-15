namespace BeatSync.Repositories.IRepository;
/// <summary>
/// Generic = Can handle any type of object
/// Example: IRepository<Song>, IRepository<Artist>
/// Where T : class = T must be a reference type
/// Example of reference type: class of song, artist, publisher
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericRepository<T> where T : class
{
    Task<ObservableCollection<T>> GetActive();
    Task<ObservableCollection<T>> GetAll();
    //Task<T> GetByName(string name);
    Task<T> Get(int id);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(int id);
}
