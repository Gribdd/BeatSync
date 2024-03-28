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
    async Task AddAlbumSongs()
    {
        await Shell.Current.DisplayAlert("Test","Test","OK");
    }
}
