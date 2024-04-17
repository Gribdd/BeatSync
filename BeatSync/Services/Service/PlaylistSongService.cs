

namespace BeatSync.Services.Service;

public class PlaylistSongService : GenericService<PlaylistSongs>, IPlaylistSongService
{
    private readonly IUnitofWork _unitofWork;

    public PlaylistSongService(IUnitofWork unitofWork) : base(unitofWork)
    {
        _unitofWork = unitofWork;
    }

    public async Task<ObservableCollection<PlaylistSongs>> GetPlaylistSongsByPlaylistIdAsync(int playlistId)
    {
        return await _unitofWork.PlaylistSongRepository.GetPlaylistSongsByPlaylistId(playlistId);
    }
}
