namespace BeatSync.ViewModel.PublisherShell;

public partial class LibraryPageViewModel : ObservableObject
{
    private readonly AlbumService _albumService;
    private readonly PublisherService _publisherService;

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

    public LibraryPageViewModel(AlbumService albumService, PublisherService publisherService)
    {
        _albumService = albumService;
        _publisherService = publisherService;
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

    public async void GetAlbums()
    {
        Albums = await _albumService.GetActiveAsync();
    }

    public async void GetActivePublisher()
    {
        Publisher = await _publisherService.GetCurrentUser();
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
