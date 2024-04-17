
namespace BeatSync.Services.Service;

public class PlaylistService : GenericService<Playlist>, IPlaylistService
{
    private readonly string playlistSongFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "PlaylistSongs.json");
    private readonly IUnitofWork _unitofWork;

    public PlaylistService(IUnitofWork unitofWork) : base(unitofWork)
    {
        _unitofWork = unitofWork;
    }

    public async Task<ObservableCollection<Playlist>> GetPlaylistsByUserAsync(int userId)
    {
        return await _unitofWork.PlaylistRepository.GetPlaylistsByUser(userId);
    }

    public async Task<Playlist> GetByNameAsync(string name, int userId)
    {
        return await _unitofWork.PlaylistRepository.GetByName(name, userId);
    }

    public override async Task UpdateAsync(int id)
    {
        var playlistToBeUpdated = await GetAsync(id);

        string[] editOptions = { "Name" };
        string selectedOption = await Shell.Current.DisplayActionSheet("Select Property to Edit", "Cancel", null, editOptions);

        var newValue = string.Empty;
        for (int index = 0; index < editOptions.Length; index++)
        {
            if (editOptions[index] == selectedOption)
            {
                switch (index)
                {
                    case 0: //Name
                        newValue = await Shell.Current.DisplayPromptAsync($"Edit Playlist {selectedOption}", $"Enter new {selectedOption}:", initialValue: playlistToBeUpdated.Name);
                        playlistToBeUpdated.Name = newValue;
                        break;
                    default:
                        break;
                }
                break;
            }
        }
        await base.UpdateAsync(playlistToBeUpdated);
    }

    public async Task<bool> AddPlaylistSong(int playlistId, int songId)
    {
        if (playlistId <= 0 || songId <= 0)
        {
            return false;
        }

        ObservableCollection<PlaylistSongs> playlistSongs = await GetPlaylistSongsAsync();
        var playlistSong = new PlaylistSongs
        {
            Id = playlistSongs.Count + 1,
            SongId = songId,
            PlaylistId = playlistId
        };

        playlistSongs.Add(playlistSong);

        var json = JsonSerializer.Serialize(playlistSongs);
        await File.WriteAllTextAsync(playlistSongFilePath, json);
        return true;
    }

    public async Task<ObservableCollection<PlaylistSongs>> GetPlaylistSongsAsync()
    {
        if (!File.Exists(playlistSongFilePath))
        {
            return new ObservableCollection<PlaylistSongs>();
        }

        var json = await File.ReadAllTextAsync(playlistSongFilePath);
        var playlistSongs = JsonSerializer.Deserialize<ObservableCollection<PlaylistSongs>>(json);
        return playlistSongs!;
    }

    public async Task<ObservableCollection<PlaylistSongs>> GetPlaylistSongsByPlaylistId(int playlistId)
    {
        if (playlistId <= 0)
        {
            return new ObservableCollection<PlaylistSongs>();
        }

        var playlistSongs = await GetPlaylistSongsAsync();
        return new ObservableCollection<PlaylistSongs>(playlistSongs.Where(playlistSong => playlistSong.PlaylistId == playlistId));
    }
}
