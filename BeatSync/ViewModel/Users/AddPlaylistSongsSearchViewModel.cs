
namespace BeatSync.ViewModel.Users;

[QueryProperty(nameof(PlaylistId), nameof(PlaylistId))]
public partial class AddPlaylistSongsSearchViewModel : ObservableObject
{
    private readonly SongService _songService;  
    private readonly PlaylistService _playlistService;

    [ObservableProperty]
    private int _playlistId;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private Song _selectedSong = new();

    [ObservableProperty]
    private string? _searchQuery;

    public AddPlaylistSongsSearchViewModel(SongService songService, PlaylistService playlistService)
    {
        _songService = songService;
        _playlistService = playlistService;
    }

    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task AddToPlaylistSongs(Song song)
    {
        if (await _playlistService.AddPlaylistSong(PlaylistId, song.Id))
        {
            await Shell.Current.DisplayAlert("Add Song to Playlist", "Song successfully added.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Something went wrong.", "OK");
        }
    }

    [RelayCommand]
    async Task SearchSong()
    {
        Songs = await _songService.GetSongsBySearchQuery(SearchQuery);
    }

    public async void GetSongs()
    {
        Songs = await _songService.GetActiveAsync();
    }
}
