namespace BeatSync.ViewModel.PublisherShell;

[QueryProperty(nameof(Publisher), nameof(Publisher))]
public partial class LibraryPageViewModel : ObservableObject
{
    private AlbumService albumService;

    [ObservableProperty]
    private ObservableCollection<Album> _albums = new();

    [ObservableProperty]
    private Album _selectedAlbum = new();

    [ObservableProperty]
    private Publisher _publisher = new();
    public LibraryPageViewModel(AlbumService albumService)
    {
        this.albumService = albumService;
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

            Albums = await albumService.UpdateAlbumAsync(albumName);
        }
    }

    [RelayCommand]
    async Task DeleteAlbum()
    {
        string albumName = await Shell.Current.DisplayPromptAsync("Delete Album", "Enter Album Name to delete:");
        if (!string.IsNullOrEmpty(albumName))
        {
            Albums = await albumService.DeleteAlbumAsync(albumName);
        }
    }
    

    [RelayCommand]
    async Task NavigateToAddAlbumSongs(Album album)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(Album), album }
        };
        await Shell.Current.GoToAsync($"{nameof(AddAlbumSongs)}",navigationParameter);
    }

    [RelayCommand]
    void Logout()
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    public async void GetAlbums()
    {
        Albums = await albumService.GetAlbumsAsync();
    }
}
