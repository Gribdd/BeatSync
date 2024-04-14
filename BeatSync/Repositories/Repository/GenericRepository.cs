using BeatSync.Repositories.IRepository;

namespace BeatSync.Repositories.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseModel
{
    private readonly string _filePath;
    protected ObservableCollection<T> _entities;

    public GenericRepository(string filePath)
    {
        _filePath = Path.Combine(FileSystem.Current.AppDataDirectory, filePath);
        _entities = new ObservableCollection<T>();
    }

    protected async Task<ObservableCollection<T>> LoadEntities()
    {
        if (File.Exists(_filePath))
        {
            var json = await File.ReadAllTextAsync(_filePath);
            _entities = JsonSerializer.Deserialize<ObservableCollection<T>>(json)!;
            // returns _entities if not null, otherwise returns new ObservableCollection<T>
            // ??= is the null-coalescing assignment operator
            return _entities ??= new ObservableCollection<T>();
        }

        return new ObservableCollection<T>();
    }

    protected async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_entities);
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task Add(T entity)
    {
        _entities = await LoadEntities();
        entity.Id = _entities.Count + 1;
        _entities.Add(entity);
        await SaveAsync();
    }

    public async Task Update(T entity)
    {
        _entities = await LoadEntities();
        var oldEntity = _entities.FirstOrDefault(e => e.Id == entity.Id);
        if (entity != null)
        {
            var index = _entities.IndexOf(oldEntity!);
            _entities[index] = entity;
        }
        await SaveAsync();
    }

    public async Task Delete(int id)
    {
        _entities = await LoadEntities();
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        if (entity != null)
            entity.IsDeleted = true;
        await SaveAsync();
    }

    public async Task<ObservableCollection<T>> GetAll()
    {
        _entities = await LoadEntities();
        return _entities;
    }

    public async Task<ObservableCollection<T>> GetActive()
    {
        _entities = await LoadEntities();
        return new ObservableCollection<T>(_entities.Where(e => e.IsDeleted == false));
    }

    public async Task<T> Get(int id)
    {
        _entities = await LoadEntities();
        return _entities.FirstOrDefault(e => e.Id == id)!;
    }
}
