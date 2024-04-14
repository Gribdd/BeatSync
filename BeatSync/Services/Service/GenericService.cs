using BeatSync.Services.IService;

namespace BeatSync.Services.Service;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IUnitofWork _unitofWork;
    private IGenericRepository<T> _repository => _unitofWork.GetRepository<T>()!;

    public GenericService(IUnitofWork unitofWork)
    {
        _unitofWork = unitofWork;
    }

    public async Task AddAsync(T entity)
    {
        await _repository.Add(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.Delete(id);
    }

    public async Task<T> GetAsync(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<ObservableCollection<T>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<ObservableCollection<T>> GetActiveAsync()
    {
        return await _repository.GetActive();
    }

    // This method is abstract in the interface
    // each service has its own implementation of this method
    public virtual Task UpdateAsync(int id)
    {
        throw new NotImplementedException();
    }

    //overload, to be used in the UserService
    //explain why there are two UpdateAsync methods
    public virtual async Task UpdateAsync(T entity)
    {
        await _repository.Update(entity);
    }

}
