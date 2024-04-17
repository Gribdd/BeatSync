using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Users;

public partial class LandingPageViewModel : ObservableObject
{
    private readonly UserService _userService;
    private readonly SongService _songService;
	
	[ObservableProperty]
	private ObservableCollection<Song> _newSongs = new();

	[ObservableProperty]
	private User _user = new();

    [ObservableProperty]
    private Song _selectedSong = new();

    [ObservableProperty]
    private MediaSource? _mediaSource;

    [ObservableProperty]
    private bool _isVisible;

    public LandingPageViewModel(
        UserService userService, SongService songService)
    {
        _userService = userService;
        _songService = songService;
    }

    [RelayCommand]
	void Logout()
	{
		Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    void PlaySong(Song song)
    {
        if (song.FilePath == null)
        {
            return;
        }

        SelectedSong = song;
        if (!IsVisible)
        {
            IsVisible = true;
        }

        MediaSource = MediaSource.FromFile(song.FilePath);
    }

    public async Task LoadSongsAsync()
    {
        NewSongs = await _songService.GetActiveAsync(); ;
    }

    public async void LoadCurrentUser()
    {
        User = await _userService.GetCurrentUser();
    }
}