using BeatSync.Selector;
using BeatSync.Services.Service;
namespace BeatSync.ViewModel.Users;

public partial class LandingPageViewModel : ObservableObject
{
    private readonly UserService _userService;
    private readonly PublisherService _publisherService;
    private readonly ArtistService _artistService;  
    private readonly SongService _songService;
    private readonly HistoryService _historyService;

    [ObservableProperty]
    private ObservableCollection<Song> _newSongs = new();

    [ObservableProperty]
    private ObservableCollection<Song> _recentlyPlayedSongs = new();

    [ObservableProperty]
    private ObservableCollection<Song> _suggestedSongsByGenre = new();

    //cannot bind to object, so we need to bind to a list of objects
    //workaround kay boang ang maui di mosugot og object
    [ObservableProperty]
    private ObservableCollection<object> _account = new();
    
    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private Publisher _publisher = new();

    [ObservableProperty]
    private Artist _artist = new();

    [ObservableProperty]
    private Song _selectedSong = new();

    [ObservableProperty]
    private MediaSource? _mediaSource;

    [ObservableProperty]
    private bool _isVisible;

    public LandingPageViewModel(
        UserService userService,
        PublisherService publisherService,
        ArtistService artistService,
        SongService songService,
        HistoryService historyService)
    {
        _userService = userService;
        _songService = songService;
        _historyService = historyService;
        _publisherService = publisherService;
        _artistService = artistService; 
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

        //getting accountType and userId so that it can cater multiple accounts for history
        var accountType = Preferences.Get("currentAccountType", -1);
        var accountId = Preferences.Get("currentUserId", -1);

        var history = new History
        {
            AccountType = accountType,
            UserId = accountId,
            SongId = song.Id,
            SongName = song.Name,
            TimeStamp = DateTime.Now
        };

        var histories = await _historyService.GetAllAsync();
        foreach (var historiesItem in histories)
        {
            if (historiesItem.AccountType == history.AccountType && historiesItem.UserId == history.UserId && historiesItem.SongId == history.SongId)
            {
                await _historyService.DeleteAsync(historiesItem.Id);
            }
        }

        await _historyService.AddAsync(history);
        MediaSource = MediaSource.FromFile(song.FilePath);

    }

    public async Task LoadSongsAsync()
    {
        NewSongs = await _songService.GetActiveAsync(); ;
    }

    public async void LoadCurrentUser()
    {
        var accountType = Preferences.Get("currentAccountType", -1);
        //clears everytime to ensure only one account is displayed
        Account.Clear();
        if(accountType == -1)
        {
            await Shell.Current.DisplayAlert("Error", "Please login to continue", "Ok");
            await Shell.Current.GoToAsync("..");
            return;
        }

        //adding to object collection but only one since it will render multiple accounts if not cleared
        if (accountType == 3)
        {
            Account.Add( await _userService.GetCurrentUser());
        }else if(accountType == 2)
        {
            Account.Add(await _publisherService.GetCurrentUser());
        }
        else if (accountType == 1)
        {
            Account.Add(await _artistService.GetCurrentUser());
        }
    }

    public async void LoadSuggestedSongs()
    {
        var histories = await _historyService.GetHistoriesByUserIdAsync(User.Id, User.AccountType);
        var songs = await _songService.GetSongsBySongIds(histories.Select(h => h.SongId).ToList());
        var suggestedSongs = new ObservableCollection<Song>(songs
            .GroupBy(s => s.Genre)
            .OrderByDescending(g => g.Count())
            .Select(g => g.First())
            .ToList());

        SuggestedSongsByGenre = suggestedSongs;
    }
}