namespace BeatSync.ViewModel.PublisherShell;

public partial class LibraryPageViewModel : ObservableObject
{
    private readonly AlbumService albumService;
    private readonly PublisherService publisherService;

    [ObservableProperty]
    private ObservableCollection<Album> _albums = new();

    [ObservableProperty]
    private Album _selectedAlbum = new();

    [ObservableProperty]
    private Publisher _publisher = new();

    public LibraryPageViewModel(AlbumService albumService, PublisherService publisherService)
    {
        this.albumService = albumService;
        this.publisherService = publisherService;
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
        if (string.IsNullOrEmpty(albumName))
        {
            return;
        }
        Albums = await albumService.UpdateAlbumAsync(albumName);
    }

    [RelayCommand]
    async Task DeleteAlbum()
    {
        string albumName = await Shell.Current.DisplayPromptAsync("Delete Album", "Enter Album Name to delete:");
        if (string.IsNullOrEmpty(albumName))
        {
            return;
        }
        Albums = await albumService.DeleteAlbumAsync(albumName);
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
        Albums = await albumService.GetActiveAlbumsAsync();
    }

    public async void GetActivePublisher()
    {
        Publisher = await publisherService.GetCurrentUser();
    }
}
