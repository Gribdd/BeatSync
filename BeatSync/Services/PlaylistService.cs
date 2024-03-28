namespace BeatSync.Services;

public partial class PlaylistService
{
    private readonly string playlistFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Playlists.json");

    public async Task<bool> AddPlaylist(Playlist playlist)
    {
        if (playlist == null)
        {
            return false;
        }

        ObservableCollection<Playlist> playlists = await GetPlaylistsAsync();
        playlist.Id = playlists.Count + 1;
        playlists.Add(playlist);


        var json = JsonSerializer.Serialize<ObservableCollection<Playlist>>(playlists);
        await File.WriteAllTextAsync(playlistFilePath, json);
        return true;
    }

    public async Task<ObservableCollection<Playlist>> GetPlaylistsAsync()
    {
        if (!File.Exists(playlistFilePath))
        {
            return new ObservableCollection<Playlist>();
        }

        var json = await File.ReadAllTextAsync(playlistFilePath);
        var playlists = JsonSerializer.Deserialize<ObservableCollection<Playlist>>(json);
        return playlists!;
    }

    public async Task<ObservableCollection<Playlist>> GetActivePlaylistAsync()
    {
        var playlists = await GetPlaylistsAsync();
        return new ObservableCollection<Playlist>(playlists.Where(m => !m.IsDeleted));
    }

    public async Task<ObservableCollection<Playlist>> GetPlaylistsByUserAsync(int userId)
    {
        var activePlaylist = await GetActivePlaylistAsync();
        return new ObservableCollection<Playlist>(activePlaylist.Where(m => m.UserId == userId));
    }


}
