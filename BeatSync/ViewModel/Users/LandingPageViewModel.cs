namespace BeatSync.ViewModel.Users;

public partial class LandingPageViewModel : ObservableObject
{
	private AdminService _adminService;
	
	[ObservableProperty]
	public ObservableCollection<Song> _newSongs = new();
	public LandingPageViewModel(AdminService adminService)
	{
		_adminService = adminService;
		LoadSongsAsync();
	}

	[RelayCommand]
	async Task Logout()
	{
        await _adminService.Logout();
    }

    public async Task LoadSongsAsync()
    {
        var songService = new SongService();
        var songs = await songService.GetActiveSongAsync();
        NewSongs = songs;
    }

}