
namespace BeatSync.ViewModel.General;

public partial class SongSearchPageViewModel: ObservableObject
{
    private readonly SongService _songService;

    [ObservableProperty]
    private ObservableCollection<Song> _songs = new();

    [ObservableProperty]
    private ObservableCollection<Song> _searchResults = new();

    [ObservableProperty]
    private bool _isResultsVisible = false;

    private string? _searchQuery;
    public string? SearchQuery
    {
        get => _searchQuery;
        set
        {
            SetProperty(ref _searchQuery, value);
            Search();
        }
    }

    public SongSearchPageViewModel(SongService songService)
    {
        _songService = songService;
    }


    [RelayCommand]
    async Task Return()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    void Search()
    {
        SearchResults.Clear();
        IsResultsVisible = true;
        foreach (var song in Songs)
        {
            if (song.Name!.Contains(SearchQuery!, StringComparison.OrdinalIgnoreCase))
            {
                SearchResults.Add(song);
            }
        }
    }

    public async Task GetSongs()
    {
        var accountType = Preferences.Get("currentAccountType", -1);
        if (accountType == 1)
        {
            var artistId = Preferences.Get("currentUserId", -1);
            Songs = await _songService.GetSongsByArtistIdAsync(artistId);
        }else if(accountType == 2)
        {
            var artistId = Preferences.Get("currentUserId", -1);
            Songs = await _songService.GetSongsByArtistIdAsync(artistId);
        }
        else
        {
            Songs = await _songService.GetActiveAsync();
        }
        
    }
}
