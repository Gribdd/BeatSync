namespace BeatSync.Repositories;

public class Repository<T> : IRepository<T> where T : class, IBaseModel
{
    private readonly string _filePath;
    private ObservableCollection<T> _entities;

    public Repository(string filePath)
    {
        _filePath = Path.Combine(FileSystem.Current.AppDataDirectory, filePath);
        _entities = new ObservableCollection<T>();
    }

    private async Task<ObservableCollection<T>> LoadEntities()
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

    private async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_entities);
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task Add(T entity)
    {
        entity.Id = _entities.Count + 1;
        entity.IsDeleted = false;

        _entities.Add(entity);
        await SaveAsync();
    }

    public async Task Update(T entity)
    {
        var existingEntity = _entities.FirstOrDefault(e => e.Id == entity.Id);
        if (existingEntity != null)
        {
            var index = _entities.IndexOf(existingEntity);
            _entities[index] = entity;
        }
        await SaveAsync();
    }

    public async Task Delete(int id)
    {
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
}
