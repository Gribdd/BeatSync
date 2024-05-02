using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Users;

public partial class CustomerLandingPageViewModel : ObservableObject
{
    private readonly UserService _userService;
    private readonly PlaylistService _playlistService;

    [ObservableProperty]
    private ObservableCollection<Playlist> _playlist = new();

    private User _user = new();
    public User User
    {
        get { return _user; }
        set { SetProperty(ref _user, value); }
    }

    public CustomerLandingPageViewModel(UserService userService, PlaylistService playlistService)
    {
        _userService = userService;
        _playlistService = playlistService;   
    }

    [RelayCommand]
    async Task OnProfileIconClicked()
    {
        bool answer = await Shell.Current.DisplayAlert("Logout", "Would you like to log out?", "Yes", "No");
        if (answer)
        {
            Application.Current!.MainPage = new AppShell();
            Preferences.Default.Set("currentUserId", -1);
        }
    }

    public async Task GetActiveCustomer()
    {
        User = await _userService.GetCurrentUser();
    }

    [RelayCommand]
    async Task ViewProfile()
    {
        await GetActiveCustomer();
        // Navigate to the ViewProfile page
        await Shell.Current.Navigation.PushAsync(new ViewProfile(this));
    }

    public async void GetPlaylists()
    {
        Playlist = await _playlistService.GetPlaylistsByUserAsync(User.Id);
    }

    [RelayCommand]
    async Task Return()
    {
        // Navigate back
        await Shell.Current.GoToAsync("..");
    }
}
