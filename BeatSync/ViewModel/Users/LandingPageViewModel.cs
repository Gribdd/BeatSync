using BeatSync.Services.Service;

namespace BeatSync.ViewModel.Users;

public partial class LandingPageViewModel : ObservableObject
{
    private readonly UserService _userService;
    private readonly SongService _songService;
    private readonly HistoryService historyService;
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
        UserService userService, 
        SongService songService,
        HistoryService historyService)
    {
        _userService = userService;
        _songService = songService;
        this.historyService = historyService;
    }

    [RelayCommand]
	void Logout()
	{
		Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    async Task PlaySong(Song song)
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

        var history = new History
        {
            AccountType = User.AccountType,
            UserId = User.Id,
            SongId = song.Id,
            SongName = song.Name,
            TimeStamp = DateTime.Now
        };

        var histories = await historyService.GetAllAsync();
        foreach (var historiesItem in histories)
        {
            if (historiesItem.AccountType == history.AccountType && historiesItem.UserId == history.UserId && historiesItem.SongId == history.SongId)
            {
                await historyService.DeleteAsync(historiesItem.Id);
            }
        }
        await historyService.AddAsync(history);
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