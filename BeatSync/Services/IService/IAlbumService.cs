namespace BeatSync.Services.IService;

public interface IAlbumService : IGenericService<Album>
{
    Task<Album> GetByNameAsync(string albumName);
    Task UpdateAlbumSongs(Album album);
}
