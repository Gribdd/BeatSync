namespace BeatSync.ViewModel.General;


public partial class AlbumSearchPageViewModel : ObservableObject
{
    private readonly AlbumService _albumService;

    [ObservableProperty]
    private ObservableCollection<Album> _albums = new();

    [ObservableProperty]
    private ObservableCollection<Album> _searchResults = new();

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


    public AlbumSearchPageViewModel(AlbumService albumService)
    {
        _albumService = albumService;
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
        foreach (var album in Albums)
        {
            if (album.Name!.Contains(SearchQuery!, StringComparison.OrdinalIgnoreCase))
            {
                SearchResults.Add(album);
            }
        }
    }

    public async Task GetAlbums()
    {
        var accountType = Preferences.Get("currentAccountType", -1);
        if (accountType == 1)
        {
            var artistId = Preferences.Get("currentUserId", -1);
            Albums = await _albumService.GetByArtistId(artistId);
        }
        else
        {
            Albums = await _albumService.GetActiveAsync();
        }
    }
}
