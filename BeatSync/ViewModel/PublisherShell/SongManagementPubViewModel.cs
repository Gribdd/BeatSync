namespace BeatSync.ViewModel.PublisherShell;

public partial class SongManagementPubViewModel : ObservableObject
{

    private SongService _songService;
    private PublisherService _publisherService;
    private PubUserHistoryViewModel _pubUserHistoryViewModel;

    [ObservableProperty]
    private MediaSource? _mediaSource;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private Song _selectedSong = new Song();

    [ObservableProperty]
    private bool _isVisible;


    public SongManagementPubViewModel(AdminService adminService, SongService songService, PublisherService publisherService, PubUserHistoryViewModel pubUserHistoryViewModel)
    {
        _adminService = adminService;
        _songService = songService;
        _publisherService = publisherService;
        _pubUserHistoryViewModel = pubUserHistoryViewModel;
    }


    [RelayCommand]
    async Task Logout()
    {

        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    async Task AddSong()
    {
        await Shell.Current.GoToAsync($"{nameof(AddSong)}");
    }

    [RelayCommand]
    Task PlaySong(Song song)
    {
        if (song.FilePath == null)
        {
            return Task.CompletedTask;
        }

        _selectedSong = song;
        if (!IsVisible)
        {
            IsVisible = true;
        }

        //Calling a method to save user history
        SaveUserHistoryAsync(song);

        MediaSource = MediaSource.FromFile(song.FilePath);
        return Task.CompletedTask;
    }

    public async void GetSongsAsync()
    {
        var songsList = await _songService.GetActiveSongAsync();
        Songs = new ObservableCollection<Song>(songsList);
    }
    [RelayCommand]
    public async Task SaveUserHistoryAsync(Song song)
    {
        int userId = _publisherService.GetCurrentUser().Id;
        string songTitle = song.Name;
        var userHistories = await _publisherService.LoadUserHistoriesAsync();
        int newHistoryId = userHistories.Count + 1;
        await _publisherService.SaveUserHistoryAsync(new History
        {
            Id = newHistoryId,
            UserId = userId,
            TimeStamp = DateTime.Now,
            SongName = songTitle
        });
        await GetSavedHistories(); 
        
    }

    [ObservableProperty]
    private ObservableCollection<History> _userHistories = new();
    

    [RelayCommand]
    public async Task GetSavedHistories()
    {
        UserHistories = new ObservableCollection<History>(await _publisherService.LoadUserHistoriesAsync());
    }
}
