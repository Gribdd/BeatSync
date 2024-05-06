namespace BeatSync.Repositories.IRepository;

public interface ISongRepository : IGenericRepository<Song>
{
    Task<Song> GetSongByName(string name);
    Task<Song> GetSongByNameAndArtistId(string name, int artistId);
    Task<Song> GetSongByArtistId(int artistId);

    Task<ObservableCollection<Song>> GetSongsByGenre(string genre);
}
