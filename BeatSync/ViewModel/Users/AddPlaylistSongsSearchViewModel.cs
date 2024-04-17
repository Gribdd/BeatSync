namespace BeatSync.ViewModel.Users;

[QueryProperty(nameof(PlaylistId), nameof(PlaylistId))]
public partial class AddPlaylistSongsSearchViewModel : ObservableObject
{
    private readonly SongService _songService;
    private readonly PlaylistSongService _playlistSongService;
    private readonly PlaylistService _playlistService;

    [ObservableProperty]
    private int _playlistId;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private Song _selectedSong = new();

    private string? _searchQuery = string.Empty;
    public string? SearchQuery
    {
        get => _searchQuery;
        set
        {
            SetProperty(ref _searchQuery, value);
            //call the search method
            SearchSong();
        }
    }

    public AddPlaylistSongsSearchViewModel(
        SongService songService,
        PlaylistSongService playlistSongService,
        PlaylistService playlistService)
    {
        _songService = songService;
        _playlistSongService = playlistSongService;
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
        await _playlistSongService.AddAsync(
            new PlaylistSongs 
            { 
                PlaylistId = PlaylistId, 
                SongId = song.Id
            });
        var playlist = await _playlistService.GetAsync(PlaylistId);
        playlist.SongCount++;
        await _playlistService.UpdateAsync(playlist);
        await Shell.Current.DisplayAlert("Add Song to Playlist", "Song successfully added.", "OK");
        await Shell.Current.GoToAsync("..");

    }

    [RelayCommand]
    async Task SearchSong()
    {
        Songs = await _songService.GetSongsBySearchQuery(SearchQuery);
    }

    public async void GetSongs()
    {
        Songs = await _songService.GetActiveAsync();
        var playlistSongs = await _playlistSongService.GetPlaylistSongsByPlaylistIdAsync(PlaylistId);

        foreach (var playlistSong in playlistSongs)
        {
            var song = Songs.FirstOrDefault(s => s.Id == playlistSong.SongId);
            if (song != null)
                Songs.Remove(song);
        }

    }
}
