namespace BeatSync.ViewModel.Users;

public partial class CustomerLibraryPageViewModel : ObservableObject
{
    private readonly AdminService adminService;
    private readonly UserService userService;
    private readonly PlaylistService playlistService;

    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();

    public CustomerLibraryPageViewModel(AdminService adminService, UserService userService, PlaylistService playlistService)
    {
        this.adminService = adminService;
        this.userService = userService;
        this.playlistService = playlistService;
    }


    [RelayCommand]
    void Logout()
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
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
        User = await userService.GetCurrentUser();
    }

    public async void LoadPlaylists()
    {
        Playlists = await playlistService.GetPlaylistsByUserAsync(User.Id);
    }

}
