namespace BeatSync.ViewModel.PublisherShell;

public partial class LibraryPageViewModel : ObservableObject
{
    private readonly AlbumService _albumService;
    private readonly PublisherService _publisherService;
    private readonly ArtistService _artistService;
    private readonly UserService _userService;

    //cannot bind to object, so we need to bind to a list of objects
    //workaround kay boang ang maui di mosugot og object
    [ObservableProperty]
    private ObservableCollection<object> _account = new();

    [ObservableProperty]
    private ObservableCollection<Album> _albums = new();

    [ObservableProperty]
    private Album _selectedAlbum = new();

    [ObservableProperty]
    private Publisher _publisher = new();

    [ObservableProperty]
    private ObservableCollection<Song> _favoriteSongs = new();
    [ObservableProperty]
    private ObservableCollection<Song> _recentlyPlayedSongs = new();

    public LibraryPageViewModel(
        AlbumService albumService,
        PublisherService publisherService,
        ArtistService artistService,
        UserService userService)
    {
        _albumService = albumService;
        _publisherService = publisherService;
        _artistService = artistService;
        _userService = userService;
    }


    [RelayCommand]
    async Task AddAlbum()
    {
        await Shell.Current.GoToAsync($"{nameof(AddAlbumPublisher)}");
    }

    [RelayCommand]
    async Task UpdateAlbum()
    {
        string albumName = await Shell.Current.DisplayPromptAsync("Update Album", "Enter Album Name to update:");
        if (!string.IsNullOrEmpty(albumName))
        {
            var album = await _albumService.GetByNameAsync(albumName);
            await _albumService.UpdateAsync(album.Id);
        }
        Albums = await _albumService.GetActiveAsync();
    }

    [RelayCommand]
    async Task DeleteAlbum()
    {
        string albumName = await Shell.Current.DisplayPromptAsync("Delete Album", "Enter Album Name to delete:");
        if (!string.IsNullOrEmpty(albumName))
        {
            var album = await _albumService.GetByNameAsync(albumName);
            await _albumService.DeleteAsync(album.Id);
        }
        Albums = await _albumService.GetActiveAsync();
    }


    [RelayCommand]
    async Task NavigateToAddAlbumSongs(Album album)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(Album), album }
        };
        await Shell.Current.GoToAsync($"{nameof(AddAlbumSongs)}", navigationParameter);
    }

    [RelayCommand]
    void Logout()
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    public async Task GetAlbums()
    {
        var accountType = Preferences.Get("currentAccountType", -1);
        if (accountType == 2)
        {
            Albums = await _albumService.GetActiveAsync();
        }
        else
        {
            var artistId = Preferences.Get("currentUserId", -1);
            Albums = await _albumService.GetByArtistId(artistId);
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

    [RelayCommand]
    public async Task NavigateRecentlyPlayedCommand()
    {
        await Shell.Current.GoToAsync(nameof(PubRecentlyPlayed));
    }

    public async void LoadFavoriteSongs()
    {
        //FavoriteSongs = await _songService.GetSongsBySongIds(User.FavoriteSongsId);
    }

}
