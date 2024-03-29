namespace BeatSync.ViewModel.PublisherShell;

[QueryProperty(nameof(Publisher), nameof(Publisher))]
public partial class SongManagementPubViewModel : ObservableObject
{
    private AdminService _adminService;
    private SongService _songService;
    private PublisherService _publisherService;

    [ObservableProperty]
    private MediaSource? _mediaSource;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private ObservableCollection<History> _userHistories = new();

    [ObservableProperty]
    private Song _selectedSong = new Song();
    
    [ObservableProperty]
    private bool _isVisible;

    [ObservableProperty]
    private Publisher _publisher = new();


    public SongManagementPubViewModel(AdminService adminService, SongService songService, PublisherService publisherService)
    {
        _adminService = adminService;
        _songService = songService;
        _publisherService = publisherService;
    }


    [RelayCommand]
    void Logout()
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    [RelayCommand]
    async Task AddSong()
    {
        await Shell.Current.GoToAsync($"{nameof(AddSong)}");
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

        //Calling a method to save user history
        await SaveUserHistoryAsync(song);

        MediaSource = MediaSource.FromFile(song.FilePath);
    }

    [RelayCommand]
    async Task SaveUserHistoryAsync(Song? song)
    {
        int userId = Preferences.Default.Get("currentUserId", -1);
        if(userId <= 0)
        {
            return;
        }

        string songTitle = song!.Name!;
        await _publisherService.SaveUserHistoryAsync(new History
        {
            UserId = userId,
            TimeStamp = DateTime.Now,
            SongName = songTitle
        });
    }

    public async void GetSongsAsync()
    {
        Songs = await _songService.GetActiveSongAsync();
    }
}
