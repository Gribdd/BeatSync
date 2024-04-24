namespace BeatSync.Services.IService;

public interface IAlbumService : IGenericService<Album>
{
    Task<Album> GetByNameAsync(string albumName);
    Task<Album> GetByNameAndArtistIdAsync(string albumName, int artistId);
    Task UpdateAlbumSongs(Album album);
}
