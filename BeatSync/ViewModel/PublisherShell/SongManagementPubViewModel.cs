using BeatSync.Services.Service;

namespace BeatSync.ViewModel.PublisherShell;

public partial class SongManagementPubViewModel : ObservableObject
{
    private readonly SongService _songService;
    private readonly PublisherService _publisherService;
    private readonly ArtistService _artistService;
    private readonly UserService _userService;
    
    [ObservableProperty]
    private ObservableCollection<object> _account = new();

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


    public SongManagementPubViewModel(
        SongService songService, 
        PublisherService publisherService, 
        ArtistService artistService,
        UserService userService
        )
    {
        _songService = songService;
        _publisherService = publisherService;
        _artistService = artistService;
        _userService = userService;
    }

    [RelayCommand]
    async Task NavigateToSearch()
    {
        await Shell.Current.GoToAsync(nameof(SongSearchPage));
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

    /// <summary>
    /// handles both publisher and artist
    /// </summary>
    public async void GetSongsAsync()
    {
        var accountType = Preferences.Get("currentAccountType", -1);
        if (accountType == 2)
        {
            Songs = await _songService.GetActiveAsync();
        }
        else
        {
            var artistId = Preferences.Get("currentUserId", -1);
            Songs = await _songService.GetSongsByArtistIdAsync(artistId);
        }
        System.Diagnostics.Debug.WriteLine($"Albums count: {Songs.Count}");
        foreach (var song in Songs)
        {
            System.Diagnostics.Debug.WriteLine($"album: {song.Name} artist: {song.ArtistName}");
        }
    }

    public async Task LoadCurrentUser()
    {
        var accountType = Preferences.Get("currentAccountType", -1);
        //clears everytime to ensure only one account is displayed
        Account.Clear();
        if (accountType == -1)
        {
            await Shell.Current.DisplayAlert("Error", "Please login to continue", "Ok");
            await Shell.Current.GoToAsync("..");
            return;
        }

        //adding to object collection but only one since it will render multiple accounts if not cleared
        if (accountType == 3)
        {
            Account.Add(await _userService.GetCurrentUser());
        }
        else if (accountType == 2)
        {
            Account.Add(await _publisherService.GetCurrentUser());
        }
        else if (accountType == 1)
        {
            Account.Add(await _artistService.GetCurrentUser());
        }
    }

}
