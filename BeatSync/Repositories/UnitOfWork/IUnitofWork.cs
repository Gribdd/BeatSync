namespace BeatSync.Repositories.UnitOfWork;

public interface IUnitofWork
{
    IUserRepository UserRepository { get; }
    IPublisherRepository PublisherRepository { get; }
    IArtistRepository ArtistRepository { get; }
    IAlbumRepository AlbumRepository { get; }
    ISongRepository SongRepository { get; }
    IPlaylistRepository PlaylistRepository { get; }
    IPlaylistSongRepository PlaylistSongRepository { get; }
    IHistoryRepository HistoryRepository { get; }
    IGenericRepository<T>? GetRepository<T>() where T : class;
}
