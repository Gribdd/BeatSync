namespace BeatSync.ViewModel.PublisherShell;

public partial class LibraryPageViewModel : ObservableObject
{
    private AdminService adminService;
    private AlbumService albumService;

    [ObservableProperty]
    private ObservableCollection<Album> _albums = new();

    [ObservableProperty]
    private Album _selectedAlbum = new();

    public LibraryPageViewModel(AdminService adminService, AlbumService albumService)
    {
        this.adminService = adminService;
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
    async Task Logout()
    {
        await adminService!.Logout();
    }

    public async void GetAlbums()
    {
        Albums = await albumService.GetAlbumsAsync();
    }
}
