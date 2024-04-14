namespace BeatSync.Services.IService;

public interface IGenericService<T> where T : class
{
    Task<ObservableCollection<T>> GetAllAsync();
    Task<ObservableCollection<T>> GetActiveAsync();
    Task<T> GetAsync(int id);
    Task AddAsync(T entity);
    abstract Task UpdateAsync(int id);
    abstract Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

}
