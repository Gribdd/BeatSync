namespace BeatSync.ViewModel.Users;

public partial class LandingPageViewModel : ObservableObject
{
    private readonly UserService userService;
	
	[ObservableProperty]
	private ObservableCollection<Song> _newSongs = new();

	[ObservableProperty]
	private User _user = new();

    public LandingPageViewModel(UserService userService)
	{
		LoadSongsAsync();
        this.userService = userService;
    }

	[RelayCommand]
	void Logout()
	{
		Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    public async Task LoadSongsAsync()
    {
        var songService = new SongService();
        var songs = await songService.GetActiveSongAsync();
        NewSongs = songs;
    }

    public async void LoadCurrentUser()
    {
        User = await userService.GetCurrentUser();
    }
}