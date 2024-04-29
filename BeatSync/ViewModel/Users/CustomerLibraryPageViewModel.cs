namespace BeatSync.ViewModel.Users;

public partial class CustomerLibraryPageViewModel : ObservableObject
{
    private readonly UserService _userService;
    private readonly PlaylistService _playlistService;
    private readonly SongService _songService;
    private readonly HistoryService _historyService;
    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();

    [ObservableProperty]
    private ObservableCollection<Song> _favoriteSongs = new();

    [ObservableProperty]
    private ObservableCollection<Song> _recentlyPlayedSongs = new();

    public CustomerLibraryPageViewModel(
        UserService userService,
        PlaylistService playlistService,
        SongService songService,
        HistoryService historyService)
    {
        _userService = userService;
        _playlistService = playlistService;
        _songService = songService;
        _historyService = historyService;
    }


    [RelayCommand]
    void Logout()
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    async Task UpdatePlaylist()
    {
        string playlistName = await Shell.Current.DisplayPromptAsync("Update Playlist", "Enter Playlist Name to update:");
        if (!string.IsNullOrEmpty(playlistName))
        {
            var playlist = await _playlistService.GetByNameAsync(playlistName, User.Id);
            await _playlistService.UpdateAsync(playlist.Id);
        }
        Playlists = await _playlistService.GetPlaylistsByUserAsync(User.Id);
    }

    [RelayCommand]
    async Task DeletePlaylist()
    {
        string playlistName = await Shell.Current.DisplayPromptAsync("Delete Playlist", "Enter Playlist Name to delete:");
        if (!string.IsNullOrEmpty(playlistName))
        {
            var playlist = await _playlistService.GetByNameAsync(playlistName, User.Id);
            await _playlistService.DeleteAsync(playlist.Id);
        }
        Playlists = await _playlistService.GetPlaylistsByUserAsync(User.Id);
    }

    [RelayCommand]
    async Task NavigateAddPlaylist()
    {
        await Shell.Current.GoToAsync($"{nameof(AddPlaylistCustomer)}");
    }

    [RelayCommand]
    async Task NavigateAddPlaylistSongs(Playlist playlist)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(Playlist), playlist },
            {nameof(User), User}
        };
        await Shell.Current.GoToAsync($"{nameof(AddPlaylistSongsCustomer)}", navigationParameter);
    }

    public async Task LoadCurrentUser()
    {
        User = await _userService.GetCurrentUser();
    }

    public async void LoadPlaylists()
    {
        Playlists = await _playlistService.GetPlaylistsByUserAsync(User.Id);
    }

    public  void LoadFavoriteSongs() //removed async for now
    {
        //FavoriteSongs = await _songService.GetSongsBySongIds(User.FavoriteSongsId);
    }

    public async void LoadRecentlyPlayedSongs()
    {
        var histories = await _historyService.GetActiveAsync();
        var filteredHistories = histories.Where(h => h.UserId == User.Id).OrderBy(h => h.TimeStamp);
        List<int> songIds = filteredHistories.Select(h => h.SongId).Distinct().ToList();
        var songs = await _songService.GetSongsBySongIds(songIds);
        var sortedSongs = new ObservableCollection<BeatSync.Models.Song>(songs.Where(s => songIds.Contains(s.Id)).OrderBy(s => filteredHistories.First(h => h.SongId == s.Id).TimeStamp).Reverse());
        RecentlyPlayedSongs = sortedSongs;
    }

    [RelayCommand]
    async Task NavigateRecentlyPlayed()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(RecentlyPlayedSongs), RecentlyPlayedSongs }
        };
        await Shell.Current.GoToAsync(nameof(CustomerRecentlyPlayed),navigationParameter);
    }

}
