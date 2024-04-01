
namespace BeatSync.Services;

public partial class PlaylistService
{
    private readonly string playlistFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Playlists.json");
    private readonly string playlistSongFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "PlaylistSongs.json");

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
        if(activePlaylist.Count == 0)
        {
            return new ObservableCollection<Playlist>();
        }
        return new ObservableCollection<Playlist>(activePlaylist.Where(m => m.UserId == userId));
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

        var json = JsonSerializer.Serialize<ObservableCollection<PlaylistSongs>>(playlistSongs);
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
