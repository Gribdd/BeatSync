
namespace BeatSync.ViewModel.Users;

[QueryProperty(nameof(Playlist), nameof(Playlist))]
[QueryProperty(nameof(User), nameof(User))]
public partial class AddPlaylistSongsCustomerViewModel : ObservableObject
{
    private readonly PlaylistService playlistService;
    private readonly SongService songService;

    [ObservableProperty]
    private Playlist _playlist = new();

    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private ObservableCollection<PlaylistSongs> _playlistSongs = new();

    public AddPlaylistSongsCustomerViewModel(PlaylistService playlistService, SongService songService)
    {
        this.playlistService = playlistService;
        this.songService = songService;
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

    public async Task GetSongsByPlaylistId()
    {
        var songIds = PlaylistSongs.Select(playlistSong => playlistSong.SongId).ToList();
        Songs = await songService.GetSongsBySongIds(songIds);
    }

    public async Task GetPlaylistSongsPlaylistId()
    {
        PlaylistSongs = await playlistService.GetPlaylistSongsByPlaylistId(Playlist.Id);
    }
    
}
