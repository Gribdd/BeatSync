namespace BeatSync.ViewModel.Users;

public partial class CustomerLibraryPageViewModel : ObservableObject
{
    private readonly UserService _userService;
    private readonly PlaylistService _playlistService;

    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();

    public CustomerLibraryPageViewModel(
        UserService userService, 
        PlaylistService playlistService)
    {
        _userService = userService;
        _playlistService = playlistService;
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
}
