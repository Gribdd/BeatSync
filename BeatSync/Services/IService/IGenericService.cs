namespace BeatSync.Services.IService;

public interface IGenericService<T> where T : class
{
    Task<ObservableCollection<T>> GetAllAsync();
    Task<ObservableCollection<T>> GetActiveAsync();
    Task<T> GetAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(int id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

}
