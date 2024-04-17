namespace BeatSync.ViewModel.Users;

[QueryProperty(nameof(Playlist), nameof(Playlist))]
[QueryProperty(nameof(User), nameof(User))]
public partial class AddPlaylistSongsCustomerViewModel : ObservableObject
{
    private readonly PlaylistService _playlistService;
    private readonly PlaylistSongService _playlistSongService;
    private readonly SongService _songService;
    private readonly UserService _userService;

    [ObservableProperty]
    private Playlist _playlist = new();

    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private ObservableCollection<PlaylistSongs> _playlistSongs = new();

    public AddPlaylistSongsCustomerViewModel(
        PlaylistService playlistService,
        PlaylistSongService playlistSongService,
        SongService songService,
        UserService userService)
    {
        _playlistService = playlistService;
        _playlistSongService = playlistSongService;
        _songService = songService;
        _userService = userService;
    }

    [RelayCommand]
    async Task Logout()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task NavigateToAddPlaylistSongsSearch()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {"PlaylistId", Playlist.Id },
        };

        await Shell.Current.GoToAsync($"{nameof(AddPlaylistSongsSearch)}", navigationParameter);
    }


    [RelayCommand]
    async Task AddSongToFavorites(Song song)
    {
        User.FavoriteSongsId.Add(song.Id);
        await _userService.UpdateAsync(User);
    }

    public bool IsFavorite(Song song)
    {
        return User.FavoriteSongsId.Contains(song.Id);
    }


    [RelayCommand]
    async Task RemoveSongFromPlaylist(Song song)
    {
        bool canDelete = await Shell.Current.DisplayAlert("Remove Song", "Are you sure you want to remove this song from the playlist?", "Yes", "No");
        var playlistSong = PlaylistSongs.FirstOrDefault(playlistSong => playlistSong.SongId == song.Id);
        if (playlistSong != null && canDelete)
        {
            await _playlistSongService.DeleteAsync(playlistSong.Id);
            Playlist.SongCount -= 1;
            await _playlistService.UpdateAsync(Playlist);
            PlaylistSongs.Remove(playlistSong);
            Songs.Remove(song);

        }
    }

    public async Task GetSongsByPlaylistId()
    {
        var songIds = PlaylistSongs.Select(playlistSong => playlistSong.SongId).ToList();
        Songs = await _songService.GetSongsBySongIds(songIds);
    }

    public async Task GetPlaylistSongsPlaylistId()
    {
        PlaylistSongs = await _playlistSongService.GetPlaylistSongsByPlaylistIdAsync(Playlist.Id);
    }
    
}
